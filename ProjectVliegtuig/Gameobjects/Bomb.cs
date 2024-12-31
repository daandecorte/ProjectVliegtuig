using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Gameobjects
{
    internal class Bomb: Bullet
    {
        public static new Texture2D texture;
        protected override Texture2D _texture
        {
            get => texture;
        }
        public Bomb(Vector2 direction, Vector2 position) : base(Vector2.Zero, position)
        {
            speed = new Vector2(0, 5);
            if (Math.Sign(direction.X) < 1) rotation -= rotation;
        }
        public override void Move()
        {
            speed *= 1.05f;
            base.Move();
        }
    }
}
