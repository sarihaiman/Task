using Microsoft.AspNetCore.Mvc;
using TaskServices;
using MyTask.Models;
using MyTask.Interface;

namespace _1.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    ITaskService TaskServicess;
    public TaskController(ITaskService TaskServicess)
    {
        this.TaskServicess = TaskServicess;
    }

    [HttpGet]
    public ActionResult<List<Task1>> Get()
    {
        return TaskServicess.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Task1> Get(int id)
    {
        var task = TaskServicess.GetById(id);
        if (task == null)
            return NotFound();
        return task;
    }

    [HttpPost]
    public ActionResult Post(Task1 newTask)
    {
        var newId = TaskServicess.Add(newTask);

        return CreatedAtAction("Post", 
            new {id = newId}, TaskServicess.GetById(newId));
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id,Task1 newTask)
    {
        var result = TaskServicess.Update(id, newTask);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
}
