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
        private Texture2D blockTexture;

        Gameobjects.Plane plane;

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

            Bullet.texture = new Texture2D(GraphicsDevice, 1, 1);
            Bullet.texture.SetData(new[] { Color.Black });
            Obstacle.texture = new Texture2D(GraphicsDevice, 1, 1);
            Obstacle.texture.SetData(new[] { Color.Black });
            LoadGameObjects();
        }
        private void LoadGameObjects()
        {
            plane = new Gameobjects.Plane(texture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit(); 
            }

            plane.Update(gameTime);
            BulletManager.Update(gameTime);
            ObstacleSpawnManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSkyBlue);
            _spriteBatch.Begin();
            plane.Draw(_spriteBatch);
            BulletManager.Draw(_spriteBatch);
            ObstacleSpawnManager.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
