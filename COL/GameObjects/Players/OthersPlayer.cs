using COL.GameFramework.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace COL.GameObjects.Players
{
    public class OthersPlayer
    {
        public string name;
        private Texture2D texture;
        public Vector2 position;
        private Vector2 velocity;
        public Keys keyStatus;
        public static List<OthersPlayer> players = new List<OthersPlayer>();
        
        public OthersPlayer(string name, Vector2 newposition)
        {
            this.name = name;
            this.texture = TextureManager.GetTexture("player");
            this.position = newposition;
        }
        public void Update(GameTime gameTime)
        {
            this.position += this.velocity;
            if(keyStatus == Keys.Left)
            {
                this.velocity.X = -Game1.CONFIG_SPEED_PLAYER;
            }
            else if(keyStatus == Keys.Right)
            {
                this.velocity.X = Game1.CONFIG_SPEED_PLAYER;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.position, Color.White);
            //Debug.WriteLine(this.position);
        }
    }
}
