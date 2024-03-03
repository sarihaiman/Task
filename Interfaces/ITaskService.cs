using MyTask.Models;
using MyTask.Models;
using System.Collections.Generic;

namespace MyTask.Interface
{
    public interface ITaskService
    {
        List<Task1> GetAllTask(int id);
        Task1 GetTaskById(int id, int UserId);
        int AddNewTask(Task1 task,int id);
        bool DeleteTask(int id,int UserId);
        bool UpdateTask(int x,Task1 task,int UserId);
    }
}