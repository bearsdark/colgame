using COL.GameFramework;
using COL.GameFramework.Fonts;
using COL.GameFramework.Textures;
using COL.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace COL.GameObjects.Shortcuts
{
    public class Inventory_Shortcut
    {
        private SpriteFont font12;

        private string goldTotal;

        private Texture2D bgItem;
        private Texture2D logo;
        private Texture2D gold;
        private Texture2D close;
        
        private Rectangle rectBgItem;
        private Rectangle rectLogo;
        private Rectangle rectGold;
        private Rectangle rectClose;
        private Rectangle[,] inventorySlotsRectangle;

        private Vector2 goldPosition;

        private Point constant;
        private Point Slots;

        public bool isVisible;
        private bool isMove;

        private InventorySlots[,] inVentorySlots;

        public Inventory_Shortcut()
        {
            for (int i = 0; i < Infomations.ListCharacters.Count; i++)
            {
                if(Infomations.ListCharacters[i].ID == Infomations.CharacterConnectID)
                {
                    this.goldTotal = Infomations.ListCharacters[i].Money.ToString();
                    break;
                }
            }
            this.font12 = FontManager.GetFont("Font12");
            this.gold = TextureManager.GetTexture("Gold");
            this.logo = TextureManager.GetTexture("LogoInventory");
            this.bgItem = TextureManager.GetTexture("bgItem");

            this.rectLogo = new Rectangle(Game1.CONFIG_WIDTH / 2 - this.bgItem.Width / 2, Game1.CONFIG_HEIGHT / 2 - this.bgItem.Height/2, this.logo.Width, this.logo.Height);
            this.rectBgItem = new Rectangle(this.rectLogo.X, this.rectLogo.Y, this.bgItem.Width, this.bgItem.Height);
            this.goldPosition = new Vector2(this.rectBgItem.Right - 120, this.rectBgItem.Bottom - 30);
            this.rectGold = new Rectangle((int)(this.goldPosition.X + this.font12.MeasureString(this.goldTotal).X) + 8, (int)this.goldPosition.Y, this.gold.Width, this.gold.Height);

            this.Slots = new Point(5, 6);
            this.inventorySlotsRectangle = new Rectangle[this.Slots.X, this.Slots.Y];
            this.inVentorySlots = new InventorySlots[this.Slots.X, this.Slots.Y];
            this.constant = new Point(rectBgItem.X + 28, rectBgItem.Y + 30);

            for (int x = 0; x < this.Slots.X; x++)
            {
                for (int y = 0; y < this.Slots.Y; y++)
                {
                    this.inventorySlotsRectangle[x, y] = new Rectangle((x * 66) + constant.X,
                         (y * 66) + constant.Y, 66, 66);
                }
            }

            for (int x = 0; x < this.Slots.X; x++)
            {
                for (int y = 0; y < this.Slots.Y; y++)
                {
                    this.inVentorySlots[x, y] = new InventorySlots(this.inventorySlotsRectangle[x, y], new Point(x, y));
                    for (int i = 0; i < Infomations.ListItemOfCharacter.Count; i++)
                    {
                        string[] position = Infomations.ListItemOfCharacter[i].Position.Split('|');
                        if(position[0] == "Inventory" && int.Parse(position[1]) == y && int.Parse(position[2]) == x)
                        {
                            this.inVentorySlots[x, y].ActiveItem(Infomations.ListItemOfCharacter[i].ID, Infomations.ListItemOfCharacter[i].Texture);
                            break;
                        }
                    }
                }
            }

            this.close = TextureManager.GetTexture("closeShortcut");
            this.rectClose = new Rectangle(this.rectBgItem.Right - this.close.Width - 5, this.rectBgItem.Y + 10, this.close.Width, this.close.Height);
        }
        public void HandleInput(GameTime gameTime)
        {
            if (Game1.MouseRect.Intersects(this.rectClose) && Functions.MouseClick() && this.isVisible)
            {
                this.isVisible = false;
            }

            if (Functions.MouseTouch())
            {
                if (Game1.MouseRect.Intersects(this.rectLogo))
                    this.isMove = true;
            }
            else
            {
                this.isMove = false;
            }
            if (this.isMove)
            {
                this.rectLogo = new Rectangle(Mouse.GetState().X - this.logo.Width / 2, Mouse.GetState().Y - this.logo.Height / 2, this.logo.Width, this.logo.Height);
                this.rectBgItem = new Rectangle(this.rectLogo.X, this.rectLogo.Y, this.bgItem.Width, this.bgItem.Height);
                this.constant = new Point(rectBgItem.X + 28, rectBgItem.Y + 30);
                this.goldPosition = new Vector2(this.rectBgItem.Right - 120, this.rectBgItem.Bottom - 30);
                this.rectGold = new Rectangle((int)(this.goldPosition.X + this.font12.MeasureString(this.goldTotal).X) + 8, (int)this.goldPosition.Y, this.gold.Width, this.gold.Height);
                this.rectClose = new Rectangle(this.rectBgItem.Right - this.close.Width - 5, this.rectBgItem.Y + 10, this.close.Width, this.close.Height);

                for (int x = 0; x < this.Slots.X; x++)
                {
                    for (int y = 0; y < this.Slots.Y; y++)
                    {
                        this.inventorySlotsRectangle[x, y] = new Rectangle((x * 66) + constant.X,
                             (y * 66) + constant.Y, 66, 66);
                        this.inVentorySlots[x, y].rect = this.inventorySlotsRectangle[x, y];
                    }
                }
            }

            if (this.rectLogo.X < 0)
            {
                this.rectLogo.X = 0;
            }
            if (this.rectLogo.Y < 0)
            {
                this.rectLogo.Y = 0;
            }
            if (this.rectLogo.X > Game1.CONFIG_WIDTH - this.rectLogo.Width)
            {
                this.rectLogo.X = Game1.CONFIG_WIDTH - this.rectLogo.Width;
            }
            if (this.rectLogo.Y > Game1.CONFIG_HEIGHT - this.rectLogo.Height)
            {
                this.rectLogo.Y = Game1.CONFIG_HEIGHT - this.rectLogo.Height;
            }

            for (int x = 0; x < this.Slots.X; x++)
            {
                for (int y = 0; y < this.Slots.Y; y++)
                {
                    this.inVentorySlots[x, y].HandleInput();

                    if (Functions.MouseClick() && Game1.MouseRect.Intersects(this.inVentorySlots[x, y].rect))
                    {
                        if (this.inVentorySlots[x, y].isActive)
                        {
                            string texture = "";
                            for (int i = 0; i < Infomations.ListItemOfCharacter.Count; i++)
                            {
                                if(Infomations.ListItemOfCharacter[i].ID == this.inVentorySlots[x, y].ItemID)
                                {
                                    texture = Infomations.ListItemOfCharacter[i].Texture;
                                    break;
                                }
                            }
                            InventorySlots.ListItemMove.Add(new ItemMove { ItemID = this.inVentorySlots[x, y].ItemID, Position = this.inVentorySlots[x, y].position, ItemTexture = texture });
                            this.inVentorySlots[x, y].UnActiveItem();
                        }
                        else
                        {
                            if(InventorySlots.ListItemMove.Count == 1)
                            {
                                this.inVentorySlots[x, y].ActiveItem(InventorySlots.ListItemMove.First().ItemID, InventorySlots.ListItemMove.First().ItemTexture, true);
                                InventorySlots.ListItemMove.RemoveAt(0);
                            }
                        }
                        if (InventorySlots.ListItemMove.Count >= 2)
                        {
                            this.inVentorySlots[x, y].ActiveItem(InventorySlots.ListItemMove.First().ItemID, "player", true);
                            InventorySlots.ListItemMove.RemoveAt(0);
                        }
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.bgItem, this.rectBgItem, Color.White);

            for (int x = 0; x < this.Slots.X; x++)
            {
                for (int y = 0; y < this.Slots.Y; y++)
                {
                    this.inVentorySlots[x, y].Draw(spriteBatch);
                }
            }
            for (int i = 0; i < InventorySlots.ListItemMove.Count; i++)
            {
                spriteBatch.Draw(TextureManager.GetTexture(InventorySlots.ListItemMove[i].ItemTexture), new Rectangle(Game1.MouseRect.X - 15, Game1.MouseRect.Y - 15, 31, 31), Color.White);
            }
            spriteBatch.DrawString(this.font12, this.goldTotal, this.goldPosition, Color.White);
            spriteBatch.Draw(this.gold, this.rectGold, Color.White);
            spriteBatch.Draw(this.logo, this.rectLogo, Color.White);
            spriteBatch.Draw(this.close, this.rectClose, Color.White);
        }
    }
}
