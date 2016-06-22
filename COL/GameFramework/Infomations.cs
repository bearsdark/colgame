using COL.GameFramework.Items;
using COL.GameFramework.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.GameFramework
{
    public static class Infomations
    {
        public static bool AccountIsLogin = false;
        public static int AccountID;
        public static string AccountUsername;
        public static int CharacterID;
        public static string CharacterName;
        public static int CharsTotal = -1;
        public static List<CharacterData> ListCharacters = new List<CharacterData>();
        public static int CharacterConnectID = -1;
        public static List<ItemDataCharacter> ListItemOfCharacter = new List<ItemDataCharacter>();
        public static FullQuestsData QuestsData = new FullQuestsData();
        public static List<ItemData> ListItems = new List<ItemData>();
    }
}
