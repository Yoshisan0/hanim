using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private bool mMouseRDown = false;//R
        private bool mMouseMDown = false;//M
        private int mWheelDelta;//Wheel
        private Keys mKeys,mKeysSP;//キー情報 通常キー,スペシャルキー

        private int? mNowSelectIndex = null;
        private string mNowMotionName;//選択中モーション名

        enum DragState { none,Move, Angle, Scale,Scroll, Joint }; 
        private DragState mDragState = DragState.none;

        private Point PreViewCenter;//PanelPreView Centerセンターポジション
        
        private ImageManagerBase ImageMan;
        public TIMELINEbase TimeLine;


        public FormMain()
        {
            InitializeComponent();
        }

        public void AttributeUpdate()
        {
            //パラメータ変更通知
            mFormAttribute.ParamChanged = false;
            //パラメータ取得処理
            //みじｓ
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //以下、初期化処理
            ClsTool.Init();
            PreViewCenter = new Point(0, 0);
            mScreenScroll = new Point(0, 0);

            ImageMan = new ImageManagerBase();
            TimeLine = new TIMELINEbase();

            this.mFormImageList = new FormImageList();
            this.mFormImageList.Show();

            this.mFormControl = new FormControl(this);
            this.mFormControl.mTimeLine = TimeLine;
            this.mFormControl.Show();

            this.mFormAttribute = new FormAttribute(this);
            this.mFormAttribute.Show();

            mFormControl.mTimeLine = TimeLine;//ControlFormに通達
            //Ver2
            mFormCell = new FormCell(this);
            mFormCell.IM = ImageMan;
            mFormCell.Show();

            AlingForms();

            //背景の再描画をキャンセル(ちらつき抑制)
            //効果いまいち
            this.SetStyle(ControlStyles.Opaque, true);
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
                this.mFormImageList = new FormImageList();
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
            if (mFormImageList != null) mFormImageList.Visible = CB_ImageList.Checked;
        }
        private void CB_Control_CheckedChanged(object sender, EventArgs e)
        {
            if(mFormControl!=null) mFormControl.Visible = CB_Control.Checked;
        }
        private void CB_Attribute_CheckedChanged(object sender, EventArgs e)
        {
            if(mFormAttribute!=null) mFormAttribute.Visible = CB_Attribute.Checked;
        }
        private void CB_CellList_CheckedChanged(object sender, EventArgs e)
        {
            if(mFormCell!=null) mFormCell.Visible = CB_CellList.Checked;
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
        }

        private void treeView_Project_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //ReName ProjectName
            if (e.Node.ImageIndex == 0)
            {
            }

            //ReName MotionName
            if (e.Node.ImageIndex == 2)
            {
                mNowMotionName = e.Node.Text;
            }

            //ReName ElementsName
            if (e.Node.ImageIndex == 4)
            {
                TimeLine.EditFrame.RenameElements(e.Node.Tag, e.Node.Text);
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
            mNowSelectIndex = TimeLine.EditFrame.SelectElement(e.Node.Tag);
            if (mNowSelectIndex != null) PanelPreView.Refresh();
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
            PanelPreView.Refresh();
        }
        private void CrossBarCheck_Click(object sender, EventArgs e)
        {
            PanelPreView.Refresh();
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

            this.TSmenu_ImageList.Checked = (this.mFormImageList != null);
            this.TSmenu_Control.Checked   = (this.mFormControl   != null);
            this.TSmenu_Attribute.Checked = (this.mFormAttribute != null);
        }

        private void ZoomLevel_ValueChanged(object sender, EventArgs e)
        {
            PanelPreView.Refresh();
        }
        private void FormMain_Resize(object sender, EventArgs e)
        {
            PanelPreView.Refresh();
        }
        /// 終了処理
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default["FormMainLocate"] = this.Location;
            Properties.Settings.Default.Save();
        }

        private void PanelPreView_Paint(object sender, PaintEventArgs e)
        {
            //以下、拡大してボケないようにする処理
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            //e.Graphics.PixelOffsetMode   = PixelOffsetMode.HighQuality;

            //画像で背景fill
            //e.Graphics.FillRectangle(new TextureBrush(Properties.Resources.Blank),new Rectangle(0,0,PanelPreView.Width,PanelPreView.Height));
            float zoom = ZoomLevel.Value / mParZOOM;//ZoomLevel(2-80)1/10にして使う
            if (zoom < 0.2) zoom = 0.2f;//下限を(0.2)1/5とする
            float grid = (float)Num_Grid.Value;

            e.Graphics.TranslateTransform(  PanelPreView.Width/2 -PanelPreView.Width/2*zoom,
                                            PanelPreView.Height/2 -PanelPreView.Height/2*zoom);
            e.Graphics.ScaleTransform(zoom, zoom);
            //GridBar
            if (GridCheck.Checked)
            {
                //スクリーン中心点にあわせるよ
                // V
                /*var p1 = new Pen(GridColor.BackColor);
                for (float cnt = (((float)PanelPreView.Width / 2) % (grid * zoom)); cnt < PanelPreView.Width; cnt += (grid * zoom))
                {
                    e.Graphics.DrawLine(p1, cnt, 0.0f, cnt, PanelPreView.Height);
                }
                //H
                for (float cnt = ((float)(PanelPreView.Height / 2) % (grid * zoom)); cnt < PanelPreView.Height; cnt += (grid * zoom))
                {
                    e.Graphics.DrawLine(p1, 0.0f, cnt, PanelPreView.Width, cnt);
                }*/

                //Grid Draw
                // V
                var p1 = new Pen(GridColor.BackColor);
                for (float cnt = ((float)PanelPreView.Width / 2) % (grid ); cnt < PanelPreView.Width; cnt += (grid))
                {
                    e.Graphics.DrawLine(p1, cnt, 0.0f, cnt, PanelPreView.Height);
                }
                //H
                for (float cnt = ((float)(PanelPreView.Height / 2) % (grid )); cnt < PanelPreView.Height; cnt += (grid))
                {
                    e.Graphics.DrawLine(p1, 0.0f, cnt, PanelPreView.Width, cnt);
                }
            }

            // 各種Itemの描画処理
            // DrawItems
            Matrix back = e.Graphics.Transform;
            if (TimeLine.EditFrame != null)
            {
                DrawParts(sender, e.Graphics);
            }
            e.Graphics.Transform = back;
            //CrossBar スクリーン移動時は原点に沿う形に
            if(CrossBarCheck.Checked)
            {
                var p1 = new Pen(CrossColor.BackColor);
                e.Graphics.DrawLine(p1,PanelPreView.Width / 2, 0, PanelPreView.Width/2, PanelPreView.Height);//V
                e.Graphics.DrawLine(p1, 0, PanelPreView.Height/2, PanelPreView.Width,PanelPreView.Height/2);//H
            }
            
        }        

        private void DrawParts(object sender, Graphics g)
        {
            //なんだか遅いなぁ・・ちらつくなぁ・・
            //表示の仕方も悩む　親もマーク表示するか　等
            //StageInfomation
            float zoom = ZoomLevel.Value / mParZOOM;
            if (zoom < 0.2) zoom = 0.2f;
            int vcx = mScreenScroll.X + PanelPreView.Width  / 2 ;//ViewCenter X
            int vcy = mScreenScroll.Y + PanelPreView.Height / 2 ;//ViewCenter Y

            FRAME frm = TimeLine.EditFrame;

            for(int cnt=0; cnt< frm.ElementsCount;cnt++)
            {                
                ELEMENTS e = frm.GetElement(cnt);
                AttributeBase atr = e.Atr;
                Matrix Back = g.Transform;
                Matrix MatObj = new Matrix();

                float vsx = atr.Width * atr.Scale.X;//* zoom;//SizeX
                float vsy = atr.Height * atr.Scale.Y;// * zoom;//SizeY

                CELL c = ImageMan.GetCellFromHash(atr.CellID);
                if (c == null) { Console.WriteLine("Image:null");return; }

                //原点を部品中心に
                //g.TranslateTransform(   vcx + (atr.Position.X + atr.Width/2)  * atr.Scale.X *zoom,
                //                        vcy + (atr.Position.Y + atr.Height/2) * atr.Scale.Y *zoom);//部品中心座標か？


                //平行移動
                g.TranslateTransform(vcx + (atr.Position.X * atr.Scale.X),vcy + (atr.Position.Y * atr.Scale.Y));
                //回転角指定
                g.RotateTransform(atr.Radius.X);
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
                g.DrawImage(c.Img, -(atr.Width *atr.Scale.X ) / 2, -(atr.Height*atr.Scale.Y ) / 2, vsx, vsy);

                //Selected DrawBounds
                if (e.Select)
                {
                    g.DrawRectangle(Pens.DarkCyan, -(atr.Width * atr.Scale.X) / 2, -(atr.Height * atr.Scale.Y) / 2, vsx-1,vsy-1);
                }
                
                //test Hit範囲をボックス描画
                /*
                 g.DrawRectangle(Pens.Aqua,  (-(atr.Width *atr.Scale.X)/2 * atr.Scale.X),
                                            (-(atr.Height *atr.Scale.Y)/2 * atr.Scale.Y),
                                            vsx - 1, vsy - 1);
                */

                g.Transform = Back;//restore Matrix

                //Cuurent Draw Grip
            }
        }
        private void GripDraw(AttributeBase atr,Graphics g)
        {
            //アイテム操作ハンドル　中心点とオブジェの４点rectと角度
            //LineColor GripColor RadiusColor

        }

        private void PanelPreView_DragDrop(object sender, DragEventArgs e)
        {
            float zoom = ZoomLevel.Value / mParZOOM;
            Point sPos = PanelPreView.PointToClient(new Point(e.X, e.Y));
            //PNGファイル直受け入れ
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //1画像 1CELL 1Element
                //File
                string[] AllPaths = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach(string str in AllPaths)
                {
                    string ext = System.IO.Path.GetExtension(str).ToLower();
                    if (ext==".png")
                    {
                        CELL c = new CELL();
                        c.FromPngFile(str);
                        ImageMan.AddCell(c);
                        AddElements(c, sPos.X, sPos.Y);
                    }
                }
                e.Effect = DragDropEffects.Copy;
            }

            //ListViewItem受け入れ
            if ( e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem lvi = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                //Cellの登録 Image Item
                CELL work = new CELL();
                work.Img =(Bitmap) lvi.ImageList.Images[lvi.ImageIndex];
                //画像そのままならこれでいいが一部切り抜きとなると変更
                // ！！！どうやらオリジナル画像でなくサムネらしい！！！
                work.Rect = new Rectangle(0, 0, work.Img.Width, work.Img.Height);
                ImageMan.AddCell(work);//画像登録
                
                AddElements(work, sPos.X, sPos.Y);
                e.Effect = DragDropEffects.Copy;
            }

            //CELL 受け入れ
            if (e.Data.GetType() == typeof(CELL))
            {
                //Store Cell Item
                CELL work = (CELL)e.Data.GetData(typeof(CELL));
                ImageMan.AddCell(work);//画像登録

                Point a = PanelPreView.PointToClient(new Point(e.X, e.Y));
                AddElements(work, a.X, a.Y);
                e.Effect = DragDropEffects.Copy;
            }
            if(e.Data.GetType()==typeof(ELEMENTS))
            { e.Effect = DragDropEffects.Copy; }
            if ( e.Data.GetType()==typeof(Image))
            { e.Effect = DragDropEffects.Copy; }            

            PanelPreView.Refresh();
        }

        /// <summary>
        /// CellからElementを作成し追加
        /// </summary>
        /// <param name="work"></param>
        /// <param name="x">クリック座標(Cliant)</param>
        /// <param name="y">クリック座標(Cliant)</param>
        private void AddElements(CELL work,int x,int y)
        {
            //アイテムの登録
            ELEMENTS elem = new ELEMENTS();
            elem.Atr = new AttributeBase();
            elem.Atr.CellID = work.GetHashCode();
            elem.Atr.Width  = work.Img.Width;
            elem.Atr.Height = work.Img.Height;
            elem.Tag = elem.GetHashCode();

            //センターからの距離に変換
            x -= PanelPreView.Width  / 2;
            y -= PanelPreView.Height / 2;
            //さらに画像サイズ半分シフトして画像中心をセンターに
            x -= elem.Atr.Width  / 2;
            y -= elem.Atr.Height / 2;

            elem.Atr.Position = new Vector3(x,y,0);
            elem.Name = elem.GetHashCode().ToString("X8");//仮名
            
            //Show - Attribute
            mFormAttribute.SetAllParam(elem.Atr);

            TimeLine.EditFrame.AddElements(elem);//Elements登録
            TimeLine.Store();//
            // "Motion"固定決め打ちしてるのはあとでモーション名管理変数に置き換え

            //TreeNode selNode = treeView_Project.Nodes[mNowMotionName];
            TreeNode selNode = treeView_Project.Nodes["Motion"];
            selNode.Nodes.Add(elem.Name,elem.Name);
            selNode.Expand();
            selNode.Nodes[elem.Name].Tag = elem.GetHashCode();
            selNode.Nodes[elem.Name].ImageIndex = 4;
            selNode.Nodes[elem.Name].SelectedImageIndex = 3;

            //Control更新
            mFormControl.Refresh();

        }
        private void RemoveElements(string name)
        {
            //Elements選択中のDelキー
        }
        //ProjectTree
        private void AddMotion(string name)
        {
            treeView_Project.SelectedNode = treeView_Project.TopNode;
            TreeNode tn = treeView_Project.Nodes.Add(name);
            tn.ImageIndex = 2;
            tn.SelectedImageIndex = 2;
            tn.Tag = 1;//仮番号
            
        }
        private void RemoveMotion(string name)
        { }
        private void UpdateTree()
        {
            //プロジェクト全体情報からTreeを作成
            //Image情報収集
            //CELL情報収集
            //Elements情報収集

        }

        private void button1_MotionAdd_Click(object sender, EventArgs e)
        {
            AddMotion("NewMotion");
            //モーションである事を示すタグを付加する？
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
            if (mNowSelectIndex != null)
            {
                ELEMENTS nowEle = TimeLine.EditFrame.GetElement((int)mNowSelectIndex);
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

                    float w = nowEle.Atr.Radius.X + mWheelDelta;
                    if (w >= 360)
                    {
                        nowEle.Atr.Radius.X = w % 360;
                    }
                    else if (w < 0)
                    {
                        nowEle.Atr.Radius.X = 360 - (float)Math.Acos(w % 360);
                    }
                    else
                    {
                        nowEle.Atr.Radius.X += mWheelDelta;
                    }


                }
                mFormAttribute.SetAllParam(nowEle.Atr);
                PanelPreView.Refresh();
            }
            else
            {
                //画面の拡大縮小
                if (mWheelDelta > 0)
                {
                    if(ZoomLevel.Value > ZoomLevel.Minimum) ZoomLevel.Value -= mWheelDelta;
                }
                else
                {
                    if (ZoomLevel.Value < ZoomLevel.Maximum) ZoomLevel.Value -= mWheelDelta;
                }
                  
                mWheelDelta = 0;
            }
            StatusLabel2.Text = $"{mWheelDelta}";
        }
        private void PanelPreView_MouseUp(object sender, MouseEventArgs e)
        {
            //releaseMouse
            mMouseLDown = false;
            mMouseMDown = false;
            mMouseRDown = false;
            mDragState = DragState.none;
        }
        private void PanelPreView_MouseDown(object sender, MouseEventArgs e)
        {
            //e.X,Yからステージ上の座標にする
            float zoom = ZoomLevel.Value / mParZOOM;
            float stPosX = ((e.X -(PanelPreView.Width  / 2)) / zoom);
            float stPosY = ((e.Y -(PanelPreView.Height / 2)) / zoom);

            //Item選択中なら移動変形処理等の準備
            if (e.Button == MouseButtons.Left)
            {
                mMouseDownPoint = new Point(e.X-(PanelPreView.Width/2),e.Y-(PanelPreView.Height/2));

                //アイテム検索
                mNowSelectIndex = TimeLine.EditFrame.SelectElement((int)stPosX,(int)stPosY, true);
                if (mNowSelectIndex != null)
                {
                    ELEMENTS nowEle = TimeLine.EditFrame.GetElement((int)mNowSelectIndex);
                    mMouseDownShift.X = (int)(nowEle.Atr.Position.X - stPosX);
                    mMouseDownShift.Y = (int)(nowEle.Atr.Position.Y - stPosY);
                }
                mMouseLDown = true;                
                PanelPreView.Refresh();
            }
        }
        private void PanelPreView_MouseMove(object sender, MouseEventArgs e)
        {
            float zoom = ZoomLevel.Value / mParZOOM;
            //e.X,Yからステージ上の座標にする
            float stPosX = (e.X - (PanelPreView.Width  / 2)) / zoom;
            float stPosY = (e.Y - (PanelPreView.Height / 2)) / zoom;

            if (mNowSelectIndex != null)
            {
                ELEMENTS nowEle = TimeLine.EditFrame.GetElement((int)mNowSelectIndex);
                //移動処理
                if (mMouseLDown)
                {
                    if (nowEle != null)
                    {
                        //+CTRL マウスでの回転 左周りにしかなってない
                        if (mKeysSP == Keys.Control)
                        {
                            float w = nowEle.Atr.Radius.X + mMouseDownShift.X;
                            if(w > 360)
                            {
                                nowEle.Atr.Radius.X = w % 360;
                            }
                            else if(w<0)
                            {
                                nowEle.Atr.Radius.X = 360- (w % 360);
                            }
                            else
                            {
                                nowEle.Atr.Radius.X += mMouseDownShift.X;
                            }

                            //w = nowEle.Atr.Radius.Y + mMouseDownShift.Y;
                            //nowEle.Atr.Radius.Y = (w > 360) ? w - 360 : 360 - w;
                            //シフトキーも押されていればZ回転 等(将来)
                        }
                        //if( mKeysSP==Keys.Alt) //将来用
                        else
                        { 
                            //差分加算
                            mDragState = DragState.Move;
                            nowEle.Atr.Position.X = stPosX + mMouseDownShift.X;
                            nowEle.Atr.Position.Y = stPosY + mMouseDownShift.Y;
                            mFormAttribute.SetAllParam(nowEle.Atr);
                        }                        
                    }
                    else
                    {
                        //アイテム選択が無い場合のLドラッグはステージのXYスクロール
                        mDragState = DragState.Scroll;
                        mScreenScroll.X = (e.X - (PanelPreView.Width / 2)) - mMouseDownPoint.X;
                        mScreenScroll.Y = (e.Y - (PanelPreView.Height / 2)) - mMouseDownPoint.Y;
                    }
                    PanelPreView.Refresh();
                }
            }
            StatusLabel.Text = $"[X:{stPosX:####}/Y:{stPosY:####}] [Px:{mMouseDownPoint.X:####}/Py:{mMouseDownPoint.Y:####}]";
            StatusLabel2.Text = $" [Select:{mNowSelectIndex}][ScX{mScreenScroll.X:###}/ScY{mScreenScroll.Y:###}] [Zoom:{zoom}]{mDragState.ToString()}:{mWheelDelta}";
        }
        private void PanelPreView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //previewKey
            //部品選択中か確認

            //GetElement
            if (mNowSelectIndex == null) return;
            ELEMENTS nowEle = TimeLine.EditFrame.GetElement((int)mNowSelectIndex);

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
        }

        private void BottonTest_Click(object sender, EventArgs e)
        {
            // testCode
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Image img = new Bitmap(fd.FileName);                
                FormImageCut fc = new FormImageCut(img,fd.FileName);
            }
        }


    }
}
