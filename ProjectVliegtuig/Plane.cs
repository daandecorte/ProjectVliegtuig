using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectVliegtuig.Animation;
using ProjectVliegtuig.Input;
using ProjectVliegtuig.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig
{
    internal class Plane : IGameObject
    {
        private Texture2D texture;
        private Animatie animatie;
        private Vector2 speed;
        private float acceleration = 0.25f;
        private Vector2 origin;
        private float rotation;
        private Vector2 position;
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
            s.Draw(texture, position ,animatie.CurrentFrame.SourceRectangle, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0f);
        }

        public void Update(GameTime gameTime)
        {
            animatie.Update(gameTime);
            Move();
        }
        private void Move()
        {
            KeyboardReader keyboard = new KeyboardReader();

            speed += Vector2.Multiply(keyboard.ReadInput(), acceleration);

            if(!(position.Y > 0+(texture.Height/6) && position.Y < 720-(texture.Height/6)))
            {
                if (position.Y < 0) { position.Y += 50; }
                else if (position.Y > 720) { position.Y -= 50; }
                speed.Y = -speed.Y;
            }
            if (!(position.X < 1280 - (texture.Width / 12) && position.X > 0 + (texture.Width / 12)))
            {
                if (position.X < 0) { position.X += 50; }
                else if (position.X > 1280) { position.X -= 50; }
                speed.X = -speed.X;
            }
            speed *= 0.98f;
            position += speed;
            rotation = (float)(Math.Atan2(speed.X, -speed.Y));
        }
    }
}
