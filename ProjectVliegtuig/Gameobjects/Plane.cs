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
        public int health = 3;
        protected double secondCounter = 0;
        protected float acceleration = 0.10f;
        protected float deceleration = 0.98f;
        protected Vector2 direction;
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
