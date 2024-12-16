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
        public static Texture2D texture;
        public Obstacle(Vector2 position)
        {
            this.position = position;
            size = new Vector2(50, 50);
            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new[] { Color.Black });
        }
        public override void Draw(SpriteBatch s)
        {
            s.Draw(texture, position, new Rectangle(0, 0, (int)size.X, (int)size.X), Color.Black);
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
            if (bullet.position.X + bullet.size.X > position.X && bullet.position.X < position.X + size.X) 
            {
                if(bullet.position.Y + bullet.size.Y > position.Y && bullet.position.Y < position.Y + size.Y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
