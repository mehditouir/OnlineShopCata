using MyShop.Common.Contracts;
using MyShop.Common.Exceptions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace MyShop.Api.Middlewares
{
    public class HttpResponseHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpResponseHandlerMiddleware> _logger;

        public HttpResponseHandlerMiddleware(RequestDelegate next, ILogger<HttpResponseHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            using (var responseBodyStream = new MemoryStream())
            {
                var originalBodyStream = context.Response.Body;
                context.Response.Body = responseBodyStream;
                
                try
                {
                    await _next(context);

                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    var responseBodyText = await new StreamReader(responseBodyStream).ReadToEndAsync();
                    var responseData = JsonSerializer.Deserialize<HttpResponseBase<object>>(responseBodyText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    responseData.StatusCode = HttpStatusCode.OK;

                    await HandleResponseAsync(responseData, context, originalBodyStream);
                }
                catch (CustomHttpException ex)
                {
                    var responseData = new HttpResponseBase<object>
                    {
                        ErrorCode = ex.GetType().Name,
                        ErrorMessage = ex.Message,
                        StatusCode = ex.StatusCode
                    };
                    await HandleResponseAsync(responseData, context, originalBodyStream, ex);
                }
                catch (Exception ex)
                {
                    var responseData = new HttpResponseBase<object>
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        ErrorMessage = "An unexpected error occurred, please contact your administrator",
                        ErrorCode = typeof(CustomUnhandledException).Name
                    };
                    await HandleResponseAsync(responseData, context, originalBodyStream, ex);
                }
            }
        }

        private async Task HandleResponseAsync(HttpResponseBase<object> responseData, HttpContext context, Stream originalBodyStream, Exception ex = null)
        {
            if (ex is not null)
            {
                //TODO : log to application insights, storage account, maybe notify to teams canal
                _logger.LogError(new
                {
                    Exception = ex.Message,
                    StackTrace = ex.StackTrace
                }.ToString());
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            var httpResponse = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(responseData));
            await originalBodyStream.WriteAsync(httpResponse);
        }
    }
}
