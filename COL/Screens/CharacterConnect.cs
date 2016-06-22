using GameStateManagement;
using Microsoft.Xna.Framework;
using COL.GameObjects;
using COL.GameFramework.Fonts;
using COL.GameFramework;
using Lidgren.Network;
using System.Diagnostics;

namespace COL.Screens
{
    public class CharacterConnect : GameScreen
    {
        public static string CharConnectStatus;
        public static string GetInfoStatus;

        private PopupError error;

        private float timer;
        private float timeGetInfo;

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            this.error = new PopupError(345, 150, "Đang kết nối đến máy chủ.", FontManager.GetFont("Font12"), Color.White);
            GetInfoStatus = "npc";
        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            this.timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if(this.timer >= 300)
            {
                this.error.IsVisible = true;
                this.error.showOk = false;
                if (CharConnectStatus == "OK")
                {
                    switch (GetInfoStatus)
                    {
                        case "npc":
                            {
                                this.error.textErr = "Đang lấy thông tin NPCs...";

                                Network.outmsg = Network.Client.CreateMessage();
                                Network.outmsg.Write("GetAllInfo");
                                Network.outmsg.Write("npc");
                                Network.Client.SendMessage(Network.outmsg, NetDeliveryMethod.ReliableOrdered);

                                GetInfoStatus = "";
                            }
                            break;
                        case "item":
                            {
                                this.error.textErr = "Đang lấy thông tin Inventory...";

                                Network.outmsg = Network.Client.CreateMessage();
                                Network.outmsg.Write("GetAllInfo");
                                Network.outmsg.Write("item");
                                Network.outmsg.Write(Infomations.CharacterConnectID);
                                Network.Client.SendMessage(Network.outmsg, NetDeliveryMethod.ReliableOrdered);

                                GetInfoStatus = "";
                            }
                            break;
                        case "allItem":
                            {
                                this.error.textErr = "Đang lấy thông tin Items...";

                                Network.outmsg = Network.Client.CreateMessage();
                                Network.outmsg.Write("GetAllInfo");
                                Network.outmsg.Write("allItem");
                                Network.Client.SendMessage(Network.outmsg, NetDeliveryMethod.ReliableOrdered);

                                GetInfoStatus = "";
                            }
                            break;
                        case "quest":
                            {
                                this.error.textErr = "Đang lấy thông tin nhiệm vụ...";

                                Network.outmsg = Network.Client.CreateMessage();
                                Network.outmsg.Write("GetAllInfo");
                                Network.outmsg.Write("quest");
                                Network.Client.SendMessage(Network.outmsg, NetDeliveryMethod.ReliableOrdered);

                                GetInfoStatus = "";
                            }
                            break;
                        case "positionCharacters":
                            {
                                this.timeGetInfo += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                                if (this.timeGetInfo >= 1000)
                                {
                                    this.error.textErr = "Đang lấy thông tin nhân vật...";

                                    Network.outmsg = Network.Client.CreateMessage();
                                    Network.outmsg.Write("GetAllPositionCharacters");
                                    Network.Client.SendMessage(Network.outmsg, NetDeliveryMethod.ReliableOrdered);

                                    GetInfoStatus = "";
                                }
                            }
                            break;
                        case "characters":
                            {
                                this.timeGetInfo += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                                if(timeGetInfo >= 1000)
                                {
                                    this.timeGetInfo = 0;
                                    this.error.textErr = "Đang lấy thông tin nhân vật...";

                                    Network.outmsg = Network.Client.CreateMessage();
                                    Network.outmsg.Write("GetAllCharacters");
                                    Network.outmsg.Write(Infomations.CharacterConnectID);
                                    Network.Client.SendMessage(Network.outmsg, NetDeliveryMethod.ReliableOrdered);

                                    GetInfoStatus = "";
                                }
                            }
                            break;
                        case "success":
                            {
                                this.error.textErr = "Đang đăng nhập vào trò chơi...";
                                this.timeGetInfo += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                                if(this.timeGetInfo >= 1000)
                                {
                                    this.timeGetInfo = 0;
                                    this.ScreenManager.AddScreen(new MainPlay(), null);
                                    this.ExitScreen();
                                    GetInfoStatus = "";
                                }
                            }
                            break;
                    }
                }
                else if(CharConnectStatus == "CharacterOnline")
                {
                    this.error.textErr = "Nhân vật đang online.";
                }
                else if(CharConnectStatus == "CharacterNotExist")
                {
                    this.error.textErr = "Nhân vật không tồn tại.";
                }
            }
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            this.ScreenManager.SpriteBatch.Begin();
            this.error.Draw(this.ScreenManager.SpriteBatch);
            this.ScreenManager.SpriteBatch.End();
        }
    }
}
