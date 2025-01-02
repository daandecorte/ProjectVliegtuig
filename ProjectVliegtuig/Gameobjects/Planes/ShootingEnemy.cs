using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Gameobjects.AmmunitionTypes;
using ProjectVliegtuig.Managers;

namespace ProjectVliegtuig.Gameobjects.Planes
{
    internal class ShootingEnemy : Enemy
    {
        public static new Texture2D texture;
        protected override Texture2D _texture
        {
            get => texture;
        }
        public ShootingEnemy(Vector2 position) : base(position)
        { }
        public override void Shoot(GameTime gameTime)
        {
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (secondCounter >= 1d)
            {
                AmmunitionManager.AmmunitionList.Add(new Bullet(speed, position));
                secondCounter = 0;
            }
        }
    }
}
