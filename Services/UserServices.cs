
using MyUser.Models;
using MyUser.Interface;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;

namespace UserServices{
public  class UserServicess:IUserService
{

private List<User> users;
  private string userfile = "User.json";

  public UserServicess()
        {
            this.userfile = Path.Combine(/*webHost.ContentRootPath,*/ "Data", "User.json");
             using (var jsonFile = File.OpenText(userfile))
            {
                users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

    private void saveToFileUsers()
    {
        File.WriteAllText(userfile, JsonSerializer.Serialize(users));
    }

 public List<User> GetAllUser() => users;

  public User GetUserById(string Password) 
    {
        return users.FirstOrDefault(p => p.Password == Password);
    }


public  string AddUser(User user)
    {
        if (users.Count == 0)

            {
                user.Password = "1";
            }
            else
            {
        user.Password =  users.Max(p => p.Password) + 1;
            }
        users.Add(user);
        saveToFileUsers();
        return user.Password;
    }
  
public bool UpdateUser(string Password, User user)
    {
        if (Password != user.Password)
            return false;

        var existingTask = GetUserById(Password);
        if (existingTask == null )
            return false;

        var index = users.IndexOf(existingTask);
        if (index == -1 )
            return false;

        users[index] = user;
        saveToFileUsers();
        return true;
    }  

public bool DeleteUser(string password)
    {
        var existingTask = GetUserById(password);
        if (existingTask == null )
            return false;
        var index = users.IndexOf(existingTask);
        if (index == -1 )
            return false;
        users.RemoveAt(index);
        saveToFileUsers();
        return true;
    }

}

public static class UserUtils
{
    public static void AddUser(this IServiceCollection services)
    {
        services.AddSingleton<IUserService,UserServicess>();
    }
}

}