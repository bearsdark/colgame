using COL.GameFramework;
using COL.Screens;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using COL.Helpers;
using COL.GameObjects.Players;
using System.Diagnostics;
using System.Collections.Generic;
using Lidgren.Network;
using Microsoft.Xna.Framework.Input;

namespace COL
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        private static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private ScreenManager _screenManager;

        public const float CONFIG_SPEED_PLAYER = 3f; //Set tốc độ chạy của Player.
        public const float CONFIG_JUMP_PLAYER = 9f; //Set lực nhảy của Player.
        public const float CONFIG_SPEEDJUMP_PLAYER = 5f; //Set tốc độ nhảy của Player.
        public const int CONFIG_WIDTH = 1200;
        public const int CONFIG_HEIGHT = 560;
        public static string CONNECT_STATUS;
        public static string playerName;
        public static int PlayerID = 7;
        public static string PlayerName = "BearsDark2";
        public static Rectangle MouseRect;
        public static MouseState lastMouse;
        public static MouseState currentMouse;
        public static KeyboardState lastKey;
        public static KeyboardState currentKey;
        public static int Volume = 50;
        public static bool AccountLogin;
        public static string MapName = "Stage1";

        public Game1()
        {
            playerName = Functions.GetRandomString();

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = CONFIG_WIDTH;
            graphics.PreferredBackBufferHeight = CONFIG_HEIGHT;

            this._screenManager = new ScreenManager(this);
            this._screenManager.AddScreen(new LoadingScreen(), null);
            this.Components.Add(this._screenManager);
        }

        public static bool FullScreen
        {
            get
            {
                return graphics.IsFullScreen;
            }
            set
            {
                if (!value && graphics.IsFullScreen)
                    graphics.ToggleFullScreen();
                else if (value && !graphics.IsFullScreen)
                    graphics.ToggleFullScreen();
            }
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            MouseRect = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
            lastMouse = currentMouse;
            currentMouse = Mouse.GetState();
            lastKey = currentKey;
            currentKey = Keyboard.GetState();

            if (CONNECT_STATUS == "Connected")
            {
                Network.Update(gameTime);

                if (Network.Client.ConnectionStatus == NetConnectionStatus.Disconnected)
                {
                    CONNECT_STATUS = "Disconnected";
                }
            }

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
