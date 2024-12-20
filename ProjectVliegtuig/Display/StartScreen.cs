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
        public StartScreen()
        {
            buttons = new List<Button>() { currentLevelButton, replayButton, exitButton, bossLevelButton };
        }

        public void Draw(SpriteBatch s)
        {
            foreach (var button in buttons)
            {
                button.Draw(s);
            }
        }
        public void Update()
        {
            currentLevelButton.Text = $"Play  Level  {Game1.currentLevel}\n      [enter]";
            replayButton.Text = $"Replay  Level {Game1.lastLevel}";
            if (Game1.currentLevel > 1) replayButton.enabled = true;
            if (new LevelCreatorFactory().GetLevelCreator(Game1.currentLevel) is CreatorBossLevel)
            {
                currentLevelButton.Text += "\n      (Boss)";
                bossLevelButton.enabled = true;
                bossLevelButton.Text = "Boss Level\n [unlocked]";
            }
            foreach (var button in buttons)
            {
                button.Update();
            }
        }
    }
}
