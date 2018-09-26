using sullied_services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace sullied_services.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(int userId);
        int CreateUser(User userToCreate);
    }
}
