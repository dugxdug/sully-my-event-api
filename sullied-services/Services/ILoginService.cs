using sullied_services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace sullied_services.Services
{
    public interface ILoginService
    {
        int LoginUser(User User);
    }
}
