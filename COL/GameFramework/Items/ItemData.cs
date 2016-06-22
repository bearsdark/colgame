using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.GameFramework.Items
{
    public class ItemData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Texture { get; set; }
    }
    public class ItemDataCharacter
    {
        public int ID { get; set; }
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Texture { get; set; }
        public string Position { get; set; }
    }
}
