using Microsoft.AspNetCore.Mvc;
using UserServices;
using MyUser.Models;
using MyUser.Interface;
using Microsoft.AspNetCore.Authorization;

namespace _1.Controllers;

[ApiController]
[Route("[controller]")]

public class userController : ControllerBase
{
    IUserService UserServicess;
    private int userid;

    public userController(IUserService UserServicess, IHttpContextAccessor httpContextAccessor)
    {
        this.UserServicess = UserServicess;
        this.userid=int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value);
    }

    // [HttpGet]
    // public ActionResult<List<User>> Get()
    // {
    //     return UserServicess.AdminGetAll();
    // }

    [HttpGet("{UserId}")]
    [Authorize(Policy = "User")]
    public ActionResult<User> Get(int UserId)
    {
        var User = UserServicess.GetUserById(UserId);
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

    [HttpPut("{UserId}")]
    [Authorize(Policy = "User")]

    public ActionResult Put(int UserId,User user)
    {
        var result = UserServicess.UpdateUser(UserId, user);
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
}
