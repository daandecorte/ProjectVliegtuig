using Microsoft.Xna.Framework;
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
        public override void Update(GameTime gameTime)
        {
            spawn(gameTime);
            base.Update(gameTime);
        }
        private void spawn(GameTime gameTime)
        {
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if(secondCounter>=10d)
            {
                secondCounter = 0;
                ObjectList.Add(new Enemy(new Vector2(random.Next(0, DisplayManager.Graphics.PreferredBackBufferWidth), random.Next(0, DisplayManager.Graphics.PreferredBackBufferHeight))));
            }
        }
    }
}
