using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_circle
{
    internal class Square
    {
        private int x, y;
        private int side;
        private int id;

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public int Side { get { return side; } set { side = value; } }
        public int Id { get { return id; } set { id = value; } }

        public Square(int x, int y, int side = 20)
        {
            this.x = x;
            this.y = y;
            this.side = side;
        }

    }
}
