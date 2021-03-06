﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Drawing;
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

        //private Point mSelect_Pos_Start; 一旦コメントアウト 2017/01/31 comment out by yoshi
        //private Point mSelect_Pos_End; 一旦コメントアウト 2017/01/31 comment out by yoshi
        //private bool mMouseDownL=false; 一旦コメントアウト 2017/01/31 comment out by yoshi
        //private bool mMouseDownR=false;
        //private bool mMouseDownM=false;
        private Point mCatchPosStart;
        private FormDragLabel mFormDragLabel = null;

        //メインフォームにセットしてもらう
        //全状態を間接参照する
        private FormMain mFormMain = null;

        private Font mFont = null;

        //以下、作業領域
        private TextBox mTextBox = null;

        public FormControl(FormMain form)
        {
            this.mFormMain = form;

            InitializeComponent();

            //ダブルバッファ強制有効化
            panel_Control.GetType().InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, panel_Control, new object[] { true });
            panel_Time.GetType().InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, panel_Time, new object[] { true });
        }

        private void FormControl_Load(object sender, EventArgs e)
        {
            //以下、ウィンドウの設定
            this.Location = ClsSystem.mSetting.mWindowControl.mLocation;
            this.Size = ClsSystem.mSetting.mWindowControl.mSize;

            //以下、初期化処理
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion != null)
            {
                int inFrameNum = (int)this.numericUpDown_MaxFrame.Value;
                clMotion.SetMaxFrameNum(inFrameNum);
            }

            this.mFont = new Font("ＭＳ ゴシック", 10.5f);
            this.panel_Time.Width = CELL_WIDTH * (int)numericUpDown_MaxFrame.Value;
            this.panel_Time.Height = HEAD_HEIGHT * 5;

            this.ToolStripMenuItem_AddRotation.Tag = EnmTypeOption.ROTATION;
            this.ToolStripMenuItem_AddScale.Tag = EnmTypeOption.SCALE;
            this.ToolStripMenuItem_AddOffset.Tag = EnmTypeOption.OFFSET;
            this.ToolStripMenuItem_AddFlip.Tag = EnmTypeOption.FLIP;
            this.ToolStripMenuItem_AddTransparency.Tag = EnmTypeOption.TRANSPARENCY;
            this.ToolStripMenuItem_AddColor.Tag = EnmTypeOption.COLOR;
            this.ToolStripMenuItem_AddUserDataText.Tag = EnmTypeOption.USER_DATA;
        }

        public void RemoveElementFromKey(int inElementKey)
        {
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            clMotion.RemoveElemFromIndex(inElementKey);

            this.RefreshAll();
        }

        /// <summary>
        /// ウィンドウ名を設定する
        /// </summary>
        /// <param name="clName">ウィンドウ名</param>
        public void SetName(ClsDatMotion clMotion)
        {
            this.Text = ClsTool.GetWindowName("Control", clMotion);

            this.RefreshAll();
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

            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion != null)
            {
                clMotion.SetMaxFrameNum(inFrameNum);
            }

            int inWidth = inFrameNum * FormControl.CELL_WIDTH + 1;
            this.panel_Time.Width = inWidth;
            this.panel_Time.Refresh();
        }

        private void NowFrame_ValueChanged(object sender, EventArgs e)
        {
            //現在フレームが変更された時の処理
            this.RefreshAll();
        }

        public void RefreshAll()
        {
            //全体再描画
            this.panel_Control.Refresh();
            this.panel_Time.Refresh();
            this.LineHeader.Refresh();
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
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            //以下、アイテム選択処理
            int inLineNo = this.GetLineNoFromPositionY(e.Y);
            ClsSystem.SetSelectFromLineNo(inLineNo);

            ClsDatElem clElem = ClsSystem.GetElemFromLineNo(inLineNo);
            if (clElem != null)
            {
                if (inLineNo == clElem.mLineNo)
                {
                    //Click Eye
                    if (0 <= e.X && e.X < 17)
                    {
                        clElem.isDisplay = !clElem.isDisplay;
                    }

                    //Click Parent
                    if (17 <= e.X && e.X < 34)
                    {
                        if (clElem.mElem != null)
                        {
                            clElem.isParent = !clElem.isParent;
                        }
                    }

                    //Click Locked
                    if (34 <= e.X && e.X < 51)
                    {
                        clElem.isLocked = !clElem.isLocked;
                    }

                    //Attribute Open
                    if (51 <= e.X && e.X < 68)
                    {
                        clElem.isOpen = !clElem.isOpen;

                        clMotion.RefreshLineNo();    //行番号とタブを割り振る処理
                    }
                }
            }

            //以下、コントロール更新処理
            this.RefreshControl();
            this.panel_Control.Refresh();
            this.panel_Time.Refresh();
            this.mFormMain.Refresh();
        }

        private void panel_Control_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            //Item選択
            int inLineNo = this.GetLineNoFromPositionY(e.Y);

            //以下、エレメント選択処理
            ClsSystem.SetSelectFromLineNo(inLineNo);

            //Item最大数を確認
            ClsDatElem clElem = ClsSystem.GetElemFromLineNo(inLineNo);
            if (clElem != null)
            {
                if (e.X > 69)
                {
                    //以下、テキストボックス削除処理
                    this.RemoveTextBoxName();

                    //以下、テキストボックス生成処理
                    this.mTextBox = new TextBox();
                    this.mTextBox.Location = new System.Drawing.Point(69, inLineNo * FormControl.CELL_HEIGHT - 1);
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
            this.RefreshControl();
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

        private void panel_Control_Paint(object sender, PaintEventArgs e)
        {
            int inWidth = this.panel_Control.Width;
            int inHeight = this.panel_Control.Height;

            //以下、モーションのコントロール描画処理
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion != null)
            {
                clMotion.DrawControl(e.Graphics, this.panel_Control.Width, this.panel_Control.Height, this.mFont);
            }
        }

        private void panel_Time_Paint(object sender, PaintEventArgs e)
        {
            //以下、モーションのコントロール描画処理
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if(clMotion!= null)
            {
                int inSelectFrameNo = (int)this.numericUpDown_NowFlame.Value;
                ClsSystem.SetSelectFrameNo(inSelectFrameNo);

                clMotion.DrawTime(e.Graphics, this.panel_Time.Width, this.panel_Time.Height);
            }
        }

        private int GetFrameNoFromPositionX(int inPosX)
        {
            return (inPosX / FormControl.CELL_WIDTH);
        }

        private int GetLineNoFromPositionY(int inPosY)
        {
            //ここに垂直スクロールバーの処理を書く

            return (inPosY / FormControl.CELL_HEIGHT);
        }

        //Right Paine
        private void panel_Time_MouseClick(object sender, MouseEventArgs e)
        {
            //以下、アイテム選択処理
            int inLineNo = this.GetLineNoFromPositionY(e.Y);
            ClsSystem.SetSelectFromLineNo(inLineNo);

            /* 一旦コメントアウト 2017/01/31 comment out by yoshi
            //Flameクリック処理
            //フレーム検出
            int cx = e.X / FormControl.CELL_WIDTH;
            int inLineNo = this.GetLineNoFromPositionY(e.Y);

            //注:範囲指定時は考慮
            //クリックフレームを現在のフレームに指定
            if (cx <= this.numericUpDown_MaxFrame.Value)
            {
                this.numericUpDown_NowFlame.Value = cx;
                this.mSelect_Pos_Start.X = cx;
                this.mSelect_Pos_Start.Y = inLineNo;

                this.mFormMain.Refresh();
            }

            //以下、コントロール更新処理
            this.RefreshControl();
            this.panel_Control.Refresh();
            this.panel_Time.Refresh();
            this.mFormMain.Refresh();
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
            /*
            if(mMouseDownL)
            {
                mSelect_Pos_End.X = e.X / FormControl.CELL_WIDTH;
                mSelect_Pos_End.Y = e.Y / FormControl.CELL_HEIGHT;
                panel_Time.Refresh();
            }
            */
        }

        private void panel_Time_MouseDown(object sender, MouseEventArgs e)
        {
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            int inFrameNo = this.GetFrameNoFromPositionX(e.X);
            int inLineNo = this.GetLineNoFromPositionY(e.Y);

            //以下、現在位置を設定する処理
            ClsSystem.SetSelectFromLineNo(inLineNo);
            ClsSystem.SetSelectFrameNo(inFrameNo);

            //以下、フレーム選択処理
            if (inFrameNo <= this.numericUpDown_MaxFrame.Value)
            {
                this.numericUpDown_NowFlame.Value = inFrameNo;
            }
            else
            {
                this.numericUpDown_NowFlame.Value = this.numericUpDown_MaxFrame.Value - 1;
            }

            /* 一旦コメントアウト 2017/01/31 comment out by yoshi
            if (e.Button == MouseButtons.Left)
            {
                //以下、座標情報初期化処理
                mMouseDownL = true;
                mSelect_Pos_End.X = 0;
                mSelect_Pos_End.Y = 0;
            }
            */

            /* 一旦コメントアウト 2017/01/31 comment out by yoshi
            //if (e.Button == MouseButtons.Right) { mMouseDownR = true; }
            //if (e.Button == MouseButtons.Middle) { mMouseDownM = true; }
            mSelect_Pos_Start.X = e.X / FormControl.CELL_WIDTH;
            mSelect_Pos_Start.Y = e.Y / FormControl.CELL_HEIGHT;
            */

            //以下、コントロール更新処理
            this.RefreshControl();
            this.panel_Control.Refresh();
            this.panel_Time.Refresh();
            this.mFormMain.Refresh();
        }

        private void panel_Time_MouseUp(object sender, MouseEventArgs e)
        {
            /* 一旦コメントアウト 2017/01/31 comment out by yoshi
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
            */
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

            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            //以下、名前設定処理
            int inLineNo = (int)this.mTextBox.Tag;
            ClsDatElem clElem = ClsSystem.GetElemFromLineNo(inLineNo);
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

        private void RefreshControl()
        {
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            int inSelectFrameNo = ClsSystem.GetSelectFrameNo();
            int inMaxFrameNum = clMotion.GetMaxFrameNum();
            int inSelectLineNo = ClsSystem.GetSelectLineNo();
            ClsDatItem clItem = clMotion.GetItemFromLineNo(inSelectLineNo);

            //以下、削除ボタン有効化設定
            bool isEnable = false;
            if (clItem != null)
            {
                if (clItem.mTypeItem == ClsDatItem.TYPE_ITEM.ELEM)
                {
                    isEnable = true;
                }
                else if (clItem.mTypeItem == ClsDatItem.TYPE_ITEM.OPTION)
                {
                    ClsDatOption clOption = clItem as ClsDatOption;
                    isEnable = clOption.IsRemoveOK();
                }
            }
            this.button_ItemRemove.Enabled = isEnable;

            //以下、アトリビュートウィンドウ設定
            if (this.mFormMain != null)
            {
                if (this.mFormMain.mFormAttribute != null)
                {
                    ClsDatElem clElem = ClsSystem.GetElemFromSelectLineNo();
                    this.mFormMain.mFormAttribute.Init(clElem, inSelectFrameNo, inMaxFrameNum);
                }
            }

            //以下、上ボタン有効化設定
            isEnable = false;
            if (clItem != null)
            {
                ClsDatElem clElem = null;
                if (clItem.mTypeItem == ClsDatItem.TYPE_ITEM.ELEM)
                {
                    clElem = clItem as ClsDatElem;
                    if (clElem.mElem == null)
                    {
                        isEnable = clMotion.CanMoveUp(clElem);
                    }
                    else
                    {
                        //isEnable = clElem.mElem.CanMoveUp(clElem);    //自分が長男かチェックする
                    }
                }
/*
                else if (clItem.mTypeItem == ClsDatItem.TYPE_ITEM.OPTION)
                {
                    ClsDatOption clOption = clItem as ClsDatOption;
                    clElem = clOption.mElem;
//                  isEnable = clElem.CanMoveUp(clOption);
                }
*/
            }
            this.button_ItemUp.Enabled = isEnable;

            //以下、下ボタン有効化設定
            isEnable = false;
            if (clItem != null)
            {
                ClsDatElem clElem = null;
                if (clItem.mTypeItem == ClsDatItem.TYPE_ITEM.ELEM)
                {
                    clElem = clItem as ClsDatElem;
                    if (clElem.mElem == null)
                    {
                        isEnable = clMotion.CanMoveDown(clElem);
                    }
                    else
                    {
                    }
                }
/*
                else if (clItem.mTypeItem == ClsDatItem.TYPE_ITEM.OPTION)
                {
                    ClsDatOption clOption = clItem as ClsDatOption;
                    clElem = clOption.mElem;
//                  isEnable = clElem.CanMoveDown(clOption);
                }
*/
            }
            this.button_ItemDown.Enabled = isEnable;
        }

        /// <summary>
        /// 現在選択中のアイテムを削除する
        /// </summary>
        private void RemoveItemFromSelectLineNo()
        {
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            int inLineNo = ClsSystem.GetSelectLineNo();
            if (inLineNo < 0) return;

            //以下、アイテム削除処理
            clMotion.RemoveItemFromLineNo(inLineNo, false, true);

            //以下、行番号振り直し処理
            clMotion.RefreshLineNo();

            //以下、コントロール更新処理
            this.RefreshControl();
            this.panel_Control.Refresh();
            this.panel_Time.Refresh();
            this.mFormMain.Refresh();
        }

        private void button_ElemParent_Click(object sender, EventArgs e)
        {
            //一つ上の親になる
        }

        private void button_ElemChild_Click(object sender, EventArgs e)
        {
            //一行上のエレメントの子供になる
        }

        private void button_ItemUp_Click(object sender, EventArgs e)
        {
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            int inLineNo = ClsSystem.GetSelectLineNo();
            if (inLineNo < 0) return;

            ClsDatItem clItem = clMotion.GetItemFromLineNo(inLineNo);
            if (clItem == null) return;
            if (clItem.mTypeItem != ClsDatItem.TYPE_ITEM.ELEM) return;

            //以下、一つ上に移動する処理
            ClsDatElem clElem = clItem as ClsDatElem;
            if (clElem.mElem == null)
            {
                clMotion.MoveUp(clElem);
            }
            else
            {
                clElem.mElem.MoveElemUp(clElem);
            }

            //以下、行番号振り直し処理
            clMotion.RefreshLineNo();

            //以下、改めてアイテムを選択する処理
            ClsSystem.SetSelectFromLineNo(clItem.mLineNo);   //上記の RefreshLineNo 関数内でmLineNoが変わっているはず

            //以下、コントロール更新処理
            this.RefreshControl();
            this.panel_Control.Refresh();
            this.panel_Time.Refresh();
            this.mFormMain.Refresh();
        }

        private void button_ItemDown_Click(object sender, EventArgs e)
        {
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            int inLineNo = ClsSystem.GetSelectLineNo();
            if (inLineNo < 0) return;

            ClsDatItem clItem = clMotion.GetItemFromLineNo(inLineNo);
            if (clItem == null) return;
            if (clItem.mTypeItem != ClsDatItem.TYPE_ITEM.ELEM) return;

            //以下、一つ下に移動する処理
            ClsDatElem clElem = clItem as ClsDatElem;
            if (clElem.mElem == null)
            {
                clMotion.MoveDown(clElem);
            }
            else
            {
                clElem.mElem.MoveElemDown(clElem);
            }

            //以下、行番号振り直し処理
            clMotion.RefreshLineNo();

            //以下、改めてアイテムを選択する処理
            ClsSystem.SetSelectFromLineNo(clItem.mLineNo);   //上記の RefreshLineNo 関数内でmLineNoが変わっているはず

            //以下、コントロール更新処理
            this.RefreshControl();
            this.panel_Control.Refresh();
            this.panel_Time.Refresh();
            this.mFormMain.Refresh();
        }

        private void button_ItemRemove_Click(object sender, EventArgs e)
        {
            this.RemoveItemFromSelectLineNo();
        }

        private void ToolStripMenuItem_RemoveElement_Click(object sender, EventArgs e)
        {
            this.RemoveItemFromSelectLineNo();
        }

        private void ToolStripMenuItem_RemoveOption_Click(object sender, EventArgs e)
        {
            this.RemoveItemFromSelectLineNo();
        }

        private void ToolStripMenuItem_AddOption_DropDownOpening(object sender, EventArgs e)
        {
            ClsDatElem clElem = ClsSystem.GetElemFromSelectLineNo();
            if (clElem == null) return;

            Dictionary<EnmTypeOption, ToolStripMenuItem> clDic = new Dictionary<EnmTypeOption, ToolStripMenuItem>();
            clDic[EnmTypeOption.ROTATION] = this.ToolStripMenuItem_AddRotation;
            clDic[EnmTypeOption.SCALE] = this.ToolStripMenuItem_AddScale;
            clDic[EnmTypeOption.OFFSET] = this.ToolStripMenuItem_AddOffset;
            clDic[EnmTypeOption.FLIP] = this.ToolStripMenuItem_AddFlip;
            clDic[EnmTypeOption.TRANSPARENCY] = this.ToolStripMenuItem_AddTransparency;
            clDic[EnmTypeOption.COLOR] = this.ToolStripMenuItem_AddColor;
            clDic[EnmTypeOption.USER_DATA] = this.ToolStripMenuItem_AddUserDataText;

            foreach (EnmTypeOption enTypeOption in Enum.GetValues(typeof(EnmTypeOption)))
            {
                bool isExist = clDic.ContainsKey(enTypeOption);
                if (!isExist) continue;

                ToolStripMenuItem clMenuItem = clDic[enTypeOption] as ToolStripMenuItem;
                if (clMenuItem == null) continue;

                isExist = clElem.mDicOption.ContainsKey(enTypeOption);
                clMenuItem.Enabled = !isExist;
            }
        }

        private void ToolStripMenuItem_RemoveOption_DropDownOpening(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            bool isRemoveElementEnable = false;
            bool isAddOptionEnable = false;
            bool isRemoveOptionEnable = false;

            int inLineNo = ClsSystem.GetSelectLineNo();
            if (inLineNo >= 0)
            {
                ClsDatItem clItem = clMotion.GetItemFromLineNo(inLineNo);
                if (clItem != null)
                {
                    isRemoveElementEnable = true;
                    isAddOptionEnable = true;
                }

                ClsDatOption clOption = ClsSystem.GetOptionFromLineNo(inLineNo);
                if (clOption != null)
                {
                    bool isRemoveOK = clOption.IsRemoveOK();
                    if (isRemoveOK)
                    {
                        isRemoveOptionEnable = true;
                    }
                }
            }

            this.ToolStripMenuItem_RemoveElement.Enabled = isRemoveElementEnable;
            this.ToolStripMenuItem_AddOption.Enabled = isAddOptionEnable;
            this.ToolStripMenuItem_RemoveOption.Enabled = isRemoveOptionEnable;
        }

        private void ToolStripMenuItem_Add_Click(object sender, EventArgs e)
        {
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            ClsDatElem clElem = ClsSystem.GetElemFromSelectLineNo();
            if (clElem == null) return;

            //以下、オプション追加処理
            ToolStripMenuItem clITem = sender as ToolStripMenuItem;
            EnmTypeOption enTypeOption = (EnmTypeOption)clITem.Tag;
            object clValue1 = ClsParam.GetDefaultValue1(enTypeOption);
            object clValue2 = ClsParam.GetDefaultValue2(enTypeOption);
            clElem.SetOption(enTypeOption, false, false, null, null, clValue1, clValue2);

            //以下、行番号振り直し処理
            clMotion.RefreshLineNo();

            //以下、コントロール更新処理
            this.RefreshControl();
            this.panel_Control.Refresh();
            this.panel_Time.Refresh();
            this.mFormMain.Refresh();
        }

        private void panel_Control_MouseDown(object sender, MouseEventArgs e)
        {
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            //以下、テキストボックス削除処理
            this.RemoveTextBoxName();

            //以下、アイテム選択処理
            int inLineNo = this.GetLineNoFromPositionY(e.Y);
            ClsSystem.SetSelectFromLineNo(inLineNo);

            if (e.Button == MouseButtons.Left)
            {
                //以下、掴んでいるエレメントを別ウィンドウで表示する処理
                ClsDatItem clItem = clMotion.GetItemFromLineNo(inLineNo);
                if (clItem != null && clItem.mTypeItem == ClsDatItem.TYPE_ITEM.ELEM)
                {
                    int inX = Cursor.Position.X;
                    int inY = Cursor.Position.Y;
                    this.mCatchPosStart = new Point(inX, inY);
                }
            }

            //以下、コントロール更新処理
            this.RefreshControl();
            this.panel_Control.Refresh();
            this.panel_Time.Refresh();
            this.mFormMain.Refresh();
        }

        private void panel_Control_MouseMove(object sender, MouseEventArgs e)
        {
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            //以下、エレメント移動処理
            if (e.Button == MouseButtons.Left)
            {
                bool isExist = false;
                if (this.mFormDragLabel != null)
                {
                    if (!this.mFormDragLabel.IsDisposed)
                    {
                        isExist = true;
                    }
                }

                //以下、挿入マーククリア処理
                clMotion.ClearInsertMark();

                if (isExist)
                {
                    int inSelectLineNo = ClsSystem.GetSelectLineNo();
                    int inLineNo = this.GetLineNoFromPositionY(e.Y);
                    if (inSelectLineNo != inLineNo)
                    {
                        //以下、挿入先チェック処理
                        ClsDatItem clItem = clMotion.GetItemFromLineNo(inLineNo);
                        if (clItem != null)
                        {
                            //以下、挿入先のエレメントに通知する処理
                            ClsDatElem clElem = null;
                            EnmMarkElement enMark = EnmMarkElement.NONE;
                            if (clItem.mTypeItem == ClsDatItem.TYPE_ITEM.ELEM)
                            {
                                clElem = clItem as ClsDatElem;

                                int inY = e.Y % FormControl.CELL_HEIGHT;
                                bool isUp = (inY < FormControl.CELL_HEIGHT / 2);
                                enMark = (isUp) ? EnmMarkElement.UP : EnmMarkElement.IN;
                            }
                            else if (clItem.mTypeItem == ClsDatItem.TYPE_ITEM.OPTION)
                            {
                                ClsDatOption clOption = clItem as ClsDatOption;
                                clElem = clOption.mElemParent;
                                if (inSelectLineNo == clElem.mLineNo)
                                {
                                    clElem = null;
                                }

                                enMark = EnmMarkElement.IN;
                            }

                            if (clElem != null)
                            {
                                //以下、挿入可能な旨をエレメントに通知する処理
                                clElem.SetInsertMark(enMark);
                            }
                        }
                    }
                }
                else
                {
                    //以下、掴んでいるエレメントを別ウィンドウで表示する処理
                    int inLineNo = ClsSystem.GetSelectLineNo();
                    ClsDatItem clItem = clMotion.FindItemFromLineNo(inLineNo);
                    if (clItem != null)
                    {
                        int inXDiff = (Cursor.Position.X - this.mCatchPosStart.X);
                        int inYDiff = (Cursor.Position.Y - this.mCatchPosStart.Y);
                        double doLen = Math.Sqrt(inXDiff * inXDiff + inYDiff * inYDiff);
                        if (doLen >= 5.0)
                        {
                            ClsDatElem clElem = clItem as ClsDatElem;
                            this.mFormDragLabel = new FormDragLabel(clElem);
                            this.mFormDragLabel.Show();
                        }
                    }
                }

                //以下、コントロール更新処理
                this.RefreshControl();
                this.panel_Control.Refresh();
                this.panel_Time.Refresh();
                this.mFormMain.Refresh();
            }
        }

        private void panel_Control_MouseUp(object sender, MouseEventArgs e)
        {
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            //以下、エレメントをドロップする処理
            if (e.Button == MouseButtons.Left)
            {
                if (this.mFormDragLabel != null)
                {
                    if (!this.mFormDragLabel.IsDisposed)
                    {
                        bool isHit = false;

                        //以下、子供として登録する処理
                        ClsDatElem clElemBase = clMotion.FindElemFromMark(EnmMarkElement.IN);
                        if (clElemBase != null)
                        {
                            ClsDatElem clElem = this.mFormDragLabel.GetElem();
                            clElemBase.AddElemChild(clElem);

                            isHit = true;
                        }

                        //以下、自分の兄として登録する処理
                        clElemBase = clMotion.FindElemFromMark(EnmMarkElement.UP);
                        if (clElemBase != null)
                        {
                            ClsDatElem clElem = this.mFormDragLabel.GetElem();
                            clElemBase.AddElemBigBrother(clElem);

                            isHit = true;
                        }

                        //以下、行番号割り振り処理
                        if (isHit)
                        {
                            clMotion.RefreshLineNo();
                        }
                    }

                    this.mFormDragLabel.Close();
                    this.mFormDragLabel.Dispose();
                    this.mFormDragLabel = null;
                }

                //以下、マークをクリアする処理
                clMotion.ClearInsertMark();

                //以下、コントロール更新処理
                this.RefreshControl();
                this.panel_Control.Refresh();
                this.panel_Time.Refresh();
                this.mFormMain.Refresh();
            }
        }

        private void ToolStripMenuItem_AddKeyFrame_Click(object sender, EventArgs e)
        {
            int inIndex = (int)this.numericUpDown_NowFlame.Value;
            if (inIndex <= 0) return;   //0フレーム目には作成できない

            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            ClsDatOption clOption = ClsSystem.GetOptionFromSelectLineNo();
            if (clOption == null) return;

            //以下、キーフレーム作成・更新処理
            ClsDatElem clElem = clOption.mElemParent;
            bool isParentFlag = ClsParam.GetDefaultParentFlag(clElem.mElem, clOption.mTypeOption);

            //以下、現在の値を取得する処理
            object clValue1 = clOption.GetValue1(inIndex);
            object clValue2 = clOption.GetValue2(inIndex);
            ClsDatKeyFrame clKeyFrame = new ClsDatKeyFrame(clOption.mTypeOption, inIndex, false, false, null, null, clValue1, clValue2);
            clOption.SetKeyFrame(inIndex, false, false, null, null, clValue1, clValue2);    //存在していたら更新、存在していなかったら追加

            //以下、コントロール更新処理
            this.RefreshControl();
            this.panel_Control.Refresh();
            this.panel_Time.Refresh();
            this.mFormMain.Refresh();
        }

        private void ToolStripMenuItem_RemoveKeyframe_Click(object sender, EventArgs e)
        {
            int inIndex = (int)this.numericUpDown_NowFlame.Value;
            if (inIndex <= 0) return;   //0フレーム目のキーフレームは消せない

            ClsDatOption clOption = ClsSystem.GetOptionFromSelectLineNo();
            if (clOption == null) return;

            //以下、キーフレーム存在チェック処理
            bool isExist = clOption.IsExistKeyFrame(inIndex);
            if (!isExist) return;

            //以下、キーフレーム削除処理
            clOption.RemoveKeyFrame(inIndex);

            //以下、コントロール更新処理
            this.RefreshControl();
            this.panel_Control.Refresh();
            this.panel_Time.Refresh();
            this.mFormMain.Refresh();
        }
    }
}
