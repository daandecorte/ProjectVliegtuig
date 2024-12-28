using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Interfaces;
using System;
using System.Diagnostics;


namespace ProjectVliegtuig.Gameobjects
{
    public class Bullet: GameObject
    {
        public static Texture2D texture;
        public Texture2D box;
        public Bullet(Vector2 direction, Vector2 position)
        {
            this.rotation = (float)Math.Atan2(direction.X, -direction.Y);
            this.speed.X = (float)Math.Sin(rotation)*20;
            this.speed.Y = -(float)Math.Cos(rotation)*20;
            size = new Vector2(texture.Width, texture.Height);
            this.position = position + (this.speed * 5);
            this.origin = new Vector2(size.X / 2, size.Y / 2);

            box = new Texture2D(graphicsDevice, 1, 1);
            box.SetData(new[] { Color.Red });
            rectangle = new Rectangle((int)(this.position.X-origin.X), (int)(this.position.Y-origin.Y), (int)size.X, (int)size.Y);
        }
        public override void Draw(SpriteBatch s)
        {
            //s.Draw(box, rectangle, Color.White);
            s.Draw(texture, position,  new Rectangle(0, 0, (int)size.X, (int)size.Y), Color.White, rotation, origin, 1, SpriteEffects.None, 0);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Move();
        }
        private void Move()
        {
            position += speed;
        }
    }
}
