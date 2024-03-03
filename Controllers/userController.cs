using Microsoft.AspNetCore.Mvc;
using UserServices;
using MyUser.Models;
using MyUser.Interface;

namespace _1.Controllers;

[ApiController]
[Route("[controller]")]
public class userController : ControllerBase
{
    IUserService UserServicess;
    public userController(IUserService UserServicess)
    {
        this.UserServicess = UserServicess;
    }

    // [HttpGet]
    // public ActionResult<List<User>> Get()
    // {
    //     return UserServicess.AdminGetAll();
    // }

    [HttpGet("{password}")]
    public ActionResult<User> Get(string password)
    {
        var User = UserServicess.GetUserById(password);
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

    // [HttpPut("{password}")]
    // public ActionResult Put(string password,User user)
    // {
    //     var result = UserServicess.UpdateUser(password, user);
    //     if (!result)
    //     {
    //         return BadRequest();
    //     }
    //     return NoContent();
    // }

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
