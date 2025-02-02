﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PAC.BusinessLogic;
using PAC.Domain;
using PAC.IBusinessLogic;

namespace PAC.WebAPI.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public AuthorizationFilter()
        {
        }
        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers[""].ToString();
            try
            {
                context.HttpContext.Request.Headers.TryGetValue("token", out var authTokens);
                var _tokenManager = context.HttpContext.RequestServices.GetRequiredService<ITokenManager>();

                var token = authTokens.FirstOrDefault();

                _tokenManager.ValidateToken(token);
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

