using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{

    public partial class FormImageList : Form
    {
        private FormMain mFormMain = null;
        private ArrayList mListImage;   //イメージリスト
        private Point mMouseDownPoint = Point.Empty; //ドラックドロップ開始点
        private bool m_isMouseLDown;    //左クリック押し下げ中

        public FormImageList(FormMain form)
        {
            InitializeComponent();

            //以下、初期化処理
            this.mFormMain = form;
        }

        private void FormImageList_Load(object sender, EventArgs e)
        {
            //以下、ウィンドウの設定
            this.Location = ClsSystem.mSetting.mWindowImageList.mLocation;
            this.Size = ClsSystem.mSetting.mWindowImageList.mSize;

            this.mListImage = new ArrayList();
            this.mMouseDownPoint = Point.Empty;
        }
        private void FormImageList_DragEnter(object sender, DragEventArgs e)
        {
            
            /*
            if (e.Data.GetDataPresent(typeof(ListViewItem[])))
            {
                //この処理が無いと、ドラッグ＆ドロップ開始早々絵が表示されない
                IntPtr pinDesktopWindow = ClsWin32.GetDesktopWindow();
                ClsWin32.ImageList_DragEnter(pinDesktopWindow, Cursor.Position.X, Cursor.Position.Y);

                e.Effect = DragDropEffects.None;
            }
            else
            */
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                bool isSuccess = false;
                string[] pclAllPaths = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string clPathTmp1 in pclAllPaths)
                {
                    if (Directory.Exists(clPathTmp1))
                    {
                        string[] pclFilePaths = System.IO.Directory.GetFiles(clPathTmp1, "*.*", SearchOption.AllDirectories);
                        foreach (string clPathTmp2 in pclFilePaths)
                        {
                            bool isChk = this.ChkImageFile(clPathTmp2);
                            if (!isChk) continue;

                            isSuccess = true;
                        }
                    }
                    else if (File.Exists(clPathTmp1))
                    {
                        bool isChk = this.ChkImageFile(clPathTmp1);
                        if (!isChk) continue;

                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }
        private void FormImageList_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                bool isSuccess = false;
                string[] pclAllPaths = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string clPathTmp1 in pclAllPaths)
                {
                    if (Directory.Exists(clPathTmp1))
                    {
                        string[] pclFilePaths = System.IO.Directory.GetFiles(clPathTmp1, "*.*", SearchOption.AllDirectories);
                        foreach (string clPathTmp2 in pclFilePaths)
                        {
                            if (!ChkImageFile(clPathTmp2)) continue;
                            this.AddItem(clPathTmp2);
                            isSuccess = true;
                        }
                    }
                    else if (File.Exists(clPathTmp1))
                    {
                        if (!ChkImageFile(clPathTmp1)) continue;
                        this.AddItem(clPathTmp1);
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }
        /// <summary>
        /// OpenFileDialog(png)
        /// </summary>
        /// <param name="path">InitalDirectory</param>
        private void AddImage(string path)
        {
            OpenFileDialog clDialog = new OpenFileDialog();
            clDialog.FileName = "";
            clDialog.InitialDirectory = ClsSystem.mSetting.mLastImageDirectory;
            clDialog.Filter = "PNGファイル(*.png)|*.png|すべてのファイル(*.*)|*.*";
            clDialog.FilterIndex = 0;
            clDialog.Title = "png ファイルを選択してください";
            clDialog.RestoreDirectory = true;
            clDialog.Multiselect = true;
            if (clDialog.ShowDialog() != DialogResult.OK) return;
            ClsSystem.mSetting.mLastImageDirectory = Path.GetDirectoryName(clDialog.FileName);

            int inCnt, inMax = clDialog.FileNames.Length;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                string clPath = clDialog.FileNames[inCnt];
                bool isChk = this.ChkImageFile(clPath);
                if (!isChk) continue;

                this.AddItem(clPath);
            }
        }
        /// <summary>
        /// ファイル名(PNGチェック済)を登録する
        /// </summary>
        /// <param name="clPath"></param>
        public void AddItem(string clPath)
        {
            Image clImage = Bitmap.FromFile(clPath);
            byte[] pchBuffer = ClsSystem.ImageToByteArray(clImage);
            string clMD5 = ClsSystem.GetMD5FromMemory(pchBuffer);
            this.AddItem(clPath, clMD5, clImage);
        }
        private void AddItem(string clPath, string clMD5, Image clImage)
        {
            //ImageManへの登録
            ImageChip chip = new ImageChip();
            chip.FromPngFile(clPath,true);
            ClsSystem.ImageMan.AddImageChip(chip);

            //以下、MD5重複チェック処理
            foreach (ListViewItem clItemTmp in this.listView.Items)
            {
                bool isEqual = clMD5.Equals(clItemTmp.SubItems[2].Text);
                if (isEqual) return;
            }

            //以下、イメージをテーブルに追加する処理
            Image clImageBig = null;
            Image clImageSmall = null;
            this.ResizeImage(clImage, ref clImageBig, ref clImageSmall);

            bool isExist = ClsSystem.mTblImage.ContainsKey(clMD5);
            if (!isExist)
            {
                ClsImage clImageData = new ClsImage();
                clImageData.Origin = clImage;
                clImageData.Big = clImageBig;
                clImageData.Small = clImageSmall;

                ClsSystem.mTblImage.Add(clMD5, clImageData);
            }

            //以下、アイテム追加処理
            int inIndexImage = this.imageList.Images.Count;
            this.mListImage.Add(clImage);
            this.imageList.Images.Add(clImage);

            string[] pclCells = new string[3];
            pclCells[0] = null;
            pclCells[1] = clPath;
            pclCells[2] = clMD5;

            ListViewItem clItem = new ListViewItem(pclCells);
            clItem.ImageIndex = inIndexImage;
            clItem.ToolTipText = clPath+"\n{"+clMD5+"}";//
            clItem.Tag = clMD5;//TagにもMD5指定
            clItem.Text = listView.Items.Count.ToString();

            this.listView.Items.Add(clItem);
            mFormMain.Refresh();
        }
        public void RemoveAllImage()
        {
            mListImage.Clear();
            listView.Items.Clear();
        }

        /// <summary>
        /// ImageManからの再登録
        /// </summary>
        public void Restore()
        {
            //AddImageでやるとIDが振り直されるので新設
            //ImageManから再登録する
            ImageChip[] icl = ClsSystem.ImageMan.ToArray();
            foreach(ImageChip ic in icl)
            {
                //以下、アイテム追加処理
                int inIndexImage = this.imageList.Images.Count;
                this.mListImage.Add(ic.Img);
                this.imageList.Images.Add(ic.Img);

                string[] pclCells = new string[3];
                pclCells[0] = null;
                pclCells[1] = ic.Path;
                pclCells[2] = ic.StrMD5;

                ListViewItem clItem = new ListViewItem(pclCells);
                clItem.ImageIndex = inIndexImage;
                clItem.ToolTipText = ic.Path + "\n{" + ic.StrMD5 + "}";//
                clItem.Tag = ic.StrMD5;//TagにもMD5指定
                clItem.Text = listView.Items.Count.ToString();

                this.listView.Items.Add(clItem);
            }
        }
        private void ResizeImage(Image clImageSrc, ref Image clImageBig, ref Image clImageSmall)
        {
            Rectangle stRectSrc = new Rectangle(0, 0, clImageSrc.Width, clImageSrc.Height);

            clImageBig = new Bitmap(128, 128);
            using (Graphics g = Graphics.FromImage(clImageBig))
            {
                int inWidth, inHeight;
                if (clImageSrc.Width == clImageSrc.Height)
                {
                    inWidth = 128;
                    inHeight = 128;
                }
                else if (clImageSrc.Width < clImageSrc.Height)
                {
                    inWidth = clImageSrc.Width * 128 / clImageSrc.Height;
                    inHeight = 128;
                }
                else
                {
                    inWidth = 128;
                    inHeight = clImageSrc.Height * 128 / clImageSrc.Width;
                }

                Rectangle stRectDst = new Rectangle((128 - inWidth) / 2, (128 - inHeight) / 2, inWidth, inHeight);
                g.DrawImage(clImageSrc, stRectDst, stRectSrc, GraphicsUnit.Pixel);
            }

            clImageSmall = new Bitmap(32, 32);
            using (Graphics g = Graphics.FromImage(clImageSmall))
            {
                int inWidth, inHeight;
                if (clImageSrc.Width == clImageSrc.Height)
                {
                    inWidth = 32;
                    inHeight = 32;
                }
                else if (clImageSrc.Width < clImageSrc.Height)
                {
                    inWidth = clImageSrc.Width * 32 / clImageSrc.Height;
                    inHeight = 32;
                }
                else
                {
                    inWidth = 32;
                    inHeight = clImageSrc.Height * 32 / clImageSrc.Width;
                }

                Rectangle stRectDst = new Rectangle((32 - inWidth) / 2, (32 - inHeight) / 2, inWidth, inHeight);
                g.DrawImage(clImageSrc, stRectDst, stRectSrc, GraphicsUnit.Pixel);
            }
        }
        /// <summary>
        /// ファイルがPNGファイルであるか確認する
        /// </summary>
        /// <param name="clPath"></param>
        /// <returns></returns>
        private bool ChkImageFile(string clPath)
        {
            try
            {
                using (Image clImage = Image.FromFile(clPath))
                {
                    if (!clImage.RawFormat.Equals(ImageFormat.Png))
                    {
                        return (false);
                    }
                }
            }
            catch (Exception)
            {
                return (false);
            }
            return (true);
        }

        private void button_Add_Click(object sender, EventArgs e)
        {             
            AddImage(ClsPath.GetPath());
        }
        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (this.listView.SelectedIndices.Count != 1) return;

            int inIndex = this.listView.SelectedIndices[0];
            string md5 = (string)this.listView.Items[inIndex].Tag;
            ClsSystem.ImageMan.RemoveImageChipMD5(md5);

            this.imageList.Images.RemoveAt(inIndex);
            this.listView.Items.RemoveAt(inIndex);
            
            int inCnt, inMax = this.listView.Items.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                this.listView.Items[inCnt].ImageIndex = inCnt;
            }
        }
        private void button_Cut_Click(object sender, EventArgs e)
        {
            if (this.listView.SelectedIndices.Count != 1) return;

            int inIndex = this.listView.SelectedIndices[0];
            this.CutImage(inIndex);
        }
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView.SelectedIndices.Count != 1) return;

            int inIndex = this.listView.SelectedIndices[0];
            this.CutImage(inIndex);
        }

        /// <summary>
        /// 指定画像を指定サイズで全分割(ImageCutの方で行うので未使用)
        /// </summary>
        /// <param name="inIndex">画像番号</param>
        /// <param name="w">幅</param>
        /// <param name="h">高</param>
        private void DvideImage(int inIndex,int w,int h)
        {
            if (w < 1 || h < 1) return;

            string clPath = this.listView.Items[inIndex].SubItems[1].Text;
            Image clImageSrc = this.mListImage[inIndex] as Image;

            for(int cy = 0;cy<clImageSrc.Height;cy+=h)
            {
                for(int cx=0;cx<clImageSrc.Width;cx+=w)
                {
                    CutPeace(clImageSrc, new Rectangle(cx,cy,cx+w-1,cy+h-1), clPath);
                }
            }      
        }
        /// <summary>
        /// 指定画像をFormImageCutに送り切出し処理を行う(1部品限定)
        /// </summary>
        /// <param name="inIndex"></param>
        private void CutImage(int inIndex)
        {
            string clPath = this.listView.Items[inIndex].SubItems[1].Text;
            Image clImageSrc = this.mListImage[inIndex] as Image;

            FormImageCut clFormImageCut = new FormImageCut(this.mFormMain, clImageSrc,clPath);
            DialogResult enResult = clFormImageCut.ShowDialog();
            if (enResult == DialogResult.OK)
            {
                //以下、画像切り取り処理
                CutPeace(clImageSrc, clFormImageCut.GetRectangle(), clPath);

                ImageChip ic = new ImageChip();
                ic.Path = clPath;
                ic.FromPngFile(ic.Path,true);

                ic.Rect = clFormImageCut.GetRectangle();
                ic.ImageCut(ic, ic.Rect);
                ClsSystem.ImageMan.AddImageChip(ic);
                this.listView.Items[inIndex+1].Tag = ic.StrMD5;
            }
            clFormImageCut.Dispose();
            clFormImageCut = null;
        }
        //RectAngle Cut:分離:9/19:amami
        private void CutPeace(Image SrcImage,Rectangle rect,string path)
        {
            //以下、画像切り取り処理
            Rectangle stRectSrc = rect;
            Bitmap clImageDst = new Bitmap(stRectSrc.Width, stRectSrc.Height);
            using (Graphics g = Graphics.FromImage(clImageDst))
            {
                Rectangle stRectDst = new Rectangle(0, 0, stRectSrc.Width, stRectSrc.Height);
                g.DrawImage(SrcImage, stRectDst, stRectSrc, GraphicsUnit.Pixel);
            }

            //以下、ハッシュ値を取得する処理
            byte[] pchBuffer = ClsSystem.ImageToByteArray(clImageDst);
            string clMD5 = ClsSystem.GetMD5FromMemory(pchBuffer);

            //以下、アイテム追加処理
            //パスに余計なもの追加しちゃいかんね・・
            //this.AddItem(path + Environment.NewLine + "cut " + stRectSrc.ToString(), clMD5, clImageDst);

            this.AddItem(path, clMD5, clImageDst);

        }

        private void listView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) m_isMouseLDown = true;
            this.mMouseDownPoint = e.Location; //new Point(e.X, e.Y);

            //if (this.mMouseDownPoint == Point.Empty) return;
            //if (e.Button != MouseButtons.Left) return;
            if (this.listView.SelectedIndices.Count!= 1) return;
            ListViewItem clItem = this.listView.GetItemAt(e.X, e.Y);
            if (clItem == null) return; //Item無し
            int inIndex = clItem.Index;
            if (inIndex < 0) return;//やっぱり無し

            int inIndexImage = clItem.ImageIndex;
            if (inIndexImage < 0) return;
            if (inIndexImage >= this.mListImage.Count) return;

            Image clImage = this.mListImage[inIndexImage] as Image;
            if (clImage == null) return;

            //以下、ドラッグ&ドロップ処理を開始する処理
            //bool isSuccess = ClsWin32.ImageList_BeginDrag(this.imageList.Handle, inIndexImage, 64, 64);
            //if (!isSuccess) return;       
            
            //ListViewItem[] pclData = new ListViewItem[1];
            //pclData[0] = this.listView.Items[inIndex];
 //           ListViewItem pclData = listView.Items[inIndex];
 //           this.listView.DoDragDrop(pclData, DragDropEffects.Link); 

