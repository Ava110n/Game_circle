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

        public Graphics G
		{
            get { return g; }
			set
			{
                g = value;
                containerSize = g.ClipBounds.Size.ToSize();
                Rectangle p = new Rectangle(new Point(0, 0), containerSize);
                bg = BufferedGraphicsManager.Current.Allocate(g, p);
            }
		}

        public Painter(Graphics g)
        {
            this.G = g;
            //bg = BufferedGraphicsManager.Current.Allocate(g, p);
        }

        public void addRectangle(MouseEventArgs e)
        {
            Square rect = new Square(e.X, e.Y);
            rects.Add(rect);

            //g.DrawRectangle(pen, rect.X - rect.Side / 2, rect.Y - rect.Side / 2, rect.Side, rect.Side);
            circlePaint(e);
        }

        public void circlePaint(MouseEventArgs e)
        {
            Circle c = new Circle(e.X,e.Y);
            //g.DrawEllipse(pen, c.X- c.R, c.Y- c.R, c.R*2, c.R*2);
            Animator anim = new Animator(c, ContainerSize);
            animators.Add(anim);
        }

        public void moving()
		{
            while (true)
            {
                draw();
                foreach (var animator in animators)
                {
                    animator.Move();
                }
            }
        }


        public void draw()
        {
            bg.Graphics.Clear(Color.White);
            //var count = rects.Count + animators.Count;
            if (bg != null)
            {
                bg.Graphics.Clear(Color.White);
                foreach (var animator in animators)
                {
                    int r = animator._circle.R;
                    bg.Graphics.DrawEllipse(pen, animator._circle.X - r, animator._circle.Y - r, 2 * r, 2 * r);
                }
                foreach (var rect in rects)
                {
                    bg.Graphics.DrawRectangle(pen, rect.X - rect.Side / 2, rect.Y - rect.Side / 2, rect.Side, rect.Side);

                }
                bg.Render();
            }
        }


    }
}
