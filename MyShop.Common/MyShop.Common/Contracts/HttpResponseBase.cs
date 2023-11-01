using System.Net;

namespace MyShop.Common.Contracts
{
    public class HttpResponseBase<TResponse>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public TResponse? Result { get; set; }
    }
}
