using ColGameServer.Entity;
using ColGameServer.GameFramework;
using ColGameServer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColGameServer.Helpers
{
    public class DBHelper
    {
        private static ColGameDbContext _dbContext = new ColGameDbContext();

        public static User AddUser(User user)
        {
            user = _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }
        public static List<User> SelectUser(int ID)
        {
            List<User> user = new List<User>();
            user = _dbContext.Users.Where(t => t.UserID == ID).ToList();

            return user;
        }
        public static List<User> SelectUser(string Username = null, string Password = null)
        {
            List<User> user = new List<User>();
            if (Username != null && Password != null)
                user = _dbContext.Users.Where(t => t.Username == Username && t.Password == Password).ToList();
            else if (Username != null && Password == null)
                user = _dbContext.Users.Where(t => t.Username == Username).ToList();
            else if (Username == null && Password != null)
                user = _dbContext.Users.Where(t => t.Password == Password).ToList();

            return user;
        }
        public static bool DeleteUser(int ID)
        {
            List<User> user;
            user = SelectUser(ID);
            
            if(user.Count > 0)
            {
                _dbContext.Users.Remove(user.First());
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public static List<User> UpdateUser(int ID, User NewInfo)
        {
            List<User> user;
            user = SelectUser(ID);

            if (user.Count > 0)
            {
                user.First().Username = NewInfo.Username;
                user.First().Password = NewInfo.Password;
                user.First().Email = NewInfo.Email;
                _dbContext.SaveChanges();
            }
            return user;
        }
        
        public static Character AddCharacter(Character character)
        {
            character = _dbContext.Characters.Add(character);
            _dbContext.SaveChanges();
            return character;
        }
        public static List<Character> SelectCharacter(int ID)
        {
            List<Character> character = new List<Character>();
            character = _dbContext.Characters.Where(t => t.CharacterID == ID).ToList();

            return character;
        }
        public static List<Character> SelectCharacterByAccountID(int ID)
        {
            List<Character> character = new List<Character>();
            character = _dbContext.Characters.Where(t => t.UserID == ID).ToList();

            return character;
        }
        public static List<Character> SelectCharacter(string Name)
        {
            List<Character> character = new List<Character>();
            character = _dbContext.Characters.Where(t => t.CharacterName == Name).ToList();

            return character;
        }
        public static bool DeleteCharacter(int ID)
        {
            List<Character> character;
            character = SelectCharacter(ID);

            if (character.Count > 0)
            {
                _dbContext.Characters.Remove(character.First());
                return true;
            }
            return false;
        }
        public static bool AddItemChar(ItemData item, int CharID, int X, int Y)
        {
            _dbContext.ItemChars.Add(new ItemChar { IC_CharID = CharID, IC_ItemID = item.ID, IC_Position = "Inventory|" + X + "|" + Y });
            _dbContext.SaveChanges();
            return true;
        }
        public static bool AddItemChar(ItemData item, int CharID, string position)
        {
            _dbContext.ItemChars.Add(new ItemChar { IC_CharID = CharID, IC_ItemID = item.ID, IC_Position = "Store|" + position });
            _dbContext.SaveChanges();
            return true;
        }
        public static List<ItemDataCharacter> SelectItemChar(int selectID, string type = "char")
        {
            List<ItemDataCharacter> ListItem = new List<ItemDataCharacter>();
            List<ItemChar> listItemData = new List<ItemChar>();
            if (type == "char")
            {
                listItemData = _dbContext.ItemChars.Where(item => item.IC_CharID == selectID).ToList();
            }
            else if(type == "item")
            {
                listItemData = _dbContext.ItemChars.Where(item => item.ItemCharID == selectID).ToList();
            }

            for (int i = 0; i < listItemData.Count; i++)
            {
                bool exist = false;
                ItemData item = new ItemData();
                for (int j = 0; j < Form1.listItems.Count; j++)
                {
                    if(Form1.listItems[j].ID == listItemData[i].IC_ItemID)
                    {
                        exist = true;
                        item = Form1.listItems[j];
                        break;
                    }
                }
                if (exist)
                {
                    ItemDataCharacter ItemAdd = new ItemDataCharacter();
                    ItemAdd.ID = listItemData[i].ItemCharID;
                    ItemAdd.ItemID = item.ID;
                    ItemAdd.Name = item.Name;
                    ItemAdd.Texture = item.Texture;
                    ItemAdd.Position = listItemData[i].IC_Position;
                    ListItem.Add(ItemAdd);
                }
            }

            return ListItem;
        }
        public static void UpdateItemData(ItemDataCharacter data)
        {
            ItemDataCharacter itemResult = new ItemDataCharacter();
            List<ItemChar> listItemData = _dbContext.ItemChars.Where(it => it.ItemCharID == data.ID).ToList();
            if(listItemData.Count > 0)
            {
                listItemData.First().IC_Position = data.Position;
                _dbContext.SaveChanges();
            }
        }
    }
}
