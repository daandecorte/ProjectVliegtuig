using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Managers
{
    public abstract class Manager<T>: IGameObject where T:IGameObject
    {
        public List<T> ObjectList { get; set; }

        public virtual void Update(GameTime gameTime)
        {
            foreach(var obj in ObjectList)
            {
                obj.Update(gameTime);
            }
        }
        public virtual void Draw(SpriteBatch s)
        {
            foreach(var obj in ObjectList)
            {
                obj.Draw(s);
            }
        }
    }
}
