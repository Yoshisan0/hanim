using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsDatMotion
    {
        public int mID;         //TreeNodeのHashCode
        public string mName;    //モーション名
        public int mFrameNum;       //トータルフレーム数
        public int mSelectFrame;    //現在選択中のフレーム
        public int mSelectLineNo;   //現在選択中の行数
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
            this.mFrameNum = 1;
            this.mSelectFrame = -1;
            this.mSelectLineNo = -1;
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

            this.mFrameNum = inFrameNum;
        }

        /// <summary>
        /// 選択中のライン番号取得処理
        /// </summary>
        /// <param name="inLineNo">選択中のライン番号</param>
        public void SetSelectLineNo(int inLineNo)
        {
            this.mSelectLineNo = inLineNo;
        }

        /// <summary>
        /// 選択中のライン番号取得処理
        /// </summary>
        /// <returns>選択中のライン番号</returns>
        public int GetSelectLineNo()
        {
            return (this.mSelectLineNo);
        }

        /// <summary>
        /// ライン番号からエレメントを取得する処理
        /// 取得できない場合は、そのライン番号にはオプションが表示されているかもしれない
        /// </summary>
        /// <param name="inLineNo">ライン番号</param>
        /// <returns>エレメント管理クラス</returns>
        public ClsDatElem GetElemFromLineNo(int inLineNo)
        {
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                if (inLineNo == clElem.mLineNo) return (clElem);

                clElem = clElem.GetElemFromLineNo(inLineNo);
                if (clElem != null) return (clElem);
            }

            return (null);
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

        /// <summary>
        /// プレビュー上のパーツの描画処理
        /// </summary>
        /// <param name="g">描画管理クラス</param>
        /// <param name="inCX">中心Ｘ座標</param>
        /// <param name="inCY">中心Ｙ座標</param>
        public void DrawPreview(Graphics g, int inCX, int inCY)
        {
            //以下、エレメント描画処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.DrawPreview(g, inCX, inCY);
            }
        }

        /// <summary>
        /// モーションのコントロール描画処理
        /// </summary>
        /// <param name="g">描画管理クラス</param>
        /// <param name="inWidth">描画先の幅</param>
        /// <param name="inHeight">描画先の高さ</param>
        /// <param name="clFont">フォント管理クラス</param>
        public void DrawControl(Graphics g, int inWidth, int inHeight, Font clFont)
        {
            //以下、エレメント描画処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.DrawControl(g, this.mSelectLineNo, inWidth, inHeight, clFont);
            }
        }

        /// <summary>
        /// モーションのタイムライン描画処理
        /// </summary>
        /// <param name="g">描画管理クラス</param>
        /// <param name="inWidth">描画先の幅</param>
        /// <param name="inHeight">描画先の高さ</param>
        public void DrawTime(Graphics g, int inWidth, int inHeight)
        {
            /*
            //DrawDragArea
            if (!mSelect_Pos_End.IsEmpty)
            {
                //選択範囲の網掛け
                SolidBrush sb = new SolidBrush(Color.FromArgb(128, 0, 0, 128));
                e.Graphics.FillRectangle(sb, mSelect_Pos_Start.X * TIME_CELL_WIDTH, 0, (mSelect_Pos_End.X - mSelect_Pos_Start.X) * TIME_CELL_WIDTH, inHeight - 1);
            }
            */

            //以下、選択中フレームのラインを表示する処理
            Brush clBrush = new SolidBrush(Color.DarkGreen);
            int inX = this.mSelectFrame * FormControl.CELL_WIDTH;
            g.FillRectangle(clBrush, inX, 0, FormControl.CELL_WIDTH, inHeight);

            //以下、エレメント描画処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.DrawTime(g, this.mSelectLineNo, this.mSelectFrame, inWidth, inHeight);
            }

            //以下、最終フレームの境界線描画処理
            Pen clPen = new Pen(Color.Green);
            inX = this.mFrameNum * FormControl.CELL_WIDTH;
            g.DrawLine(clPen, inX, 0, inX, inHeight);
        }
    }
}
