using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Animation
{
    internal class Animatie
    {
        public AnimationFrame CurrentFrame { get; set; }
        public int fps { get; set; }

        public List<AnimationFrame> frames;
        private int counter = 0;
        private double secondCounter = 0;
        public Animatie()
        {
            frames = new List<AnimationFrame>();
        }
        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];

            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;

            if (secondCounter >= 1d / fps)
            {
                counter++;
                secondCounter = 0;
            }
            if (counter >= frames.Count)
            {
                counter = 0;
            }
        }
        public void GetFramesFromTexture(int width, int height, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            int widthOfFrame = width / numberOfWidthSprites;
            int heightOfFrame = height / numberOfHeightSprites;
            for (int y = 0; y <= height - heightOfFrame; y += heightOfFrame)
            {
                for (int x = 0; x <= width - widthOfFrame; x += widthOfFrame)
                {
                    frames.Add(new AnimationFrame(
                   new Rectangle(x, y, widthOfFrame, heightOfFrame)));
                }
            }
            CurrentFrame = frames[counter];
        }
    }
}
