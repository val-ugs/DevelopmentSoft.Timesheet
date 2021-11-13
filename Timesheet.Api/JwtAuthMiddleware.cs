﻿using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Api
{
    public class JwtAuthMiddleware
    {
        private readonly RequestDelegate _next; 

        public JwtAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /*
         "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJMYXN0TmFtZSI6ItCY0LLQsNC90L7QsiIsIm5iZiI6MTYzNjg0Mjk1NSwiZXhwIjoxNjM2ODQ2NTU1LCJpYXQiOjE2MzY4NDI5NTUsImF1ZCI6IkNoaWVmIn0.MloYdegNu7ChAjFX-UxN2-5RBoOS_8P4JvULb2VRvgk"
         */

        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"]
                .FirstOrDefault();

            if (authHeader != null)
            {
                var secret = "secret secret secret secret secret";
                var key = Encoding.UTF8.GetBytes(secret);

                var token = authHeader.Split(" ").Last();

                var tokenHandler = new JwtSecurityTokenHandler();

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };

                tokenHandler.ValidateToken(token,
                    validationParameters,
                    out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                context.Items["LastName"] = jwtToken.Claims
                    .First(x => x.Type == "LastName")
                    .Value;
            }

            await _next(context);
        }
    }
}
