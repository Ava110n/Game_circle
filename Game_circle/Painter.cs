using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_circle
{
    internal class Painter
    {
        private List<Square> rects = new List<Square>();
        private List<Animator> animators = new List<Animator>();
        Thread? t = null;
        private Graphics g;
        private BufferedGraphics bg;
        object locker = new object();
        Pen pen = new Pen(Color.Black);
        private Size containerSize;
        private Panel mainPanel;
        
        public Size ContainerSize
        {
            get { return containerSize; }
            set {
                //Rectangle p = new Rectangle(0, 0, Convert.ToInt32(g.VisibleClipBounds.Width), Convert.ToInt32(g.VisibleClipBounds.Height));
                containerSize = value;
                Rectangle p = new Rectangle(new Point(0, 0), containerSize);
                bg = BufferedGraphicsManager.Current.Allocate(g, p);        
            }
        }

        public Painter(Graphics g)
        {
            this.g = g;
        }

        public void addRectangle(MouseEventArgs e)
        {
            Square rect = new Square(e.X, e.Y);
            rects.Add(rect);

            g.DrawRectangle(pen, rect.X - rect.Side / 2, rect.Y - rect.Side / 2, rect.Side, rect.Side);
        }

        public void circlePaint()
        {
            Circle c = new Circle(0,0);
        }

        public void draw()
        {
            //var count = rects.Count + animators.Count;
            bg.Graphics.Clear(Color.White);
            foreach(var animator in animators)
            {
                int r = animator._circle.R;
                bg.Graphics.DrawEllipse(pen, animator._circle.X-r, animator._circle.Y-r, 2*r, 2*r);
            }
            foreach(var rect in rects)
            {
                bg.Graphics.DrawRectangle(pen, rect.X - rect.Side / 2, rect.Y - rect.Side / 2, rect.Side, rect.Side);

            }
            bg.Render();
        }


    }
}
