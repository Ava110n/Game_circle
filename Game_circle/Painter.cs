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
            }
        }

        public Graphics G
		{
            get { return g; }
			set
			{
                //lock (locker)
                //{
                    g = value;
                    //ContainerSize = g.ClipBounds.Size.ToSize();
                    ContainerSize=g.VisibleClipBounds.Size.ToSize();
                    Rectangle p = new Rectangle(new Point(0, 0), containerSize);
                    bg = BufferedGraphicsManager.Current.Allocate(g, p);
                //}
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

        public void show()
        {
            Thread t = new Thread(new ThreadStart(moving));
            t.Start();
            //t.Abort();
        }

        public void moving()
		{
            while (true)
            {
                draw();
                int count = animators.Count;
                if (count > 0)
                {
                    foreach (var animator in animators.ToList())
                    {
                        animator.Move();
                        if (AsAlive(animator) == false)
                        {
                            try
                            {
                                //Thread.CurrentThread.Interrupt();
                                Thread.CurrentThread.Abort();
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
            
        }

        private bool AsAlive(Animator animator)
        {
            int x = animator._circle.X;
            int y = animator._circle.Y;
            int r = animator._circle.R;
            if (x+r<0 || x - r > containerSize.Width)
            {
                animators.Remove(animator);
                return false;
            }
            if (y + r < 0 || y - r > containerSize.Height)
            {
                animators.Remove(animator);
                return false;
            }
            return true;
        }


        public void draw()
        {
           
            //var count = rects.Count + animators.Count;
            lock(locker)
            {
                bg.Graphics.Clear(Color.White);
                foreach (var animator in animators.ToList())
                {
                    int r = animator._circle.R;
                    bg.Graphics.DrawEllipse(pen, animator._circle.X - r, animator._circle.Y - r, 2 * r, 2 * r);
                }
                foreach (var rect in rects.ToList())
                {
                    bg.Graphics.DrawRectangle(pen, rect.X - rect.Side / 2, rect.Y - rect.Side / 2, rect.Side, rect.Side);

                }
                bg.Render();
            }
            //Thread.Sleep(100);
        }


    }
}
