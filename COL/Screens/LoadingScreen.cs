using GameStateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using COL.GameFramework.TextureManager;
using COL.Helpers;
using Microsoft.Xna.Framework.Graphics;
using COL.GameFramework.Textures;
using COL.GameFramework.Fonts;
using System.Diagnostics;
using COL.GameFramework.Sounds;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace COL.Screens
{
    public class LoadingScreen : GameScreen
    {
        private int _maxIndex;
        private int _curentIndex;
        private Texture2D loadingBarBg;
        private Texture2D loading;
        private Rectangle barRectangle;
        private Rectangle rectLoading;
        private int widthForLoading;

        private List<Object> _datas;

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            this.loadingBarBg = this.ScreenManager.Game.Content.Load<Texture2D>("Texture/Background/loadingBarBg");
            this.loading = this.ScreenManager.Game.Content.Load<Texture2D>("Texture/Other/loadingLoad");

            this.barRectangle = new Rectangle(Game1.CONFIG_WIDTH / 2 - this.loadingBarBg.Width / 2, Game1.CONFIG_HEIGHT / 2 - this.loadingBarBg.Height / 2, this.loadingBarBg.Width + 6, this.loadingBarBg.Height);

            this._maxIndex = 0;
            this._curentIndex = 0;

            this._datas = new List<object>();

            List<TextureData> textureData = DataHelpers.GetDataContent<List<TextureData>>("../../../Assets/TextureData.xml");
            if (textureData != null && textureData.Any())
            {
                this._maxIndex += textureData.Count;
                this._datas.AddRange(textureData);
            }

            List<FontData> fontData = DataHelpers.GetDataContent<List<FontData>>("../../../Assets/FontData.xml");
            if (fontData != null && fontData.Any())
            {
                this._maxIndex += fontData.Count;
                this._datas.AddRange(fontData);
            }

            this.rectLoading = new Rectangle((int)this.barRectangle.X + 24, (int)this.barRectangle.Y + 26, 1, this.loading.Height);

            this.widthForLoading = this.loadingBarBg.Width / this._maxIndex;
        }
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (this._curentIndex >= this._maxIndex)
            {
                this.ExitScreen();
                this.ScreenManager.AddScreen(new FirstConnection(), null);
                return;
            }

            if (this._datas[this._curentIndex].GetType() == typeof(TextureData))
            {
                TextureData textureData = this._datas[this._curentIndex] as TextureData;
                Texture2D texture = this.ScreenManager.Game.Content.Load<Texture2D>(textureData.Path);
                TextureManager.AddTexture(textureData.AssetName, texture);
            }
            else if (this._datas[this._curentIndex].GetType() == typeof(FontData))
            {
                FontData fontData = this._datas[this._curentIndex] as FontData;
                SpriteFont font = this.ScreenManager.Game.Content.Load<SpriteFont>(fontData.Path);
                FontManager.AddFont(fontData.AssetName, font);
            }
            this._curentIndex++;

            this.rectLoading.Width += this.widthForLoading;
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            this.ScreenManager.SpriteBatch.Begin();
            this.ScreenManager.SpriteBatch.Draw(this.loadingBarBg, this.barRectangle, Color.White);
            this.ScreenManager.SpriteBatch.Draw(this.loading, this.rectLoading, Color.White);
            this.ScreenManager.SpriteBatch.End();
        }
    }
}
