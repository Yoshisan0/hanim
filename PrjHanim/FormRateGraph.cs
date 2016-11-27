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
        private static float POS_X0 = 0.0f;         //開始座標Ｘ
        private static float POS_Y0 = 1.0f;         //開始座標Ｙ
        private static float POS_X1 = 0.5f;         //中心座標Ｘ
        private static float POS_Y1 = 0.5f;         //中心座標Ｙ
        private static float POS_X2 = 1.0f;         //終了座標Ｘ
        private static float POS_Y2 = 0.0f;         //終了座標Ｙ
        private static float VEC_X0 = 0.08f;        //開始座標のベクトルＸ
        private static float VEC_Y0 = -0.08f;       //開始座標のベクトルＹ
        private static float VEC_X1 = 0.08f;        //中心座標のベクトルＸ
        private static float VEC_Y1 = -0.08f;       //中心座標のベクトルＹ
        private static float VEC_X2 = 0.08f;        //終了座標のベクトルＸ
        private static float VEC_Y2 = -0.08f;       //終了座標のベクトルＹ
        private static float SIZE_ELLIPSE = 15.0f;  //円の直径
        private static int MAX_X = 256;             //保存用データの長さ

        private bool mPush;         //マウスを押しているかどうかのフラグ
        private int mFrmStart;      //開始フレーム
        private int mFrmEnd;        //終了フレーム
        private int mFrmCurrent;    //カレントフレーム
        private Pen mPenRed;        //赤いラインのペン
        private Pen mPenGraph;      //ベクトルのペン
        private Pen mPenLine;       //ラインのペン
        private Pen mPenGrid;       //グリッドのペン
        private Pen mPenCurrent;    //カレントフレームのペン
        private Vector3[] mListPos; //ポイントのリスト
        private Vector3[] mListVec; //ベクトルのリスト
        private int mGripNo;        //掴んでいるポイントの番号(0:掴んでいない 1:中間ポイント 2:始点のベクトル 3:中間点のベクトル 4:中間点の左下ベクトル 5:終点の左下ベクトル)
        private Bitmap mImage0;     //イメージ
        private Bitmap mImage1;     //イメージ
        private bool mChange;       //変更フラグ
        private FormMain mFormMain; //メインフォーム
        private EnmParam mParam;    //パラメーター種別

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="clForm">メインフォーム</param>
        /// <param name="enParam">種別</param>
        /// <param name="inFrmStart">開始フレーム</param>
        /// <param name="inFrmEnd">終了フレーム</param>
        /// <param name="inFrmCurrent">カレントフレーム</param>
        public FormRateGraph(FormMain clForm, EnmParam enParam, int inFrmStart, int inFrmEnd, int inFrmCurrent)
        {
            InitializeComponent();

            //以下、初期化処理
            this.mFormMain = clForm;
            this.Text = "レートグラフ " + enParam;
            this.mGripNo = 0;
            this.mParam = enParam;
            this.mFrmStart = inFrmStart;
            this.mFrmEnd = inFrmEnd;
            this.mFrmCurrent = inFrmCurrent;

            this.mListPos = new Vector3[3];
            this.mListPos[0] = new Vector3(FormRateGraph.POS_X0, FormRateGraph.POS_Y0, 0.0f);
            this.mListPos[1] = new Vector3(FormRateGraph.POS_X1, FormRateGraph.POS_Y1, 0.0f);
            this.mListPos[2] = new Vector3(FormRateGraph.POS_X2, FormRateGraph.POS_Y2, 0.0f);

            this.mListVec = new Vector3[3];
            this.mListVec[0] = new Vector3(FormRateGraph.VEC_X0, FormRateGraph.VEC_Y0, 0.0f);
            this.mListVec[1] = new Vector3(FormRateGraph.VEC_X1, FormRateGraph.VEC_Y1, 0.0f);
            this.mListVec[2] = new Vector3(FormRateGraph.VEC_X2, FormRateGraph.VEC_Y2, 0.0f);

            //panel_PreView.DoubleBuuferd = true;
            panel_PreView.GetType().InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, panel_PreView, new object[] { true });
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="clForm">メインフォーム</param>
        /// <param name="enParam">種別</param>
        /// <param name="inFrmStart">開始フレーム</param>
        /// <param name="inFrmEnd">終了フレーム</param>
        /// <param name="inFrmCurrent">カレントフレーム</param>
        /// <param name="clPos">中心座標(0.0～1.0)</param>
        /// <param name="pclListVec">各ベクトル(0.0～1.0)</param>
        public FormRateGraph(FormMain clForm, EnmParam enParam, int inFrmStart, int inFrmEnd, int inFrmCurrent, Vector3 clPos, Vector3[] pclListVec)
        {
            InitializeComponent();

            //以下、初期化処理
            this.mFormMain = clForm;
            this.Text = "レートグラフ " + enParam;
            this.mGripNo = 0;
            this.mParam = enParam;
            this.mFrmStart = inFrmStart;
            this.mFrmEnd = inFrmEnd;
            this.mFrmCurrent = inFrmCurrent;

            this.mListPos = new Vector3[3];
            this.mListPos[0] = new Vector3(FormRateGraph.POS_X0, FormRateGraph.POS_Y0, 0.0f);
            this.mListPos[1] = new Vector3(clPos.X, clPos.Y, clPos.Z);
            this.mListPos[2] = new Vector3(FormRateGraph.POS_X2, FormRateGraph.POS_Y2, 0.0f);

            this.mListVec = new Vector3[3];
            int inCnt;
            for (inCnt = 0; inCnt < 3; inCnt++)
            {
                this.mListVec[inCnt] = new Vector3(pclListVec[inCnt].X, pclListVec[inCnt].Y, pclListVec[inCnt].Z);
            }

            //panel_PreView.DoubleBuuferd = true;
            panel_PreView.GetType().InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, panel_PreView, new object[] { true });
        }

        /// <summary>
        /// 重みを返す処理
        /// </summary>
        /// <param name="clTween">トゥイーン情報</param>
        /// <param name="puchRate">重みリスト</param>
        /// <param name="inFrmCurrent">カレントフレーム</param>
        /// <returns>重み(0.0～1.0)</returns>
        public static float GetRate(ClsTween clTween, byte[] puchRate, int inFrmCurrent)
        {
            if (clTween == null) return (0.0f);
            if (puchRate == null)
            {
                puchRate = FormRateGraph.CreateSaveData(clTween);
                if (puchRate == null) return (0.0f);
            }
            if (!(clTween.mFrmStart <= inFrmCurrent && inFrmCurrent <= clTween.mFrmEnd)) return (0.0f);

            float flFrmSize = clTween.mFrmEnd - clTween.mFrmStart;
            inFrmCurrent -= clTween.mFrmStart;
            byte uchRate = (byte)((float)inFrmCurrent / flFrmSize * puchRate.Length);
            float flRate = (float)uchRate / byte.MaxValue;

            return (flRate);
        }

        /// <summary>
        /// フレーム情報の設定
        /// </summary>
        /// <param name="inFrmStart">開始フレーム</param>
        /// <param name="inFrmEnd">終了フレーム</param>
        /// <param name="inFrmCurrent">カレントフレーム</param>
        public void SetFrame(int inFrmStart, int inFrmEnd, int inFrmCurrent)
        {
            this.mFrmStart = inFrmStart;
            this.mFrmEnd = inFrmEnd;
            this.mFrmCurrent = inFrmCurrent;
        }

        /// <summary>
        /// トゥイーン情報を出力用データに変換する処理
        /// </summary>
        /// <param name="clTween">トゥイーン情報</param>
        /// <returns>出力用データ</returns>
        public static byte[] CreateSaveData(ClsTween clTween)
        {
            if (clTween == null) return (null);

            byte[] puchRate = new byte[FormRateGraph.MAX_X];

            int inWidth = FormRateGraph.MAX_X;
            int inHeight = byte.MaxValue;
            Bitmap clImage = FormRateGraph.CreateImage(clTween, inWidth, inHeight);

            //以下、出力用データ作成処理
            int inX, inY;
            for (inX = 0; inX < inWidth; inX ++)
            {
                for (inY = 0; inY < inHeight; inY ++)
                {
                    Color stColor = clImage.GetPixel(inX, inY);
                    if (stColor.A!= 0) continue;

                    puchRate[inX] = (byte)inY;
                    break;
                }
            }

            return (puchRate);
        }

        /// <summary>
        /// トゥイーン情報をアイコン画像に変換する処理
        /// </summary>
        /// <param name="clTween">トゥイーン情報</param>
        /// <param name="inWidth">イメージ幅</param>
        /// <param name="inHeight">イメージ高さ</param>
        /// <returns>画像</returns>
        public static Bitmap CreateImage(ClsTween clTween, int inWidth, int inHeight)
        {
            Pen clPen = new Pen(Color.Green);
            Bitmap clImage = new Bitmap(inWidth, inHeight);

            //以下、ペン作成処理
            Pen stPen = new Pen(Color.Red);

            //以下、ライン作成処理 
            int inPosX0 = (int)(FormRateGraph.POS_X0 * inWidth);
            int inPosY0 = (int)(FormRateGraph.POS_Y0 * inHeight);
            int inPosX1 = (int)(clTween.mPos.X * inWidth);
            int inPosY1 = (int)(clTween.mPos.Y * inHeight);
            int inPosX2 = (int)(FormRateGraph.POS_X2 * inWidth);
            int inPosY2 = (int)(FormRateGraph.POS_Y2 * inHeight);
            int inVecX0 = inPosX0 + (int)(clTween.mListVec[0].X * inWidth);
            int inVecY0 = inPosY0 + (int)(clTween.mListVec[0].Y * inHeight);
            int inVecX1 = inPosX1 - (int)(clTween.mListVec[1].X * inWidth);
            int inVecY1 = inPosY1 - (int)(clTween.mListVec[1].Y * inHeight);
            int inVecX2 = inPosX1 + (int)(clTween.mListVec[1].X * inWidth);
            int inVecY2 = inPosY1 + (int)(clTween.mListVec[1].Y * inHeight);
            int inVecX3 = inPosX2 - (int)(clTween.mListVec[2].X * inWidth);
            int inVecY3 = inPosY2 - (int)(clTween.mListVec[2].Y * inHeight);
            Point stPos0 = new Point(inPosX0, inPosY0);
            Point stVec0 = new Point(inVecX0, inVecY0);
            Point stVec1 = new Point(inVecX1, inVecY1);
            Point stPos1 = new Point(inPosX1, inPosY1);
            Point stVec2 = new Point(inVecX2, inVecY2);
            Point stVec3 = new Point(inVecX3, inVecY3);
            Point stPos2 = new Point(inPosX2, inPosY2);
            Point[] pclListPos = { stPos0, stVec0, stVec1, stPos1, stVec2, stVec3, stPos2 };

            //以下、画像作成処理
            using (Graphics g = Graphics.FromImage(clImage))
            {
                g.Clear(Color.Transparent);
                g.DrawBeziers(stPen, pclListPos);
            }

            return (clImage);
        }

        /// <summary>
        /// トゥイーン情報の取得
        /// </summary>
        /// <returns>トゥイーン情報</returns>
        public ClsTween GetTween()
        {
            ClsTween clTween = new ClsTween(this.mParam, this.mFrmStart, this.mFrmEnd, this.mListPos[1], this.mListVec);
            return (clTween);
        }

        private void FormRateGraph_Load(object sender, EventArgs e)
        {
            this.mPenRed = new Pen(Color.Red, 0.5f);
            this.mPenGraph = new Pen(this.button_ColorGraph.BackColor);
            this.mPenLine = new Pen(this.button_ColorLine.BackColor, 2.0f);
            this.mPenGrid = new Pen(this.button_ColorGrid.BackColor);
            this.mPenCurrent = new Pen(this.button_ColorCurrent.BackColor);

            this.mImage0 = new Bitmap(this.panel_PreView.Width, this.panel_PreView.Height);
            this.mImage1 = new Bitmap(this.panel_PreView.Width, this.panel_PreView.Height);

            this.mChange = true;
            this.panel_PreView.Refresh();
        }

        private void SetColor(Button clButton)
        {
            using (ColorDialog clColorDialog = new ColorDialog())
            {
                if (clColorDialog.ShowDialog() == DialogResult.OK)
                {
                    clButton.BackColor = clColorDialog.Color;
                }
            }
        }

        private void button_ColorBack_Click(object sender, EventArgs e)
        {
            this.SetColor(sender as Button);

            this.panel_PreView.BackColor = this.button_ColorBack.BackColor;

            this.panel_PreView.Refresh();
        }

        private void button_ColorLine_Click(object sender, EventArgs e)
        {
            this.SetColor(sender as Button);

            this.mPenLine = new Pen(this.button_ColorLine.BackColor, 2.0f);

            this.mChange = true;
            this.panel_PreView.Refresh();
        }

        private void button_ColorGraph_Click(object sender, EventArgs e)
        {
            this.SetColor(sender as Button);

            this.mPenGraph = new Pen(this.button_ColorGraph.BackColor);

            this.mChange = true;
            this.panel_PreView.Refresh();
        }

        private void button_ColorGrid_Click(object sender, EventArgs e)
        {
            this.SetColor(sender as Button);

            this.mPenGrid = new Pen(this.button_ColorGrid.BackColor);

            this.panel_PreView.Refresh();
        }

        private void button_ColorCurrent_Click(object sender, EventArgs e)
        {
            this.SetColor(sender as Button);

            this.mPenCurrent = new Pen(this.button_ColorCurrent.BackColor);

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
            g.DrawLine(this.mPenGraph, flX0, flY0, flX1, flY1);

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
                int inXOld = 0;
                int inYOld = inHeight;
                float flX;
                int inY;

                for (flX = 0.0f; flX < inWidth; flX += flSpanX)
                {
                    for (inY = 0; inY < inHeight; inY++)
                    {
                        int inX = (int)flX;

                        Color stColor = this.mImage0.GetPixel(inX, inY);
                        if (stColor.A <= 0) continue;

                        g.DrawLine(this.mPenLine, inXOld, inYOld, inX, inY);

                        inXOld = inX;
                        inYOld = inY;
                        break;
                    }
                }
            }
        }

        private void panel_PreView_Paint(object sender, PaintEventArgs e)
        {
            int inCnt;

            //以下、背景クリア処理
            e.Graphics.Clear(this.button_ColorBack.BackColor);

            //以下、グリッド描画処理
            if (this.checkBox_GridCheck.Checked) {
                //以下、縦ライン描画処理
                int inFrmCount = this.mFrmEnd - this.mFrmStart;
                if (inFrmCount < 0) inFrmCount = 1;
                int inFrmCurrent = this.mFrmCurrent - this.mFrmStart;

                float flSpan = (float)this.panel_PreView.Width / inFrmCount;
                for (inCnt = 1; inCnt < inFrmCount; inCnt++)
                {
                    if (inCnt == inFrmCurrent)
                    {
                        e.Graphics.DrawLine(this.mPenCurrent, inCnt * flSpan, 0.0f, inCnt * flSpan, this.panel_PreView.Height);
                    }
                    else
                    {
                        e.Graphics.DrawLine(this.mPenGrid, inCnt * flSpan, 0.0f, inCnt * flSpan, this.panel_PreView.Height);
                    }
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
            Pen clPen = (this.mGripNo == 1) ? Pens.Red : this.mPenGraph;
            this.DrawCircle(e.Graphics, clPen, flX, flY);

            //以下、ベクトル描画処理
            clPen = (this.mGripNo == 2) ? Pens.Red : this.mPenGraph;
            this.DrawArrow(e.Graphics, clPen, this.mListPos[0].X, this.mListPos[0].Y, this.mListVec[0].X, this.mListVec[0].Y);
            clPen = (this.mGripNo == 3) ? Pens.Red : this.mPenGraph;
            this.DrawArrow(e.Graphics, clPen, this.mListPos[1].X, this.mListPos[1].Y, this.mListVec[1].X, this.mListVec[1].Y);
            clPen = (this.mGripNo == 4) ? Pens.Red : this.mPenGraph;
            this.DrawArrow(e.Graphics, clPen, this.mListPos[1].X, this.mListPos[1].Y, -this.mListVec[1].X, -this.mListVec[1].Y);
            clPen = (this.mGripNo == 5) ? Pens.Red : this.mPenGraph;
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
            this.mListPos[0].X = FormRateGraph.POS_X0;
            this.mListPos[0].Y = FormRateGraph.POS_Y0;
            this.mListPos[1].X = FormRateGraph.POS_X1;
            this.mListPos[1].Y = FormRateGraph.POS_Y1;
            this.mListPos[2].X = FormRateGraph.POS_X2;
            this.mListPos[2].Y = FormRateGraph.POS_Y2;

            this.mListVec[0].X = FormRateGraph.VEC_X0;
            this.mListVec[0].Y = FormRateGraph.VEC_Y0;
            this.mListVec[1].X = FormRateGraph.VEC_X1;
            this.mListVec[1].Y = FormRateGraph.VEC_Y1;
            this.mListVec[2].X = FormRateGraph.VEC_X2;
            this.mListVec[2].Y = FormRateGraph.VEC_Y2;

            this.mChange = true;

            this.panel_PreView.Refresh();
        }

        private void button_Rate2_Click(object sender, EventArgs e)
        {
            this.mListPos[0].X = FormRateGraph.POS_X0;
            this.mListPos[0].Y = FormRateGraph.POS_Y0;
            this.mListPos[1].X = 1.0f - (float)(1.0f / Math.Sqrt(2.0f));
            this.mListPos[1].Y = 1.0f - (float)(1.0f / Math.Sqrt(2.0f));
            this.mListPos[2].X = FormRateGraph.POS_X2;
            this.mListPos[2].Y = FormRateGraph.POS_Y2;

            this.mListVec[0].X = 0.0f;
            this.mListVec[0].Y = -0.305f;
            this.mListVec[1].X = 0.155f;
            this.mListVec[1].Y = -0.155f;
            this.mListVec[2].X = 0.305f;
            this.mListVec[2].Y = 0.0f;

            this.mChange = true;

            this.panel_PreView.Refresh();
        }

        private void button_Rate3_Click(object sender, EventArgs e)
        {
            this.mListPos[0].X = FormRateGraph.POS_X0;
            this.mListPos[0].Y = FormRateGraph.POS_Y0;
            this.mListPos[1].X = (float)(1.0f / Math.Sqrt(2.0f));
            this.mListPos[1].Y = (float)(1.0f / Math.Sqrt(2.0f));
            this.mListPos[2].X = FormRateGraph.POS_X2;
            this.mListPos[2].Y = FormRateGraph.POS_Y2;

            this.mListVec[0].X = 0.305f;
            this.mListVec[0].Y = 0.0f;
            this.mListVec[1].X = 0.155f;
            this.mListVec[1].Y = -0.155f;
            this.mListVec[2].X = 0.0f;
            this.mListVec[2].Y = -0.305f;

            this.mChange = true;

            this.panel_PreView.Refresh();
        }

        private void checkBox_GridCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.panel_PreView.Refresh();
        }
    }
}
