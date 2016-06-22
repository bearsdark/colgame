using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COL.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using COL.Helpers;
using COL.GameFramework;
using Lidgren.Network;

namespace COL.GameObjects.Shortcuts
{
    public class Shortcut
    {
        private Esc_Shortcut Esc;
        private Inventory_Shortcut Inventory;
        private Quest_Shortcut Quest;

        public static bool IsVisibile;

        public Shortcut(ScreenManager screenManager)
        {
            this.Esc = new Esc_Shortcut(screenManager);
            this.Inventory = new Inventory_Shortcut();
            this.Quest = new Quest_Shortcut();

            IsVisibile = false;
        }

        private Keys[] listKeyShortcut = new Keys[]
        {
            Keys.Escape, Keys.I, Keys.L
        };

        public void HandleInput(GameTime gameTime)
        {
            foreach(Keys key in this.listKeyShortcut)
            {
                if (Functions.KeyboardPressed(key))
                {
                    switch (key)
                    {
                        case Keys.Escape:
                            {
                                this.Quest.IsVisible = false;
                                this.Inventory.isVisible = false;

                                if (this.Esc.isVisible)
                                {
                                    IsVisibile = false;
                                    this.Esc.isVisible = false;
                                    Option_Shortcut.isVisible = false;
                                }
                                else
                                {
                                    IsVisibile = true;
                                    this.Esc.isVisible = true;
                                    Option_Shortcut.isVisible = false;
                                }
                            }
                            break;
                        case Keys.I:
                            {
                                this.Esc.isVisible = false;
                                this.Quest.IsVisible = false;

                                if (this.Inventory.isVisible)
                                    this.Inventory.isVisible = false;
                                else
                                    this.Inventory.isVisible = true;
                            }
                            break;
                        case Keys.L:
                            {
                                this.Esc.isVisible = false;
                                this.Inventory.isVisible = false;

                                if (this.Quest.IsVisible)
                                    this.Quest.IsVisible = false;
                                else
                                    this.Quest.IsVisible = true;
                            }
                            break;
                    }
                }
            }

            if (this.Esc.isVisible)
                this.Esc.HandleInput(gameTime);

            if (this.Inventory.isVisible)
                this.Inventory.HandleInput(gameTime);

            if (this.Quest.IsVisible)
                this.Quest.Input();
        }
        public void Update(GameTime gameTime)
        {
            if (this.Esc.isVisible)
                this.Esc.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            if (this.Esc.isVisible)
                this.Esc.Draw(spriteBatch);

            if (this.Inventory.isVisible)
                this.Inventory.Draw(spriteBatch);

            if (this.Quest.IsVisible)
                this.Quest.Draw(spriteBatch);
        }
    }
}
