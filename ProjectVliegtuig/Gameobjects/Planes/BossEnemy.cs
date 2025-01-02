using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.Gameobjects.AmmunitionTypes;
using ProjectVliegtuig.Managers;
using System;

namespace ProjectVliegtuig.Gameobjects.Planes
{
    public class BossEnemy : Enemy
    {
        public static new Texture2D texture;
        private bool left = true;
        protected override Texture2D _texture
        {
            get => texture;
        }
        public BossEnemy(Vector2 position) : base(position, 10)
        {
            acceleration = 0.20f;
            scale = 1.4f;
        }
        public override void Shoot(GameTime gameTime)
        {
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (secondCounter >= 0.15d)
            {
                Vector2 d = new Vector2((float)Math.Sin(rotation), -(float)Math.Cos(rotation));
                if (left)
                {
                    AmmunitionManager.AmmunitionList.Add(new Bullet(d, new Vector2(position.X - d.Y * 50, position.Y + d.X * 50)));
                    left = !left;
                }
                else
                {
                    AmmunitionManager.AmmunitionList.Add(new Bullet(d, new Vector2(position.X + d.Y * 50, position.Y - d.X * 50)));
                    left = !left;
                }
                secondCounter = 0;
            }
        }
    }
}
