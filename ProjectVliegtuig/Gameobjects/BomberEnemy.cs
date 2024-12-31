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
            acceleration = 0.20f;
        }
        protected override void UpdateDirection(Vector2 destination)
        {
            destination.Y = 200;
            base.UpdateDirection(destination);
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
