using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Display;
using ProjectVliegtuig.Gameobjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Managers
{
    public static class BulletManager
    {
        public static List<Bullet> BulletList { get; set; } = new List<Bullet>();

        public static void Draw(SpriteBatch s)
        {
            foreach (var bullet in BulletList)
            {
                bullet.Draw(s);
            }
        }
        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < BulletList.Count; i++)
            {
                BulletList[i].Update(gameTime);
                if (BulletList[i].position.X > DisplayManager.Graphics.PreferredBackBufferWidth || BulletList[i].position.X < 0 || BulletList[i].position.Y < 0 || BulletList[i].position.Y > DisplayManager.Graphics.PreferredBackBufferHeight)
                {
                    BulletList.RemoveAt(i);
                }
            }
        }


    }
}
