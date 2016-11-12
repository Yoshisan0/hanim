using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{
    public partial class FormRateGraph : Form
    {
        public FormRateGraph()
        {
            InitializeComponent();
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
        }

        private void button_BackColor_Click(object sender, EventArgs e)
        {
            ColorDialog cdg = new ColorDialog();
            if (cdg.ShowDialog() == DialogResult.OK)
            {
                Button b = (Button)sender;
                b.BackColor = cdg.Color;
                panel_PreView.BackColor = cdg.Color;
            }
            cdg.Dispose();
            panel_PreView.Refresh();
        }
    }
}
