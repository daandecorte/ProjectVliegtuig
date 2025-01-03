using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Interfaces;
using System;

namespace ProjectVliegtuig.Gameobjects.Abstracts
{
    public abstract class Ammunition : GameObject, IMovable
    {
        public Ammunition(Vector2 direction, Vector2 position)
        {
            this.rotation = (float)Math.Atan2(direction.X, -direction.Y);
            size = new Vector2(_texture.Width, _texture.Height);
            this.origin = new Vector2(size.X / 2, size.Y / 2);
            rectangle = new Rectangle();
        }
        public override void Draw(SpriteBatch s)
        {
            s.Draw(_texture, position, new Rectangle(0, 0, (int)size.X, (int)size.Y), Color.White, rotation, origin, 1, SpriteEffects.None, 0);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Move();
        }
        public virtual void Move()
        {
            position += speed;
        }
    }
}
