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
        public override List<Enemy> ObjectList { get; set; } = new List<Enemy>();
        private static Random random = new Random();
        public EnemyManager()
        {

        }
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < ObjectList.Count; i++)
            {
                ObjectList[i].Update(gameTime);
                for (int j = 0; j<BulletManager.BulletList.Count; j++)
                {
                    try
                    {
                        if (ObjectList[i].Collide(BulletManager.BulletList[j]))
                        {
                            BulletManager.BulletList.Remove(BulletManager.BulletList[j]);
                            j--;
                        }
                    }
                    catch (Exception e)
                    { }
                }
                if (ObjectList[i].health <= 0)
                {
                    ObjectList.RemoveAt(i);
                    if (i > 0) i--;
                }
                else if (ObjectList[i].Collide(Game1.plane))
                {
                    if (ObjectList[i] is BossEnemy)
                    {
                        ObjectList[i].health--;
                    }
                    else
                    {
                        ObjectList.RemoveAt(i);
                        if (i > 0) i--;
                    }
                    Game1.plane.health--;
                }
            }
        }
        public void spawn(Enemy enemy)
        {
            ObjectList.Add(enemy);
        }
    }
}
