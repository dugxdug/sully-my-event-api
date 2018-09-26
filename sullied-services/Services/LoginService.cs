using sullied_data;
using sullied_services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sullied_services.Services
{
    public class LoginService : ILoginService
    {
        private readonly SulliedDbContext _db;

        public LoginService(SulliedDbContext context)
        {
            _db = context;
        }

        public int LoginUser(User User)
        {
            var user = _db.Users.Where(x => x.Email == User.Email && x.Password == User.Password).Select(x => new { x.Id }).FirstOrDefault();

            if(user == null)
            {
                return 0;
            }
            else
            {
               return user.Id;
            }
        }
    }
}
