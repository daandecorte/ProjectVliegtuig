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
using System.Text;

namespace ProjectVliegtuig
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D background;

        public static Player player;
        public static int currentLevel = 1;
        public static int lastLevel = 1;

        LevelCreatorFactory levelCreatorFactory;
        Level level;
        private int bossLevel = 0;

        private bool isPlaying = false;
        private bool pauzePressed = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            DisplayManager.Init(_graphics);
            StartScreen.Init();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            DisplayManager.getDisplay().Apply();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameObject.graphicsDevice = GraphicsDevice;

            Player.texture = Content.Load<Texture2D>("plane");
            Player.healthBar = Content.Load<Texture2D>("hearts");
            Enemy.texture = Content.Load<Texture2D>("enemy");
            ShootingEnemy.texture = Content.Load<Texture2D>("shootingenemy");
            BossEnemy.texture = Content.Load<Texture2D>("bossenemy");
            background = Content.Load<Texture2D>("background");
            Level.winscreen = Content.Load<Texture2D>("winscreen");
            Bullet.texture = Content.Load<Texture2D>("bullet");
            Button.texture = Content.Load<Texture2D>("button");
            Button.font = Content.Load<SpriteFont>("font");
            Level.Font = Content.Load<SpriteFont>("font");
            StartScreen.Font = Content.Load<SpriteFont>("font");
            StartScreen.pauzeScreen = Content.Load<Texture2D>("start");
            StartScreen.gameOverScreen = Content.Load<Texture2D>("gameover");
            LoadGameObjects();
        }
        private void LoadGameObjects()
        {
            BulletManager.Init();
            player = new Player();
            levelCreatorFactory = new LevelCreatorFactory();
            StartScreen.GetStartScreen().exitButton.Click += ExitButton_Click;
            StartScreen.GetStartScreen().currentLevelButton.Click += CurrentLevelButton_Click;
            StartScreen.GetStartScreen().bossLevelButton.Click += BossLevelButton_Click;
            StartScreen.GetStartScreen().replayButton.Click += ReplayButton_Click;
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P) && level != null && !pauzePressed)
            {
                isPlaying = !isPlaying;
                pauzePressed = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.P)) pauzePressed = false;

            if (isPlaying)
            {
                if (player.health <= 0)
                {
                    isPlaying = false;
                    lastLevel = currentLevel;
                    currentLevel = 1;
                }
                player.Update(gameTime);
                BulletManager.GetBulletManager().Update(gameTime);
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
                StartScreen.Update();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicWrap);
            _spriteBatch.Draw(background, new Rectangle(0,0,DisplayManager.getDisplay().width, DisplayManager.getDisplay().height), Color.White);
            if(isPlaying)
            {
                player.Draw(_spriteBatch);
                BulletManager.GetBulletManager().Draw(_spriteBatch);
                level.Draw(_spriteBatch);
                IsMouseVisible = false;
            }
            else
            {
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
            player.Reset();
            level = levelCreatorFactory.GetLevelCreator(currentLevel).CreateLevel();
        }
    }
}
