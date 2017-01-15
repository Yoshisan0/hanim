﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Json;

namespace PrjHikariwoAnim
{
    //左ペイン:プロジェクトコントロールTree
    // |-プロジェクト名
    // | +-ImageManager(利用するイメージ)
    // | +-CellList<Cell>(イメージのどの部分か)
    // |
    // |-+アクション名(Run,Idle等)
    //   +-フレームコントローラ
    //   +-フレームの集合<Frame[]>
    //    +-オブジェクトの１つ<Flame>
    //     +-<AttibuteBase>->Cell
    //             
    //
    //以下アクション数でアクション名

    //右ペイン:メインステージ　PanelPreview
    // フレームの集合のコントロール

    public partial class FormMain : Form
    {
        private const float mParZOOM = 10f;//Zoom倍率の固定値

        private FormImageList mFormImageList;
        private FormControl mFormControl;
        private FormAttribute mFormAttribute;
        private FormCell mFormCell;

        private Point mMouseDownPoint = Point.Empty;
        private Point mMouseDownShift;
        private Point mScreenScroll;
        private bool mMouseLDown = false;//L
        //private bool mMouseRDown = false;//R
        //private bool mMouseMDown = false;//M
        private int mWheelDelta;//Wheel
        private Keys mKeys,mKeysSP;//キー情報 通常キー,スペシャルキー

        //編集中の選択中エレメントのインデックス 非選択=null
        //これはTimeLine内のFrameあたりに移動させたいなぁ 11/11移動
        //private int? mNowElementsIndex = null;

        private Motion2 m2;
        
        //private string mNowMotionName;//選択中モーション名

        enum DragState { none,Move, Angle, Scale,Scroll, Joint }; 
        private DragState mDragState = DragState.none;

        private Point PreViewCenter;//PanelPreView Centerセンターポジション
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            //以下、初期化処理
            //panel_PreView.DoubleBuuferd = true;
            panel_PreView.GetType().InvokeMember("DoubleBuffered",BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,null,panel_PreView,new object[] { true });
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //以下、システム初期化処理
            ClsSystem.Init();

            //以下、メインウィンドウの座標の設定
            this.Location = ClsSystem.mSetting.mWindowMain.mLocation;
            this.Size = ClsSystem.mSetting.mWindowMain.mSize;

            //以下、コントロール初期化処理
            this.checkBox_GridCheck.Checked = ClsSystem.mSetting.mWindowMain_DrawGird;
            this.checkBox_CrossBar.Checked = ClsSystem.mSetting.mWindowMain_DrawCross;
            this.checkBox_CellList.Checked = ClsSystem.mSetting.mWindowMain_CellList;
            this.checkBox_Attribute.Checked = ClsSystem.mSetting.mWindowMain_Attribute;
            this.checkBox_Control.Checked = ClsSystem.mSetting.mWindowMain_Control;
            this.checkBox_ImageList.Checked = ClsSystem.mSetting.mWindowMain_ImageList;
            this.checkBox_Snap.Checked = ClsSystem.mSetting.mWindowMain_GridSnap;
            this.numericUpDown_Grid.Value = ClsSystem.mSetting.mWindowMain_WidthGrid;

            //以下、TreeNode作成処理
            //初期モーションTreeの追加
            ClsDatMotion clMotion = this.treeView_Project_AddMotion("Motion");

            //以下、初期化処理
            PreViewCenter = new Point(0, 0);
            mScreenScroll = new Point(0, 0);

            this.mFormImageList = new FormImageList(this);
            this.mFormImageList.Show();

            this.mFormControl = new FormControl(this);
            this.mFormControl.SetMotion(clMotion);
            this.mFormControl.Show();

            this.mFormAttribute = new FormAttribute(this);
            this.mFormAttribute.Show();

            //Ver2
            mFormCell = new FormCell(this);
            //mFormCell.Owner = this;
            mFormCell.ImageMan = ClsSystem.ImageMan;
            mFormCell.Show();

            //背景の再描画をキャンセル(ちらつき抑制)
            //効果いまいち
            this.SetStyle(ControlStyles.Opaque, true);

            //以下、各フォーム表示・非表示処理
            if (mFormImageList != null) mFormImageList.Visible = checkBox_ImageList.Checked;
            if (mFormControl != null) mFormControl.Visible = checkBox_Control.Checked;
            if (mFormAttribute != null) mFormAttribute.Visible = checkBox_Attribute.Checked;
            if (mFormCell != null) mFormCell.Visible = checkBox_CellList.Checked;

            //Test
            m2 = new Motion2("tekitou");
            m2.test();
        }

        /// <summary>
        /// ウィンドウ名設定
        /// </summary>
        /// <param name="clMotion">選択中のモーション管理クラス</param>
        public void SetName(ClsDatMotion clMotion)
        {
            string clAppName = ClsSystem.GetAppFileName();
            this.Text = ClsSystem.GetWindowName(clAppName, clMotion);
        }

        public void AttributeUpdate()
        {
            /* ※データ構造が変わったので一旦コメントアウト comment out by yoshi 2017/01/08
            //mFormAttributeのパラメータ変更時に呼び出される
            //パラメータ取得処理
            //エディット中アイテムにパラメータ再取得
            if (ClsSystem.mMotionSelectKey >= 0)
            {
                ELEMENTS e = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey].EditFrame.GetActiveElements();
                if (e != null)
                {
                    mFormAttribute.GetAllParam(ref e.Atr);
                    panel_PreView.Refresh();
                }
            }
            */
        }

