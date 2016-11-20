using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{
    public partial class FormRateGraph : Form
    {
        private static float SIZE_ELLIPSE = 15.0f;  //円の直径
        private bool mPush;         //マウスを押しているかどうかのフラグ
        private int mGridWidth;     //縦ラインの分割数
        private Pen mPenRed;        //ラインのペン
        private Pen mPen0;          //ラインのペン
        private Pen mPen1;          //ラインのペン
        private Pen mPenGrid;       //グリッドのペン
        private Vector3[] mListPos; //ポイントのリスト
        private Vector3[] mListVec; //ベクトルのリスト
        private int mGripNo;        //掴んでいるポイントの番号(0:掴んでいない 1:中間ポイント 2:始点のベクトル 3:中間点のベクトル 4:中間点の左下ベクトル 5:終点の左下ベクトル)
        private Bitmap mImage0;     //イメージ
        private Bitmap mImage1;     //イメージ
        private bool mChange;       //変更フラグ

        public FormRateGraph(int inGridWidth)
        {
            InitializeComponent();

            //以下、初期化処理
            this.mGripNo = 0;
            this.mGridWidth = inGridWidth;

            //panel_PreView.DoubleBuuferd = true;
            panel_PreView.GetType().InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, panel_PreView, new object[] { true });
        }

        private void FormRateGraph_Load(object sender, EventArgs e)
        {
            this.mPenRed = new Pen(Color.Red, 0.5f);
            this.mPen0 = new Pen(this.button_GraphColor.BackColor);
            this.mPen1 = new Pen(this.button_GraphColor.BackColor, 2.0f);
            this.mPenGrid = new Pen(this.button_GridColor.BackColor);

            this.mListPos = new Vector3[3];
            this.mListPos[0] = new Vector3(0.0f, 1.0f, 0.0f);
            this.mListPos[1] = new Vector3(0.5f, 0.5f, 0.0f);
            this.mListPos[2] = new Vector3(1.0f, 0.0f, 0.0f);

            this.mListVec = new Vector3[4];
            this.mListVec[0] = new Vector3(0.08f, -0.08f, 0.0f);
            this.mListVec[1] = new Vector3(0.08f, -0.08f, 0.0f);
            this.mListVec[2] = new Vector3(0.08f, -0.08f, 0.0f);

            this.mImage0 = new Bitmap(this.panel_PreView.Width, this.panel_PreView.Height);
            this.mImage1 = new Bitmap(this.panel_PreView.Width, this.panel_PreView.Height);
            this.mChange = true;

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

                this.mPen0 = new Pen(this.button_GraphColor.BackColor);
            }
            cdg.Dispose();

            this.mChange = true;

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
            this.mImage0 = new Bitmap(this.panel_PreView.Width, this.panel_PreView.Height);
            this.mImage1 = new Bitmap(this.panel_PreView.Width, this.panel_PreView.Height);

            this.mChange = true;

            this.panel_PreView.Refresh();
        }

        /// <summary>
        /// 矢印描画処理
        /// </summary>
        /// <param name="g">グラフィックス</param>
        /// <param name="clPenCircle">円のペン</param>
        /// <param name="flX">Ｘ座標(0.0～1.0)</param>
        /// <param name="flY">Ｙ座標(0.0～1.0)</param>
        /// <param name="flVecX">ベクトルＸ(0.0～1.0)</param>
        /// <param name="flVecY">ベクトルＹ(0.0～1.0)</param>
        private void DrawArrow(Graphics g, Pen clPenCircle, float flX, float flY, float flVecX, float flVecY)
        {
            //以下、矢印描画処理
            float flX0 = flX * this.panel_PreView.Width;
            float flY0 = flY * this.panel_PreView.Height;
            float flX1 = flX0 + flVecX * this.panel_PreView.Width;
            float flY1 = flY0 + flVecY * this.panel_PreView.Height;
            g.DrawLine(this.mPen0, flX0, flY0, flX1, flY1);

            //以下、矢印先の円を描画する処理
            this.DrawCircle(g, clPenCircle, flX1, flY1);
        }

        private void DrawCircle(Graphics g, Pen clPen, float flX, float flY)
        {
            g.DrawEllipse(clPen, flX - FormRateGraph.SIZE_ELLIPSE / 2, flY - FormRateGraph.SIZE_ELLIPSE / 2, FormRateGraph.SIZE_ELLIPSE, FormRateGraph.SIZE_ELLIPSE);
        }

        private void MakeImage()
        {
            using (Graphics g = Graphics.FromImage(this.mImage1))
            {
                g.Clear(Color.Transparent);

                int inWidth = this.mImage0.Width;
                int inHeight = this.mImage0.Height;
                float flSpanX = inWidth * 0.01f;
                int inOldX = 0;
                int inOldY = inHeight;
                float flX;
                int inY;
                for (flX = 0.0f; flX < inWidth; flX += flSpanX)
                {
                    for (inY = 0; inY < inHeight; inY++)
                    {
                        int inX = (int)flX;

                        Color stColor = this.mImage0.GetPixel(inX, inY);
                        if (stColor.A <= 0) continue;

                        g.DrawLine(this.mPen1, inOldX, inOldY, inX, inY);

                        inOldX = inX;
                        inOldY = inY;
                        break;
                    }
                }
            }
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
            int inPosX0 = (int)(this.mListPos[0].X * this.panel_PreView.Width);
            int inPosY0 = (int)(this.mListPos[0].Y * this.panel_PreView.Height);
            int inPosX1 = (int)(this.mListPos[1].X * this.panel_PreView.Width);
            int inPosY1 = (int)(this.mListPos[1].Y * this.panel_PreView.Height);
            int inPosX2 = (int)(this.mListPos[2].X * this.panel_PreView.Width);
            int inPosY2 = (int)(this.mListPos[2].Y * this.panel_PreView.Height);
            int inVecX0 = inPosX0 + (int)(this.mListVec[0].X * this.panel_PreView.Width);
            int inVecY0 = inPosY0 + (int)(this.mListVec[0].Y * this.panel_PreView.Height);
            int inVecX1 = inPosX1 - (int)(this.mListVec[1].X * this.panel_PreView.Width);
            int inVecY1 = inPosY1 - (int)(this.mListVec[1].Y * this.panel_PreView.Height);
            int inVecX2 = inPosX1 + (int)(this.mListVec[1].X * this.panel_PreView.Width);
            int inVecY2 = inPosY1 + (int)(this.mListVec[1].Y * this.panel_PreView.Height);
            int inVecX3 = inPosX2 - (int)(this.mListVec[2].X * this.panel_PreView.Width);
            int inVecY3 = inPosY2 - (int)(this.mListVec[2].Y * this.panel_PreView.Height);
            Point stPos0 = new Point(inPosX0, inPosY0);
            Point stVec0 = new Point(inVecX0, inVecY0);
            Point stVec1 = new Point(inVecX1, inVecY1);
            Point stPos1 = new Point(inPosX1, inPosY1);
            Point stVec2 = new Point(inVecX2, inVecY2);
            Point stVec3 = new Point(inVecX3, inVecY3);
            Point stPos2 = new Point(inPosX2, inPosY2);
            Point[] pclListPos = { stPos0, stVec0, stVec1, stPos1, stVec2, stVec3, stPos2 };

            using (Graphics g = Graphics.FromImage(this.mImage0))
            {
                g.Clear(Color.Transparent);
                g.DrawBeziers(this.mPenRed, pclListPos);
            }

            e.Graphics.DrawImage(this.mImage0, 0, 0);

            if (this.mChange)
            {
                this.mChange = false;
                MakeImage();
            }
            e.Graphics.DrawImage(this.mImage1, 0, 0);

            /*
            //以下、ライン描画処理
            float flX0 = this.mListPos[0].X * this.panel_PreView.Width;
            float flY0 = this.mListPos[0].Y * this.panel_PreView.Height;
            ClsMoveHermite clMoveHermite = new ClsMoveHermite(this.mListPos[0].Y, this.mListVec[0].Y * 2.0, this.mListPos[1].Y, this.mListVec[1].Y * 2.0);
            double doPosX, doSpan = this.mListPos[1].X * 0.005;
            for (doPosX = doSpan; doPosX < this.mListPos[1].X; doPosX += doSpan)
            {
                double doPosY = clMoveHermite.Exec(doPosX / this.mListPos[1].X);
                float flX1 = (float)doPosX * this.panel_PreView.Width;
                float flY1 = (float)doPosY * this.panel_PreView.Height;

                e.Graphics.DrawLine(this.mPen, flX0, flY0, flX1, flY1);

                flX0 = flX1;
                flY0 = flY1;
            }

            flX0 = this.mListPos[1].X * this.panel_PreView.Width;
            flY0 = this.mListPos[1].Y * this.panel_PreView.Height;
            clMoveHermite = new ClsMoveHermite(this.mListPos[1].Y, this.mListVec[1].Y * 2.0, this.mListPos[2].Y, this.mListVec[2].Y * 2.0);
            doSpan = (this.mListPos[2].X - this.mListPos[1].X) * 0.005;
            for (doPosX = doSpan; doPosX < this.mListPos[2].X - this.mListPos[1].X; doPosX += doSpan)
            {
                double doPosY = clMoveHermite.Exec(doPosX / (this.mListPos[2].X - this.mListPos[1].X));
                float flX1 = (float)(this.mListPos[1].X + doPosX) * this.panel_PreView.Width;
                float flY1 = (float)doPosY * this.panel_PreView.Height;

                e.Graphics.DrawLine(this.mPen, flX0, flY0, flX1, flY1);

                flX0 = flX1;
                flY0 = flY1;
            }
            */

            //以下、ポイント描画処理
            float flX = this.mListPos[1].X * this.panel_PreView.Width;
            float flY = this.mListPos[1].Y * this.panel_PreView.Height;
            Pen clPen = (this.mGripNo == 1) ? Pens.Red : this.mPen0;
            this.DrawCircle(e.Graphics, clPen, flX, flY);

            //以下、ベクトル描画処理
            clPen = (this.mGripNo == 2) ? Pens.Red : this.mPen0;
            this.DrawArrow(e.Graphics, clPen, this.mListPos[0].X, this.mListPos[0].Y, this.mListVec[0].X, this.mListVec[0].Y);
            clPen = (this.mGripNo == 3) ? Pens.Red : this.mPen0;
            this.DrawArrow(e.Graphics, clPen, this.mListPos[1].X, this.mListPos[1].Y, this.mListVec[1].X, this.mListVec[1].Y);
            clPen = (this.mGripNo == 4) ? Pens.Red : this.mPen0;
            this.DrawArrow(e.Graphics, clPen, this.mListPos[1].X, this.mListPos[1].Y, -this.mListVec[1].X, -this.mListVec[1].Y);
            clPen = (this.mGripNo == 5) ? Pens.Red : this.mPen0;
            this.DrawArrow(e.Graphics, clPen, this.mListPos[2].X, this.mListPos[2].Y, -this.mListVec[2].X, -this.mListVec[2].Y);
        }

        /// <summary>
        /// ポイントと円のあたり判定
        /// </summary>
        /// <param name="flPointX">ポイントのＸ座標</param>
        /// <param name="flPointY">ポイントのＹ座標</param>
        /// <param name="flCircleX">円のＸ座標</param>
        /// <param name="flCircleY">円のＹ座標</param>
        /// <param name="flR">円の半径</param>
        /// <returns>結果フラグ</returns>
        private bool IsHitPointAndCircle(float flPointX, float flPointY, float flCircleX, float flCircleY, float flR)
        {
            float flLen = (float)Math.Sqrt((flPointX - flCircleX) * (flPointX - flCircleX) + (flPointY - flCircleY) * (flPointY - flCircleY));
            return (flLen < flR);
        }

        private void panel_PreView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mPush = true;
                this.mGripNo = 0;

                //0:掴んでいない
                //1:中間ポイント
                //2:始点のベクトル
                //3:中間点のベクトル
                //4:中間点の左下ベクトル
                //5:終点の左下ベクトル

                //以下、中間点チェック処理
                float flX = this.mListPos[1].X * this.panel_PreView.Width;
                float flY = this.mListPos[1].Y * this.panel_PreView.Height;
                bool isHit = this.IsHitPointAndCircle(e.X, e.Y, flX, flY, FormRateGraph.SIZE_ELLIPSE / 2);
                if (isHit) this.mGripNo = 1;

                //以下、始点のベクトルチェック処理
                flX = this.mListPos[0].X * this.panel_PreView.Width;
                flY = this.mListPos[0].Y * this.panel_PreView.Height;
                flX += this.mListVec[0].X * this.panel_PreView.Width;
                flY += this.mListVec[0].Y * this.panel_PreView.Height;
                isHit = this.IsHitPointAndCircle(e.X, e.Y, flX, flY, FormRateGraph.SIZE_ELLIPSE / 2);
                if (isHit) this.mGripNo = 2;

                //以下、中間点のベクトルチェック処理
                flX = this.mListPos[1].X * this.panel_PreView.Width;
                flY = this.mListPos[1].Y * this.panel_PreView.Height;
                flX += this.mListVec[1].X * this.panel_PreView.Width;
                flY += this.mListVec[1].Y * this.panel_PreView.Height;
                isHit = this.IsHitPointAndCircle(e.X, e.Y, flX, flY, FormRateGraph.SIZE_ELLIPSE / 2);
                if (isHit) this.mGripNo = 3;

                //以下、中間点の左下ベクトルチェック処理
                flX = this.mListPos[1].X * this.panel_PreView.Width;
                flY = this.mListPos[1].Y * this.panel_PreView.Height;
                flX += -this.mListVec[1].X * this.panel_PreView.Width;
                flY += -this.mListVec[1].Y * this.panel_PreView.Height;
                isHit = this.IsHitPointAndCircle(e.X, e.Y, flX, flY, FormRateGraph.SIZE_ELLIPSE / 2);
                if (isHit) this.mGripNo = 4;

                //以下、終点の左下ベクトルチェック処理
                flX = this.mListPos[2].X * this.panel_PreView.Width;
                flY = this.mListPos[2].Y * this.panel_PreView.Height;
                flX += -this.mListVec[2].X * this.panel_PreView.Width;
                flY += -this.mListVec[2].Y * this.panel_PreView.Height;
                isHit = this.IsHitPointAndCircle(e.X, e.Y, flX, flY, FormRateGraph.SIZE_ELLIPSE / 2);
                if (isHit) this.mGripNo = 5;

                this.panel_PreView.Refresh();
            }
        }

        private void panel_PreView_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.mPush) return;

            int inX = e.X;
            int inY = e.Y;

            if (inX < 0) inX = 0;
            if (inX > this.panel_PreView.Width) inX = this.panel_PreView.Width;
            if (inY < 0) inY = 0;
            if (inY > this.panel_PreView.Height) inY = this.panel_PreView.Height;

            switch (this.mGripNo)
            {
            case 1:
                {
                    this.mListPos[1].X = (float)inX / this.panel_PreView.Width;
                    this.mListPos[1].Y = (float)inY / this.panel_PreView.Height;
                }
                break;
            case 2: //始点のベクトル
                {
                    this.mListVec[0].X = (float)inX / this.panel_PreView.Width;
                    this.mListVec[0].Y = (float)(inY - this.panel_PreView.Height) / this.panel_PreView.Height;
                }
                break;
            case 3: //中間点のベクトル
                {
                    float flBaseX = this.mListPos[1].X * this.panel_PreView.Width;
                    float flBaseY = this.mListPos[1].Y * this.panel_PreView.Height;
                    this.mListVec[1].X = (float)(inX - flBaseX) / this.panel_PreView.Width;
                    this.mListVec[1].Y = (float)(inY - flBaseY) / this.panel_PreView.Height;

                    if (this.mListVec[1].X < 0.0f) this.mListVec[1].X = 0.0f;
                }
                break;
            case 4: //中間点の左下ベクトル
                {
                    float flBaseX = this.mListPos[1].X * this.panel_PreView.Width;
                    float flBaseY = this.mListPos[1].Y * this.panel_PreView.Height;
                    this.mListVec[1].X = -(float)(inX - flBaseX) / this.panel_PreView.Width;
                    this.mListVec[1].Y = -(float)(inY - flBaseY) / this.panel_PreView.Height;

                    if (this.mListVec[1].X < 0.0f) this.mListVec[1].X = 0.0f;
                }
                break;
            case 5: //終点の左下ベクトル
                {
                    this.mListVec[2].X = -(float)(inX - this.panel_PreView.Width) / this.panel_PreView.Width;
                    this.mListVec[2].Y = -(float)inY / this.panel_PreView.Height;
                }
                break;
            }

            this.mChange = true;

            this.panel_PreView.Refresh();
        }

        private void panel_PreView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mPush = false;
                this.mGripNo = 0;

                this.panel_PreView.Refresh();
            }
        }

        private void button_Rate1_Click(object sender, EventArgs e)
        {
            this.mListPos[0].X = 0.0f;
            this.mListPos[0].Y = 1.0f;
            this.mListPos[1].X = 0.5f;
            this.mListPos[1].Y = 0.5f;
            this.mListPos[2].X = 1.0f;
            this.mListPos[2].Y = 0.0f;

            this.mChange = true;

            this.panel_PreView.Refresh();
        }

        private void button_Rate2_Click(object sender, EventArgs e)
        {

        }

        private void button_Rate3_Click(object sender, EventArgs e)
        {

        }
    }
}
