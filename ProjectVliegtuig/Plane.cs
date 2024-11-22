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
        private float acceleration = 0.1f;
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

            //for (int i = 0; i < 6; i++)
            //{
            //    animatie.AddFrame(new AnimationFrame(new Rectangle(i * 100, 0, 100, 100)));
            //}
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
            //float rotationStatus = 0;
            //float keysDown = 0;
            KeyboardReader keyboard = new KeyboardReader();
            //if (Keyboard.GetState().IsKeyDown(Keys.Z))
            //{
            //    //speed.Y -= acceleration;
            //    rotationStatus += 2;
            //    keysDown++;
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.S))
            //{
            //    //speed.Y += acceleration;
            //    rotationStatus += 1;
            //    keysDown++;
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.D))
            //{
            //    //speed.X += acceleration;
            //    if (rotationStatus == 2) rotationStatus = 0;
            //    rotationStatus += 0.5f;
            //    keysDown++;
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.Q))
            //{
            //    //speed.X -= acceleration;
            //    rotationStatus += 1.5f;
            //    keysDown++;
            //}
            speed += Vector2.Multiply(keyboard.ReadInput(), acceleration);

            if(!(position.Y > 0+(texture.Height/6) && position.Y < 720-(texture.Height/6)))
            {
                if (speed.Y < -10) { speed.Y += acceleration * 50; position.Y += 10; }
                else if (speed.Y > 10) { speed.Y -= acceleration * 50; position.Y -= 10; }
                speed.Y = -speed.Y;
            }
            if (!(position.X < 1280 - (texture.Width / 12) && position.X > 0 + (texture.Width / 12)))
            {
                if (speed.X < -10) { speed.X += acceleration * 50; position.X += 10; }
                else if (speed.X > 10) { speed.X -= acceleration * 50; position.X -= 10; }
                speed.X = -speed.X;
            }
            position += speed;

            //if (keysDown != 0)
            //{
                //rotation = rotationStatus / keysDown;
                rotation = (float)(Math.Atan2(keyboard.ReadInput().X, -keyboard.ReadInput().Y));
            //}
        }
    }
}
