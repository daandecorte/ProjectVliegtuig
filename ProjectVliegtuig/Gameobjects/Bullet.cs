using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Interfaces;
using System;
using System.Diagnostics;


namespace ProjectVliegtuig.Gameobjects
{
    public class Bullet: IGameObject
    {
        public static Texture2D texture;
        public Vector2 position;
        public int size = 10;
        private float rotation;
        private Vector2 speed;
        public Bullet(Vector2 speed, Vector2 position)
        {
            this.rotation = (float)Math.Atan2(speed.X, -speed.Y);
            this.speed.X = (float)Math.Sin(rotation)*20;
            this.speed.Y = -(float)Math.Cos(rotation)*20;
            this.position = position;
            this.position += speed*5;
        }
        public void Draw(SpriteBatch s)
        {
            s.Draw(texture, position, new Rectangle(0, 0, size, size), Color.Black, rotation, new Vector2(size/2, size/2), 1, SpriteEffects.None, 0);
        }
        public void Update(GameTime gameTime)
        {
            Move();
        }
        private void Move()
        {
            position += speed;
        }


    }
}
