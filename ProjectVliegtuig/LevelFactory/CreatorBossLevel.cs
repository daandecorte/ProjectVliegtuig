using ProjectVliegtuig.Interfaces;
using ProjectVliegtuig.LevelFactory;

namespace ProjectVliegtuig.LevelCreators
{
    internal class CreatorBossLevel : ILevelCreator
    {
        public Level CreateLevel()
        {
            return new Level(4, 4, 1, 1);
        }
    }
}
