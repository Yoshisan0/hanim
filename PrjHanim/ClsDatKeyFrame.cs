using System;
using System.Collections.Generic;
using System.Xml;

namespace PrjHikariwoAnim
{
    [Serializable]
    public class ClsDatKeyFrame
    {
        public int mFrameNo;                //フレームNo
        public bool mParentFlag;            //親の設定に依存するかどうかのフラグ
        public EnmTypeOption mTypeOption;   //オプションタイプ
        public object mValue1;              //値（何の値かはタイプに依存する　Ｘ座標など）
        public object mValue2;              //値（何の値かはタイプに依存する　Ｙ座標など）
        public ClsDatTween mTween1;         //トゥイーン管理クラス
        public ClsDatTween mTween2;         //トゥイーン管理クラス

        /*
        //以下、表示設定
        EnmTypeOption.DISPLAY
        mValue1 = clParam.mDisplay
        mValue2 = null

        //以下、座標設定
        EnmTypeOption.POSITION
        mValue1 = clParam.mX
        mValue2 = clParam.mY

        //以下、回転設定
        EnmTypeOption.ROTATION
        mValue1 = clParam.mRZ
        mValue2 = null

        //以下、スケール設定
        EnmTypeOption.SCALE
        mValue1 = clParam.mSX
        mValue2 = clParam.mSY

        //以下、オフセット設定
        EnmTypeOption.OFFSET
        mValue1 = clParam.mCX
        mValue2 = clParam.mCY

        //以下、反転設定
        EnmTypeOption.FLIP
        mValue1 = clParam.mFlipH
        mValue2 = clParam.mFlipV

        //以下、透明設定
        EnmTypeOption.TRANSPARENCY
        mValue1 = clParam.mTrans
        mValue2 = null

        //以下、カラー設定 
        EnmTypeOption.COLOR
        mValue1 = clParam.mColor
        mValue2 = null

        //以下、ユーザーデータ設定 
        EnmTypeOption.USER_DATA
        mValue1 = clParam.mUserData
        mValue2 = null
        */


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="enTypeOption">オプションタイプ</param>
        /// <param name="inFrame">フレームNo</param>
        /// <param name="isParentFlag">親の設定に依存するかどうか</param>
        /// <param name="clValue1">値１</param>
        /// <param name="clValue2">値２</param>
        /// <param name="clTween1">トゥイーン１</param>
        /// <param name="clTween2">トゥイーン２</param>
        public ClsDatKeyFrame(EnmTypeOption enTypeOption, int inFrameNo, bool isParentFlag, object clValue1, object clValue2, ClsDatTween clTween1, ClsDatTween clTween2)
        {
            this.mFrameNo = inFrameNo;
            this.mParentFlag = isParentFlag;
            this.mTypeOption = enTypeOption;
            this.mTween1 = clTween1;
            this.mTween2 = clTween2;
            this.mValue1 = clValue1;
            this.mValue2 = clValue2;
        }

        /// <summary>
        /// キーフレームの全てを削除する処理
        /// </summary>
        public void RemoveAll()
        {
            //以下、トゥイーン全削除処理
            if (this.mTween1 != null)
            {
                this.mTween1.RemoveAll();
                this.mTween1 = null;
            }

            if (this.mTween2 != null)
            {
                this.mTween2.RemoveAll();
                this.mTween2 = null;
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
            case EnmTypeOption.NONE:
                this.mValue1 = null;
                break;
            case EnmTypeOption.DISPLAY:
                this.mValue1 = Convert.ToBoolean(clValue1);
                break;
            case EnmTypeOption.FLIP:
                this.mValue1 = Convert.ToBoolean(clValue1);
                this.mValue2 = Convert.ToBoolean(clValue2);
                break;
            case EnmTypeOption.POSITION:
            case EnmTypeOption.ROTATION:
            case EnmTypeOption.SCALE:
            case EnmTypeOption.OFFSET:
                this.mValue1 = Convert.ToSingle(clValue1);
                this.mValue2 = Convert.ToSingle(clValue2);
                break;
            case EnmTypeOption.TRANSPARENCY:
                this.mValue1 = Convert.ToSingle(clValue1);
                break;
            case EnmTypeOption.COLOR:
                this.mValue1 = Convert.ToInt32(clValue1);
                break;
            case EnmTypeOption.USER_DATA:
                this.mValue1 = clValue1;
                break;
            }

            //以下、各管理クラス作成処理
            foreach (XmlNode clNode in clListNode)
            {
                if ("Tween1".Equals(clNode.Name))
                {
                    ClsDatTween clDatTween = new ClsDatTween(EnmParam.NONE, 0, null, null);
                    clDatTween.Load(clNode);

                    this.mTween1 = clDatTween;
                    continue;
                }

                if ("Tween2".Equals(clNode.Name))
                {
                    ClsDatTween clDatTween = new ClsDatTween(EnmParam.NONE, 0, null, null);
                    clDatTween.Load(clNode);

                    this.mTween2 = clDatTween;
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
            if (this.mTween1 != null)
            {
                this.mTween1.Save("Tween1", clHeader + ClsSystem.FILE_TAG);
            }
            if (this.mTween2 != null)
            {
                this.mTween2.Save("Tween2", clHeader + ClsSystem.FILE_TAG);
            }

            ClsTool.AppendElementEnd(clHeader, "KeyFrame");
        }
    }
}
