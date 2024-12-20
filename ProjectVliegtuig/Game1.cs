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
        private Texture2D pauzescreen;
        private Texture2D gameOverScreen;

        public static Gameobjects.Plane plane;
        LevelCreatorFactory levelCreatorFactory;
        public static int currentLevel = 1;
        private int bossLevel = 0;
        public static int lastLevel = 1;
        Level level;

        private bool isPlaying = false;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            DisplayManager.Init(_graphics);
            StartScreen.Init();
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
            pauzescreen = Content.Load<Texture2D>("start");
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
            StartScreen.GetStartScreen().exitButton.Click += ExitButton_Click;
            StartScreen.GetStartScreen().currentLevelButton.Click += CurrentLevelButton_Click;
            StartScreen.GetStartScreen().bossLevelButton.Click += BossLevelButton_Click;
            StartScreen.GetStartScreen().replayButton.Click += ReplayButton_Click;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit(); 
            }

            if(isPlaying)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.P))
                {
                    isPlaying = false;
                }
                if (plane.health <= 0)
                {
                    isPlaying = false;
                    lastLevel = currentLevel;
                    currentLevel = 1;
                }
                plane.Update(gameTime);
                BulletManager.Update(gameTime);
                level.Update(gameTime);
                if (level?.LevelOver == true)
                {
                    isPlaying = false;
                    lastLevel = currentLevel;
                    currentLevel++;
                    if (levelCreatorFactory.GetLevelCreator(currentLevel) is CreatorBossLevel) bossLevel = currentLevel;
                    if (levelCreatorFactory.GetLevelCreator(currentLevel) == null) currentLevel = 1;
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    StartLevel();
                }
                StartScreen.Update();
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
                IsMouseVisible = false;
            }
            else
            {
                if (plane.health <= 0)
                {
                    _spriteBatch.Draw(gameOverScreen, DisplayManager.getDisplay().fullScreenRectangle, Color.White);
                }
                else
                {
                    _spriteBatch.Draw(pauzescreen, DisplayManager.getDisplay().fullScreenRectangle, Color.White);
                }
                StartScreen.Draw(_spriteBatch);
                IsMouseVisible = true;

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
                currentLevel = bossLevel;
                StartLevel();
            }
        }
        private void ReplayButton_Click(object sender, EventArgs e)
        {
            if (!isPlaying)
            {
                currentLevel=lastLevel;
                StartLevel();
            }
        }

        #endregion
        private void StartLevel()
        {
            BulletManager.BulletList.Clear();
            isPlaying = true;
            plane.health = 3;
            level = levelCreatorFactory.GetLevelCreator(currentLevel).CreateLevel();
            level.StartLevel();
        }
    }
}
