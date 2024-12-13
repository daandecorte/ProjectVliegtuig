using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Gameobjects
{
    public abstract class GameObject: IGameObject
    {
        public static GraphicsDevice graphicsDevice;
        public Texture2D texture;
        public Vector2 speed;
        public Vector2 position;
        public float rotation;
        public Vector2 size;
        public Vector2 origin;
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch s);
    }
}
