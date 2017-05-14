using System;
using System.Collections.Generic;
using System.Reflection;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        //以下、各種コンポーネント
        public FormControl mFormControl;
        public FormAttribute mFormAttribute;
        public FormCell mFormCell;

        //以下、OpenGLコンポーネントに引き継ぐための一時保持領域
        private Point mPosMouseLOld = Point.Empty;
        private Point mPosMouseROld = Point.Empty;
        private float[] mListScale = new float[8] { 0.125f, 0.25f, 0.5f, 1.0f, 2.0f, 4.0f, 8.0f, 16.0f };   //スケールリスト
        private float mCenterX = 0.0f;  //中心Ｘ座標
        private float mCenterY = 0.0f;  //中心Ｙ座標

        //private Point mMouseDownShift;
        private bool mMouseDownL = false;//L
        private bool mMouseDownR = false;//R
        //private bool mMouseMDown = false;//M
        private Keys mKeys,mKeysSP;//キー情報 通常キー,スペシャルキー

        //編集中の選択中エレメントのインデックス 非選択=null
        //これはTimeLine内のFrameあたりに移動させたいなぁ 11/11移動
        //private int? mNowElementsIndex = null;
        
        //private string mNowMotionName;//選択中モーション名

        enum DragState { none,Move, Angle, Scale,Scroll, Joint };
        //private DragState mDragState = DragState.none;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            //以下、初期化処理
