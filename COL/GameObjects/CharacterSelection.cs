using COL.GameFramework.Fonts;
using COL.GameFramework.Textures;
using COL.Helpers;
using COL.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace COL.GameObjects
{
    public class CharacterSelection
    {
        private Texture2D texture;
        public Rectangle textureRect;

        public Vector2 position;

        private Color colour = new Color(255, 255, 255, 255);
        private Color color = Color.White;

        public bool down;
        public bool isClicked = false;
        public bool preview;
        
        public int charID;

        private string charName;
        private string charClass;

        private SpriteFont font;

        public CharacterSelection(string texture, Vector2 newPosition, int id, string name, string charClass)
        {
            this.texture = TextureManager.GetTexture(texture);
            this.position = new Vector2(newPosition.X, newPosition.Y);
            this.textureRect = new Rectangle((int)position.X, (int)position.Y, this.texture.Width, this.texture.Height);
            this.charID = id;
            this.charName = name;
            this.charClass = charClass;
            this.font = FontManager.GetFont("Font12");
        }

        public void Update(GameTime gameTime)
        {
            //timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //speed.X += 2;
            //this.position.X += speed.X;
            if (Game1.MouseRect.Intersects(textureRect))
            {
                //if(timer >= 2)
                //{
                //    position.X = 0;
                //}

                if (colour.A >= 255) down = false;
                if (colour.A <= 0) down = true;

                if (down) colour.A += 5;
                else colour.A -= 5;

                if (Functions.MouseClick())
                {
                    colour.A = 0;
                    isClicked = true;
                    CharacterScreen.characterSelectID = this.charID;
                }
            }
            else if (colour.A < 255 && !isClicked)
                colour.A += 5;

            if (isClicked)
            {
                colour.A = 0;
                this.color = Color.Red;
            }
            else
                this.color = Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, textureRect, colour);
            spriteBatch.DrawString(this.font, "Tên nhân vật: " + this.charName, new Vector2(this.textureRect.X + 10, this.textureRect.Y + 5), this.color);
            spriteBatch.DrawString(this.font, "Class: " + this.charClass, new Vector2(this.textureRect.X + 10, this.textureRect.Y + 25), this.color);
        }
    }
}
