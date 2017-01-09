using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{
    /// <summary>
    /// 左ObjectTree
    /// 右TimeLine
    /// </summary>
    
    //Memo
    //このフォームはタイムライン操作に専念する
    /* 左ペイン
     Elements 選択 入れ替え ロック 表示スイッチ

     */
     /*
     name:操作系
     TimeLine範囲ドラッグ可能
     Cut Copy Paste Fill
     ダブルクリック ->キーフレームをそこに作成
     Delキー 現在のフレームを削除
     Insキー 現在のフレームに挿入
      
     */
      
     /*右ペイン
      * mListLine:
      フレーム操作:
      前後移動:左右カーソル

      キーフレーム登録(Enter)/削除(Del)
      範囲　削除/追加/挿入

      */
    public partial class FormControl : Form
    {
        private static readonly int HEAD_HEIGHT = 20;
        private static readonly int TIME_CELL_HEIGHT = 18;
        private static readonly int TIME_CELL_WIDTH = 12;

        private int mSelectElementKey;
        private Point mSelect_Pos_Start;
        private Point mSelect_Pos_End;
        private bool mMouseDownL=false;
        //private bool mMouseDownR=false;
        //private bool mMouseDownM=false;

        //メインフォームにセットしてもらう
        //全状態を間接参照する
        private ClsDatMotion mMotion = null;
        private FormMain mFormMain = null;

        private Font mFont = null;

        public FormControl(FormMain form)
        {
            this.mFormMain = form;

            InitializeComponent();

            //ダブルバッファ強制有効化
            panel_Time.GetType().InvokeMember("DoubleBuffered",BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,null,panel_Time,new object[] { true });
        }

        private void FormControl_Load(object sender, EventArgs e)
        {
            //以下、ウィンドウの設定
            this.Location = ClsSystem.mSetting.mWindowControl.mLocation;
            this.Size = ClsSystem.mSetting.mWindowControl.mSize;

            //以下、初期化処理
            this.mSelectElementKey = -1;
            this.mFont = new Font("ＭＳ ゴシック", 10.5f);
            this.panel_Time.Width = TIME_CELL_WIDTH * (int)numericUpDown_MaxFrame.Value;
            this.panel_Time.Height =HEAD_HEIGHT*5;          
        }

        /// <summary>
        /// ウィンドウ名設定
        /// </summary>
        /// <param name="clMotion">選択中のモーション管理クラス</param>
        public void SetName(ClsDatMotion clMotion)
        {
            this.Text = ClsSystem.GetWindowName("Control", clMotion);
        }

        public int GetElementSelectKey()
        {
            return (this.mSelectElementKey);
        }

        public void RemoveElementFromKey(int inElementKey)
        {
            if (this.mMotion == null) return;

            this.mMotion.RemoveElemFromIndex(inElementKey);

            if (inElementKey == this.mSelectElementKey)
            {
                this.mSelectElementKey = -1;
            }

            RefreshAll();
        }

        /// <summary>
        /// モーションの設定や変更、再設定(描画更新も行う
        /// </summary>
        /// <param name="clMotion">モーション管理クラス</param>
        public void SetMotion(ClsDatMotion clMotion)
        {
            this.mMotion = clMotion;

            //以下画面更新予定　タイミングによってはguiがまだ準備できていない場合もありうる？
            //Formの準備ができているか確認してリフレッシュ
            RefreshAll();
        }

        private void FormControl_DragEnter(object sender, DragEventArgs e)
        {
            //このフォームは操作に専念するため外部からのD&Dは不要に
            //D&Dはフォーム上のフレーム複製や移動に専念する
            /*
            IntPtr pinDesktopWindow = ClsWin32.GetDesktopWindow();
            ClsWin32.ImageList_DragEnter(pinDesktopWindow, Cursor.Position.X, Cursor.Position.Y);

            bool isSuccess = e.Data.GetDataPresent(typeof(ListViewItem[]));
            if (isSuccess)
            {
                //以下、ドラッグ＆ドロップを有効化する処理
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
            */
        }
        private void FormControl_DragDrop(object sender, DragEventArgs e)
        {
            /*
            IntPtr pinDesktopWindow = ClsWin32.GetDesktopWindow();
            ClsWin32.ImageList_DragLeave(pinDesktopWindow);

            if (e.Data.GetDataPresent(typeof(ListViewItem[])))
            {
                //以下、ドラッグ＆ドロップ処理
                ListViewItem[] pclItems = (ListViewItem[])e.Data.GetData(typeof(ListViewItem[]));
                foreach (ListViewItem clItem in pclItems)
                {
                    string clKey = clItem.SubItems[2].Text;

                    bool isExist = ClsTool.mTblImage.ContainsKey(clKey);
                    if(!isExist) continue;

                    ClsLine clLine = new ClsLine();
                    clLine.mMD5 = clKey;
                    clLine.mName = clKey;
                    this.mListLine.Add(clLine);
                }
            }
            */
        }

        private void splitContainer_Panel1_Scroll(object sender, ScrollEventArgs e)
        {
            if(e.ScrollOrientation== ScrollOrientation.VerticalScroll)
            {
                this.splitContainer.Panel2.VerticalScroll.Value = e.NewValue;
            }
        }
        private void splitContainer_Panel2_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                this.splitContainer.Panel1.VerticalScroll.Value = e.NewValue;
            }
        }
        private void MaxFrame_ValueChanged(object sender, EventArgs e)
        {
            int inFrameNum = (int)this.numericUpDown_MaxFrame.Value;
            this.mMotion.SetFrameNum(inFrameNum);

            int inWidth = inFrameNum * FormControl.TIME_CELL_WIDTH + 1;
            this.panel_Time.Width = inWidth;
            this.panel_Time.Refresh();
        }
        private void NowFrame_ValueChanged(object sender, EventArgs e)
        {
            //現在フレームが変更された時の処理
            RefreshAll();

        }
        private void RefreshAll()
        {
            //全体再描画
            //panel_Control.Refresh();
            panel_Time.Refresh();
            LineHeader.Refresh();
        }
        private void ButtonPrev_Click(object sender, EventArgs e)
        {
            numericUpDown_NowFlame.Value = 0;
        }
        private void ButtonFore_Click(object sender, EventArgs e)
        {
            numericUpDown_NowFlame.Value = numericUpDown_MaxFrame.Value;
        }
        private void LineHeader_Paint(object sender, PaintEventArgs e)
        {
            //LulerLine 目盛
            Brush numBrush = Brushes.AntiqueWhite;
            for (int cx = 1; cx < (LineHeader.Width / TIME_CELL_WIDTH); cx++)
            {
                if (cx % 5 == 0)
                {
                    e.Graphics.DrawLine(Pens.Orange, cx * TIME_CELL_WIDTH, LineHeader.Height / 4, cx * TIME_CELL_WIDTH, LineHeader.Height - 1);
                    e.Graphics.DrawString(cx.ToString(), Font, Brushes.AntiqueWhite, new Point((cx - 1) * TIME_CELL_WIDTH, 0));
                }
                else
                {
                    e.Graphics.DrawLine(Pens.Green, cx * TIME_CELL_WIDTH, LineHeader.Height / 2, cx * TIME_CELL_WIDTH, LineHeader.Height - 1);
                }
            }
            //NowPotision 矢印
            e.Graphics.DrawImage(Properties.Resources.LineMarker, new Point(TIME_CELL_WIDTH * (int)numericUpDown_NowFlame.Value + (TIME_CELL_WIDTH / 2) - 4, 2));
        }

        //Left  Paine
        private void panel_Control_DragDrop(object sender, DragEventArgs e)
        {
            //Itemの移動?
        }
        private void panel_Control_MouseClick(object sender, MouseEventArgs e)
        {
            //※ e.Y がそのまま mListElem のインデックスになるわけではないので、ここは修正する必要があります

            //Item選択
            var work = e.Y / TIME_CELL_HEIGHT;
            //Item最大数を確認
            if (work < this.mMotion.mListElem.Count)
            {
                ClsDatElem ele = this.mMotion.mListElem[work];
                mSelectElementKey = work;

                //Click Eye
                if (e.X < 16) {
                    ele.isVisible = !ele.isVisible;
                }

                //Click Locked
                if (e.X > 16 && e.X < 32) {
                    ele.isLocked = !ele.isLocked;
                }

                //Attribute Open
                if (e.X > 32 && e.X < 48) {
                    ele.isOpen = !ele.isOpen;

                    this.mMotion.Assignment();    //行番号とタブを割り振る処理
                }

                //SelectElements
                if (e.X > 48) { ele.isSelect = !ele.isSelect; }

                panel_Control.Refresh();
                panel_Time.Refresh();
                mFormMain.Refresh();
            }
        }
        private void panel_Control_MouseEnter(object sender, EventArgs e)
        {

        }
        private void panel_Control_MouseLeave(object sender, EventArgs e)
        {

        }
        private void panel_Control_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void panel_Control_Paint(object sender, PaintEventArgs e)
        {
            int inWidth = this.panel_Control.Width;
            int inHeight = this.panel_Control.Height;

            //以下、イメージリスト描画処理
            e.Graphics.Clear(Color.Black);

//※ここはthis.mMotionをe.Graphicsで描画しやすいように並べて（各クラスのmLineNoを見て）描画するようにします
//※トリッキーなやり方としては、this.mMotionにe.Graphicsを渡してやって内部で描画する

            //以下、横ライン描画処理
            int inY = TIME_CELL_HEIGHT;
            //e.Graphics.DrawLine(Pens.Black, 0, inY, inWidth, inY);

            int inMax = this.mMotion.mListElem.Count;
            for (int inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem ele = this.mMotion.mListElem[inCnt];
                SolidBrush sb= new SolidBrush(Color.Black);
                if (ele == null) continue;
                //背景塗り
                if(inCnt %2 !=0)
                {
                    sb = new SolidBrush(Color.FromArgb(0xff, 30, 30, 40));
                    e.Graphics.FillRectangle(sb, 0, inCnt * TIME_CELL_HEIGHT, panel_Control.Width, TIME_CELL_HEIGHT - 1);
                }
                if (ele.isSelect)
                {
                    //選択中Elementsの背景強調
                    sb = new SolidBrush(Color.FromArgb(128,Color.Green));
                    e.Graphics.FillRectangle(sb, 0, inCnt * TIME_CELL_HEIGHT, panel_Control.Width, TIME_CELL_HEIGHT - 1);
                }
                //ステートマーク 目
                if (ele.isVisible)
                {
                    e.Graphics.DrawImage(Properties.Resources.see, 2, inCnt * TIME_CELL_HEIGHT);
                }
                else
                {
                    e.Graphics.DrawImage(Properties.Resources.unSee, 2, inCnt * TIME_CELL_HEIGHT);
                }
                //ステートマーク 鍵
                if (ele.isLocked)
                {
                    e.Graphics.DrawImage(Properties.Resources.locked, 2+16, inCnt * TIME_CELL_HEIGHT);
                }
                else
                {
                    e.Graphics.DrawImage(Properties.Resources.unLock, 2+16, inCnt * TIME_CELL_HEIGHT);
                }
                //ステートマーク 属性開閉
                if (ele.isOpen)
                {
                    e.Graphics.DrawImage(Properties.Resources.minus, 2+32, inCnt * TIME_CELL_HEIGHT);
                }
                else
                {
                    e.Graphics.DrawImage(Properties.Resources.plus, 2+32, inCnt * TIME_CELL_HEIGHT);
                }
                //以下、名前描画処理
                if (!string.IsNullOrEmpty(ele.mName))
                {
                    e.Graphics.DrawString(ele.mName, mFont, Brushes.White, 2+48, inCnt * TIME_CELL_HEIGHT);
                }
            }

            //e.Graphics.FillRectangle(Brushes.Lime, new Rectangle(0, 0, 500, 500));

        }

        //Right Paine
        private void panel_Time_MouseClick(object sender, MouseEventArgs e)
        {
            //Flameクリック処理
            //フレーム検出
            int cx = e.X / TIME_CELL_WIDTH;
            int cy = e.Y / TIME_CELL_HEIGHT;
            //注:範囲指定時は考慮
            //クリックフレームを現在のフレームに指定
            if (cx <= numericUpDown_MaxFrame.Value)
            {
                numericUpDown_NowFlame.Value = cx;
                mSelect_Pos_Start.X = cx;
                mSelect_Pos_Start.Y = cy;

                /* ※データ構造が変わったので一旦コメントアウト comment out by yoshi 2017/01/08
                //既存フレームがあればMainPreviewに表示
                if (!mMotion.ToFrame(cx))
                {
                    //無ければ前後フレームから補完を行う
                    mMotion.Completion(cx);
                }
                */
                mFormMain.Refresh();
            }          
        }
        private void panel_Time_MouseEnter(object sender, EventArgs e)
        {

        }
        private void panel_Time_MouseLeave(object sender, EventArgs e)
        {

        }
        private void panel_Time_MouseMove(object sender, MouseEventArgs e)
        {
            if(mMouseDownL)
            {
                mSelect_Pos_End.X = e.X / TIME_CELL_WIDTH;
                mSelect_Pos_End.Y = e.Y / TIME_CELL_HEIGHT;
                panel_Time.Refresh();
            }

        }
        private void panel_Time_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mMouseDownL = true;
                mSelect_Pos_End.X = 0;
                mSelect_Pos_End.Y = 0;
            }
            //if (e.Button == MouseButtons.Right) { mMouseDownR = true; }
            //if (e.Button == MouseButtons.Middle) { mMouseDownM = true; }
            mSelect_Pos_Start.X = e.X / TIME_CELL_WIDTH;
            mSelect_Pos_Start.Y = e.Y / TIME_CELL_HEIGHT;
        }
        private void panel_Time_MouseUp(object sender, MouseEventArgs e)
        {
            if(mMouseDownL)
            {
                //Flameクリック処理
                //フレーム検出
                int cx = e.X / TIME_CELL_WIDTH;
                int cy = e.Y / TIME_CELL_HEIGHT;
                //注:範囲指定時は考慮
                //クリックフレームを現在のフレームに指定
                if (cx <= numericUpDown_MaxFrame.Value)
                {
                    numericUpDown_NowFlame.Value = cx;
                    mSelect_Pos_End.X = cx;
                    mSelect_Pos_End.Y = cy;
                }
            }
            mMouseDownL = false;
            //mMouseDownR = false;
            //mMouseDownM = false;
        }
        private void panel_Time_Paint(object sender, PaintEventArgs e)
        {
            //TimeLine
            int inWidth = this.panel_Time.Width;
            int inHeight = this.panel_Time.Height;
            int inFrame = (int)this.numericUpDown_MaxFrame.Value;
            int CellWidth = TIME_CELL_WIDTH;
            int CellHeight = TIME_CELL_HEIGHT;
            int inCnt, inMax = 5;

            //全消去
            e.Graphics.Clear(Color.Black);

//※ここはthis.mMotionをe.Graphicsで描画しやすいように並べて（各クラスのmLineNoを見て）描画するようにします
//※トリッキーなやり方としては、this.mMotionにe.Graphicsを渡してやって内部で描画する

            //以下、横ライン描画処理

            //e.Graphics.DrawLine(Pens.Black, 0, CellHeight, inWidth - 1, CellHeight);

            if (mMotion == null) return;
            inMax = this.mMotion.mListElem.Count;   //Elements数
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                SolidBrush sb = new SolidBrush(Color.FromArgb(64,Color.Gray));
                //e.Graphics.DrawLine(Pens.Black, 0, inY, inWidth, inY);
                //選択中Elementsの背景強調
                ClsDatElem ele= this.mMotion.mListElem[inCnt];
                if ((inCnt % 2) != 0)
                {
                    e.Graphics.FillRectangle(sb, 0, inCnt * CellHeight, panel_Time.Width, CellHeight - 1);
                }
                if (ele.isSelect)
                {
                    //選択色
                    sb = new SolidBrush(Color.FromArgb(128, Color.Green));
                    e.Graphics.FillRectangle(sb, 0, inCnt * CellHeight, panel_Time.Width, CellHeight - 1);
                }
            }

            /* ※データ構造が変わったので一旦コメントアウト comennt out by yoshi 2017/01/08
            //以下、縦ライン描画処理
            for (inCnt = 0; inCnt < inFrame; inCnt++)
            {
                Pen pen = new Pen( Color.FromArgb(255,40,40,40));// Pens.DimGray;

                //5の倍数の時(グレイ)
                if (inCnt % 5 == 0) pen = Pens.DarkGreen;
                //現在のフレームの時(赤)
                if (inCnt == numericUpDown_NowFlame.Value)
                {
                    SolidBrush sb = new SolidBrush(Color.FromArgb(64,Color.Red));
                    e.Graphics.FillRectangle(sb, inCnt * CellWidth, 0, CellWidth, inHeight - 1);
                }
                //標準(黒)
                e.Graphics.DrawLine(pen, inCnt * CellWidth, 0, inCnt * CellWidth, inHeight);

                //Draw FRAMEtype
                FRAME frm = this.mMotion.GetFrame(inCnt);
                if(frm!=null)
                {
                    if(frm.Type == FRAME.TYPE.KeyFrame)
                    {
//現在テスト中（ここから）
//キーフレームの表示を画像で行う！？ 

//                      SolidBrush sb = new SolidBrush(Color.FromArgb(64,Color.Aquamarine));
//                      e.Graphics.FillRectangle(sb, inCnt * CellWidth, 0, CellWidth, inHeight - 1);

                        e.Graphics.DrawImage(Properties.Resources.markRed, inCnt * CellWidth + 2, 1);   //Ｙ座標はどうやって取得するのが良いだろうか？
//現在テスト中（ここまで）
                    }
                    if (frm.Type == FRAME.TYPE.Control)
                    { }
                }

            }
            */


            //DrawDragArea
            if (!mSelect_Pos_End.IsEmpty)
            {
                //選択範囲の網掛け
                SolidBrush sb = new SolidBrush(Color.FromArgb(128, 0, 0, 128));
                e.Graphics.FillRectangle(sb, mSelect_Pos_Start.X * TIME_CELL_WIDTH, 0, (mSelect_Pos_End.X-mSelect_Pos_Start.X) * TIME_CELL_WIDTH, inHeight - 1);
            }

        }

        private void panel_Time_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //タイムライン上ダブルクリックは KeyFrame作成
            int pos = e.X / TIME_CELL_WIDTH;
            //フレーム範囲チェック
            if (pos >= numericUpDown_MaxFrame.Value) return;

            /* ※データ構造が変わったので一旦コメントアウト comennt out by yoshi 2017/01/08
            FRAME newframe = this.mMotion.EditFrame.Clone();
            newframe.FrameNum = pos;
            newframe.Type = FRAME.TYPE.KeyFrame;
            this.mMotion.AddFrame(newframe);
            */

            //表示更新
            panel_Time.Refresh();
            mFormMain.Refresh();            
        }

        private void FormControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Closeキャンセルして非表示にするだけ
            e.Cancel = true;

            //this.Visible = false; //自身で消さなくても下の操作で消える
            this.mFormMain.checkBox_Control.Checked = false;
        }

        private void LineHeader_MouseDown(object sender, MouseEventArgs e)
        {
            this.panel_Time_MouseDown(sender, e);
        }

        private void LineHeader_MouseUp(object sender, MouseEventArgs e)
        {
            this.panel_Time_MouseUp(sender, e);
        }

        private void ToolStripMenuItem_AddKey_Click(object sender, EventArgs e)
        {
//現在テスト中（ここから）
            /*
            FRAME clFrame = this.mMotion.GetFrame(4);
            if (clFrame != null) return;    //存在チェックはこのような感じ

            clFrame = new FRAME();
            clFrame.FrameNum = 4;
            clFrame.Type = FRAME.TYPE.KeyFrame;
            this.mMotion.AddFrame(clFrame);
            */
//現在テスト中（ここまで）
        }

        private void ToolStripMenuItem_DelKey_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem_DelFrame_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem_InsertFrame_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem_Cut_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem_Copy_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem_OverWrite_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem_Insert_Click(object sender, EventArgs e)
        {

        }
    }
}
