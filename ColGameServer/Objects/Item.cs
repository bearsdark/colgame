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
    public class Item
    {
        public static List<ItemData> GetAllItems()
        {
            List<ItemData> ListItems = new List<ItemData>();
            ListItems = DataXMLHelpers.GetDataContent<List<ItemData>>("../../Assets/ItemsData.xml");

            return ListItems;
        }
        public static void SendAllItem(NetIncomingMessage incmsg)
        {
            Network.outmsg = Network.Server.CreateMessage();
            Network.outmsg.Write("GetAllInfo");
            Network.outmsg.Write("allItem");
            Network.outmsg.Write(Form1.listItems.Count);
            for (int i = 0; i < Form1.listItems.Count; i++)
            {
                Network.outmsg.WriteAllProperties(Form1.listItems[i]);
            }
            Network.Server.SendMessage(Network.outmsg, incmsg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
        }
        public static void SendAllItemOfCharacter(NetIncomingMessage incmsg, int charId)
        {
            List<ItemDataCharacter> listItemOfChar = new List<ItemDataCharacter>();
            listItemOfChar = DBHelper.SelectItemChar(charId);

            Network.outmsg = Network.Server.CreateMessage();
            Network.outmsg.Write("GetAllInfo");
            Network.outmsg.Write("item");
            Network.outmsg.Write(listItemOfChar.Count);
            for (int i = 0; i < listItemOfChar.Count; i++)
            {
                Network.outmsg.WriteAllProperties(listItemOfChar[i]);
            }
            Network.Server.SendMessage(Network.outmsg, incmsg.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);

            Form1.StatusMessage("Gửi dữ liệu Item đến " + incmsg.SenderEndPoint + " thành công!\n");
        }
    }
}
