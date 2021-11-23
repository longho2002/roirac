using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToanRoiRac_ck
{
    public class Circular_BTN :Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, 50, 50);
            this.Region = new Region(gp);
            base.OnPaint(pevent);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Circular_BTN
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.UseVisualStyleBackColor = false;
            this.ResumeLayout(false);

        }
    }
}
