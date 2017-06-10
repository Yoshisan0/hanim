using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace PrjHikariwoAnim
{
    [Serializable]
    public class ClsDatTween
    {
        //TweenのParamなので所在を明確にする為ここに移動しました amami 11/27
        //どのパラメータに対してかの指定
        //UserDataはStringやで
        public enum EnmParam    //オプションにもあるから、それ使った方が良いかも？ coment by yoshi 2017/01/08
        {
            NONE = 0,

            POSITION_X,
            POSITION_Y,
            ROTATION,
            SCALE_X,
            SCALE_Y,
            TRANS,
            FLIP_H,
            FLIP_V,
            VISIBLE,
            COLOR,
            OFFSET_X,
            OFFSET_Y,
            USERDATA,
        }

        public EnmParam mParam;
        public int mLength;         //継続フレーム数
        public ClsVector3 mPos;
        public Image mImage;
        public List<ClsVector3> mListVec;

        //シリアライザ用
        public ClsDatTween() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="enParam">種別</param>
        /// <param name="inFrmEnd">終了フレーム</param>
        /// <param name="clPos">座標</param>
        /// <param name="pclVec">各ベクトル</param>
        public ClsDatTween(EnmParam enParam, int inLength, ClsVector3 clPos, List<ClsVector3> pclVec)
        {
            this.mParam = enParam;
            this.mLength = inLength;
            this.mPos = new ClsVector3(clPos.X, clPos.Y, 0.0f);

            this.mListVec = new List<ClsVector3>();
            int inCnt;
            for (inCnt = 0; inCnt < 3; inCnt++)
            {
                ClsVector3 clVec = new ClsVector3(pclVec[inCnt].X, pclVec[inCnt].Y, 0.0f);
                this.mListVec.Add(clVec);
            }

            this.mImage = FormRateGraph.CreateImage(this, 15, 15);
        }

        public void RemoveAll()
        {
            if (this.mListVec != null)
            {
                this.mListVec.Clear();
                this.mListVec = null;
            }
        }

        /// <summary>
        /// クローン処理
        /// </summary>
        /// <returns>トゥイーン情報</returns>
        public ClsDatTween Clone()
        {
            ClsDatTween clTween = new ClsDatTween(this.mParam, this.mLength, this.mPos, this.mListVec);
            return (clTween);
        }

        /// <summary>
        /// 読み込み処理
        /// </summary>
        /// <param name="clXmlNode">xmlノード</param>
        public void Load(XmlNode clXmlNode)
        {
            XmlNodeList clListNode = clXmlNode.ChildNodes;
            string clParam = ClsTool.GetStringFromXmlNodeList(clListNode, "Param");
            this.mParam = (EnmParam)Enum.Parse(typeof(EnmParam), clParam);
            this.mLength = ClsTool.GetIntFromXmlNodeList(clListNode, "Length");
            this.mPos = ClsTool.GetVecFromXmlNodeList(clListNode, "Pos");

            //以下、各管理クラス作成処理
            foreach (XmlNode clNode in clListNode)
            {
                if ("Vec".Equals(clNode.Name))
                {
                    ClsVector3 clVec = ClsTool.GetVecFromXmlNode(clNode);
                    this.mListVec.Add(clVec);
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
            //以下、トゥイーン保存処理
            ClsTool.AppendElementStart(clHeader, "Tween");
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Param", this.mParam.ToString());
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Length", this.mLength);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Pos", this.mPos);

            //以下、ベクトルリスト保存処理
            int inMax = this.mListVec.Count;
            if (inMax >= 1)
            {
                int inCnt;
                for (inCnt = 0; inCnt < inMax; inCnt++)
                {
                    ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Vec", this.mListVec[inCnt]);
                }
            }

            ClsTool.AppendElementEnd(clHeader, "Tween");
        }

        public void Load()
        {
        }
    }
}
