using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.Properties;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        bool penOnOff;
        Color penColor;
        int penSize, oldX, oldY;
        Image []a = new Image[100];

        public Form1()
        {
            InitializeComponent();
            a[0] = pictureBox1.Image;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MainPanel.AutoScroll = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.Location = new Point(0, 0);
            penOnOff = false;
            penColor = Color.Red;
            penSize = 6;
            radioButton2.Checked = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBox1.Image);
            Bitmap newImg = new Bitmap(img.Width, img.Height);
            for (int x = 0; x < img.Width; x++)
                for (int y = 0; y < img.Height; y++)
                {
                    Color c = img.GetPixel(x, y);
                    newImg.SetPixel(newImg.Width - x - 1, y, c);
                }
            pictureBox1.Image = newImg;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBox1.Image);
            Bitmap newImg = new Bitmap(img.Width, img.Height);
            for (int y = 0; y < img.Height; y++)
                for (int x = 0; x < img.Width; x++)
                {
                    Color c = img.GetPixel(x, y);
                    newImg.SetPixel(x, newImg.Height - y - 1, c);
                }
            pictureBox1.Image = newImg;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBox1.Image);
            Bitmap newImg = new Bitmap(img.Height, img.Width);
            for (int x = 0; x < img.Width; x++)
                for (int y = 0; y < img.Height; y++)
                {
                    Color c = img.GetPixel(x, y);
                    newImg.SetPixel(img.Height - y -1, x, c);
                }
            pictureBox1.Image = newImg;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBox1.Image);
            Bitmap newImg = new Bitmap(img.Width, img.Height);
            for (int x = 0; x < img.Width; x++)
                for (int y = 0; y < img.Height; y++)
                {
                    Color c = img.GetPixel(x, y);
                    newImg.SetPixel(newImg.Width - x - 1, newImg.Height - y - 1, c);
                }
            pictureBox1.Image = newImg;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Bitmap img = new Bitmap(pictureBox1.Image);
            Bitmap newImg = new Bitmap(img.Height, img.Width);
            for (int x = 0; x < img.Width; x++)
                for (int y = 0; y < img.Height; y++)
                {
                    Color c = img.GetPixel(x, y);
                    newImg.SetPixel(y, img.Width - x - 1, c);
                }
            pictureBox1.Image = newImg;
            //a[1] = pictureBox1.Image;
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            System.IO.FileStream fs = new System.IO.FileStream(saveFileDialog1.FileName, System.IO.FileMode.Create);
            switch (saveFileDialog1.FilterIndex)
            {
                case 1:
                    pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case 2:
                    pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case 3:
                    pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
            }
            fs.Close();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void x128ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = WindowsFormsApplication1.Properties.Resources._128x128;
        }

        private void x480ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = WindowsFormsApplication1.Properties.Resources._640x480;
        }

        private void x1024ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = WindowsFormsApplication1.Properties.Resources._1024x1024;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(penOnOff == false)
            {
                penOnOff = true;
                button6.Text = ("ВЫКЛЮЧИТЬ");
            }else
            {
                penOnOff = false;
                button6.Text = ("ВКЛЮЧИТЬ");
            }
        }

        private void panelColor_Click(object sender, EventArgs e)
        {
            DialogResult dr = colorDialog1.ShowDialog();
            if(dr == System.Windows.Forms.DialogResult.OK)
            {
                penColor = colorDialog1.Color;
                panelColor.BackColor = penColor;
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            penSize = Convert.ToInt32(radioButton1.Text);
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            penSize = Convert.ToInt32(radioButton2.Text);
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            penSize = Convert.ToInt32(radioButton3.Text);
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            penSize = Convert.ToInt32(radioButton4.Text);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int a = 255 - trackBar1.Value;
            penColor = Color.FromArgb(a, penColor);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (penOnOff == false)
                return;
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
                return;
            Bitmap bm = new Bitmap(pictureBox1.Image);
            Graphics g = Graphics.FromImage(bm);
            g.DrawLine(new Pen(penColor, penSize), oldX, oldY, e.X, e.Y);
            oldX = e.X;
            oldY = e.Y;
            pictureBox1.Image = bm;
            pictureBox1.Update();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            oldX = e.X;
            oldY = e.Y;
        }

    }
}