        private void LoadProject_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".hap";
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                if (ClsSystem.mDicMotion!=null)
                {
                    //ClsSystem.mDicMotion[ClsSystem.mEditMotionKey].LoadFromFile(ofd.FileName);
                    //StreamからMotion読込
                    //StreamReader sr = new StreamReader(ofd.FileName);

//                  newMotion.LoadFromFile(ofd.FileName); //hapを開いて中のモーションを取り出してロードする必要がある
                    this.treeView_Project_AddMotion("test");

                    //TreeView登録

                    //treeView_project_Rebuild();
                    this.treeView_Project.Refresh();
                }
            }

            ofd.Dispose();
        }
        private void SaveProject_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".hap";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //*.hapには各モーションの情報を全て出力する感じに
                /*
                if (ClsSystem.mEditMotionKey >= 0)
                {
                    ClsSystem.mDicMotion[ClsSystem.mEditMotionKey].SaveToFile(sfd.FileName);
                }
                */
            }
            sfd.Dispose();
        }
        private void TSMenu_ImageList_Click(object sender, EventArgs e)
        {
            if (this.mFormImageList == null)
            {
                this.mFormImageList = new FormImageList(this);
                this.mFormImageList.Show();
            }
            else
            {                                
                this.mFormImageList.Close();
                this.mFormImageList.Dispose();
                this.mFormImageList = null;
            }
        }
        private void TSMenu_Control_Click(object sender, EventArgs e)
        {
        }
        private void TSMenu_Attribute_Click(object sender, EventArgs e)
        {
        }
        private void TSMenu_CellList_Click(object sender, EventArgs e)
        {
        }
        private void CB_ImageList_CheckedChanged(object sender, EventArgs e)
        {
            if (mFormImageList != null) mFormImageList.Visible = checkBox_ImageList.Checked;
        }
        private void CB_Control_CheckedChanged(object sender, EventArgs e)
        {
            if(mFormControl!=null) mFormControl.Visible = checkBox_Control.Checked;
        }
        private void CB_Attribute_CheckedChanged(object sender, EventArgs e)
        {
            if(mFormAttribute!=null) mFormAttribute.Visible = checkBox_Attribute.Checked;
        }
        private void CB_CellList_CheckedChanged(object sender, EventArgs e)
        {
            if(mFormCell!=null) mFormCell.Visible = checkBox_CellList.Checked;
        }
        private void Botton_AlingForm_Click(object sender, EventArgs e)
        {
            AlingForms();
        }
        private void AlingForms()
        {
            //AllForm Alingment
            //フォームをメイン基準で並べる
            //アトリビュート
            this.mFormAttribute.Location = new Point(Location.X + Width, Location.Y);
            this.mFormControl.Location   = new Point(Location.X, Location.Y + Height);
            this.mFormImageList.Location = new Point(Location.X - this.mFormImageList.Width, Location.Y);
            this.mFormCell.Location      = new Point(Location.X - this.mFormCell.Width, Location.Y + Height);
        }
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            this.mKeys = e.KeyData;
            this.mKeysSP = e.Modifiers;
        }
        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            this.mKeys = Keys.None;
            this.mKeysSP = Keys.None;

            if (e.KeyData == Keys.Delete)
            {
//ここで確認ダイアログ表示（編集中のモーションを保存しますか？）

                //Element Remove
                int inMotionKey = this.GetMotionSelectedKey();
                if (inMotionKey >= 0)
                {
                    //以下、TreeNode削除処理
                    TreeNode clTreeNode = this.treeView_Project.TopNode;
                    while (clTreeNode != null)
                    {
                        int inKeyTmp = clTreeNode.GetHashCode();
                        if (inKeyTmp == inMotionKey)
                        {
                            clTreeNode.Remove();
                            break;
                        }

                        clTreeNode = clTreeNode.NextNode;
                    }

                    //以下、Motion削除処理
                    if (ClsSystem.mMotionSelectKey == inMotionKey)
                    {
                        //編集中のモーションが削除されたので、
                        //コントロールウィンドウとメインウィンドウの情報をクリアする

                        ClsSystem.mMotionSelectKey = -1;

                        //以下、コントロール設定処理
                        this.SetName(null);
                        this.mFormControl.SetName(null);
                        this.mFormAttribute.SetName(null);
                    }

                    //以下、Motionクラス削除処理
                    bool isExist = ClsSystem.mDicMotion.ContainsKey(inMotionKey);
                    if (isExist)
                    {
                        ClsDatMotion clMotion = ClsSystem.mDicMotion[inMotionKey];
                        clMotion.RemoveAll();
                        ClsSystem.mDicMotion.Remove(inMotionKey);    //モーションクラス削除処理

                        this.panel_PreView.Refresh();
                        this.treeView_Project_Update();
                    }

                    /*
                    int? inActiveIndex = ClsSystem.mDicMotion[inMotionKey].EditFrame.ActiveIndex;
                    if (inActiveIndex != null)
                    {
                        ClsSystem.mDicMotion[inMotionKey].EditFrame.Remove((int)inActiveIndex);
                        this.panel_PreView.Refresh();
                        this.treeView_Project_Update();
                    }

                    //以下、TreeNode削除処理
                    TreeNode clTreeNode = this.treeView_Project.TopNode;
                    while (clTreeNode != null)
                    {
                        int inKeyTmp = clTreeNode.GetHashCode();
                        if (inKeyTmp== inMotionKey)
                        {
                            clTreeNode.Remove();
                            break;
                        }

                        clTreeNode = clTreeNode.NextNode;
                    }

                    //以下、Motion削除処理
                    if (ClsSystem.mMotionSelectKey == inMotionKey)
                    {
//編集中のモーションが削除されたので、
//コントロールウィンドウとメインウィンドウの情報をクリアする

                        ClsSystem.mMotionSelectKey = -1;
                    }

                    //以下、Motionクラス削除処理
                    bool isExist = ClsSystem.mDicMotion.ContainsKey(inMotionKey);
                    if (isExist)
                    {
                        ClsDatMotion clMotion = ClsSystem.mDicMotion[inMotionKey];
                        if (clMotion != null)
                        {
                            clMotion.Clear();   //全削除するときはこれで良いのだろうか？
                        }
                        ClsSystem.mDicMotion.Remove(inMotionKey);    //モーションクラス削除処理
                    }
                    */
                }
            }
        }
        private void FormMain_Resize(object sender, EventArgs e)
        {
            panel_PreView.Refresh();
        }
        /// 終了処理
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //以下、ウィンドウ情報保存処理
            ClsSystem.mSetting.mWindowImageList.mLocation = this.mFormImageList.Location;
            ClsSystem.mSetting.mWindowImageList.mSize = this.mFormImageList.Size;
            ClsSystem.mSetting.mWindowControl.mLocation = this.mFormControl.Location;
            ClsSystem.mSetting.mWindowControl.mSize = this.mFormControl.Size;
            ClsSystem.mSetting.mWindowAttribute.mLocation = this.mFormAttribute.Location;
            ClsSystem.mSetting.mWindowAttribute.mSize = this.mFormAttribute.Size;
            ClsSystem.mSetting.mWindowCell.mLocation = this.mFormCell.Location;
            ClsSystem.mSetting.mWindowCell.mSize = this.mFormCell.Size;
            ClsSystem.mSetting.mWindowMain.mLocation = this.Location;
            ClsSystem.mSetting.mWindowMain.mSize = this.Size;

            //以下、コントロール保存処理
            ClsSystem.mSetting.mWindowMain_DrawGird = this.checkBox_GridCheck.Checked;
            ClsSystem.mSetting.mWindowMain_DrawCross = this.checkBox_CrossBar.Checked;
            ClsSystem.mSetting.mWindowMain_CellList = this.checkBox_CellList.Checked;
            ClsSystem.mSetting.mWindowMain_Attribute = this.checkBox_Attribute.Checked;
            ClsSystem.mSetting.mWindowMain_Control = this.checkBox_Control.Checked;
            ClsSystem.mSetting.mWindowMain_ImageList = this.checkBox_ImageList.Checked;
            ClsSystem.mSetting.mWindowMain_GridSnap = this.checkBox_Snap.Checked;
            ClsSystem.mSetting.mWindowMain_WidthGrid = (int)this.numericUpDown_Grid.Value;

            //以下、終了処理
            ClsSystem.Exit();
        }
        private void CheckButton_Changed(object sender, EventArgs e)
        {
            panel_PreView.Refresh();
        }
        private void timerMain_Tick(object sender, EventArgs e)
        {
            if (this.mFormImageList != null)
            {
                if (this.mFormImageList.IsDisposed)
                {
                    this.mFormImageList.Dispose();
                    this.mFormImageList = null;
                }
            }

            if (this.mFormControl != null)
            {
                if (this.mFormControl.IsDisposed)
                {
                    this.mFormControl.Dispose();
                    this.mFormControl = null;
                }
            }

            if (this.mFormAttribute != null)
            {
                if (this.mFormAttribute.IsDisposed)
                {
                    this.mFormAttribute.Dispose();
                    this.mFormAttribute = null;
                }
            }

            this.ToolStripMenuItem_ImageList.Checked = (this.mFormImageList != null);
            this.ToolStripMenuItem_Control.Checked = (this.mFormControl != null);
            this.ToolStripMenuItem_Attribute.Checked = (this.mFormAttribute != null);
        }
        private void ZoomLevel_ValueChanged(object sender, EventArgs e)
        {
            panel_PreView.Refresh();
        }

        //TreeView_Project
        private void treeView_Project_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //ReName ProjectName
            if (e.Node.ImageIndex == 0)
            {
            }

            //ReName MotionName
            if (e.Node.ImageIndex == 2)
            {
                //this.mNowMotionName = e.Label;

                int inHashCode = e.Node.GetHashCode();
                ClsDatMotion clMotion = ClsSystem.mDicMotion[inHashCode];
                if (clMotion != null)
                {
                    clMotion.SetName(e.Label);
                }

                //以下、各コントロールの設定
                this.SetName(clMotion);
                this.mFormControl.SetName(clMotion);
                this.mFormAttribute.SetName(clMotion);
            }

            //ReName ElementsName
            if (e.Node.ImageIndex == 4)
            {
                //e.Label:新Text e.node.TExt:旧Text

                TreeNode clTreeNode = this.FindTopNodeFromChildNode(e.Node);
                int inHashCode = clTreeNode.GetHashCode();
                ClsDatMotion clMotion = ClsSystem.mDicMotion[inHashCode];
                if (clMotion != null)
                {
                    ClsDatElem clElem = clMotion.GetSelectElem();
                    if (clElem != null)
                    {
                        clElem.SetName(e.Label);
                    }
                }

                //以下、各コントロールの設定
                this.SetName(clMotion);
                this.mFormControl.SetName(clMotion);
                this.mFormAttribute.SetName(clMotion);
            }
            
        }

        private TreeNode FindTopNodeFromChildNode(TreeNode clNode)
        {
            if (clNode == null) return (clNode);

            while (clNode.Parent != null)
            {
                clNode = clNode.Parent;
            }

            return (clNode);
        }

        private void treeView_Project_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //ノードがエレメントかどうか確認する
            //nodeのimageindexから判別

            bool isHit = false;

            //Select Motion Node
            //MotionNodeか確認
            if (e.Node.ImageIndex == 2)
            {
                int inHashCode = e.Node.GetHashCode();

                //以下、モーションインデックス変更処理
                ClsSystem.mMotionSelectKey = inHashCode;

                bool isExist = ClsSystem.mDicMotion.ContainsKey(ClsSystem.mMotionSelectKey);
                if (isExist)
                {
                    ClsDatMotion clMotion = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey];
                    clMotion.SetSelectElem(-1);
                }

                isHit = true;
            }

            //Select Elements Node
            if (e.Node.ImageIndex == 4)
            {
                TreeNode clNode = FindTopNodeFromChildNode(e.Node);
                int inMotionKey = clNode.GetHashCode();

                //以下、モーションインデックス変更処理
                ClsSystem.mMotionSelectKey = inMotionKey;

                bool isExist = ClsSystem.mDicMotion.ContainsKey(inMotionKey);
                if (isExist)
                {
                    ClsDatMotion clMotion = ClsSystem.mDicMotion[inMotionKey];
                    int inElemKey = e.Node.GetHashCode();
                    clMotion.SetSelectElem(inElemKey);
                }

                isHit = true;
            }

            if (isHit)
            {
                //以下、各種コントロール設定処理
                this.treeView_Project.SelectedNode = e.Node;

                //以下、ウィンドウ名を修正する処理
                bool isExist = ClsSystem.mDicMotion.ContainsKey(ClsSystem.mMotionSelectKey);
                if (isExist)
                {
                    ClsDatMotion clMotion = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey];
                    this.SetName(clMotion);
                    this.mFormAttribute.SetName(clMotion);
                    this.mFormControl.SetName(clMotion);
                }

                //新しく選択したモーションをメインウィンドウに表示する
                //新しく選択したモーションをコントロールウィンドウに表示する
            }
            else
            {
                //以下、モーションインデックス変更処理
                ClsSystem.mMotionSelectKey = -1;

                //以下、コントロール設定処理
                this.SetName(null);
                this.mFormControl.SetName(null);
                this.mFormAttribute.SetName(null);
            }

            this.panel_PreView.Refresh();
        }

        private void treeView_Project_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            //未使用 将来的に使うかもしれない
            //TreeViewのDrawNodeを変更しデザイナーcs側でイベントに登録して使う
            //カスタム(TreeView.DrawMode=OwnerDrawAll)描画
            //ノード単位の自前描画
            //BG
            e.Graphics.FillRectangle(Brushes.Black, e.Bounds.Location.X, e.Bounds.Location.Y, e.Bounds.Width, e.Bounds.Height);
            //Icon
            e.Graphics.DrawImage(imageList_Thumb.Images[e.Node.ImageIndex], e.Bounds.X, e.Bounds.Y);
            e.Graphics.DrawString(e.Node.Text,Font,Brushes.White, e.Bounds.Location.X,e.Bounds.Location.Y);
        }
        /// <summary>
        /// treeView_Project更新
        /// </summary>
        public void treeView_Project_Update()
        {
            //現在のMotionNodeを探す モーション名決め打ちなので将来略
            //TreeNode tn = treeView_Project.Nodes["Motion"];
            //TreeViewて複数選択できないじゃーん！！！！
            //Add Editing AllElements
            bool isExist = ClsSystem.mDicMotion.ContainsKey(ClsSystem.mMotionSelectKey);
            if (isExist)
            {
                ClsDatMotion clMotion = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey];
                for (int cnt = 0; cnt < clMotion.mListElem.Count; cnt++)
                {
                    ClsDatElem elm = clMotion.mListElem[cnt];
                    TreeNode tn = treeView_Project.Nodes["Motion"].Nodes[elm.mName];
                    if (tn == null) continue;
                    if (elm.isSelect)
                    {
                        tn.ImageIndex = 3;//選択中
                    }
                    else
                    {
                        tn.ImageIndex = 4;//非選択
                    }
                }
            }
        }

        private TreeNode FindSelectTreeNode()
        {
            if (ClsSystem.mMotionSelectKey < 0) return (null);

            TreeNode clTreeNode = this.treeView_Project.TopNode;
            while (clTreeNode != null)
            {
                int inHashCode = clTreeNode.GetHashCode();
                if (inHashCode == ClsSystem.mMotionSelectKey) break;

                clTreeNode = clTreeNode.NextNode;
            }

            return (clTreeNode);
        }

        /// <summary>
        /// CellからElementを作成し追加
        /// </summary>
        /// <param name="work"></param>
        /// <param name="x">クリック座標(Cliant)</param>
        /// <param name="y">クリック座標(Cliant)</param>
        private void treeView_Project_AddElements(ImageChip work, int x, int y)
        {
            if (ClsSystem.mMotionSelectKey < 0) return;

            TreeNode clTreeNode = this.FindSelectTreeNode();
            if (clTreeNode == null) return;

            bool isExist = ClsSystem.mDicMotion.ContainsKey(ClsSystem.mMotionSelectKey);
            if (!isExist) return;

            ClsDatMotion clMotion = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey];

            //アイテムの登録
            ClsDatElem elem = new ClsDatElem();
            elem.ImageChipID = work.GetHashCode();
            elem.mAttInit.Width = work.Img.Width;
            elem.mAttInit.Height = work.Img.Height;

            //センターからの距離に変換
            x -= panel_PreView.Width / 2;
            y -= panel_PreView.Height / 2;

            //さらに画像サイズ半分シフトして画像中心をセンターに
            x -= elem.mAttInit.Width / 2;
            y -= elem.mAttInit.Height / 2;

            elem.mAttInit.Position = new Vector3(x, y, 0);

            //Show - Attribute
            this.mFormAttribute.SetAllParam(elem.mAttInit);

            clMotion.AddElements(elem);//Elements登録
            //clMotion.Store();//

            //以下、行番号とタブを割り振る処理
            clMotion.Assignment();

            /*
            //TreeViewへの登録
            //TreeViewの選択中Motionを取得
            TreeNode clTreeNodeAdd = clTreeNode.Nodes.Add(elem.mName, elem.mName);
            clTreeNode.Expand();
            clTreeNode.Nodes[elem.mName].Tag = elem.GetHashCode();
            clTreeNode.Nodes[elem.mName].ImageIndex = 4;
            clTreeNode.Nodes[elem.mName].SelectedImageIndex = 3;
            */

            //Control更新
            this.mFormControl.Refresh();
        }

        /*
        private void treeView_Project_RemoveElements(string name)
        {
            //Elements選択中のDelキー
        }
        */

        private ClsDatMotion treeView_Project_AddMotion(string clMotionName)
        {
            treeView_Project.SelectedNode = treeView_Project.TopNode;
            TreeNode tn = treeView_Project.Nodes.Add(clMotionName);
            tn.ImageIndex = 2;
            tn.SelectedImageIndex = 2;
            tn.Tag = ClsSystem.mDicMotion.Count; //不要？

            //以下、モーションクラス生成処理
            int inHashCode = tn.GetHashCode();
            ClsDatMotion clMotion = new ClsDatMotion(inHashCode, clMotionName);
            ClsSystem.mDicMotion.Add(inHashCode, clMotion);

            return (clMotion);
        }

        private int GetMotionSelectedKey()
        {
            TreeNode clTreeNode = this.treeView_Project.TopNode;
            while (clTreeNode != null) {
                if (clTreeNode.IsSelected)
                {
                    int inHashCode = clTreeNode.GetHashCode();
                    bool isExist = ClsSystem.mDicMotion.ContainsKey(inHashCode);
                    if (isExist)
                    {
                        return (inHashCode);
                    }
                }
                clTreeNode = clTreeNode.NextNode;
            }

            return (-1);
        }

        private void treeView_Project_RemoveMotion(string name)
        {
        }
        private void treeView_Project_DragDrop(object sender, DragEventArgs e)
        {
            //ドロップされたデータがTreeNodeか調べる
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                TreeView tv = (TreeView)sender;
                //元ノード取得
                TreeNode src = (TreeNode)e.Data.GetData(typeof(TreeNode));
                //ドロップ先のTreeNodeを取得する
                TreeNode dest = tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));
                //Motionのみの移動に限定する
                if (dest !=null && dest.Parent !=null && src.Parent !=null && dest !=src && IsMotionNode(src) && IsMotionNode(dest) && !IsChildNode(src,dest))
                {
                    //ドロップされたNodeのコピーを作成
                    TreeNode cln = (TreeNode)src.Clone();
                    dest.Nodes.Add(cln);
                    dest.Expand();
                    tv.SelectedNode = cln;
                    //構造をTimelineに反映させる
                    //src to dest
                    //destの種類に応じて親になるか子になるか振分
                    //ルート -> 同レベルの前へ移動
                    if(dest.Parent==null)
                    {

                    }
                    //１つ上がルート ->　同レベルの次へ移動
                    //上記以外 -> 対象の子へ移動
                    //※対象がイメージタイプでは無い場合どうするか？

                    if (ClsSystem.mMotionSelectKey >= 0)
                    {
                        /* ※データ構造が変わったので一旦コメントアウト comment out by yoshi 2017/01/08
                        if (ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey].EditFrame.Move(src.Name, dest.Name, true) == false) {
                            Console.WriteLine("Elements move False");
                        }
                        */
                    }
                                      
                }else e.Effect = DragDropEffects.None;
            } else e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// あるTreeNodeが別のTreeNodeの子ノードか調べる
        /// </summary>
        /// <param name="parentNode">親ノードか調べるTreeNode</param>
        /// <param name="childNode">子ノードか調べるTreeNode</param>
        /// <returns>子ノードの時はTrue</returns>
        private static bool IsChildNode(TreeNode parentNode, TreeNode childNode)
        {
            if (childNode.Parent == parentNode) return true;
            else if (childNode.Parent != null)  return IsChildNode(parentNode, childNode.Parent);
            else return false;
        }
        /// <summary>
        /// ノードが指定の名前を含むか(親を遡り)確認する
        /// </summary>
        /// <param name="src"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static bool IsMotionNode(TreeNode src,string name="Motion")
        {
            if (src.Name == name) return true; //それ自体がモーション
            else if(src.Parent!=null)return IsMotionNode(src.Parent, name);
            else return false;
        }  

        private void treeView_Project_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //ドラッグ開始
            TreeView tv = (TreeView)sender;
            tv.SelectedNode = (TreeNode)e.Item;
            tv.Focus();
            //ノードのドラッグを開始する
            DragDropEffects dde = tv.DoDragDrop(e.Item, DragDropEffects.All);
            //移動した時は、ドラッグしたノードを削除する
            if ((dde & DragDropEffects.Move) == DragDropEffects.Move)
            {
                tv.Nodes.Remove((TreeNode)e.Item);
            }
        }
        private void treeView_Project_DragOver(object sender, DragEventArgs e)
        {
            //TreeNodeか確認
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                //CtrlKey(8)== Copy / nonkey==Move
                if ((e.KeyState & 8) == 8 && (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy) { e.Effect = DragDropEffects.Copy; }
                else if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move) { e.Effect = DragDropEffects.Move; }
                else { e.Effect = DragDropEffects.None; }
            }
            else
                //TreeNodeでなければ受け入れない
                e.Effect = DragDropEffects.None;

            //マウス下のNodeを選択する
            if (e.Effect != DragDropEffects.None)
            {
                TreeView tv = (TreeView)sender;
                //マウスのあるNodeを取得する
                TreeNode dst = tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));
                //ドラッグされているNodeを取得する
                TreeNode src = (TreeNode)e.Data.GetData(typeof(TreeNode));
                //マウス下のNodeがドロップ先として適切か調べる
                if (dst != null && dst != src && !IsChildNode(src, dst))
                {
                    //Nodeを選択する
                    if (dst.IsSelected == false) tv.SelectedNode = dst;
                }
                else e.Effect = DragDropEffects.None;
            }
        }

        private void button_MotionNew_Click(object sender, EventArgs e)
        {
            this.treeView_Project_AddMotion("NewMotion");
            
            //モーションである事を示すタグを付加する？
        }

        private void panel_ProjectTopBase_Click(object sender, EventArgs e)
        {
            //ProjectPaineの開閉(おまけ)
            treeView_Project.Visible = !treeView_Project.Visible;
        }

        //PanelPreView周り
        private void PanelPreView_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(ClsSystem.mSetting.mMainColorBack);

            //以下、拡大してボケないようにする処理
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            //e.Graphics.PixelOffsetMode   = PixelOffsetMode.HighQuality;

            //画像で背景fill
            //e.Graphics.FillRectangle(new TextureBrush(Properties.Resources.Blank),new Rectangle(0,0,PanelPreView.Width,PanelPreView.Height));
            float zoom = HScrollBar_ZoomLevel.Value / mParZOOM;//ZoomLevel(2-80)1/10にして使う
            if (zoom < 0.2) zoom = 0.2f;//下限を(0.2)1/5とする
            float grid = (float)numericUpDown_Grid.Value;

            e.Graphics.TranslateTransform(  panel_PreView.Width/2 -panel_PreView.Width/2*zoom,
                                            panel_PreView.Height/2 -panel_PreView.Height/2*zoom);
            e.Graphics.ScaleTransform(zoom, zoom);
            //GridBar
            if (checkBox_GridCheck.Checked)
            {
                //Grid Draw
                // V
                var p1 = new Pen(ClsSystem.mSetting.mMainColorGrid);
                for (float cnt = ((float)panel_PreView.Width / 2) % (grid ); cnt < panel_PreView.Width; cnt += (grid))
                {
                    e.Graphics.DrawLine(p1, cnt, 0.0f, cnt, panel_PreView.Height);
                }
                //H
                for (float cnt = ((float)(panel_PreView.Height / 2) % (grid )); cnt < panel_PreView.Height; cnt += (grid))
                {
                    e.Graphics.DrawLine(p1, 0.0f, cnt, panel_PreView.Width, cnt);
                }
            }

            // 各種Itemの描画処理
            // DrawItems
            Matrix back = e.Graphics.Transform;
            if (ClsSystem.mMotionSelectKey >= 0)
            {
//              if (ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey].EditFrame != null)
                {
                    PanelPreview_Paint_DrawParts(sender, e.Graphics);
                }
            }
            e.Graphics.Transform = back;
            //CrossBar スクリーン移動時は原点に沿う形に
            if(checkBox_CrossBar.Checked)
            {
                var p1 = new Pen(ClsSystem.mSetting.mMainColorCenterLine);
                e.Graphics.DrawLine(p1,panel_PreView.Width / 2, 0, panel_PreView.Width/2, panel_PreView.Height);//V
                e.Graphics.DrawLine(p1, 0, panel_PreView.Height/2, panel_PreView.Width,panel_PreView.Height/2);//H
            }
            
        }
        /// <summary>
        /// 1エレメント描画処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="g"></param>
        private void PanelPreview_Paint_DrawParts(object sender, Graphics g)
        {
            //表示の仕方も悩む　親もマーク表示するか　等
            //StageInfomation
            float zoom = HScrollBar_ZoomLevel.Value / mParZOOM;
            if (zoom < 0.2) zoom = 0.2f;//縮小Zoom制限 制限しないと0除算エラー

            //View Center X,Y
            int vcx = mScreenScroll.X + panel_PreView.Width / 2;//ViewCenter X
            int vcy = mScreenScroll.Y + panel_PreView.Height / 2;//ViewCenter Y

            bool isExist = ClsSystem.mDicMotion.ContainsKey(ClsSystem.mMotionSelectKey);
            if (isExist)
            {
                ClsDatMotion clMotion = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey];
                clMotion.DrawElem(g, vcx, vcy);
            }
        }
        /// <summary>
        /// ドラッグアンドドロップ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelPreView_DragDrop(object sender, DragEventArgs e)
        {
            float zoom = HScrollBar_ZoomLevel.Value / mParZOOM;
            Point sPos = panel_PreView.PointToClient(new Point(e.X, e.Y));

            //PNGファイル直受け入れ
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //1画像 1CELL 1Element
                //File
                string[] AllPaths = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string str in AllPaths)
                {
                    string ext = System.IO.Path.GetExtension(str).ToLower();
                    if (ext == ".png")
                    {
                        ImageChip c = new ImageChip();
                        c.FromPngFile(str);
                        ClsSystem.ImageMan.AddImageChip(c);
                        this.treeView_Project_AddElements(c, sPos.X, sPos.Y);

                        //ImageListへ登録と更新
                        this.mFormImageList.AddItem(str);
                        this.mFormImageList.Refresh();

                        //CellListの表示更新
                        this.mFormCell.Refresh();
                    }
                }
                e.Effect = DragDropEffects.Copy;
            }

            //ListViewItem受け入れ
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem lvi = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                //Cellの登録 Image Item
                ImageChip work = new ImageChip();
                
                work.Img = (Bitmap)lvi.ImageList.Images[lvi.ImageIndex];
                //画像そのままならこれでいいが一部切り抜きとなると変更
                // ！！！どうやらオリジナル画像でなくサムネらしい！！！
                //必要なのはmFormImagelist.mListImageの中身っぽい？
                
                work.Rect = new Rectangle(0, 0, work.Img.Width, work.Img.Height);
                ClsSystem.ImageMan.AddImageChip(work);//画像サイズ登録実画像はいずこ！

                treeView_Project_AddElements(work, sPos.X, sPos.Y);
                e.Effect = DragDropEffects.Copy;
            }

            //CELL 受け入れ
            if (e.Data.GetDataPresent(typeof(ImageChip)))
            {
                //Store Cell Item
                ImageChip work = (ImageChip)e.Data.GetData(typeof(ImageChip));
                ClsSystem.ImageMan.AddImageChip(work);//画像登録

                //PreViewに配置し更新
                Point a = panel_PreView.PointToClient(new Point(e.X, e.Y));
                treeView_Project_AddElements(work, a.X, a.Y);

                e.Effect = DragDropEffects.Copy;
            }
            if (e.Data.GetType() == typeof(ELEMENTS))
            { e.Effect = DragDropEffects.Copy; }
            if (e.Data.GetType() == typeof(Image))
            { e.Effect = DragDropEffects.Copy; }

            panel_PreView.Refresh();
        }
        private void PanelPreView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            //Drop受け入れ準備
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            { e.Effect = DragDropEffects.Copy; }

            if (e.Data.GetDataPresent(typeof(ImageChip)))
            { e.Effect = DragDropEffects.Copy; }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            { e.Effect = DragDropEffects.Copy; }
        }
        private void PanelPreView_MouseWheel(object sender, MouseEventArgs e)
        {
            mWheelDelta = (e.Delta > 0)? + 1:-1 ;//+/-に適正化

            bool isExist = ClsSystem.mDicMotion.ContainsKey(ClsSystem.mMotionSelectKey);
            if (isExist)
            {
                ClsDatMotion clMotion = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey];

                /* ※データ構造が変わったので一旦コメントアウト comment out by yoshi 2017/01/08
                ELEMENTS nowEle = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey].EditFrame.GetActiveElements();
                //アイテム選択中のホイール操作
                if (mKeysSP == Keys.Shift)
                {
                    //Shift+Wheel 部品の拡縮 0.1単位 最小0.1に制限
                    mDragState = DragState.Scale;
                    nowEle.Atr.Scale.X += ((nowEle.Atr.Scale.X + mWheelDelta / 10f) > 0.1f) ? mWheelDelta / 10f : 0.1f;
                    nowEle.Atr.Scale.Y += ((nowEle.Atr.Scale.Y + mWheelDelta / 10f) > 0.1f) ? mWheelDelta / 10f : 0.1f;
                    //nowEle.Atr.Scale.Z += ((nowEle.Atr.Scale.Z + mWheelDelta / 10f) > 0.1f) ? mWheelDelta / 10f : 0.1f;
                }
                if (mKeysSP == Keys.Control)
                {
                    //Ctrl+Wheel 回転 1度単位
                    mDragState = DragState.Angle;

                    float w = nowEle.Atr.Radius.Z + mWheelDelta;
                    if (w >= 360)
                    {
                        nowEle.Atr.Radius.Z = w % 360;
                    }
                    else if (w < 0)
                    {
                        //nowEle.Atr.Radius.Z = 360 - (float)Math.Acos(w % 360);
                        nowEle.Atr.Radius.Z = (w % 360) + 360;
                    }
                    else
                    {
                        nowEle.Atr.Radius.Z += mWheelDelta;
                    }
                }
                mFormAttribute.SetAllParam(nowEle.Atr);
                panel_PreView.Refresh();
                */
            }
            else
            {
                //画面の拡大縮小
                if (mWheelDelta > 0)
                {
                    if (HScrollBar_ZoomLevel.Value > HScrollBar_ZoomLevel.Minimum) HScrollBar_ZoomLevel.Value -= mWheelDelta;
                }
                else
                {
                    if (HScrollBar_ZoomLevel.Value < HScrollBar_ZoomLevel.Maximum) HScrollBar_ZoomLevel.Value -= mWheelDelta;
                }

                mWheelDelta = 0;
            }

            StatusLabel2.Text = $"{mWheelDelta}";
        }
        private void PanelPreView_MouseUp(object sender, MouseEventArgs e)
        {
            //releaseMouse
            mMouseLDown = false;
            //mMouseMDown = false;
            //mMouseRDown = false;
            mDragState = DragState.none;
        }
        private void PanelPreView_MouseDown(object sender, MouseEventArgs e)
        {
            //e.X,Yからステージ上の座標にする
            float zoom = HScrollBar_ZoomLevel.Value / mParZOOM;
            float stPosX = ((e.X -(panel_PreView.Width  / 2)) / zoom);
            float stPosY = ((e.Y -(panel_PreView.Height / 2)) / zoom);

            
            if (e.Button == MouseButtons.Left)
            {
                mMouseDownPoint = new Point(e.X-(panel_PreView.Width/2),e.Y-(panel_PreView.Height/2));

                //アイテム検索
                bool isExist = ClsSystem.mDicMotion.ContainsKey(ClsSystem.mMotionSelectKey);
                if (isExist)
                {
                    ClsDatMotion clMotion = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey];
                    /* こういう風にする？
                    ClsDatElem clElem = clMotion.FindElemFromPosition((int)stPosX, (int)stPosY);
                    if(clElem!= null)
                    {
                    }
                    */

                    /* ※データ構造が変わったので一旦コメントアウト comment out by yoshi 2016/01/09
                    this.SetNowElementsIndex(clMotion.EditFrame.SelectElement((int)stPosX, (int)stPosY, true));
                    ELEMENTS nowEle = clMotion.EditFrame.GetActiveElements();

                    //Item選択中なら移動変形処理等の準備
                    if (nowEle != null)
                    {
                        mMouseDownShift.X = (int)(nowEle.Atr.Position.X - stPosX);
                        mMouseDownShift.Y = (int)(nowEle.Atr.Position.Y - stPosY);
                    }

                    this.mMouseLDown = true;
                    this.panel_PreView.Refresh();
                    this.treeView_Project.Refresh();
                    this.mFormControl.Refresh();
                    */
                }
            }
        }
        private void PanelPreView_MouseMove(object sender, MouseEventArgs e)
        {
            float zoom = HScrollBar_ZoomLevel.Value / mParZOOM;
            //e.X,Yからステージ上の座標にする
            float stPosX = (e.X - (panel_PreView.Width  / 2)) / zoom;
            float stPosY = (e.Y - (panel_PreView.Height / 2)) / zoom;

            bool isExist = ClsSystem.mDicMotion.ContainsKey(ClsSystem.mMotionSelectKey);
            if (isExist)
            {
                ClsDatMotion clMotion = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey];

                /* ※データ構造が変わったので一旦コメントアウト comment out by yoshi 2017/01/08
                ELEMENTS nowEle = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey].EditFrame.GetActiveElements();
                if (nowEle != null)
                {
                    //移動処理
                    if (mMouseLDown)
                    {
                        if (nowEle != null)
                        {
                            //+CTRL マウスでの回転 左周りにしかなってない
                            if (mKeysSP == Keys.Control)
                            {
                                int w = (int)nowEle.Atr.Radius.Z + ((int)stPosX - mMouseDownPoint.X) / 4;
                                if (w < 0)
                                {
                                    //右回転
                                    w = 360 + (w % 360);
                                    nowEle.Atr.Radius.Z = w;
                                }
                                else
                                {
                                    //左回転
                                    w %= 360;
                                    nowEle.Atr.Radius.Z = w;
                                }
                                //別キーも押されていれば別軸回転 等(将来)
                            }
                            //if( mKeysSP==Keys.Alt) //将来用
                            else
                            {
                                //移動
                                //差分加算
                                mDragState = DragState.Move;
                                nowEle.Atr.Position.X = stPosX + mMouseDownShift.X;
                                nowEle.Atr.Position.Y = stPosY + mMouseDownShift.Y;
                                //mFormAttribute.SetAllParam(nowEle.Atr);
                            }
                        }
                        else
                        {
                            //アイテム選択が無い場合のLドラッグはステージのXYスクロール
                            mDragState = DragState.Scroll;
                            mScreenScroll.X = (e.X - (panel_PreView.Width / 2)) - mMouseDownPoint.X;
                            mScreenScroll.Y = (e.Y - (panel_PreView.Height / 2)) - mMouseDownPoint.Y;
                        }
                        mFormAttribute.SetAllParam(nowEle.Atr);
                        panel_PreView.Refresh();
                    }
                }
                
                StatusLabel.Text = $"[X:{stPosX:####}/Y:{stPosY:####}] [Px:{mMouseDownPoint.X:####}/Py:{mMouseDownPoint.Y:####}][Shift{mMouseDownShift.X}/{mMouseDownShift.Y}]";
                StatusLabel2.Text = $" [Select:{ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey].EditFrame.ActiveIndex}][ScX{mScreenScroll.X:###}/ScY{mScreenScroll.Y:###}] [Zoom:{zoom}]{mDragState.ToString()}:{mWheelDelta}";
                */
            }
        }
        private void PanelPreView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //previewKey
            //なぜかイベント発生しない？なんだろ？
            //メインフォームのほうが優先されるらしい keyPreview=True
            //部品選択中か確認

            //GetElement
            bool isExist = ClsSystem.mDicMotion.ContainsKey(ClsSystem.mMotionSelectKey);
            if (!isExist) return;

            ClsDatMotion clMotion = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey];

            //カーソル
            if (e.KeyData == Keys.Shift)
            {
                //Scaling
                //shift+
                if (e.KeyData == Keys.Up)
                {
                    
                }
                if (e.KeyData == Keys.Down)
                {

                }
                if (e.KeyData == Keys.PageUp)
                {

                }

                if (e.KeyData == Keys.PageDown)
                {

                }
            }
            if(e.KeyData == Keys.Control)
            {

            }
            if(e.KeyData==Keys.Delete)
            {
                //Element Remove
                //以下、エレメントを削除する処理
                //どうやって画像のインターフェイスからエレメントキーを取得するのが良いだろうか？
                //clMotion.RemoveElem(inElementKey);
            }
        }
        /// <summary>
        /// エディット中の選択エレメントをインデックス指定と関連画面更新
        /// </summary>
        /// <param name="ElementsIndex"></param>
        public void SetNowElementsIndex(int? ElementsIndex)
        {
            //現在の選択と違う物であれば変更を行う
            if (ClsSystem.mMotionSelectKey <= 0) return;

/*
            if (ElementsIndex == null)
            {
                ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey].EditFrame.ActiveIndex=null;//無選択に
                return;
            }

            int? idx = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey].EditFrame.ActiveIndex;
            if (idx != ElementsIndex)
            {
                //現在の選択を解除
                ELEMENTS elem = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey].EditFrame.GetActiveElements();
                if (elem != null)
                {
                    elem.isSelect = false;
                }

                //更新
                ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey].EditFrame.ActiveIndex = ElementsIndex;
                //新規選択を有効
                elem = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey].EditFrame.GetActiveElements();
                elem.isSelect = true;

                //各種リフレッシュ
                panel_PreView.Refresh();
                treeView_Project_Update();                
                treeView_Project.Refresh();
                mFormAttribute.SetAllParam(elem.Atr);
                mFormAttribute.Refresh();
                mFormControl.Refresh();
            }
*/
        }

        private void BottonTest_Click(object sender, EventArgs e)
        {
            // testCode
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Image img = new Bitmap(fd.FileName);                
                FormImageCut fc = new FormImageCut(this, img,fd.FileName);
            }
        }

        //test
        private void writeImageListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClsSystem.ImageMan.SaveToFile("ImageTest");
        }

        /// <summary>
        /// デバッグ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_DebugGraph_Click(object sender, EventArgs e)
        {
            FormRateGraph clFormRateGraph = new FormRateGraph(this, ClsTween.EnmParam.POSITION_X, 10, 20, 15);
            clFormRateGraph.Show();
        }

        private void ToolStripMenuItem_DebugRootOpen_Click(object sender, EventArgs e)
        {
            string clPath = ClsPath.GetPath();
            Process.Start(clPath);
        }

        private void ToolStripMenuItem_Setting_Click(object sender, EventArgs e)
        {
            FormSetting clFormSetting = new FormSetting();
            DialogResult enResult = clFormSetting.ShowDialog();
            if (enResult != DialogResult.OK) return;

            //以下、設定処理
//ここで背景色を変えたり、グリッド色を変えたりする？
        }

        private void ToolStripMenuItem_DebugSave_Click(object sender, EventArgs e)
        {
            ClsSetting clSetting = new ClsSetting();
            clSetting.Save();
        }

        private void ToolStripMenuItem_DebugLoad_Click(object sender, EventArgs e)
        {
            using (FileStream clStream = new FileStream("C:\\Users\\Yoshi\\Desktop\\hoge.json", FileMode.Open))
            {
                DataContractJsonSerializer clSerializer = new DataContractJsonSerializer(typeof(ClsSetting));
                ClsSetting a = (ClsSetting)clSerializer.ReadObject(clStream);
                clStream.Close();
            }
        }

        private void ToolStripMenuItem_DebugExport_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> clDicFile = new Dictionary<string, object>();

            //以下、ファイル情報出力処理
            clDicFile["ver"] = "0.0.1";
            clDicFile["hogehoge"] = 99;

            //以下、イメージ出力処理
            int inCnt, inMax = ClsSystem.ImageMan.ImageChipList.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ImageChip clCell = ClsSystem.ImageMan.ImageChipList[inCnt];
                string clKey = clCell.ID.ToString();
                clDicFile[clKey] = clCell.Export();
            }

/*
            //以下、アニメ出力処理
            if (ClsSystem.mEditMotionKey >= 0)
            {
                inMax = ClsSystem.mDicMotion[ClsSystem.mEditMotionKey].gmTimeLine.Count;
                for (inCnt = 0; inCnt < inMax; inCnt++)
                {
                    FRAME clFrame = ClsSystem.mDicMotion[ClsSystem.mEditMotionKey].gmTimeLine[inCnt];
                    clDicFile["frm_" + inCnt] = clFrame.Export();   //ここのキーはアニメ名（ユニーク制約にしないとダメ＞＜）としたい
                }
            }
*/

            //以下、DictionaryをJson形式に変換する処理
            string clJsonData = ClsSystem.DictionaryToJson(clDicFile);

            //以下、ファイル出力処理
            string clPath = ClsPath.GetPath();
            string clPathFile = Path.Combine(clPath, "よしさんデバッグ用ファイル.txt");
            File.WriteAllText(clPathFile, clJsonData);
        }
    }
}
