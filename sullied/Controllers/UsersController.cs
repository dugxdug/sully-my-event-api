﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sullied_services.Models;
using sullied_services.Services;

namespace sullied.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userService.GetUsers();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userService.GetUser(id);
        }

        // POST api/values
        [HttpPost]
        public User Post([FromBody] User user)
        {
            return _userService.CreateUser(user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
