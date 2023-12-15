using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drawing
{
    public partial class Form1 : Form
    {
        Graphics g;
        int x = -1;
        int y = -1;
        bool moving=false;
        Pen pen;
        Bitmap saveImage;
        public Form1()
        {
            InitializeComponent();
            g=panel1.CreateGraphics();
            g.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen=new Pen(Color.Black,(float)numericUpDown1.Value);
            pen.StartCap=pen.EndCap=System.Drawing.Drawing2D.LineCap.Round;
            saveImage=new Bitmap(panel1.Width, panel1.Height);
            //panel1.BackgroundImage=saveImage;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox p=(PictureBox)sender;
            pen.Color=p.BackColor;
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            moving=true;
            x=e.X;
            y=e.Y;
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving==true&&x!=-1&&y!=-1)
            {
                g.DrawLine(pen, new Point(x, y), e.Location);
                x=e.X; 
                y=e.Y;
            }
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
            x=-1;
            y=-1;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            ColorDialog cd=new ColorDialog();
            pen.Width=(float)numericUpDown1.Value;
            if(cd.ShowDialog() == DialogResult.OK)
            {
                pen.Color=cd.Color;
                pen.Width=5;
                pictureBox7.BackColor=cd.Color;
            }
           
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            pen.Color= Color.White;
            pen.Width=10;
            pictureBox7.BackColor=Color.Transparent;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width=(float)numericUpDown1.Value;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog=new SaveFileDialog();
            saveFileDialog.Filter="Png Files (*jpg) | *.jpg";
            //saveFileDialog.DefaultExt="png";
            //saveFileDialog.AddExtension=true;
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //saveImage.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                Bitmap bitmap = saveImage.Clone(new Rectangle(0, 0, panel1.Width, panel1.Height), saveImage.PixelFormat);
                bitmap.Save(saveFileDialog.FileName,ImageFormat.Jpeg);
            }
        }
    }
}
