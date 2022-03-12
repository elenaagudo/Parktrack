using Data.Helpers;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Models;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Index(MasterRequest master)
        {
            master.Password = HashHelper.GetMD5Hash(master.Password);
            var response = _userService.Authenticate(master);

            if (response == null)
                return NotFound(new { errorMessage = "Email or password is incorrect" });


            return Ok(response);
        }
    }
}
