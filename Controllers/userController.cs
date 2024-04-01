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
    
    [HttpGet]
    [Route("[action]")]
    [Authorize(Policy = "Admin")]
    public ActionResult<List<User>> getAll()
    {
        return UserServicess.GetAllUser();
    }

    [HttpGet]
    [Authorize(Policy = "User")]
    public ActionResult<User> Get()
    {
        var User = UserServicess.GetUserById(userid);
        if (User == null)
            return NotFound();
        return User;
    }

    [HttpPut("")]
    [Authorize(Policy = "User")]
    public ActionResult Put(User user)
    {
        var result = UserServicess.UpdateUser(userid, user);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }

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