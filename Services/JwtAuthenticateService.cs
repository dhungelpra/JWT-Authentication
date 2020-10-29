using JWT_Authentication.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWT_Authentication.Services
{
    public class JwtAuthenticateService : IJwtAuthenticateService
    {
        private readonly JwtAuthenticationKey _jwtAuthenticationKey;
        public JwtAuthenticateService(IOptions<JwtAuthenticationKey> jwtAuthenticationKey)
        {
            _jwtAuthenticationKey = jwtAuthenticationKey.Value;
        }


        private List<User> users = new List<User>(){
            new User{UserId=1,UserName="Prabhakar",Password="Dhungel",FirstName="Prabhakar",
            LastName="Dhungel"
            }
            };
        public User Authenticate(string userName, string password)
        {
            var user = users.SingleOrDefault(x => x.UserName == userName && x.Password == password);
            //if user not found
            if(user==null)
            {
                return null;
            }
            //if user found
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtAuthenticationKey.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;
            return user;
        }
    }
}
