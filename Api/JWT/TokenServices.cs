using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Entitis;
using Microsoft.IdentityModel.Tokens;

namespace Api.JWT
{
    public class TokenServices : ITokenServices
    {
        private readonly SymmetricSecurityKey _key;
        public TokenServices(IConfiguration config)
        {
            _key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateWebToken(User user)
        {
            var claims=new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
            };
            var creds=new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor=new SecurityTokenDescriptor{
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(7),
                SigningCredentials=creds
            };

            var tokenHanlder=new JwtSecurityTokenHandler();
            var token=tokenHanlder.CreateToken(tokenDescriptor);
            return tokenHanlder.WriteToken(token);
        }
    }
}