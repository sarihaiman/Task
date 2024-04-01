using MyUser.Models;
using System.Collections.Generic;

namespace MyUser.Interface
{
    public interface IUserService
    {
        int AddUser(User user);
        List<User> GetAllUser();
        User GetUserById(int id);
        bool UpdateUser(int x, User user);
        bool DeleteUser(int id);
    }
}