using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace COL.Helpers
{
    public static class CollisionHelper
    {
        public static bool DetectTopOf(this Rectangle r1, Rectangle r2)
        {
            return (
                r1.Bottom >= r2.Top &&
                r1.Right >= r2.Left &&
                r1.Left <= r2.Right &&
                r1.Bottom <= r2.Top + 10
                );
        }
        public static bool DetectBottomOf(this Rectangle r1, Rectangle r2)
        {
            return (
                r1.Top <= r2.Bottom + 4 &&
                r1.Right >= r2.Left &&
                r1.Left <= r2.Right &&
                r1.Top >= r2.Bottom
                );
        }
        public static bool DetectLeftOf(this Rectangle r1, Rectangle r2)
        {
            return (
                r1.Right >= r2.Left &&
                r1.Right <= r2.Left + r2.Width / 2 &&
                r1.Bottom > r2.Top + 10 &&
                r1.Top < r2.Bottom
                );
        }
        public static bool DetectRightOf(this Rectangle r1, Rectangle r2)
        {
            return (
                r1.Left <= r2.Right &&
                r1.Left >= r2.Right - r2.Width / 2 &&
                r1.Bottom > r2.Top + 10 &&
                r1.Top < r2.Bottom
                );
        }
    }
}
