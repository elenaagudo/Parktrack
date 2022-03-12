using Data.Context;
using Data.Models;
using Microsoft.IdentityModel.Tokens;
using Service.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IUserService
    {
        string Authenticate(MasterRequest user);
        Master GetById(int id);
    }

    public class UserService : IUserService
    {
        private ParktrackContext db;

        public UserService(ParktrackContext dbcontext)
        {
            db = dbcontext;
        }
        public string Authenticate(MasterRequest user)
        {
            Master masterUser = db.Master.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (masterUser == null)
            {
                return null;
            }

            string token = generateJwtToken(masterUser);

            return token;
        }

        public Master GetById(int id)
        {
            return db.Master.FirstOrDefault(x => x.Id == id);
        }

        public string generateJwtToken(Master masterUser)
        {
            // Genera token valido para 96 días.
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("this is my custom Secret key for authnetication");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("idmaster", masterUser.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(180),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
