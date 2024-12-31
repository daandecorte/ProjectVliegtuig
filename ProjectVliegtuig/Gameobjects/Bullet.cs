using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Interfaces;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;


namespace ProjectVliegtuig.Gameobjects
{
    public class Bullet: Ammunition
    {
        public static Texture2D texture;
        protected override Texture2D _texture
        {
            get => texture;
        }
        public Bullet(Vector2 direction, Vector2 position)
        {
            this.rotation = (float)Math.Atan2(direction.X, -direction.Y);
            this.speed.X = (float)Math.Sin(rotation)*20;
            this.speed.Y = -(float)Math.Cos(rotation)*20;
            size = new Vector2(_texture.Width, _texture.Height);
            this.position = position + (this.speed * 5);
            this.origin = new Vector2(size.X / 2, size.Y / 2);

            rectangle = new Rectangle();
        }
    }
}
