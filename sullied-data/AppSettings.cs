using System;
using System.Collections.Generic;
using System.Text;

namespace sullied_data
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string SulliedConnection { get; set; }
    }
}
