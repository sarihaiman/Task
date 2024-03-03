using MyUser.Models;
using System.Collections.Generic;

namespace MyUser.Interface
{
    public interface IUserService
    {
        string AddUser(User user);
        List<User> GetAllUser();
        User GetUserById(string id);
        
        // bool UpdateUser(string x,User user);
        bool DeleteUser(string id);
    }
}