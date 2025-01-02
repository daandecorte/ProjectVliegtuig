using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProjectVliegtuig.Managers
{
    public abstract class Manager<T>: IManager where T:IGameObject
    {
        public List<T> ObjectList { get; set; }
        List<IGameObject> IManager.ObjectList
        {
            get => ObjectList.Cast<IGameObject>().ToList();
            set => ObjectList = value.Cast<T>().ToList();
        }
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
