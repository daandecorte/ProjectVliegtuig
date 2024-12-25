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
    public class EnemyManager : Manager<Enemy>
    {
        public EnemyManager()
        {
            ObjectList = new List<Enemy>();
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
                if (ObjectList[i].Collide(Game1.player))
                {
                    if (ObjectList[i] is BossEnemy)
                    {
                        ObjectList[i].health--;
                    }
                    else
                    {
                        ObjectList[i].health=0;
                    }
                    Game1.player.health--;
                }
                if (ObjectList[i].health <= 0)
                {
                    ExplosionManager.GetExplosionManager().AddExplosion(ObjectList[i].position);
                    ObjectList.RemoveAt(i);
                    if (i > 0) i--;
                }
            }
        }
        public void spawn(Enemy enemy)
        {
            ObjectList.Add(enemy);
        }
    }
}
