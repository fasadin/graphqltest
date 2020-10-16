using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using graphqltest.Common.Data;
using graphqltest.Providers;

namespace graphqltest
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthOptions authOptions;

        public JwtMiddleware(RequestDelegate next, IOptions<Settings> appSettings)
        {
            _next = next;
            authOptions = appSettings.Value.AuthOptions;
        }

        public async Task Invoke(HttpContext context, IUserProvider userProvider)
        {
            var token = context.Request.Headers["Authorization"]
                .FirstOrDefault()?
                .Split(" ")
                .Last();

            if (token != null)
                AttachUserToContext(context, userProvider, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IUserProvider userProvider, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(authOptions.SecureKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken) validatedToken;
                var userId = Guid.Parse(
                    jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId)
                        .Value);

                // attach user to context on successful jwt validation
                context.Items["User"] = userProvider.GetUserDetails(userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}