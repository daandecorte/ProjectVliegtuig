using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Display
{
    public class DisplayManager
    {
        public int width = 1920;
        public int height = 1080;
		private GraphicsDeviceManager graphicsDeviceManager;
        private static DisplayManager displayManager;
        private DisplayManager(GraphicsDeviceManager graphics)
        {
            graphicsDeviceManager = graphics;
        }
        public static void init(GraphicsDeviceManager graphics)
        {
            if(displayManager == null)
            {
                displayManager = new DisplayManager(graphics);
            }
        }
        public static DisplayManager getDisplay()
        {
            if (displayManager != null)
            {
                return displayManager;
            }
            else return null;
        }

        public void Apply()
        {
            if(graphicsDeviceManager != null)
            {
                graphicsDeviceManager.IsFullScreen = false;
                graphicsDeviceManager.PreferredBackBufferWidth = width;
                graphicsDeviceManager.PreferredBackBufferHeight = height;
                graphicsDeviceManager.ApplyChanges();
            }
        }
	}
}
