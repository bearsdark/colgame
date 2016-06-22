using COL.GameFramework.Fonts;
using COL.GameFramework.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.GameObjects.Npcs
{
    public class ConversationSelect
    {
        private string str;
        private Vector2 position;
        private Texture2D textureSelected;
        private Rectangle rectTexture;
        private SpriteFont font;

        public bool selected;

        public ConversationSelect(string str, Vector2 position)
        {
            this.str = str;
            this.position = position;
            this.font = FontManager.GetFont("Font12");

            this.textureSelected = TextureManager.GetTexture("selectStr");
            this.rectTexture = new Rectangle((int)this.position.X, (int)this.position.Y - 5, 200, 20);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.selected)
                spriteBatch.Draw(this.textureSelected, this.rectTexture, Color.White);

            spriteBatch.DrawString(this.font, this.str, this.position, Color.Yellow);
        }
    }
}
