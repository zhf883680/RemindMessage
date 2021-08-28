using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTpl.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";

                // var title = "An error occured: " + ex.Message;
                // var details = ex.ToString();
                //
                // var problem = new ProblemDetails
                // {
                //     Status = 200,
                //     Title = title,
                //     Detail = details
                // };

                
                httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                await httpContext.Response.WriteAsync(Core.Extensions.JsonExtensions.ModelToJson(new
                {
                    result=1,
                    data=ex.Message
                }));
            }
        }
    }
}