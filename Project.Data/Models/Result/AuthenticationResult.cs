using System;

namespace Project.Data.Models
{
    public class AuthenticationResult
    {
        public string access_token { get; set; }
        public TimeSpan expires_in { get; set; }
    }
}
