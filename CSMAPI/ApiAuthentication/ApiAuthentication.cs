using System.Net;

namespace CSMAPI.ApiAuthentication
{
    public class ApiAuthentication
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IConfiguration _config;

        public ApiAuthentication(RequestDelegate requestDelegate, IConfiguration config)
        {
            _requestDelegate = requestDelegate;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiConstants.APIPublicKey, out var actualValue))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("\tApi Key is missing!");
                return;
            }

            if (!actualValue.Equals(_config.GetValue<string>(ApiConstants.APISecretKey)))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("\tApi Key is invalid!");
                return;
            }

            await _requestDelegate(context);
        }
    }
}
