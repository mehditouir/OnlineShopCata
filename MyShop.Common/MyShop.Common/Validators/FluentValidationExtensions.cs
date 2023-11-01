using FluentValidation.Results;

namespace MyShop.Common.Validators
{
    public static class FluentValidationExtensions
    {
        public static IEnumerable<string> GetErrorMessages(this ValidationResult result)
        {
            return result.Errors.Select(x => x.ErrorMessage);
        }
    }
}
