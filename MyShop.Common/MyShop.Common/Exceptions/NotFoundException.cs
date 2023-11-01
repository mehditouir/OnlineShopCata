using System.Net;

namespace MyShop.Common.Exceptions
{
    public class NotFoundException : CustomHttpException
    {
        public NotFoundException(string entityName) : base(BuildErrorMessage(entityName)) 
        {
            StatusCode = HttpStatusCode.NotFound;
        }

        private static string BuildErrorMessage(string entityName)
        {
            return $"{entityName} not found";
        }
    }
}
