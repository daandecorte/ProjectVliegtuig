using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Display
{
    public class DisplayManager
    {
        public readonly int width = 1920;
        public readonly int height = 1080;
        public readonly Rectangle fullScreenRectangle;
		private GraphicsDeviceManager graphicsDeviceManager;
        private static DisplayManager displayManager;
        private DisplayManager(GraphicsDeviceManager graphics)
        {
            graphicsDeviceManager = graphics;
            fullScreenRectangle = new Rectangle(0, 0, width, height);
        }
        public static void Init(GraphicsDeviceManager graphics)
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
