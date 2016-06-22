using ColGameServer.GameFramework;
using ColGameServer.Helpers;
using Lidgren.Network;
using System.Collections.Generic;

namespace ColGameServer.Objects.Quests
{
    public class Quest
    {
        public static void SendQuests(NetIncomingMessage incmsg)
        {
            Network.outmsg = Network.Server.CreateMessage();
            Network.outmsg.Write("GetAllInfo");
            Network.outmsg.Write("quest");

            Network.outmsg.Write(Form1.Quests.Quests.Count);
            for (int i = 0; i < Form1.Quests.Quests.Count; i++)
            {
                Network.outmsg.WriteAllProperties(Form1.Quests.Quests[i]);
            }

            Network.outmsg.Write(Form1.Quests.RequireNpcs.Count);
            for (int i = 0; i < Form1.Quests.RequireNpcs.Count; i++)
            {
                Network.outmsg.WriteAllProperties(Form1.Quests.RequireNpcs[i]);
            }

            Network.outmsg.Write(Form1.Quests.RequireMonsters.Count);
            for (int i = 0; i < Form1.Quests.RequireMonsters.Count; i++)
            {
                Network.outmsg.WriteAllProperties(Form1.Quests.RequireMonsters[i]);
            }

            Network.outmsg.Write(Form1.Quests.ReceiveItems.Count);
            for (int i = 0; i < Form1.Quests.ReceiveItems.Count; i++)
            {
                Network.outmsg.WriteAllProperties(Form1.Quests.ReceiveItems[i]);
            }

            Network.Server.SendMessage(Network.outmsg, incmsg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);

            Form1.StatusMessage("Gửi dữ liệu Quests đến " + incmsg.SenderEndPoint + " thành công!\n");
        }
        public static FullQuestsData GetAllQuestsServer()
        {
            FullQuestsData QuestSV = new FullQuestsData();

            QuestSV.Quests = GetAllQuests();
            QuestSV.RequireNpcs = GetAllRequireNpcQuests();
            QuestSV.RequireMonsters = GetAllRequireMonsterQuests();
            QuestSV.ReceiveItems = GetAllReceiveItemQuest();

            return QuestSV;
        }
        public static List<QuestData> GetAllQuests()
        {
            List<QuestData> ListQuests = new List<QuestData>();
            ListQuests = DataXMLHelpers.GetDataContent<List<QuestData>>("../../Assets/QuestsData.xml");

            return ListQuests;
        }
        public static List<RequireNpcInQuest> GetAllRequireNpcQuests()
        {
            List<RequireNpcInQuest> ListNpcQuests = new List<RequireNpcInQuest>();
            ListNpcQuests = DataXMLHelpers.GetDataContent<List<RequireNpcInQuest>>("../../Assets/RequireNpcsQuests.xml");

            return ListNpcQuests;
        }
        public static List<RequireMonsterInQuest> GetAllRequireMonsterQuests()
        {
            List<RequireMonsterInQuest> ListMonsterQuests = new List<RequireMonsterInQuest>();
            ListMonsterQuests = DataXMLHelpers.GetDataContent<List<RequireMonsterInQuest>>("../../Assets/RequireMonstersQuests.xml");

            return ListMonsterQuests;
        }
        public static List<ReceiveItemInQuest> GetAllReceiveItemQuest()
        {
            List<ReceiveItemInQuest> ListItemQuests = new List<ReceiveItemInQuest>();
            ListItemQuests = DataXMLHelpers.GetDataContent<List<ReceiveItemInQuest>>("../../Assets/ReceiveItemsQuests.xml");

            return ListItemQuests;
        }
    }
}
