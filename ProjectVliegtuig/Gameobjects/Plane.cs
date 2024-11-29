using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectVliegtuig.Animation;
using ProjectVliegtuig.Display;
using ProjectVliegtuig.Input;
using ProjectVliegtuig.Interfaces;
using ProjectVliegtuig.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Gameobjects
{
    internal class Plane : IGameObject
    {
        private Texture2D texture;
        private Animatie animatie;
        private Vector2 speed;
        private float acceleration = 0.25f;
        private float deceleration = 0.98f;
        private Vector2 origin;
        private float rotation;
        private Vector2 position;
        private bool pressed = false;
        public Plane(Texture2D texture)
        {
            this.texture = texture;
            position = new Vector2(200, 200);
            origin = new Vector2(texture.Width / 12, texture.Height / 6);
            speed = new Vector2(0, 0);

            animatie = new Animatie();
            animatie.GetFramesFromTextureProperties(texture.Width, texture.Height, 6, 3);
        }
        public void Draw(SpriteBatch s)
        {
            s.Draw(texture, position, animatie.CurrentFrame.SourceRectangle, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0f);
        }

        public void Update(GameTime gameTime)
        {
            animatie.Update(gameTime);
            Shoot();
            Move();
        }
        private void Move()
        {
            KeyboardReader keyboard = new KeyboardReader();

            speed += Vector2.Multiply(keyboard.ReadInput(), acceleration);

            if (!(position.Y >= origin.Y && position.Y <= DisplayManager.Graphics.PreferredBackBufferHeight - origin.Y))
            {
                if (position.Y <= origin.Y) { position.Y++; }
                else if (position.Y >= DisplayManager.Graphics.PreferredBackBufferHeight-origin.Y) { position.Y--; }
                speed.Y = -speed.Y;
            }
            if (!(position.X <= DisplayManager.Graphics.PreferredBackBufferWidth - origin.X && position.X >= origin.X))
            {
                if (position.X <= origin.X) { position.X++; }
                else if (position.X >= DisplayManager.Graphics.PreferredBackBufferWidth-origin.X) { position.X--; }
                speed.X = -speed.X;
            }
            speed *= deceleration;
            position += speed;
            rotation = (float)Math.Atan2(speed.X, -speed.Y);
        }
        private void Shoot()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !pressed)
            {
                pressed = true;
                BulletManager.BulletList.Add(new Bullet(speed, position));
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                pressed = false;
            }
        }
    }
}
