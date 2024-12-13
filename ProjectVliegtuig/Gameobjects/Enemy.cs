﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Interfaces;
using ProjectVliegtuig.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Gameobjects
{
    public class Enemy : GameObject
    {
        public static Texture2D texture;
        private Vector2 direction;
        private float acceleration = 0.10f;
        private float deceleration = 0.98f;

        private int _health = 3;

        public virtual int health
        {
            get { return _health; }
            set { _health = value; }
        }


        public Texture2D box;
        public Enemy(Vector2 position)
        {
            this.position = position;
            direction = new Vector2(0, 0);

            box = new Texture2D(graphicsDevice, 1, 1);
            box.SetData(new[] { Color.Red });

            origin = new Vector2(texture.Width / 2, texture.Height/2);
            size = new Vector2(texture.Width, texture.Height);
        }
        public override void Draw(SpriteBatch s)
        {
            //s.Draw(box, new Rectangle((int)position.X+((int)Math.Sin(rotation)*50), (int)position.Y +((int)Math.Sin(rotation)*50), 10, 10), Color.White);
            s.Draw(texture, position, new Rectangle(0, 0, (int)size.X, (int)size.Y), Color.White, rotation, origin, 1, SpriteEffects.None, 0.0f);
            //s.Draw(box, position, new Rectangle(0, 0, 10, 10), Color.White);
        }
        public override void Update(GameTime gameTime)
        {
            if (Game1.plane.position.X > position.X)
            {
                direction.X = 1;
            }
            if (Game1.plane.position.X < position.X)
            {
                direction.X = -1;
            }
            if (Game1.plane.position.Y > position.Y)
            {
                direction.Y = 1;
            }
            if (Game1.plane.position.Y < position.Y)
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
            else if (o is Plane) 
            {
                obj = o as Plane;
            } 
            else return false;
            if (obj.position.X + obj.origin.X >= position.X-origin.X && obj.position.X-obj.origin.X <= position.X + origin.X) 
            {
                if (obj.position.Y + obj.origin.Y >= position.Y-origin.Y && obj.position.Y-obj.origin.Y <= position.Y + origin.Y)
                {
                    health--;
                    speed = (obj.speed/2f)+speed;
                    return true;
                }
            }
            return false;
        }
        protected virtual void Shoot(GameTime gameTime)
        {
            
        }
    }
}
