using Microsoft.AspNetCore.Diagnostics;
using System.Net.Mime;

namespace Yatt_Service.Exceptions
{
    public static class GlobalExceptionHandlingExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(handlerApp =>
            {
                handlerApp.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = feature?.Error;

                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    context.Response.StatusCode = exception switch
                    {
                        ForbiddenException => StatusCodes.Status403Forbidden,
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    await context.Response.WriteAsync(exception?.Message ?? "Error");
                });
            });
        }
    }
}
