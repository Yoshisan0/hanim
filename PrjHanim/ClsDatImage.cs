using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;

namespace PrjHikariwoAnim
{
    public class ClsDatImage
    {
        public int mID;             //ランダム値（ClsSystem.mDicImageのキー）
        public bool mSelect;        //選択フラグ
        public string mPath;        //ファイルパス（画像 or カット画像の場合は元画像パス）
        public ClsDatRect mRect;    //切り取り情報（null:元画像 null以外:カット画像）
        public Image mImgOrigin;    //オリジナル画像
        public Image mImgBig;       //拡大画像
        public Image mImgSmall;     //縮小画像

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClsDatImage()
        {
            this.mSelect = false;
            this.mPath = null;
            this.mRect = null;
            this.mImgOrigin = null;
            this.mImgBig = null;
            this.mImgSmall = null;
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
            ClsDatImage.CreateImage(clImage, ref this.mImgBig, ref this.mImgSmall);
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

            if (this.mImgBig != null)
            {
                this.mImgBig.Dispose();
                this.mImgBig = null;
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
        /// <param name="clImageBig">拡大画像</param>
        /// <param name="clImageSmall">縮小画像</param>
        private static void CreateImage(Image clImageSrc, ref Image clImageBig, ref Image clImageSmall)
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
        /// 読み込み処理
        /// </summary>
        /// <param name="clXmlElem">xmlエレメント</param>
        public void Load(XmlElement clXmlElem)
        {
            XmlNodeList clListNode = clXmlElem.ChildNodes;
            foreach (XmlNode clNode in clListNode)
            {
                if ("ID".Equals(clNode.Name))
                {
                    this.mID = Convert.ToInt32(clNode.InnerText);
                    continue;
                }

                if ("Path".Equals(clNode.Name))
                {
                    this.mPath = clNode.InnerText;
                    continue;
                }

                if ("Rect".Equals(clNode.Name))
                {
                    this.mRect = new ClsDatRect();
                    this.mRect.Load(clNode);
                    continue;
                }

                throw new Exception("this is not normal Image. Image Path=" + this.mPath);
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
