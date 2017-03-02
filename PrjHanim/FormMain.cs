using System;
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
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;

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

        public FormControl mFormControl;
        public FormAttribute mFormAttribute;
        public FormCell mFormCell;

        //private Point mMouseDownShift;
        private Point mScreenScroll;
        private bool mMouseDownL = false;//L
        //private bool mMouseRDown = false;//R
        //private bool mMouseMDown = false;//M
        private int mWheelDelta;//Wheel
        private Keys mKeys,mKeysSP;//キー情報 通常キー,スペシャルキー

        //編集中の選択中エレメントのインデックス 非選択=null
        //これはTimeLine内のFrameあたりに移動させたいなぁ 11/11移動
        //private int? mNowElementsIndex = null;
        
        //private string mNowMotionName;//選択中モーション名

        enum DragState { none,Move, Angle, Scale,Scroll, Joint };
        //private DragState mDragState = DragState.none;

        private Point mMouseOldPoint = Point.Empty;
        private Point mPreViewCenter;   //PanelPreView Centerセンターポジション

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
            this.checkBox_Snap.Checked = ClsSystem.mSetting.mWindowMain_GridSnap;
            this.numericUpDown_Grid.Value = ClsSystem.mSetting.mWindowMain_WidthGrid;

            //以下、TreeNode作成処理
            //初期モーションTreeの追加
            ClsDatMotion clMotion = this.AddMotion("DefMotion");

            //以下、初期化処理
            this.mPreViewCenter = new Point(0, 0);
            this.mScreenScroll = new Point(0, 0);

            this.mFormControl = new FormControl(this);
            this.mFormControl.SetMotion(clMotion);
            this.mFormControl.Show();

            this.mFormAttribute = new FormAttribute(this);
            this.mFormAttribute.Show();

            //Ver2
            mFormCell = new FormCell(this);
            //mFormCell.TopLevel = false;
            //mFormCell.FormBorderStyle = FormBorderStyle.None;
            //mFormCell.Visible = true;
            //mFormCell.Dock = DockStyle.Fill;
            mFormCell.Show();
            //Panel_Chip.Controls.Add(mFormCell);

            //Motion選択状態にする 他フォームの準備完了後
            listView_Motion.Items[0].Selected = true;
            ClsSystem.mMotionSelectKey = listView_Motion.Items[0].GetHashCode();//選択中変更

            //FileHistory追加

            foreach (string str in ClsSystem.mSetting.mFileHistory)
            {
                ToolStripMenuItem tsi = new ToolStripMenuItem(str,null,TSMenu_History_Click);
                tsi.Tag = str;
                //projectHistoryToolStripMenuItem
                projectHistoryToolStripMenuItem.DropDown.Items.Add(tsi);
                //FileToolStripMenuItem.DropDown.Items.Add(tsi);
            }

            //
            if(ClsSystem.mSetting.mProjectAutoReload)
            {
                if (ClsSystem.mSetting.mFileHistory.Count > 1)
                {
                    string clFilePath = ClsSystem.mSetting.mFileHistory[0];
                    this.LoadProject(clFilePath);
                }
            }

            //背景の再描画をキャンセル(ちらつき抑制)
            //効果いまいち
            this.SetStyle(ControlStyles.Opaque, true);

            //以下、各フォーム表示・非表示処理
            if (mFormControl != null) mFormControl.Visible = checkBox_Control.Checked;
            if (mFormAttribute != null) mFormAttribute.Visible = checkBox_Attribute.Checked;
            if (mFormCell != null) mFormCell.Visible = checkBox_CellList.Checked;
        }

        /// <summary>
        /// ウィンドウ名設定
        /// </summary>
        /// <param name="clMotion">選択中のモーション管理クラス</param>
        public void SetName(ClsDatMotion clMotion)
        {
            string clAppName = ClsTool.GetAppFileName();
            this.Text = ClsTool.GetWindowName(clAppName, clMotion);
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
        /// <summary>
        /// 新規プロジェクト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_New_Click(object sender, EventArgs e)
        {
            //NewProject

            if (MessageBox.Show("新規プロジェクトの作成\n既存データは全て初期化されます\nCreate a New Project.\nClear All is OK?", "Cleate Project", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //以下、イメージクリア処理
                ClsSystem.RemoveAllImage();

                //以下、モーションクリア処理
                ClsSystem.RemoveAllMotion();
                listView_Motion.Clear();

                ClsDatMotion clMotion = this.AddMotion("DefMotion");
                //Motion選択状態にする 他フォームの準備完了後
                listView_Motion.Items[0].Selected = true;
                ClsSystem.mMotionSelectKey = listView_Motion.Items[0].GetHashCode();//選択中変更

                mFormControl.RefreshAll();
                mFormCell.Refresh();
            }
        }
        /// <summary>
        /// 読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadProject_Click(object sender, EventArgs e)
        {
            OpenFileDialog clLoadDialog = new OpenFileDialog();
            clLoadDialog.DefaultExt = ".hap";
            clLoadDialog.Filter = "hanim project file(*.hap)|*.hap|all file(*.*)|*.*";
            clLoadDialog.FilterIndex = 1;
            if (clLoadDialog.ShowDialog()==DialogResult.OK)
            {
                this.LoadProject(clLoadDialog.FileName);

                this.listView_Motion.Refresh();

                //以下、選択中のライン番号初期化処理
                foreach (int inKey in ClsSystem.mDicMotion.Keys)
                {
                    ClsSystem.mMotionSelectKey = inKey;
                    break;
                }
                ClsDatMotion clDatMotion = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey];
                clDatMotion.SetSelectFromLineNo(-1);

                //以下、各種コントロール設定処理
                //以下、ウィンドウ名を修正する処理
                this.SetName(clDatMotion);
                this.mFormControl.SetMotion(clDatMotion);
                this.mFormAttribute.Init(null);

                //以下、各種ウィンドウ更新処理
                this.panel_PreView.Refresh();
                this.mFormCell.Refresh();
                this.mFormControl.RefreshAll();

                System.Media.SystemSounds.Exclamation.Play();   //読込完了音
            }
            clLoadDialog.Dispose();
        }

        /// <summary>
        /// プロジェクト読み込み処理
        /// </summary>
        /// <param name="clFilePath">ファイルパス</param>
        public void LoadProject(string clFilePath)
        {
            //以下、イメージクリア処理
            ClsSystem.RemoveAllImage();

            //以下、モーションクリア処理
            ClsSystem.RemoveAllMotion();
            this.listView_Motion.Items.Clear();     //listView_Motion.Items全削除

            //以下、プロジェクトファイル読み込み処理
            ClsSystem.Load(this.listView_Motion, clFilePath);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveProject_Click(object sender, EventArgs e)
        {
            if (ClsSystem.mSetting.mFileHistory.Count >= 1)
            {
                string clFilePath = ClsSystem.mSetting.mFileHistory[0];
                this.SaveProject(clFilePath);

                System.Media.SystemSounds.Exclamation.Play();   //保存完了音
            }
        }

        /// <summary>
        /// 名前を付けて保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_SaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog clSaveDialog = new SaveFileDialog();
            clSaveDialog.DefaultExt = ".hap";
            clSaveDialog.Filter = "hanim project file(*.hap)|*.hap|all file(*.*)|*.*";
            clSaveDialog.FilterIndex = 1;
            if (clSaveDialog.ShowDialog() == DialogResult.OK)
            {
                this.SaveProject(clSaveDialog.FileName);

                //FileHistory Store
                ClsSystem.mSetting.mFileHistory.Insert(0, clSaveDialog.FileName);

                System.Media.SystemSounds.Exclamation.Play();   //保存完了音
            }
            clSaveDialog.Dispose();
        }

        /// <summary>
        /// プロジェクト保存処理
        /// </summary>
        /// <param name="clFilePath">ファイルパス</param>
        private void SaveProject(string clFilePath)
        {
            ClsSystem.Save(clFilePath);
        }

        private void TSMenu_History_Click(object sender,EventArgs e)
        {
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            LoadProject(ts.Text);
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

                    //以下、ListView_Motion.Items 削除処理
                    if(listView_Motion.SelectedItems.Count>=1)
                    {
                        //複数同時選択は禁止になってるので選択の0番目のみでOK　のはず
                        int inHash = listView_Motion.SelectedItems[0].GetHashCode();
                        listView_Motion.SelectedItems[0].Remove();
                        //mDicMotionからの削除
                        ClsDatMotion clMotion = ClsSystem.mDicMotion[inHash];
                        clMotion.Remove();
                        ClsSystem.mDicMotion.Remove(inHash);    //モーションクラス削除処理

                        //編集中のモーションが削除されたので、
                        //コントロールウィンドウとメインウィンドウの情報をクリアする

                        ClsSystem.mMotionSelectKey = -1;

                        //以下、コントロール設定処理
                        this.SetName(null);
                        this.mFormControl.SetMotion(null);
                        this.mFormAttribute.Init(null);

                        this.panel_PreView.Refresh();
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
                this.mFormControl.SetMotion(clMotion);
                this.mFormAttribute.Init(null);
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
            /*
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
                    clMotion.SetSelectLineNo(-1);
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
            */
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
        private void treeView_Project_RemoveMotion(string name)
        {
        }
        //アイテムD&D移動用
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
                if (dest != null && dest.Parent != null && src.Parent != null && dest != src && IsMotionNode(src) && IsMotionNode(dest) && !IsChildNode(src, dest))
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
                    if (dest.Parent == null)
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

                }
                else e.Effect = DragDropEffects.None;
            }
            else e.Effect = DragDropEffects.None;
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
            else if (childNode.Parent != null) return IsChildNode(parentNode, childNode.Parent);
            else return false;
        }
        /// <summary>
        /// ノードが指定の名前を含むか(親を遡り)確認する
        /// </summary>
        /// <param name="src"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static bool IsMotionNode(TreeNode src, string name = "Motion")
        {
            if (src.Name == name) return true; //それ自体がモーション
            else if (src.Parent != null) return IsMotionNode(src.Parent, name);
            else return false;
        }
        //
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

        /// <summary>
        /// treeView_Project更新
        /// </summary>
        public void treeView_Project_Update()
        {
            /*
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
                    tn.ImageIndex = 4;  //非選択
                }
            }
            
            */
        }
        private TreeNode FindSelectTreeNode()
        {
            /*
            if (ClsSystem.mMotionSelectKey < 0) return (null);

            TreeNode clTreeNode = this.treeView_Project.TopNode;
            while (clTreeNode != null)
            {
                int inHashCode = clTreeNode.GetHashCode();
                if (inHashCode == ClsSystem.mMotionSelectKey) break;

                clTreeNode = clTreeNode.NextNode;
            }

            return (clTreeNode);
            */
            return null;
        }

        /// <summary>
        /// CellからElementを作成し追加
        /// </summary>
        /// <param name="clDatMotion">モーション管理クラス</param>
        /// <param name="clDatImage">イメージ管理クラス</param>
        /// <param name="x">クリック座標(Cliant)</param>
        /// <param name="y">クリック座標(Cliant)</param>
        private void AddElement(ClsDatMotion clDatMotion, ClsDatImage clDatImage, int x, int y)
        {
            //アイテムの登録
            ClsDatElem clDatElem = new ClsDatElem(clDatMotion, null);
            clDatElem.SetImage(clDatImage);

            //センターからの距離に変換
            x -= panel_PreView.Width / 2;
            y -= panel_PreView.Height / 2;

            //さらに画像サイズ半分シフトして画像中心をセンターに
            x -= clDatElem.mAttrInit.Width / 2;
            y -= clDatElem.mAttrInit.Height / 2;

            clDatElem.mAttrInit.Position = new Vector3(x, y, 0);

            //Show - Attribute
            this.mFormAttribute.SetAllParam(clDatElem.mAttrInit);

            clDatMotion.AddElements(clDatElem);  //Elements登録

            //以下、行番号とタブを割り振る処理
            clDatMotion.Assignment();

            //Control更新
            this.mFormControl.Refresh();
        }

        private ClsDatMotion AddMotion(string clMotionName)
        {
            ListViewItem clListViewItem = new ListViewItem(clMotionName, 2);
            listView_Motion.Items.Add(clListViewItem);
            clListViewItem.Tag = ClsSystem.mDicMotion.Count;

            ClsDatMotion clMotion = new ClsDatMotion(clListViewItem.GetHashCode(), clMotionName);
            ClsSystem.mDicMotion.Add(clListViewItem.GetHashCode(), clMotion);

            return (clMotion);
        }
        private int GetMotionSelectedKey()
        {
            if (listView_Motion.SelectedItems.Count > 0)
            {
                return listView_Motion.SelectedItems[0].GetHashCode();
            }
            
            /*
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
            */

            return (-1);
        }

        private void button_MotionNew_Click(object sender, EventArgs e)
        {
            this.AddMotion("NewMotion");
            
            //モーションである事を示すタグを付加する？
        }
        private void panel_ProjectTopBase_Click(object sender, EventArgs e)
        {
            //ProjectPaineの開閉(おまけ)
            listView_Motion.Visible = !listView_Motion.Visible;
        }

        //ListView TreeViewの階層構造が不要になるのでListViewへ置き換える
        private void listView_Motion_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            //e.Item == Index
            if (!e.CancelEdit && e.Label!=null && e.Label!="")
            {
                int inHashCode = listView_Motion.Items[e.Item].GetHashCode();
                ClsDatMotion clMotion = ClsSystem.mDicMotion[inHashCode];
                if (clMotion != null)
                {
                    clMotion.SetName(e.Label);
                }
                listView_Motion.Items[e.Item].Text = e.Label;

                //以下、各コントロールの設定
                this.SetName(clMotion);
                this.mFormControl.SetMotion(clMotion);
                this.mFormAttribute.Init(null);
            }
        }
        private void listView_Motion_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //e==選択されたlistViewItem のはず
            int inHashCode = e.Item.GetHashCode();

            //モーション存在確認   
            if (ClsSystem.mDicMotion.ContainsKey(inHashCode))
            {
                //アイテムが存在
                ClsSystem.mMotionSelectKey = inHashCode;//選択中変更
                ClsDatMotion clMotion = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey];
                clMotion.SetSelectFromLineNo(-1);

                //以下、各種コントロール設定処理
                //以下、ウィンドウ名を修正する処理
                this.SetName(clMotion);
                this.mFormControl.SetMotion(clMotion);
                this.mFormAttribute.Init(null);

                //新しく選択したモーションをメインウィンドウに表示する
                //新しく選択したモーションをコントロールウィンドウに表示する
            }
            else
            {
                //非選択
                ClsSystem.mMotionSelectKey = -1;
                //以下、コントロール設定処理
                this.SetName(null);
                this.mFormControl.SetMotion(null);
                this.mFormAttribute.Init(null);
            }
            this.panel_PreView.Refresh();
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
            float flZoom = HScrollBar_ZoomLevel.Value / mParZOOM;//ZoomLevel(2-80)1/10にして使う
            if (flZoom < 0.2) flZoom = 0.2f;//下限を(0.2)1/5とする
            float flGrid = flZoom * (float)numericUpDown_Grid.Value;

            /*
            e.Graphics.TranslateTransform(
                this.panel_PreView.Width/2 -this.panel_PreView.Width/2*zoom,
                this.panel_PreView.Height/2 -this.panel_PreView.Height/2*zoom
            );
            e.Graphics.ScaleTransform(zoom, zoom);
            */

            //以下、グリッド表示処理
            if (this.checkBox_GridCheck.Checked)
            {
                Pen clPen = new Pen(ClsSystem.mSetting.mMainColorGrid);

                //以下、垂直ライン描画処理
                float flStartX = this.mPreViewCenter.X + this.panel_PreView.Width / 2;
                while (flStartX >= 0) flStartX -= flGrid;

                for (float flCnt = flStartX; flCnt < this.panel_PreView.Width; flCnt += (flGrid))
                {
                    e.Graphics.DrawLine(clPen, flCnt, 0.0f, flCnt, this.panel_PreView.Height);
                }

                //以下、水平ライン描画処理
                float flStartY = this.mPreViewCenter.Y + this.panel_PreView.Height / 2;
                while (flStartY >= 0) flStartY -= flGrid;

                for (float flCnt = flStartY; flCnt < this.panel_PreView.Height; flCnt += (flGrid))
                {
                    e.Graphics.DrawLine(clPen, 0.0f, flCnt, this.panel_PreView.Width, flCnt);
                }
            }

            // モーション描画処理
            // DrawItems
            Matrix back = e.Graphics.Transform;
            if (ClsSystem.mMotionSelectKey >= 0)
            {
                this.DrawPreview(e.Graphics);
            }
            e.Graphics.Transform = back;

            //CrossBar スクリーン移動時は原点に沿う形に
            if(checkBox_CrossBar.Checked)
            {
                Pen clPen = new Pen(ClsSystem.mSetting.mMainColorCenterLine);
                float flX = this.mPreViewCenter.X + this.panel_PreView.Width / 2;
                float flY = this.mPreViewCenter.Y + this.panel_PreView.Height / 2;
                e.Graphics.DrawLine(clPen, flX, 0, flX, this.panel_PreView.Height);  //垂直ライン
                e.Graphics.DrawLine(clPen, 0, flY, this.panel_PreView.Width, flY);   //水平ライン
            }
        }

        /// <summary>
        /// モーション描画処理
        /// </summary>
        /// <param name="g"></param>
        private void DrawPreview(Graphics g)
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
                clMotion.DrawPreview(g, vcx, vcy);
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
                    string ext = Path.GetExtension(str).ToLower();
                    if (ext == ".png")
                    {
                        int inKey = ClsSystem.CreateImageFromFile(str);
                        if (inKey >= 0)
                        {
                            //以下、選択中のモーションがあるかチェック
                            if (ClsSystem.mMotionSelectKey >= 0)
                            {
                                //以下、辞書に選択中のモーションが存在するかチェック
                                bool isExist = ClsSystem.mDicMotion.ContainsKey(ClsSystem.mMotionSelectKey);
                                if (isExist)
                                {
                                    //以下、エレメント追加
                                    ClsDatMotion clDatMotion = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey];
                                    ClsDatImage clDatImage = ClsSystem.mDicImage[inKey];
                                    this.AddElement(clDatMotion, clDatImage, sPos.X, sPos.Y);
                                }
                            }
                        }

                        //CellListの表示更新
                        this.mFormCell.Refresh();
                    }
                }
                e.Effect = DragDropEffects.Copy;
            }

            /*
            //ListViewItem受け入れ
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem lvi = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                //ImageChipの登録
                ClsDatImage work;
                
                //work.Img = (Bitmap)lvi.ImageList.Images[lvi.ImageIndex];
                string md5 = (string)lvi.Tag;//ListViewのTagからMD5を取得
                work =  ClsSystem.ImageMan.GetImageChipFromMD5(md5);//取得したMD5から特定
                if (work != null)
                {
                    //ClsSystem.ImageMan.AddImageChip(work);
                    listView_Motion_AddElements(work, sPos.X, sPos.Y);
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    System.Console.WriteLine("ListViewからのドロップ時MD5不詳");
                }
            }
            */
            //※一旦コメントアウト

            //ImageChip 受け入れ
            if (e.Data.GetDataPresent(typeof(ClsDatImage)))
            {
                //以下、選択中のモーションがあるかチェック
                if (ClsSystem.mMotionSelectKey >= 0)
                {
                    //以下、辞書に選択中のモーションが存在するかチェック
                    bool isExist = ClsSystem.mDicMotion.ContainsKey(ClsSystem.mMotionSelectKey);
                    if (isExist)
                    {
                        //以下、エレメント追加
                        ClsDatMotion clDatMotion = ClsSystem.mDicMotion[ClsSystem.mMotionSelectKey];
                        ClsDatImage clDatImage = (ClsDatImage)e.Data.GetData(typeof(ClsDatImage));
                        Point a = panel_PreView.PointToClient(new Point(e.X, e.Y));
                        this.AddElement(clDatMotion, clDatImage, a.X, a.Y);
                    }
                }

                e.Effect = DragDropEffects.Copy;
            }

            /*
            if (e.Data.GetType() == typeof(ELEMENTS))
            {
                e.Effect = DragDropEffects.Copy;
            }
            */
            //※ここがよく分からない

            if (e.Data.GetType() == typeof(Image))
            {
                e.Effect = DragDropEffects.Copy;
            }

            panel_PreView.Refresh();
        }
        private void PanelPreView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            //Drop受け入れ準備
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            if (e.Data.GetDataPresent(typeof(ClsDatImage)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }

        }
        private void PanelPreView_MouseWheel(object sender, MouseEventArgs e)
        {
            mWheelDelta = (e.Delta > 0) ? +1 : -1;//+/-に適正化

            //画面の拡大縮小
            if (mWheelDelta > 0)
            {
                if (HScrollBar_ZoomLevel.Value < HScrollBar_ZoomLevel.Maximum) HScrollBar_ZoomLevel.Value += mWheelDelta;
            }
            else
            {
                if (HScrollBar_ZoomLevel.Value > HScrollBar_ZoomLevel.Minimum) HScrollBar_ZoomLevel.Value += mWheelDelta;
            }

            mWheelDelta = 0;

            

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

            StatusLabel2.Text = $"{mWheelDelta}";

            this.panel_PreView.Refresh();
        }

        private void PanelPreView_MouseDown(object sender, MouseEventArgs e)
        {
            //e.X,Yからステージ上の座標にする
            float zoom = HScrollBar_ZoomLevel.Value / mParZOOM;
            float stPosX = ((e.X -(panel_PreView.Width  / 2)) / zoom);
            float stPosY = ((e.Y -(panel_PreView.Height / 2)) / zoom);

            this.mMouseDownL = false;

            if (e.Button == MouseButtons.Left)
            {
                this.mMouseDownL = true;
                this.mMouseOldPoint = new Point(e.X, e.Y);

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

            //アイテム選択が無い場合のLドラッグはステージのXYスクロール
            if (this.mMouseDownL)
            {
                this.mPreViewCenter.X += e.X - this.mMouseOldPoint.X;
                this.mPreViewCenter.Y += e.Y - this.mMouseOldPoint.Y;
                this.mMouseOldPoint.X = e.X;
                this.mMouseOldPoint.Y = e.Y;
            }

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

            this.panel_PreView.Refresh();
        }

        private void PanelPreView_MouseUp(object sender, MouseEventArgs e)
        {
            //releaseMouse
            //mMouseLDown = false;
            //mMouseMDown = false;
            //mMouseRDown = false;
            //mDragState = DragState.none;

            if (e.Button == MouseButtons.Left)
            {
                this.mMouseDownL = false;
                this.mMouseOldPoint = Point.Empty;
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
            
        }

        /// <summary>
        /// デバッグ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_DebugGraph_Click(object sender, EventArgs e)
        {
            FormRateGraph clFormRateGraph = new FormRateGraph(this, ClsDatTween.EnmParam.POSITION_X, 10, 20, 15);
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

        private void ToolStripMenuItem_ExpCellList_Click(object sender, EventArgs e)
        {
            /*
            //ExportImageList(XML imageList)
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".ximg";
            if(sfd.ShowDialog() == DialogResult.OK)
            { 
                ClsSystem.ImageMan.SaveToFile(sfd.FileName,".ximg");
            }
            */
            //※一旦コメントアウト
        }

        private void partsFormInMainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mFormCell.TopLevel)
            {
                //in
                mFormCell.TopLevel = false;
                mFormCell.Location = new Point(0, 0);
                mFormCell.FormBorderStyle = FormBorderStyle.None;
                mFormCell.Visible = true;
                mFormCell.Dock = DockStyle.Fill;                
                panel_chip.Controls.Add(mFormCell);
            }
            else
            {
                //out
                panel_chip.Controls.Remove(mFormCell);
                mFormCell.Location = this.Location;
                mFormCell.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                mFormCell.Dock = DockStyle.None;
                mFormCell.TopLevel = true;                
            }
        }

        private void numericUpDown_Grid_ValueChanged(object sender, EventArgs e)
        {
            this.panel_PreView.Refresh();
        }

        private void ToolStripMenuItem_DebugExport_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> clDicFile = new Dictionary<string, object>();

            //以下、ファイル情報出力処理
            clDicFile["ver"] = "0.0.1";
            clDicFile["hogehoge"] = 99;

            /*
            //以下、イメージ出力処理
            int inCnt, inMax = ClsSystem.ImageMan.ChipCount();
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ImageChip clCell = ClsSystem.ImageMan.GetImageChipFromIndex(inCnt);
                string clKey = clCell.ID.ToString();
                clDicFile[clKey] = clCell.Export();
            }
            */

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
            string clJsonData = ClsTool.DictionaryToJson(clDicFile);

            //以下、ファイル出力処理
            string clPath = ClsPath.GetPath();
            string clPathFile = Path.Combine(clPath, "よしさんデバッグ用ファイル.txt");
            File.WriteAllText(clPathFile, clJsonData);
        }
    }
}
