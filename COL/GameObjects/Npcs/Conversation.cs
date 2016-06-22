using COL.GameFramework.Fonts;
using COL.GameFramework.Textures;
using COL.GameObjects.Players;
using COL.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace COL.GameObjects.Npcs
{
    public class Conversation
    {
        private Texture2D texture;
        private Rectangle rectangle;
        private float opacity;
        private bool isVisible;

        private SpriteFont font12;
        private string str;
        private Vector2 positionStr;
        private Color colorStr = Color.White;
        
        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }
        public string Content
        {
            get { return this.str; }
            set { this.str = value; }
        }
        public static bool Visible = false;

        public Conversation(float opacity = 0.5f, int X = (Game1.CONFIG_WIDTH / 2) - 287, int Y = (Game1.CONFIG_HEIGHT / 2) - 60, string properties = "max")
        {
            this.texture = TextureManager.GetTexture("conversationMin");

            this.rectangle = new Rectangle(X, Y, this.texture.Width, this.texture.Height);
            this.isVisible = false;
            this.opacity = opacity;

            this.font12 = FontManager.GetFont("Font12");
            this.positionStr = new Vector2(X + 10, Y + 10);
        }
        public void Update(GameTime gameTime)
        {
        }
        public void HandleInput(GameTime gameTime)
        {
            Visible = this.isVisible;

            if ((Functions.MouseClick() || Functions.KeyboardPressed(Keys.Space)) && this.isVisible)
            {
                this.isVisible = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.isVisible == true)
            {
                spriteBatch.Draw(this.texture, this.rectangle, Color.White * this.opacity);
                spriteBatch.DrawString(this.font12, this.str, this.positionStr, this.colorStr);
            }
        }
    }
}
