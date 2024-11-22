using Microsoft.Xna.Framework.Input;
using ProjectVliegtuig.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Input
{
    internal class KeyboardReader : IInputReader
    {
        public Vector2 ReadInput()
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;
            if(state.IsKeyDown(Keys.Z))
            {
                direction.Y = -1;
            }
            if (state.IsKeyDown(Keys.S))
            {
                direction.Y = 1;
            }
            if (state.IsKeyDown(Keys.D))
            {
                direction.X = 1;
            }
            if (state.IsKeyDown(Keys.Q))
            {
                direction.X = -1;
            }
            return direction;
        }
    }
}
