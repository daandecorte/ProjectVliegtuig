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
    public class Player : Plane
    {
        public static Texture2D texture;
        private Animatie animatie;

        private bool pressed = false;
        public static Texture2D healthBar;

        KeyboardReader keyboard = new KeyboardReader();

        private Texture2D box;
        private static Player player;
        public Player()
        {
            acceleration = 0.25f;
            deceleration = 0.98f;
            Reset();
            size = new Vector2(texture.Width/6, texture.Height);
            origin = new Vector2(size.X / 2, size.Y / 2);

            box = new Texture2D(graphicsDevice, 1, 1);
            box.SetData(new[] { Color.Red });

            animatie = new Animatie();
            animatie.GetFramesFromTexture(texture.Width, texture.Height, 6, 1);
        }
        public static void Init()
        {
            if(player==null)
            {
                player = new Player();
            }
        }
        public static Player Get()
        {
            return player;
        }
        public override void Draw(SpriteBatch s)
        {
            //s.Draw(box, rectangle, Color.White);

            s.Draw(texture, position, animatie.CurrentFrame.SourceRectangle, Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
            s.Draw(healthBar, new Vector2(0, 0) , new Rectangle(0, 0, (healthBar.Width/3)*health, healthBar.Height), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            rectangle = new Rectangle((int)position.X - (int)origin.X, (int)position.Y - (int)origin.Y, (int)size.X, (int)size.Y);
            animatie.fps = 10 + 5 * (int)Math.Sqrt(Math.Pow(speed.X, 2) + Math.Pow(speed.Y, 2));
            animatie.Update(gameTime);
            Shoot(gameTime);
            Move();
            Collide();
        }
        protected override void Move()
        {
            direction = keyboard.ReadInput();

            base.Move();

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
        }
        protected override void Shoot(GameTime gameTime)
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
                if(rectangle.Intersects(bullet.rectangle))
                {
                    health--;
                    speed = (bullet.speed / 2f) + speed;
                    BulletManager.BulletList.RemoveAt(i);
                    if(i>0) i--;
                }
            }
        }
        public void Reset()
        {
            health = 3;
            position = new Vector2(DisplayManager.getDisplay().width / 2, DisplayManager.getDisplay().height / 2);
            speed = new Vector2(0, 0);
        }
    }
}
