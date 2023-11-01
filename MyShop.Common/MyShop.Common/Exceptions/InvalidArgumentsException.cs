using System.Net;

namespace MyShop.Common.Exceptions
{
    public class InvalidArgumentsException : CustomHttpException
    {
        public InvalidArgumentsException(IEnumerable<string> errorMessages) : base(ConcatErrorMessages(errorMessages)) 
        {
            StatusCode = HttpStatusCode.BadRequest;
        }

        private static string ConcatErrorMessages(IEnumerable<string> errorMessages)
        {
            return string.Join(";", errorMessages);
        }
    }
}
