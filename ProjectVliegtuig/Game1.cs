using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using ProjectVliegtuig.Display;
using ProjectVliegtuig.Gameobjects;
using ProjectVliegtuig.LevelCreators;
using ProjectVliegtuig.Levels;
using ProjectVliegtuig.Managers;

namespace ProjectVliegtuig
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D texture;
        private Texture2D healthBar;
        private Texture2D background;
        private Texture2D blockTexture;
        private Texture2D startscreen;
        private Texture2D gameOverScreen;
        private Texture2D enemyPlane;

        public static Gameobjects.Plane plane;
        //EnemyManager enemyManager;
        LevelCreatorFactory levelCreatorFactory;
        int currentLevel = 3;
        Level level;

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
            Gameobjects.Plane.texture = Content.Load<Texture2D>("plane");
            Gameobjects.Plane.healthBar = Content.Load<Texture2D>("hearts");
            Enemy.texture = Content.Load<Texture2D>("enemy");
            background = Content.Load<Texture2D>("background");
            startscreen = Content.Load<Texture2D>("start");
            gameOverScreen = Content.Load<Texture2D>("gameover");
            GameObject.graphicsDevice = GraphicsDevice;
            LoadGameObjects();
        }
        private void LoadGameObjects()
        {
            plane = new Gameobjects.Plane();
            //enemyManager = new EnemyManager();
            levelCreatorFactory = new LevelCreatorFactory();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit(); 
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Enter) && !isPlaying)
            {
                isPlaying = true;
                plane.health = 3;
                level = levelCreatorFactory.GetLevelCreator(currentLevel).CreateLevel();
            }
            if(plane.health<=0)
            {
                isPlaying = false;
            }
            if(isPlaying)
            {
                plane.Update(gameTime);
                BulletManager.Update(gameTime);
                level.Update(gameTime);
                //enemyManager.Update(gameTime);
                //ObstacleSpawnManager.Update(gameTime);
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
                //enemyManager.Draw(_spriteBatch);
                //ObstacleSpawnManager.Draw(_spriteBatch);
            }
            else
            {
                if(plane.health<=0)
                {
                    _spriteBatch.Draw(gameOverScreen, DisplayManager.getDisplay().fullScreenRectangle, Color.White);
                }
                else
                {
                    _spriteBatch.Draw(startscreen, DisplayManager.getDisplay().fullScreenRectangle, Color.White);
                }
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
