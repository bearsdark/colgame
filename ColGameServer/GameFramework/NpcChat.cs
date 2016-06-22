using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColGameServer.GameFramework
{
    public class NpcChat
    {
        public int ChatID { get; set; }
        public int NpcID { get; set; }
        public string Content { get; set; }
        public ContentSelect ContentSelect { get; set; }
    }
}
