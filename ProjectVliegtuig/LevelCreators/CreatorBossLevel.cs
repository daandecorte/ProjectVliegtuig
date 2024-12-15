using ProjectVliegtuig.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.LevelCreators
{
    internal class CreatorBossLevel : LevelCreator
    {
        public override Level CreateLevel()
        {
            return new BossLevel();
        }
    }
}
