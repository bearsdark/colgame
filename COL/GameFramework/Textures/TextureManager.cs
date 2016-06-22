using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.GameFramework.Textures
{
    public class TextureManager
    {
        private static TextureManager _instance = new TextureManager();
        private Dictionary<string, Texture2D> _textures;
        public TextureManager()
        {
            this._textures = new Dictionary<string, Texture2D>();
        }

        public static void AddTexture(string name, Texture2D texture)
        {
            if (!_instance._textures.ContainsKey(name)) //Kiểm tra nếu không tồn tại Texture.
            {
                _instance._textures.Add(name, texture);
            }
        }

        public static Texture2D GetTexture(string name)
        {
            if (_instance._textures.ContainsKey(name)) //Kiểm tra nếu tồn tại Texture.
            {
                return _instance._textures[name];
            }
            return null;
        }
    }
}
