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
        public bool mSelect;    //選択フラグ
        public string mName;    //名前
        public string mPath;    //ファイルパス
        public Rectangle mRect; //切り取り情報
        public Image Origin;    //オリジナル画像
        public Image Big;       //拡大画像
        public Image Small;     //縮小画像

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClsDatImage()
        {
            this.mSelect = false;
            this.mName = "";
            this.mPath = "";
            this.mRect = new Rectangle();
            this.Origin = null;
            this.Big = null;
            this.Small = null;
        }

        /// <summary>
        /// ファイル名からイメージ情報を登録する処理
        /// </summary>
        /// <param name="clFilePath">イメージファイルパス</param>
        public void SetImageFromFilePath(string clFilePath)
        {
            Image clImage = Bitmap.FromFile(clFilePath);
            this.SetImage(clImage);
        }

        /// <summary>
        /// イメージ情報を登録する処理
        /// </summary>
        /// <param name="clImage">オリジナル画像</param>
        public void SetImage(Image clImage)
        {
            this.Origin = clImage;
            ClsDatImage.CreateImage(clImage, ref this.Big, ref this.Small);
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        public void Remove()
        {
            if (this.Origin != null)
            {
                this.Origin.Dispose();
                this.Origin = null;
            }

            if (this.Big != null)
            {
                this.Big.Dispose();
                this.Big = null;
            }

            if (this.Small != null)
            {
                this.Small.Dispose();
                this.Small = null;
            }
        }

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
                if ("Name".Equals(clNode.Name))
                {
                    this.mName = clNode.InnerText;
                    continue;
                }

                if ("Path".Equals(clNode.Name))
                {
                    this.mPath = clNode.InnerText;
                    continue;
                }

                if ("Rect".Equals(clNode.Name))
                {
                    this.mRect = ClsTool.GetRectFromXmlNode(clNode);
                    continue;
                }

                throw new Exception("this is not normal Image. Image Path=" + this.mPath);
            }

            //以下、イメージ復元処理
            //this.SetImage(Image clImage);

            //以下、ハッシュ値設定

        }

        /// <summary>
        /// 保存処理
        /// </summary>
        /// <param name="clHeader">ヘッダー</param>
        public void Save(string clHeader)
        {
            //以下、イメージ保存処理
            ClsTool.AppendElementStart(clHeader, "Image");
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Name", this.mName);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Path", this.mPath);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Rect", this.mRect);
            ClsTool.AppendElementEnd(clHeader, "Image");
        }
    }
}
