using System.Drawing;
using System.Xml;
using System.Drawing.Imaging;
using Tao.OpenGl;

namespace PrjHikariwoAnim
{
    public class ClsDatImage
    {
        public int mID;             //ランダム値（ClsSystem.mDicImageのキー）
        public bool mSelect;        //選択フラグ
        public string mPath;        //ファイルパス（画像 or カット画像の場合は元画像パス）
        public ClsDatRect mRect;    //切り取り情報（null:元画像 null以外:カット画像）
        public Image mImgOrigin;    //オリジナル画像
        public Image mImgSmall;     //縮小画像
        public uint[] mListTex;     //OpenGLで管理するイメージ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClsDatImage()
        {
            this.mSelect = false;
            this.mPath = null;
            this.mRect = null;
            this.mImgOrigin = null;
            this.mImgSmall = null;
            this.mListTex = new uint[6];
        }

        /// <summary>
        /// ファイル名からイメージ情報を登録する処理
        /// </summary>
        /// <param name="clFilePath">イメージファイルパス</param>
        public void SetImageFromFilePath(string clFilePath)
        {
            this.mPath = clFilePath;
            Image clImage = Bitmap.FromFile(clFilePath);
            this.SetImage(clImage);
        }

        /// <summary>
        /// イメージ情報を登録する処理
        /// </summary>
        /// <param name="clImage">オリジナル画像</param>
        public void SetImage(Image clImage)
        {
            this.mImgOrigin = clImage;
            this.CreateImage(clImage, ref this.mImgSmall);
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        public void Remove()
        {
            if (this.mImgOrigin != null)
            {
                this.mImgOrigin.Dispose();
                this.mImgOrigin = null;
            }

            if (this.mImgSmall != null)
            {
                this.mImgSmall.Dispose();
                this.mImgSmall = null;
            }
        }

        /// <summary>
        /// イメージ作成処理
        /// </summary>
        /// <param name="clImageSrc">オリジナル画像</param>
        /// <param name="clImageSmall">縮小画像</param>
        private void CreateImage(Image clImageSrc, ref Image clImageSmall)
        {
            Rectangle stRectSrc = new Rectangle(0, 0, clImageSrc.Width, clImageSrc.Height);

            //以下、縮小画像作成処理
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

            //以下、OpenGL用の画像を作成する処理
            Bitmap clBitmap = (Bitmap)clImageSrc.Clone();
            clBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Rectangle stRect = new Rectangle(0, 0, clBitmap.Width, clBitmap.Height);
            BitmapData clBitmapData = clBitmap.LockBits(stRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Gl.glGenTextures(1, this.mListTex);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, this.mListTex[0]);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA8, clBitmap.Width, clBitmap.Height, 0, Gl.GL_BGRA, Gl.GL_UNSIGNED_BYTE, clBitmapData.Scan0);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
        }

        /// <summary>
        /// 読み込み処理
        /// </summary>
        /// <param name="clXmlElem">xmlエレメント</param>
        public void Load(XmlElement clXmlElem)
        {
            XmlNodeList clListNode = clXmlElem.ChildNodes;
            this.mID = ClsTool.GetIntFromXmlNodeList(clListNode, "ID");
            this.mPath = ClsTool.GetStringFromXmlNodeList(clListNode, "Path");

            //以下、各管理クラス作成処理
            foreach (XmlNode clNode in clListNode)
            {
                if ("Rect".Equals(clNode.Name))
                {
                    this.mRect = new ClsDatRect();
                    this.mRect.Load(clNode);
                    continue;
                }
            }

            //以下、イメージ復元処理
            Bitmap clImage = (Bitmap)Bitmap.FromFile(this.mPath);
            if (this.mRect != null)
            {
                //以下、イメージカット処理
                Rectangle stRect = new Rectangle(this.mRect.mX, this.mRect.mY, this.mRect.mW, this.mRect.mH);
                clImage = clImage.Clone(stRect, clImage.PixelFormat);
            }
            this.SetImage(clImage);
        }

        /// <summary>
        /// 保存処理
        /// </summary>
        /// <param name="clHeader">ヘッダー</param>
        public void Save(string clHeader)
        {
            //以下、イメージ保存処理
            ClsTool.AppendElementStart(clHeader, "Image");
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "ID", this.mID);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Path", this.mPath);
            if (this.mRect != null)
            {
                this.mRect.Save(clHeader + ClsSystem.FILE_TAG);
            }
            ClsTool.AppendElementEnd(clHeader, "Image");
        }
    }
}
