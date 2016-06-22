using ColGameServer.Entity;
using ColGameServer.GameFramework;
using ColGameServer.Helpers;
using ColGameServer.Objects;
using ColGameServer.Objects.Quests;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColGameServer
{
    public delegate void textBoxStatusMessage(string message);
    public delegate void playersTotal(string text);

    public partial class Form1 : Form
    {
        public static textBoxStatusMessage StatusMessage;
        public static playersTotal TotalPlayers;

        public static List<NpcData> listNpcs = new List<NpcData>();
        public static List<ItemData> listItems = new List<ItemData>();
        public static FullQuestsData Quests = new FullQuestsData();
        public static List<CharacterData> ListCharacters = new List<CharacterData>();

        public Form1()
        {
            InitializeComponent();

            Network.Config = new NetPeerConfiguration("ColGameServer");
            Network.Config.Port = 1235;
            Network.Server = new NetServer(Network.Config);
            Network.Server.Start();

            textBoxStatus.AppendText("Đang lấy dữ liệu...!" + "\r\n");
            
            listNpcs = Npcs.GetAllNpcs();
            textBoxStatus.AppendText("Lấy dữ liệu NPC thành công!\n");

            listItems = Item.GetAllItems();
            textBoxStatus.AppendText("Lấy dữ liệu Item thành công!\n");

            Quests = Quest.GetAllQuestsServer();
            textBoxStatus.AppendText("Lấy dữ liệu Quets thành công!\n");
            Debug.WriteLine(Quests);
            
            textBoxStatus.AppendText("Hoàn thành quá trình lấy dữ liệu...\n");
            textBoxStatus.AppendText("Server đã sẵn sàng!\n");

            StatusMessage = new textBoxStatusMessage(this.AppendStatusMessage);
            TotalPlayers = new playersTotal(this.ChangeTotalPlayers);
        }

        private void AppendStatusMessage(string message)
        {
            this.textBoxStatus.Invoke((MethodInvoker)(() =>
            {
                this.textBoxStatus.AppendText(message);
            }));
        }
        
        private void ChangeTotalPlayers(string total)
        {
            this.totalPlayers.Invoke((MethodInvoker)(() =>
            {
                this.totalPlayers.Text = "Players: " + total;
            }));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Network.Update();
            //Player.Update();
        }
    }
}
