using System;
using System.Collections.Generic;
using System.Xml;

namespace PrjHikariwoAnim
{
    [Serializable]
    public class ClsDatKeyFrame
    {
        public int mFrameNo;            //フレームNo
        public ClsDatTween mTween;      //トゥイーン管理クラス
        public TYPE_OPTION mTypeOption; //オプションタイプ
        public object mValue1;          //値（何の値かはタイプに依存する）
        public object mValue2;          //値（何の値かはタイプに依存する）

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="enTypeOption">オプションタイプ</param>
        /// <param name="inFrame">フレームNo</param>
        /// <param name="clValue1">値１</param>
        /// <param name="clValue2">値２</param>
        public ClsDatKeyFrame(TYPE_OPTION enTypeOption, int inFrameNo, object clValue1, object clValue2)
        {
            this.mTypeOption = enTypeOption;
            this.mFrameNo = inFrameNo;
            this.mValue1 = clValue1;
            this.mValue2 = clValue2;
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
            this.mFrameNo = ClsTool.GetIntFromXmlNodeList(clListNode, "Frame");

            string clValue1 = ClsTool.GetStringFromXmlNodeList(clListNode, "Value1");
            string clValue2 = ClsTool.GetStringFromXmlNodeList(clListNode, "Value2");
            switch (this.mTypeOption)
            {
            case TYPE_OPTION.NONE:
                this.mValue1 = null;
                break;
            case TYPE_OPTION.DISPLAY:
                this.mValue1 = Convert.ToBoolean(clValue1);
                break;
            case TYPE_OPTION.FLIP:
                this.mValue1 = Convert.ToBoolean(clValue1);
                this.mValue2 = Convert.ToBoolean(clValue2);
                break;
            case TYPE_OPTION.POSITION:
            case TYPE_OPTION.ROTATION:
            case TYPE_OPTION.SCALE:
            case TYPE_OPTION.OFFSET:
                this.mValue1 = Convert.ToSingle(clValue1);
                this.mValue2 = Convert.ToSingle(clValue2);
                break;
            case TYPE_OPTION.TRANSPARENCY:
                this.mValue1 = Convert.ToSingle(clValue1);
                break;
            case TYPE_OPTION.COLOR:
                this.mValue1 = Convert.ToInt32(clValue1);
                break;
            case TYPE_OPTION.USER_DATA:
                this.mValue1 = clValue1;
                break;
            }

            //以下、各管理クラス作成処理
            foreach (XmlNode clNode in clListNode)
            {
                if ("Tween".Equals(clNode.Name))
                {
                    ClsDatTween clDatTween = new ClsDatTween();
                    clDatTween.Load(clNode);

                    this.mTween = clDatTween;
                    continue;
                }
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
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Frame", this.mFrameNo);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Value1", this.mValue1.ToString());
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Value2", this.mValue1.ToString());

            //以下、トゥイーン保存処理
            if (this.mTween != null)
            {
                this.mTween.Save(clHeader + ClsSystem.FILE_TAG);
            }

            ClsTool.AppendElementEnd(clHeader, "KeyFrame");
        }
    }
}
