﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AuthorizationFilterExample.Filters
{
    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        private readonly string _permission;

        public AuthorizeActionFilter(string permission)
        {
            _permission = permission;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorized = CheckUserPermission(context.HttpContext.User, _permission);

            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private bool CheckUserPermission(ClaimsPrincipal user, string permission)
        {


            // Let's assume this user has only read permission.
            return permission == "Read";
        }
    }
}
