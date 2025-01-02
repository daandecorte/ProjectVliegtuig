using ProjectVliegtuig.Interfaces;
using ProjectVliegtuig.LevelFactory;

namespace ProjectVliegtuig.LevelCreators
{
    internal class CreatorLevel2 : ILevelCreator
    {
        public Level CreateLevel()
        {
            return new Level(1, 2, 4, 3);
        }
    }
}
