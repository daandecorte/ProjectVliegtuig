using Microsoft.Xna.Framework;
using ProjectVliegtuig.Display;
using ProjectVliegtuig.Gameobjects.Abstracts;
using System.Collections.Generic;

namespace ProjectVliegtuig.Managers
{
    public class AmmunitionManager: Manager<Ammunition>
    {
        private static AmmunitionManager ammunitionManager;
        public static List<Ammunition> AmmunitionList { get => ammunitionManager.ObjectList; }

        private AmmunitionManager()
        {
            ObjectList = new List<Ammunition>();
        }
        public static AmmunitionManager Init()
        {
            if(ammunitionManager==null)
            {
                ammunitionManager = new AmmunitionManager();
            }
            ammunitionManager.ObjectList.Clear();
            return ammunitionManager;
        }
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < AmmunitionList.Count; i++)
            {
                AmmunitionList[i].Update(gameTime);
                if (AmmunitionList[i].position.X > DisplayManager.getDisplay().width || AmmunitionList[i].position.X < 0 || AmmunitionList[i].position.Y < 0 || AmmunitionList[i].position.Y > DisplayManager.getDisplay().height)
                {
                    AmmunitionList.RemoveAt(i);
                    i--;
                }
            }
        }


    }
}
