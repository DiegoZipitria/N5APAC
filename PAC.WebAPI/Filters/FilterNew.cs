using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PAC.Domain;
using PAC.IBusinessLogic;

namespace PAC.WebAPI.Filters
{
    public class FilterNew : Attribute, IAuthorizationFilter
    {
        private readonly int[] _requiredage;

        public FilterNew(params int[] _requiredage)
        {
            _requiredage = _requiredage;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                context.HttpContext.Request.Headers.TryGetValue("token", out var authTokens);
                var _tokenManager = context.HttpContext.RequestServices.GetRequiredService<ITokenManager>();

                var token = authTokens.FirstOrDefault();
                var userClaims = _tokenManager.ValidateToken(token);

                if (!_requiredage.Contains(userClaims.Age))
                {
                    ErrorDto errorDto = new ErrorDto();
                    errorDto.IsSuccess = false;
                    errorDto.Code = 403;
                    errorDto.ErrorMessage = "Access denied. Insufficient Age.";

                    context.Result = new ObjectResult(errorDto)
                    {
                        StatusCode = errorDto.Code
                    };
                }
            }
            catch (Exception e) when (e is ArgumentException || e is ArgumentNullException)
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.IsSuccess = false;
                errorDto.Code = 401;
                errorDto.ErrorMessage = $"Please provide a valid Token. Error: {e.Message}";

                context.Result = new ObjectResult(errorDto)
                {
                    StatusCode = errorDto.Code
                };
            }
        }
    }
}
