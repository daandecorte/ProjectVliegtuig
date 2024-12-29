using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectVliegtuig.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Display
{
    public class Button: IGameObject
    {
        public static SpriteFont font;
        public static Texture2D texture;

        private MouseState currentMouse;
        private MouseState previousMouse;
        private bool isHovering;

        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Color PenColor { get; set; }
        public Vector2 Position { get; set; }
        public Keys key { get; set; }
        public bool enabled = true;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
        }
        public string Text { get; set; }

        public Button()
        {
            PenColor = Color.Black;
        }
        public void Update(GameTime gameTime)
        {
            if (enabled)
            {
                previousMouse = currentMouse;
                currentMouse = Mouse.GetState();
                var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);
                isHovering = false;
                if (mouseRectangle.Intersects(Rectangle))
                {
                    isHovering = true;
                    if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        Click?.Invoke(this, new EventArgs());
                    }
                }
                if(Keyboard.GetState().IsKeyDown(key))
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
        public void Draw(SpriteBatch s)
        {
            Color color = Color.White;
            if (isHovering || !enabled) color = Color.Gray;
            s.Draw(texture, Rectangle, color);
            if(!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2);

                s.DrawString(font, Text, new Vector2(x, y), PenColor);
            }
        }

    }
}
