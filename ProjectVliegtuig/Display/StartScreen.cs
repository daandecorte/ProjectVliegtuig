using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVliegtuig.LevelCreators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Display
{
    public class StartScreen
    {
        public Button currentLevelButton = new Button() {Position=new Vector2(DisplayManager.getDisplay().width/2-100, DisplayManager.getDisplay().height/2-250) };
        public Button replayButton = new Button() { Position = new Vector2(DisplayManager.getDisplay().width/2 - 100, DisplayManager.getDisplay().height/2-100), enabled=false };
        public Button bossLevelButton = new Button() { Text="Boss Level\n   [locked]", Position=new Vector2(DisplayManager.getDisplay().width/2 - 100, DisplayManager.getDisplay().height/2+50), enabled=false};
        public Button exitButton = new Button() { Text="Exit", Position=new Vector2(DisplayManager.getDisplay().width/2 - 100, DisplayManager.getDisplay().height/2+200) };
        private List<Button> buttons;

        private static StartScreen startScreen;
        private StartScreen()
        {
            buttons = new List<Button>() { currentLevelButton, replayButton, exitButton, bossLevelButton };
        }
        public static void Init()
        {
            if (startScreen == null) startScreen = new StartScreen();
        }
        public static StartScreen GetStartScreen()
        {
            if (startScreen != null)
            {
                return startScreen;
            }
            else return null;
        }

        public static void Draw(SpriteBatch s)
        {
            foreach (var button in startScreen.buttons)
            {
                button.Draw(s);
            }
        }
        public static void Update()
        {
            startScreen.currentLevelButton.Text = $"Play  Level  {Game1.currentLevel}\n      [enter]";
            startScreen.replayButton.Text = $"Replay  Level {Game1.lastLevel}";
            if (Game1.currentLevel > 1) startScreen.replayButton.enabled = true;
            if (new LevelCreatorFactory().GetLevelCreator(Game1.currentLevel) is CreatorBossLevel)
            {
                startScreen.currentLevelButton.Text += "\n      (Boss)";
                startScreen.bossLevelButton.enabled = true;
                startScreen.bossLevelButton.Text = "Boss Level\n [unlocked]";
            }
            foreach (var button in startScreen.buttons)
            {
                button.Update();
            }
        }
    }
}
