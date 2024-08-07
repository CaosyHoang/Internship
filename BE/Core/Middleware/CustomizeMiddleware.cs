using Microsoft.AspNetCore.Http;

namespace Core.Middleware
{
    public class CustomizeMiddleware
    {
        #region Declaration

        private RequestDelegate _next;

        #endregion

        #region Property
        #endregion

        #region Constructor

        public CustomizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region Method

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await context.Response.WriteAsync(ex.Message);
            }
        }

        #endregion



    }
}
