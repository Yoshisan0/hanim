using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PrjHikariwoAnim
{
    [Serializable]
    public class ClsDatKeyFrame
    {
        public int mFrame;          //フレームNo
        public ClsDatTween mTween;  //トゥイーン管理クラス

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="inFrame">フレームNo</param>
        public ClsDatKeyFrame(int inFrame)
        {
            this.mFrame = inFrame;
        }

        /// <summary>
        /// キーフレームの全てを削除する処理
        /// </summary>
        public void RemoveAll()
        {
            //以下、トゥイーン全削除処理
            if (this.mTween != null)
            {
                this.mTween.RemoveAll();
                this.mTween = null;
            }
        }

        /// <summary>
        /// エクスポート
        /// </summary>
        /// <returns>出力情報</returns>
        public Dictionary<string, object> Export()
        {
            return (null);
        }

        /// <summary>
        /// 読み込み処理
        /// </summary>
        /// <param name="clXmlNode">xmlノード</param>
        public void Load(XmlNode clXmlNode)
        {
            XmlNodeList clListNode = clXmlNode.ChildNodes;
            foreach (XmlNode clNode in clListNode)
            {
                if ("Frame".Equals(clNode.Name))
                {
                    this.mFrame = Convert.ToInt32(clNode.InnerText);
                    continue;
                }

                if ("Tween".Equals(clNode.Name))
                {
                    ClsDatTween clDatTween = new ClsDatTween();
                    clDatTween.Load(clNode);

                    this.mTween = clDatTween;
                    continue;
                }

                throw new Exception("this is not normal KeyFrame.");
            }
        }

        /// <summary>
        /// 保存処理
        /// </summary>
        /// <param name="clHeader">ヘッダー</param>
        public void Save(string clHeader)
        {
            //以下、キーフレーム保存処理
            ClsTool.AppendElementStart(clHeader, "KeyFrame");
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Frame", this.mFrame);

            //以下、トゥイーン保存処理
            if (this.mTween != null)
            {
                this.mTween.Save(clHeader + ClsSystem.FILE_TAG);
            }

            ClsTool.AppendElementEnd(clHeader, "KeyFrame");
        }
    }
}
