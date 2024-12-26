using Microsoft.Xna.Framework;
using ProjectVliegtuig.Effects;
using ProjectVliegtuig.Gameobjects;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Managers
{
    internal class ExplosionManager: Manager<Explosion>
    {
        private static ExplosionManager explosionManager;
        public static List<Explosion> ExplosionList { get => explosionManager.ObjectList; }

        private ExplosionManager()
        {
            ObjectList = new List<Explosion>();
        }
        public static ExplosionManager Init()
        {
            explosionManager = new ExplosionManager();
            return explosionManager;
        }
        public static void AddExplosion(Vector2 position)
        {
            ExplosionList.Add(new Explosion(position));
        }
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < ObjectList.Count; i++)
            {
                if (ObjectList[i].AnimationDone)
                {
                    ObjectList.RemoveAt(i);
                    if (i > 0) i--;
                }
            }
            base.Update(gameTime);
        }
    }
}
