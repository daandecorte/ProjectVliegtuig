using ProjectVliegtuig.Interfaces;
using ProjectVliegtuig.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.LevelCreators
{
    internal class CreatorBossLevel : ILevelCreator
    {
        public Level CreateLevel()
        {
            return new Level(4, 4, 1, int.MaxValue);
        }
    }
}
