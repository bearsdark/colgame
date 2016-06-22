using COL.GameFramework.Fonts;
using COL.GameObjects;
using COL.Helpers;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.Screens
{
    public class PopupCreateCharacter : GameScreen
    {
        private Texture2D popupBackground;
        private Texture2D textInputBackground;
        private Texture2D btnOk;
        private Texture2D btnCancel;

        private Vector2 backgroundPosition;
        private Vector2 textInputPosition;

        private Rectangle rectBtnOk;
        private Rectangle rectBtnCancel;

        private TextInput textInput;

        private string textInputControl;

        private SpriteFont font;

        private float spaceTimeTextInput;

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            this.font = FontManager.GetFont("Font12");
            this.textInput = new TextInput(20);
            this.spaceTimeTextInput = 0;
        }
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            if (Functions.MouseClick())
            {
                if (Game1.MouseRect.Intersects(this.rectBtnOk))
                {
                    //Click Ok
                }
                else if (Game1.MouseRect.Intersects(this.rectBtnCancel))
                {
                    //Click cancel
                }
            }
        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            this.spaceTimeTextInput += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (this.spaceTimeTextInput >= 500)
                this.textInputControl = "|";
            else if (this.spaceTimeTextInput >= 1000)
            {
                this.textInputControl = "";
                this.spaceTimeTextInput = 0;
            }
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
