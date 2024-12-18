using ProjectVliegtuig.Interfaces;
using ProjectVliegtuig.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.LevelCreators
{
    internal class CreatorLevel2 : ILevelCreator
    {
        public Level CreateLevel()
        {
            return new Level(1, 2, 3, 3);
        }
    }
}
