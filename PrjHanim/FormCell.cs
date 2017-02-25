using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PrjHikariwoAnim
{
    public partial class FormCell : Form
    {        
        private int mTumsSize=64;//Thumbnailサイズ
        private FormMain mFormMain;
        private Point mMouseDownPoint = Point.Empty; //ドラックドロップ開始点
        private bool m_isMouseLDown;    //左クリック押し下げ中
        public int mSelectIndex;

        public FormCell(FormMain form)
        {
            InitializeComponent();
            mFormMain = form;
        }
        private void FormCell_Load(object sender, EventArgs e)
        {
            //以下、ウィンドウの設定
            this.Location = ClsSystem.mSetting.mWindowCell.mLocation;
            this.Size = ClsSystem.mSetting.mWindowCell.mSize;
        }
        private void FormCell_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Closeキャンセルして非表示にするだけ
            e.Cancel = true;

            //this.Visible = false; //自身で消さなくても下の操作で消える
            this.mFormMain.checkBox_CellList.Checked = false;
        }
        private void FormCell_Resize(object sender, EventArgs e)
        {
            panel_list.Width = panel_listBase.Width;
            panel_list.Height = mTumsSize * (ClsSystem.mListImage.Count / (panel_list.Width / mTumsSize));
            if (panel_list.Height < panel_listBase.Height) panel_list.Height = panel_listBase.Height;
            panel_listBase.Refresh();
        }
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

        private void panel_list_DragEnter(object sender, DragEventArgs e)
        {
            //受け入れ準備
            //File
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void panel_list_DragDrop(object sender, DragEventArgs e)
        {
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
                        ClsSystem.CreateImageFromFile(str);

                        //ImageListへ登録と更新
                        //CellListの表示更新
                        Refresh();
                    }
                }

                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void panel_list_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_isMouseLDown = true;
            }
            this.mMouseDownPoint = new Point(e.X,e.Y); //new Point(e.X, e.Y);
        }
        private void panel_list_MouseUp(object sender, MouseEventArgs e)
        {
            m_isMouseLDown = false;
            mMouseDownPoint = Point.Empty;
        }
        private void panel_list_MouseMove(object sender, MouseEventArgs e)
        {
            if (mMouseDownPoint != Point.Empty && m_isMouseLDown)
            {
                //マウス左押し下げ中にドラッグ中
                Rectangle dragRegion = new Rectangle(
                    mMouseDownPoint.X - SystemInformation.DragSize.Width / 2,
                    mMouseDownPoint.Y - SystemInformation.DragSize.Height / 2,
                    SystemInformation.DragSize.Width,
                    SystemInformation.DragSize.Height);

                //DragSize外に出たらドラッグ開始とする
                if (!dragRegion.Contains(e.X,e.Y))
                {
                    //ドラッグ開始
                    int selectIndex = PointToItemIndex(e.X , e.Y );
                    if (selectIndex >=0 && selectIndex < ClsSystem.mListImage.Count)
                    {
                        DragDropEffects rete = panel_listBase.DoDragDrop(ClsSystem.mListImage[selectIndex], DragDropEffects.Copy);
                        if(rete==DragDropEffects.Copy)
                        {
                            //正常完了
                            m_isMouseLDown = false;
                        }
                    }
                }
            }                        
        }

        private void panel_list_Paint(object sender, PaintEventArgs e)
        {
            //DrawCells自力描画でがんばる
            //Selectedは枠色で表現?網かけ？
            int cnt = 0;//初期インデックス
            int bSize = mTumsSize;//BoxSize
            int dpX = 0;//DrawPosX
            int dpY = 0;//DrawPosY
            int cx = panel_listBase.Width / mTumsSize;//横並び数

            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            //サムネイル表示はクリックセレクトの関係から縦横固定サイズが望ましい

            if (ClsSystem.mListImage.Count <= 0) return;

            while (dpY < panel_listBase.Height && cnt < ClsSystem.mListImage.Count)
            {
                Image src = ClsSystem.mListImage[cnt].Origin;
                if (src == null)
                {
                    Console.Out.Write("CellImage is Null");
                    return;
                }
                //Tumbサイズ
                float rw = (float)bSize / src.Width;
                float rh = (float)bSize / src.Height;
                float rf = Math.Min(rw, rh);
                Point ds = new Point((int)(src.Width * rf), (int)(src.Height * rf));
                
                //
                e.Graphics.DrawImage(src, dpX+(bSize / 2 - (ds.X / 2)), dpY + (bSize / 2 - (ds.Y / 2)), ds.X, ds.Y);

                //DrawFlame
                if (ClsSystem.mListImage[cnt].mSelect)
                {
                    e.Graphics.DrawRectangle(Pens.GreenYellow, new Rectangle(dpX,dpY, bSize - 1, bSize - 1));
                }
                else
                {
                    e.Graphics.DrawRectangle(Pens.Brown, new Rectangle(dpX,dpY, bSize - 1, bSize - 1));
                }
                cnt++;
                dpX = (cnt%cx) * bSize;                
                dpY = (cnt/cx) * bSize;
            }
            e.Graphics.Dispose();
        }
        private void panel_list_Click(object sender, EventArgs e)
        {
            //Cell Select
            int sel = PointToItemIndex(mMouseDownPoint.X, mMouseDownPoint.Y);
            if (sel >= 0 && sel < ClsSystem.mListImage.Count)
            {
                ClsSystem.mListImage[sel].mSelect = !ClsSystem.mListImage[sel].mSelect;
                panel_listBase.Refresh();
            }
        }
        private void panel_list_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Call ImageCuter
            int inIndex = PointToItemIndex(e.X, e.Y);
            ClsDatImage ic = ClsSystem.mListImage[inIndex];

            FormImageCut fic = new FormImageCut(this.mFormMain, ic.Origin, ic.mPath);
            if (fic.ShowDialog() == DialogResult.OK)
            {
                
            }

            panel_list.Refresh();
        }
        private int PointToItemIndex(int x,int y)
        {
            int cx = x / mTumsSize;
            int cy = y / mTumsSize;
            return (cy * (panel_list.Width / mTumsSize)) + cx;
        }

        private void button_LoadPic_Click(object sender, EventArgs e)
        {
            //LoadImage
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "png";
            ofd.Multiselect = true;
            ofd.Filter = "png|*.png";
            ofd.InitialDirectory = ClsSystem.mSetting.mLastImageDirectory;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string fn in ofd.FileNames)
                {
                    ClsSystem.CreateImageFromFile(fn);
                }
            }
            ofd.Dispose();

            panel_list.Refresh();
        }

        private void button_OpenDoc_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        private void button_Cut_Click(object sender, EventArgs e)
        {
            List<int> clListIndex = ClsSystem.GetImageSelectIndex();
            if (clListIndex.Count<= 0) return;

            int inIndex = clListIndex[0];
            ClsDatImage clDatImage = ClsSystem.mListImage[inIndex];

            //以下、イメージカットウィンドウ表示処理
            FormImageCut clFormImageCut = new FormImageCut(this.mFormMain, clDatImage.Origin, clDatImage.mPath);
            DialogResult enResult = clFormImageCut.ShowDialog();
            if (enResult == DialogResult.OK)
            {
                int inCnt, inMax = clFormImageCut.mListCutImage.Count;
                for (inCnt = 0; inCnt < inMax; inCnt++)
                {
                    //以下、画像登録処理
                    ClsDatCutImage clDatCutImage = clFormImageCut.mListCutImage[inCnt];
                    ClsSystem.CreateImageFromImage(clDatCutImage.mImage);
                }
            }
            clFormImageCut.Dispose();
            clFormImageCut = null;

            //以下、リフレッシュ処理
            this.Refresh();
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            List<string> clListRemove = new List<string>();
            int inCnt, inMax = ClsSystem.mListImage.Count;
            for (inCnt= inMax- 1; inCnt>= 0;inCnt--)    //複数削除の可能性があるので、後ろからなめる
            {
                ClsDatImage clDatImage = ClsSystem.mListImage[inCnt];
                if (!clDatImage.mSelect) continue;

                clDatImage.Remove();
                ClsSystem.mListImage.RemoveAt(inCnt);
            }

            panel_listBase.Refresh();
        }
    }
}
