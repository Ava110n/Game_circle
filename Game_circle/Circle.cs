using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_circle
{
    internal class Circle
    {
        private int x, y;
        private int r;

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public int R { get { return r; } set { r = value; } }

        public Circle(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
