using GameStateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using COL.GameFramework.Textures;
using COL.GameFramework.Fonts;
using COL.Helpers;
using Microsoft.Xna.Framework.Input;
using COL.GameObjects;
using System.Diagnostics;
using COL.GameFramework;
using System.Threading;
using Lidgren.Network;

namespace COL.Screens
{
    public class LoginGame : GameScreen
    {
        private PopupError Err;

        private Texture2D textureBG;

        private Rectangle rectBG;

        private bool usernameSelected;
        private bool passwordSelected;
        public static bool loginProcess;

        private SpriteFont font12;

        private float spaceTimeUser;
        private float spaceTimePass;

        private string usernameTxtTemp;
        private string passwordTxtTemp;

        private TextInput textInputUsername;
        private TextInput textInputPassword;

        private List<ButtonClick> listButton = new List<ButtonClick>();

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            this.Err = new PopupError(345, 180, "Tài khoản hoặc mật khẩu không hợp lệ.", FontManager.GetFont("Font12"), Color.White);

            this.textureBG = TextureManager.GetTexture("LoginGameBG");

            this.listButton.Add(new ButtonClick("UsernameInput", new Point(480, 280)));
            this.listButton.Add(new ButtonClick("PasswordInput", new Point(480, 340)));
            this.listButton.Add(new ButtonClick("btnLogin", new Point(480, 420)));

            this.rectBG = new Rectangle(0, 0, this.textureBG.Width, this.textureBG.Height);

            this.usernameSelected = true;
            this.passwordSelected = false;

            this.font12 = FontManager.GetFont("Font12");
            this.usernameTxtTemp = "";
            this.passwordTxtTemp = "";

            this.textInputUsername = new TextInput(18);
            this.textInputPassword = new TextInput(24, "*");
        }
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            this.Err.HandleInput(gameTime);

            foreach (ButtonClick button in this.listButton)
                button.HandleInput(gameTime);

            if (Functions.KeyboardPressed(Keys.Tab))
            {
                if (this.usernameSelected)
                {
                    this.usernameSelected = false;
                    this.passwordSelected = true;
                }
                else if (this.passwordSelected)
                {
                    this.passwordSelected = false;
                    this.usernameSelected = true;
                }
                else
                {
                    this.usernameSelected = true;
                }
            }
        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            if (loginProcess)
            {
                loginProcess = false;
                if (Infomations.AccountIsLogin)
                {
                    this.ScreenManager.AddScreen(new InfoCharacters(), null);
                    this.ExitScreen();
                }
                else
                {
                    this.Err.IsVisible = true;
                    this.Err.textErr = "Tài khoản hoặc mật khẩu không đúng.";
                }
            }

            if (this.listButton[2].Clicked || ((this.passwordSelected || this.usernameSelected) && Functions.KeyboardPressed(Keys.Enter) && !this.Err.IsVisible))
            {
                if(this.CheckValidAccountLogin(this.textInputUsername.text, this.textInputPassword.text))
                {
                    if(Game1.CONNECT_STATUS == "Connected")
                    {
                        this.AccountLogin(this.textInputUsername.text, this.textInputPassword.text);
                    }
                    else
                    {
                        this.Err.IsVisible = true;
                        this.Err.textErr = "Kết nối với máy chủ thất bại.\nBạn vui lòng kiểm tra lại đường truyền.";
                    }
                }
                else
                    this.Err.IsVisible = true;
                this.listButton[2].Clicked = false;
            }
            if (this.listButton[0].Clicked)
            {
                this.passwordSelected = false;
                this.usernameSelected = true;
                this.listButton[0].Clicked = false;
            }
            if (this.listButton[1].Clicked)
            {
                this.passwordSelected = true;
                this.usernameSelected = false;
                this.listButton[1].Clicked = false;
            }

            if (this.usernameSelected)
            {
                this.passwordTxtTemp = "";
                this.textInputUsername.Update(gameTime);

                this.spaceTimeUser += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (this.spaceTimeUser >= 500)
                {
                    this.usernameTxtTemp = "|";
                }
                if (this.spaceTimeUser >= 1000)
                {
                    this.spaceTimeUser = 0;
                    this.usernameTxtTemp = "";
                }
            }
            else if (this.passwordSelected)
            {
                this.usernameTxtTemp = "";
                this.textInputPassword.Update(gameTime);

                this.spaceTimePass += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (this.spaceTimePass >= 500)
                {
                    this.passwordTxtTemp = "|";
                }
                if (this.spaceTimePass >= 1000)
                {
                    this.spaceTimePass = 0;
                    this.passwordTxtTemp = "";
                }
            }
        }
        private bool CheckValidAccountLogin(string username, string password)
        {
            if (username.Length < 4)
                return false;
            if (password.Length < 4)
                return false;

            return true;
        }
        private void AccountLogin(string username, string password)
        {
            Network.outmsg = Network.Client.CreateMessage();

            Network.outmsg.Write("AccountLogin");
            Network.outmsg.Write(username);
            Network.outmsg.Write(Functions.md5(password));

            Network.Client.SendMessage(Network.outmsg, NetDeliveryMethod.ReliableOrdered);
        }
        private void CharacterConnect()
        {
            Network.outmsg = Network.Client.CreateMessage();

            Network.outmsg.Write("characterConnect");
            Network.outmsg.Write(Game1.playerName);
            Network.outmsg.Write(50);
            Network.outmsg.Write(50);

            Network.Client.SendMessage(Network.outmsg, NetDeliveryMethod.ReliableOrdered);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            this.ScreenManager.SpriteBatch.Begin();

            this.ScreenManager.SpriteBatch.Draw(this.textureBG, this.rectBG, Color.White);

            foreach (ButtonClick button in this.listButton)
                button.Draw(this.ScreenManager.SpriteBatch);

            this.ScreenManager.SpriteBatch.DrawString(this.font12, this.textInputUsername.text + this.usernameTxtTemp, new Vector2(585, 312), Color.White);
            this.ScreenManager.SpriteBatch.DrawString(this.font12, this.textInputPassword.textReplace + this.passwordTxtTemp, new Vector2(585, 372), Color.White);

            if (this.Err.IsVisible)
                this.Err.Draw(this.ScreenManager.SpriteBatch);

            this.ScreenManager.SpriteBatch.End();
        }
    }
}
