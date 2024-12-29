using ProjectVliegtuig.Interfaces;
using ProjectVliegtuig.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.LevelCreators
{
    internal class CreatorLevel5 : ILevelCreator
    {
        public Level CreateLevel()
        {
            return new Level(1, 3, 8, 1);
        }
    }
}
