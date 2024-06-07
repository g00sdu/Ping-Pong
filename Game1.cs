using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PingPong
{
    public enum GameState
    {
        MainMenu,
        Playing,
        Exit,
        HowToPlay
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private GameState _currentState;
        private SpriteFont _font;
        private Paddle _paddle;
        private Paddle _paddle2;
        private Ball _ball;
        private Button _startButton;
        private Button _exitButton;
        private Button _howToPlayButton;
        private Button _backButton;
        private Texture2D _pingPongTextTexture;
        private Texture2D _howToPlayTexture;
        private Song _backgroundMusic;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Globals.width;
            _graphics.PreferredBackBufferHeight = Globals.height;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _paddle = new Paddle(false, Color.RoyalBlue);
            _paddle2 = new Paddle(true, Color.OrangeRed);
            _ball = new Ball();
            _currentState = GameState.MainMenu;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.pixel = new Texture2D(GraphicsDevice, 1, 1);
            Globals.pixel.SetData(new[] { Color.White });

            _font = Content.Load<SpriteFont>("Score");
            _startButton = new Button(Content.Load<Texture2D>("StartButton"), new Vector2(Globals.width / 2, Globals.height / 2f));
            _howToPlayButton = new Button(Content.Load<Texture2D>("HowToPlayButton"), new Vector2(Globals.width / 4.2f, Globals.height / 1.25f));
            _exitButton = new Button(Content.Load<Texture2D>("ExitButton"), new Vector2(Globals.width / 2, Globals.height / 1.25f));
            _pingPongTextTexture = Content.Load<Texture2D>("PingPongText");
            _howToPlayTexture = Content.Load<Texture2D>("HowToPlayMenu");
            _backButton = new Button(Content.Load<Texture2D>("BackButton"), new Vector2(Globals.width / 1.25f, Globals.height / 1.25f));
            _backgroundMusic = Content.Load<Song>("BackgroundMusic");
            PlayBackgroundMusic();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (_currentState)
            {
                case GameState.MainMenu:
                    UpdateMainMenu(gameTime);
                    break;
                case GameState.Playing:
                    UpdatePlaying(gameTime);
                    break;
                case GameState.Exit:
                    Exit();
                    break;
                case GameState.HowToPlay:
                    UpdateHowToPlay(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        private void UpdateMainMenu(GameTime gameTime)
        {
            if (_startButton.IsClicked())
            {
                _currentState = GameState.Playing;
            }
            else if (_howToPlayButton.IsClicked())
            {
                _currentState = GameState.HowToPlay;
            }
            else if (_exitButton.IsClicked())
            {
                _currentState = GameState.Exit;
            }
        }

        private void UpdateHowToPlay(GameTime gameTime)
        {
            if (_backButton.IsClicked())
            {
                _currentState = GameState.MainMenu;
            }
        }

        private void UpdatePlaying(GameTime gameTime)
        {
            _paddle.Update(gameTime);
            _paddle2.Update(gameTime);
            _ball.Update(gameTime, _paddle, _paddle2);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Globals.spriteBatch.Begin();
            switch (_currentState)
            {
                case GameState.MainMenu:
                    DrawMainMenu();
                    break;
                case GameState.Playing:
                    DrawPlaying();
                    break;
                case GameState.HowToPlay:
                    DrawHowToPlay();
                    break;
            }
            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawMainMenu()
        {
            Vector2 texturePosition = new Vector2((Globals.width - _pingPongTextTexture.Width) / 2, (Globals.height / 6) - (_pingPongTextTexture.Height / 2));
            Globals.spriteBatch.Draw(_pingPongTextTexture, texturePosition, Color.White);
            _startButton.Draw(Globals.spriteBatch);
            _howToPlayButton.Draw(Globals.spriteBatch);
            _exitButton.Draw(Globals.spriteBatch);
        }

        private void DrawHowToPlay()
        {
            Globals.spriteBatch.Draw(_howToPlayTexture, new Rectangle(0, 0, Globals.width, Globals.height), Color.White);
            _backButton.Draw(Globals.spriteBatch);
        }

        private void DrawPlaying()
        {
            float scoreScale = 3f;

            Globals.spriteBatch.DrawString(_font, Globals.scorePlayer1.ToString(), new Vector2(100, 50),
                Color.White, 0f, Vector2.Zero, scoreScale, SpriteEffects.None, 0f);
            Globals.spriteBatch.DrawString(_font, Globals.scorePlayer2.ToString(), new Vector2(Globals.width - 112, 50),
                Color.White, 0f, Vector2.Zero, scoreScale, SpriteEffects.None, 0f);

            _paddle.Draw();
            _paddle2.Draw();
            _ball.Draw();
        }

        private void PlayBackgroundMusic()
        {
            MediaPlayer.Play(_backgroundMusic);
            MediaPlayer.Volume = 0.025f;
            MediaPlayer.IsRepeating = true;
        }
    }
}