//          ClsWin32.ImageList_EndDrag();

//            IntPtr pinDesktopWindow = ClsWin32.GetDesktopWindow();
//            ClsWin32.ImageList_DragEnter(pinDesktopWindow, Cursor.Position.X, Cursor.Position.Y);

        }
        private void listView_MouseMove(object sender, MouseEventArgs e)
        {
            if (mMouseDownPoint != Point.Empty || m_isMouseLDown)
            {
                //マウス左押し下げ中にドラッグ中
                Rectangle dragRegion = new Rectangle(
                    mMouseDownPoint.X - SystemInformation.DragSize.Width / 2,
                    mMouseDownPoint.Y - SystemInformation.DragSize.Height / 2,
                    SystemInformation.DragSize.Width,
                    SystemInformation.DragSize.Height);
                //DragSize外に出たらドラッグ開始とする
                if (!dragRegion.Contains(e.X, e.Y))
                {
                    //ドラッグ開始
                    ListViewItem clItem = this.listView.GetItemAt(mMouseDownPoint.X,mMouseDownPoint.Y);
                    var a = clItem.SubItems[0];
                    if (clItem == null) return; //Item無し
                    this.listView.DoDragDrop(clItem, DragDropEffects.Copy);                
                }
            }
/*
            // Imageの初期化 
            imageList1.Images.Clear(); 
            Rectangle itemRect = lst.GetItemRectangle(itemIndex); 
            imageList1.ImageSize = new Size(itemRect.Width, itemRect.Height); 
 */        
/*
            // 半透明イメージの元画像を作成、ImageListに追加 
            Bitmap.png = new Bitmap(itemRect.Width, itemRect.Height); 
            Graphics g = Graphics.FromImage.png); 
            g.DrawString(itemText, lst.Font, new SolidBrush(lst.ForeColor), 0, 0); 
            imageList1.Images.Add.png);
 */
/*
            // ImageList_BeginDragにはドラッグする
            // イメージの中における相対座標を指定する 
            if (ClsWin32.ImageList_BeginDrag(this.imageList.Handle, inImageIndex, e.X, e.Y)) 
            { 
//              lstSource.DoDragDrop(itemText, DragDropEffects.Copy); 
                ClsWin32.ImageList_EndDrag();
            } 
            mouseDownPoint = Point.Empty; 
 */
        }
        private void listView_MouseUp(object sender, MouseEventArgs e)
        {
            m_isMouseLDown = false;
            if (this.mMouseDownPoint == Point.Empty) return;

            IntPtr pinDesktopWindow = ClsWin32.GetDesktopWindow();
            ClsWin32.ImageList_DragLeave(pinDesktopWindow);
            ClsWin32.ImageList_EndDrag();

            this.mMouseDownPoint = Point.Empty;

            Console.WriteLine("MouseUp");
        }
        private void timerMain_Tick(object sender, EventArgs e)
        {
            if (this.mMouseDownPoint != Point.Empty)
            {
                ClsWin32.ImageList_DragMove(Cursor.Position.X, Cursor.Position.Y);
            }
        }
        private void View_Checked_Changed(object sender, EventArgs e)
        {
            if (checkBox_View.Checked)
            { listView.View = View.LargeIcon ; }
            else
            { listView.View = View.Details; }
        }
        //MyDocumentを開く　9/19:amami
        private void button_OpenMyDoc_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(System.Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments));
        }

        private void FormImageList_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Closeキャンセルして非表示にするだけ
            e.Cancel = true;

            //this.Visible = false; //自身で消さなくても下の操作で消える
            this.mFormMain.checkBox_ImageList.Checked = false;
        }
    }
}
