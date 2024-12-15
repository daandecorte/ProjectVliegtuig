using ProjectVliegtuig.Gameobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Levels
{
    public abstract class Level
    {
        public abstract List<Enemy> Enemies { get; set; }
        public abstract int EnemyCount { get; set; }
        public abstract int SpawnInterval { get; set; }
        public abstract void StartLevel();
    }
}
