using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Interfaces
{
    internal interface IManager : IGameObject
    {
        List<IGameObject> ObjectList { get; set; }
    }
}
