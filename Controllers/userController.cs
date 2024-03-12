using Microsoft.AspNetCore.Mvc;
using UserServices;
using MyUser.Models;
using MyUser.Interface;
using Microsoft.AspNetCore.Authorization;
using System.IO;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace _1.Controllers;

[ApiController]
[Route("[controller]")]

public class userController : ControllerBase
{
    private List<User> users;
    private string userfile;
    IUserService UserServicess;
    private int userid;

    public userController(IUserService UserServicess, IHttpContextAccessor httpContextAccessor)
    {
        this.userfile = Path.Combine(/*webHost.ContentRootPath,*/ "Data", "User.json");
        this.UserServicess = UserServicess;
        this.userid = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value);
        using (var jsonFile = System.IO.File.OpenText(userfile))
        {
            users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }

    // [HttpGet]
    // public ActionResult<List<User>> Get()
    // {
    //     return UserServicess.AdminGetAll();
    // }

    [Route("[action]")] 
    [Authorize(Policy = "Admin")]
    [HttpGet]
    public ActionResult<List<User>> GetAll()
    {
        return UserServicess.GetAllUser();
    }

    [Authorize(Policy = "User")]
    [HttpGet]
    public ActionResult<User> Get()
    {
        var User = UserServicess.GetUserById(userid);
        if (User == null)
            return NotFound();
        return User;
    }

    // [HttpPost]
    // public ActionResult Post(Task1 newUser)
    // {
    //     var newId = UserServicess.Add(newUser);

    //     return CreatedAtAction("Post", 
    //         new {id = newId}, UserServicess.GetById(newId));
    // }

   
    [Authorize(Policy = "User")] 
    [HttpPut("")]

    public ActionResult Put(User user)
    {
        var result = UserServicess.UpdateUser(userid, user);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }

    // [HttpDelete("{password}")]
    // public ActionResult Delete(string password)
    // {
    //     var result = UserServicess.DeleteUser(password);
    //     if (!result)
    //     {
    //         return BadRequest();
    //     }
    //     return NoContent();
    // }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public ActionResult Post(User user)
    {
        int newId = UserServicess.AddUser(user);
        return CreatedAtAction("Post",
            new { password = newId }, UserServicess.GetUserById(newId));
    }

    [HttpDelete("{UserId}")]
    [Authorize(Policy = "Admin")]
    public ActionResult Delete(int UserId)
    {
        var result = UserServicess.DeleteUser(UserId);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
}
