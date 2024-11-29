using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;


namespace ProjectVliegtuig.Gameobjects
{
    public class Bullet
    {
        private float rotation;
        public Vector2 position;
        private Vector2 speed;
        public static Texture2D texture;
        public Bullet(Vector2 speed, Vector2 position)
        {
            this.rotation= (float)Math.Atan2(speed.X, -speed.Y);
            this.speed.X = (float)Math.Sin(rotation)*20;
            this.speed.Y = -(float)Math.Cos(rotation)*20;
            this.position = position;
            this.position += speed*5;
        }
        public void Draw(SpriteBatch s)
        {
            s.Draw(texture, position, new Rectangle(0, 0, 10, 10), Color.Black, rotation, new Vector2(0, 0), 1, SpriteEffects.None, 0);
        }
        public void Move()
        {
            position += speed;
        }
    }
}
