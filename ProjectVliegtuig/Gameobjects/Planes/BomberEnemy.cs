using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Gameobjects.AmmunitionTypes;
using ProjectVliegtuig.Managers;

namespace ProjectVliegtuig.Gameobjects.Planes
{
    internal class BomberEnemy : Enemy
    {
        public static new Texture2D texture;
        protected override Texture2D _texture
        {
            get => texture;
        }
        public BomberEnemy(Vector2 position) : base(position, 5)
        {
            acceleration = 0.20f;
        }
        protected override void UpdateDirection(Vector2 destination)
        {
            destination.Y = 200;
            base.UpdateDirection(destination);
        }
        public override void Shoot(GameTime gameTime)
        {
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (secondCounter >= 2d)
            {
                AmmunitionManager.AmmunitionList.Add(new Bomb(speed, position));
                secondCounter = 0;
            }
        }
    }
}
