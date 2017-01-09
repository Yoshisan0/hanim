using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsDatElem : ClsDatItem
    {
        // コントロール左側ペインに相当する部分
        public enum ELEMENTSTYPE {
            Image,
            Shape,
            Joint,
            Effect,
            Accessory,
            FX
        }
        public enum ELEMENTSSTYLE {
            Rect,
            Circle,
            Point
        }

        public string mName;                //エレメント名
        public ELEMENTSTYPE mType;          //Default Image
        public ELEMENTSSTYLE mStyle;        //Default Rect
        public bool isVisible;              //表示非表示(目)
        public bool isLocked;               //ロック状態(鍵)
        public bool isOpen;                 //属性開閉状態(+-)
        public bool isSelect;               //選択状態
        public int ImageChipID;             //イメージID
        public List<ClsDatElem> mListElem;  //エレメント管理クラスのリスト
        public Dictionary<ClsDatOption.TYPE, ClsDatOption> mDicOption;  //キーはアトリビュートのタイプ 値はオプション管理クラス
        public AttributeBase mAttInit;      //初期情報

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClsDatElem()
        {
            this.mName = this.GetHashCode().ToString("X8");//仮名
            this.mType = ELEMENTSTYPE.Image;
            this.mStyle = ELEMENTSSTYLE.Rect;
            this.isVisible = true;  //表示非表示(目)
            this.isLocked = false;  //ロック状態(鍵)
            this.isOpen = false;    //属性開閉状態(+-)
            this.isSelect = false;  //選択状態
            this.mListElem = new List<ClsDatElem>();
            this.mDicOption = new Dictionary<ClsDatOption.TYPE, ClsDatOption>();
            this.mAttInit = new AttributeBase();
        }

        /// <summary>
        /// エレメントの全てを削除する処理
        /// ※これを読んだ後は ClsDatMotion.Assignment を呼んで行番号を割り振りなおさなければならない
        /// </summary>
        public void RemoveAll()
        {
            //以下、子供のエレメントリスト全削除処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.RemoveAll();
            }
            this.mListElem.Clear();

            //以下、オプション全削除処理
            foreach (ClsDatOption.TYPE enKey in this.mDicOption.Keys)
            {
                ClsDatOption clOption = this.mDicOption[enKey];
                clOption.RemoveAll();
            }
            this.mDicOption.Clear();
        }

        /// <summary>
        /// 行番号からエレメントを削除する処理
        /// ※これを読んだ後は ClsDatMotion.Assignment を呼んで行番号を割り振りなおさなければならない
        /// </summary>
        /// <param name="inLineNo">行番号</param>
        public void RemoveElemFromLineNo(int inLineNo)
        {
            if (inLineNo < 0) return;
            if (!this.isOpen) return;

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                if (inLineNo == clElem.mLineNo)
                {
                    clElem.RemoveAll();
                    this.mListElem.RemoveAt(inCnt);
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
            if (!this.isOpen) return;

            foreach (ClsDatOption.TYPE enType in this.mDicOption.Keys)
            {
                ClsDatOption clOption = this.mDicOption[enType];
                if (clOption.mLineNo != inLineNo) continue;

                clOption.RemoveAll();
                this.mDicOption.Remove(enType);
                return;
            }

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
            clDic["Atr_0"] = this.Atr.Export();
            return (clDic);
            */

            return (null);
        }

        /// <summary>
        /// エレメント名設定処理
        /// </summary>
        /// <param name="clName">エレメント名</param>
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
            foreach (ClsDatOption.TYPE enType in this.mDicOption.Keys)
            {
                ClsDatOption clOption = this.mDicOption[enType];
                clOption.SetFrameNum(inFrameNum);
            }

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.SetFrameNum(inFrameNum);
            }
        }

        /// <summary>
        /// 行番号割り振り処理
        /// </summary>
        /// <param name="clMotion">モーション管理クラス</param>
        /// <param name="inTab">タブ値</param>
        public void Assignment(ClsDatMotion clMotion, int inTab)
        {
            this.mLineNo = clMotion.mWorkLineNo;
            clMotion.mWorkLineNo++;

            if (!this.isOpen) return;   //開いていなかったら子供エレメントと子供オプションを見に行かない

            foreach (ClsDatOption.TYPE enType in this.mDicOption.Keys)
            {
                ClsDatOption clOption = this.mDicOption[enType];
                clOption.mTab = inTab;  //タブ値設定
                clOption.Assignment(clMotion);
            }

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.mTab = inTab;    //タブ値設定
                clElem.Assignment(clMotion, inTab + 1);
            }
        }

        /// <summary>
        /// 行番号からエレメントを取得する処理
        /// </summary>
        /// <param name="clMotion">モーション管理クラス</param>
        /// <param name="inLineNo">行番号</param>
        public void FindElemFromLineNo(ClsDatMotion clMotion, int inLineNo)
        {
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                if (clElem.mLineNo == inLineNo)
                {
                    clMotion.mWorkElem = this;
                    return;
                }

                clElem.FindElemFromLineNo(clMotion, inLineNo);
            }
        }

        /// <summary>
        /// 行番号からオプションを取得する処理
        /// </summary>
        /// <param name="clMotion">モーション管理クラス</param>
        /// <param name="inLineNo">行番号</param>
        public void FindOptionFromLineNo(ClsDatMotion clMotion, int inLineNo)
        {
            if (!this.isOpen) return;   //開いていなかったら子供エレメントと子供オプションを見に行かない

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                if (clElem.mLineNo == inLineNo) return;  //Optionを検索したかったのだが、該当のItemがElementだった
                clElem.FindOptionFromLineNo(clMotion, inLineNo);
            }

            foreach (ClsDatOption.TYPE enType in this.mDicOption.Keys)
            {
                ClsDatOption clOption = this.mDicOption[enType];
                if (clOption.mLineNo == inLineNo)
                {
                    clMotion.mWorkOption = clOption;
                    return;
                }
            }
        }
    }
}
