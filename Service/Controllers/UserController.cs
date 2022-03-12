using Data.Context;
using Data.Helpers;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private ParktrackContext db;

        public UserController(ParktrackContext db)
        {
            this.db = db;
        }

        [CustomAuthorize]
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            List<User> userList = db.User.ToList();
            return Ok(userList);
        }

        [CustomAuthorize]
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            User user = db.User.FirstOrDefault(u=>u.Id == id);
            return Ok(user);
        }

        [CustomAuthorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                // encriptar contraseña
                user.Password = HashHelper.GetMD5Hash(user.Password);

                // comprobar si ya existe el email o el username
                User dbUser = db.User.Where(u => u.Username == user.Username || u.Email == user.Email).FirstOrDefault();
                if (dbUser != null)
                {
                    return BadRequest("Username or email already exists");
                }

                user.Active = true;
                user.IsDeleted = false;
                user.CreationDate = DateTime.Now;

                db.User.Add(user);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
