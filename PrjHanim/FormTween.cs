﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{
    public partial class FormTween : Form
    {
        private bool mPush;                 //マウスを押しているかどうかのフラグ
        private int mFrmStart;              //開始フレーム
        private int mFrmEnd;                //終了フレーム
        private int mFrmCurrent;            //カレントフレーム
        private Pen mPenRed;                //赤いラインのペン
        private Pen mPenForce;              //ベクトルのペン
        private Pen mPenGraph;              //ラインのペン
        private Pen mPenGrid;               //グリッドのペン
        private Pen mPenCenterLine;         //カレントフレームのペン
        private List<ClsVector3> mListPos;  //ポイントのリスト
        private List<ClsVector3> mListVec;  //ベクトルのリスト
        private int mGripNo;                //掴んでいるポイントの番号(0:掴んでいない 1:中間ポイント 2:始点のベクトル 3:中間点のベクトル 4:中間点の左下ベクトル 5:終点の左下ベクトル)
        private Bitmap mImage0;             //イメージ
        private Bitmap mImage1;             //イメージ
        private bool mChange;               //変更フラグ
        private FormMain mFormMain;         //メインフォーム
        private EnmParam mParam;            //パラメーター種別

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="clForm">メインフォーム</param>
        /// <param name="enParam">種別</param>
        /// <param name="inFrmStart">開始フレーム</param>
        /// <param name="inFrmEnd">終了フレーム</param>
        /// <param name="inFrmCurrent">カレントフレーム</param>
        public FormTween(FormMain clForm, EnmParam enParam, int inFrmStart, int inFrmEnd, int inFrmCurrent)
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

            this.mListPos = new List<ClsVector3>();
            this.mListPos.Add(new ClsVector3(ClsDatTween.POS_X0, ClsDatTween.POS_Y0, 0.0f));
            this.mListPos.Add(new ClsVector3(ClsDatTween.POS_X1, ClsDatTween.POS_Y1, 0.0f));
            this.mListPos.Add(new ClsVector3(ClsDatTween.POS_X2, ClsDatTween.POS_Y2, 0.0f));

            this.mListVec = new List<ClsVector3>();
            this.mListVec.Add(new ClsVector3(ClsDatTween.VEC_X0, ClsDatTween.VEC_Y0, 0.0f));
            this.mListVec.Add(new ClsVector3(ClsDatTween.VEC_X1, ClsDatTween.VEC_Y1, 0.0f));
            this.mListVec.Add(new ClsVector3(ClsDatTween.VEC_X2, ClsDatTween.VEC_Y2, 0.0f));

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
        public FormTween(FormMain clForm, EnmParam enParam, int inFrmStart, int inFrmEnd, int inFrmCurrent, ClsVector3 clPos, List<ClsVector3> clListVec)
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

            this.mListPos = new List<ClsVector3>();
            this.mListPos.Add(new ClsVector3(ClsDatTween.POS_X0, ClsDatTween.POS_Y0, 0.0f));
            this.mListPos.Add(new ClsVector3(clPos.X, clPos.Y, clPos.Z));
            this.mListPos.Add(new ClsVector3(ClsDatTween.POS_X2, ClsDatTween.POS_Y2, 0.0f));

            this.mListVec = new List<ClsVector3>();
            int inCnt;
            for (inCnt = 0; inCnt < 3; inCnt++)
            {
                this.mListVec.Add(new ClsVector3(clListVec[inCnt].X, clListVec[inCnt].Y, clListVec[inCnt].Z));
            }

            //panel_PreView.DoubleBuuferd = true;
            panel_PreView.GetType().InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, panel_PreView, new object[] { true });
        }

        private void FormRateGraph_Load(object sender, EventArgs e)
        {
            //以下、ウィンドウの設定
            this.Location = ClsSystem.mSetting.mWindowRateGraph.mLocation;
            this.Size = ClsSystem.mSetting.mWindowRateGraph.mSize;

            //以下、コントロール初期化処理
            this.checkBox_GridCheck.Checked = ClsSystem.mSetting.mWindowRateGraph_DrawGrid;

            this.mPenRed = new Pen(Color.Red, 0.5f);
            this.mPenForce = new Pen(ClsSystem.mSetting.mRateGraphColorForce);
            this.mPenGraph = new Pen(ClsSystem.mSetting.mRateGraphColorGraph);
            this.mPenGrid = new Pen(ClsSystem.mSetting.mRateGraphColorGrid);
            this.mPenCenterLine = new Pen(ClsSystem.mSetting.mRateGraphColorCenterLine);

            this.mImage0 = new Bitmap(this.panel_PreView.Width, this.panel_PreView.Height);
            this.mImage1 = new Bitmap(this.panel_PreView.Width, this.panel_PreView.Height);

            this.mChange = true;
            this.panel_PreView.Refresh();
        }

        private void FormRateGraph_FormClosing(object sender, FormClosingEventArgs e)
        {
            //以下、ウィンドウ情報保存処理
            ClsSystem.mSetting.mWindowRateGraph.mLocation = this.Location;
            ClsSystem.mSetting.mWindowRateGraph.mSize = this.Size;

            //以下、コントロール保存処理
            ClsSystem.mSetting.mWindowRateGraph_DrawGrid = this.checkBox_GridCheck.Checked;
        }

        /// <summary>
        /// 重みを返す処理
        /// </summary>
        /// <param name="clDatTween">トゥイーン情報</param>
        /// <param name="puchRate">重みリスト</param>
        /// <param name="inFrmCurrent">カレントフレーム</param>
        /// <returns>重み(0.0～1.0)</returns>
        public static float GetRate(ClsDatKeyFrame clDatKeyFrame, ClsDatTween clDatTween, byte[] puchRate, int inFrmCurrent)
        {
            if (clDatTween == null) return (0.0f);
            if (clDatTween.mRate == null) return (0.0f);

            if (!(clDatKeyFrame.mFrameNo <= inFrmCurrent && inFrmCurrent <= clDatKeyFrame.mFrameNo + clDatTween.mLength)) return (0.0f);

            float flFrmLength = clDatTween.mLength;
            inFrmCurrent -= clDatKeyFrame.mFrameNo;
            byte uchRate = (byte)((float)inFrmCurrent / flFrmLength * ClsDatTween.MAX_WEIGHT);
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
        public static void CreateTweenWeight(ClsDatTween clTween)
        {
            if (clTween == null) return;

            byte[] puchRate = new byte[ClsDatTween.MAX_WEIGHT];

            int inWidth = ClsDatTween.MAX_WEIGHT;
            int inHeight = byte.MaxValue;
            Bitmap clImage = ClsDatTween.CreateImage(clTween, inWidth, inHeight);

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

            clTween.mRate = puchRate;
        }

        /// <summary>
        /// トゥイーン情報の取得
        /// </summary>
        /// <returns>トゥイーン情報</returns>
        public ClsDatTween GetTween()
        {
            ClsDatTween clTween = new ClsDatTween(this.mParam, this.mFrmEnd - this.mFrmStart, this.mListPos[1], this.mListVec);
            return (clTween);
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

            this.panel_PreView.BackColor = ClsSystem.mSetting.mRateGraphColorBack;

            this.panel_PreView.Refresh();
        }

        private void button_ColorLine_Click(object sender, EventArgs e)
        {
            this.SetColor(sender as Button);

            this.mPenGraph = new Pen(ClsSystem.mSetting.mRateGraphColorGraph, 2.0f);

            this.mChange = true;
            this.panel_PreView.Refresh();
        }

        private void button_ColorGraph_Click(object sender, EventArgs e)
        {
            this.SetColor(sender as Button);

            this.mPenForce = new Pen(ClsSystem.mSetting.mRateGraphColorForce);

            this.mChange = true;
            this.panel_PreView.Refresh();
        }

        private void button_ColorGrid_Click(object sender, EventArgs e)
        {
            this.SetColor(sender as Button);

            this.mPenGrid = new Pen(ClsSystem.mSetting.mRateGraphColorGrid);

            this.panel_PreView.Refresh();
        }

        private void button_ColorCurrent_Click(object sender, EventArgs e)
        {
            this.SetColor(sender as Button);

            this.mPenCenterLine = new Pen(ClsSystem.mSetting.mRateGraphColorCenterLine);

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
            g.DrawLine(this.mPenForce, flX0, flY0, flX1, flY1);

            //以下、矢印先の円を描画する処理
            this.DrawCircle(g, clPenCircle, flX1, flY1);
        }

        private void DrawCircle(Graphics g, Pen clPen, float flX, float flY)
        {
            g.DrawEllipse(clPen, flX - ClsDatTween.SIZE_ELLIPSE / 2, flY - ClsDatTween.SIZE_ELLIPSE / 2, ClsDatTween.SIZE_ELLIPSE, ClsDatTween.SIZE_ELLIPSE);
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

                        g.DrawLine(this.mPenGraph, inXOld, inYOld, inX, inY);

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
            e.Graphics.Clear(ClsSystem.mSetting.mRateGraphColorBack);

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
                        e.Graphics.DrawLine(this.mPenCenterLine, inCnt * flSpan, 0.0f, inCnt * flSpan, this.panel_PreView.Height);
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
            Pen clPen = (this.mGripNo == 1) ? Pens.Red : this.mPenForce;
            this.DrawCircle(e.Graphics, clPen, flX, flY);

            //以下、ベクトル描画処理
            clPen = (this.mGripNo == 2) ? Pens.Red : this.mPenForce;
            this.DrawArrow(e.Graphics, clPen, this.mListPos[0].X, this.mListPos[0].Y, this.mListVec[0].X, this.mListVec[0].Y);
            clPen = (this.mGripNo == 3) ? Pens.Red : this.mPenForce;
            this.DrawArrow(e.Graphics, clPen, this.mListPos[1].X, this.mListPos[1].Y, this.mListVec[1].X, this.mListVec[1].Y);
            clPen = (this.mGripNo == 4) ? Pens.Red : this.mPenForce;
            this.DrawArrow(e.Graphics, clPen, this.mListPos[1].X, this.mListPos[1].Y, -this.mListVec[1].X, -this.mListVec[1].Y);
            clPen = (this.mGripNo == 5) ? Pens.Red : this.mPenForce;
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
                bool isHit = this.IsHitPointAndCircle(e.X, e.Y, flX, flY, ClsDatTween.SIZE_ELLIPSE / 2);
                if (isHit) this.mGripNo = 1;

                //以下、始点のベクトルチェック処理
                flX = this.mListPos[0].X * this.panel_PreView.Width;
                flY = this.mListPos[0].Y * this.panel_PreView.Height;
                flX += this.mListVec[0].X * this.panel_PreView.Width;
                flY += this.mListVec[0].Y * this.panel_PreView.Height;
                isHit = this.IsHitPointAndCircle(e.X, e.Y, flX, flY, ClsDatTween.SIZE_ELLIPSE / 2);
                if (isHit) this.mGripNo = 2;

                //以下、中間点のベクトルチェック処理
                flX = this.mListPos[1].X * this.panel_PreView.Width;
                flY = this.mListPos[1].Y * this.panel_PreView.Height;
                flX += this.mListVec[1].X * this.panel_PreView.Width;
                flY += this.mListVec[1].Y * this.panel_PreView.Height;
                isHit = this.IsHitPointAndCircle(e.X, e.Y, flX, flY, ClsDatTween.SIZE_ELLIPSE / 2);
                if (isHit) this.mGripNo = 3;

                //以下、中間点の左下ベクトルチェック処理
                flX = this.mListPos[1].X * this.panel_PreView.Width;
                flY = this.mListPos[1].Y * this.panel_PreView.Height;
                flX += -this.mListVec[1].X * this.panel_PreView.Width;
                flY += -this.mListVec[1].Y * this.panel_PreView.Height;
                isHit = this.IsHitPointAndCircle(e.X, e.Y, flX, flY, ClsDatTween.SIZE_ELLIPSE / 2);
                if (isHit) this.mGripNo = 4;

                //以下、終点の左下ベクトルチェック処理
                flX = this.mListPos[2].X * this.panel_PreView.Width;
                flY = this.mListPos[2].Y * this.panel_PreView.Height;
                flX += -this.mListVec[2].X * this.panel_PreView.Width;
                flY += -this.mListVec[2].Y * this.panel_PreView.Height;
                isHit = this.IsHitPointAndCircle(e.X, e.Y, flX, flY, ClsDatTween.SIZE_ELLIPSE / 2);
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
            this.mListPos[0].X = ClsDatTween.POS_X0;
            this.mListPos[0].Y = ClsDatTween.POS_Y0;
            this.mListPos[1].X = ClsDatTween.POS_X1;
            this.mListPos[1].Y = ClsDatTween.POS_Y1;
            this.mListPos[2].X = ClsDatTween.POS_X2;
            this.mListPos[2].Y = ClsDatTween.POS_Y2;

            this.mListVec[0].X = ClsDatTween.VEC_X0;
            this.mListVec[0].Y = ClsDatTween.VEC_Y0;
            this.mListVec[1].X = ClsDatTween.VEC_X1;
            this.mListVec[1].Y = ClsDatTween.VEC_Y1;
            this.mListVec[2].X = ClsDatTween.VEC_X2;
            this.mListVec[2].Y = ClsDatTween.VEC_Y2;

            this.mChange = true;

            this.panel_PreView.Refresh();
        }

        private void button_Rate2_Click(object sender, EventArgs e)
        {
            this.mListPos[0].X = ClsDatTween.POS_X0;
            this.mListPos[0].Y = ClsDatTween.POS_Y0;
            this.mListPos[1].X = 1.0f - (float)(1.0f / Math.Sqrt(2.0f));
            this.mListPos[1].Y = 1.0f - (float)(1.0f / Math.Sqrt(2.0f));
            this.mListPos[2].X = ClsDatTween.POS_X2;
            this.mListPos[2].Y = ClsDatTween.POS_Y2;

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
            this.mListPos[0].X = ClsDatTween.POS_X0;
            this.mListPos[0].Y = ClsDatTween.POS_Y0;
            this.mListPos[1].X = (float)(1.0f / Math.Sqrt(2.0f));
            this.mListPos[1].Y = (float)(1.0f / Math.Sqrt(2.0f));
            this.mListPos[2].X = ClsDatTween.POS_X2;
            this.mListPos[2].Y = ClsDatTween.POS_Y2;

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

        private void button_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
