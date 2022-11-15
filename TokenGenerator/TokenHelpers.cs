using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TokenGenerator
{
   public class TokenHelpers
   {

       public static string GenerateToken(TokenParameters param)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(param.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(param.Issuer, param.Audience,
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static bool ValidateToken(string token, TokenParameters param)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters(param);

            try
            {
                IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static TokenValidationParameters GetValidationParameters(TokenParameters param)
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true, 
                ValidateAudience = true,
                ValidateIssuer = true,   
                ValidIssuer = param.Issuer,
                ValidAudience = param.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(param.Key))
            };
        }

    }
}
