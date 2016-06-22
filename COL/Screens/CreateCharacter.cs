using COL.GameFramework;
using COL.GameFramework.Textures;
using COL.GameObjects;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.Screens
{
    public class CreateCharacter : GameScreen
    {
        private Texture2D background;
        private Texture2D playerReview;
        private Rectangle rectBackground;
        private Rectangle rectPlayerReview;

        private ButtonClick deleteChar;

        private ButtonClick createChar;

        private ButtonClick exit;

        private Texture2D listChar;
        private Rectangle listCharRect;

        private List<CharacterSelection> list = new List<CharacterSelection>();

        public static int characterSelect;

        public override void Activate(bool instancePreserved)
        {
            //Screen;
            this.background = TextureManager.GetTexture("bg");
            this.rectBackground = new Rectangle(0, 0, Game1.CONFIG_WIDTH, Game1.CONFIG_HEIGHT);

            this.listChar = TextureManager.GetTexture("listChar");
            this.listCharRect = new Rectangle((Game1.CONFIG_WIDTH / 2) + 240,
                (Game1.CONFIG_HEIGHT / 2) - 200, this.listChar.Width, this.listChar.Height);

            //Button;
            this.createChar = new ButtonClick("ButtonGrenNormal", new Point((Game1.CONFIG_WIDTH / 2) + 450,
                (Game1.CONFIG_HEIGHT / 2) + 230), "ButtonGrenHover", 120, 50, "Font12", "Tạo nhân vật");

            this.exit = new ButtonClick("ButtonReadNormal", new Point((Game1.CONFIG_WIDTH / 2) + 450, Game1.CONFIG_HEIGHT - 550), "ButtonReadHover", 75, 30, "Font12", "Thoát");

            this.deleteChar = new ButtonClick("ButtonReadNormal", new Point(20, 510), "ButtonReadHover", 120, 50, "Font12", "Xóa nhân vật");

            int height = 180;
            for (int i = 0; i < Infomations.ListCharacters.Count; i++)
            {
                if (i == 0)
                {
                    this.playerReview = TextureManager.GetTexture(Infomations.ListCharacters[i].Texture);
                    characterSelect = Infomations.ListCharacters[i].ID;
                }
                this.list.Add(new CharacterSelection("listChar2", new Vector2((Game1.CONFIG_WIDTH / 2) + 260,
                    (Game1.CONFIG_HEIGHT / 2) - height), Infomations.ListCharacters[i].ID, Infomations.ListCharacters[i].Name, Infomations.ListCharacters[i].Class));
                height -= 70;
            }

            this.rectPlayerReview = new Rectangle((Game1.CONFIG_WIDTH / 2) - 128, (Game1.CONFIG_HEIGHT / 2) - 225, this.playerReview.Width, this.playerReview.Height);

            base.Activate(instancePreserved);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            this.createChar.HandleInput(gameTime);
            this.deleteChar.HandleInput(gameTime);
            this.exit.HandleInput(gameTime);

            if (this.createChar.Clicked)
            {
                this.ExitScreen();
                this.ScreenManager.AddScreen(new CreateCharacter(), null);
            }
            else if (this.exit.Clicked)
            {
                this.ScreenManager.Game.Exit();
            }

            base.HandleInput(gameTime, input);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            for (int i = 0; i < Infomations.ListCharacters.Count; i++)
            {
                if (Infomations.ListCharacters[i].ID.Equals(characterSelect))
                {
                    this.playerReview = TextureManager.GetTexture(Infomations.ListCharacters[i].Texture);
                    break;
                }
            }

            for (int i = 0; i < this.list.Count; i++)
            {
                this.list[i].Update(gameTime);

                if (this.list[i].charID.Equals(characterSelect))
                    this.list[i].isClicked = true;
                else
                    this.list[i].isClicked = false;
            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            this.ScreenManager.SpriteBatch.Begin();

            this.ScreenManager.SpriteBatch.Draw(this.background, this.rectBackground, Color.White);

            this.createChar.Draw(this.ScreenManager.SpriteBatch);
            this.deleteChar.Draw(this.ScreenManager.SpriteBatch);
            this.exit.Draw(this.ScreenManager.SpriteBatch);

            for (int i = 0; i < list.Count; i++)
            {
                this.list[i].Draw(this.ScreenManager.SpriteBatch);
            }

            this.ScreenManager.SpriteBatch.Draw(this.playerReview, this.rectPlayerReview, Color.White);

            this.ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
