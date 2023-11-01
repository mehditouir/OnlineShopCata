using System.Net;

namespace MyShop.Common.Exceptions
{
    public class BusinessException : CustomHttpException
    {
        public BusinessException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.UnprocessableEntity;
        }
    }
}
