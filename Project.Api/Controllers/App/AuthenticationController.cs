using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ginger.Authentication.Jwt;
using Project.Data.Models;
using Project.Service;
using Microsoft.AspNetCore.Mvc;

namespace Project.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/authenticate")]
    public class AuthenticationController : Controller
    {
        private readonly IErrorLogService _errorLogService;
        private readonly IUsersService _usersService;

        public AuthenticationController(IErrorLogService errorLogService, IUsersService usersService)
        {
            _errorLogService = errorLogService;
            _usersService = usersService;
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<AuthenticationResult>> Authenticate([FromBody]Users users)
        {
            try
            {
                var user = (await _usersService.Where(m => (m.Email.Trim().ToLower() == users.Email.Trim().ToLower() || m.UserName.Trim().ToLower() == users.Email.Trim().ToLower()) &&  m.Password == users.Password && m.UserTypeCode == users.UserTypeCode)).FirstOrDefault();

                // If user not found
                if (user == null)
                    return new Result<AuthenticationResult>(false, System.Net.HttpStatusCode.NotFound, Message.NotFound);

                // If user's email is not verified
                if (!user.IsEmailVerified)
                    return new Result<AuthenticationResult>(false, System.Net.HttpStatusCode.Unauthorized, Message.VrifyEmail);

                // If user is inactive or deleted or blocked
                if (!user.IsActive || user.IsDeleted || user.IsBlocked)
                    return new Result<AuthenticationResult>(false, System.Net.HttpStatusCode.Unauthorized, Message.UserBlocked);

                return new Result<AuthenticationResult>(true, System.Net.HttpStatusCode.OK, new AuthenticationResult()
                {
                    access_token = new JwtTokenBuilder().AddClaims(GetClaim(user.Email, user.UserTypeId, user.UserTypeCode)).Build().Value,
                    expires_in = new TimeSpan(0, 5, 0)
                });
            }

            catch (Exception ex)
            {
                await _errorLogService.LogExceptionAsnc(ex);
                return new Result<AuthenticationResult>(false, System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        #region Private
        private Dictionary<string, string> GetClaim(string email, long userId, string role)
        {
            var claim = new Dictionary<string, string>();
            claim.Add("Email", email);
            claim.Add("Sid", userId.ToString());
            claim.Add("Role", role);
            return claim;
        }
        #endregion
    }
}