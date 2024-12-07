using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Gameobjects
{
    public class Obstacle : GameObject
    {
        private int size = 50;
        public Obstacle(Vector2 position)
        {
            this.position = position;

            this.texture = new Texture2D(graphicsDevice, 1, 1);
            this.texture.SetData(new[] { Color.Black });
        }
        public override void Draw(SpriteBatch s)
        {
            s.Draw(texture, position, new Rectangle(0, 0, size, size), Color.Black);
        }

        public override void Update(GameTime gameTime)
        {

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
                if(bullet.position.Y + bullet.size > position.Y && bullet.position.Y < position.Y + size)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
