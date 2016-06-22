using ColGameServer.Entity;
using ColGameServer.GameFramework;
using ColGameServer.Helpers;
using ColGameServer.Objects.Quests;
using Lidgren.Network;
using System.Collections.Generic;

namespace ColGameServer.Objects
{
    public class Network
    {
        public static NetServer Server;
        public static NetPeerConfiguration Config;
        private static NetIncomingMessage incmsg;
        public static NetOutgoingMessage outmsg;
        private static List<NetConnection> listConnection = new List<NetConnection>();
        
        public static void Update()
        {
            for (int i = 0; i < listConnection.Count; i++)
            {
                if(listConnection[i].Status == NetConnectionStatus.Disconnected)
                {
                    Form1.StatusMessage(listConnection[i].RemoteEndPoint.ToString() + " Đã mất kết nối!\n");
                    for (int j = 0; j < Form1.ListCharacters.Count; j++)
                    {
                        if (Form1.ListCharacters[j].IP.Equals(listConnection[i].RemoteEndPoint))
                        {
                            if(Server.Connections.Count > 0)
                            {
                                outmsg = Server.CreateMessage();
                                outmsg.Write("CharacterDisconnected");
                                outmsg.Write(Form1.ListCharacters[j].ID);
                                Server.SendMessage(outmsg, Server.Connections, NetDeliveryMethod.Unreliable, 0);
                            }

                            Form1.ListCharacters.RemoveAt(j);
                            break;
                        }
                    }
                    listConnection.RemoveAt(i);
                    break;
                }
            }
            while ((incmsg = Server.ReadMessage()) != null)
            {
                switch (incmsg.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        {
                            string headStringMessage = incmsg.ReadString();

                            switch (headStringMessage)
                            {
                                case "UpdateItemInventory":
                                    {
                                        ItemDataCharacter itemData = new ItemDataCharacter();
                                        incmsg.ReadAllProperties(itemData);
                                        DBHelper.UpdateItemData(itemData);
                                    }
                                    break;
                                case "GetInfoAllCharacters":
                                    {
                                        int id = incmsg.ReadInt32();
                                        List<CharacterData> characters = new List<CharacterData>();
                                        characters = Characters.GetCharacterInAccount(id, incmsg.SenderEndPoint);
                                        Network.outmsg = Network.Server.CreateMessage();
                                        Network.outmsg.Write("GetInfoAllCharacters");
                                        Network.outmsg.Write(characters.Count);
                                        for (int i = 0; i < characters.Count; i++)
                                        {
                                            Network.outmsg.WriteAllProperties(characters[i]);
                                        }
                                        Network.Server.SendMessage(Network.outmsg, incmsg.SenderConnection, NetDeliveryMethod.Unreliable, 0);
                                    }
                                    break;
                                case "AccountLogin":
                                    {
                                        string username = incmsg.ReadString();
                                        string password = incmsg.ReadString();

                                        bool result = Account.AccountLogin(username, password, incmsg.SenderEndPoint);
                                        Network.outmsg = Network.Server.CreateMessage();
                                        Network.outmsg.Write("AccountLogin");
                                        Network.outmsg.Write(result);
                                        Network.outmsg.Write(username);
                                        if (result)
                                        {
                                            Form1.StatusMessage(username + " - " + incmsg.SenderEndPoint + " đã đăng nhập.\n");
                                            for (int i = 0; i < Account.ListAccount.Count; i++)
                                            {
                                                if (Account.ListAccount[i].Username.Equals(username))
                                                {
                                                    Network.outmsg.Write(Account.ListAccount[i].ID);
                                                    break;
                                                }
                                            }
                                        }

                                        Network.Server.SendMessage(Network.outmsg, incmsg.SenderConnection, NetDeliveryMethod.Unreliable, 0);
                                    }
                                    break;
                                case "CharacterMove":
                                    {
                                        string name = incmsg.ReadString();
                                        PlayerActions action = (PlayerActions)incmsg.ReadByte();

                                        Network.outmsg = Network.Server.CreateMessage();
                                        Network.outmsg.Write("CharacterMove");
                                        Network.outmsg.Write(name);
                                        Network.outmsg.Write((byte)action);

                                        Network.Server.SendMessage(Network.outmsg, Network.Server.Connections, NetDeliveryMethod.Unreliable, 0);
                                        
                                    }
                                    break;
                                case "UpdatePositionCharacter":
                                    {
                                        string name = incmsg.ReadString();
                                        int X = incmsg.ReadInt32();
                                        int Y = incmsg.ReadInt32();

                                        Network.outmsg = Network.Server.CreateMessage();
                                        Network.outmsg.Write("UpdatePositionCharacter");
                                        Network.outmsg.Write(name);
                                        Network.outmsg.Write(X);
                                        Network.outmsg.Write(Y);
                                        Network.Server.SendMessage(Network.outmsg, Network.Server.Connections, NetDeliveryMethod.Unreliable, 0);

                                        Characters.UpdatePosition(name, X, Y);
                                    }
                                    break;
                                case "GetAllPositionCharacters":
                                    {
                                        outmsg = Server.CreateMessage();
                                        outmsg.Write("RequestPositionCharacter");
                                        Server.SendMessage(outmsg, Server.Connections, NetDeliveryMethod.Unreliable, 0);
                                    }
                                    break;
                                case "GetAllCharacters":
                                    {
                                        int charID = incmsg.ReadInt32();

                                        outmsg = Server.CreateMessage();
                                        outmsg.Write("GetAllCharacters");
                                        outmsg.Write(Form1.ListCharacters.Count);
                                        for (int i = 0; i < Form1.ListCharacters.Count; i++)
                                        {
                                            outmsg.WriteAllProperties(Form1.ListCharacters[i]);
                                        }
                                        Server.SendMessage(Network.outmsg, incmsg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
                                    }
                                    break;
                                case "CharacterConnection":
                                    {
                                        int characterID = incmsg.ReadInt32();
                                        bool checkOnline = false;

                                        for (int i = 0; i < Form1.ListCharacters.Count; i++)
                                        {
                                            if (Form1.ListCharacters[i].ID.Equals(characterID))
                                            {
                                                checkOnline = true;
                                                outmsg = Server.CreateMessage();
                                                outmsg.Write("CharacterConnection");
                                                outmsg.Write("CharacterOnline");
                                                Server.SendMessage(Network.outmsg, incmsg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
                                                break;
                                            }
                                        }

                                        if (!checkOnline)
                                        {
                                            Character character = Characters.CharacterConnect(characterID);

                                            if (character != null)
                                            {
                                                CharacterData charData = Characters.ConvertToCharacterData(character, incmsg.SenderEndPoint);
                                                Form1.ListCharacters.Add(charData);
                                                int charInList = 0;
                                                for (int i = 0; i < Form1.ListCharacters.Count; i++)
                                                {
                                                    if (Form1.ListCharacters[i].ID.Equals(charData.ID))
                                                    {
                                                        charInList = i;
                                                        outmsg = Server.CreateMessage();
                                                        outmsg.Write("CharacterConnection");
                                                        outmsg.Write("OK");
                                                        outmsg.Write(charData.ID);
                                                        Server.SendMessage(Network.outmsg, incmsg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
                                                        break;
                                                    }
                                                }

                                                outmsg = Server.CreateMessage();
                                                outmsg.Write("OtherCharacterConnection");
                                                outmsg.WriteAllProperties(Form1.ListCharacters[charInList]);
                                                Server.SendMessage(Network.outmsg, Network.Server.Connections, NetDeliveryMethod.ReliableOrdered, 0);
                                            }
                                            else
                                            {
                                                outmsg = Server.CreateMessage();
                                                outmsg.Write("CharacterConnection");
                                                outmsg.Write("CharacterNotExist");
                                                Server.SendMessage(Network.outmsg, incmsg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
                                            }
                                        }
                                    }
                                    break;
                                case "Connection":
                                    {
                                        listConnection.Add(incmsg.SenderConnection);
                                        Form1.StatusMessage(incmsg.SenderEndPoint + " Đã kết nối!\n");
                                    }
                                    break;
                                case "GetAllInfo":
                                    {
                                        string type = incmsg.ReadString();
                                        switch (type)
                                        {
                                            case "npc":
                                                {
                                                    Npcs.SendAllNpcs(incmsg);
                                                }
                                                break;
                                            case "item":
                                                {
                                                    int charID = incmsg.ReadInt32();
                                                    Item.SendAllItemOfCharacter(incmsg, charID);
                                                }
                                                break;
                                            case "quest":
                                                {
                                                    Quest.SendQuests(incmsg);
                                                }
                                                break;
                                            case "allItem":
                                                {
                                                    Item.SendAllItem(incmsg);
                                                }
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
                Server.Recycle(incmsg);
            }
        }
    }
}
