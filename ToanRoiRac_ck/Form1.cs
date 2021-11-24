using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToanRoiRac_ck
{
    public partial class Form1 : Form
    {
        public static List<Point> point = new List<Point>();
        public static int count = 1;
        public static Graph a = new Graph();
        public static Circular_BTN tmp = new Circular_BTN();
        public static int id = 0;
        public Form1()
        {
            InitializeComponent();
            pannel_city.BackColor = Color.White;
            init();
        }

        private void init()
        {
            int cap = a.getSize();
            for (int i = 1; i < cap; i++)
            {
                for (int j = 1; j < cap; j++)
                {
                    a.A[i, j] = 9999;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            LBmode.Text = "Add";
            pannel_city.Click -= addPoint;
            pannel_city.Click += addPoint;
            On_Off_drag(true);
            OffEdit();
        }
        //53, 57
        protected void addPoint(object sender, EventArgs e)
        {
            Point tmpPoint = pannel_city.PointToClient(Cursor.Position);
            point.Add(tmpPoint);
            Button btntmp = new Button()
            {
                Width = 50,
                Height = 50,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Text = count.ToString(),
                BackColor = Color.Red
            };
            btntmp.TabStop = false;
            btntmp.FlatStyle = FlatStyle.Flat;
            btntmp.FlatAppearance.BorderSize = 0;
            btntmp.Location = new Point(tmpPoint.X - btntmp.Width / 2, tmpPoint.Y - btntmp.Height / 2);
            count++;
            pannel_city.Controls.Add(btntmp);
        }

        // button edit click
        private void button3_Click(object sender, EventArgs e)
        {
            LBmode.Text = "Edit";
            a.n = count;
            pannel_city.Click -= addPoint;
            On_Off_drag(true);
            foreach (Button i in pannel_city.Controls)
            {
                i.Click -= ChangePos;
                i.Click += ChangePos;
            }
        }

        private void On_Off_drag(bool choose)
        {
            foreach (Button i in pannel_city.Controls)
            {
                ControlExtension.Draggable(i, choose);
            }
        }
        private void ChangePos(object sender, EventArgs e)
        {
            id = int.Parse((sender as Button).Text);
            editForm b = new editForm();
            b.ShowDialog();
            Draw();
        }
        private void Draw()
        {
            if (point.Count() != count - 1)
                return;
            UpdatePos();
            using (Graphics g = pannel_city.CreateGraphics())
            {
                //g.Clear(Color.White);
                int len = a.n;
                for (int j = 1; j <= len; j++)
                {
                    for (int i = 1; i <= len; i++)
                    {
                        if (a.A[j, i] != 9999)
                        {
                            g.DrawLine(new Pen(Color.Black, 3), point[i - 1], point[j - 1]);
                            int x = (point[j - 1].X + point[i - 1].X) / 2;
                            int y = (point[j - 1].Y + point[i - 1].Y) / 2;
                            g.FillRectangle(new SolidBrush(Color.Cyan), x - 15, y - 15, 30, 30);
                            g.DrawString(a.A[i, j].ToString(), new Font("Arial", 12), new SolidBrush(Color.DarkOrange), x - 6, y - 6);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LBmode.Text = "Process";
            pannel_city.Click -= addPoint;
            On_Off_drag(true);
            pannel_city.Click -= addPoint;
            int res;
            bool flag;
            int num_select = 1;
            flag = int.TryParse(TB_start.Text, out res);
            if (flag)
            {
                a.city[0] = res;
            }
            else
            {
                MessageBox.Show("Wrong Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string[] s = textBox1.Text.Split(' ');
            foreach (var i in s)
            {
                flag = int.TryParse(i, out res);
                if (flag)
                {
                    a.city[num_select++] = res;
                }
                else
                {
                    MessageBox.Show("Wrong Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            a.numOfCity = num_select;
            a.Dulich();
            if (a.MIN >= 9999)
            {
                TB_res.Text = "Không có đường đi";
                return;
            }
            string tmpRES = "";
            using (Graphics g = pannel_city.CreateGraphics())
            {
                for (int j = 0; j < a.city_ - 1; j++)
                {
                    tmpRES += (a.Min_Path[j]).ToString() + "->";
                    g.DrawLine(new Pen(Color.GreenYellow, 3), point[a.Min_Path[j] - 1], point[a.Min_Path[j + 1] - 1]);
                    int x = (point[a.Min_Path[j] - 1].X + point[a.Min_Path[j + 1] - 1].X) / 2;
                    int y = (point[a.Min_Path[j] - 1].Y + point[a.Min_Path[j + 1] - 1].Y) / 2;
                    g.DrawString(a.A[a.Min_Path[j + 1], a.Min_Path[j]].ToString(), new Font("Arial", 16), new SolidBrush(Color.Blue), x - 8, y - 8);
                }
            }

            for (int i = 0; i < a.city_; i++)
            {
                foreach (var VARIABLE in pannel_city.Controls)
                {
                    Button btn = VARIABLE as Button;
                    if (btn.Text == a.Min_Path[i].ToString())
                    {
                        btn.BackColor = Color.YellowGreen;
                    }
                }
            }
            tmpRES += (a.Min_Path[a.city_ - 1]).ToString();
            TB_res.Text = tmpRES;
            textBox2.Text = a.MIN.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int choose = 0;
            if (cb.SelectedIndex != null)
            {
                int.TryParse(cb.SelectedIndex.ToString(), out choose);
            }
            else
            {
                return;
            }

            switch (choose)
            {
                case 0:
                    {
                        string[] t = File.ReadAllLines("test1.txt");
                        a.DocFile(t);
                        break;
                    }
                case 1:
                    {
                        string[] t = File.ReadAllLines("test2.txt");
                        a.DocFile(t);
                        break;
                    }
                case 2:
                    {
                        string[] t = File.ReadAllLines("test3.txt");
                        a.DocFile(t);
                        break;
                    }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pannel_city.Invalidate();
            LBmode.Text = "Drag";
            pannel_city.Click -= addPoint;
            OffEdit();
            On_Off_drag(true);
            //pannel_city.Invalidate();
        }
        private void OffEdit()
        {
            foreach (Button i in pannel_city.Controls)
            {
                i.Click -= ChangePos;
            }
        }
        private void UpdatePos()
        {
            point.Clear();
            foreach (Button i in pannel_city.Controls)
            {
                point.Add(new Point(i.Location.X + i.Width / 2, i.Location.Y + i.Height / 2));
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            LBmode.Text = "Draw";
            pannel_city.Click -= addPoint;
            On_Off_drag(false);
            Draw();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pannel_city.Controls.Clear();
            count = 0;
            pannel_city.Click -= addPoint;
            On_Off_drag(false);
        }
    }
}
