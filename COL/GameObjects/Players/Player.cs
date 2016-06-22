using COL.GameFramework;
using COL.GameFramework.Fonts;
using COL.GameFramework.Rectangle;
using COL.GameObjects.Npcs;
using COL.Helpers;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace COL.GameObjects.Players
{
    public class Player
    {
        private Animation animationPlayer;

        private Texture2D texture;
        private Vector2 position;
        private Vector2 velocity;
        private Rectangle rectangle;

        public PlayerActions actionStatus;
        private KeyboardState pasKey;
        private Rectangle bgRect;

        public int charID;

        public string Name;
        public string MapName;

        private SpriteFont font;
        private bool isJumped = false;
        public bool SendRectBg;
        private bool _sendPressKeyLeft;
        private bool _sendPressKeyRight;
        private bool _sendPressKeySpace;
        private bool _sendPosition;

        public static List<Player> ListPlayer = new List<Player>();

        public Player(int charID, string name, Vector2 pos, string MapName)
        {
            this.animationPlayer = new Animation("playernormalright", 250f, 5, pos, true);
            this.Name = name;
            this.MapName = MapName;
            this.SendRectBg = false;
            this.charID = charID;
            this.font = FontManager.GetFont("Font12");
        }
        public void HandleInput(GameTime gameTime)
        {
            if (!Conversation.Visible)
            {
                var key = Keyboard.GetState();

                if (key.IsKeyDown(Keys.Left))
                {
                    if (!this._sendPressKeyLeft)
                    {
                        this._sendPosition = false;
                        this._sendPressKeyRight = false;
                        this._sendPressKeyLeft = true;
                        this.SendActions(PlayerActions.MoveLeft);
                    }
                }
                else if (key.IsKeyDown(Keys.Right))
                {
                    if (!this._sendPressKeyRight)
                    {
                        this._sendPosition = false;
                        this._sendPressKeyLeft = false;
                        this._sendPressKeyRight = true;
                        this.SendActions(PlayerActions.MoveRight);
                    }
                }
                else
                {
                    if (this._sendPressKeyLeft)
                    {
                        this._sendPressKeyLeft = false;
                        this.velocity.X = 0;
                        this.SendActions(PlayerActions.StopMoveLeft);
                    }
                    if (this._sendPressKeyRight)
                    {
                        this._sendPressKeyRight = false;
                        this.velocity.X = 0;
                        this.SendActions(PlayerActions.StopMoveRight);
                    }
                    if (!this._sendPosition)
                    {
                        this._sendPosition = true;
                        this.SendUpdatePosition();
                    }
                }

                if (key.IsKeyDown(Keys.Space))
                {
                    if (!this._sendPressKeySpace)
                    {
                        this._sendPressKeySpace = true;
                        this.SendActions(PlayerActions.Jump);
                    }
                }
                else if (this._sendPressKeySpace)
                {
                    this._sendPressKeySpace = false;
                    this.SendActions(PlayerActions.StopJump);
                }
            }
        }
        public void SendUpdatePosition()
        {
            Network.outmsg = Network.Client.CreateMessage();
            Network.outmsg.Write("UpdatePositionCharacter");
            Network.outmsg.Write(this.Name);
            Network.outmsg.Write((int)this.animationPlayer.position.X);
            Network.outmsg.Write((int)this.animationPlayer.position.Y);
            Network.Client.SendMessage(Network.outmsg, NetDeliveryMethod.Unreliable);
        }
        private void SendActions(PlayerActions action)
        {
            Network.outmsg = Network.Client.CreateMessage();
            Network.outmsg.Write("CharacterMove");
            Network.outmsg.Write(this.Name);
            Network.outmsg.Write((byte)action);
            Network.Client.SendMessage(Network.outmsg, NetDeliveryMethod.Unreliable);
        }
        public void Update(GameTime gameTime)
        {
            this.pasKey = Keyboard.GetState();

            if(this.actionStatus == PlayerActions.MoveLeft)
            {
                this.animationPlayer.SetAgainAnimation("playermoveleft", 200f, 4, true);
                this.velocity.X = -Game1.CONFIG_SPEED_PLAYER;
            }
            if(this.actionStatus == PlayerActions.MoveRight)
            {
                this.animationPlayer.SetAgainAnimation("playermoveright", 200f, 4, true);
                this.velocity.X = Game1.CONFIG_SPEED_PLAYER;
            }
            if(this.actionStatus == PlayerActions.StopMoveLeft || this.actionStatus == PlayerActions.StopMoveRight)
            {
                this.animationPlayer.SetAgainAnimation("playernormalright", 250f, 5, true);
                this.velocity.X = 0;
            }
            if(this.actionStatus == PlayerActions.Jump && this.isJumped == false)
            {
                this.isJumped = true;

                this.velocity.Y = -Game1.CONFIG_JUMP_PLAYER;
                this.animationPlayer.position.Y -= Game1.CONFIG_SPEEDJUMP_PLAYER;
            }
            this.animationPlayer.position += this.velocity;
            this.animationPlayer.PlayAnimation(gameTime);

            if (this.velocity.Y < 10)
            {
                this.isJumped = true;
                this.velocity.Y += 0.4f;
            }
        }
        public void CollisionMaps(RectangleData rectData)
        {
            Rectangle newRectMap = new Rectangle(rectData.x, rectData.y, rectData.width, rectData.height); //Khởi tạo Rectangle của Map (Lấy trong file XML).
            Rectangle newRectPlayer = new Rectangle((int)this.animationPlayer.position.X, (int)this.animationPlayer.position.Y, this.animationPlayer.rectangle.Width, this.animationPlayer.rectangle.Height); //Khởi tạo new Rectangle của Player.

            if (newRectPlayer.DetectTopOf(newRectMap)) //Kiểm tra va chạm bên trên.
            {
                this.animationPlayer.position.Y = newRectMap.Y - newRectPlayer.Height; //Nếu va chạm bên trên thì Player sẽ đứng yên tại vị trí này.
                this.velocity.Y = 0; //Sẽ không di chuyển.
                this.isJumped = false; //Cho phép Player nhảy.
            }
            else if (newRectPlayer.DetectLeftOf(newRectMap)) //Kiểm tra va chạm bên trái.
            {
                this.animationPlayer.position.X = newRectMap.X - newRectPlayer.Width - 1; //Player sẽ bị chặn lại tại 1 tọa độ này.
                this.velocity.X = 0f; //Ngưng di chuyển.
            }
            else if (newRectPlayer.DetectRightOf(newRectMap)) //Kiểm tra va chạm bên phải.
            {
                this.animationPlayer.position.X = newRectMap.Right + 1; //Player sẽ bị chặn lại tại 1 tọa độ này.
                this.velocity.X = 0f; //Ngưng di chuyển.
            }
            else if (newRectPlayer.DetectBottomOf(newRectMap)) //Kiểm tra va chạm bên dưới.
            {
                this.velocity.Y = 1f; //Player sẽ rơi xuống với tốc độ 1f.
                this.isJumped = true; //Sẽ không thể nhảy cho đến khi va chạm bên trên của Rectangle khác.
            }

            if (this.animationPlayer.position.X <= 0)
            {
                this.animationPlayer.position.X = 1;
                this.velocity.X = 0;
            }
            else if (this.animationPlayer.position.X + this.rectangle.Width >= this.bgRect.Width)
            {
                this.animationPlayer.position.X = this.bgRect.Right - this.rectangle.Width;
                this.velocity.X = 0;
            }
            if (this.animationPlayer.position.Y <= 0)
            {
                this.animationPlayer.position.Y = 1;
            }
            else if (this.animationPlayer.position.Y + this.rectangle.Height >= this.bgRect.Height)
            {
                this.animationPlayer.position.Y = this.bgRect.Height - this.rectangle.Height;
                this.isJumped = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            this.animationPlayer.Draw(spriteBatch);
            spriteBatch.DrawString(this.font, this.Name, new Vector2(this.animationPlayer.position.X - 10, this.animationPlayer.position.Y - 10), Color.Red);
        }

        public Vector2 Position
        {
            set { this.animationPlayer.position = value; }
            get { return this.animationPlayer.position; }
        }
        public Rectangle Rectangle
        {
            get { return this.animationPlayer.rectangle; }
            set { this.animationPlayer.rectangle = value; }
        }
        public Rectangle BGRect
        {
            set { this.bgRect = value; }
        }
    }
}
