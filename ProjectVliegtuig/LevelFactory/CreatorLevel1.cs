using ProjectVliegtuig.Interfaces;
using ProjectVliegtuig.LevelFactory;

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
