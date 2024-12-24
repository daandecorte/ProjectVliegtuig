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
        private Vector2 direction;
        protected float acceleration = 0.10f;
        protected float deceleration = 0.98f;

        private int _health = 3;

        public virtual int health
        {
            get { return _health; }
            set { _health = value; }
        }
        protected virtual Texture2D _texture
        {
            get => texture;
        }
        public Texture2D box;
        public Enemy(Vector2 position)
        {
            this.position = position;
            direction = new Vector2(0, 0);

            box = new Texture2D(graphicsDevice, 1, 1);
            box.SetData(new[] { Color.Red });

            origin = new Vector2(_texture.Width / 2, _texture.Height/2);
            size = new Vector2(_texture.Width, _texture.Height);

        }
        public override void Draw(SpriteBatch s)
        {
            //s.Draw(box, rectangle, Color.White);
            s.Draw(_texture, position, new Rectangle(0, 0, (int)size.X, (int)size.Y), Color.White, rotation, origin, 1, SpriteEffects.None, 0.0f);
        }
        public override void Update(GameTime gameTime)
        {
            rectangle = new Rectangle((int)(position.X - origin.X), (int)(position.Y - origin.Y), (int)(size.X), (int)(size.Y));
            if (Game1.player.position.X > position.X)
            {
                direction.X = 1;
            }
            if (Game1.player.position.X < position.X)
            {
                direction.X = -1;
            }
            if (Game1.player.position.Y > position.Y)
            {
                direction.Y = 1;
            }
            if (Game1.player.position.Y < position.Y)
            {
                direction.Y = -1;
            }
            Move();
            Shoot(gameTime);
        }
        private void Move()
        {
            speed += direction * acceleration;
            speed *= deceleration;
            position += speed;
            rotation = (float)Math.Atan2(speed.X, -speed.Y);
        }
        public bool Collide(object o)
        {
            GameObject obj;
            if (o is Bullet)
            {
                obj = o as Bullet;
            }
            else if (o is Player) 
            {
                obj = o as Player;
            } 
            else return false;
            if(rectangle.Intersects(obj.rectangle))
            {
                health--;
                speed = (obj.speed/2f)+speed;
                obj.speed = speed + obj.speed;
                return true;
            }
            return false;
        }
        protected override void Shoot(GameTime gameTime)
        {
            
        }
    }
}
