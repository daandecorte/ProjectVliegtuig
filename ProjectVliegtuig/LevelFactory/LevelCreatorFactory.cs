﻿using ProjectVliegtuig.Interfaces;

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
                case 2:
                    return new CreatorLevel2();
                case 3:
                    return new CreatorLevel3();
                case 4:
                    return new CreatorLevel4();
                case 5:
                    return new CreatorLevel5();
                case 6:
                    return new CreatorBossLevel();
                default:
                    return null;
            }
        }
    }
}
