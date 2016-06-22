using COL.GameFramework.Fonts;
using COL.GameFramework.Textures;
using COL.Helpers;
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
    public class Option_Shortcut
    {
        public static bool isVisible;
        public Texture2D bg;
        private Texture2D bg2;
        private SpriteFont font9;
        private Point positionMouseInBg;
        private Texture2D volume1;
        private Texture2D volume2;
        private Texture2D volume_bar;
        private Texture2D circleVolume;
        private Rectangle rectVolume1;
        private Rectangle rectVolume2;
        private Rectangle rectVolumebar;
        private Rectangle rectCircleVolume;
        public Rectangle rectBg;
        private Rectangle rectBg2;
        private int mouseTouchID;

        private bool isMove;
        private bool isTouch;
        private bool changeVolume;

        public Option_Shortcut()
        {
            this.bg = TextureManager.GetTexture("bgShortcut");
            this.bg2 = TextureManager.GetTexture("bgShortcut0_1");
            this.volume1 = TextureManager.GetTexture("-");
            this.volume2 = TextureManager.GetTexture("+");
            this.volume_bar = TextureManager.GetTexture("volume_bar");
            this.circleVolume = TextureManager.GetTexture("circle");

            this.rectBg = new Rectangle((Game1.CONFIG_WIDTH / 2) - (this.bg.Width / 2), (Game1.CONFIG_HEIGHT / 2) - (this.bg.Height / 2), this.bg.Width, this.bg.Height);

            this.font9 = FontManager.GetFont("Font9");
        }
        public void HandleInput(GameTime gameTime)
        {

            if (Functions.MouseTouch())
            {
                if (Game1.MouseRect.Intersects(this.rectCircleVolume) && !this.isTouch)
                {
                    this.isTouch = true;
                    this.mouseTouchID = 1;
                    this.changeVolume = true;
                }
                else if (Game1.MouseRect.Intersects(this.rectBg) && !Game1.MouseRect.Intersects(this.rectBg2) && !this.isTouch)
                {
                    this.isTouch = true;
                    this.mouseTouchID = 2;
                }
            }
            else
            {
                this.isTouch = false;
                this.mouseTouchID = 0;
            }

            /*******Mouse Touch Background Shortcut*******/
            if (this.mouseTouchID == 2)
                this.isMove = true;
            else
            {
                this.isMove = false;
                this.positionMouseInBg = new Point(Mouse.GetState().X - this.rectBg.X, Mouse.GetState().Y - this.rectBg.Y);
            }

            if (this.isMove)
                this.rectBg = new Rectangle(Mouse.GetState().X - this.positionMouseInBg.X, Mouse.GetState().Y - this.positionMouseInBg.Y, this.bg.Width, this.bg.Height);

            /*******Mouse Touch Volume*******/

            if(this.mouseTouchID == 1 && this.changeVolume)
            {
                float temp = (float)Mouse.GetState().X - this.rectVolumebar.X;
                temp = (float)temp / this.rectVolumebar.Width;
                temp = (float)temp * 100f;
                Game1.Volume = (int)temp;
            }

            this.ChangeVolumeWithButton();
            this.ChangeVolumeWithBar();
        }
        private void ChangeVolumeWithBar()
        {
            if (Functions.MouseClick())
            {
                if (Game1.MouseRect.Intersects(this.rectVolumebar))
                {
                    if (Game1.MouseRect.X < this.rectCircleVolume.X)
                    {
                        Game1.Volume -= 5;
                    }
                    else if(Game1.MouseRect.X > this.rectCircleVolume.X)
                    {
                        Game1.Volume += 5;
                    }
                }
            }
        }
        private void ChangeVolumeWithButton()
        {
            if (Functions.MouseClick())
            {
                if (Game1.MouseRect.Intersects(this.rectVolume1))
                {
                    Game1.Volume--;
                }
                else if (Game1.MouseRect.Intersects(this.rectVolume2))
                {
                    Game1.Volume++;
                }
            }
        }
        public void Update(GameTime gameTime)
        {
            this.rectVolume1 = new Rectangle(this.rectBg.X + 15, this.rectBg.Y + 60, this.volume1.Width, this.volume1.Height);
            this.rectVolume2 = new Rectangle(this.rectBg.Right - 27, this.rectBg.Y + 60, this.volume2.Width, this.volume2.Height);
            this.rectVolumebar = new Rectangle(this.rectBg.X + 28, this.rectBg.Y + 64, 179, this.volume_bar.Height);
            this.rectBg2 = new Rectangle(this.rectBg.X + 7, this.rectBg.Y + 30, this.bg2.Width, this.bg2.Height + 5);

            int XCircle = (int)(this.rectVolumebar.X + ((float)this.rectVolumebar.Width / 100 * Game1.Volume) - (this.circleVolume.Width / 2));
            this.rectCircleVolume = new Rectangle(XCircle, this.rectVolumebar.Y - 3, this.circleVolume.Width, this.circleVolume.Height);

            if (this.rectCircleVolume.X <= this.rectVolumebar.X || Game1.Volume < 0)
            {
                this.rectCircleVolume.X = this.rectVolumebar.X + 1;
                Game1.Volume = 0;
            }
            else if (this.rectCircleVolume.X >= this.rectVolumebar.Right - this.rectCircleVolume.Width || Game1.Volume > 100)
            {
                this.rectCircleVolume.X = this.rectVolumebar.Right - this.rectCircleVolume.Width - 1;
                Game1.Volume = 100;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.bg, this.rectBg, Color.White * 0.95f);
            spriteBatch.DrawString(this.font9, "Tuy chon", new Vector2(this.rectBg.X + this.rectBg.Width / 2 - 22, this.rectBg.Y + 8), Color.White);
            spriteBatch.Draw(this.bg2, this.rectBg2, Color.White);
            spriteBatch.DrawString(this.font9, "Am luong:", new Vector2(this.rectBg.X + 10, this.rectBg.Y + 40), Color.White);
            spriteBatch.Draw(this.volume1, this.rectVolume1, Color.White);
            spriteBatch.Draw(this.volume2, this.rectVolume2, Color.White);
            spriteBatch.Draw(this.volume_bar, this.rectVolumebar, Color.White);
            spriteBatch.Draw(this.circleVolume, this.rectCircleVolume, Color.White);
        }
    }
}
