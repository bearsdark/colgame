using COL.GameFramework.Fonts;
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
    public class ButtonClick
    {
        private Texture2D texture;

        private Rectangle rectangle;
        private Rectangle rectangleClick;

        private Vector2 textPosition;

        private string textureHover = "";
        private string textureName;
        private string text = null;

        private SpriteFont font;

        public Color TextColor = Color.White;

        private bool clicked;

        public bool Clicked
        {
            get { return this.clicked; }
            set { this.clicked = value; }
        }

        public Point Position
        {
            get
            {
                return new Point(this.rectangle.X, this.rectangle.Y);
            }
            set
            {
                this.rectangle.X = value.X;
                this.rectangle.Y = value.Y;
            }
        }

        public ButtonClick(string textureName, Point position)
        {
            this.texture = TextureManager.GetTexture(textureName);
            this.rectangle = new Rectangle(position.X, position.Y, this.texture.Width, this.texture.Height);
        }
        public ButtonClick(string textureName, Point position, int width, int height)
        {
            this.texture = TextureManager.GetTexture(textureName);
            this.rectangle = new Rectangle(position.X, position.Y, width, height);
        }
        public ButtonClick(string textureName, Point position, int width, int height, Rectangle rectangleClick)
        {
            this.texture = TextureManager.GetTexture(textureName);
            this.rectangle = new Rectangle(position.X, position.Y, width, height);
            this.rectangleClick = rectangleClick;
        }
        public ButtonClick(string textureName, Point position, string textureHover)
        {
            this.texture = TextureManager.GetTexture(textureName);
            this.textureHover = textureHover;
            this.rectangle = new Rectangle(position.X, position.Y, this.texture.Width, this.texture.Height);
            this.textureName = textureName;
        }
        public ButtonClick(string textureName, Point position, string textureHover, int width, int height)
        {
            this.texture = TextureManager.GetTexture(textureName);
            this.textureHover = textureHover;
            this.rectangle = new Rectangle(position.X, position.Y, width, height);
            this.textureName = textureName;
        }
        public ButtonClick(string textureName, Point position, string textureHover = "", int width = 0, int height = 0, string font = "Font12", string text = null, int xText = 0, int yText = 0)
        {
            this.texture = TextureManager.GetTexture(textureName);
            this.textureName = textureName;
            this.textureHover = textureHover;
            this.rectangle = new Rectangle(position.X, position.Y,
                                    (width != 0) ? width : this.texture.Width,
                                    (height != 0) ? height : this.texture.Height);
            if(text != null)
            {
                this.font = FontManager.GetFont(font);
                this.text = text;
                this.textPosition = new Vector2((xText != 0) ? xText : this.rectangle.X + this.rectangle.Width / 2 - this.font.MeasureString(this.text).X / 2,
                                            (yText != 0) ? yText : this.rectangle.Y + this.rectangle.Height / 2 - this.font.MeasureString(this.text).Y / 2);
            }
        }

        public void HandleInput(GameTime gameTime)
        {
            if(this.rectangleClick != Rectangle.Empty)
            {
                if (Game1.MouseRect.Intersects(this.rectangleClick))
                {
                    if (this.textureHover != "")
                        this.texture = TextureManager.GetTexture(this.textureHover);

                    if (Functions.MouseClick())
                        this.clicked = true;
                }
                else
                {
                    if (this.textureHover != "")
                        this.texture = TextureManager.GetTexture(this.textureName);
                }
            }
            else
            {
                if (Game1.MouseRect.Intersects(this.rectangle))
                {
                    if (this.textureHover != "")
                        this.texture = TextureManager.GetTexture(this.textureHover);

                    if (Functions.MouseClick())
                        this.clicked = true;
                }
                else
                {
                    if (this.textureHover != "")
                        this.texture = TextureManager.GetTexture(this.textureName);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.rectangle, Color.White);
            if(this.text != null)
            {
                spriteBatch.DrawString(this.font, this.text, this.textPosition, this.TextColor);
            }
        }
    }
}
