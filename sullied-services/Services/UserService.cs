using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using sullied_data;
using sullied_data.Models;
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

        public User CreateUser(User userToCreate)
        {
            var newUser = new UserEntity
            {
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
                Email = userToCreate.Email,
                Password = userToCreate.Password
            };

            var user = _db.Users.Add(newUser);

            var result = _db.SaveChanges();

            return new User
            {
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
                Email = userToCreate.Email,
                Password = userToCreate.Password,
                Id = result
            };
        }
    }
}
