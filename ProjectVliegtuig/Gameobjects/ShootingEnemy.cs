﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Gameobjects
{
    internal class ShootingEnemy: Enemy
    {
        public static new Texture2D texture;
        private double secondCounter=0;
        protected override Texture2D _texture
        {
            get => texture;
        }
        public ShootingEnemy(Vector2 position): base(position)
        {}
        protected override void Shoot(GameTime gameTime)
        {
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (secondCounter >= 1d)
            {
                BulletManager.BulletList.Add(new Bullet(speed, position));
                secondCounter = 0;
            }
        }
    }
}
