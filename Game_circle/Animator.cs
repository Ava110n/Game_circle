using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_circle
{
    internal class Animator
    {
        public Circle _circle;
        private int dx, dy;
        private Size containerSize;
        Thread? t = null;

        public Animator(Circle circle, Size containerSize)
        {
            this._circle = circle;
            this.containerSize = containerSize;
        }

        public void Move()
        {
            _circle.X += dx;
            _circle.Y += dy;
        }

        public bool isAlive()
        {
            //лево-право
            if(_circle.X - _circle.R < 0 || _circle.X + _circle.R > containerSize.Width)
            {
                return false;
            }
            //верх-низ
            if (_circle.Y - _circle.R < 0 || _circle.Y + _circle.R > containerSize.Height)
            {
                return false;
            }
            return true;
        }

    }
}
