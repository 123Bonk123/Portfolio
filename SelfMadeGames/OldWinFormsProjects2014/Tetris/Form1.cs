using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int areaWidth = 10;
        int areaHeight = 15;
        int sizeBlock = 20;

        int[,] area;
        Figure figure = null;

        public Form1()
        {
            InitializeComponent();
            area = new int[areaHeight, areaWidth];
        }

        public class Figure
        {
            public int x, y;
            public int[,] body = new int[4, 4];

            private int[, ,] source = {{{0,0,0,0},
                                       {1,1,1,1},
                                       {0,0,0,0},
                                       {0,0,0,0}},

                                     {{0,0,0,0},
                                      {1,1,1,0},
                                      {1,0,0,0},
                                      {0,0,0,0}},

                                     {{0,0,0,0},
                                      {0,1,1,0},
                                      {0,1,1,0},
                                      {0,0,0,0}},

                                     {{0,1,0,0},
                                      {0,1,1,0},
                                      {0,0,1,0},
                                      {0,0,0,0}},

                                     {{0,0,0,0},
                                      {1,1,1,0},
                                      {0,1,0,0},
                                      {0,0,0,0}},

                                      {{0,0,0,0},
                                      {1,1,1,0},
                                      {0,0,1,0},
                                      {0,0,0,0}},

                                     {{0,0,1,0},
                                      {0,1,1,0},
                                      {0,1,0,0},
                                      {0,0,0,0}}
                                     };
            public Figure()
            {
                x = 3; y = -2;
                Random rand = new Random();
                int n = rand.Next(7);
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        body[i, j] = source[n, j, i];
            }
            public void rotate()
            {
                int[,] body1 = new int[4, 4];
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                    {
                        body1[i, j] = body[j, 4 - i - 1];
                    }
                body = body1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (figure == null)
                figure = new Figure();
            if (figure_ok() == false)
                theEnd();
            figure_down();
            // вызываем функцию перерисовки панели
            panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // создаем прямоугольник - границу игровой области
            Rectangle border = new Rectangle(0, 0, sizeBlock * areaWidth, sizeBlock * areaHeight);

            // рисуем этот прямоугольник на панели
            panel1.CreateGraphics().DrawRectangle(Pens.Blue, border);

            // рисуем упавшые фигуры
            draw_area();

            // рисуем фигуру
            if (figure != null)
                draw_figure();
        }

        private void draw_area()
        {
                for (int i = 0; i < areaHeight; i++)
                    for (int j = 0; j < areaWidth; j++)
                            draw(j, i, area[i, j]);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
        private void draw(int x, int y, int value)
        {
            if (value == 0 || x < 0 || x >= areaWidth || y < 0 || y >= areaHeight)
                return;
            Rectangle r = new Rectangle(x * sizeBlock, y * sizeBlock, sizeBlock, sizeBlock);
            panel1.CreateGraphics().FillRectangle(Brushes.Red, r);
        }

        private void draw_figure()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    draw(figure.x + i, figure.y + j, figure.body[i, j]);
        }

        private void figure_down()
        {
            figure.y++;
            if(figure_ok() == false)
            {
                figure.y--;
                save_figure();
                figure = null;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (figure == null) return;
            switch (e.KeyCode)
            {
                case Keys.Left: //стрелка ВЛЕВО
                    figure.x--;
                    if (figure_ok() == false)
                        figure.x++;
                    break;
                case Keys.Right: //стрелка ВПРАВО
                    figure.x++;
                    if (figure_ok() == false)
                        figure.x--;
                    break;
                case Keys.Up: //стрелка ВВЕРХ
                    figure.rotate();
                    if (figure_ok() == false)
                    {
                        figure.rotate();
                        figure.rotate();
                        figure.rotate();
                    }
                    break;
                case Keys.Down: //стрелка ВНИЗ
                    figure.y++;
                    if (figure_ok() == false)
                        figure.y--;
                    break;
            }
            panel1.Refresh();
        }
        
        // получает состояние клетки в рабочей области
        private int get_area(int x, int y)
        {
            if (x < 0 || x >= areaWidth || y >= areaHeight) return 1;
            if (y < 0) return 0;
            return area[y, x];
        }

        // проверяет местоположение падающей фигуры
        private bool figure_ok()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (figure.body[i, j] == 1 && get_area(figure.x + i, figure.y + j) != 0)
                        return false;
                }
            return true;
        }

        private void set_area(int x, int y, int value)
        {
            if (x < 0 || x > areaWidth || y > areaHeight) return;
            if (y < 0) return;
            area[y, x] = value;
        }

        // сохраняем положение фигуры в массиве area
        private void save_figure()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (figure.body[i, j] == 1)
                        set_area(figure.x + i, figure.y + j, 1);
            check_lines();
        }

        // удаляем заполненные линии
        private void check_lines()
        {
            // создаем новый массив того же размера, что и area
            int[,] new_area = new int[areaHeight, areaWidth];
            int line = areaHeight - 1;

            //двигаемся от последней строки вверх к первой
            for(int j = areaHeight - 1; j >= 0; j--)
            {
                // если j-тая строка в массиве area не заполнена целиком единицами
                if(check_ones(j) == false)
                {
                    // записываем эту строку в new_area
                    for (int i = 0; i < areaWidth; i++)
                        new_area[line, i] = area[j, i];
                    line--;
                }
                // иначе ничего не добавляем в new_area
            }
            area = new_area;
        }

        private bool check_ones(int j)
        {
            for (int i = 0; i < areaWidth; i++)
                if (area[j, i] == 0)
                    return false;
            return true;

        }

        private void theEnd()
        {
            timer1.Enabled = false;
            MessageBox.Show("КОНЕЦ ИГРЫ!");
            Close();
        }
    }
}
