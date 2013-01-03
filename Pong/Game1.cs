using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        //private Texture2D playerPaddle;

        private GameObjects gameObjects;
        private Paddle playerPaddle;
        private Paddle computerPaddle;
        private Ball ball;
        private Score score;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Assets";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            TouchPanel.EnabledGestures = GestureType.VerticalDrag | GestureType.Flick | GestureType.Tap;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var gameBoundaries = new Rectangle(0, 9, Window.ClientBounds.Width, Window.ClientBounds.Height);

            var paddleTexture = Content.Load<Texture2D>("Paddle");
            
            playerPaddle = new Paddle(paddleTexture,Vector2.Zero,gameBoundaries,Paddle.PlayerTypes.Human);

            var computerPaddleLocation = new Vector2(gameBoundaries.Width - paddleTexture.Width - 0);

            computerPaddle = new Paddle(paddleTexture, computerPaddleLocation, gameBoundaries,Paddle.PlayerTypes.Computer);
            ball = new Ball(Content.Load<Texture2D>("Ball"),Vector2.Zero, gameBoundaries);
            ball.AttachTo(playerPaddle);
            // TODO: use this.Content to load your game content here

            score = new Score(Content.Load<SpriteFont>("GameFont"), gameBoundaries); 

            gameObjects = new GameObjects {PlayerPaddle = playerPaddle, ComputerPaddle = computerPaddle, Ball = ball, Score = score};
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // TODO: Add your update logic here

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameObjects.TouchInput = new TouchInput();
            GetTouchInput();

            playerPaddle.Update(gameTime, gameObjects);
            computerPaddle.Update(gameTime, gameObjects);
            ball.Update(gameTime, gameObjects);
            score.Update(gameTime, gameObjects);

            base.Update(gameTime);
        }

        private void GetTouchInput()
        {
            while (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                if (gesture.Delta.Y > 0)
                    gameObjects.TouchInput.Down = true;
                if (gesture.Delta.Y < 0)
                    gameObjects.TouchInput.Up = true;
                if (gesture.GestureType == GestureType.Tap)
                    gameObjects.TouchInput.Tapped = true;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            playerPaddle.Draw(_spriteBatch);
            ball.Draw(_spriteBatch);
            computerPaddle.Draw(_spriteBatch);
            score.Draw(_spriteBatch);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
