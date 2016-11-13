using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{
    public partial class FormRateGraph : Form
    {
        private static float SIZE_ELLIPSE = 15.0f;  //円の直径
        private bool mPush;
        private int mGridWidth;
        private Pen mPen;
        private Pen mPenGrid;
        private Vector3[] mListPos;
        private Vector3[] mListVec;
        private int mGripNo;    //掴んでいるポイントの番号(-1:掴んでいない 0～2:番号)

        public FormRateGraph(int inGridWidth)
        {
            InitializeComponent();

            //以下、初期化処理
            this.mGripNo = -1;
            this.mGridWidth = inGridWidth;

            //panel_PreView.DoubleBuuferd = true;
            panel_PreView.GetType().InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, panel_PreView, new object[] { true });
        }

        private void FormRateGraph_Load(object sender, EventArgs e)
        {
            this.mPen = new Pen(this.button_GraphColor.BackColor, 2.0f);
            this.mPenGrid = new Pen(this.button_GridColor.BackColor);

            this.mListPos = new Vector3[3];
            this.mListPos[0] = new Vector3(0.0f, 1.0f, 0.0f);
            this.mListPos[1] = new Vector3(0.5f, 0.5f, 0.0f);
            this.mListPos[2] = new Vector3(1.0f, 0.0f, 0.0f);

            this.mListVec = new Vector3[3];
            this.mListVec[0] = new Vector3(0.0f, 0.0f, 0.0f);
            this.mListVec[1] = new Vector3(0.0f, 0.0f, 0.0f);
            this.mListVec[2] = new Vector3(0.0f, 0.0f, 0.0f);

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
            int inCnt;

            //以下、背景クリア処理
            e.Graphics.Clear(this.button_BackColor.BackColor);

            //以下、グリッド描画処理
            if (this.checkBox_GridCheck.Checked) {
                //以下、縦ライン描画処理
                float flSpan = (float)this.panel_PreView.Width / this.mGridWidth;
                for (inCnt = 1; inCnt < this.mGridWidth; inCnt++)
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

            //以下、ライン描画処理
            float flX0 = this.mListPos[0].X * this.panel_PreView.Width;
            float flY0 = this.mListPos[0].Y * this.panel_PreView.Height;
            float flX1 = this.mListPos[1].X * this.panel_PreView.Width;
            float flY1 = this.mListPos[1].Y * this.panel_PreView.Height;
            e.Graphics.DrawLine(this.mPen, flX0, flY0, flX1, flY1);

            flX0 = this.mListPos[1].X * this.panel_PreView.Width;
            flY0 = this.mListPos[1].Y * this.panel_PreView.Height;
            flX1 = this.mListPos[2].X * this.panel_PreView.Width;
            flY1 = this.mListPos[2].Y * this.panel_PreView.Height;
            e.Graphics.DrawLine(this.mPen, flX0, flY0, flX1, flY1);

            //以下、ポイント描画処理
            float flX = this.mListPos[1].X * this.panel_PreView.Width;
            float flY = this.mListPos[1].Y * this.panel_PreView.Height;
            Pen clPen = (this.mGripNo == 1) ? Pens.Red : this.mPen;
            e.Graphics.DrawEllipse(clPen, flX - FormRateGraph.SIZE_ELLIPSE / 2, flY - FormRateGraph.SIZE_ELLIPSE / 2, FormRateGraph.SIZE_ELLIPSE, FormRateGraph.SIZE_ELLIPSE);
        }

        private void panel_PreView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mPush = true;

                //以下、グリップ処理
                this.mGripNo = -1;
                float flX = this.mListPos[1].X * this.panel_PreView.Width;
                float flY = this.mListPos[1].Y * this.panel_PreView.Height;
                float flLen = (float)Math.Sqrt((e.X - flX) * (e.X - flX) + (e.Y - flY) * (e.Y - flY));
                if (flLen < FormRateGraph.SIZE_ELLIPSE / 2)
                {
                    this.mGripNo = 1;
                }

                this.panel_PreView.Refresh();
            }
        }

        private void panel_PreView_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.mPush) return;

            if (this.mGripNo == 1)
            {
                int inX = e.X;
                int inY = e.Y;
                if (inX < 0) inX = 0;
                if (inX > this.panel_PreView.Width) inX = this.panel_PreView.Width;
                if (inY < 0) inY = 0;
                if (inY > this.panel_PreView.Height) inY = this.panel_PreView.Height;

                this.mListPos[this.mGripNo].X = (float)inX / this.panel_PreView.Width;
                this.mListPos[this.mGripNo].Y = (float)inY / this.panel_PreView.Height;

                this.panel_PreView.Refresh();
            }
        }

        private void panel_PreView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mPush = false;
                this.mGripNo = -1;

                this.panel_PreView.Refresh();
            }
        }
    }
}
