using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Todo.Shared.Models;
using ILogger = NLog.ILogger;

namespace Todo.Shared.Utils;

public static class DefaultExtension
{
    public static IApplicationBuilder UseDefaultException(this IApplicationBuilder app, ILogger logger, bool isDev)
    {
        return app.UseExceptionHandler((Action<IApplicationBuilder>) (configure =>
            configure.Run((RequestDelegate) (async handler =>
            {
                HttpResponse response = handler.Response;
                response.ContentType = "application/json";
                IExceptionHandlerFeature exceptionHandlerFeature = handler.Features.Get<IExceptionHandlerFeature>();

                if (exceptionHandlerFeature == null) return;

                DefaultResponse defaultResponse = new DefaultResponse();
                if (exceptionHandlerFeature.Error is DefaultException error2)
                {
                    defaultResponse.Error = HttpStatusCode.BadRequest.ToString();
                    response.StatusCode = (int) error2.Status;
                }
                else
                    response.StatusCode = 500;

                defaultResponse.StackTrace = exceptionHandlerFeature.Error.StackTrace.Replace("\n", ".");
                defaultResponse.Exception = exceptionHandlerFeature.Error.TargetSite.DeclaringType.FullName + "." + new StackTrace(exceptionHandlerFeature.Error).GetFrame(0).GetMethod().Name;
                defaultResponse.Message = exceptionHandlerFeature.Error.Message;
                defaultResponse.Path = ((ExceptionHandlerFeature) exceptionHandlerFeature).Path;
                defaultResponse.Status = response.StatusCode;

                logger.Error(defaultResponse.Message + " " + defaultResponse.StackTrace);

                if (!isDev) return;

                await response.WriteAsync(JsonConvert.SerializeObject((object) defaultResponse)).ConfigureAwait(false);
            }))));
    }
}