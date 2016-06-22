using ColGameServer.Entity;
using ColGameServer.GameFramework;
using ColGameServer.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ColGameServer.Objects
{
    public class Account
    {
        public static List<UserData> ListAccount = new List<UserData>();

        public static bool AccountLogin(string username, string password, IPEndPoint IP)
        {

            List<User> users = new List<User>();

            users = DBHelper.SelectUser(username, password);

            if(users.Count > 0)
            {
                UserData user = new UserData();
                user.ID = users.First().UserID;
                user.Username = users.First().Username;
                user.IP = IP;
                ListAccount.Add(user);
                return true;
            }
            return false;
        }
    }
}
