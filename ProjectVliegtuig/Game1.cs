using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectVliegtuig.Display;
using ProjectVliegtuig.Gameobjects;
using ProjectVliegtuig.Managers;

namespace ProjectVliegtuig
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D texture;
        private Texture2D background;
        private Texture2D blockTexture;
        private Texture2D startscreen;
        private Texture2D enemyPlane;

        public static Gameobjects.Plane plane;
        EnemyManager enemyManager;

        private bool isPlaying = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            DisplayManager.Graphics = _graphics;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {   
            base.Initialize();
        }

        protected override void LoadContent()
        {
            DisplayManager.Apply();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("plane");
            enemyPlane = Content.Load<Texture2D>("enemy");
            background = Content.Load<Texture2D>("background");
            startscreen = Content.Load<Texture2D>("start");
            GameObject.graphicsDevice = GraphicsDevice;
            LoadGameObjects();
        }
        private void LoadGameObjects()
        {
            plane = new Gameobjects.Plane(texture);
            enemyManager = new EnemyManager(enemyPlane);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit(); 
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                isPlaying = true;
            }
            if(plane.health<=0)
            {
                isPlaying = false;
            }
            if(isPlaying)
            {
                plane.Update(gameTime);
                BulletManager.Update(gameTime);
                enemyManager.Update(gameTime);
                //ObstacleSpawnManager.Update(gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSkyBlue);
            _spriteBatch.Begin();
            //_spriteBatch.Draw(background, new Rectangle(0,0,1280, 720), Color.White);
            if(isPlaying)
            {
                plane.Draw(_spriteBatch);
                BulletManager.Draw(_spriteBatch);
                enemyManager.Draw(_spriteBatch);
                //ObstacleSpawnManager.Draw(_spriteBatch);
            }
            else
            {
                if(plane.health<=0)
                {

                }
                else
                {
                    _spriteBatch.Draw(startscreen, new Rectangle(0, 0, DisplayManager.Graphics.PreferredBackBufferWidth, DisplayManager.Graphics.PreferredBackBufferHeight), Color.White);
                }
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
