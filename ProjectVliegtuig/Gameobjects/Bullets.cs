using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Gameobjects
{
    public static class Bullets
    {
        public static List<Bullet> BulletList { get; set; } = new List<Bullet>();

        public static void Draw(SpriteBatch s)
        {
            foreach (var bullet in BulletList)
            {
                bullet.Draw(s);
            }
        }
        public static void Move()
        {
            for(int i = 0; i<BulletList.Count;i++)
            {
                BulletList[i].Move();
                if(BulletList[i].position.X>1280|| BulletList[i].position.X<0|| BulletList[i].position.Y<0|| BulletList[i].position.Y>720)
                {
                    BulletList.RemoveAt(i);
                } 
            }
        }


    }
}
