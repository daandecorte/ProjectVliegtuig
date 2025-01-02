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
        private static Player player;

        public bool wasHit = false;
        private double invincibleTime = 1d;
        private double timer = 0;
        protected override Texture2D _texture 
        {
            get => texture;
        }
        public Player()
        {
            health = 3;
            acceleration = 0.25f;
            deceleration = 0.98f;
            Reset();
            size = new Vector2(_texture.Width/6, _texture.Height);
            origin = new Vector2(size.X / 2, size.Y / 2);

            animatie = new Animatie();
            animatie.GetFramesFromTexture(_texture.Width, _texture.Height, 6, 1);
            rectangle = new Rectangle();
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
            if(!wasHit) s.Draw(_texture, position, animatie.CurrentFrame.SourceRectangle, Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
            else if(!(10*Math.Round(timer, 1)%2==0)) s.Draw(_texture, position, animatie.CurrentFrame.SourceRectangle, Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
            s.Draw(healthBar, new Vector2(0, 0) , new Rectangle(0, 0, (healthBar.Width/3)*health, healthBar.Height), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if(wasHit)
            {
                timer+=gameTime.ElapsedGameTime.TotalSeconds;
                if(timer>=invincibleTime)
                {
                    timer = 0;
                    wasHit = false;
                }
            }
            animatie.fps = 10 + 5 * (int)Math.Sqrt(Math.Pow(speed.X, 2) + Math.Pow(speed.Y, 2));
            animatie.Update(gameTime);
            base.Update(gameTime);
            Collide();
        }
        public override void Move()
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
        public override void Shoot(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !pressed)
            {
                pressed = true;
                AmmunitionManager.AmmunitionList.Add(new Bullet(speed, position));
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
            for (int i = 0; i < AmmunitionManager.AmmunitionList.Count; i++)
            {
                Ammunition bullet = AmmunitionManager.AmmunitionList[i];
                if(rectangle.Intersects(bullet.rectangle))
                {
                    Hit(bullet);
                    AmmunitionManager.AmmunitionList.RemoveAt(i);
                    if(i>0) i--;
                }
            }
        }
        public void Reset()
        {
            health = 3;
            wasHit = false;
            timer = 0;
            position = new Vector2(DisplayManager.getDisplay().width / 2, DisplayManager.getDisplay().height / 2);
            speed = new Vector2(0, 0);
        }
        public void Hit(GameObject o)
        {
            if (o is BossEnemy) health = 0;
            if(!wasHit)
            {
                speed = (o.speed / 2f) + speed;
                health--;
                wasHit = true;
            }
        }
    }
}
