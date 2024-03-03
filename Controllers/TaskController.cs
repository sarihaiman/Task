using Microsoft.AspNetCore.Mvc;
using TaskServices;
using MyTask.Models;
using MyTask.Interface;
using System.Web;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
// using MyUser.Services;
using MyTask.Services;
using UserServices;
using System.Text.Json;
using MyUser.Interface;
using System.Linq;
using System.IO;

namespace _1.Controllers;

[ApiController]
[Route("[controller]")]
public class todoController : ControllerBase
{
    ITaskService TaskServicess;
    private int userid;

    public todoController(ITaskService TaskServicess, IHttpContextAccessor httpContextAccessor)
    {
        this.TaskServicess = TaskServicess;
        this.userid=int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value);
    }

    [HttpGet]
    [Authorize(Policy = "User")]
    public ActionResult<List<Task1>> Get()
    {
        return TaskServicess.GetAllTask(this.userid);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult<Task1> Get(int id)
    {

        var task = TaskServicess.GetTaskById(id,userid);
        if (task == null)
            return NotFound();
        return task;
    }

    [HttpPost]
    [Authorize(Policy = "User")]
    public ActionResult Post(Task1 newTask)
    {
        newTask.UserId=userid;
        var newId = TaskServicess.AddNewTask(newTask,userid);
        return CreatedAtAction("Post", 
            new {id = newId}, TaskServicess.GetTaskById(newId,userid));
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult Put(int id,Task1 newTask)
    {
        newTask.UserId=userid;
        var result = TaskServicess.UpdateTask(id, newTask,userid);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult Delete(int id)
    {
        var result = TaskServicess.DeleteTask(id,userid);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
}
