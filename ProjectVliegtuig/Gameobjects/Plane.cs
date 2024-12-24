using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Gameobjects
{
    public abstract class Plane: GameObject
    {
        protected abstract void Shoot(GameTime gameTime);
    }
}