//            panel_PreView.GetType().InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, panel_PreView, new object[] { true });
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //以下、システム初期化処理
            ClsSystem.Init();

            //以下、メインウィンドウの座標の設定
            this.Location = ClsSystem.mSetting.mWindowMain.mLocation;
            this.Size = ClsSystem.mSetting.mWindowMain.mSize;

            //以下、TreeNode作成処理
            //初期モーションTreeの追加
            ClsDatMotion clMotion = this.AddMotion("DefMotion");

            //以下、コンポーネント初期化処理
            this.componentOpenGL.MouseWheel += new MouseEventHandler(this.componentOpenGL_MouseWheel);
            this.checkBox_GridCheck.Checked = ClsSystem.mSetting.mWindowMain_DrawGird;
            this.checkBox_CrossBar.Checked = ClsSystem.mSetting.mWindowMain_DrawCross;
            this.checkBox_CellList.Checked = ClsSystem.mSetting.mWindowMain_CellList;
            this.checkBox_Attribute.Checked = ClsSystem.mSetting.mWindowMain_Attribute;
            this.checkBox_Control.Checked = ClsSystem.mSetting.mWindowMain_Control;
            this.checkBox_Snap.Checked = ClsSystem.mSetting.mWindowMain_GridSnap;
            this.numericUpDown_Grid.Value = ClsSystem.mSetting.mWindowMain_WidthGrid;
            this.comboBox_Zoom.SelectedIndex = 3;

            //以下、コントロール初期化処理
            //ClsView.Init(this.panel_PreView);

            this.mFormControl = new FormControl(this);
            this.mFormControl.SetName(clMotion);
            this.mFormControl.Show();

            this.mFormAttribute = new FormAttribute(this);
            this.mFormAttribute.Show();

            //以下、初期化処理

            //Ver2
            this.mFormCell = new FormCell(this);
            //mFormCell.TopLevel = false;
            //mFormCell.FormBorderStyle = FormBorderStyle.None;
            //mFormCell.Visible = true;
            //mFormCell.Dock = DockStyle.Fill;
            this.mFormCell.Show();
            //Panel_Chip.Controls.Add(mFormCell);

            //Motion選択状態にする 他フォームの準備完了後
            this.listView_Motion.Items[0].Selected = true;
            int inKey = this.listView_Motion.Items[0].GetHashCode();
            ClsSystem.SetSelectMotionKey(inKey);    //選択中変更

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
                int inKey = listView_Motion.Items[0].GetHashCode();
                ClsSystem.SetSelectMotionKey(inKey);    //選択中変更

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
                ClsSystem.SetSelectMotionDefault();
                ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
                clMotion.SetSelectFromLineNo(-1);

                //以下、各種コントロール設定処理
                //以下、ウィンドウ名を修正する処理
                this.SetName(clMotion);
                this.mFormControl.SetName(clMotion);
                this.mFormAttribute.Init(null, 0, ClsSystem.DEFAULT_FRAME_NUM);

                //以下、各種ウィンドウ更新処理
                this.RefreshViewer(sender, e);
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
            // AllForm Alingment

            //フォームをメイン基準で並べる
            //アトリビュート
            this.mFormAttribute.Location = new Point(Location.X + Width, Location.Y);
            this.mFormControl.Location   = new Point(Location.X, Location.Y + Height);
            this.mFormCell.Location      = new Point(Location.X - this.mFormCell.Width, Location.Y);
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
                    ClsSystem.RemoveMotion(inHash);    //モーションクラス削除処理

                    //編集中のモーションが削除されたので、
                    //コントロールウィンドウとメインウィンドウの情報をクリアする

                    ClsSystem.SetSelectMotionKey(-1);

                    //以下、コントロール設定処理
                    this.SetName(null);
                    this.mFormControl.SetName(null);
                    this.mFormAttribute.Init(null, 0, ClsSystem.DEFAULT_FRAME_NUM);

                    this.RefreshViewer(sender, e);
                }
            }
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

        private void RefreshViewer(object sender, EventArgs e)
        {
            int inIndex = this.comboBox_Zoom.SelectedIndex;
            if (inIndex < 0 || inIndex >= 8) inIndex = 3;

            this.componentOpenGL.mCenterX = this.mCenterX;
            this.componentOpenGL.mCenterY = this.mCenterY;
            this.componentOpenGL.mScale = this.mListScale[inIndex];
            this.componentOpenGL.mCrossBarVisible = this.checkBox_CrossBar.Checked;
            this.componentOpenGL.mCrossColor = ClsSystem.mSetting.mMainColorCenterLine;
            this.componentOpenGL.mGridVisible = this.checkBox_GridCheck.Checked;
            this.componentOpenGL.mGridColor = ClsSystem.mSetting.mMainColorGrid;
            this.componentOpenGL.mGridSpan = (int)this.numericUpDown_Grid.Value;
            this.componentOpenGL.mCanvasWidth = this.componentOpenGL.Width;
            this.componentOpenGL.mCanvasHeight = this.componentOpenGL.Height;
            this.componentOpenGL.Refresh();

            this.StatusLabel2.Text = "zoom=" + this.componentOpenGL.mScale;
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

        //TreeView_Project
        private void treeView_Project_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //ReName MotionName
            if (e.Node.ImageIndex == 2)
            {
                //this.mNowMotionName = e.Label;

                int inHashCode = e.Node.GetHashCode();
                ClsDatMotion clMotion = ClsSystem.GetMotion(inHashCode);
                if (clMotion != null)
                {
                    clMotion.SetName(e.Label);
                }

                //以下、各コントロールの設定
                this.SetName(clMotion);
                this.mFormControl.SetName(clMotion);
                this.mFormAttribute.Init(null, 0, ClsSystem.DEFAULT_FRAME_NUM);
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
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
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
        /// Elementを作成し追加する
        /// </summary>
        /// <param name="clDatMotion">モーション管理クラス</param>
        /// <param name="clDatImage">イメージ管理クラス</param>
        /// <param name="inX">クリック座標(Client)</param>
        /// <param name="inY">クリック座標(Client)</param>
        private void AddElement(ClsDatMotion clDatMotion, ClsDatImage clDatImage, int inX, int inY)
        {
            //以下、エレメント追加処理
            ClsDatElem clDatElem = new ClsDatElem(clDatMotion, null, inX, inY);
            clDatElem.SetImage(clDatImage);

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
//          clListViewItem.Tag = ClsSystem.mDicMotion.Count;

            int inKey = clListViewItem.GetHashCode();
            ClsDatMotion clMotion = new ClsDatMotion(inKey, clMotionName);
            ClsSystem.AddMotion(inKey, clMotion);

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
                ClsDatMotion clMotion = ClsSystem.GetMotion(inHashCode);
                if (clMotion != null)
                {
                    clMotion.SetName(e.Label);
                }
                listView_Motion.Items[e.Item].Text = e.Label;

                //以下、各コントロールの設定
                this.SetName(clMotion);
                this.mFormControl.SetName(clMotion);
                this.mFormAttribute.Init(null, 0, ClsSystem.DEFAULT_FRAME_NUM);
            }
        }
        private void listView_Motion_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //e==選択されたlistViewItem のはず
            int inHashCode = e.Item.GetHashCode();

            //モーション存在確認
            ClsDatMotion clMotion = ClsSystem.GetMotion(inHashCode);
            if (clMotion!= null)
            {
                //アイテムが存在
                ClsSystem.SetSelectMotionKey(inHashCode);   //選択中変更
                clMotion.SetSelectFromLineNo(-1);

                //以下、各種コントロール設定処理
                //以下、ウィンドウ名を修正する処理
                this.SetName(clMotion);
                this.mFormControl.SetName(clMotion);
                this.mFormAttribute.Init(null, 0, ClsSystem.DEFAULT_FRAME_NUM);

                //新しく選択したモーションをメインウィンドウに表示する
                //新しく選択したモーションをコントロールウィンドウに表示する
            }
            else
            {
                //非選択
                ClsSystem.SetSelectMotionKey(-1);

                //以下、コントロール設定処理
                this.SetName(null);
                this.mFormControl.SetName(null);
                this.mFormAttribute.Init(null, 0, ClsSystem.DEFAULT_FRAME_NUM);
            }

            this.RefreshViewer(sender, e);
        }

        /*
        //PanelPreView周り
        private void PanelPreView_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.Clear(ClsSystem.mSetting.mMainColorBack);

                //以下、拡大してボケないようにする
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

                //以下、半ドットずらして表示する
                e.Graphics.PixelOffsetMode   = PixelOffsetMode.Half;

                //画像で背景fill
                //e.Graphics.FillRectangle(new TextureBrush(Properties.Resources.Blank),new Rectangle(0,0,PanelPreView.Width,PanelPreView.Height));

                //以下、グリッド表示処理
                if (this.checkBox_GridCheck.Checked)
                {
                    float flGrid = ClsView.mScale * (float)numericUpDown_Grid.Value;
                    Pen clPen = new Pen(ClsSystem.mSetting.mMainColorGrid);

                    //以下、垂直ライン描画処理
                    float flStartX = ClsView.WorldPosX2CameraPosX(0);
                    while (flStartX >= 0) flStartX -= flGrid;

                    for (float flCnt = flStartX; flCnt < this.userControlOpenGL.Width; flCnt += (flGrid))
                    {
                        e.Graphics.DrawLine(clPen, flCnt, 0.0f, flCnt, this.userControlOpenGL.Height);
                    }

                    //以下、水平ライン描画処理
                    float flStartY = ClsView.WorldPosY2CameraPosY(0);
                    while (flStartY >= 0) flStartY -= flGrid;

                    for (float flCnt = flStartY; flCnt < this.userControlOpenGL.Height; flCnt += (flGrid))
                    {
                        e.Graphics.DrawLine(clPen, 0.0f, flCnt, this.userControlOpenGL.Width, flCnt);
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
                if (checkBox_CrossBar.Checked)
                {
                    Pen clPen = new Pen(ClsSystem.mSetting.mMainColorCenterLine);
                    float flX = ClsView.WorldPosX2CameraPosX(0);
                    float flY = ClsView.WorldPosY2CameraPosY(0);
                    e.Graphics.DrawLine(clPen, flX, 0, flX, this.userControlOpenGL.Height);  //垂直ライン
                    e.Graphics.DrawLine(clPen, 0, flY, this.userControlOpenGL.Width, flY);   //水平ライン
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
        */

        private void componentOpenGL_DragDrop(object sender, DragEventArgs e)
        {
//            Point sPos = panel_PreView.PointToClient(new Point(e.X, e.Y));
            Point sPos = new Point(0, 0);

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
                            ClsDatMotion clDatMotion = ClsSystem.GetSelectMotion();
                            if (clDatMotion != null)    //選択中のモーション存在チェック
                            {
                                ClsDatImage clDatImage = ClsSystem.GetImage(inKey);
                                if (clDatImage != null) //イメージ存在チェック
                                {
                                    //以下、エレメント追加
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
                //以下、エレメント追加
                ClsDatMotion clDatMotion = ClsSystem.GetSelectMotion();
                if (clDatMotion != null)
                {
                    ClsDatImage clDatImage = (ClsDatImage)e.Data.GetData(typeof(ClsDatImage));

//                  Point a = panel_PreView.PointToClient(new Point(e.X, e.Y));
                    Point a = new Point(0, 0);
                    this.AddElement(clDatMotion, clDatImage, a.X, a.Y);
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

            this.RefreshViewer(sender, e);
        }

        private void componentOpenGL_DragEnter(object sender, DragEventArgs e)
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

        private void componentOpenGL_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mMouseDownL = true;
                this.mPosMouseLOld = new Point(e.X, e.Y);
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.mMouseDownR = true;
                this.mPosMouseROld = new Point(e.X, e.Y);
            }
        }

        private void componentOpenGL_MouseMove(object sender, MouseEventArgs e)
        {
            bool isRefresh = false;

            if (this.mMouseDownL)
            {
                this.mPosMouseLOld.X = e.X;
                this.mPosMouseLOld.Y = e.Y;

                isRefresh = true;
            }

            //以下、スクリーン座標移動処理
            if (this.mMouseDownR)
            {
                this.mCenterX += e.X - this.mPosMouseROld.X;
                this.mCenterY -= e.Y - this.mPosMouseROld.Y;

                this.mPosMouseROld.X = e.X;
                this.mPosMouseROld.Y = e.Y;

                isRefresh = true;
            }

            if (isRefresh)
            {
                this.RefreshViewer(sender, e);
            }
        }

        private void componentOpenGL_MouseUp(object sender, MouseEventArgs e)
        {
            //releaseMouse
            //mMouseLDown = false;
            //mMouseMDown = false;
            //mMouseRDown = false;
            //mDragState = DragState.none;

            if (e.Button == MouseButtons.Left)
            {
                this.mMouseDownL = false;
                this.mPosMouseLOld = Point.Empty;
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.mMouseDownR = false;
                this.mPosMouseROld = Point.Empty;
            }
        }

        private void componentOpenGL_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta > 0)
                {
                    //以下、画面拡大処理
                    if (this.comboBox_Zoom.SelectedIndex < 7)
                    {
                        /*
                        int inIndex = this.comboBox_Zoom.SelectedIndex;
                        float flScale = this.mListScale[inIndex];
                        float flMouseX = this.MousePosX2WorldPosX(e.X);
                        float flMouseY = this.MousePosY2WorldPosY(e.Y);
                        this.mCenterX -= (int)(flMouseX * flScale);
                        this.mCenterY -= (int)(flMouseY * flScale);
                        */

                        this.comboBox_Zoom.SelectedIndex += 1;

                        this.toolStripStatusLabel_DebugSize.Text = "x=" + e.X + " y=" + e.Y;
                    }
                }
                else if (e.Delta < 0)
                {
                    //以下、画面縮小処理
                    if (this.comboBox_Zoom.SelectedIndex > 0)
                    {
                        /*
                        int inIndex = this.comboBox_Zoom.SelectedIndex;
                        float flScale = this.mListScale[inIndex];
                        float flMouseX = this.MousePosX2WorldPosX(e.X);
                        float flMouseY = this.MousePosY2WorldPosY(e.Y);
                        this.mCenterX += (int)(flMouseX * flScale);
                        this.mCenterY += (int)(flMouseY * flScale);
                        */

                        this.comboBox_Zoom.SelectedIndex -= 1;

                        this.toolStripStatusLabel_DebugSize.Text = "x=" + e.X + " y=" + e.Y;
                    }
                }

                this.RefreshViewer(sender, e);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        public float MousePosX2WorldPosX(float flX)
        {
            return (flX);
        }

        public float MousePosY2WorldPosY(float flY)
        {
            return (flY);
        }

        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        */

        /// <summary>
        /// エディット中の選択エレメントをインデックス指定と関連画面更新
        /// </summary>
        /// <param name="ElementsIndex"></param>
        public void SetNowElementsIndex(int? ElementsIndex)
        {
            //現在の選択と違う物であれば変更を行う
            int inSelectMotionKey = ClsSystem.GetSelectMotionKey();
            if (inSelectMotionKey <= 0) return;

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

            this.RefreshViewer(sender, e);
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

        private void ToolStripMenuItem_DebugOpenGL_Click(object sender, EventArgs e)
        {
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
