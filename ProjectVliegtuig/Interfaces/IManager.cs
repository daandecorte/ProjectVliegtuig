using System.Collections.Generic;

namespace ProjectVliegtuig.Interfaces
{
    internal interface IManager : IGameObject
    {
        List<IGameObject> ObjectList { get; set; }
    }
}
