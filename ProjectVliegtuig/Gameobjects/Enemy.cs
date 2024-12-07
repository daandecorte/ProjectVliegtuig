using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Interfaces;
using ProjectVliegtuig.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Gameobjects
{
    public class Enemy : GameObject
    {
        private Vector2 direction;
        private Vector2 origin;
        private int size = 50;
        public int health = 3;
        public Enemy(Vector2 position)
        {
            this.position = position;
            direction = new Vector2(0, 0);
            origin = new Vector2(size/2, size/2);
        }
        public override void Draw(SpriteBatch s)
        {
            texture = new Texture2D(s.GraphicsDevice, 1, 1);
            texture.SetData(new[] { Color.Red });
            s.Draw(texture, position, new Rectangle(0, 0, size, size), Color.White, rotation, origin, 1.0f ,SpriteEffects.None, 0.0f);
        }
        public override void Update(GameTime gameTime)
        {
            if (Game1.plane.position.X > position.X)
            {
                direction.X = 1;
            }
            if(Game1.plane.position.X < position.X)
            {
                direction.X = -1;
            }
            if(Game1.plane.position.Y > position.Y)
            {
                direction.Y = 1;
            }
            if(Game1.plane.position.Y < position.Y)
            {
                direction.Y = -1;
            }
            speed += direction * 0.10f;
            speed *= 0.98f;
            position += speed;
            rotation = (float)Math.Atan2(speed.X, -speed.Y);
        }
        public bool Collide(object o)
        {
            Bullet bullet;
            if (o is Bullet)
            {
                bullet = o as Bullet;
            }
            else return false;
            if (bullet.position.X + bullet.size > position.X && bullet.position.X < position.X + size)
            {
                if (bullet.position.Y + bullet.size > position.Y && bullet.position.Y < position.Y + size)
                {
                    health--;
                    speed = (bullet.speed/1.5f)+speed;
                    return true;
                }
            }
            return false;
        }
    }
}
