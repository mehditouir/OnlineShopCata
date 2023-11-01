namespace MyShop.Common.Contracts
{
    public static class ContractExtensions
    {
        public static HttpResponseBase<T> ToHttpResponse<T>(this T offer)
        {
            return new HttpResponseBase<T>
            {
                Result = offer
            };
        }
    }
}
