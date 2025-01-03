using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Gameobjects.Abstracts;
using System;

namespace ProjectVliegtuig.Gameobjects.AmmunitionTypes
{
    public class Bullet : Ammunition
    {
        public static Texture2D texture;
        protected override Texture2D _texture
        {
            get => texture;
        }
        public Bullet(Vector2 direction, Vector2 position): base(direction, position)
        {
            this.speed.X = (float)Math.Sin(rotation) * 20;
            this.speed.Y = -(float)Math.Cos(rotation) * 20;
            this.position = position + this.speed * 5;
        }
    }
}
