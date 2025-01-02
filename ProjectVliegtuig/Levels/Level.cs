using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Display;
using ProjectVliegtuig.Gameobjects;
using ProjectVliegtuig.Interfaces;
using ProjectVliegtuig.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ProjectVliegtuig.Levels
{
    public class Level: IGameObject
    {
        public static SpriteFont Font;
        public static Texture2D winscreen;
        private static List<IManager> managers;
        private static Random random = new Random();

        private int maxEnemyLevel;
        private int minEnemyLevel;
        private int spawnInterval;
        private double secondCounter = 0;

        private int enemyCount;
        private int enemiesSpawned = 0;
        private bool hasWon = false;

        private bool MaxEnemiesSpawned { get => enemiesSpawned >= enemyCount; }
        public bool LevelOver { get; private set; }
        public Level(int minEnemyLevel, int maxEnemyLevel, int enemyCount, int spawnInterval)
        {
            this.minEnemyLevel = minEnemyLevel-1;
            this.maxEnemyLevel = maxEnemyLevel;
            this.enemyCount = enemyCount;
            this.spawnInterval = spawnInterval;

            Init();
        }
        private void Init()
        {
            Player.Get().Reset();
            managers = [
                EnemyManager.Init(),
                AmmunitionManager.Init(),
                ExplosionManager.Init()
            ];
        }
        public void Update(GameTime gameTime)
        {
            Spawn(gameTime);
            foreach (var manager in managers)
            {
                manager.Update(gameTime);
            }
            Player.Get().Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, $"{enemyCount - enemiesSpawned + EnemyManager.EnemyList.Count} Enemies Remaining", new Vector2(1600, 10), Color.Black, 0, new Vector2(0,0), 1.2f, SpriteEffects.None, 0);
            foreach (var manager in managers)
            {
                manager.Draw(spriteBatch);
            }
            Player.Get().Draw(spriteBatch);
            if (hasWon) spriteBatch.Draw(winscreen, new Rectangle(0, 0, winscreen.Width, winscreen.Height), Color.White);
        }
        private void Spawn(GameTime gameTime)
        {
            if (!MaxEnemiesSpawned)
            {
                secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
                Vector2 spawnPos;
                if (secondCounter >= spawnInterval)
                {
                    spawnPos = RandomSpawnPosition();
                    switch (random.Next(minEnemyLevel, maxEnemyLevel))
                    {
                        case 0:
                            EnemyManager.Spawn(new Enemy(spawnPos));
                            break;
                        case 1:
                            EnemyManager.Spawn(new ShootingEnemy(spawnPos));
                            break;
                        case 2:
                            EnemyManager.Spawn(new BomberEnemy(spawnPos));
                            break;
                        case 3:
                            EnemyManager.Spawn(new BossEnemy(spawnPos));
                            break;
                        default:
                            break;
                    }
                    secondCounter = 0;
                    enemiesSpawned++;
                }
            }
            else
            {
                if(EnemyManager.EnemyList.Count==0)
                {
                    hasWon = true;
                    secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
                    if(secondCounter>=1)
                    {
                        LevelOver = true;
                    }
                }
            }
        }
        private Vector2 RandomSpawnPosition()
        {
            Vector2 spawnPos = new Vector2();
            switch (random.Next(0, 4))
            {
                case 0:
                    spawnPos = new Vector2(random.Next(0, DisplayManager.getDisplay().width), -100);
                    break;
                case 1:
                    spawnPos = new Vector2(random.Next(0, DisplayManager.getDisplay().width), DisplayManager.getDisplay().height + 100);
                    break;
                case 2:
                    spawnPos = new Vector2(-100, random.Next(0, DisplayManager.getDisplay().height));
                    break;
                case 3:
                    spawnPos = new Vector2(DisplayManager.getDisplay().width + 100, random.Next(0, DisplayManager.getDisplay().height));
                    break;
            }
            return spawnPos;
        }

    }
}
