using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyUser.Models;
// using MyUser.Services;
using MyTask.Services;
using UserServices;
using System.Text.Json;
using MyUser.Interface;
using System.Linq;
using System.IO;

namespace FBI.Controllers
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
            this.userfile = Path.Combine(/*webHost.ContentRootPath,*/ "Data", "User.json");
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

            if(user.Admin==true)
                claims.Add(new Claim("type", "Admin"));
            var token = TaskTokenService.GetToken(claims);

            return new OkObjectResult(TaskTokenService.WriteToken(token));
        }

        // [HttpGet]
        // [Authorize(Policy = "Admin")]
        // [Route("[action]")]
        // public ActionResult<List<User>> Get()
        // {
        //     return UserServicess.GetAllUser();
        // }

        // [HttpGet("{id}")]
        // public ActionResult<User> Get(string id)
        // {
        //     var User = UserServicess.AdminGetById(id);
        //     if (User == null)
        //         return NotFound();
        //     return User;
        // }


        // [HttpPost]
        // [Authorize(Policy = "Admin")]
        // public ActionResult Post(User user)
        // {
        //     int newId = UserServicess.AddUser(user);
        //     return CreatedAtAction("Post",
        //         new { password = newId }, UserServicess.GetUserById(newId));
        // }

        // [HttpDelete("{UserId}")]
        // [Authorize(Policy = "Admin")]
        // public ActionResult Delete(int UserId)
        // {
        //     var result = UserServicess.DeleteUser(UserId);
        //     if (!result)
        //     {
        //         return BadRequest();
        //     }
        //     return NoContent();
        // }
    }

}
