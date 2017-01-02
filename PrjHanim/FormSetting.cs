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
    public partial class FormSetting : Form
    {
        public FormSetting()
        {
            InitializeComponent();

            //以下、初期化処理
            this.DialogResult = DialogResult.None;
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void panel_MainBackColor_Click(object sender, EventArgs e)
        {
        }

        private void panel_MainGridColor_Click(object sender, EventArgs e)
        {
        }

        private void panel_MainCenterLineColor_Click(object sender, EventArgs e)
        {
        }

        private void panel_RateGraphBackColor_Click(object sender, EventArgs e)
        {
        }

        private void panel_RateGraphGridColor_Click(object sender, EventArgs e)
        {
        }

        private void panel_RateGraphCenterLineColor_Click(object sender, EventArgs e)
        {
        }

        private void panel_RateGraphGraphColor_Click(object sender, EventArgs e)
        {
        }

        private void panel_RateGraphForceColor_Click(object sender, EventArgs e)
        {
        }
    }
}
