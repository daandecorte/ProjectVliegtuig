using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Gameobjects.Abstracts;
using System;

namespace ProjectVliegtuig.Gameobjects.AmmunitionTypes
{
    internal class Bomb : Ammunition
    {
        public static Texture2D texture;
        protected override Texture2D _texture
        {
            get => texture;
        }
        public Bomb(Vector2 direction, Vector2 position) : base(Vector2.Zero, position)
        {
            this.speed.Y = -(float)Math.Cos(rotation) * 20;
            this.position = position + this.speed * 5;
            speed.Y = 5;
            if (Math.Sign(direction.X) < 1) rotation -= rotation;
        }
        public override void Move()
        {
            speed *= 1.05f;
            base.Move();
        }
    }
}
