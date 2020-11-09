using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using TesteBackEndWebMotors.Library.CustomException;
using TesteBackEndWebMotors.ViewModel;

namespace TesteBackEndWebMotors.Util
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ErroViewModel erro = null;

            if (exception is NossaException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                erro = new ErroViewModel(exception.Message);
            }
            else if (exception is Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                erro = new ErroViewModel("Ocorreu uma falha interna, favor tentar novamente mais tarde");
            }

            var result = JsonConvert.SerializeObject(erro);
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(result);
        }
    }
}
