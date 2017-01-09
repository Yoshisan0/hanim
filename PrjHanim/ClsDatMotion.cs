﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsDatMotion
    {
        public int mID;         //TreeNodeのHashCode
        public string mName;    //モーション名
        private int mElemSelectIndex;       //現在編集中のエレメントリストのインデックス
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
            this.mElemSelectIndex = -1;
            this.mListElem = new List<ClsDatElem>();
        }

        /// <summary>
        /// モーションの全てを削除する処理
        /// ※これを読んだ後は ClsDatMotion.Assignment を呼んで行番号を割り振りなおさなければならない
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
        /// インデックスからエレメントを削除する処理
        /// ※これを読んだ後は ClsDatMotion.Assignment を呼んで行番号を割り振りなおさなければならない
        /// </summary>
        /// <param name="inIndex">インデックス</param>
        public void RemoveElemFromIndex(int inIndex)
        {
            if (inIndex < 0) return;
            if (inIndex >= this.mListElem.Count) return;

            ClsDatElem clElem = this.mListElem[inIndex];
            clElem.RemoveAll();
            this.mListElem.RemoveAt(inIndex);
        }

        /// <summary>
        /// 行番号からエレメントを削除する処理
        /// ※これを読んだ後は ClsDatMotion.Assignment を呼んで行番号を割り振りなおさなければならない
        /// </summary>
        /// <param name="inLineNo">行番号</param>
        public void RemoveElemFromLineNo(int inLineNo)
        {
            if (inLineNo < 0) return;

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                if (inLineNo == clElem.mLineNo)
                {
                    this.RemoveElemFromIndex(inCnt);
                    return;
                }

                clElem.RemoveElemFromLineNo(inLineNo);
            }
        }

        /// <summary>
        /// 行番号からオプションを削除する処理
        /// ※これを読んだ後は ClsDatMotion.Assignment を呼んで行番号を割り振りなおさなければならない
        /// </summary>
        /// <param name="inLineNo">行番号</param>
        public void RemoveOptionFromLineNo(int inLineNo)
        {
            if (inLineNo < 0) return;

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.RemoveOptionFromLineNo(inLineNo);
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
        public void SetSelectElem(int inHashCode)
        {
            this.mElemSelectIndex = inHashCode;
        }

        /// <summary>
        /// 選択中のエレメント取得処理
        /// </summary>
        /// <returns>選択中のエレメント管理クラス</returns>
        public ClsDatElem GetSelectElem()
        {
            int inIndex = this.mElemSelectIndex;
            if (inIndex < 0) return (null);
            if (inIndex >= this.mListElem.Count) return (null);

            ClsDatElem clElem = this.mListElem[inIndex];
            return (clElem);
        }

        /// <summary>
        /// エレメント追加処理
        /// ※これを読んだ後は ClsDatMotion.Assignment を呼んで行番号を割り振りなおさなければならない
        /// </summary>
        /// <param name="clElem">エレメント</param>
        public void AddElements(ClsDatElem clElem)
        {
            this.mListElem.Add(clElem);
        }

        /// <summary>
        /// 行番号割り振り処理
        /// </summary>
        public void Assignment()
        {
            this.mWorkLineNo = 0;
            int inTab = 0;

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.mTab = inTab;
                clElem.Assignment(this, inTab + 1);
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