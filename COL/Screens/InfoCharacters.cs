using GameStateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using COL.GameObjects;
using COL.GameFramework.Fonts;
using COL.GameFramework;
using COL.Helpers;
using Lidgren.Network;
using System.Diagnostics;
using System.Threading;

namespace COL.Screens
{
    public class InfoCharacters : GameScreen
    {
        private PopupError _error;

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            this._error = new PopupError(345, 150, "Đang lấy thông tin nhân vật...", FontManager.GetFont("Font12"), Color.White, false);
            this._error.IsVisible = true;

            this.GetInfoCharacter();
        }
        private void GetInfoCharacter()
        {
            Network.outmsg = Network.Client.CreateMessage();

            Network.outmsg.Write("GetInfoAllCharacters");
            Network.outmsg.Write(Infomations.AccountID);

            Network.Client.SendMessage(Network.outmsg, NetDeliveryMethod.ReliableOrdered);
        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            
            if(Infomations.CharsTotal != -1)
            {
                Thread.Sleep(300);
                this.ScreenManager.AddScreen(new CharacterScreen(), null);
                this.ExitScreen();
            }
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            this.ScreenManager.SpriteBatch.Begin();
            this._error.Draw(this.ScreenManager.SpriteBatch);
            this.ScreenManager.SpriteBatch.End();
        }
    }
}
