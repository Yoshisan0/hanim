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
        //このフォームはセル画像情報を管理しとりだすだけで
        //操作等はしないほうがいいかもしれないので少し考え直し

        
        public ImageManagerBase IM ;

        private int mTumsSize=64;

        private FormMain mFormMain;
        private Point mMouseDownPoint = Point.Empty; //ドラックドロップ開始点
        private bool m_isMouseLDown;    //左クリック押し下げ中

        public FormCell(FormMain form)
        {
            InitializeComponent();
            IM = new ImageManagerBase();
            mFormMain = form;
        }

        private void CloseWindow_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void FormCell_DragEnter(object sender, DragEventArgs e)
        {
            //受け入れ準備
            //File
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            /*
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
            */
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
        private void FormCell_DragDrop(object sender, DragEventArgs e)
        {
            //File受け入れ
            bool isSuccess = false;
            string[] pclAllPaths = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string clPathTmp1 in pclAllPaths)
            {
                if (Directory.Exists(clPathTmp1))
                {
                    string[] pclFilePaths = Directory.GetFiles(clPathTmp1, "*.*", SearchOption.AllDirectories);
                    foreach (string clPathTmp2 in pclFilePaths)
                    {
                        bool isChk = this.ChkImageFile(clPathTmp2);
                        if (!isChk) continue;

                       IM.SetImage (clPathTmp2);
                        isSuccess = true;
                    }
                }
                else if (File.Exists(clPathTmp1))
                {
                    bool isChk = this.ChkImageFile(clPathTmp1);
                    if (!isChk) continue;

                    IM.SetImage(clPathTmp1);
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

        private void FormCell_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) m_isMouseLDown = true;
            this.mMouseDownPoint = e.Location; //new Point(e.X, e.Y);
        }

        private void FormCell_MouseMove(object sender, MouseEventArgs e)
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
                    int sellectIndex = (e.Y / mTumsSize);
                    if (sellectIndex < IM.CellList.Count)
                    {                        
                        panel2.DoDragDrop(IM.CellList[sellectIndex], DragDropEffects.Copy);
                    }
                }
            }
        }

        private void addImageFile()
        {
         
        }
        private void addCell()
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //DrawCells自力描画でがんばる
            //Selectedは枠色で表現?網かけ？
            int cnt = 0;//初期インデックス
            int bSize = mTumsSize;//BoxSize
            int drawPos = 0;//描画横幅累計 コンポーネント横幅まで描画

            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            //サムネイル表示はクリックセレクトの関係から縦横固定サイズが望ましい

            if (IM.CellList.Count <= 0) return;

            while (drawPos < panel2.Height && cnt < IM.CellList.Count)
            {
                Image src = IM.CellList[cnt].Img;
                if (src == null)
                {
                    Console.Out.Write("CellImage is Null");
                    return;
                }

                float rw = bSize / src.Width;
                float rh = bSize / src.Height;
                float rf = Math.Min(rw, rh);
                Point ds = new Point((int)(src.Width * rf), (int)(src.Height * rf));

                e.Graphics.DrawImage(src,(bSize / 2 - (ds.X / 2)),drawPos + (bSize / 2 - (ds.Y / 2)), ds.X, ds.Y);

                //DrawFlame
                if (IM.CellList[cnt].Selected)
                {
                    e.Graphics.DrawRectangle(Pens.GreenYellow, new Rectangle(0,drawPos, bSize - 1, bSize - 1));
                }
                else
                {
                    e.Graphics.DrawRectangle(Pens.Brown, new Rectangle(0,drawPos, bSize - 1, bSize - 1));
                }
                drawPos += bSize;
                cnt++;
            }
        }

        private void FormCell_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
            mFormMain.checkBox_CellList.Checked = false;            
        }
    }
}
