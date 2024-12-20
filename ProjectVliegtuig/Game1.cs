using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using ProjectVliegtuig.Display;
using ProjectVliegtuig.Gameobjects;
using ProjectVliegtuig.LevelCreators;
using ProjectVliegtuig.Levels;
using ProjectVliegtuig.Managers;
using System;

namespace ProjectVliegtuig
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D background;
        private Texture2D startscreen;
        private Texture2D gameOverScreen;

        public static Gameobjects.Plane plane;
        LevelCreatorFactory levelCreatorFactory;
        public static int currentLevel = 1;
        Level level;

        StartScreen startScreen;

        private bool isPlaying = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            DisplayManager.init(_graphics);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {   
            base.Initialize();
        }

        protected override void LoadContent()
        {
            DisplayManager.getDisplay().Apply();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameObject.graphicsDevice = GraphicsDevice;

            Gameobjects.Plane.texture = Content.Load<Texture2D>("plane");
            Gameobjects.Plane.healthBar = Content.Load<Texture2D>("hearts");
            Enemy.texture = Content.Load<Texture2D>("enemy");
            ShootingEnemy.texture = Content.Load<Texture2D>("shootingenemy");
            BossEnemy.texture = Content.Load<Texture2D>("bossenemy");
            background = Content.Load<Texture2D>("background");
            startscreen = Content.Load<Texture2D>("start");
            gameOverScreen = Content.Load<Texture2D>("gameover");
            Level.winscreen = Content.Load<Texture2D>("winscreen");
            Bullet.texture = Content.Load<Texture2D>("bullet");
            Button.texture = Content.Load<Texture2D>("button");
            Button.font = Content.Load<SpriteFont>("font");
            Level.Font = Content.Load<SpriteFont>("font");
            LoadGameObjects();
        }
        private void LoadGameObjects()
        {
            plane = new Gameobjects.Plane();
            levelCreatorFactory = new LevelCreatorFactory();
            startScreen = new StartScreen();
            startScreen.exitButton.Click += ExitButton_Click;
            startScreen.currentLevelButton.Click += CurrentLevelButton_Click;
            startScreen.bossLevelButton.Click += BossLevelButton_Click;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit(); 
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Enter) && !isPlaying)
            {
                StartLevel();
            }
            if(Keyboard.GetState().IsKeyDown(Keys.P)&&isPlaying)
            {
                isPlaying = false;
            }
            if (plane.health<=0)
            {
                isPlaying = false;
                currentLevel = 1;
            }
            if(isPlaying)
            {
                plane.Update(gameTime);
                BulletManager.Update(gameTime);
                level.Update(gameTime);
            }
            else
            {
                startScreen.Update();
            }
            if (level?.LevelOver==true) 
            {
                if(isPlaying)
                {
                    isPlaying = false;
                    currentLevel++;
                    if (levelCreatorFactory.GetLevelCreator(currentLevel) == null) currentLevel = 1;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSkyBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0,0,DisplayManager.getDisplay().width, DisplayManager.getDisplay().height), Color.White);
            if(isPlaying)
            {
                plane.Draw(_spriteBatch);
                BulletManager.Draw(_spriteBatch);
                level.Draw(_spriteBatch);
            }
            else
            {
                if (plane.health <= 0)
                {
                    _spriteBatch.Draw(gameOverScreen, DisplayManager.getDisplay().fullScreenRectangle, Color.White);
                }
                else
                {
                    _spriteBatch.Draw(startscreen, DisplayManager.getDisplay().fullScreenRectangle, Color.White);
                }
                startScreen.Draw(_spriteBatch);

            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        #region buttonFunctions
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Exit();
        }
        private void CurrentLevelButton_Click(object sender, EventArgs e)
        {
            if(!isPlaying) StartLevel();
        }
        private void BossLevelButton_Click(object sender, EventArgs e)
        {
            if(!isPlaying)
            {
                currentLevel = 3;
                StartLevel();
            }
        }

        #endregion
        private void StartLevel()
        {
            isPlaying = true;
            plane.health = 3;
            level = levelCreatorFactory.GetLevelCreator(currentLevel).CreateLevel();
            level.StartLevel();
        }
    }
}
