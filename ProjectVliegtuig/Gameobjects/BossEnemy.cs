using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Gameobjects
{
    public class BossEnemy: Enemy
    {
        public static new Texture2D texture;
        private int _health = 10;
        private double secondCounter = 0;
        private bool left = true;
        public override int health
        {
            get { return _health; }
            set { _health = value; }
        }
        protected override Texture2D _texture 
        {
            get => texture;
        }

        public BossEnemy(Vector2 position) : base(position)
        {
            acceleration = 0.15f;
        }
        protected override void Shoot(GameTime gameTime)
        {
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (secondCounter >= 0.15d)
            {
                Vector2 d = new Vector2((float)Math.Sin(rotation), -(float)Math.Cos(rotation));
                if(left)
                {
                    BulletManager.BulletList.Add(new Bullet(d, new Vector2(position.X - (d.Y * 50), position.Y + (d.X * 50))));
                    left = !left;
                }
                else
                {
                    BulletManager.BulletList.Add(new Bullet(d, new Vector2(position.X + (d.Y * 50), position.Y - (d.X * 50))));
                    left = !left;
                }
                secondCounter = 0;
            }
        }
    }
}
