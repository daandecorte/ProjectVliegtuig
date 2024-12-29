using ProjectVliegtuig.Interfaces;
using ProjectVliegtuig.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.LevelCreators
{
    public class CreatorLevel1 : ILevelCreator
    {
        public Level CreateLevel()
        {
            return new Level(1, 1, 3, 3);
        }
    }
}
