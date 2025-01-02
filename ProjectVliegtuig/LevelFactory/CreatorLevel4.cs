using ProjectVliegtuig.Interfaces;
using ProjectVliegtuig.LevelFactory;

namespace ProjectVliegtuig.LevelCreators
{
    internal class CreatorLevel4 : ILevelCreator
    {
        public Level CreateLevel()
        {
            return new Level(2, 2, 4, 2);
        }
    }
}
