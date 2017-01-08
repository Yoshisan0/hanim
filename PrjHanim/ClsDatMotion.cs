using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsDatMotion
    {
        public int mID;         //TreeNodeのHashCode
        public string mName;    //モーション名
        public int mElemSelectKey;          //現在編集中のエレメントキー（TreeNodeのハッシュコード）
        public List<ClsDatElem> mListElem;  //エレメント管理クラスのリスト

        //以下、作業領域
        public int mWorkLineNo;             //行番号割り振り時に利用する一時保持領域
        public ClsDatElem mWorkElem;        //検索時に利用するエレメント一時保持領域
        public ClsDatOption mWorkOption;    //検索時に利用するオプション一時保持領域

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="inID">TreeNodeのハッシュコード</param>
        /// <param name="clName">モーション名</param>
        public ClsDatMotion(int inID, string clName)
        {
            this.mID = inID;
            this.mName = clName;
            this.mElemSelectKey = -1;
            this.mListElem = new List<ClsDatElem>();
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
                if (inElementKey != clElem.mID) continue;

                clElem.RemoveAll();
                this.mListElem.RemoveAt(inCnt);
                break;
            }
        }

        /// <summary>
        /// エクスポート
        /// </summary>
        /// <returns>出力情報</returns>
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
        /// エレメント追加処理
        /// </summary>
        /// <param name="clElem">エレメント</param>
        public void AddElements(ClsDatElem clElem)
        {
            this.mListElem.Add(clElem);
        }

        /// <summary>
        /// 行番号割り振り処理
        /// </summary>
        public void AssignmentLineNo()
        {
            this.mWorkLineNo = 0;

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.AssignmentLineNo(this);
            }
        }

        /// <summary>
        /// 行番号からエレメントを検索する処理
        /// </summary>
        /// <param name="inLineNo">行番号</param>
        /// <returns>エレメント</returns>
        public ClsDatElem FindElemFromLineNo(int inLineNo)
        {
            this.mWorkElem = null;

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.FindElemFromLineNo(this, inLineNo);

                if (this.mWorkElem != null)
                {
                    return (this.mWorkElem);
                }
            }

            return (this.mWorkElem);
        }

        /// <summary>
        /// 行番号からオプションを検索する処理
        /// </summary>
        /// <param name="inLineNo">行番号</param>
        /// <returns>オプション</returns>
        public ClsDatOption FindOptionFromLineNo(int inLineNo)
        {
            this.mWorkOption = null;

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.FindOptionFromLineNo(this, inLineNo);

                if (this.mWorkElem != null)
                {
                    return (this.mWorkOption);
                }
            }

            return (this.mWorkOption);
        }
    }
}
