using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using sullied_data;
using sullied_services.Models;

namespace sullied_services.Services
{
    public class UserService : IUserService
    {
        private readonly SulliedDbContext _db;

        public UserService(SulliedDbContext context)
        {
            _db = context;
        }
        public User GetUser(int userId)
        {
            var user =  _db.Users.Where(x => x.Id == userId).ProjectTo<User>().FirstOrDefault();

            return user;
        }

        public List<User> GetUsers()
        {
            var user = _db.Users.ProjectTo<User>().ToList();

            return user;
        }
    }
}
