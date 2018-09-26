using Microsoft.AspNetCore.Mvc;
using sullied_services.Models;
using sullied_services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sullied.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        
        [HttpPost]
        public int Post([FromBody] User user)
        {
            return _loginService.LoginUser(user);
        }
    }
}
