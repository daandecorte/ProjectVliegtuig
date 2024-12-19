using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Display;
using ProjectVliegtuig.Gameobjects;
using ProjectVliegtuig.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Levels
{
    public class Level
    {
        private int maxEnemyLevel;
        private int minEnemyLevel;
        private int spawnInterval;
        private EnemyManager enemyManager;
        private static Random random = new Random();
        private double secondCounter;

        private int enemyCount;
        private int enemiesSpawned = 0;
        public bool started { get; private set; } = false;

        private bool MaxEnemiesSpawned { get => enemiesSpawned >= enemyCount; }
        public bool LevelOver { get; private set; }
        public Level(int minEnemyLevel, int maxEnemyLevel, int enemyCount, int spawnInterval)
        {
            this.minEnemyLevel = minEnemyLevel-1;
            this.maxEnemyLevel = maxEnemyLevel;
            this.enemyCount = enemyCount;
            this.spawnInterval = spawnInterval;
            this.secondCounter = spawnInterval;
            this.enemyManager = new EnemyManager();
        }

        public void StartLevel()
        {
            started = true;
        }
        public void Update(GameTime gameTime)
        {
            spawn(gameTime);
            enemyManager.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            enemyManager.Draw(spriteBatch);
        }
        private void spawn(GameTime gameTime)
        {
            if(!MaxEnemiesSpawned)
            {
                secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
                Vector2 spawnPos;
                if (secondCounter >= spawnInterval)
                {
                    spawnPos = randomSpawnPosition();
                    switch (random.Next(minEnemyLevel, maxEnemyLevel))
                    {
                        case 0:
                            enemyManager.spawn(new Enemy(spawnPos));
                            break;
                        case 1:
                            enemyManager.spawn(new ShootingEnemy(spawnPos));
                            break;
                        case 2:
                            enemyManager.spawn(new BossEnemy(spawnPos));
                            break;
                        default:
                            break;
                    }
                    secondCounter = 0;
                    enemiesSpawned++;
                }
            }else
            {
                if(enemyManager.ObjectList.Count==0)
                {
                    LevelOver = true;
                }
            }
        }
        private Vector2 randomSpawnPosition()
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
