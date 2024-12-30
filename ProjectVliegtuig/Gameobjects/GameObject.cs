using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Gameobjects
{
    public abstract class GameObject: IGameObject
    {
        public static GraphicsDevice graphicsDevice;
        public Rectangle rectangle;
        public Vector2 speed;
        public Vector2 position;
        public float rotation;
        public Vector2 size;
        public Vector2 origin;
        public virtual void Update(GameTime gameTime)
        {
            rectangle.Width = (int)Math.Sqrt(Math.Pow(size.X * Math.Cos(rotation), 2) + Math.Pow(size.Y * Math.Sin(rotation), 2));
            rectangle.Height = (int)Math.Sqrt(Math.Pow(size.Y * Math.Cos(rotation), 2) + Math.Pow(size.X * Math.Sin(rotation), 2));
            rectangle.X = (int)position.X - rectangle.Width / 2;
            rectangle.Y = (int)position.Y - rectangle.Height / 2;
        }
        public abstract void Draw(SpriteBatch s);
    }
}
