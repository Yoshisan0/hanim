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
        private bool mPush;
        private int mX;
        private int mY;
        private int mGridWidth;
        private Pen mPen;
        private Pen mPenGrid;

        public FormRateGraph(int inGridWidth)
        {
            InitializeComponent();

            //以下、初期化処理
            this.mGridWidth = inGridWidth;
        }

        private void FormRateGraph_Load(object sender, EventArgs e)
        {
            this.mPen = new Pen(this.button_GraphColor.BackColor);
            this.mPenGrid = new Pen(this.button_GridColor.BackColor);

            this.panel_PreView.Refresh();
        }

        private void button_BackColor_Click(object sender, EventArgs e)
        {
            ColorDialog cdg = new ColorDialog();
            if (cdg.ShowDialog() == DialogResult.OK)
            {
                Button b = (Button)sender;
                b.BackColor = cdg.Color;

                this.panel_PreView.BackColor = cdg.Color;
            }
            cdg.Dispose();

            this.panel_PreView.Refresh();
        }

        private void button_GraphColor_Click(object sender, EventArgs e)
        {
            ColorDialog cdg = new ColorDialog();
            if (cdg.ShowDialog() == DialogResult.OK)
            {
                Button b = (Button)sender;
                b.BackColor = cdg.Color;

                this.mPen = new Pen(this.button_GraphColor.BackColor);
            }
            cdg.Dispose();

            this.panel_PreView.Refresh();
        }

        private void button_GridColor_Click(object sender, EventArgs e)
        {
            ColorDialog cdg = new ColorDialog();
            if (cdg.ShowDialog() == DialogResult.OK)
            {
                Button b = (Button)sender;
                b.BackColor = cdg.Color;

                this.mPenGrid = new Pen(this.button_GridColor.BackColor);
            }
            cdg.Dispose();

            this.panel_PreView.Refresh();
        }

        private void FormRateGraph_Resize(object sender, EventArgs e)
        {
            this.panel_PreView.Refresh();
        }

        private void panel_PreView_Paint(object sender, PaintEventArgs e)
        {
            //以下、背景クリア処理
            e.Graphics.Clear(this.button_BackColor.BackColor);

            //以下、グリッド描画処理
            if (this.checkBox_GridCheck.Checked) {
                int inCnt;

                //以下、縦ライン描画処理
                float flSpan = (float)this.panel_PreView.Width / 10;
                for (inCnt = 1; inCnt < 10; inCnt++)
                {
                    e.Graphics.DrawLine(this.mPenGrid, inCnt * flSpan, 0.0f, inCnt * flSpan, this.panel_PreView.Height);
                }

                //以下、横ライン描画処理
                flSpan = (float)this.panel_PreView.Height / 10;
                for (inCnt = 1; inCnt < 10; inCnt++)
                {
                    e.Graphics.DrawLine(this.mPenGrid, 0.0f, inCnt * flSpan, this.panel_PreView.Width, inCnt * flSpan);
                }
            }

            //以下、ペジェ曲線描画処理
            e.Graphics.DrawLine(this.mPen, 0.0f, this.panel_PreView.Height, this.panel_PreView.Width, 0.0f);
        }

        private void panel_PreView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mPush = true;
            }
        }

        private void panel_PreView_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.mPush) return;

            this.mX = e.X;
            this.mY = e.Y;
        }

        private void panel_PreView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mPush = false;
            }
        }
    }
}
