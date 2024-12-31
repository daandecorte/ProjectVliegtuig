using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Display;
using ProjectVliegtuig.Gameobjects;
using ProjectVliegtuig.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Managers
{
    public class BulletManager: Manager<Ammunition>
    {
        private static BulletManager bulletManager;
        public static List<Ammunition> BulletList { get => bulletManager.ObjectList; }

        private BulletManager()
        {
            ObjectList = new List<Ammunition>();
        }
        public static BulletManager Init()
        {
            if(bulletManager==null)
            {
                bulletManager = new BulletManager();
            }
            bulletManager.ObjectList.Clear();
            return bulletManager;
        }
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < BulletList.Count; i++)
            {
                BulletList[i].Update(gameTime);
                if (BulletList[i].position.X > DisplayManager.getDisplay().width || BulletList[i].position.X < 0 || BulletList[i].position.Y < 0 || BulletList[i].position.Y > DisplayManager.getDisplay().height)
                {
                    BulletList.RemoveAt(i);
                    i--;
                }
            }
        }


    }
}
