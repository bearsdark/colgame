using COL.GameFramework.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.GameObjects
{
    public class Animation
    {
        public Texture2D texture;
        public Vector2 position;
        public Rectangle rectangle;

        private float spaceTime;
        public float frameTime;
        public int frames;
        private int currentFrame;
        private int frameWidth;
        private int frameHeight;
        private bool looping;

        public Animation(string textureName, float frameSpeed, int frames, Vector2 position, bool looping)
        {
            this.frameTime = frameSpeed;
            this.frames = frames;
            this.looping = looping;
            this.texture = TextureManager.GetTexture(textureName);

            this.frameWidth = (this.texture.Width / this.frames);
            this.frameHeight = this.texture.Height;

            this.position = position;
        }
        public void SetAgainAnimation(string textureName, float frameSpeed, int frames, bool looping)
        {
            this.frameTime = frameSpeed;
            this.frames = frames;
            this.looping = looping;
            this.texture = TextureManager.GetTexture(textureName);

            this.frameWidth = (this.texture.Width / this.frames);
            this.frameHeight = this.texture.Height;
        }

        public void PlayAnimation(GameTime gameTime)
        {
            this.spaceTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            this.rectangle = new Rectangle(this.currentFrame * this.frameWidth, 0, this.frameWidth, this.frameHeight);

            if(this.spaceTime >= this.frameTime)
            {
                if(this.currentFrame >= this.frames - 1)
                {
                    if (this.looping)
                        this.currentFrame = 0;
                }
                else
                {
                    this.currentFrame++;
                }
                this.spaceTime = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.position, this.rectangle, Color.White);
        }
    }
}
