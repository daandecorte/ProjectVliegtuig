using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Interfaces;

namespace ProjectVliegtuig.Gameobjects.Abstracts
{
    public abstract class Ammunition : GameObject, IMovable
    {
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
