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
    public partial class Form1 : Form // наследуемся от Form, чтобы получить пустое графическое окно
    {
        int size = 10; // кол-во клеток игры
        int oldSize;
        int sizeBlock = 30; // размер клетки
        int xBlock, yBlock;
        int maxSize = 20; 
        int p;
        int m;
        int[,] area; 
        bool[,] open;
        bool[,] openFirst;
        bool[,] flag;
        Label[,] labels;
        Brush closeColor;
        Color openColor;
        Color valueColor;
        Pen lineColor;

        public Form1() // конструктор
        {
            InitializeComponent();

            valueColor = Color.Red;
            closeColor = Brushes.Gray;
            openColor = Color.LightGray;
            lineColor = Pens.Blue;
            включитьНастройкиToolStripMenuItem.Checked = false;
            radioButton8.Checked = true; // размер клетки и кол-во мин (кнопки)
            Size = new Size(size * sizeBlock + 21, Size.Height);

            area = new int[maxSize, maxSize];
            open = new bool[maxSize, maxSize];
            openFirst = new bool[maxSize, maxSize];
            flag = new bool[maxSize, maxSize];
            for (int i = 0; i < maxSize; i++)
                for (int j = 0; j < maxSize; j++)
                {
                    open[i, j] = false;
                    openFirst[i, j] = false;
                    flag[i, j] = false;
                }
            labels = new Label[maxSize, maxSize];
            for (int i = 0; i < maxSize; i++)
                for (int j = 0; j < maxSize; j++)
                {
                    labels[i, j] = new Label()
                    {
                        Location = new Point(panel1.Location.X + i * sizeBlock + 1, panel1.Location.Y + menuStrip1.Size.Height + j * sizeBlock + 1),
                        Width = sizeBlock,
                        Height = sizeBlock,
                        Visible = false,
                        ForeColor = valueColor,
                        Enabled = true,
                        Text = "",
                        BackColor = openColor,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font(Font, FontStyle.Bold)
                    };
                    Controls.Add(labels[i, j]);
                }
            mineSetter();
            p = 0;
            m = size;
            radioButton2.Checked = true;
            paint();
        }
        

        private void Form1_Load(object sender, EventArgs e) // функция загрузки формы
        {
            paint();
        }

        private void mineSetter() // расставляет мины по полю
        {
            Random rand = new Random();
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                        area[i, j] = 0;
            for(int i = 0; i < size; i++)
            {
                int x, y;
                do
                {
                    x = rand.Next(size);
                    y = rand.Next(size);
                } while (area[x, y] == 10);
                area[x, y] = 10;
            }


            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (area[i, j] == 10)
                        valueSetter(i-1, j-1);

        }

        private void openZero(int i, int j)
        {
            if ((i >= 0) && (i < size))
                if ((j >= 0) && (j < size))
                    // проверим, не было ли открыто поле раньше
                    if (open[i, j] == false)
                    {
                        // откроем
                        open[i, j] = true;
                        // если поле пустое (=0), просто пооткрываем всех его соседей
                        if (area[i, j] == 0)
                        {
                            if (border(i + 1, j)) openZero(i + 1, j);
                            if (border(i + 1, j - 1)) openZero(i + 1, j - 1);
                            if (border(i + 1, j + 1)) openZero(i + 1, j + 1);
                            if (border(i - 1, j - 1)) openZero(i - 1, j - 1);
                            if (border(i - 1, j)) openZero(i - 1, j);
                            if (border(i - 1, j + 1)) openZero(i - 1, j + 1);
                            if (border(i, j - 1)) openZero(i, j - 1);
                            if (border(i, j + 1)) openZero(i, j + 1);
                        }
                    }
        }
        bool border(int i, int j)
        {
            if (i < 0 || i >= size || j < 0 || j >= size)
                return false;
            return true;
        }

        private void valueSetter(int x, int y)
        {
            for (int i = x; i < x + 3; i++)
                for (int j = y; j < y + 3; j++)
                {
                    if (i < 0 || j < 0 || i > size - 1 || j > size - 1)
                        continue;
                    if (area[i, j] != 10)
                        area[i, j]++;
                }
            labelsSetter();
        }

        void labelsSetter() // оформляем все клетки
        {
            for (int i=0; i < size; i++)
                for (int j=0; j < size; j++)
                {
                    labels[i, j].Location = new Point(panel1.Location.X + i * sizeBlock + 1, panel1.Location.Y + 23 + j * sizeBlock + 2);
                    labels[i, j].Width = sizeBlock - 1;
                    labels[i, j].Height = sizeBlock - 1;
                    labels[i, j].Text = area[j, i].ToString();
                    labels[i, j].Visible = false;
                    labels[i, j].ForeColor = valueColor;
                    labels[i, j].BackColor = openColor;
                    labels[i, j].TextAlign = ContentAlignment.MiddleCenter;
                }
        }

        void paint()
        {
            labelsSetter();
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (labels[i, j].Text == "0")
                        labels[i, j].Text = "";
            draw_area();
            toolStripStatusLabel2.Text = p.ToString();
            m = size - flags;
            if (m < 0)
                m = 0;
            toolStripStatusLabel4.Text = m.ToString(); // сколько осталось мин

            Rectangle border = new Rectangle(0, 0, sizeBlock * size, sizeBlock * size); // создаем клетку (прямоугольник)

            panel1.CreateGraphics().DrawRectangle(Pens.Blue, border); //рисуем прямоугольник

            for (int i = 0; i < size; i++)
            {
                panel1.CreateGraphics().DrawLine(lineColor, 0, i * sizeBlock, size * sizeBlock, i * sizeBlock); // создаем графику линии
                panel1.CreateGraphics().DrawLine(lineColor, i * sizeBlock, 0, i * sizeBlock, size * sizeBlock);
            }
            theEnd();
        }

        private void draw_area()
        {
            
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    if(open[i, j] == false)
                    {
                        labels[j, i].Visible = false; // если клетка закрыта, не видим номера
                        Rectangle r = new Rectangle(j * sizeBlock, i * sizeBlock, sizeBlock, sizeBlock); // рисуем закрытую клетку
                        panel1.CreateGraphics().FillRectangle(closeColor, r);
                    }
                    else
                    {
                        labels[j, i].Visible = true;
                        labels[j, i].BringToFront();
                    }
                    if(flag[i, j] == true)
                    {
                        if (sizeBlock == 20)
                            panel1.CreateGraphics().DrawImage(Resources.Flag_20_, j * sizeBlock, i * sizeBlock);
                        if (sizeBlock == 30)
                            panel1.CreateGraphics().DrawImage(Resources.Flag_30_, j * sizeBlock, i * sizeBlock);
                        if (sizeBlock == 40)
                            panel1.CreateGraphics().DrawImage(Resources.Flag_40_, j * sizeBlock, i * sizeBlock);
                        if (sizeBlock == 50)
                            panel1.CreateGraphics().DrawImage(Resources.Flag_50_, j * sizeBlock, i * sizeBlock);
                    }
                    else
                    {
                        Rectangle r = new Rectangle(j * sizeBlock, i * sizeBlock, sizeBlock, sizeBlock);
                        panel1.CreateGraphics().FillRectangle(closeColor, r);
                    }
                }
            
        }

        private void theEnd()
        {
            int count = 0;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (open[i, j] == true)
                        count++;
            if(count == size*size - size)
            {
                DialogResult dr = MessageBox.Show("Хотите сыграть еще раз?", "ВЫ ВЫИГРАЛИ!!!", MessageBoxButtons.YesNo);//выбрасываем диалоговое окно конца игры
                if (dr == System.Windows.Forms.DialogResult.No)
                    Close();
                else
                {
                    for (int i = 0; i < oldSize; i++)
                        for (int j = 0; j < oldSize; j++)
                            labels[j, i].Visible = false;
                    for (int i = 0; i < size; i++)
                        for (int j = 0; j < size; j++)
                        {
                            open[i, j] = false;
                            flag[i, j] = false;
                        }
                    mineSetter();
                    labelsSetter();
                    if (sizeBlock == 20)
                        radioButton1_CheckedChanged(new object(), new EventArgs());
                    if (sizeBlock == 30)
                        radioButton2_CheckedChanged(new object(), new EventArgs());
                    if (sizeBlock == 40)
                        radioButton3_CheckedChanged(new object(), new EventArgs());
                    if (sizeBlock == 50)
                        radioButton4_CheckedChanged(new object(), new EventArgs());
                    paint();
                }
            }
        }
        int flags = 0;

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            xBlock = e.Y / sizeBlock; // координаты курсора
            yBlock = e.X / sizeBlock;
            if (e.Button == System.Windows.Forms.MouseButtons.Left && area[xBlock, yBlock] != 10) //задаем нажатие левой кнопки мыши
            {
                openZero(xBlock, yBlock);
                points();
                paint();
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right) // правая
            {
                if (flag[xBlock, yBlock] == false)
                {
                    flag[xBlock, yBlock] = true;
                    flags++;
                }
                else
                {
                    flag[xBlock, yBlock] = false;
                    flags--;
                }
                paint();
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Left && area[xBlock, yBlock] == 10)//если нажали лев кнп на мину, рисуем все мины
            {
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        if (area[i, j] == 10)
                        {
                            if (sizeBlock == 20)
                                panel1.CreateGraphics().DrawImage(Resources.Mine_20_, j * sizeBlock, i * sizeBlock);
                            if (sizeBlock == 30)
                                panel1.CreateGraphics().DrawImage(Resources.Mine_30_, j * sizeBlock, i * sizeBlock);
                            if (sizeBlock == 40)
                                panel1.CreateGraphics().DrawImage(Resources.Mine_40_, j * sizeBlock, i * sizeBlock);
                            if (sizeBlock == 50)
                                panel1.CreateGraphics().DrawImage(Resources.Mine_50_, j * sizeBlock, i * sizeBlock);
                        }
                        
                DialogResult dr = MessageBox.Show("Хотите сыграть еще раз?", "ВЫ ПОПАЛИ НА МИНУ!!!", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.No)
                    Close();
                else
                {
                    for (int i = 0; i < oldSize; i++)
                        for (int j = 0; j < oldSize; j++)
                            labels[j, i].Visible = false;
                    for (int i = 0; i < size; i++)
                        for (int j = 0; j < size; j++)
                        {
                            open[i, j] = false;
                            flag[i, j] = false;
                        }
                    mineSetter();
                    labelsSetter();
                    if (sizeBlock == 20)
                        radioButton1_CheckedChanged(sender, e);
                    if (sizeBlock == 30)
                        radioButton2_CheckedChanged(sender, e);
                    if (sizeBlock == 40)
                        radioButton3_CheckedChanged(sender, e);
                    if (sizeBlock == 50)
                        radioButton4_CheckedChanged(sender, e);
                    paint();
                }
            }
        }

        private void включитьНастройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(включитьНастройкиToolStripMenuItem.Checked == false)
            {
                включитьНастройкиToolStripMenuItem.Checked = true;
                включитьНастройкиToolStripMenuItem.Text = "Выключить настройки";
                Size = new Size(size * sizeBlock + 360, Size.Height); // увеличивает размер формы для настроек
            }
            else
            {
                включитьНастройкиToolStripMenuItem.Checked = false;
                включитьНастройкиToolStripMenuItem.Text = "Включить настройки";
                Size = new Size(size * sizeBlock + 21, Size.Height);
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            DialogResult dr = colorDialog1.ShowDialog(); // отображаем цветовую панель
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                SolidBrush sb = new SolidBrush(colorDialog1.Color); //заливка
                closeColor = sb;
                panel2.BackColor = colorDialog1.Color;
                paint();
            }
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            DialogResult dr = colorDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                openColor = colorDialog1.Color;
                panel3.BackColor = colorDialog1.Color;
                labelsSetter();
                paint();
            } 
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            DialogResult dr = colorDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                lineColor = new Pen(colorDialog1.Color);
                panel4.BackColor = colorDialog1.Color;
                paint();
            }
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            DialogResult dr = colorDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                valueColor = colorDialog1.Color;
                panel5.BackColor = colorDialog1.Color;
                labelsSetter();
                paint();
            } 
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) //первая кнопка размеров клетки
        {
            sizeBlock = 20;
            if (включитьНастройкиToolStripMenuItem.Checked == false)
            {
                Size = new Size(size * sizeBlock + 21, size * sizeBlock + 92);
                panel1.Size = new Size(sizeBlock * size + 1, sizeBlock * size + 1);
            }
            else
            {
                Size = new Size(size * sizeBlock + 360, size * sizeBlock + 182); //если настройки включены, размер окна другой
                panel1.Size = new Size(sizeBlock * size + 1, sizeBlock * size + 1);
            }
            paint();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            sizeBlock = 30;
            if (включитьНастройкиToolStripMenuItem.Checked == false)
            {
                Size = new Size(size * sizeBlock + 21, size * sizeBlock + 92);
                panel1.Size = new Size(sizeBlock * size + 1, sizeBlock * size + 1);
            }else
            {
                Size = new Size(size * sizeBlock + 360, size * sizeBlock + 92);
                panel1.Size = new Size(sizeBlock * size + 1, sizeBlock * size + 1);
            }
            paint();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            sizeBlock = 40;
            if (включитьНастройкиToolStripMenuItem.Checked == false)
            {
                Size = new Size(size * sizeBlock + 21, size * sizeBlock + 92);
                panel1.Size = new Size(sizeBlock * size + 1, sizeBlock * size + 1);
            }
            else
            {
                Size = new Size(size * sizeBlock + 360, size * sizeBlock + 92);
                panel1.Size = new Size(sizeBlock * size + 1, sizeBlock * size + 1);
            }
            paint();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            sizeBlock = 50;
            if (включитьНастройкиToolStripMenuItem.Checked == false)
            {
                Size = new Size(size * sizeBlock + 21, size * sizeBlock + 92);
                panel1.Size = new Size(sizeBlock * size + 1, sizeBlock * size + 1);
            }
            else
            {
                Size = new Size(size * sizeBlock + 360, size * sizeBlock + 92);
                panel1.Size = new Size(sizeBlock * size + 1, sizeBlock * size + 1);
            }
            paint();
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e) //меняет кол-во мин
        {
            oldSize = size;
            size = 10;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            oldSize = size;
            size = 12;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            oldSize = size;
            size = 15;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            oldSize = size;
            size = 20;
        }

        private void points()//начисление очков
        {
            p = 0;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (open[i, j] == true)
                        p += area[i, j] * 10;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           DialogResult dr = MessageBox.Show("Игра начнется сначала", "Сохранение настроек", MessageBoxButtons.OKCancel);
            if (dr == System.Windows.Forms.DialogResult.OK) //перерисовываем все
            {
                for (int i = 0; i < oldSize; i++) 
                    for (int j = 0; j < oldSize; j++)
                        labels[j, i].Visible = false; 
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                    {
                        open[i, j] = false;
                        openFirst[i, j] = false;
                        flag[i, j] = false;
                    }             
                mineSetter();
                labelsSetter();
                if (sizeBlock == 20)
                    radioButton1_CheckedChanged(sender, e);
                if (sizeBlock == 30)
                    radioButton2_CheckedChanged(sender, e);
                if (sizeBlock == 40)
                    radioButton3_CheckedChanged(sender, e);
                if (sizeBlock == 50)
                    radioButton4_CheckedChanged(sender, e);
                paint();
            }
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paint();
        }

    }

}
