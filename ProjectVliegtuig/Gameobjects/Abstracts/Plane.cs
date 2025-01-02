using Microsoft.Xna.Framework;
using ProjectVliegtuig.Interfaces;
using System;

namespace ProjectVliegtuig.Gameobjects.Abstracts
{
    public abstract class Plane : GameObject, IMovable, IShooting
    {
        public int health;
        protected double secondCounter = 0;
        protected float acceleration = 0.15f;
        protected float deceleration = 0.98f;
        protected Vector2 direction;
        protected float scale = 1f;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Shoot(gameTime);
            Move();
        }
        public virtual void Shoot(GameTime gameTime) { }
        public virtual void Move()
        {
            speed += direction * acceleration;
            speed *= deceleration;
            position += speed;
            rotation = (float)Math.Atan2(speed.X, -speed.Y);
        }
    }
}
