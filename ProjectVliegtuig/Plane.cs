using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectVliegtuig.Animation;
using ProjectVliegtuig.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig
{
    internal class Plane : IGameObject
    {
        private Texture2D texture;
        private Animatie animatie;
        private int x=200;
        private int y =200;
        public Plane(Texture2D texture)
        {
            this.texture = texture;
            animatie = new Animatie();
            animatie.GetFramesFromTextureProperties(texture.Width, texture.Height, 6, 1);

            //for (int i = 0; i < 6; i++)
            //{
            //    animatie.AddFrame(new AnimationFrame(new Rectangle(i * 100, 0, 100, 100)));
            //}
        }
        public void Draw(SpriteBatch s)
        {
            s.Draw(texture, new Vector2(x,y) ,animatie.CurrentFrame.SourceRectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            animatie.Update(gameTime);
            if(Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                y--;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S))
            {
                y++;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                x++;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                x--;
            }
        }
    }
}
