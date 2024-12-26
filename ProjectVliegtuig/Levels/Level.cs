﻿using Microsoft.Xna.Framework;
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

        private int maxEnemyLevel;
        private int minEnemyLevel;
        private int spawnInterval;
        private static Random random = new Random();
        private double secondCounter;

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
            this.secondCounter = spawnInterval;

            EnemyManager.GetManager().ObjectList.Clear();
            BulletManager.GetManager().ObjectList.Clear();
            ExplosionManager.GetManager().ObjectList.Clear();
        }
        public static void Init()
        {
            EnemyManager.Init();
            BulletManager.Init();
            ExplosionManager.Init();
            managers = [
                EnemyManager.GetManager(),
                BulletManager.GetManager(),
                ExplosionManager.GetManager()
            ];
        }
        public void Update(GameTime gameTime)
        {
            Spawn(gameTime);
            foreach (var manager in managers)
            {
                manager.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, $"{enemyCount - enemiesSpawned + EnemyManager.GetManager().ObjectList.Count} Enemies Remaining", new Vector2(1680, 10), Color.Black, 0, new Vector2(0,0), 1.4f, SpriteEffects.None, 0);
            if (hasWon) spriteBatch.Draw(winscreen, new Rectangle(0, 0, winscreen.Width, winscreen.Height), Color.White);
            foreach (var manager in managers)
            {
                manager.Draw(spriteBatch);
            }
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
                            EnemyManager.GetManager().Spawn(new Enemy(spawnPos));
                            break;
                        case 1:
                            EnemyManager.GetManager().Spawn(new ShootingEnemy(spawnPos));
                            break;
                        case 2:
                            EnemyManager.GetManager().Spawn(new BossEnemy(spawnPos));
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
                if(EnemyManager.GetManager().ObjectList.Count==0)
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
