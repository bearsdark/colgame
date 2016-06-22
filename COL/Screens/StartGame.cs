using GameStateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using COL.GameFramework.Textures;
using COL.Helpers;
using System.Diagnostics;
using COL.GameObjects;

namespace COL.Screens
{
    public class StartGame : GameScreen
    {
        private Texture2D textureBG;

        private Rectangle rectBG;

        private List<ButtonClick> listButton = new List<ButtonClick>();

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            this.listButton.Add(new ButtonClick("btnSignin", new Point(500, 310), 230, 100, new Rectangle(550, 345, 130, 38)));
            this.listButton.Add(new ButtonClick("btnExitgame", new Point(500, 410), 230, 100, new Rectangle(550, 445, 130, 38)));

            this.textureBG = TextureManager.GetTexture("StartGameBG");

            this.rectBG = new Rectangle(0, 0, this.textureBG.Width, this.textureBG.Height);
        }
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            foreach(ButtonClick button in this.listButton)
            {
                button.HandleInput(gameTime);
            }

            if (this.listButton[0].Clicked)
            {
                this.ExitScreen();
                this.ScreenManager.AddScreen(new FirstConnection(), null);
            }
            if (this.listButton[1].Clicked)
            {
                this.ScreenManager.Game.Exit();
            }
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            this.ScreenManager.SpriteBatch.Begin();

            this.ScreenManager.SpriteBatch.Draw(this.textureBG, this.rectBG, Color.White);

            foreach (ButtonClick button in this.listButton)
                button.Draw(this.ScreenManager.SpriteBatch);

            this.ScreenManager.SpriteBatch.End();
        }
    }
}
