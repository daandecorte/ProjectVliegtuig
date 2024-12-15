using ProjectVliegtuig.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.LevelCreators
{
    public class CreatorLevel1 : LevelCreator
    {
        public override Level CreateLevel()
        {
            return new Level1();
        }
    }
}
