using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    //とりあえず作成したバージョン

    public class ClsDatMotion
    {
        public string mName;    //モーション名
        public int mElemSelectKey;          //現在編集中のエレメントキー（TreeNodeのハッシュコード）
        public List<ClsDatElem> mListElem;  //エレメント管理クラスのリスト

        public ClsDatMotion(string clName)
        {
            this.mName = clName;
            this.mElemSelectKey = -1;
            this.mListElem = new List<ClsDatElem>();
        }

        public Dictionary<string, object> Export()
        {
            /*
            Dictionary<string, object> clDic = new Dictionary<string, object>();
            int inCnt, inMax = this.mFrame.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ELEMENTS clElement = this.mFrame[inCnt];
                clDic["elm_" + inCnt] = clElement.Export();
            }
            clDic["idx"] = (this.ActiveIndex == null) ? 0 : this.ActiveIndex;
            clDic["txt"] = (this.Text == null) ? "" : this.Text;
            clDic["type"] = this.Type.ToString();
            clDic["num"] = this.FrameNum;
            if (this.mTween != null)
            {
                byte[] puchData = FormRateGraph.CreateSaveData(this.mTween);
                clDic["twn"] = Convert.ToBase64String(puchData);
            }

            return (clDic);
            */

            return (null);
        }

        /// <summary>
        /// モーション名設定処理
        /// </summary>
        /// <param name="clName">モーション名</param>
        public void SetName(string clName)
        {
            this.mName = clName;
        }

        /// <summary>
        /// フレーム数変更処理
        /// </summary>
        /// <param name="inFrameNum">フレーム数</param>
        public void SetFrameNum(int inFrameNum)
        {
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.SetFrameNum(inFrameNum);
            }
        }

        /// <summary>
        /// エレメント選択処理
        /// </summary>
        /// <param name="inHashCode">TreeNodeのHashCode</param>
        public void SelectElem(int inHashCode)
        {
            this.mElemSelectKey = inHashCode;
        }
        
        /// <summary>
        /// モーションの全てを削除する処理
        /// </summary>
        public void RemoveAll()
        {
            //以下、エレメント全削除処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.RemoveAll();
            }
            this.mListElem.Clear();
        }

        /// <summary>
        /// エレメント削除処理
        /// </summary>
        /// <param name="inElementKey">エレメントキー</param>
        public void RemoveElem(int inElementKey)
        {
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                int inHashCode = clElem.GetHashCode();
                if (inHashCode != inElementKey) continue;

                clElem.RemoveAll();
                this.mListElem.RemoveAt(inCnt);
                break;
            }
        }
    }
}
