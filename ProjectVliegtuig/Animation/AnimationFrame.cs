﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVliegtuig.Animation
{
    public class AnimationFrame
    {
        public Rectangle SourceRectangle { get; set; }
        public AnimationFrame(Rectangle rectangle)
        {
            SourceRectangle = rectangle;
        }
    }
}
