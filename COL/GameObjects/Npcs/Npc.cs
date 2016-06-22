using COL.GameFramework.Fonts;
using COL.GameFramework.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.GameObjects.Npcs
{
    public class Npc
    {
        private int id;
        private string name;
        private Texture2D texture;
        private Rectangle rectangle;
        private string str;
        private string MapName;
        public static List<Npc> ListNpc = new List<Npc>();

        public string Str
        {
            get { return this.str; }
            set { this.str = value; }
        }
        public int ID
        {
            get { return this.id; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }
        public Rectangle Rectangle
        {
            get { return this.rectangle; }
            set { this.rectangle = value; }
        }

        public Npc(int id, string Name, string Texture, int X, int Y, string MapName)
        {
            this.id = id;
            this.name = Name;
            this.texture = TextureManager.GetTexture(Texture);
            this.rectangle = new Rectangle(X, Y, this.texture.Width, this.texture.Height);
            this.MapName = MapName;
            this.str = "Chao mung ban den voi the gioi game";
        }
        public void HandleInput(GameTime gameTime)
        {
        }
        public void Update(GameTime gameTime)
        {
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.rectangle, Color.White * 0.5f);
        }
    }
}
