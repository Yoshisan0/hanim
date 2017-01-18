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
    public partial class FormSingleInput : Form
    {
        public string initString;
        public FormSingleInput()
        {
            initString = "";
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            if(e.KeyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void FormSingleInput_Load(object sender, EventArgs e)
        {
            textBox1.Text = initString;
        }
    }
}
