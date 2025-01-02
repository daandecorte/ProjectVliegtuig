using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Animation;
using ProjectVliegtuig.Gameobjects.Abstracts;

namespace ProjectVliegtuig.Effects
{
    internal class Explosion : GameObject
    {
        public static Texture2D texture;
        private Animatie animatie;
        private double aliveTime = 0;
        private int fps = 10;
        public bool AnimationDone { get => aliveTime >= 7d/(double)fps; }
        protected override Texture2D _texture
        {
            get => texture;
        }

        public Explosion(Vector2 position)
        {
            this.position = position;
            animatie = new Animatie() { fps = this.fps };
            animatie.GetFramesFromTexture(_texture.Width, _texture.Height, 7, 1);
            origin = new Vector2(_texture.Width / 14, _texture.Height / 2);
        }
        public override void Draw(SpriteBatch s)
        {
            s.Draw(_texture, position, animatie.CurrentFrame.SourceRectangle, Color.White, 0, origin, 1, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {
            aliveTime += gameTime.ElapsedGameTime.TotalSeconds;
            animatie.Update(gameTime);
        }
    }
}
