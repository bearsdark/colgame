using COL.GameFramework.Fonts;
using COL.GameFramework.Textures;
using COL.Helpers;
using COL.Screens;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace COL.GameObjects.Shortcuts
{
    public class Esc_Shortcut
    {
        private Option_Shortcut option;

        public bool isVisible;
        private SpriteFont font11;

        private Texture2D background;
        private Texture2D btnOption;
        private Texture2D btnBack;
        private Texture2D btnExit;

        private Vector2 position;

        private Rectangle rectBtnOption;
        private Rectangle rectBtnBack;
        private Rectangle rectBtnExit;

        private string optionText;
        private string backText;
        private string exitText;

        private ScreenManager screenManager;

        public Esc_Shortcut(ScreenManager screenManager)
        {
            this.screenManager = screenManager;
            this.option = new Option_Shortcut();
            this.isVisible = false;
            this.font11 = FontManager.GetFont("Font9");

            this.background = TextureManager.GetTexture("EscBg");
            this.btnOption = TextureManager.GetTexture("bgBtn141_25_1");
            this.btnBack = TextureManager.GetTexture("bgBtn141_25_1");
            this.btnExit = TextureManager.GetTexture("bgBtn141_25_1");

            this.position = new Vector2((Game1.CONFIG_WIDTH / 2) - (this.background.Width / 2),
                                        (Game1.CONFIG_HEIGHT / 2) - (this.background.Height / 2));

            this.rectBtnOption = new Rectangle(528, 210, this.btnOption.Width, this.btnOption.Height);
            this.rectBtnBack = new Rectangle(528, 250, this.btnBack.Width, this.btnBack.Height);
            this.rectBtnExit = new Rectangle(528, 290, this.btnExit.Width, this.btnExit.Height);

            this.optionText = "Tùy chọn";
            this.backText = "Trở lại";
            this.exitText = "Thoát";
        }
        public void HandleInput(GameTime gameTime)
        {
            if (Option_Shortcut.isVisible == false)
            {
                this.option.rectBg = new Rectangle((Game1.CONFIG_WIDTH / 2) - (this.option.bg.Width / 2), (Game1.CONFIG_HEIGHT / 2) - (this.option.bg.Height / 2), this.option.bg.Width, this.option.bg.Height);
                if (Game1.MouseRect.Intersects(this.rectBtnOption))
                {
                    this.btnOption = TextureManager.GetTexture("bgBtn141_25_2");
                    if (Functions.MouseClick())
                    {
                        Option_Shortcut.isVisible = true;
                    }
                }
                else if (Game1.MouseRect.Intersects(this.rectBtnBack))
                {
                    this.btnBack = TextureManager.GetTexture("bgBtn141_25_2");
                }
                else if (Game1.MouseRect.Intersects(this.rectBtnExit))
                {
                    this.btnExit = TextureManager.GetTexture("bgBtn141_25_2");
                    if (Functions.MouseClick())
                    {
                        this.screenManager.Game.Exit();
                    }
                }
                else
                {
                    this.btnOption = TextureManager.GetTexture("bgBtn141_25_1");
                    this.btnBack = TextureManager.GetTexture("bgBtn141_25_1");
                    this.btnExit = TextureManager.GetTexture("bgBtn141_25_1");
                }
            }
            else
            {
                this.option.HandleInput(gameTime);
            }
        }
        public void Update(GameTime gameTime)
        {
            if(this.isVisible == false)
                Option_Shortcut.isVisible = false;

            if (Option_Shortcut.isVisible == true)
                this.option.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(Option_Shortcut.isVisible == false)
            {
                spriteBatch.Draw(this.background, this.position, null, Color.White * 0.95f);

                spriteBatch.Draw(this.btnOption, this.rectBtnOption, Color.White);
                spriteBatch.DrawString(this.font11, this.optionText, new Vector2(this.rectBtnOption.X + 45, this.rectBtnOption.Y + 5), Color.White);

                spriteBatch.Draw(this.btnBack, this.rectBtnBack, Color.White);
                spriteBatch.DrawString(this.font11, this.backText, new Vector2(this.rectBtnBack.X + 52, this.rectBtnBack.Y + 5), Color.White);

                spriteBatch.Draw(this.btnExit, this.rectBtnExit, Color.White);
                spriteBatch.DrawString(this.font11, this.exitText, new Vector2(this.rectBtnExit.X + 55, this.rectBtnExit.Y + 5), Color.White);
            }
            else
            {
                this.option.Draw(spriteBatch);
            }
        }
    }
}
