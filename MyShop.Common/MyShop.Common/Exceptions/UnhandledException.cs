namespace MyShop.Common.Exceptions
{
    public class CustomUnhandledException : CustomHttpException
    {
        public CustomUnhandledException(string errorMessage) : base(errorMessage)
        {
            StatusCode = System.Net.HttpStatusCode.InternalServerError;
        }
    }
}
