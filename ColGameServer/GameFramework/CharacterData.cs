using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ColGameServer.GameFramework
{
    public class CharacterData
    {
        public int ID { get; set; }
        public int Money { get; set; }
        public int UserID { get; set; }
        public int HpTotal { get; set; }
        public int HpCurent { get; set; }
        public int ManaTotal { get; set; }
        public int ManaCurent { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Texture { get; set; }
        public string Position { get; set; }
        public string QuestsComplete { get; set; }
        public string QuestsUnComplete { get; set; }
        public IPEndPoint IP { get; set; }
    }
}
