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
        
        public ImageManagerBase ImageMan ;

        private int mTumsSize=64;//Thumbnailサイズ
        private FormMain mFormMain;
        private Point mMouseDownPoint = Point.Empty; //ドラックドロップ開始点
        private bool m_isMouseLDown;    //左クリック押し下げ中

        public FormCell(FormMain form)
        {
            InitializeComponent();
            ImageMan = new ImageManagerBase();
            mFormMain = form;
        }
        private void FormCell_DragEnter(object sender, DragEventArgs e)
        {
            //受け入れ準備
            //File
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
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
                        //ImageListへ登録と更新
                        //CellListの表示更新
                        Refresh();
                    }
                }
                e.Effect = DragDropEffects.Copy;
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
                    if (sellectIndex < ImageMan.CellList.Count)
                    {                        
                        panel2.DoDragDrop(ImageMan.CellList[sellectIndex], DragDropEffects.Copy);
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

            if (ImageMan.CellList.Count <= 0) return;

            while (drawPos < panel2.Height && cnt < ImageMan.CellList.Count)
            {
                Image src = ImageMan.CellList[cnt].Img;
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
                if (ImageMan.CellList[cnt].Selected)
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
            //Closeキャンセルして非表示にするだけ
            e.Cancel = true;

            //this.Visible = false; //自身で消さなくても下の操作で消える
            this.mFormMain.checkBox_CellList.Checked = false;
        }
    }
}
