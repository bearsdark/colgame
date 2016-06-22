using GameStateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using COL.GameFramework;
using Lidgren.Network;
using System.Threading;
using System.Diagnostics;
using COL.Helpers;
using COL.GameObjects.Players;
using COL.GameObjects;
using COL.GameFramework.Fonts;

namespace COL.Screens
{
    public class FirstConnection : GameScreen
    {
        private bool reconnect;
        private float spaceTime;
        private PopupError Error;
        private int coutTime;

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            this.Error = new PopupError(345, 150, "Đang kết nối đến máy chủ...", FontManager.GetFont("Font12"), Color.White, false);

            Network.Config = new NetPeerConfiguration("ColGameServer");
            Network.Client = new NetClient(Network.Config);

            this.spaceTime = 0;
            this.Connection();
        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            this.Error.HandleInput(gameTime);
            this.Error.IsVisible = true;

            if (Network.Client.ConnectionStatus == NetConnectionStatus.None || Network.Client.ConnectionsCount > 0 || Network.Client.ConnectionStatus == NetConnectionStatus.Connected)
            {
                Game1.CONNECT_STATUS = "Connected";
                //this.GetInfoConnect();
                Thread.Sleep(300);
                this.SenderConnection();
                Thread.Sleep(500);
                this.ExitScreen();
                this.ScreenManager.AddScreen(new LoginGame(), null);
            }
            else
            {
                this.spaceTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(this.spaceTime >= 5 && !this.reconnect)
                {
                    this.Error.textErr = "Không thể kết nối đến máy chủ.\n\tĐang thử lại...";
                    this.spaceTime = 0;
                    this.reconnect = true;
                }

                if (this.reconnect)
                {
                    this.coutTime = 5 - (int)this.spaceTime;
                    this.Error.textErr = "Không thể kết nối đến máy chủ.\nĐang thử lại trong " + this.coutTime.ToString() + " giây...";
                    if (this.spaceTime >= 5)
                    {
                        this.spaceTime = 0;
                        this.Connection();
                        Thread.Sleep(500);
                    }
                }
            }
        }
        private void GetInfoConnect()
        {
            Network.outmsg = Network.Client.CreateMessage();
            Network.outmsg.Write("GetFirstInfo");
            Network.Client.SendMessage(Network.outmsg, NetDeliveryMethod.ReliableOrdered);
        }
        private void Connection()
        {
            Network.Client.Start();
            Network.Client.Connect("127.0.0.1", 1235);
        }
        private void SenderConnection()
        {
            Network.outmsg = Network.Client.CreateMessage();
            Network.outmsg.Write("Connection");
            Network.Client.SendMessage(Network.outmsg, NetDeliveryMethod.ReliableOrdered);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            this.ScreenManager.SpriteBatch.Begin();
            this.Error.Draw(this.ScreenManager.SpriteBatch);
            this.ScreenManager.SpriteBatch.End();
        }
    }
}
