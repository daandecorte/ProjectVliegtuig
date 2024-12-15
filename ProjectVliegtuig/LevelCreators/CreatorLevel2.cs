using ProjectVliegtuig.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.LevelCreators
{
    internal class CreatorLevel2 : LevelCreator
    {
        public override Level CreateLevel()
        {
            return new Level2();
        }
    }
}
