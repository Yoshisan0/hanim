using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{
    public partial class FormImageCut : Form
    {
        private static readonly int WIDTH_THUMS = 64;  //サムネイル幅高
        private FormMain mFormMain = null;
        private bool mRectStart;
        private bool mRectEnd;
        private Point mPosStart;        //左上のポジション
        private Point mPosEnd;          //右下のポジション
        private Image mImage;           //オリジナルのイメージ
        private string mImagePath;
        private double mMag;
        private bool mMouseDown;        //マウスが押されたらtrueになる
        private bool mShiftDown;        //シフトキーが押されたらtrueになる
        private Point mOld;
        private Pen mPenGrid;
        private SolidBrush mBrushRect;
        private Color mBrushRectColor;

        public List<ClsDatCutImage> mListCutImage;  //カットしたイメージのリスト

        public FormImageCut(FormMain form, Image clImage, string fullpath)
        {
            this.mImagePath = fullpath;
            this.mListCutImage = new List<ClsDatCutImage>();

            InitializeComponent();

            //以下、初期化処理
            this.mFormMain = form;
            this.mRectStart = false;
            this.mRectEnd = false;
            this.mPosStart = new Point();
            this.mPosEnd = new Point();
            this.mImage = clImage;
            this.mMag = 1.0;
            this.mMouseDown = false;
            this.mShiftDown = false;
            this.mOld = new Point();
            this.mPenGrid = new Pen(Color.Silver);
            this.mBrushRectColor = Color.FromArgb(128, Color.Lime.R, Color.Lime.G, Color.Lime.B);
            this.mBrushRect = new SolidBrush(this.mBrushRectColor);
        }

        private void FormImageCut_Load(object sender, EventArgs e)
        {
            //以下、ウィンドウの設定
            this.Location = ClsSystem.mSetting.mWindowImageCut.mLocation;
            this.Size = ClsSystem.mSetting.mWindowImageCut.mSize;

            //以下、初期化処理
            this.DialogResult = DialogResult.None;
            this.comboBox_Mag.SelectedIndex = 0;
            this.panel_Image.GetType().InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, this.panel_Image, new object[] { true });
        }

        public Image GetImage()
        {
            return (this.mImage);
        }

        public Rectangle GetRectangle()
        {
            Rectangle stRect = new Rectangle();
            stRect.X = this.mPosStart.X;
            stRect.Y = this.mPosStart.Y;
            stRect.Width = this.mPosEnd.X - this.mPosStart.X;
            stRect.Height = this.mPosEnd.Y - this.mPosStart.Y;
            return (stRect);
        }

        private void FormImageCut_FormClosing(object sender, FormClosingEventArgs e)
        {
            //以下、ウィンドウ情報保存処理
            ClsSystem.mSetting.mWindowImageCut.mLocation = this.Location;
            ClsSystem.mSetting.mWindowImageCut.mSize = this.Size;

            //以下、カットされたイメージのリスト
            int inCnt, inMax = this.mListCutImage.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatCutImage clDatCutImage = this.mListCutImage[inCnt];
                clDatCutImage.Remove();
            }
            this.mListCutImage.Clear();
        }

        private void button_Cut_Click(object sender, EventArgs e)
        {
            //Start>End Swap
            int wrk = 0;
            if (mPosStart.X > mPosEnd.X)
            {
                wrk = mPosStart.X;
                mPosStart.X = mPosEnd.X;
                mPosEnd.X = wrk;
            }
            if (mPosStart.Y > mPosEnd.Y)
            {
                wrk = mPosStart.Y;
                mPosStart.Y = mPosEnd.Y;
                mPosEnd.Y = wrk;
            }
            //保護クリップ
            if (mPosStart.X < 0) mPosStart.X = 0;
            if (mPosStart.Y < 0) mPosStart.Y = 0;
            if (mPosEnd.X > mImage.Width) mPosEnd.X = mImage.Width;
            if (mPosEnd.Y > mImage.Height) mPosEnd.Y = mImage.Height;

            Size wh = new Size((mPosEnd.X - mPosStart.X), (mPosEnd.Y - mPosStart.Y));
            Rectangle r = new Rectangle(mPosStart, wh);

            //以下、イメージ切り取り処理
            Bitmap clBmpSrc = new Bitmap(this.mImage);
            ClsDatCutImage clDatCutImage = new ClsDatCutImage();
            clDatCutImage.mImage = clBmpSrc.Clone(r, clBmpSrc.PixelFormat);
            clDatCutImage.mSelect = false;
            this.mListCutImage.Add(clDatCutImage);

            this.panel_CellList.Height = (this.mListCutImage.Count/(panel_CellList.Width/ FormImageCut.WIDTH_THUMS))* FormImageCut.WIDTH_THUMS;
            this.splitContainerBase.Panel2.Refresh();
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            this.panel_Image.Refresh();
        }
        private void panel_Image_Paint(object sender, PaintEventArgs e)
        {
            //以下、拡大してボケないようにする処理
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            //以下、画像表示処理
            e.Graphics.DrawImage(this.mImage, 0.0f, 0.0f, this.panel_Image.Width, this.panel_Image.Height);

            //以下、選択用矩形表示処理
            if (this.mRectStart && this.mRectEnd)
            {
                int inX1 = 0;
                int inX2 = 0;
                int inY1 = 0;
                int inY2 = 0;

                if (this.mPosStart.X < this.mPosEnd.X)
                {
                    inX1 = (int)(this.mPosStart.X * this.mMag);
                    inX2 = (int)(this.mPosEnd.X * this.mMag);
                }
                else
                {
                    inX1 = (int)(this.mPosEnd.X * this.mMag);
                    inX2 = (int)(this.mPosStart.X * this.mMag);
                }

                if (this.mPosStart.Y < this.mPosEnd.Y)
                {
                    inY1 = (int)(this.mPosStart.Y * this.mMag);
                    inY2 = (int)(this.mPosEnd.Y * this.mMag);
                }
                else
                {
                    inY1 = (int)(this.mPosEnd.Y * this.mMag);
                    inY2 = (int)(this.mPosStart.Y * this.mMag);
                }

                e.Graphics.FillRectangle(this.mBrushRect, inX1, inY1, inX2 - inX1, inY2 - inY1);
            }

            //以下、グリッド表示処理
            if (this.checkBox_Grid.Checked)
            {
                int inGridX = (int)numericUpDown_DivX.Value;
                int inGridY = (int)numericUpDown_DivY.Value;

                int inCnt, inMax = this.mImage.Width / inGridX;
                for (inCnt = 1; inCnt < inMax; inCnt++)
                {
                    int inX = (int)(inCnt * inGridX * this.mMag);
                    Point stPos1 = new Point(inX, 0);
                    Point stPos2 = new Point(inX, this.panel_Image.Height - 1);
                    e.Graphics.DrawLine(this.mPenGrid, stPos1, stPos2);
                }

                inMax = this.mImage.Height / inGridY;
                for (inCnt = 1; inCnt < inMax; inCnt++)
                {
                    int inY = (int)(inCnt * inGridY * this.mMag);
                    Point stPos1 = new Point(0, inY);
                    Point stPos2 = new Point(this.panel_Image.Width - 1, inY);
                    e.Graphics.DrawLine(this.mPenGrid, stPos1, stPos2);
                }
            }
        }
        private void panel_Image_MouseDown(object sender, MouseEventArgs e)
        {
            this.mOld.X = Cursor.Position.X;
            this.mOld.Y = Cursor.Position.Y;
            this.mMouseDown = true;

            Point stPos = this.panel_Image.PointToClient(Cursor.Position);
            if (this.checkBox_Magnet.Checked)
            {
                double doValX = (double)numericUpDown_DivX.Value * this.mMag;
                double doValY = (double)numericUpDown_DivY.Value * this.mMag;

                stPos.X = (int)Math.Round((decimal)(stPos.X / doValX)) * (int)doValX;
                stPos.Y = (int)Math.Round((decimal)(stPos.Y / doValY)) * (int)doValY;
            }

            this.mRectStart = true;
            this.mPosStart.X = (int)(stPos.X / this.mMag);
            this.mPosStart.Y = (int)(stPos.Y / this.mMag);
            this.mRectEnd = false;
            this.mPosEnd.X = (int)(stPos.X / this.mMag);
            this.mPosEnd.Y = (int)(stPos.Y / this.mMag);
            this.mPosEnd = new Point();

            this.panel.Refresh();
        }
        private void panel_Image_MouseUp(object sender, MouseEventArgs e)
        {
            //終了点確定
            if (mMouseDown)
            {
                this.mMouseDown = false;
            }
        }

        private void FormImageCut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey)
            {
                if (!this.mShiftDown)
                {
                    this.panel.Cursor = Cursors.NoMove2D;
                    this.mShiftDown = true;
                }
            }
        }
        private void FormImageCut_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey)
            {
                this.panel.Cursor = Cursors.Default;
                this.mShiftDown = false;
            }
        }
        private void timerMain_Tick(object sender, EventArgs e)
        {
            if (this.mMouseDown)
            {
                if (this.mShiftDown)
                {
                    //以下、画像移動処理
                    if (this.mOld.X != Cursor.Position.X || this.mOld.Y != Cursor.Position.Y)
                    {
                        int inX = Cursor.Position.X;
                        int inY = Cursor.Position.Y;
                        int inDiffX = inX - this.mOld.X;
                        int inDiffY = inY - this.mOld.Y;
                        this.panel.AutoScrollPosition = new Point(-this.panel.AutoScrollPosition.X - inDiffX, -this.panel.AutoScrollPosition.Y - inDiffY);
                        this.mOld.X = inX;
                        this.mOld.Y = inY;

                        this.panel.Refresh();
                    }
                }
                else
                {
                    //以下、矩形表示処理
                    if (this.mOld.X != Cursor.Position.X || this.mOld.Y != Cursor.Position.Y)
                    {
                        if (this.mRectStart)
                        {
                            Point stPos = this.panel_Image.PointToClient(Cursor.Position);
                            if (this.checkBox_Magnet.Checked)
                            {
                                double doValX = (double)numericUpDown_DivX.Value * this.mMag;
                                double doValY = (double)numericUpDown_DivY.Value * this.mMag;

                                stPos.X = (int)Math.Round((decimal)(stPos.X / doValX)) * (int)doValX;
                                stPos.Y = (int)Math.Round((decimal)(stPos.Y / doValY)) * (int)doValY;
                            }

                            this.mRectEnd = true;
                            this.mPosEnd.X = (int)(stPos.X / this.mMag);
                            this.mPosEnd.Y = (int)(stPos.Y / this.mMag);

                            this.panel.Refresh();
                        }
                    }
                }
            }

            bool isEnable = false;
            if (this.mRectStart && this.mRectEnd)
            {
                if (this.mPosStart.X != this.mPosEnd.X ||
                    this.mPosStart.Y != this.mPosEnd.Y)
                {
                    isEnable = true;
                }
            }
            this.button_Cut.Enabled = isEnable;
        }

        private void checkBox_Grid_CheckedChanged(object sender, EventArgs e)
        {
            this.panel.Refresh();
        }
        private void comboBox_Mag_SelectedIndexChanged(object sender, EventArgs e)
        {
            Match clMatch = Regex.Match(this.comboBox_Mag.Text, "^(\\d*?)%$");
            if (clMatch.Success)
            {
                int inMag = Convert.ToInt32(clMatch.Groups[1].Value);
                this.mMag = inMag / 100.0;
            }

            this.panel_Image.Width = (int)(this.mImage.Width * this.mMag);
            this.panel_Image.Height = (int)(this.mImage.Height * this.mMag);

            this.panel.Refresh();
        }
        private void UD_DivXY_ValueChanged(object sender, EventArgs e)
        {
            panel_Image.Refresh();
        }
        private void panel_ColorGrid_Click(object sender, EventArgs e)
        {
            ColorDialog clDialog = new ColorDialog();
            clDialog.Color = this.mPenGrid.Color;
            DialogResult enResult = clDialog.ShowDialog();
            if (enResult != DialogResult.OK) return;

            this.mPenGrid = new Pen(clDialog.Color);
            this.panel_ColorGrid.BackColor = clDialog.Color;

            this.panel_Image.Refresh();
        }
        private void panel_ColorRect_Click(object sender, EventArgs e)
        {
            ColorDialog clDialog = new ColorDialog();
            clDialog.Color = this.mBrushRect.Color;
            DialogResult enResult = clDialog.ShowDialog();
            if (enResult != DialogResult.OK) return;

            this.mBrushRectColor = Color.FromArgb(128, clDialog.Color.R, clDialog.Color.G, clDialog.Color.B);
            this.mBrushRect = new SolidBrush(this.mBrushRectColor);
            this.panel_ColorRect.BackColor = clDialog.Color;

            this.panel_Image.Refresh();
        }

        private void panel_CellList_Paint(object sender, PaintEventArgs e)
        {
            //DrawCells自力描画でがんばる
            //描画開始番号
            //Selectedは枠色で表現?網かけ？
            int cnt = 0;//初期インデックス
            int bSize = FormImageCut.WIDTH_THUMS;//BoxSize
            //int drawXPos = 0;//描画横幅累計 コンポーネント横幅まで描画
            //int drawYPos = 0;//

            int cvx = panel_CellList.Width  / bSize; //横に並ぶ数
            int cvy = panel_CellList.Height / bSize; //縦に並ぶ数
            //ImageManager.CellList.Count / cx;
            if(this.mListCutImage.Count <= 0) return;

            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            //サムネイル表示はセレクトの関係から縦横固定サイズが望ましい

            for(int cy =0 ; cy< cvy; cy++)
            {
                for(int cx = 0; cx < cvx;cx++)
                {
                    if (cnt < this.mListCutImage.Count)
                    {
                        ClsDatCutImage clDatCutImage = this.mListCutImage[cnt];
                        if (clDatCutImage == null)
                        {
                            Console.Out.Write("CellImage is Null");
                            return;
                        }

                        //Thumbnail Resize
                        float rw = (float)bSize / clDatCutImage.mImage.Width;
                        float rh = (float)bSize / clDatCutImage.mImage.Height;
                        float rf = Math.Min(rw, rh);
                        Point ds = new Point((int)(clDatCutImage.mImage.Width * rf), (int)(clDatCutImage.mImage.Height * rf));

                        e.Graphics.DrawImage(clDatCutImage.mImage, (cx*bSize) + (bSize / 2 - (ds.X / 2)), (cy*bSize) + (bSize / 2 - (ds.Y / 2)), ds.X, ds.Y);

                        //DrawFlame
                        if (this.mListCutImage[cnt].mSelect)
                        {
                            e.Graphics.DrawRectangle(Pens.GreenYellow, new Rectangle(cx*bSize,cy*bSize, bSize - 1, bSize - 1));
                        }
                        else
                        {
                            e.Graphics.DrawRectangle(Pens.Brown, new Rectangle(cx*bSize,cy*bSize , bSize - 1, bSize - 1));
                        }
                    }
                    cnt++;
                }
            }
        }
        private void panel_CellList_MouseUp(object sender, MouseEventArgs e)
        {
            //Cell Select
            int selX = e.X / FormImageCut.WIDTH_THUMS;
            int selY = e.Y / FormImageCut.WIDTH_THUMS;
            int sel = (selY * (panel_CellList.Width/ FormImageCut.WIDTH_THUMS)) + selX;
            if (sel < this.mListCutImage.Count)
            {
                this.mListCutImage[sel].mSelect = !this.mListCutImage[sel].mSelect;
                this.splitContainerBase.Refresh();
            }
        }

        //CEll削除 セレクトしたもののみ削除
        private void button_CellDelete_Click(object sender, EventArgs e)
        {
            //
//            ImageManager.RemoveSelectedCell();
            //※ここで編集中のイメージを削除する？

            splitContainerBase.Refresh();
        }

        private void button_Divid_Click(object sender, EventArgs e)
        {
            //等間隔分割
            int dx = (int)numericUpDown_DivX.Value;
            int dy = (int)numericUpDown_DivY.Value;

            for (int cy=0;cy<(mImage.Height/dy);cy++)
            {
                for (int cx=0;cx<(mImage.Width/dx);cx++)
                {
                    Rectangle r = new Rectangle(cx*dx,cy*dy,dx,dy);
                    ClsDatImage c = new ClsDatImage();
                    c.mRect = r;
                    c.mName = cy.ToString("00") +":"+ cx.ToString("00");
//                    ImageManager.AddImageChipFromImage(mImage, c);
                    //※ここでイメージをカットする？
                }
            }
            mPosStart = new Point(0, 0);
            mPosEnd = new Point(dx, dy);//仮

            panel_CellList.Width = splitContainerBase.ClientSize.Width;
            panel_CellList.Height = (this.mListCutImage.Count / (panel_CellList.Width / FormImageCut.WIDTH_THUMS) + 1) * FormImageCut.WIDTH_THUMS;
            //splitContainerBase.Refresh();
        }
        private void button_Clear_Click(object sender, EventArgs e)
        {
            //全セルの削除
            int inCnt, inMax = this.mListCutImage.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatCutImage clDatCutImage = this.mListCutImage[inCnt];
                clDatCutImage.Remove();
            }
            this.mListCutImage.Clear();

            splitContainerBase.Refresh();
        }
        private void CellSave_Click(object sender, EventArgs e)
        {
            if(this.mListCutImage.Count >0)
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.DefaultExt = "cell";
                if (sd.ShowDialog() == DialogResult.OK)
                {
//                    ImageManager.SaveToFile(sd.FileName,"");
                    //※ここで編集中のイメージを保存する？
                    //というかこの機能は必要？

                    //ImageManager.GetImageChipFromIndex(0).ToBinaryFile(sd.FileName);
                }
            }
        }

        private void splitContainerBase_Panel2_Resize(object sender, EventArgs e)
        {
            panel_CellList.Width = splitContainerBase.ClientSize.Width;
            panel_CellList.Height = (this.mListCutImage.Count / (panel_CellList.Width / FormImageCut.WIDTH_THUMS) +1) * FormImageCut.WIDTH_THUMS;
            splitContainerBase.Refresh();
        }
    }

    public class ClsDatCutImage
    {
        public Image mImage;
        public bool mSelect;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClsDatCutImage()
        {
            this.mImage = null;
            this.mSelect = false;
        }

        /// <summary>
        /// イメージ削除処理
        /// </summary>
        public void Remove()
        {
            if (this.mImage != null)
            {
                this.mImage.Dispose();
                this.mImage = null;
            }
        }
    }
}
