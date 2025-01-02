using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Display;
using ProjectVliegtuig.Effects;
using ProjectVliegtuig.Gameobjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Managers
{
    public class EnemyManager : Manager<Enemy>
    {
        private static EnemyManager enemyManager;
        public static List<Enemy> EnemyList { get => enemyManager.ObjectList; }

        private EnemyManager()
        {
            ObjectList = new List<Enemy>();
        }
        public static EnemyManager Init()
        {
            if(enemyManager==null)
            {
                enemyManager = new EnemyManager();
            }
            enemyManager.ObjectList.Clear();
            return enemyManager;
        }
        public static void Spawn(Enemy enemy)
        {
            EnemyList.Add(enemy);
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < ObjectList.Count; i++)
            {
                ObjectList[i].Update(gameTime);
                for (int j = 0; j<BulletManager.BulletList.Count; j++)
                {
                    if (ObjectList[i].Collide(BulletManager.BulletList[j]))
                    {
                        BulletManager.BulletList.Remove(BulletManager.BulletList[j]);
                        j--;
                    }
                }
                if (!Player.Get().wasHit && ObjectList[i].Collide(Player.Get()))
                {
                    Player.Get().Hit(ObjectList[i]);
                } 
                if (ObjectList[i].health <= 0)
                {
                    ExplosionManager.AddExplosion(ObjectList[i].position);
                    ObjectList.RemoveAt(i);
                    if (i > 0) i--;
                }
            }
        }
    }
}
