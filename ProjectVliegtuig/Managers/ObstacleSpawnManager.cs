﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Display;
using ProjectVliegtuig.Gameobjects;
using ProjectVliegtuig.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Managers
{
    public static class ObstacleSpawnManager//: Manager<Obstacle>
    {
        public static List<Obstacle> obstacles { get; set; } = new List<Obstacle>();
        private static double secondCounter = 0;
        private static Random random = new Random();
        public static void Draw(SpriteBatch s)
        {
            foreach (var obstacle in obstacles)
            {
                obstacle.Draw(s);
            }
        }
        public static void Update(GameTime gameTime)
        {
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (secondCounter >= 2d)
            {
                secondCounter = 0;
                SpawnObstacle();
            }

            for (int i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].Update(gameTime);
                foreach(var bullet in BulletManager.BulletList)
                {
                    try {
                        if (obstacles[i].Collide(bullet))
                        {
                            obstacles.RemoveAt(i);
                            i--;
                        }
                    }
                    catch(Exception e)
                    {}
                }
            }
        }
        private static void SpawnObstacle()
        {
            obstacles.Add(
                new Obstacle(
                    new Vector2(
                        random.Next(0, DisplayManager.getDisplay().width - 30),
                        random.Next(0, DisplayManager.getDisplay().height - 30)
                    )
                )
            );
        }
    }
}
