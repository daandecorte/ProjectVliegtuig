using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using ProjectVliegtuig.Display;
using ProjectVliegtuig.Effects;
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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        private Texture2D background;
        public static Player player;

        LevelCreatorFactory levelCreatorFactory;
        Level level;
        private int bossLevel = 0;
        public static int currentLevel = 1;
        public static int lastLevel = 1;

        private bool isPlaying = false;
        private bool pauzePressed = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            DisplayManager.Init(graphics);
            StartScreen.Init();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            DisplayManager.getDisplay().Apply();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameObject.graphicsDevice = GraphicsDevice;

            background = Content.Load<Texture2D>("background");
            
            Player.texture = Content.Load<Texture2D>("plane");
            Player.healthBar = Content.Load<Texture2D>("hearts");
            
            Enemy.texture = Content.Load<Texture2D>("enemy");
            Enemy.healthBar = Content.Load<Texture2D>("enemyhealth");
            ShootingEnemy.texture = Content.Load<Texture2D>("shootingenemy");
            BossEnemy.texture = Content.Load<Texture2D>("bossenemy");
            
            Bullet.texture = Content.Load<Texture2D>("bullet");
            
            Button.texture = Content.Load<Texture2D>("button");
            Button.font = Content.Load<SpriteFont>("font");

            Explosion.texture = Content.Load<Texture2D>("explosion");
            
            Level.winscreen = Content.Load<Texture2D>("winscreen");
            Level.Font = Content.Load<SpriteFont>("font");

            StartScreen.Font = Content.Load<SpriteFont>("font");
            StartScreen.pauzeScreen = Content.Load<Texture2D>("start");
            StartScreen.gameOverScreen = Content.Load<Texture2D>("gameover");
            
            LoadGameObjects();
        }
        private void LoadGameObjects()
        {
            BulletManager.Init();
            ExplosionManager.Init();
            player = new Player();
            levelCreatorFactory = new LevelCreatorFactory();
            
            StartScreen.GetStartScreen().exitButton.Click += ExitButton_Click;
            StartScreen.GetStartScreen().currentLevelButton.Click += CurrentLevelButton_Click;
            StartScreen.GetStartScreen().bossLevelButton.Click += BossLevelButton_Click;
            StartScreen.GetStartScreen().replayButton.Click += ReplayButton_Click;
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P) && level?.LevelOver==false && !pauzePressed)
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
                ExplosionManager.GetExplosionManager().Update(gameTime);
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
                StartScreen.GetStartScreen().Update(gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicWrap);
            spriteBatch.Draw(background, new Rectangle(0,0,DisplayManager.getDisplay().width, DisplayManager.getDisplay().height), Color.White);
            if(isPlaying)
            {
                BulletManager.GetBulletManager().Draw(spriteBatch);
                ExplosionManager.GetExplosionManager().Draw(spriteBatch);
                player.Draw(spriteBatch);
                level.Draw(spriteBatch);
                IsMouseVisible = false;
            }
            else
            {
                StartScreen.GetStartScreen().Draw(spriteBatch);
                IsMouseVisible = true;

            }
            spriteBatch.End();

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
            ExplosionManager.GetExplosionManager().ObjectList.Clear();
            isPlaying = true;
            player.Reset();
            level = levelCreatorFactory.GetLevelCreator(currentLevel).CreateLevel();
        }
    }
}
