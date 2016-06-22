using COL.GameFramework.Items;
using COL.GameFramework.Npcs;
using COL.GameFramework.Quests;
using COL.GameObjects.Npcs;
using COL.GameObjects.Players;
using COL.Screens;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;

namespace COL.GameFramework
{
    public class Network
    {
        public static NetClient Client;
        public static NetPeerConfiguration Config;
        /*public*/
        static NetIncomingMessage incmsg;
        public static NetOutgoingMessage outmsg;

        public static void Update(GameTime gameTime)
        { 
            while ((incmsg = Client.ReadMessage()) != null)
            {
                switch (incmsg.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        {
                            string headStringMessage = incmsg.ReadString();

                            switch (headStringMessage)
                            {
                                case "CharacterDisconnected":
                                    {
                                        int charID = incmsg.ReadInt32();

                                        for (int i = 0; i < Player.ListPlayer.Count; i++)
                                        {
                                            if (Player.ListPlayer[i].charID.Equals(charID))
                                            {
                                                Player.ListPlayer.RemoveAt(i);
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                case "GetInfoAllCharacters":
                                    {
                                        int total = incmsg.ReadInt32();
                                        Infomations.CharsTotal = total;
                                        if(total > 0)
                                        {
                                            for (int i = 0; i < total; i++)
                                            {
                                                CharacterData charData = new CharacterData();
                                                incmsg.ReadAllProperties(charData);
                                                Infomations.ListCharacters.Add(charData);
                                            }
                                        }
                                    }
                                    break;
                                case "AccountLogin":
                                    {
                                        bool result = incmsg.ReadBoolean();
                                        string username = incmsg.ReadString();
                                        Infomations.AccountIsLogin = result;
                                        Infomations.AccountUsername = username;
                                        if (result)
                                            Infomations.AccountID = incmsg.ReadInt32();

                                        LoginGame.loginProcess = true;
                                    }
                                    break;
                                case "CharacterMove":
                                    {
                                        string name = incmsg.ReadString();
                                        var action = (PlayerActions)incmsg.ReadByte();

                                        for (int i = 0; i < Player.ListPlayer.Count; i++)
                                        {
                                            if (Player.ListPlayer[i].Name.Equals(name))
                                            {
                                                Player.ListPlayer[i].actionStatus = action;
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                case "UpdatePositionCharacter":
                                    {
                                        string name = incmsg.ReadString();
                                        int X = incmsg.ReadInt32();
                                        int Y = incmsg.ReadInt32();

                                        for (int i = 0; i < Player.ListPlayer.Count; i++)
                                        {
                                            if(Player.ListPlayer[i].Name == name)
                                            {
                                                Player.ListPlayer[i].Position = new Vector2(X, Y);
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                case "RequestPositionCharacter":
                                    {
                                        for (int i = 0; i < Player.ListPlayer.Count; i++)
                                        {
                                            if(Player.ListPlayer[i].charID == Infomations.CharacterConnectID)
                                            {
                                                Player.ListPlayer[i].SendUpdatePosition();
                                                break;
                                            }
                                        }

                                        CharacterConnect.GetInfoStatus = "characters";
                                    }
                                    break;
                                case "GetAllCharacters":
                                    {
                                        int totals = incmsg.ReadInt32();
                                        for (int i = 0; i < totals; i++)
                                        {
                                            CharacterData charData = new CharacterData();
                                            incmsg.ReadAllProperties(charData);
                                            bool exist = false;
                                            for (int j = 0; j < Player.ListPlayer.Count; j++)
                                            {
                                                if(Player.ListPlayer[j].charID == charData.ID)
                                                {
                                                    exist = true;
                                                    break;
                                                }
                                            }
                                            if (!exist)
                                            {
                                                string[] position = charData.Position.Split('|');
                                                Player.ListPlayer.Add(new Player(charData.ID, charData.Name, new Vector2(int.Parse(position[1]), int.Parse(position[2])), position[0]));
                                            }
                                        }

                                        CharacterConnect.GetInfoStatus = "success";
                                    }
                                    break;
                                case "OtherCharacterConnection":
                                    {
                                        CharacterData charData = new CharacterData();
                                        incmsg.ReadAllProperties(charData);

                                        if(charData.ID != Infomations.CharacterConnectID)
                                        {
                                            bool exist = false;
                                            for (int i = 0; i < Player.ListPlayer.Count; i++)
                                            {
                                                if (Player.ListPlayer[i].charID.Equals(charData.ID))
                                                {
                                                    exist = true;
                                                    break;
                                                }
                                            }
                                            if (!exist)
                                            {
                                                string[] position = charData.Position.Split('|');
                                                Player.ListPlayer.Add(new Player(charData.ID, charData.Name, new Vector2(int.Parse(position[1]), int.Parse(position[2])), position[0]));
                                            }
                                        }
                                    }
                                    break;
                                case "CharacterConnection":
                                    {
                                        CharacterConnect.CharConnectStatus = incmsg.ReadString();

                                        if (CharacterConnect.CharConnectStatus == "OK")
                                        {
                                            Infomations.CharacterConnectID = incmsg.ReadInt32();
                                            for (int i = 0; i < Infomations.ListCharacters.Count; i++)
                                            {
                                                if (Infomations.ListCharacters[i].ID.Equals(Infomations.CharacterConnectID))
                                                {
                                                    string[] position = Infomations.ListCharacters[i].Position.Split('|');
                                                    Game1.MapName = position[0];
                                                    Player.ListPlayer.Add(new Player(Infomations.ListCharacters[i].ID, Infomations.ListCharacters[i].Name, new Vector2(int.Parse(position[1]), int.Parse(position[2])), position[0]));
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case "GetAllInfo":
                                    {
                                        string type = incmsg.ReadString();
                                        switch (type)
                                        {
                                            case "npc":
                                                {
                                                    int totalNpc = incmsg.ReadInt32();
                                                    for (int i = 0; i < totalNpc; i++)
                                                    {
                                                        bool exist = false;
                                                        NpcData npc = new NpcData();
                                                        incmsg.ReadAllProperties(npc);
                                                        for (int j = 0; j < Npc.ListNpc.Count; j++)
                                                        {
                                                            if (Npc.ListNpc[j].ID.Equals(npc.ID))
                                                            {
                                                                exist = true;
                                                                break;
                                                            }
                                                        }
                                                        if (!exist)
                                                            Npc.ListNpc.Add(new Npc(npc.ID, npc.Name, npc.Texture, npc.X, npc.Y, npc.MapName));
                                                    }

                                                    CharacterConnect.GetInfoStatus = "item";
                                                }
                                                break;
                                            case "item":
                                                {
                                                    int totalItem = incmsg.ReadInt32();
                                                    for (int i = 0; i < totalItem; i++)
                                                    {
                                                        ItemDataCharacter item = new ItemDataCharacter();
                                                        incmsg.ReadAllProperties(item);
                                                        bool exist = false;
                                                        for (int j = 0; j < Infomations.ListItemOfCharacter.Count; j++)
                                                        {
                                                            if(Infomations.ListItemOfCharacter[j].ID == item.ID)
                                                            {
                                                                exist = true;
                                                                break;
                                                            }
                                                        }
                                                        if (!exist)
                                                            Infomations.ListItemOfCharacter.Add(item);
                                                    }

                                                    CharacterConnect.GetInfoStatus = "quest";
                                                }
                                                break;
                                            case "quest":
                                                {
                                                    List<QuestData> Quests = new List<QuestData>();
                                                    List<RequireNpcInQuest> RequireNpcs = new List<RequireNpcInQuest>();
                                                    List<RequireMonsterInQuest> RequireMonsters = new List<RequireMonsterInQuest>();
                                                    List<ReceiveItemInQuest> ReceiveIntems = new List<ReceiveItemInQuest>();

                                                    int totalQuests = incmsg.ReadInt32();
                                                    for (int i = 0; i < totalQuests; i++)
                                                    {
                                                        QuestData data = new QuestData();
                                                        incmsg.ReadAllProperties(data);
                                                        bool exist = false;
                                                        for (int j = 0; j < Quests.Count; j++)
                                                        {
                                                            if (Quests[j].Name == data.Name)
                                                            {
                                                                exist = true;
                                                                break;
                                                            }
                                                        }
                                                        if (!exist)
                                                            Quests.Add(data);
                                                    }

                                                    int totalRequireNpcs = incmsg.ReadInt32();
                                                    for (int i = 0; i < totalRequireNpcs; i++)
                                                    {
                                                        RequireNpcInQuest data = new RequireNpcInQuest();
                                                        incmsg.ReadAllProperties(data);
                                                        bool exist = false;
                                                        for (int j = 0; j < RequireNpcs.Count; j++)
                                                        {
                                                            if (RequireNpcs[j].QuestName == data.QuestName)
                                                            {
                                                                exist = true;
                                                                break;
                                                            }
                                                        }
                                                        if (!exist)
                                                            RequireNpcs.Add(data);
                                                    }

                                                    int totalRequireMonster = incmsg.ReadInt32();
                                                    for (int i = 0; i < totalRequireMonster; i++)
                                                    {
                                                        RequireMonsterInQuest data = new RequireMonsterInQuest();
                                                        incmsg.ReadAllProperties(data);
                                                        bool exist = false;
                                                        for (int j = 0; j < RequireMonsters.Count; j++)
                                                        {
                                                            if (RequireMonsters[j].QuestName == data.QuestName)
                                                            {
                                                                exist = true;
                                                                break;
                                                            }
                                                        }
                                                        if (!exist)
                                                            RequireMonsters.Add(data);
                                                    }

                                                    int totalReceiveItems = incmsg.ReadInt32();
                                                    for (int i = 0; i < totalReceiveItems; i++)
                                                    {
                                                        ReceiveItemInQuest data = new ReceiveItemInQuest();
                                                        incmsg.ReadAllProperties(data);
                                                        bool exist = false;
                                                        for (int j = 0; j < ReceiveIntems.Count; j++)
                                                        {
                                                            if (ReceiveIntems[j].QuestName == data.QuestName)
                                                            {
                                                                exist = true;
                                                                break;
                                                            }
                                                        }
                                                        if (!exist)
                                                            ReceiveIntems.Add(data);
                                                    }

                                                    Infomations.QuestsData.Quests = Quests;
                                                    Infomations.QuestsData.RequireNpcs = RequireNpcs;
                                                    Infomations.QuestsData.RequireMonsters = RequireMonsters;
                                                    Infomations.QuestsData.ReceiveItems = ReceiveIntems;

                                                    CharacterConnect.GetInfoStatus = "allItem";
                                                }
                                                break;
                                            case "allItem":
                                                {
                                                    int total = incmsg.ReadInt32();
                                                    for (int i = 0; i < total; i++)
                                                    {
                                                        bool exist = false;
                                                        ItemData data = new ItemData();
                                                        incmsg.ReadAllProperties(data);

                                                        for (int j = 0; j < Infomations.ListItems.Count; j++)
                                                        {
                                                            if(Infomations.ListItems[j].ID == data.ID)
                                                            {
                                                                exist = true;
                                                                break;
                                                            }
                                                        }

                                                        if (!exist)
                                                            Infomations.ListItems.Add(data);
                                                    }

                                                    CharacterConnect.GetInfoStatus = "positionCharacters";
                                                }
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
                Client.Recycle(incmsg);
            }
        }
    }
}
