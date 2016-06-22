using ColGameServer.GameFramework;
using ColGameServer.Helpers;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColGameServer.Objects
{
    public class Npcs
    {
        public static List<NpcData> GetAllNpcs()
        {
            List<NpcData> ListNpc = new List<NpcData>();
            ListNpc = DataXMLHelpers.GetDataContent<List<NpcData>>("../../Assets/NpcsData.xml");

            return ListNpc;
        }
        public static void SendAllNpcs(NetIncomingMessage incmsg)
        {
            Network.outmsg = Network.Server.CreateMessage();
            Network.outmsg.Write("GetAllInfo");
            Network.outmsg.Write("npc");
            Network.outmsg.Write(Form1.listNpcs.Count);
            for (int i = 0; i < Form1.listNpcs.Count; i++)
            {
                Network.outmsg.WriteAllProperties(Form1.listNpcs[i]);
            }
            Network.Server.SendMessage(Network.outmsg, incmsg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);

            Form1.StatusMessage("Gửi dữ liệu NPC đến " + incmsg.SenderEndPoint + " thành công!\n");
        }
    }
}
