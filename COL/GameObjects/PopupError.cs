using COL.GameFramework.Textures;
using COL.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.GameObjects
{
    public class PopupError
    {
        private Texture2D background;
        private Texture2D btnOk;

        private Rectangle rectBackground;
        private Rectangle rectBtnOk;

        public string textErr;

        private SpriteFont font;

        public bool IsVisible;
        public bool showOk;

        private Color textColor;

        private float timeShow;

        public PopupError(int width, int height, string text, SpriteFont textFont, Color textColor, bool showButtonOk = true)
        {
            this.background = TextureManager.GetTexture("ErrorBackground");
            this.rectBackground = new Rectangle(Game1.CONFIG_WIDTH / 2 - this.background.Width / 2, Game1.CONFIG_HEIGHT / 2 - this.background.Height / 2, width, height);
            this.showOk = showButtonOk;
            this.textErr = text;
            this.font = textFont;
            this.textColor = textColor;
            if (this.showOk)
            {
                this.btnOk = TextureManager.GetTexture("bgBtn141_25_1");
                this.rectBtnOk = new Rectangle(this.rectBackground.X + this.background.Width / 2 - this.btnOk.Width / 2, this.rectBackground.Bottom - 50, this.btnOk.Width, this.btnOk.Height);
            }
        }
        public PopupError(int width, int height, string text, SpriteFont textFont, Color textColor, int X, int Y, bool showButtonOk = true)
        {
            this.background = TextureManager.GetTexture("ErrorBackground");
            this.rectBackground = new Rectangle(X, Y, width, height);
            this.showOk = showButtonOk;
            this.textErr = text;
            this.font = textFont;
            this.textColor = textColor;
            if (this.showOk)
            {
                this.btnOk = TextureManager.GetTexture("bgBtn141_25_1");
                this.rectBtnOk = new Rectangle(this.rectBackground.X + this.background.Width / 2 - this.btnOk.Width / 2, this.rectBackground.Bottom - 50, this.btnOk.Width, this.btnOk.Height);
            }
        }
        public void HandleInput(GameTime gameTime)
        {
            if (this.IsVisible)
            {
                this.timeShow += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (this.showOk)
                {
                    if ((Functions.MouseClick() && Game1.MouseRect.Intersects(this.rectBtnOk)) || (this.timeShow >= 300 && Functions.KeyboardPressed(Keys.Enter)))
                    {
                        this.timeShow = 0;
                        this.IsVisible = false;
                    }
                }
            }
            else
                this.timeShow = 0;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.IsVisible)
            {
                spriteBatch.Draw(this.background, this.rectBackground, Color.White);
                spriteBatch.DrawString(this.font, this.textErr, new Vector2(this.rectBackground.X + this.background.Width / 2 - this.font.MeasureString(this.textErr).X / 2, this.rectBackground.Y + 75), this.textColor);
                if (this.showOk)
                {
                    spriteBatch.Draw(this.btnOk, this.rectBtnOk, Color.White);
                    spriteBatch.DrawString(this.font, "Đồng ý", new Vector2(this.rectBtnOk.X + this.rectBtnOk.Width / 2 - this.font.MeasureString("Đồng ý").X / 2, this.rectBtnOk.Y + 5), Color.White);
                }
            }
        }
    }
}
