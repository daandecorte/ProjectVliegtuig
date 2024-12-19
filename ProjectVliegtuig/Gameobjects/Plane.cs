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
    public class Plane : GameObject
    {
        public static Texture2D texture;
        private Animatie animatie;
        private float acceleration = 0.25f;
        private float deceleration = 0.98f;
        private bool pressed = false;
        private double secondCounter = 0;
        public int health = 3;
        public static Texture2D healthBar;

        private Texture2D box;
        public Plane()
        {
            position = new Vector2(DisplayManager.getDisplay().width / 2, DisplayManager.getDisplay().height / 2);
            size = new Vector2(texture.Width/6, texture.Height/3);
            origin = new Vector2(size.X / 2, size.Y / 2);
            speed = new Vector2(0, 0);

            box = new Texture2D(graphicsDevice, 1, 1);
            box.SetData(new[] { Color.Red });
            Debug.Write("animatie");
            animatie = new Animatie();
            animatie.GetFramesFromTextureProperties(texture.Width, texture.Height, 6, 3);
        }
        public override void Draw(SpriteBatch s)
        {
            //s.Draw(box, new Rectangle((int)position.X - (int)origin.X, (int)position.Y - (int)origin.Y, (int)size.X, (int)size.Y), Color.White);

            s.Draw(texture, position, animatie.CurrentFrame.SourceRectangle, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0f);
            s.Draw(healthBar, new Vector2(0, 0) , new Rectangle(0, 0, (healthBar.Width/3)*health, healthBar.Height), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            animatie.Update(gameTime);
            Shoot(gameTime);
            Move();
            Collide();
        }
        private void Move()
        {
            KeyboardReader keyboard = new KeyboardReader();

            speed += Vector2.Multiply(keyboard.ReadInput(), acceleration);

            if (!(position.Y >= origin.Y && position.Y <= DisplayManager.getDisplay().height - origin.Y))
            {
                if (position.Y <= origin.Y) { position.Y++; }
                else if (position.Y >= DisplayManager.getDisplay().height - origin.Y) { position.Y--; }
                speed.Y = -speed.Y;
            }
            if (!(position.X <= DisplayManager.getDisplay().width - origin.X && position.X >= origin.X))
            {
                if (position.X <= origin.X) { position.X++; }
                else if (position.X >= DisplayManager.getDisplay().width - origin.X) { position.X--; }
                speed.X = -speed.X;
            }
            speed *= deceleration;
            position += speed;
            rotation = (float)Math.Atan2(speed.X, -speed.Y);
        }
        private void Shoot(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !pressed)
            {
                pressed = true;
                BulletManager.BulletList.Add(new Bullet(speed, position));
                secondCounter = 0;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space) && pressed)
            {
                secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
                if (secondCounter >= 0.2d)
                {
                    pressed = false;
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                pressed = false;
            }
        }
        private void Collide()
        {
            for (int i = 0; i < BulletManager.BulletList.Count; i++)
            {
                Bullet bullet = BulletManager.BulletList[i];
                if (bullet.position.X + bullet.origin.X >= position.X - origin.X && bullet.position.X - bullet.origin.X <= position.X + origin.X)
                {
                    if (bullet.position.Y + bullet.origin.Y >= position.Y - origin.Y && bullet.position.Y - bullet.origin.Y <= position.Y + origin.Y)
                    {
                        health--;
                        BulletManager.BulletList.RemoveAt(i);
                        if(i>0) i--;
                    }
                }
            }
        }
    }
}
