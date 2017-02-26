using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PrjHikariwoAnim
{
    public class ClsDatRect
    {
        public int mX;
        public int mY;
        public int mW;
        public int mH;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClsDatRect()
        {
            this.mX = 0;
            this.mY = 0;
            this.mW = 0;
            this.mH = 0;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="inX">Ｘ座標</param>
        /// <param name="inY">Ｙ座標</param>
        /// <param name="inW">幅</param>
        /// <param name="inH">高さ</param>
        public ClsDatRect(int inX, int inY, int inW, int inH)
        {
            this.mX = inX;
            this.mY = inY;
            this.mW = inW;
            this.mH = inH;
        }

        /// <summary>
        /// 読み込み処理
        /// </summary>
        /// <param name="clXmlElem">xmlエレメント</param>
        public void Load(XmlNode clXmlElem)
        {
            XmlNodeList clListNode = clXmlElem.ChildNodes;
            foreach (XmlNode clNode in clListNode)
            {
                if ("X".Equals(clNode.Name))
                {
                    this.mX = Convert.ToInt32(clNode.InnerText);
                    continue;
                }

                if ("Y".Equals(clNode.Name))
                {
                    this.mY = Convert.ToInt32(clNode.InnerText);
                    continue;
                }

                if ("W".Equals(clNode.Name))
                {
                    this.mW = Convert.ToInt32(clNode.InnerText);
                    continue;
                }

                if ("H".Equals(clNode.Name))
                {
                    this.mH = Convert.ToInt32(clNode.InnerText);
                    continue;
                }

                throw new Exception("this is not normal Rect.");
            }
        }

        /// <summary>
        /// 保存処理
        /// </summary>
        /// <param name="clHeader">ヘッダー</param>
        public void Save(string clHeader)
        {
            //以下、イメージ保存処理
            ClsTool.AppendElementStart(clHeader, "Rect");
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "X", this.mX);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Y", this.mY);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "W", this.mW);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "H", this.mH);
            ClsTool.AppendElementEnd(clHeader, "Rect");
        }
    }
}
