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
            rectangle.X = (int)position.X - (int)origin.X;
            rectangle.Y = (int)position.Y - (int)origin.Y;
        }
        public abstract void Draw(SpriteBatch s);
    }
}
