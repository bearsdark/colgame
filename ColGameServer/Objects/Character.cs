using ColGameServer.Entity;
using ColGameServer.GameFramework;
using ColGameServer.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ColGameServer.Objects
{
    public class Characters
    {
        public static void UpdatePosition(string name, int X, int Y)
        {
            for (int i = 0; i < Form1.ListCharacters.Count; i++)
            {
                if (Form1.ListCharacters[i].Name == name)
                {
                    CharacterData charData = Form1.ListCharacters[i];
                    string Map = charData.Position.Split('|')[0];
                    charData.Position = Map + "|" + X + "|" + Y;
                    Form1.ListCharacters[i] = charData;
                    break;
                }
            }
        }
        public static Character CharacterConnect(int ID)
        {
            Character result = new Character();

            if(DBHelper.SelectCharacter(ID).Count > 0)
            {
                result = DBHelper.SelectCharacter(ID).First();
                return result;
            }
            result = null;
            return result;
        }

        public static CharacterData ConvertToCharacterData(Character character, IPEndPoint IP)
        {
            CharacterData result = new CharacterData();

            result.ID = character.CharacterID;
            result.Name = character.CharacterName;
            result.Position = character.CharacterPosition;
            result.Texture = character.CharacterTexture;
            result.HpTotal = character.CharacterHpTotal;
            result.HpCurent = character.CharacterHpCurent;
            result.ManaTotal = character.CharacterManaTotal;
            result.ManaCurent = character.CharacterManaCurent;
            result.Money = character.CharacterMoney;
            result.UserID = character.UserID;
            result.Class = character.CharacterClass;
            result.QuestsComplete = character.CharacterQuestsComplete;
            result.QuestsUnComplete = character.CharacterQuestsUnComplete;
            result.IP = IP;

            return result;
        }
        public static List<CharacterData> GetCharacterInAccount(int AccountID, IPEndPoint IP)
        {
            List<CharacterData> ListCharsResult = new List<CharacterData>();

            List<Character> listChars = new List<Character>();
            listChars = DBHelper.SelectCharacterByAccountID(AccountID);

            for (int i = 0; i < listChars.Count; i++)
            {
                ListCharsResult.Add(ConvertToCharacterData(listChars[i], IP));
            }

            return ListCharsResult;
        }
    }
}
