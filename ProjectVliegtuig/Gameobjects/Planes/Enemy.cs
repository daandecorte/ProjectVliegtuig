using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Gameobjects.Abstracts;
using System;

namespace ProjectVliegtuig.Gameobjects.Planes
{
    public class Enemy : Abstracts.Plane
    {
        public static Texture2D texture;
        public static Texture2D healthBar;
        public Texture2D box;
        protected int maxHealth;

        protected override Texture2D _texture
        {
            get => texture;
        }
        public Enemy(Vector2 position, int health = 3)
        {
            this.position = position;
            direction = new Vector2(0, 0);

            box = new Texture2D(graphicsDevice, 1, 1);
            box.SetData(new[] { Color.Red });

            origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            size = new Vector2(_texture.Width, _texture.Height);

            this.health = health;
            maxHealth = health;
            rectangle = new Rectangle();
        }
        public override void Draw(SpriteBatch s)
        {
            //s.Draw(box, rectangle, Color.White);
            s.Draw(healthBar, new Vector2(position.X - origin.X / 2, position.Y - rectangle.Height / 1.2f), new Rectangle(0, (int)(healthBar.Height / 6 * Math.Ceiling(6f / maxHealth * health)) - healthBar.Height / 6, healthBar.Width, healthBar.Height / 6), Color.White);
            s.Draw(_texture, position, new Rectangle(0, 0, (int)size.X, (int)size.Y), Color.White, rotation, origin, scale, SpriteEffects.None, 0.0f);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateDirection(Player.Get().position);
        }
        protected virtual void UpdateDirection(Vector2 destination)
        {
            double destinationDirection = Math.Atan2(destination.X - position.X, destination.Y - position.Y);
            direction.X = (float)Math.Sin(destinationDirection);
            direction.Y = (float)Math.Cos(destinationDirection);
        }
        public bool Collide(GameObject obj)
        {
            if (rectangle.Intersects(obj.rectangle))
            {
                if (obj is Player)
                {
                    if (!(this is BossEnemy))
                        health = 0;
                }
                else health--;
                speed = obj.speed / 2f + speed;
                return true;
            }
            else return false;
        }
    }
}
