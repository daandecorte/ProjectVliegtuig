using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Gameobjects
{
    public abstract class Plane: GameObject
    {
        public int health;
        protected double secondCounter = 0;
        protected float acceleration = 0.15f;
        protected float deceleration = 0.98f;
        protected Vector2 direction;
        protected float scale = 1f;
        protected abstract void Shoot(GameTime gameTime);
        protected virtual void Move()
        {
            speed += direction * acceleration;
            speed *= deceleration;
            position += speed;
            rotation = (float)Math.Atan2(speed.X, -speed.Y);
        }
    }
}
