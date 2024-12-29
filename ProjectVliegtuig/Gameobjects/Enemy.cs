using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Interfaces;
using ProjectVliegtuig.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Gameobjects
{
    public class Enemy : Plane
    {
        public static Texture2D texture;
        public static Texture2D healthBar;
        public Texture2D box;
        protected int maxHealth;

        protected virtual Texture2D _texture
        {
            get => texture;
        }
        public Enemy(Vector2 position, int health = 3)
        {
            this.position = position;
            direction = new Vector2(0, 0);

            box = new Texture2D(graphicsDevice, 1, 1);
            box.SetData(new[] { Color.Red });

            origin = new Vector2(_texture.Width / 2, _texture.Height/2);
            size = new Vector2(_texture.Width, _texture.Height);

            this.health = health;
            maxHealth = health;
            rectangle = new Rectangle((int)(position.X - origin.X), (int)(position.Y - origin.Y), (int)size.X, (int)size.Y);
        }
        public override void Draw(SpriteBatch s)
        {
            //s.Draw(box, rectangle, Color.White);
            s.Draw(healthBar, new Vector2(position.X-(origin.X/2), position.Y-size.Y), new Rectangle(0, (int)(((healthBar.Height)/6)*Math.Ceiling((6f/ (float)maxHealth)*health))-healthBar.Height/6, healthBar.Width, healthBar.Height/6), Color.White);
            s.Draw(_texture, position, new Rectangle(0, 0, (int)size.X, (int)size.Y), Color.White, rotation, origin, scale, SpriteEffects.None, 0.0f);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateDirection();
            Move();
            Shoot(gameTime);
        }
        protected virtual void UpdateDirection()
        {
            direction.X = Math.Sign((int)Player.Get().position.X - (int)position.X);
            direction.Y = Math.Sign((int)Player.Get().position.Y - (int)position.Y);
        }
        public bool Collide(GameObject obj)
        {
            if(rectangle.Intersects(obj.rectangle))
            {
                health--;
                speed = (obj.speed/2f)+speed;
                obj.speed = speed + obj.speed;
                return true;
            }
            else return false;
        }
        protected override void Shoot(GameTime gameTime)
        {}
    }
}
