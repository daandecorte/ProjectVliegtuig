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
        private int _health = 10;
        private double secondCounter = 0;
        private bool left = true;
        public override int health
        {
            get { return _health; }
            set { _health = value; }
        }

        public BossEnemy(Texture2D texture, Vector2 position) : base(texture, position)
        {
        }
        protected override void Shoot(GameTime gameTime)
        {
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (secondCounter >= 0.2d)
            {
                Vector2 d = new Vector2((float)Math.Sin(rotation), -(float)Math.Cos(rotation));
                if(left)
                {
                    BulletManager.BulletList.Add(new Bullet(d, new Vector2(position.X-(d.Y*50), position.Y+(d.X *50))));
                    left = !left;
                }
                else
                {
                    BulletManager.BulletList.Add(new Bullet(d, new Vector2(position.X+(d.Y *50), position.Y - (d.X * 50))));
                    left = !left;
                }
                secondCounter = 0;
            }
        }
    }
}
