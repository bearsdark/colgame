using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ColGameServer.GameFramework
{
    public class UserData
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public IPEndPoint IP { get; set; }
    }
}
