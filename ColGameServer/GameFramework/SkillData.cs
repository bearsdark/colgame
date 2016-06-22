using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColGameServer.GameFramework
{
    public class SkillData
    {
        public int SkillID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Properties { get; set; }
        public int Damage { get; set; }
        public int UseMana { get; set; }
        public int TotalLevel { get; set; }
    }
}
