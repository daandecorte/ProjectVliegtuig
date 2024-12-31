using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectVliegtuig.Gameobjects;
using ProjectVliegtuig.Interfaces;
using ProjectVliegtuig.LevelCreators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Display
{
    public class StartScreen: IGameObject
    {
        public static SpriteFont Font;
        public static Texture2D gameOverScreen;
        public static Texture2D pauzeScreen;
        public Button currentLevelButton = new Button() { key = Keys.Enter, Position = new Vector2(DisplayManager.getDisplay().width / 2 - 150, DisplayManager.getDisplay().height / 2 - 350) };
        public Button replayButton = new Button() { key = Keys.R, Position = new Vector2(DisplayManager.getDisplay().width / 2 - 150, DisplayManager.getDisplay().height / 2 - 150), enabled = false };
        public Button bossLevelButton = new Button() { Text = "Boss Level\n  [locked]", Position = new Vector2(DisplayManager.getDisplay().width / 2 - 150, DisplayManager.getDisplay().height / 2 + 50), enabled = false };
        public Button exitButton = new Button() { key = Keys.Escape, Text = " Exit\n[Esc]", Position = new Vector2(DisplayManager.getDisplay().width / 2 - 150, DisplayManager.getDisplay().height / 2 + 250) };
        private List<Button> buttons;

        private static StartScreen startScreen;
        private StartScreen()
        {
            buttons = new List<Button>() { currentLevelButton, replayButton, exitButton, bossLevelButton };
        }
        public static void Init()
        {
            if (startScreen == null)
            {
                startScreen = new StartScreen();
            }
        }
        public static StartScreen GetStartScreen()
        {
            if (startScreen != null)
            {
                return startScreen;
            }
            else return null;
        }
        public void Draw(SpriteBatch s)
        {
            if (Player.Get().health <= 0)
            {
                s.Draw(gameOverScreen, DisplayManager.getDisplay().fullScreenRectangle, Color.White);
            }
            else
            {
                s.Draw(pauzeScreen, DisplayManager.getDisplay().fullScreenRectangle, Color.White);
            }
            foreach (var button in startScreen.buttons)
            {
                button.Draw(s);
            }
            ShowInfo(s);
        }
        public void Update(GameTime gameTime)
        {
            startScreen.currentLevelButton.Text = $"Play  Level  {Game1.currentLevel}\n     [Enter]";
            startScreen.replayButton.Text = $"Replay  Level {Game1.lastLevel}\n          [R]";
            if (Game1.currentLevel > 1) startScreen.replayButton.enabled = true;
            if (new LevelCreatorFactory().GetLevelCreator(Game1.currentLevel) is CreatorBossLevel)
            {
                startScreen.currentLevelButton.Text += "\n     (Boss)";
                startScreen.bossLevelButton.enabled = true;
                startScreen.bossLevelButton.Text = "Boss Level\n [unlocked]";
            }
            foreach (var button in startScreen.buttons)
            {
                button.Update(gameTime);
            }
        }
        private static void ShowInfo(SpriteBatch s) 
        {
            s.DrawString(Font, "Controls:", new Vector2(10, 10), Color.Black, 0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
            s.DrawString(Font, "Move: 'ZQSD' / Arrow keys", new Vector2(10, 50), Color.Black, 0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
            s.DrawString(Font, "Shoot: 'Space'", new Vector2(10, 90), Color.Black, 0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
            s.DrawString(Font, "Pause: 'P'", new Vector2(10, 130), Color.Black, 0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
        }
    }
}
