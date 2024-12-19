using ProjectVliegtuig.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.LevelCreators
{
    internal class LevelCreatorFactory
    {
        public ILevelCreator GetLevelCreator(int level)
        {
            switch(level)
            {
                case 1:
                    return new CreatorLevel1();
                    break;
                case 2:
                    return new CreatorLevel2();
                    break;
                case 3:
                    return new CreatorBossLevel();
                    break;
                default:
                    return null;
                    break;
            }
        }
    }
}
