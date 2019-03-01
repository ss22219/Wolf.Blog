using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;

namespace BlogWeb.Filters
{
    public class AdminRoleAttribute : ActionFilterAttribute
    {
        private string userName;
        private string password;
        public AdminRoleAttribute()
        {
            userName = Startup.Configuration.GetSection("Application").GetSection("UserName").Get<string>();
            password = Startup.Configuration.GetSection("Application").GetSection("Password").Get<string>();
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;
            string auth = request.Headers["Authorization"];
            if (string.IsNullOrEmpty(auth))
            {
                response.Headers.Add("WWW-Authenticate", new Microsoft.Extensions.Primitives.StringValues("Basic"));
                context.Result = new StatusCodeResult(401);
                return;
            }

            string encodedAuth = null;
            if (auth.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                encodedAuth = auth.Substring("Basic ".Length).Trim();
            }

            if (string.IsNullOrEmpty(encodedAuth))
            {
                response.Headers.Add("WWW-Authenticate", new Microsoft.Extensions.Primitives.StringValues("Basic"));
                context.Result = new StatusCodeResult(401);
                return;
            }

            var userpass = DecodeUserIdAndPassword(encodedAuth);
            if (userpass.userid != userName || userpass.password != password)
            {
                response.Headers.Add("WWW-Authenticate", new Microsoft.Extensions.Primitives.StringValues("Basic"));
                context.Result = new StatusCodeResult(401);
                return;
            }
            base.OnActionExecuting(context);
        }

        private static (string userid, string password) DecodeUserIdAndPassword(string encodedAuth)
        {
            var userpass = Encoding.UTF8.GetString(Convert.FromBase64String(encodedAuth));

            var separator = userpass.IndexOf(':');
            if (separator == -1) throw new InvalidOperationException("Invalid Authorization header: Missing separator character ':'. See RFC2617.");

            return (userpass.Substring(0, separator), userpass.Substring(separator + 1));
        }
    }
}
