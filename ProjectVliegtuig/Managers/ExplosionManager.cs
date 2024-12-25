using Microsoft.Xna.Framework;
using ProjectVliegtuig.Effects;
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
        public static ExplosionManager explosionManager;
        private ExplosionManager()
        {
            ObjectList = new List<Explosion>();
        }
        public static void Init()
        {
            if (explosionManager == null) explosionManager = new ExplosionManager();
        }
        public static ExplosionManager GetExplosionManager()
        {
            return explosionManager;
        }
        public void AddExplosion(Vector2 position)
        {
            ObjectList.Add(new Explosion(position));
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
