using Login.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Login.Extencions
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        protected readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var message = string.Empty;
                var localErro = string.Empty;

                var sTackTrace = new StackTrace(error);
                var thisasm = Assembly.GetExecutingAssembly();
                var methodName = sTackTrace.GetFrames().Select(f => f.GetMethod()).First(m => m.Module.Assembly == thisasm).ToString();

                switch (error)
                {
                    case DomainException e:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        message = error.Message;
                        localErro = "DomainException: " + methodName;
                        break;
                    case SecurityTokenExpiredException:
                    case SecurityTokenInvalidSignatureException:
                    case ArgumentException:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        message = ErrorMessages.TOKEN_ERRO;
                        localErro = "Token Security: " + methodName;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        message = ErrorMessages.ERRO_GENERICO;
                        localErro = "Metodo: " + methodName;
                        break;
                }
                _logger.LogError(error, error.Message);
                var result = JsonSerializer.Serialize(new { message = message });
                await response.WriteAsync(result);
            }
        }
    }
}
