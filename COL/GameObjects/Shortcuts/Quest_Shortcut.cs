using COL.GameFramework;
using COL.GameFramework.Fonts;
using COL.GameFramework.Quests;
using COL.GameFramework.Textures;
using COL.Helpers;
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
    public class Quest_Shortcut
    {
        public bool IsVisible;
        private bool _isMove;
        private bool _showList;

        private SpriteFont Font16;
        private SpriteFont Font14;
        private SpriteFont Font12;

        private Texture2D logo;
        private Texture2D background;
        private Texture2D bgList;
        private Texture2D scrollBar;
        private Texture2D btnViewList;
        private Texture2D btnclose;
        private Texture2D bgItem;

        private Rectangle rectLogo;
        private Rectangle rectBackground;
        private Rectangle rectBgList;
        private Rectangle rectScrollBar;
        private Rectangle rectBtnViewList;
        private Rectangle rectBtnClose;
        private Rectangle rectBgItem;

        private List<ListQuests> _listQuest = new List<ListQuests>();

        public static string selectQuestName;
        public static string questTitle;
        public static string questDescription;

        public Quest_Shortcut()
        {
            this.Font16 = FontManager.GetFont("Font16");
            this.Font12 = FontManager.GetFont("Font12");

            string[] listQuest = Infomations.ListCharacters.Find(item => item.ID == Infomations.CharacterConnectID).QuestsUnComplete.Split('|');
            
            this.logo = TextureManager.GetTexture("logoQuest");
            this.background = TextureManager.GetTexture("bgQuest");
            this.bgList = TextureManager.GetTexture("bgListQuest");
            this.scrollBar = TextureManager.GetTexture("scrollBarQuest");
            this.btnViewList = TextureManager.GetTexture("btnViewListQuest");
            this.btnclose = TextureManager.GetTexture("closeShortcut");
            this.bgItem = TextureManager.GetTexture("bgItemQuest");

            this.rectLogo = new Rectangle(Game1.CONFIG_WIDTH / 2 - this.background.Width / 2, Game1.CONFIG_HEIGHT / 2 - this.background.Height / 2, this.logo.Width, this.logo.Height);
            this.rectBackground = new Rectangle(this.rectLogo.X, this.rectLogo.Y, this.background.Width, this.background.Height);
            this.rectBtnClose = new Rectangle(this.rectBackground.Right - 25, this.rectBackground.Y + 12, this.btnclose.Width, this.btnclose.Height);
            this.rectBtnViewList = new Rectangle(this.rectBackground.X + 25, this.rectBackground.Bottom - 3, this.btnViewList.Width, this.btnViewList.Height);
            this.rectBgList = new Rectangle(this.rectBackground.X - this.bgList.Width + 30, this.rectBackground.Y + 60, this.bgList.Width, this.bgList.Height);

            for (int i = 0; i < listQuest.Count(); i++)
            {
                QuestData data = Infomations.QuestsData.Quests.Find(q => q.Name == listQuest[i]);
                this._listQuest.Add(new ListQuests(data.Name, data.Title, data.Content, data.Description));

                if (i == 0)
                {
                    selectQuestName = listQuest[i];
                    questTitle = Functions.WrapText(this.Font16, data.Title, 300);
                    questDescription = Functions.WrapText(this.Font12, data.Description, 300);
                    this._listQuest[i].RectBackground = new Rectangle(this.rectBgList.X + 5, this.rectBgList.Y + 5, 160, 80);
                    this._listQuest[i].Active = true;
                }
                else
                    this._listQuest[i].RectBackground = new Rectangle(this.rectBgList.X + 5, i * 80 + this.rectBgList.Y, 160, 80);
            }

            this._showList = true;
        }

        public void Input()
        {
            for (int i = 0; i < this._listQuest.Count; i++)
            {
                if(this._listQuest[i].Active && this._listQuest[i].Name != selectQuestName)
                {
                    this._listQuest[i].Active = false;
                }
                this._listQuest[i].Input();
            }

            if (Functions.MouseClick() && this.IsVisible)
            {
                if (Game1.MouseRect.Intersects(this.rectBtnClose))
                    this.IsVisible = false;

                if(Game1.MouseRect.Intersects(this.rectBtnViewList))
                {
                    if (this._showList)
                        this._showList = false;
                    else
                        this._showList = true;
                }
            }

            if (Functions.MouseTouch())
            {
                if (Game1.MouseRect.Intersects(this.rectLogo))
                    this._isMove = true;
            }
            else
                this._isMove = false;

            if (this._isMove)
            {
                this.rectLogo = new Rectangle(Mouse.GetState().X - this.logo.Width / 2, Mouse.GetState().Y - this.logo.Height / 2, this.logo.Width, this.logo.Height);

                this.rectBackground = new Rectangle(this.rectLogo.X, this.rectLogo.Y, this.background.Width, this.background.Height);
                this.rectBtnClose = new Rectangle(this.rectBackground.Right - 25, this.rectBackground.Y + 12, this.btnclose.Width, this.btnclose.Height);
                this.rectBtnViewList = new Rectangle(this.rectBackground.X + 25, this.rectBackground.Bottom - 3, this.btnViewList.Width, this.btnViewList.Height);
                this.rectBgList = new Rectangle(this.rectBackground.X - this.bgList.Width + 30, this.rectBackground.Y + 60, this.bgList.Width, this.bgList.Height);
                
                for (int i = 0; i < this._listQuest.Count; i++)
                {
                    this._listQuest[i].UpdatePosition(new Point(this.rectBgList.X + 3, i * 80 + this.rectBgList.Y));
                }
            }

            if (this.rectLogo.X < 0)
                this.rectLogo.X = 0;
            if (this.rectLogo.Y < 0)
                this.rectLogo.Y = 0;
            if (this.rectLogo.X > Game1.CONFIG_WIDTH - this.rectLogo.Width)
                this.rectLogo.X = Game1.CONFIG_WIDTH - this.rectLogo.Width;
            if (this.rectLogo.Y > Game1.CONFIG_HEIGHT - this.rectLogo.Height)
                this.rectLogo.Y = Game1.CONFIG_HEIGHT - this.rectLogo.Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.background, this.rectBackground, Color.White);
            spriteBatch.Draw(this.btnViewList, this.rectBtnViewList, Color.White);

            if (this._showList)
            {
                spriteBatch.Draw(this.bgList, this.rectBgList, Color.White);
                for (int i = 0; i < this._listQuest.Count; i++)
                {
                    this._listQuest[i].Draw(spriteBatch);
                }
            }

            spriteBatch.DrawString(this.Font16, questTitle.ToUpper(), new Vector2(this.rectBackground.X + 50, this.rectBackground.Y + 50), Color.Black);
            spriteBatch.DrawString(this.Font12, questDescription, new Vector2(this.rectBackground.X + 50, this.rectBackground.Y + 70 + this.Font16.MeasureString(questTitle).Y), Color.Black);
            spriteBatch.DrawString(this.Font16, "PHẦN THƯỞNG", new Vector2(this.rectBackground.X + 50, this.rectBackground.Y + 90 + this.Font16.MeasureString(questTitle).Y + this.Font12.MeasureString(questDescription).Y), Color.Black);
            spriteBatch.Draw(this.btnclose, this.rectBtnClose, Color.White);
            spriteBatch.Draw(this.logo, this.rectLogo, Color.White);
        }
    }

    public class ListQuests
    {
        public bool Active;

        public string Name;
        public string Title;
        public string Content;
        public string Description;

        private Texture2D bgActive;

        private SpriteFont Font16;
        private SpriteFont Font14;
        private SpriteFont Font12;

        public Rectangle RectBackground;

        public ListQuests(string name, string title, string content, string des)
        {
            this.Font14 = FontManager.GetFont("Font14");
            this.Font12 = FontManager.GetFont("Font12");
            this.Font16 = FontManager.GetFont("Font16");

            this.bgActive = TextureManager.GetTexture("bgActiveQuestList");

            this.Content = Functions.WrapText(this.Font12, content, 155f);
            this.Name = name;
            this.Title = title;
            this.Description = des;
        }

        public void Input()
        {
            if (Functions.MouseClick() && Game1.MouseRect.Intersects(this.RectBackground))
            {
                this.Active = true;
                Quest_Shortcut.selectQuestName = this.Name;
                Quest_Shortcut.questTitle = Functions.WrapText(this.Font16, this.Title, 300);
                Quest_Shortcut.questDescription = Functions.WrapText(this.Font12, this.Description, 300);
            }
        }

        public void UpdatePosition(Point position)
        {
            this.RectBackground.X = position.X;
            this.RectBackground.Y = position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.Active)
                spriteBatch.Draw(this.bgActive, this.RectBackground, Color.White);

            spriteBatch.DrawString(this.Font14, this.Title, new Vector2(this.RectBackground.X + 3, this.RectBackground.Y + 10), Color.Yellow);
            spriteBatch.DrawString(this.Font12, this.Content, new Vector2(this.RectBackground.X + 3, this.RectBackground.Y + 13 + this.Font14.MeasureString(this.Title).Y), Color.White);
        }
    }
}
