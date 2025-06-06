using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp11
{
    public partial class Captcha : Form
    {
        public Captcha()
        {
            InitializeComponent();
            pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
        }
        public string Role { get; set; }
        public int UserId { get; set; }
        private string text = String.Empty;

        private Bitmap CreateImage(int Width, int Height)
        {
            Random rnd = new Random();
            Bitmap result = new Bitmap(Width, Height);
            int Xpos = 10;
            int Ypos = 10;
            Brush[] colors =
             { Brushes.Black, Brushes.Red, Brushes.RoyalBlue, Brushes.Green,
Brushes.Yellow, Brushes.White, Brushes.Tomato, Brushes.Sienna,
Brushes.Pink };
            Pen[] colorpens = {Pens.Black, Pens.Red, Pens.RoyalBlue, Pens.Green,
Pens.Yellow, Pens.White, Pens.Tomato, Pens.Sienna, Pens.Pink };
            FontStyle[] fontstyle = {FontStyle.Bold, FontStyle.Italic, FontStyle.Regular,
FontStyle.Strikeout, FontStyle.Underline};
            Int16[] rotate = { 1, -1, 2, -2, 3, -3, 4, -4, 5, -5, 6, -6 };
            Graphics g = Graphics.FromImage((System.Drawing.Image)result);
            g.Clear(Color.Gray);
            g.RotateTransform(rnd.Next(rotate.Length));
            text = String.Empty;
            string ALF = " 7890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];
            g.DrawString(text, new Font("Arial", 25, fontstyle[rnd.Next(fontstyle.Length)]),
            colors[rnd.Next(colors.Length)],
            new PointF(Xpos, Ypos));
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)],
            new Point(0, 0),
            new Point(Width - 1, Height - 1));
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)],
            new Point(0, Height - 1),
            new Point(Width - 1, 0));
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == this.text)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверная капча! Попробуйте снова.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
                textBox1.Clear();
                textBox1.Focus();
            }
        }

    }
}

