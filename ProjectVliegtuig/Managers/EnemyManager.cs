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
        private double secondCounter=10d;
        private Texture2D texture;
        public EnemyManager(Texture2D texture)
        {
            this.texture = texture;
        }
        public override void Update(GameTime gameTime)
        {
            spawn(gameTime);
            for (int i = 0; i < ObjectList.Count; i++)
            {
                ObjectList[i].Update(gameTime);
                for (int j = 0; j<BulletManager.BulletList.Count; j++)
                {
                    try
                    {
                        if(ObjectList[i].Collide(BulletManager.BulletList[j]))
                        {
                            BulletManager.BulletList.Remove(BulletManager.BulletList[j]);
                            j--;
                        }
                    }
                    catch (Exception e)
                    { }
                }
                if (ObjectList[i].health<=0)
                {
                    ObjectList.RemoveAt(i);
                }
            }
        }
        private void spawn(GameTime gameTime)
        {
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 spawnPos = new Vector2();
            if(secondCounter>=5d)
            {
                switch(random.Next(0, 4))
                {
                    case 0:
                        spawnPos = new Vector2(random.Next(0, DisplayManager.Graphics.PreferredBackBufferWidth), -100);
                        break;
                    case 1:
                        spawnPos = new Vector2(random.Next(0, DisplayManager.Graphics.PreferredBackBufferWidth), DisplayManager.Graphics.PreferredBackBufferHeight+100);
                        break;
                    case 2:
                        spawnPos = new Vector2(-100, random.Next(0, DisplayManager.Graphics.PreferredBackBufferHeight));
                        break;
                    case 3:
                        spawnPos = new Vector2(DisplayManager.Graphics.PreferredBackBufferWidth+100, random.Next(0, DisplayManager.Graphics.PreferredBackBufferHeight));
                        break;
                }
                ObjectList.Add(new Enemy(texture, spawnPos));
                secondCounter = 0;
            }
        }
    }
}
