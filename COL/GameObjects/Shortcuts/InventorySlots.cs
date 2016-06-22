using COL.GameFramework;
using COL.GameFramework.Textures;
using COL.Helpers;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace COL.GameObjects.Shortcuts
{
    public class InventorySlots
    {
        public bool isHover;
        public bool isActive;

        private Texture2D bgHover;
        private Texture2D bgActive;

        public Rectangle rect;
        private Rectangle rectBgHover;
        private Rectangle rectBgActive;

        public Point position;

        public int ItemID;

        public static List<ItemMove> ListItemMove = new List<ItemMove>();

        public InventorySlots(Rectangle rect, Point pos)
        {
            this.rect = rect;
            this.position = pos;

            this.bgHover = TextureManager.GetTexture("hoverInventory");
        }
        public void ActiveItem(int ItemID, string ItemTexture, bool update = false)
        {
            this.ItemID = ItemID;
            this.isActive = true;
            this.bgActive = TextureManager.GetTexture(ItemTexture);

            if (update)
            {
                for (int i = 0; i < Infomations.ListItemOfCharacter.Count; i++)
                {
                    if (Infomations.ListItemOfCharacter[i].ID == ItemID)
                    {
                        Infomations.ListItemOfCharacter[i].Position = "Inventory|" + this.position.Y.ToString() + "|" + this.position.X.ToString();

                        Network.outmsg = Network.Client.CreateMessage();
                        Network.outmsg.Write("UpdateItemInventory");
                        Network.outmsg.WriteAllProperties(Infomations.ListItemOfCharacter[i]);
                        Network.Client.SendMessage(Network.outmsg, NetDeliveryMethod.ReliableOrdered);

                        break;
                    }
                }
            }
        }
        public void UnActiveItem()
        {
            this.ItemID = 0;
            this.isActive = false;
        }
        public void HandleInput()
        {
            this.rectBgHover = new Rectangle(this.rect.X + 6, this.rect.Y + 3, this.rect.Width - 8, this.rect.Height - 8);
            this.rectBgActive = new Rectangle(this.rect.X + 5, this.rect.Y + 2, this.rect.Width - 5, this.rect.Height - 5);

            if (Game1.MouseRect.Intersects(this.rect) && !this.isActive)
            {
                this.isHover = true;
            }
            else
            {
                this.isHover = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.isHover)
                spriteBatch.Draw(this.bgHover, this.rectBgHover, Color.White * 0.3f);
            if (this.isActive)
                spriteBatch.Draw(this.bgActive, this.rectBgActive, Color.White);
        }
    }
}
