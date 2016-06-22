using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.GameFramework.Fonts
{
    public class FontManager
    {
        private static FontManager _instance = new FontManager();
        private Dictionary<string, SpriteFont> _fonts;
        public FontManager()
        {
            this._fonts = new Dictionary<string, SpriteFont>();
        }

        public static void AddFont(string name, SpriteFont font)
        {
            if (!_instance._fonts.ContainsKey(name)) //Kiểm tra nếu không tồn tại Font.
            {
                _instance._fonts.Add(name, font);
            }
        }

        public static SpriteFont GetFont(string name)
        {
            if (_instance._fonts.ContainsKey(name)) //Kiểm tra nếu tồn tại Font.
            {
                return _instance._fonts[name];
            }
            return null;
        }
    }
}
