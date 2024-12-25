using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Animation;
using ProjectVliegtuig.Gameobjects;
using ProjectVliegtuig.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Effects
{
    internal class Explosion : GameObject
    {
        public static Texture2D texture;
        private Animatie animatie;
        private double aliveTime = 0;
        private int fps = 30;
        public bool AnimationDone { get => aliveTime >= 36d/(double)fps; }

        public Explosion(Vector2 position)
        {
            this.position = position;
            animatie = new Animatie() { fps=fps};
            animatie.GetFramesFromTextureProperties(texture.Width, texture.Height, 6, 6);
            origin = new Vector2(texture.Width / 12, texture.Height / 12);
        }
        public override void Draw(SpriteBatch s)
        {
            s.Draw(texture, position, animatie.CurrentFrame.SourceRectangle, Color.White, 0, origin, 1, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {
            aliveTime += gameTime.ElapsedGameTime.TotalSeconds;
            animatie.Update(gameTime);
        }
    }
}
