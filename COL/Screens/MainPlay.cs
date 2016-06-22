using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using COL.GameObjects.Players;
using COL.GameObjects.Maps;
using COL.GameFramework.Rectangle;
using COL.GameFramework.Textures;
using COL.GameObjects.Camera;
using COL.GameObjects.Npcs;
using Microsoft.Xna.Framework.Input;
using COL.Helpers;
using COL.GameObjects.Shortcuts;
using COL.GameObjects;
using COL.GameFramework.Fonts;
using COL.GameFramework;
using System.Diagnostics;
using COL.GameFramework.Items;

namespace COL.Screens
{
    public class MainPlay : GameScreen
    {
        //private Player player;
        private RectangleMaps rectMaps;
        private DetectCamera camera;
        private Conversation conversation;
        private MiniMap miniMap;
        /************* Start Background ************/
        private Texture2D bgTexture;
        private Rectangle bgRectangle;
        /************* End Background ************/

        /************* Start Rectangle ************/
        private Texture2D drawRect;
        /************* End Rectangle ************/

        //Shortcuts
        private Shortcut shortCut;

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            this.drawRect = TextureManager.GetTexture("drawRect"); //Hình rectangle.

            this.bgTexture = TextureManager.GetTexture("background"); //Hình nền Maps.
            this.bgRectangle = new Rectangle(0, 0, this.bgTexture.Width, this.bgTexture.Height); //Tọa độ hình nền maps
            
            //this.player = new Player(this.ScreenManager.Game); //Bắt đầu khởi tạo Object Player.

            this.rectMaps = new RectangleMaps(this.ScreenManager.Game, "../../../Assets/RectangleData" + Game1.MapName + ".xml"); //Bắt đầu load các Rectangle của Level.

            //Camera
            this.camera = new DetectCamera(this.ScreenManager.GraphicsDevice.Viewport);

            this.conversation = new Conversation(9999f);

            //Shortcut
            this.shortCut = new Shortcut(this.ScreenManager);

            //Minimap
            this.miniMap = new MiniMap(this.ScreenManager.GraphicsDevice.Viewport, this.bgTexture, this.bgRectangle, new Vector2(958, -10), new Vector2(0.1f, 0.15f));
            
        }
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            for (int i = 0; i < Player.ListPlayer.Count; i++)
            {
                if (Player.ListPlayer[i].charID.Equals(Infomations.CharacterConnectID))
                {
                    Player.ListPlayer[i].HandleInput(gameTime);
                    break;
                }
            }

            foreach(Npc npc in Npc.ListNpc)
            {
                npc.HandleInput(gameTime);
            }

            if(Shortcut.IsVisibile == false)
            {
                foreach (Npc npc in Npc.ListNpc)
                {
                    for (int i = 0; i < Player.ListPlayer.Count; i++)
                    {
                        if (Player.ListPlayer[i].charID.Equals(Infomations.CharacterConnectID))
                        {
                            if (Functions.KeyboardPressed(Keys.RightControl) && npc.Rectangle.Intersects(new Rectangle((int)Player.ListPlayer[i].Position.X, (int)Player.ListPlayer[i].Position.Y, Player.ListPlayer[i].Rectangle.Width, Player.ListPlayer[i].Rectangle.Height)) && this.conversation.IsVisible == false)
                            {
                                this.conversation.Content = npc.Str;
                                this.conversation.IsVisible = true;
                            }
                            break;
                        }
                    }
                }
            }

            this.conversation.HandleInput(gameTime);

            this.shortCut.HandleInput(gameTime);

        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            //if (Game1.CONNECT_STATUS != "Connected")
            //{
            //    this.ExitScreen();
            //}

            foreach (Npc npc in Npc.ListNpc)
            {
                npc.Update(gameTime);
            }

            for (int i = 0; i < Player.ListPlayer.Count; i++)
            {
                Player.ListPlayer[i].Update(gameTime);

                if (Player.ListPlayer[i].SendRectBg == false)
                {
                    Player.ListPlayer[i].BGRect = this.bgRectangle;
                    Player.ListPlayer[i].SendRectBg = true;
                }
            }

            foreach (RectangleData rectMapData in this.rectMaps.GetRectData)
            {
                for (int i = 0; i < Player.ListPlayer.Count; i++)
                {
                    Player.ListPlayer[i].CollisionMaps(rectMapData);
                }
            }

            for (int i = 0; i < Player.ListPlayer.Count; i++)
            {
                if (Player.ListPlayer[i].charID.Equals(Infomations.CharacterConnectID))
                {
                    //Update Camera
                    this.camera.Update(Player.ListPlayer[i].Position, this.bgTexture.Width, this.bgTexture.Height);
                    this.miniMap.playerPosition = Player.ListPlayer[i].Position;
                    break;
                }
            }

            this.conversation.Update(gameTime);

            this.shortCut.Update(gameTime);

            this.miniMap.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            //Draw theo camera
            this.ScreenManager.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, this.camera.Transform);

            this.ScreenManager.SpriteBatch.Draw(this.bgTexture, this.bgRectangle, Color.White); //Vẽ maps.

            //Npc Draw
            foreach (Npc npc in Npc.ListNpc)
            {
                npc.Draw(this.ScreenManager.SpriteBatch);
            }

            for (int i = 0; i < Player.ListPlayer.Count; i++)
            {
                if(Player.ListPlayer[i].MapName == Game1.MapName)
                {
                    Player.ListPlayer[i].Draw(this.ScreenManager.SpriteBatch);
                }
            }

            foreach (RectangleData rectDataMaps in this.rectMaps.GetRectData) //Vẽ các Rectangle, dùng cho dễ kiểm soát vị trí Rectangle trong file XML.
            {
                Rectangle newRect = new Rectangle(rectDataMaps.x, rectDataMaps.y, rectDataMaps.width, rectDataMaps.height);
                this.ScreenManager.SpriteBatch.Draw(this.drawRect, newRect, Color.White);
            }
            this.ScreenManager.SpriteBatch.End();

            //Draw Minimap
            this.miniMap.Draw(this.ScreenManager.SpriteBatch);

            //Draw không theo camera.
            this.ScreenManager.SpriteBatch.Begin();
            this.shortCut.Draw(this.ScreenManager.SpriteBatch);
            this.conversation.Draw(this.ScreenManager.SpriteBatch);
            this.ScreenManager.SpriteBatch.DrawString(FontManager.GetFont("Font12"), Game1.MouseRect.ToString(), new Vector2(0, 0), Color.White);
            this.ScreenManager.SpriteBatch.End();

        }
    }
}