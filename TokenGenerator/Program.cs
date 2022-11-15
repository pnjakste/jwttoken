using System.IdentityModel;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TokenGenerator;


var param = new TokenParameters()
{
    Audience = "test",
    Issuer = "test",
    Key = "The-quick-brown-fox-jumps-over-the-lazy-dog"
};

var token = TokenHelpers.GenerateToken(param);
if (TokenHelpers.ValidateToken(token, param))
{
    Console.WriteLine("Token is valid");
}
else
{
    Console.WriteLine("Token is invalid");
}
