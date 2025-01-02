using ProjectVliegtuig.Interfaces;
using ProjectVliegtuig.LevelFactory;

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
