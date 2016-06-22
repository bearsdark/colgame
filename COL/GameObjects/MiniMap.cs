using COL.GameFramework.Textures;
using COL.GameObjects.Camera;
using COL.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace COL.GameObjects
{
    public class MiniMap
    {
        private Viewport viewport;

        private Texture2D player;
        private Texture2D map;

        private Rectangle rect;
        private Rectangle rectMap;

        private Vector2 position;
        private Vector2 scale;
        private Vector2 playerPos;
        private Vector2 detectCenter;
        private Vector2 positionMap;
        public Vector2 playerPosition;

        private int updateWidth;
        private int updateHeight;

        private bool isUpdateWidth;
        private bool isUpdateHeight;

        public MiniMap(Viewport viewport, Texture2D map, Rectangle rectangle, Vector2 position, Vector2 scale)
        {
            this.map = map;
            this.player = TextureManager.GetTexture("playerMini");
            this.rectMap = rectangle;
            this.rect = new Rectangle(0, 0, this.rectMap.Width / 2, this.rectMap.Height / 2);
            this.scale = scale;
            this.position = position;
            this.viewport = viewport; //1002 17
            this.positionMap = position + new Vector2(44, 27);
        }

        public void Update(GameTime gameTime)
        {

            Vector2 temp = this.positionMap + this.playerPosition * this.scale;
            this.playerPos = temp;

            this.detectCenter.X = this.positionMap.X + this.rect.Width / 2 * this.scale.X;
            this.detectCenter.Y = this.positionMap.Y + this.rect.Height / 2 * this.scale.Y;
            
            if (this.playerPos.X >= this.detectCenter.X)
            {
                if (!this.isUpdateWidth)
                {
                    this.isUpdateWidth = true;
                    this.updateWidth = (int)(this.detectCenter.X - this.playerPosition.X);
                }

                this.playerPos.X = this.detectCenter.X;
                this.rect.X = (int)(this.playerPosition.X - this.detectCenter.X + this.updateWidth);
            }
            if(this.playerPos.Y >= this.detectCenter.Y)
            {
                if (!this.isUpdateHeight)
                {
                    this.isUpdateHeight = true;
                    this.updateHeight = (int)(this.detectCenter.Y - this.playerPosition.Y);
                }

                this.playerPos.Y = this.detectCenter.Y;
                this.rect.Y = (int)(this.playerPosition.Y - this.detectCenter.Y + this.updateHeight);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(TextureManager.GetTexture("minimap"), this.position, Color.White);
            spriteBatch.Draw(this.map, this.positionMap, this.rect, Color.White, 0f, Vector2.Zero, this.scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(this.player, this.playerPos, Color.White);
            spriteBatch.End();
        }
    }
}
