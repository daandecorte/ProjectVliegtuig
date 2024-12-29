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
    internal class BomberEnemy: Enemy
    {
        public static new Texture2D texture;
        protected override Texture2D _texture
        {
            get => texture;
        }
        public BomberEnemy(Vector2 position): base(position, 5) 
        {
            scale = 1.4f;
            acceleration = 0.15f;
        }
        protected override void UpdateDirection()
        {
            if (200 - position.Y > 50 || 200 - position.Y < -50)
                direction.Y = Math.Sign(200 - (int)position.Y);
            else direction.Y = 0;
            direction.X = Math.Sign((int)Player.Get().position.X - position.X);
        }
        protected override void Shoot(GameTime gameTime)
        {
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (secondCounter >= 2d)
            {
                BulletManager.BulletList.Add(new Bomb(speed, position));
                secondCounter = 0;
            }
        }
    }
}
