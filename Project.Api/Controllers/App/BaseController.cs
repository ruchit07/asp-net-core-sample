using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Data.Models;
using Project.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Project.Api.Controllers
{
    public class BaseController<T, TService> : Controller where T : class
    {
        protected readonly IService<T> _service;
        protected readonly IErrorLogService _errorLogService;

        public BaseController(IService<T> service, IErrorLogService errorLogService)
        {
            _service = service;
            _errorLogService = errorLogService;
        }

        #region 'CRUD Api'
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<Result<IEnumerable<T>>> GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Failed<IEnumerable<T>>();

                return Succeeded(await _service.GetAllAsync());
            }
            catch (Exception ex)
            {
                await _errorLogService.LogExceptionAsnc(ex);
                return Failed<IEnumerable<T>>();
            }
        }

        /// <summary>
        /// Get record by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]   
        public virtual async Task<Result<T>> Get(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Failed<T>();

                return Succeeded(await _service.GetAsync(id));
            }
            catch (Exception ex)
            {
                await _errorLogService.LogExceptionAsnc(ex);
                return Failed<T>();
            }
        }

        /// <summary>
        /// Create new record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<Result<T>> Create([FromBody] T model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest<T>(FetchModelError(ModelState));

                return Succeeded(await _service.AddAsync(model), Message.AddSuccess);
            }
            catch (Exception ex)
            {
                await _errorLogService.LogExceptionAsnc(ex);
                return Failed<T>();
            }
        }

        /// <summary>
        /// Update item by id
        /// </summary>
        /// <param name="model">Record to update</param>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public virtual async Task<Result<T>> Update([FromBody] T model, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest<T>(FetchModelError(ModelState));

                var entity = await _service.GetAsync(id);
                if (entity == null)
                    return NotFound<T>(Message.UpdateError);

                return Succeeded(await _service.UpdateAsync(model, id), Message.UpdateSuccess);
            }
            catch (Exception ex)
            {
                await _errorLogService.LogExceptionAsnc(ex);
                return Failed<T>();
            }
        }

        /// <summary>
        /// Delete record by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual async Task<Result<T>> Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest<T>(FetchModelError(ModelState));

                var entity = await _service.GetAsync(id);
                if (entity == null)
                    return NotFound<T>(Message.DeleteError);

                await _service.DeleteAsync(entity);

                return Succeeded<T>(Message.DeleteSuccess);
            }
            catch (Exception ex)
            {
                await _errorLogService.LogExceptionAsnc(ex);
                return Failed<T>();
            }
        }
        #endregion

        #region 'No Action'
        [NonAction]
        protected Result<T> Succeeded<T>(T result) where T : class
        {
            return new Result<T>(Status.Succeeded, System.Net.HttpStatusCode.OK, result);
        }

        protected Result<T> Succeeded<T>(string message) where T : class
        {
            return new Result<T>(Status.Succeeded, System.Net.HttpStatusCode.OK, message);
        }

        protected Result<T> Succeeded<T>(T result, string message) where T : class
        {
            return new Result<T>(Status.Succeeded, System.Net.HttpStatusCode.OK, message, result);
        }

        [NonAction]
        protected Result<T> Failed<T>() where T : class
        {
            return new Result<T>(Status.Failed, System.Net.HttpStatusCode.InternalServerError, Message.SomethingWentWrong);
        }

        [NonAction]
        protected Result<T> Failed<T>(string message) where T : class
        {
            if (string.IsNullOrEmpty(message))
                message = Message.SomethingWentWrong;

            return new Result<T>(Status.Failed, System.Net.HttpStatusCode.InternalServerError, message);
        }

        [NonAction]
        protected Result<T> BadRequest<T>(string message) where T : class
        {
            return new Result<T>(Status.Failed, System.Net.HttpStatusCode.BadRequest, message);
        }

        [NonAction]
        protected Result<T> Unauthorized<T>() where T : class
        {
            return new Result<T>(Status.Failed, System.Net.HttpStatusCode.Unauthorized, Message.Unauthorized);
        }

        [NonAction]
        protected Result<T> NotFound<T>() where T : class
        {
            return new Result<T>(Status.Failed, System.Net.HttpStatusCode.NotFound, Message.NotFound);
        }

        [NonAction]
        protected Result<T> NotFound<T>(string message) where T : class
        {
            return new Result<T>(Status.Failed, System.Net.HttpStatusCode.NotFound, message);
        }

        [NonAction]
        protected string FetchModelError(ModelStateDictionary modelState)
        {
            return string.Join(Environment.NewLine, ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage));
        }
        #endregion
    }
}