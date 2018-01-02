using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace PrjHikariwoAnim
{
    public class ClsDatMotion
    {
        public int mID;                     //ランダム値（ClsSystem.mDicMotionのキー）
        public int mItemHashCode;           //TreeNodeのHashCode
        public string mName;                //モーション名
        public int mMaxFrameNum;            //最大フレーム数
        public List<ClsDatElem> mListElem;  //エレメント管理クラスのリスト

        //以下、作業領域
        public int mWorkLineNo;             //行番号割り振り時に利用する一時保持領域
        public ClsDatElem mWorkElem;        //検索時に利用するエレメント一時保持領域
        public ClsDatOption mWorkOption;    //検索時に利用するオプション一時保持領域
        public ClsDatItem mWorkItem;        //検索時に利用するアイテム一時保持領域

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="inID">TreeNodeのハッシュコード</param>
        /// <param name="clName">モーション名</param>
        public ClsDatMotion(int inID, string clName)
        {
            this.mID = inID;
            this.mName = clName;
            this.mMaxFrameNum = ClsSystem.DEFAULT_FRAME_NUM;
            this.mListElem = new List<ClsDatElem>();
        }

        /// <summary>
        /// モーションの全てを削除する処理
        /// </summary>
        public void Remove()
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
        /// ※これを読んだ後は ClsDatMotion.RefreshLineNo を呼んで行番号を割り振りなおさなければならない
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
        /// 行番号からアイテムを削除する処理
        /// ※これを読んだ後は ClsDatMotion.RefreshLineNo を呼んで行番号を割り振りなおさなければならない
        /// </summary>
        /// <param name="inLineNo">行番号</param>
        /// <param name="isForce">強制フラグ</param>
        /// <param name="isRemove">実体削除フラグ</param>
        public void RemoveItemFromLineNo(int inLineNo, bool isForce, bool isRemove)
        {
            ClsDatItem clItem = ClsSystem.GetItemFromLineNo(inLineNo);
            if (clItem == null) return;

            switch(clItem.mTypeItem) {
            case ClsDatItem.TYPE_ITEM.ELEM:
                this.RemoveElemFromLineNo(inLineNo, isRemove);
                break;
            case ClsDatItem.TYPE_ITEM.OPTION:
                this.RemoveOptionFromLineNo(inLineNo, isForce, isRemove);
                break;
            }
        }

        /// <summary>
        /// 行番号からエレメントを削除する処理
        /// ※これを読んだ後は ClsDatMotion.RefreshLineNo を呼んで行番号を割り振りなおさなければならない
        /// </summary>
        /// <param name="inLineNo">行番号</param>
        /// <param name="isRemove">実体削除フラグ</param>
        public void RemoveElemFromLineNo(int inLineNo, bool isRemove)
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

                clElem.RemoveElemFromLineNo(inLineNo, isRemove);
            }
        }

        /// <summary>
        /// 行番号からオプションを削除する処理
        /// ※これを読んだ後は ClsDatMotion.RefreshLineNo を呼んで行番号を割り振りなおさなければならない
        /// </summary>
        /// <param name="inLineNo">行番号</param>
        /// <param name="isForce">強制フラグ</param>
        /// <param name="isRemove">実体削除フラグ</param>
        public void RemoveOptionFromLineNo(int inLineNo, bool isForce, bool isRemove)
        {
            if (inLineNo < 0) return;

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.RemoveOptionFromLineNo(inLineNo, isForce, isRemove);
            }
        }

        /// <summary>
        /// ハッシュコードからエレメントを削除する処理
        /// ※これを読んだ後は ClsDatMotion.RefreshLineNo を呼んで行番号を割り振りなおさなければならない
        /// （ClsDatElemと重複しているので、いづれ継承でまとめる）
        /// </summary>
        /// <param name="inHashCode">ハッシュコード</param>
        /// <param name="isRemove">実体削除フラグ</param>
        public void RemoveElemFromHashCode(int inHashCode, bool isRemove)
        {
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                int inHashCodeTmp = clElem.GetHashCode();
                if (inHashCode == inHashCodeTmp)
                {
                    if (isRemove)
                    {
                        clElem.RemoveAll();
                    }

                    this.mListElem.RemoveAt(inCnt);
                    return;
                }

                clElem.RemoveElemFromHashCode(inHashCode, isRemove);
            }
        }

        /// <summary>
        /// モーションロード後にモーション内構造の再構築を行う
        /// </summary>
        public void Restore()
        {
            //ElementList再構築
            foreach(ClsDatElem clElem in this.mListElem)
            {
                clElem.mMotion = this;  //親の再指定
                clElem.Restore();
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
        /// 読み込み処理
        /// </summary>
        /// <param name="clXmlElem">xmlエレメント</param>
        public void Load(XmlElement clXmlElem)
        {
            XmlNodeList clListNode = clXmlElem.ChildNodes;
            this.mID = ClsTool.GetIntFromXmlNodeList(clListNode, "ID");
            this.mName = ClsTool.GetStringFromXmlNodeList(clListNode, "Name");
            this.mMaxFrameNum = ClsTool.GetIntFromXmlNodeList(clListNode, "FrameNum");

            //以下、各管理クラス作成処理
            foreach (XmlNode clNode in clListNode)
            {
                if ("Elem".Equals(clNode.Name))
                {
                    ClsDatElem clDatElem = new ClsDatElem(this, null, 0.0f, 0.0f, null, null);
                    clDatElem.Load(clNode);

                    this.mListElem.Add(clDatElem);
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
            //以下、モーション保存処理
            ClsTool.AppendElementStart(clHeader, "Motion");
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "ID", this.mID);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Name", this.mName);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "FrameNum", this.mMaxFrameNum);

            //以下、エレメントリスト保存処理
            foreach (ClsDatElem clDatElem in this.mListElem)
            {
                clDatElem.Save(clHeader + ClsSystem.FILE_TAG);
            }

            ClsTool.AppendElementEnd(clHeader, "Motion");
        }

        /// <summary>
        /// 読み込み処理
        /// </summary>
        public void Load()
        {
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
        /// <param name="inMaxFrameNum">フレーム数</param>
        public void SetMaxFrameNum(int inMaxFrameNum)
        {
            this.mMaxFrameNum = inMaxFrameNum;
        }

        /// <summary>
        /// フレーム数取得処理
        /// </summary>
        /// <returns>フレーム数</returns>
        public int GetMaxFrameNum()
        {
            return (this.mMaxFrameNum);
        }

        /// <summary>
        /// アイテムを選択する処理
        /// </summary>
        /// <param name="clItem">アイテム</param>
        public void SetSelectFromItem(ClsDatItem clItem)
        {
            if (clItem == null) return;

            ClsSystem.SetSelectFromLineNo(clItem.mLineNo);
        }

        /// <summary>
        /// エレメント追加処理
        /// ※これを読んだ後は ClsDatMotion.RefreshLineNo を呼んで行番号を割り振りなおさなければならない
        /// </summary>
        /// <param name="clElem">エレメント</param>
        public void AddElements(ClsDatElem clElem)
        {
            this.mListElem.Add(clElem);
        }

        /// <summary>
        /// 行番号割り振り処理
        /// </summary>
        public void RefreshLineNo()
        {
            this.mWorkLineNo = 0;
            int inTab = 0;

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.mTab = inTab;
                clElem.RefreshLineNo(this, inTab + 1);
            }
        }

        /// <summary>
        /// プレビュー上のパーツの描画処理
        /// </summary>
        /// <param name="clGL">OpenGLコンポーネント</param>
        /// <param name="inFrameNo">フレーム番号</param>
        /// <param name="inMaxFrameNum">フレーム数</param>
        public void DrawPreview(ComponentOpenGL clGL, int inFrameNo, int inMaxFrameNum)
        {
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                //以下、マトリクス初期化処理
                float[] pflMat = clGL.InitElemMatrix();

                //以下、エレメント描画処理
                ClsDatElem clElem = this.mListElem[inCnt];
                ClsParam clParam = new ClsParam();
                clElem.DrawPreview(clGL, inFrameNo, inMaxFrameNum, clParam, pflMat);
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
            int inLineNo = ClsSystem.GetSelectLineNo();

            //以下、エレメント描画処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.DrawControl(g, inLineNo, inWidth, inHeight, clFont);
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
            int inLineNo = ClsSystem.GetSelectLineNo();
            int inFrameNo = ClsSystem.GetSelectFrameNo();

            //以下、選択中フレームのラインを表示する処理
            Brush clBrush = new SolidBrush(Color.DarkGreen);
            int inX = inFrameNo * FormControl.CELL_WIDTH;
            g.FillRectangle(clBrush, inX, 0, FormControl.CELL_WIDTH, inHeight);

            //以下、エレメント描画処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.DrawTime(g, inLineNo, inFrameNo, this.mMaxFrameNum, inWidth, inHeight);
            }

            //以下、最終フレームの境界線描画処理
            Pen clPen = new Pen(Color.Green);
            inX = this.mMaxFrameNum * FormControl.CELL_WIDTH;
            g.DrawLine(clPen, inX, 0, inX, inHeight);
        }

        /// <summary>
        /// 指定のエレメントが上に移動できるかチェックする処理
        /// </summary>
        /// <param name="clElem">エレメント</param>
        /// <returns>移動可能フラグ</returns>
        public bool CanMoveUp(ClsDatElem clElem)
        {
            if (clElem == null) return (false);
            if (this.mListElem == null) return (false);
            if (this.mListElem.Count <= 0) return (false);

            int inHashCode1 = clElem.GetHashCode();

            ClsDatElem clElem1ST = this.mListElem[0] as ClsDatElem;
            int inHashCode2 = clElem1ST.GetHashCode();
            if (inHashCode1 == inHashCode2) return (false);

            return (true);
        }

        /// <summary>
        /// 指定のエレメントが下に移動できるかチェックする処理
        /// </summary>
        /// <param name="clElem">エレメント</param>
        /// <returns>移動可能フラグ</returns>
        public bool CanMoveDown(ClsDatElem clElem)
        {
            if (clElem == null) return (false);
            if (this.mListElem == null) return (false);
            if (this.mListElem.Count <= 0) return (false);

            int inHashCode1 = clElem.GetHashCode();

            ClsDatElem clElemEnd = this.mListElem[this.mListElem.Count- 1] as ClsDatElem;
            int inHashCode2 = clElemEnd.GetHashCode();
            if (inHashCode1 == inHashCode2) return (false);

            return (true);
        }

        /// <summary>
        /// エレメント検索処理
        /// </summary>
        /// <param name="clElem">エレメント</param>
        /// <returns>インデックス</returns>
        public int FindIndexFromElem(ClsDatElem clElem)
        {
            int inHashCode1 = clElem.GetHashCode();

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElemTmp = this.mListElem[inCnt];
                int inHashCode2 = clElemTmp.GetHashCode();
                if (inHashCode1 == inHashCode2) return (inCnt);
            }

            return (-1);
        }

        /// <summary>
        /// 指定のエレメントを上に移動する処理
        /// </summary>
        /// <param name="clElem">エレメント</param>
        public void MoveUp(ClsDatElem clElem)
        {
            bool isCanMove = this.CanMoveUp(clElem);
            if (!isCanMove) return;

            int inIndex = this.FindIndexFromElem(clElem);
            if (inIndex < 0) return;

            //以下、一つ上と入れ替える処理
            ClsDatElem clElemTmp = this.mListElem[inIndex - 1];
            this.mListElem[inIndex - 1] = clElem;
            this.mListElem[inIndex] = clElemTmp;
        }

        /// <summary>
        /// 指定のエレメントを下に移動する処理
        /// </summary>
        /// <param name="clElem">エレメント</param>
        public void MoveDown(ClsDatElem clElem)
        {
            bool isCanMove = this.CanMoveDown(clElem);
            if (!isCanMove) return;

            int inIndex = this.FindIndexFromElem(clElem);
            if (inIndex < 0) return;

            //以下、一つ下と入れ替える処理
            ClsDatElem clElemTmp = this.mListElem[inIndex + 1];
            this.mListElem[inIndex + 1] = clElem;
            this.mListElem[inIndex] = clElemTmp;
        }

        /// <summary>
        /// 行番号からアイテムを検索する処理
        /// </summary>
        /// <param name="inLineNo">行番号</param>
        /// <returns>アイテム</returns>
        public ClsDatItem GetItemFromLineNo(int inLineNo)
        {
            this.mWorkItem = null;

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.FindItemFromLineNo(this, inLineNo);

                if (this.mWorkItem != null)
                {
                    return (this.mWorkItem);
                }
            }

            return (null);
        }

        /// <summary>
        /// 挿入可能マークからエレメントを検索する処理
        /// </summary>
        /// <param name="enMark">挿入可能マーク</param>
        /// <returns>エレメント</returns>
        public ClsDatElem FindElemFromMark(EnmMarkElement enMark)
        {
            this.mWorkElem = null;

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.FindElemFromMark(this, enMark);

                if (this.mWorkElem != null)
                {
                    return (this.mWorkElem);
                }
            }

            return (null);
        }

        /// <summary>
        /// 行番号からアイテムを検索する処理
        /// </summary>
        /// <param name="inLineNo">行番号</param>
        /// <returns>アイテム</returns>
        public ClsDatItem FindItemFromLineNo(int inLineNo)
        {
            this.mWorkItem = null;

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.FindItemFromLineNo(this, inLineNo);

                if (this.mWorkItem != null)
                {
                    return (this.mWorkItem);
                }
            }

            return (null);
        }

        /// <summary>
        /// 挿入可能マークのクリア
        /// </summary>
        public void ClearInsertMark()
        {
            //以下、子エレメントの挿入マークを消す処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.ClearInsertMark();
            }
        }
    }
}
