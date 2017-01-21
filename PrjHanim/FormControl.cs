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
        public static readonly int HEAD_HEIGHT = 20;
        public static readonly int CELL_HEIGHT = 18;
        public static readonly int CELL_WIDTH = 12;

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

        //以下、作業領域
        private TextBox mTextBox = null;

        public FormControl(FormMain form)
        {
            this.mFormMain = form;

            InitializeComponent();

            //ダブルバッファ強制有効化
            panel_Control.GetType().InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, panel_Time, new object[] { true });
            panel_Time.GetType().InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, panel_Time, new object[] { true });
        }

        private void FormControl_Load(object sender, EventArgs e)
        {
            //以下、ウィンドウの設定
            this.Location = ClsSystem.mSetting.mWindowControl.mLocation;
            this.Size = ClsSystem.mSetting.mWindowControl.mSize;

            //以下、初期化処理
            if (this.mMotion != null)
            {
                int inFrameNum = (int)this.numericUpDown_MaxFrame.Value;
                this.mMotion.SetFrameNum(inFrameNum);
            }

            this.mFont = new Font("ＭＳ ゴシック", 10.5f);
            this.panel_Time.Width = CELL_WIDTH * (int)numericUpDown_MaxFrame.Value;
            this.panel_Time.Height = HEAD_HEIGHT * 5;
        }

        /// <summary>
        /// ウィンドウ名設定
        /// </summary>
        /// <param name="clMotion">選択中のモーション管理クラス</param>
        public void SetName(ClsDatMotion clMotion)
        {
            this.Text = ClsSystem.GetWindowName("Control", clMotion);
        }

        public void RemoveElementFromKey(int inElementKey)
        {
            if (this.mMotion == null) return;

            this.mMotion.RemoveElemFromIndex(inElementKey);

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

            int inWidth = inFrameNum * FormControl.CELL_WIDTH + 1;
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
            for (int cx = 1; cx < (LineHeader.Width / FormControl.CELL_WIDTH); cx++)
            {
                if (cx % 5 == 0)
                {
                    e.Graphics.DrawLine(Pens.Orange, cx * FormControl.CELL_WIDTH, LineHeader.Height / 4, cx * FormControl.CELL_WIDTH, LineHeader.Height - 1);
                    e.Graphics.DrawString(cx.ToString(), Font, Brushes.AntiqueWhite, new Point((cx - 1) * FormControl.CELL_WIDTH, 0));
                }
                else
                {
                    e.Graphics.DrawLine(Pens.Green, cx * FormControl.CELL_WIDTH, LineHeader.Height / 2, cx * FormControl.CELL_WIDTH, LineHeader.Height - 1);
                }
            }

            //NowPotision 矢印
            e.Graphics.DrawImage(Properties.Resources.LineMarker, new Point(FormControl.CELL_WIDTH * (int)numericUpDown_NowFlame.Value + (FormControl.CELL_WIDTH / 2) - 3, 2));
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
            int inLineNo = e.Y / FormControl.CELL_HEIGHT;

            //以下、エレメント選択処理
            this.mMotion.SetSelectLineNo(inLineNo);

            //Item最大数を確認
            ClsDatElem clElem = this.mMotion.FindElemFromLineNo(inLineNo);
            if (clElem != null)
            {
                //Click Eye
                if (e.X < 16)
                {
                    clElem.isVisible = !clElem.isVisible;
                }

                //Click Locked
                if (e.X > 16 && e.X < 32)
                {
                    clElem.isLocked = !clElem.isLocked;
                }

                //Attribute Open
                if (e.X > 32 && e.X < 48)
                {
                    clElem.isOpen = !clElem.isOpen;

                    this.mMotion.Assignment();    //行番号とタブを割り振る処理
                }
            }

            //以下、コントロール更新処理
            this.panel_Control.Refresh();
            this.panel_Time.Refresh();
            this.mFormMain.Refresh();
        }

        private void panel_Control_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Item選択
            int inLineNo = e.Y / FormControl.CELL_HEIGHT;

            //以下、エレメント選択処理
            this.mMotion.SetSelectLineNo(inLineNo);

            //Item最大数を確認
            ClsDatElem clElem = this.mMotion.FindElemFromLineNo(inLineNo);
            if (clElem != null)
            {
                if (e.X > 48)
                {
                    //以下、テキストボックス削除処理
                    this.RemoveTextBoxName();

                    //以下、テキストボックス生成処理
                    this.mTextBox = new TextBox();
                    this.mTextBox.Location = new System.Drawing.Point(52, inLineNo * FormControl.CELL_HEIGHT - 1);
                    this.mTextBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                    this.mTextBox.MaxLength = ClsDatElem.MAX_NAME;
                    this.mTextBox.Text = clElem.GetName();
                    this.mTextBox.Name = "textBox_Name";
                    this.mTextBox.Size = new System.Drawing.Size(80, 19);
                    this.mTextBox.Tag = inLineNo;
                    this.mTextBox.TabIndex = 0;
                    this.mTextBox.Leave += new System.EventHandler(this.textBox_Name_Leave);
                    this.mTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Name_KeyDown);
                    this.panel_Control.Controls.Add(this.mTextBox);

                    this.mTextBox.Focus();
                }
            }

            //以下、コントロール更新処理
            this.panel_Control.Refresh();
            this.panel_Time.Refresh();
            this.mFormMain.Refresh();
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

            //以下、モーションのコントロール描画処理
            bool isExist = ClsSystem.mDicMotion.ContainsKey(ClsSystem.mMotionSelectKey);
            if (isExist)
            {
                ClsDatMotion clMotion = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey];
                clMotion.DrawControl(e.Graphics, this.panel_Control.Width, this.panel_Control.Height, this.mFont);
            }
        }

        private void panel_Time_Paint(object sender, PaintEventArgs e)
        {
            int inWidth = this.panel_Time.Width;
            int inHeight = this.panel_Time.Height;

            //以下、モーションのコントロール描画処理
            bool isExist = ClsSystem.mDicMotion.ContainsKey(ClsSystem.mMotionSelectKey);
            if (isExist)
            {
                ClsDatMotion clMotion = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey];
                clMotion.mSelectFrame = (int)numericUpDown_NowFlame.Value;
                clMotion.DrawTime(e.Graphics, this.panel_Time.Width, this.panel_Time.Height);
            }
        }

        //Right Paine
        private void panel_Time_MouseClick(object sender, MouseEventArgs e)
        {
            //Flameクリック処理
            //フレーム検出
            int cx = e.X / FormControl.CELL_WIDTH;
            int cy = e.Y / FormControl.CELL_HEIGHT;

            //注:範囲指定時は考慮
            //クリックフレームを現在のフレームに指定
            if (cx <= this.numericUpDown_MaxFrame.Value)
            {
                this.numericUpDown_NowFlame.Value = cx;
                this.mSelect_Pos_Start.X = cx;
                this.mSelect_Pos_Start.Y = cy;

                this.mFormMain.Refresh();
            }

            //Item最大数を確認
            if (cy < this.mMotion.mListElem.Count)
            {
                ClsDatElem clElem = this.mMotion.mListElem[cy];

                this.mMotion.SetSelectLineNo(clElem.mLineNo);

                //以下、コントロール更新処理
                this.panel_Control.Refresh();
                this.panel_Time.Refresh();
                this.mFormMain.Refresh();
            }

            //以下、選択した行がエレメントだった場合の処理
            /*
            this.mMotion.FindElemFromLineNo();
            this.mMotion.FindOptionFromLineNo();
            */
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
                mSelect_Pos_End.X = e.X / FormControl.CELL_WIDTH;
                mSelect_Pos_End.Y = e.Y / FormControl.CELL_HEIGHT;
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
            mSelect_Pos_Start.X = e.X / FormControl.CELL_WIDTH;
            mSelect_Pos_Start.Y = e.Y / FormControl.CELL_HEIGHT;
        }

        private void panel_Time_MouseUp(object sender, MouseEventArgs e)
        {
            if(mMouseDownL)
            {
                //Flameクリック処理
                //フレーム検出
                int cx = e.X / FormControl.CELL_WIDTH;
                int cy = e.Y / FormControl.CELL_HEIGHT;

                //注:範囲指定時は考慮
                //クリックフレームを現在のフレームに指定
                if (cx <= numericUpDown_MaxFrame.Value)
                {
                    numericUpDown_NowFlame.Value = cx;
                    mSelect_Pos_End.X = cx;
                    mSelect_Pos_End.Y = cy;
                }

                this.panel_Control.Refresh();
                this.panel_Time.Refresh();
            }
            mMouseDownL = false;
            //mMouseDownR = false;
            //mMouseDownM = false;
        }

        private void panel_Time_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //タイムライン上ダブルクリックは KeyFrame作成
            int pos = e.X / FormControl.CELL_WIDTH;

            //フレーム範囲チェック
            if (pos >= numericUpDown_MaxFrame.Value) return;

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

        private void textBox_Name_Leave(object sender, EventArgs e)
        {
            //以下、テキストボックス削除処理
            this.RemoveTextBoxName();
        }

        private void textBox_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                //以下、テキストボックス削除処理
                this.RemoveTextBoxName();
            }
        }

        private void RemoveTextBoxName()
        {
            if (this.mTextBox == null) return;

            //以下、名前設定処理
            int inLineNo = (int)this.mTextBox.Tag;
            ClsDatElem clElem = this.mMotion.FindElemFromLineNo(inLineNo);
            if (clElem != null)
            {
                string clName = this.mTextBox.Text;
                if (!string.IsNullOrEmpty(clName))
                {
                    clElem.SetName(clName);
                }
            }

            //以下、テキストボックス削除処理
            this.panel_Control.Controls.Remove(this.mTextBox);
            this.mTextBox = null;

            //以下、コントロール更新処理
            this.panel_Control.Refresh();
            this.panel_Time.Refresh();
            this.mFormMain.Refresh();
        }

        private void panel_Control_MouseDown(object sender, MouseEventArgs e)
        {
            //以下、テキストボックス削除処理
            this.RemoveTextBoxName();
        }

        private void button_ElemParent_Click(object sender, EventArgs e)
        {
            //一つ上の親になる
        }

        private void button_ElemChild_Click(object sender, EventArgs e)
        {
            //一行上のエレメントの子供になる
        }

        private void button_ElemUp_Click(object sender, EventArgs e)
        {
            //兄と弟の入れ替えとする
            //子供が親になったり、親が子供になったりしない
        }

        private void button_ElemDown_Click(object sender, EventArgs e)
        {
            //兄と弟の入れ替えとする
            //子供が親になったり、親が子供になったりしない
        }

        private void button_ElemRemove_Click(object sender, EventArgs e)
        {
            int inLineNo = this.mMotion.GetSelectLineNo();
            if (inLineNo < 0) return;

            //以下、エレメント削除処理
            this.mMotion.RemoveElemFromLineNo(inLineNo);

            //以下、行番号振り直し処理
            this.mMotion.Assignment();

            //以下、コントロール更新処理
            this.panel_Control.Refresh();
            this.panel_Time.Refresh();
            this.mFormMain.Refresh();
        }
    }
}
