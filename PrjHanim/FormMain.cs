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
        
        private string mNowMotionName;//選択中モーション名

        enum DragState { none,Move, Angle, Scale,Scroll, Joint }; 
        private DragState mDragState = DragState.none;

        private Point PreViewCenter;//PanelPreView Centerセンターポジション
        
        private ImageManagerBase ImageMan;
        public TIMELINEbase TimeLine;   //←これをList<Motion>に修正する事になる？

        public FormMain()
        {
            InitializeComponent();
            //panel_PreView.DoubleBuuferd = true;
            panel_PreView.GetType().InvokeMember("DoubleBuffered",BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,null,panel_PreView,new object[] { true });
        }

        public void AttributeUpdate()
        {
            //mFormAttributeのパラメータ変更時に呼び出される
            //パラメータ取得処理
            //エディット中アイテムにパラメータ再取得
            ELEMENTS e = TimeLine.EditFrame.GetActiveElements();
            if(e!=null)
            {
                mFormAttribute.GetAllParam(ref e.Atr);
                panel_PreView.Refresh();
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //以下、システム初期化処理
            ClsSystem.Init();

            //以下、初期化処理
            PreViewCenter = new Point(0, 0);
            mScreenScroll = new Point(0, 0);

            ImageMan = new ImageManagerBase();
            TimeLine = new TIMELINEbase();

            this.mFormImageList = new FormImageList(this);
            this.mFormImageList.Show();

            this.mFormControl = new FormControl(this);
            this.mFormControl.mTimeLine = TimeLine;
            this.mFormControl.Show();

            this.mFormAttribute = new FormAttribute(this);
            this.mFormAttribute.Show();

            mFormControl.mTimeLine = TimeLine;//ControlFormに通達
            //Ver2
            mFormCell = new FormCell(this);
            //mFormCell.Owner = this;
            mFormCell.ImageMan = ImageMan;
            mFormCell.Show();

            AlingForms();

            //背景の再描画をキャンセル(ちらつき抑制)
            //効果いまいち
            this.SetStyle(ControlStyles.Opaque, true);

            //以下、各フォーム表示・非表示処理
            if (mFormImageList != null) mFormImageList.Visible = checkBox_ImageList.Checked;
            if (mFormControl != null) mFormControl.Visible = checkBox_Control.Checked;
            if (mFormAttribute != null) mFormAttribute.Visible = checkBox_Attribute.Checked;
            if (mFormCell != null) mFormCell.Visible = checkBox_CellList.Checked;
    }
        private void LoadProject_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".hap";
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                TimeLine.LoadFromFile(ofd.FileName);
            }
            ofd.Dispose();
        }
        private void SaveProject_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".hap";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                TimeLine.SaveToFile(sfd.FileName);
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
            mFormAttribute.Location = new Point(Location.X + Width, Location.Y);
            mFormControl.Location   = new Point(Location.X, Location.Y + Height);
            mFormImageList.Location = new Point(Location.X - 50, Location.Y);
            mFormCell.Location      = new Point(Location.X - 50, Location.Y + Height);
        }
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            mKeys = e.KeyData;
            mKeysSP = e.Modifiers;
        }
        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            mKeys = Keys.None;
            mKeysSP = Keys.None;

            if (e.KeyData == Keys.Delete)
            {
                //Element Remove
                TimeLine.EditFrame.Remove((int)TimeLine.EditFrame.ActiveIndex);
                panel_PreView.Refresh();
                treeView_Project_Update();
            }
        }
        private void FormMain_Resize(object sender, EventArgs e)
        {
            panel_PreView.Refresh();
        }
        /// 終了処理
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default["Location_FormMain"]    = this.Location;
            //            Properties.Settings.Default["Location_FormAttribute"] = value;
            //            Properties.Settings.Default["Location_FormControl"] = value;
            //            Properties.Settings.Default["Location_FormImageCut"] = value;
            //            Properties.Settings.Default["Location_FormImageList"] = value;
            Properties.Settings.Default["BackColor_ColorBack"]  = this.button_BackColor.BackColor;
            Properties.Settings.Default["BackColor_ColorGrid"]  = this.button_GridColor.BackColor;
            Properties.Settings.Default["BackColor_ColorCross"] = this.button_CrossColor.BackColor;
            Properties.Settings.Default["Checked_DrawGird"]     = this.checkBox_GridCheck.Checked;
            Properties.Settings.Default["Checked_DrawCross"]    = this.checkBox_CrossBar.Checked;
            Properties.Settings.Default["Value_WidthGrid"]      = this.numericUpDown_Grid.Value;
            Properties.Settings.Default["Checked_GridSnap"]     = this.checkBox_Snap.Checked;
            Properties.Settings.Default["Checked_ImageList"]    = this.checkBox_ImageList.Checked;
            Properties.Settings.Default["Checked_Control"]      = this.checkBox_Control.Checked;
            Properties.Settings.Default["Checked_Attribute"]    = this.checkBox_Attribute.Checked;
            Properties.Settings.Default.Save();//<-基本的にはバインドされたものはここで自動セーブ
        }
        private void button_BackColor_Click(object sender, EventArgs e)
        {
            ColorDialog cdg = new ColorDialog();
            if (cdg.ShowDialog() == DialogResult.OK)
            {
                Button b = (Button)sender;
                b.BackColor = cdg.Color;
                panel_PreView.BackColor = cdg.Color;
            }
            cdg.Dispose();
            panel_PreView.Refresh();
        }
        private void Button_Color_Click(object sender, EventArgs e)
        {
            ColorDialog cdg = new ColorDialog();
            if (cdg.ShowDialog() == DialogResult.OK)
            {
                Button b = (Button)sender;
                b.BackColor = cdg.Color;
            }
            cdg.Dispose();
            panel_PreView.Refresh();
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
                mNowMotionName = e.Label;
            }

            //ReName ElementsName
            if (e.Node.ImageIndex == 4)
            {
                //e.Label:新Text e.node.TExt:旧Text
                TimeLine.EditFrame.RenameElements(e.Node.Tag, e.Label);
                mFormControl.Refresh();
            }
            
        }
        private void treeView_Project_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //ノードがエレメントかどうか確認する
            //nodeのimageindexから判別する？
            //Motionノードクリックされたらモーション切り替えを行う(将来)
            //Select Motion
            if (e.Node.ImageIndex == 2)
            {
            }
            //SelectElements
            //TagとElements.Nameが合致するものを選択
            TimeLine.EditFrame.SelectElement(e.Node.Tag);
            if (TimeLine.EditFrame.ActiveIndex != null) panel_PreView.Refresh();
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
            for(int cnt=0;cnt<TimeLine.EditFrame.ElementsCount;cnt++)
            {
                ELEMENTS elm = TimeLine.EditFrame.GetElement(cnt);
                TreeNode tn = treeView_Project.Nodes["Motion"].Nodes[elm.Name];
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
        /// <summary>
        /// CellからElementを作成し追加
        /// </summary>
        /// <param name="work"></param>
        /// <param name="x">クリック座標(Cliant)</param>
        /// <param name="y">クリック座標(Cliant)</param>
        private void treeView_Project_AddElements(CELL work, int x, int y)
        {
            //アイテムの登録
            ELEMENTS elem = new ELEMENTS();
            elem.Atr = new AttributeBase();
            elem.Atr.CellID = work.GetHashCode();
            elem.Atr.Width = work.Img.Width;
            elem.Atr.Height = work.Img.Height;
            elem.Tag = elem.GetHashCode();

            //センターからの距離に変換
            x -= panel_PreView.Width / 2;
            y -= panel_PreView.Height / 2;
            //さらに画像サイズ半分シフトして画像中心をセンターに
            x -= elem.Atr.Width / 2;
            y -= elem.Atr.Height / 2;

            elem.Atr.Position = new Vector3(x, y, 0);
            elem.Name = elem.GetHashCode().ToString("X8");//仮名

            //Show - Attribute
            mFormAttribute.SetAllParam(elem.Atr);

            TimeLine.EditFrame.AddElements(elem);//Elements登録
            TimeLine.Store();//
            // "Motion"固定決め打ちしてるのはあとでモーション名管理変数に置き換え

            //TreeNode selNode = treeView_Project.Nodes[mNowMotionName];
            TreeNode selNode = treeView_Project.Nodes["Motion"];
            selNode.Nodes.Add(elem.Name, elem.Name);
            selNode.Expand();
            selNode.Nodes[elem.Name].Tag = elem.GetHashCode();
            selNode.Nodes[elem.Name].ImageIndex = 4;
            selNode.Nodes[elem.Name].SelectedImageIndex = 3;

            //Control更新
            mFormControl.Refresh();

        }
        private void treeView_Project_RemoveElements(string name)
        {
            //Elements選択中のDelキー
        }
        private void treeView_Project_AddMotion(string name)
        {
            treeView_Project.SelectedNode = treeView_Project.TopNode;
            TreeNode tn = treeView_Project.Nodes.Add(name);
            tn.ImageIndex = 2;
            tn.SelectedImageIndex = 2;
            tn.Tag = 1;//仮番号

        }
        private void treeView_Project_RemoveMotion(string name)
        { }
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
                    

                    if (TimeLine.EditFrame.Move(src.Name, dest.Name,true) == false) { Console.WriteLine("Elements move False"); };
                                      
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
        private void button1_MotionAdd_Click(object sender, EventArgs e)
        {
            treeView_Project_AddMotion("NewMotion");
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
                var p1 = new Pen(button_GridColor.BackColor);
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
            if (TimeLine.EditFrame != null)
            {
                PanelPreview_Paint_DrawParts(sender, e.Graphics);
            }
            e.Graphics.Transform = back;
            //CrossBar スクリーン移動時は原点に沿う形に
            if(checkBox_CrossBar.Checked)
            {
                var p1 = new Pen(button_CrossColor.BackColor);
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

            FRAME frm = TimeLine.EditFrame;

            for (int cnt = 0; cnt < frm.ElementsCount; cnt++)
            {
                ELEMENTS e = frm.GetElement(cnt);
                AttributeBase atr = e.Atr;

                Matrix Back = g.Transform;
                Matrix MatObj = new Matrix();//今はgのMatrixを使っているので未使用

                //以後 将来親子関係が付く場合は親をあわせた処理にする事となる

                //スケールにあわせた部品の大きさを算出
                float vsx = atr.Width * atr.Scale.X;// * zoom;//SizeX 画面ズームは1段手前でmatrixで行っている
                float vsy = atr.Height * atr.Scale.Y;// * zoom;//SizeY

                //パーツの中心点
                float pcx = atr.Position.X + atr.Offset.X;
                float pcy = atr.Position.X + atr.Offset.X;
                Color Col = Color.FromArgb(atr.Color);

                //カラーマトリックス作成
                System.Drawing.Imaging.ColorMatrix colmat = new System.Drawing.Imaging.ColorMatrix();
                if (atr.isColor)
                {
                    colmat.Matrix00 = (float)(Col.R * (atr.ColorRate / 100f));//Red  Col.R * Col.Rate
                    colmat.Matrix11 = (float)(Col.G * (atr.ColorRate / 100f));//Green
                    colmat.Matrix22 = (float)(Col.B * (atr.ColorRate / 100f));//Blue
                }
                else
                {
                    colmat.Matrix00 = 1;//Red
                    colmat.Matrix11 = 1;//Green
                    colmat.Matrix22 = 1;//Blue
                }
                if (atr.isTransparrency)
                {
                    colmat.Matrix33 = (atr.Transparency / 100f);
                }
                else
                {
                    colmat.Matrix33 = 1;
                }
                colmat.Matrix44 = 1;//w
                System.Drawing.Imaging.ImageAttributes ia = new System.Drawing.Imaging.ImageAttributes();
                ia.SetColorMatrix(colmat);

                //Cell画像存在確認 画像の無いサポート部品の場合もありえるかも
                CELL c = ImageMan.GetCellFromHash(atr.CellID);
                if (c == null) { Console.WriteLine("Image:null"); return; }

                //原点を部品中心に
                //g.TranslateTransform(   vcx + (atr.Position.X + atr.Width/2)  * atr.Scale.X *zoom,
                //                        vcy + (atr.Position.Y + atr.Height/2) * atr.Scale.Y *zoom);//部品中心座標か？

                //中心に平行移動
                g.TranslateTransform(vcx + atr.Position.X + atr.Offset.X, vcy + atr.Position.Y + atr.Offset.Y);
                //回転角指定
                g.RotateTransform(atr.Radius.Z);

                //スケーリング調
                g.ScaleTransform(atr.Scale.X, atr.Scale.X);
                //g.TranslateTransform(vcx + (atr.Position.X * atr.Scale.X), vcy + (atr.Position.Y * atr.Scale.Y));

                //MatObj.Translate(-(vcx + atr.Position.X +(atr.Width /2))*atr.Scale.X,-(vcy + atr.Position.Y +(atr.Height/2))*atr.Scale.Y,MatrixOrder.Append);
                //MatObj.Translate(0, 0);
                //MatObj.Scale(atr.Scale.X,atr.Scale.Y,MatrixOrder.Append);
                //MatObj.Rotate(atr.Radius.X,MatrixOrder.Append);
                //MatObj.Translate((vcx + atr.Position.X + (atr.Width / 2)) * atr.Scale.Y, (vcy + atr.Position.Y + (atr.Height / 2)) * atr.Scale.Y,MatrixOrder.Append);

                //g.TranslateTransform(vcx, vcy);
                //描画
                /*
                g.DrawImage(c.Img,
                    -(atr.Width  * atr.Scale.X * zoom )/2,
                    -(atr.Height * atr.Scale.Y * zoom )/2,
                    vsx,vsy);*/
                //g.DrawImage(c.Img,vcx+ (now.Position.X*zoom)-(vsx/2),vcy+ (now.Position.Y*zoom)-(vsy/2),vsx,vsy);
                //g.Transform = MatObj;

                //Draw
                if (atr.isTransparrency || atr.isColor)
                {
                    g.DrawImage(c.Img, new Rectangle((int)(atr.Offset.X - (atr.Width * atr.Scale.X) / 2), (int)(atr.Offset.Y - (atr.Height * atr.Scale.Y) / 2), (int)vsx, (int)vsy), 0, 0, c.Img.Width, c.Img.Height, GraphicsUnit.Pixel, ia);
                }
                else
                {
                    //透明化カラー補正なし
                    g.DrawImage(c.Img, new Rectangle((int)(atr.Offset.X - (atr.Width * atr.Scale.X) / 2), (int)(atr.Offset.Y - (atr.Height * atr.Scale.Y) / 2), (int)vsx, (int)vsy));
                }
                //Draw Helper
                if (checkBox_Helper.Checked)
                {
                    //中心点やその他のサポート表示
                    //CenterPosition
                    g.DrawEllipse(Pens.OrangeRed, -4, -4, 8, 8);

                    //Selected DrawBounds
                    if (e.isSelect)
                    {
                        g.DrawRectangle(Pens.DarkCyan, atr.Offset.X - (atr.Width * atr.Scale.X) / 2, atr.Offset.Y - (atr.Height * atr.Scale.Y) / 2, vsx - 1, vsy - 1);
                    }
                    //test Hit範囲をボックス描画
                    //ElementsType
                    if (e.Style == ELEMENTS.ELEMENTSSTYLE.Rect)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.Orange)), (-(atr.Width * atr.Scale.X) / 2 ),(-(atr.Height * atr.Scale.Y) / 2 ), vsx - 1, vsy - 1);
                    }
                    if(e.Style == ELEMENTS.ELEMENTSSTYLE.Circle)
                    {
                        g.FillEllipse(new SolidBrush(Color.FromArgb(128, Color.Orange)), (-(atr.Width * atr.Scale.X) / 2 ), (-(atr.Height * atr.Scale.Y) / 2 ), vsx - 1, vsy - 1);
                    }                
                }
                g.Transform = Back;//restore Matrix

                //Cuurent Draw Grip
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
                        CELL c = new CELL();
                        c.FromPngFile(str);
                        ImageMan.AddCell(c);
                        treeView_Project_AddElements(c, sPos.X, sPos.Y);
                        //ImageListへ登録と更新
                        mFormImageList.AddItem(str);
                        mFormImageList.Refresh();
                        //CellListの表示更新
                        mFormCell.Refresh();
                    }
                }
                e.Effect = DragDropEffects.Copy;
            }

            //ListViewItem受け入れ
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem lvi = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                //Cellの登録 Image Item
                CELL work = new CELL();
                
                work.Img = (Bitmap)lvi.ImageList.Images[lvi.ImageIndex];
                //画像そのままならこれでいいが一部切り抜きとなると変更
                // ！！！どうやらオリジナル画像でなくサムネらしい！！！
                //必要なのはmFormImagelist.mListImageの中身っぽい？
                
                work.Rect = new Rectangle(0, 0, work.Img.Width, work.Img.Height);
                ImageMan.AddCell(work);//画像サイズ登録実画像はいずこ！

                treeView_Project_AddElements(work, sPos.X, sPos.Y);
                e.Effect = DragDropEffects.Copy;
            }

            //CELL 受け入れ
            if (e.Data.GetDataPresent(typeof(CELL)))
            {
                //Store Cell Item
                CELL work = (CELL)e.Data.GetData(typeof(CELL));
                ImageMan.AddCell(work);//画像登録
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

            if (e.Data.GetDataPresent(typeof(CELL)))
            { e.Effect = DragDropEffects.Copy; }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            { e.Effect = DragDropEffects.Copy; }
        }
        private void PanelPreView_MouseWheel(object sender, MouseEventArgs e)
        {
            mWheelDelta = (e.Delta > 0)? + 1:-1 ;//+/-に適正化
            if (TimeLine.EditFrame.ActiveIndex != null)
            {
                ELEMENTS nowEle = TimeLine.EditFrame.GetActiveElements();
                //アイテム選択中のホイール操作
                if (mKeysSP == Keys.Shift)
                {
                    //Shift+Wheel 部品の拡縮 0.1単位 最小0.1に制限
                    mDragState = DragState.Scale;
                    nowEle.Atr.Scale.X += ((nowEle.Atr.Scale.X + mWheelDelta / 10f) > 0.1f) ? mWheelDelta / 10f : 0.1f;
                    nowEle.Atr.Scale.Y += ((nowEle.Atr.Scale.Y + mWheelDelta / 10f) > 0.1f) ? mWheelDelta / 10f : 0.1f;
                    //nowEle.Atr.Scale.Z += ((nowEle.Atr.Scale.Z + mWheelDelta / 10f) > 0.1f) ? mWheelDelta / 10f : 0.1f;
                }
                if(mKeysSP == Keys.Control)
                {
                    //Ctrl+Wheel 回転 1度単位
                    mDragState = DragState.Angle;

                    float w = nowEle.Atr.Radius.Z+ mWheelDelta;
                    if (w >= 360)
                    {
                        nowEle.Atr.Radius.Z = w % 360;
                    }
                    else if (w < 0)
                    {
                        //nowEle.Atr.Radius.Z = 360 - (float)Math.Acos(w % 360);
                        nowEle.Atr.Radius.Z = (w % 360)+360;
                    }
                    else
                    {
                        nowEle.Atr.Radius.Z += mWheelDelta;
                    }
                }
                mFormAttribute.SetAllParam(nowEle.Atr);
                panel_PreView.Refresh();
            }
            else
            {
                //画面の拡大縮小
                if (mWheelDelta > 0)
                {
                    if(HScrollBar_ZoomLevel.Value > HScrollBar_ZoomLevel.Minimum) HScrollBar_ZoomLevel.Value -= mWheelDelta;
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
                SetNowElementsIndex(TimeLine.EditFrame.SelectElement((int)stPosX,(int)stPosY, true));
                ELEMENTS nowEle = TimeLine.EditFrame.GetActiveElements();
                //Item選択中なら移動変形処理等の準備
                if (nowEle != null)
                {              
                    mMouseDownShift.X = (int)(nowEle.Atr.Position.X - stPosX);
                    mMouseDownShift.Y = (int)(nowEle.Atr.Position.Y - stPosY);
                }
                mMouseLDown = true;                
                panel_PreView.Refresh();
                treeView_Project.Refresh();
                mFormControl.Refresh();
            }
        }
        private void PanelPreView_MouseMove(object sender, MouseEventArgs e)
        {
            float zoom = HScrollBar_ZoomLevel.Value / mParZOOM;
            //e.X,Yからステージ上の座標にする
            float stPosX = (e.X - (panel_PreView.Width  / 2)) / zoom;
            float stPosY = (e.Y - (panel_PreView.Height / 2)) / zoom;

            ELEMENTS nowEle = TimeLine.EditFrame.GetActiveElements();
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
                            int w=(int)nowEle.Atr.Radius.Z+ ((int)stPosX- mMouseDownPoint.X )/4;
                            if (w < 0)
                            {
                                //右回転
                                w = 360 + (w%360) ;
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
            StatusLabel2.Text = $" [Select:{TimeLine.EditFrame.ActiveIndex}][ScX{mScreenScroll.X:###}/ScY{mScreenScroll.Y:###}] [Zoom:{zoom}]{mDragState.ToString()}:{mWheelDelta}";
        }
        private void PanelPreView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //previewKey
            //なぜかイベント発生しない？なんだろ？
            //メインフォームのほうが優先されるらしい keyPreview=True
            //部品選択中か確認

            //GetElement
            ELEMENTS nowEle = TimeLine.EditFrame.GetActiveElements();
            if (nowEle == null) return;

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
                TimeLine.EditFrame.Remove((int)TimeLine.EditFrame.ActiveIndex);
            }
        }
        /// <summary>
        /// エディット中の選択エレメントをインデックス指定と関連画面更新
        /// </summary>
        /// <param name="ElementsIndex"></param>
        public void SetNowElementsIndex(int? ElementsIndex)
        {
            //現在の選択と違う物であれば変更を行う
            if (ElementsIndex == null)
            {
                TimeLine.EditFrame.ActiveIndex=null;//無選択に
                return;
            }
            int? idx = TimeLine.EditFrame.ActiveIndex;
            if (idx != ElementsIndex)
            {
                //現在の選択を解除
                ELEMENTS elem = TimeLine.EditFrame.GetActiveElements();
                if (elem != null)
                {
                    elem.isSelect = false;
                }
                //更新
                TimeLine.EditFrame.ActiveIndex = ElementsIndex;
                //新規選択を有効
                elem = TimeLine.EditFrame.GetActiveElements();
                elem.isSelect = true;
                //各種リフレッシュ
                panel_PreView.Refresh();
                treeView_Project_Update();                
                treeView_Project.Refresh();
                mFormAttribute.SetAllParam(elem.Atr);
                mFormAttribute.Refresh();
                mFormControl.Refresh();
            }
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
            ImageMan.SaveToFile("ImageTest");
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

        private void ToolStripMenuItem_DebugExport_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> clDicFile = new Dictionary<string, object>();

            //以下、ファイル情報出力処理
            clDicFile["ver"] = "0.0.1";
            clDicFile["hogehoge"] = 99;

            //以下、イメージ出力処理
            int inCnt, inMax = this.ImageMan.CellList.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                CELL clCell = this.ImageMan.CellList[inCnt];
                string clKey = clCell.ID.ToString();
                clDicFile[clKey] = clCell.Export();
            }

            //以下、アニメ出力処理
            inMax = this.TimeLine.gmTimeLine.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                FRAME clFrame = this.TimeLine.gmTimeLine[inCnt];
                clDicFile["frm_" + inCnt] = clFrame.Export();   //ここのキーはアニメ名（ユニーク制約にしないとダメ＞＜）としたい
            }

            //以下、DictionaryをJson形式に変換する処理
            string clJsonData = ClsSystem.DictionaryToString(clDicFile);

            //以下、ファイル出力処理
            string clPath = ClsPath.GetPath();
            string clPathFile = Path.Combine(clPath, "よしさんデバッグ用ファイル.txt");
            File.WriteAllText(clPathFile, clJsonData);
        }
    }
}
