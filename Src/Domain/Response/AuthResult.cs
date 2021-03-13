using System.Collections.Generic;
using System.Linq;

namespace Domain.Identity.Response
{
    public class AuthResult
    {
        internal AuthResult(bool succeeded, IEnumerable<string> errors, string token)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Token = token;
        }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public string Token { get; set; }

        public static AuthResult Success()
        {
            return new AuthResult(true, new string[] { }, string.Empty);
        }

        public static AuthResult ResponseToken(string token)
        {
            return new AuthResult(true, new string[] { }, token);
        }

        public static AuthResult Failure(IEnumerable<string> errors)
        {
            return new AuthResult(false, errors, string.Empty);
        }
    }
}
