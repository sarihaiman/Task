using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyUser.Models;
using MyTask.Services;
using UserServices;
using System.Text.Json;
using MyUser.Interface;
using System.Linq;
using System.IO;

namespace MyTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private string userfile;
        IUserService UserServicess;
        private List<User> users;
        public LoginController(IUserService UserServicess)
        {
            this.UserServicess = UserServicess;
            this.userfile = Path.Combine("Data", "User.json");
            using (var jsonFile = System.IO.File.OpenText(userfile))
            {
                users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        [HttpPost]
        public ActionResult Login([FromBody] User User)
        {
            var user = users.FirstOrDefault(u => u.Username == User.Username && u.Password == User.Password);

            if (user == null)
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim("type", "User"),
                new Claim("UserId",user.UserId.ToString()),
            };

            if (user.Admin == true)
                claims.Add(new Claim("type", "Admin"));
            var token = TaskTokenService.GetToken(claims);

            return new OkObjectResult(TaskTokenService.WriteToken(token));
        }
    }
}