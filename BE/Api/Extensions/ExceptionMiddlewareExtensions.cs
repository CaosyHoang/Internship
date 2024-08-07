using Core.DTOs;
using Core.Exceptions;
using Core.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Đã xảy ra lỗi: {contextFeature.Error.Message}");

                        int statusCode = (int)HttpStatusCode.InternalServerError;
                        var Errors = new List<string>();

                        // Xử lý các loại exception:
                        switch (contextFeature.Error)
                        {
                            case ValidateException exception:
                                statusCode = exception.StatusCode;
                                Errors.Add(exception.Message);
                                break;
                            case Exception exception:
                                Errors.Add(exception.Message);
                                break;
                            // Log chi tiết hơn cho các lỗi không xác định:
                            default:
                                logger.LogError($"Đã xảy ra lỗi: {contextFeature.Error}");
                                break;
                        }
                        context.Response.StatusCode = statusCode;
                        var res = new ResultDetails()
                        {
                            Success = false,
                            Data = null,
                            StatusCode = statusCode,
                        };
                        res.Errors.AddRange(Errors);
                        await context.Response.WriteAsync(res.ToString());
                    }
                });
            });
        }
    }
}
