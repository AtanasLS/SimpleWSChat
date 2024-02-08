using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Externalities.QueryModels;

namespace Externalities.Interfaces
{
    public interface IUserRepository 
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User CreateUser(string username);
        User UpdateUser(int id, string username);
        void DeleteUser(int id);
    }
}