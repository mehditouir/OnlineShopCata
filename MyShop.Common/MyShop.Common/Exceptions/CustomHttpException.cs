using System.Net;

namespace MyShop.Common.Exceptions
{
    public abstract class CustomHttpException : Exception 
    {
        public HttpStatusCode StatusCode { get; set; }

        public CustomHttpException(string errorMessage) : base(errorMessage) { }
    }
}
