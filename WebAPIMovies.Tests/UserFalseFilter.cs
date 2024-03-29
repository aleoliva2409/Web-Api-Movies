﻿using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace WebAPIMovies.Tests
{
    public class UserFalseFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Email, "example@mail.com"),
                    new Claim(ClaimTypes.Name, "example@mail.com"),
                    new Claim(ClaimTypes.NameIdentifier, "9722b56a-77ea-4e41-941d-e319b6eb3712"),
                }, "test"));

            await next();
        }
    }
}
