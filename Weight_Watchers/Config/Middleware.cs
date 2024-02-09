using Newtonsoft.Json;

namespace Subscriber.WebApi.Config
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> _looger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> looger)
        {
            this.next = next;
            _looger = looger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
               string time = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm");

                _looger.LogDebug($"start cell api {context.Request.Path} start in {time} s");

                await next(context);

                string time2 = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm");

                _looger.LogInformation($"the function: {context.Request.Method} finished  in {time2} s");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            //context.Response.ContentType = "application/json";            
            return context.Response.WriteAsync(result);
        }
    }
}
