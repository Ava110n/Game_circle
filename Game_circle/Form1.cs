namespace Game_circle
{
    public partial class Form1 : Form
    {
        private bool onClick = false;
        Painter painter;

        public Form1()
        {
            InitializeComponent();
            painter = new Painter(mainPanel.CreateGraphics());
        }

        private void button_Click(object sender, EventArgs e)
        {
            onClick = true;
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            painter.draw();
        }

        private void mainPanel_Click(object sender, EventArgs e)
        {
           
        }

        private void mainPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (onClick) painter.addRectangle(e);
            onClick = false;
            painter.moving();
        }

		private void Form1_Load(object sender, EventArgs e)
		{
            
		}
	}
}