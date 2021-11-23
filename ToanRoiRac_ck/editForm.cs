using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToanRoiRac_ck
{
    public partial class editForm : Form
    {
        protected int i = 1;
        public editForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            int num = 0;
            i = Form1.id;
            Panel oldPan = new Panel() { Location = new Point(0, 0), Width = 0, Height = 0 };
            for (int j = 1; j < Form1.a.n; j++)
            {
                if (j != i)
                {
                    Panel curPan = new Panel()
                    {
                        Location = new Point(oldPan.Location.X, oldPan.Location.Y + oldPan.Height),
                        Width = 130,
                        Height = 40,
                    };

                    Label lb = new Label()
                    {
                        Location = new Point(0, 0),
                        Width = 70,
                        Height = 30,
                        Font = new Font("Arial", 10, FontStyle.Bold),
                    };
                    lb.Location = new Point(0, 0);

                    TextBox tb = new TextBox()
                    {
                        Location = new Point(lb.Location.X + lb.Width + 10, 0),
                        Width = 40,
                        Height = 30,
                        Font = new Font("Arial", 10, FontStyle.Bold),
                    };

                    lb.Text = i.ToString() + "-->" + j.ToString();
                    tb.Text = Form1.a.A[i, j].ToString();

                    curPan.Controls.Add(tb);
                    curPan.Controls.Add(lb);

                    oldPan = curPan;
                    panel1.Controls.Add(curPan);
                    if (num == 9)
                    {
                        num = 0;
                        Panel tmpPan = new Panel();
                        tmpPan.Location = new Point(curPan.Width + curPan.Location.X, 0);
                        oldPan = tmpPan;
                        oldPan.Width = 0;
                        oldPan.Height = 0;
                    }
                    num++;
                }
                else
                {
                    Panel l = new Panel();
                    l.Hide();
                    panel1.Controls.Add(l);
                }
            }
        }
        private void btn_Update_Click(object sender, EventArgs e)
        {
            int j = 1;
            foreach (Panel val in panel1.Controls)
            {
                if (i != j)
                {
                    int res = 9999;
                    TextBox tb = val.Controls[0] as TextBox;
                    int.TryParse(tb.Text, out res);
                    Form1.a.A[i, j] = Form1.a.A[j, i] = res;
                }
                j++;
            }
            this.Close();
        }
    }
}
