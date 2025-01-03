﻿using Microsoft.Xna.Framework;
using ProjectVliegtuig.Gameobjects.Planes;
using System.Collections.Generic;

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
                for (int j = 0; j<AmmunitionManager.AmmunitionList.Count; j++)
                {
                    if (ObjectList[i].Collide(AmmunitionManager.AmmunitionList[j]))
                    {
                        AmmunitionManager.AmmunitionList.Remove(AmmunitionManager.AmmunitionList[j]);
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
