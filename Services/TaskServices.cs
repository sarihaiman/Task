using MyTask.Models;
using MyTask.Interface;
using MyUser.Models;
using MyUser.Interface;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;

namespace TaskServices
{
    public class TaskServicess : ITaskService
    {
        private List<Task1> tasks;
        private string taskfile = "Task.json";

        public TaskServicess()
        {
            this.taskfile = Path.Combine(/*webHost.ContentRootPath,*/ "Data", "Task.json");
            using (var jsonFile = File.OpenText(taskfile))
            {
                tasks = JsonSerializer.Deserialize<List<Task1>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private void saveToFile()
        {
            File.WriteAllText(taskfile, JsonSerializer.Serialize(tasks));
        }

        public List<Task1> GetAllTask(int id) => tasks.Where(x => x.UserId == id).ToList();

        public Task1 GetTaskById(int id,int userid)
        {
            return tasks.FirstOrDefault(p => p.Id == id && p.UserId==userid);
        }
        public int AddNewTask(Task1 newTask,int id)
        {
            if (tasks.Count == 0)
            {
                newTask.Id = 1;
            }
            else
            {
                newTask.Id = tasks.Max(p => p.Id) + 1;
            }
            tasks.Add(newTask);
            saveToFile();
            return newTask.Id;
        }

        public bool UpdateTask(int id, Task1 newTask,int userid)
        {
            if (id != newTask.Id)
                return false;

            var existingTask = GetTaskById(id,userid);
            if (existingTask == null)
                return false;

            var index = tasks.IndexOf(existingTask);
            if (index == -1)
                return false;

            tasks[index] = newTask;
            saveToFile();
            return true;
        }

        public bool DeleteTask(int id,int userid)
        {
            var existingTask = GetTaskById(id,userid);
            if (existingTask == null)
                return false;
            var index = tasks.IndexOf(existingTask);
            if (index == -1)
                return false;
            tasks.RemoveAt(index);
            saveToFile();
            return true;
        }
    }

    public static class TaskUtils
    {
        public static void AddTask(this IServiceCollection services)
        {
            services.AddSingleton<ITaskService, TaskServicess>();
        }
    }

}

