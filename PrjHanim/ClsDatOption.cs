using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace PrjHikariwoAnim
{
    [Serializable]
    public class ClsDatOption : ClsDatItem
    {
        public ClsDatElem mElem;        //親エレメント
        public TYPE_OPTION mTypeOption; //タイプ
        private Dictionary<int, ClsDatKeyFrame> mDicKeyFrame;  //キーはフレーム番号　値はキーフレーム管理クラス

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="clElem">親エレメント</param>
        /// <param name="enType">オプションタイプ</param>
        /// <param name="isParentFlag">親の設定に依存するかどうか</param>
        /// <param name="clValue1">初期状態の値１</param>
        /// <param name="clValue2">初期状態の値２</param>
        public ClsDatOption(ClsDatElem clElem, TYPE_OPTION enTypeOption, bool isParentFlag, object clValue1, object clValue2)
        {
            this.mTypeItem = TYPE_ITEM.OPTION;

            this.mElem = clElem;
            this.mTypeOption = enTypeOption;
            this.mDicKeyFrame = new Dictionary<int, ClsDatKeyFrame>();

            //以下、0フレーム目にキーフレームを登録する処理（0フレーム目には必ずキーフレームが存在する）
            ClsDatKeyFrame clKeyFrame = new ClsDatKeyFrame(enTypeOption, 0, isParentFlag, clValue1, clValue2);
            this.mDicKeyFrame.Add(0, clKeyFrame);
        }

        /// <summary>
        /// オプションの全てを削除する処理
        /// </summary>
        public void RemoveAll()
        {
            //以下、キーフレーム全削除処理
            foreach (int inKey in this.mDicKeyFrame.Keys)
            {
                ClsDatKeyFrame clDatKeyFrame = this.mDicKeyFrame[inKey];
                clDatKeyFrame.RemoveAll();
            }
            this.mDicKeyFrame.Clear();
        }

        /// <summary>
        /// キーフレーム削除処理
        /// </summary>
        /// <param name="inFrameNo">フレーム番号</param>
        public void RemoveKeyFrame(int inFrameNo)
        {
            if (inFrameNo == 0) return; //0フレーム目のキーフレームは消せない

            bool isExist = this.IsExistKeyFrame(inFrameNo);
            if (!isExist) return;

            ClsDatKeyFrame clDatKeyFrame = this.mDicKeyFrame[inFrameNo];
            clDatKeyFrame.RemoveAll();
            this.mDicKeyFrame.Remove(inFrameNo);
        }

        /// <summary>
        /// 削除可能フラグの取得
        /// </summary>
        /// <returns>削除可能フラグ</returns>
        public bool IsRemoveOK()
        {
            bool isRemoveOK = ClsDatOption.IsRemoveOK(this.mTypeOption);
            return (isRemoveOK);
        }

        /// <summary>
        /// 削除可能フラグの取得
        /// </summary>
        /// <param name="enTypeOption">オプション種別</param>
        /// <returns>削除可能フラグ</returns>
        public static bool IsRemoveOK(TYPE_OPTION enTypeOption)
        {
            if (enTypeOption == TYPE_OPTION.NONE) return (false);
            if (enTypeOption == TYPE_OPTION.DISPLAY) return (false);
            if (enTypeOption == TYPE_OPTION.POSITION) return (false);

            return (true);
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
        /// キーフレーム存在チェック処理
        /// </summary>
        /// <param name="inFrameNo">フレーム番号</param>
        /// <returns>存在フラグ</returns>
        public bool IsExistKeyFrame(int inFrameNo)
        {
            bool isExist = this.mDicKeyFrame.ContainsKey(inFrameNo);
            return (isExist);
        }

        /// <summary>
        /// キーフレーム登録・更新処理
        /// </summary>
        /// <param name="inFrameNo">フレーム番号</param>
        /// <param name="isParentFlag">親の設定に依存するかどうか</param>
        /// <param name="clValue1">値１</param>
        /// <param name="clValue2">値２</param>
        public void SetKeyFrame(int inFrameNo, bool isParentFlag, object clValue1, object clValue2)
        {
            bool isExist = this.mDicKeyFrame.ContainsKey(inFrameNo);
            if (isExist)
            {
                ClsDatKeyFrame clKeyFrame = this.mDicKeyFrame[inFrameNo];
                clKeyFrame.mParentFlag = isParentFlag;
                clKeyFrame.mValue1 = clValue1;
                clKeyFrame.mValue2 = clValue2;
            }
            else
            {
                this.mDicKeyFrame[inFrameNo] = new ClsDatKeyFrame(this.mTypeOption, inFrameNo, isParentFlag, clValue1, clValue2);
            }
        }

        /// <summary>
        /// 読み込み処理
        /// </summary>
        /// <param name="clXmlNode">xmlノード</param>
        public void Load(XmlNode clXmlNode)
        {
            XmlNodeList clListNode = clXmlNode.ChildNodes;
            string clTypeOption = ClsTool.GetStringFromXmlNodeList(clListNode, "TypeOption");
            this.mTypeOption = (TYPE_OPTION)Enum.Parse(typeof(TYPE_OPTION), clTypeOption);

            //以下、各管理クラス作成処理
            foreach (XmlNode clNode in clListNode)
            {
                if ("KeyFrame".Equals(clNode.Name))
                {
                    bool isParentFlag = ClsParam.GetDefaultParentFlag(this.mElem, this.mTypeOption);
                    object clValue1 = ClsParam.GetDefaultValue1(this.mTypeOption);
                    object clValue2 = ClsParam.GetDefaultValue2(this.mTypeOption);
                    ClsDatKeyFrame clDatKeyFrame = new ClsDatKeyFrame(this.mTypeOption, 0, isParentFlag, clValue1, clValue2);
                    clDatKeyFrame.Load(clNode);

                    this.mDicKeyFrame[clDatKeyFrame.mFrameNo] = clDatKeyFrame;
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
            //以下、オプション保存処理
            ClsTool.AppendElementStart(clHeader, "Option");
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "TypeOption", this.mTypeOption.ToString());

            //以下、キーフレーム保存処理
            foreach (int inKey in this.mDicKeyFrame.Keys)
            {
                ClsDatKeyFrame clDatKeyFrame = this.mDicKeyFrame[inKey];
                clDatKeyFrame.Save(clHeader + ClsSystem.FILE_TAG);
            }

            ClsTool.AppendElementEnd(clHeader, "Option");
        }

        /// <summary>
        /// コントロールに表示するかチェックする処理
        /// </summary>
        /// <param name="enType">オプション種別</param>
        /// <returns>表示フラグ</returns>
        public static bool IsDraw(TYPE_OPTION enTypeOption)
        {
            if (enTypeOption == TYPE_OPTION.NONE) return (false);
            if (enTypeOption == TYPE_OPTION.DISPLAY) return (false);    //DISPLAYはエレメントとして描画するので、オプションとしては描画しない

            return (true);
        }

        /// <summary>
        /// 行番号割り振り処理
        /// </summary>
        /// <param name="clMotion">モーション管理クラス</param>
        public void Assignment(ClsDatMotion clMotion)
        {
            //以下、表示しなくてはいけない種別かどうかチェックする処理
            bool isDraw = ClsDatOption.IsDraw(this.mTypeOption);
            if (!isDraw) return;

            //以下、行番号設定
            this.mLineNo = clMotion.mWorkLineNo;
            clMotion.mWorkLineNo++;
        }

        /// <summary>
        /// キーフレーム取得処理
        /// </summary>
        /// <param name="inFrameNo">フレーム番号</param>
        /// <returns>キーフレーム</returns>
        public ClsDatKeyFrame GetKeyFrame(int inFrameNo)
        {
            bool isExist = this.mDicKeyFrame.ContainsKey(inFrameNo);
            if (!isExist) return (null);

            ClsDatKeyFrame clKeyFrame = this.mDicKeyFrame[inFrameNo];
            return (clKeyFrame);
        }

        /// <summary>
        /// エレメントのコントロール描画処理
        /// </summary>
        /// <param name="g">描画管理クラス</param>
        /// <param name="inSelectLineNo">選択中のライン番号</param>
        /// <param name="inWidth">描画先の幅</param>
        /// <param name="inHeight">描画先の高さ</param>
        /// <param name="clFont">フォント管理クラス</param>
        public void DrawControl(Graphics g, int inSelectLineNo, int inWidth, int inHeight, Font clFont)
        {
            //以下、表示しなくてはいけない種別かどうかチェックする処理
            bool isDraw = ClsDatOption.IsDraw(this.mTypeOption);
            if (!isDraw) return;

            //以下、横ライン描画処理
            int inY = FormControl.CELL_HEIGHT;
            //g.DrawLine(Pens.Black, 0, inY, inWidth, inY);

            //以下、背景を塗る処理
            if (inSelectLineNo == this.mLineNo)
            {
                //選択中Elementsの背景強調
                SolidBrush sb = new SolidBrush(Color.DarkGreen);
                g.FillRectangle(sb, 0, this.mLineNo * FormControl.CELL_HEIGHT, inWidth, FormControl.CELL_HEIGHT);
            }
            else
            {
                if (this.mLineNo % 2 == 0)
                {
                    SolidBrush sb = new SolidBrush(Color.Black);
                    g.FillRectangle(sb, 0, this.mLineNo * FormControl.CELL_HEIGHT, inWidth, FormControl.CELL_HEIGHT);
                }
                else
                {
                    SolidBrush sb = new SolidBrush(Color.FromArgb(0xFF, 20, 20, 30));
                    g.FillRectangle(sb, 0, this.mLineNo * FormControl.CELL_HEIGHT, inWidth, FormControl.CELL_HEIGHT);
                }
            }

            //以下、名前描画処理
            string clName = ClsTool.CnvTypeOption2Name(this.mTypeOption);
            if (!string.IsNullOrEmpty(clName))
            {
                string clBlank = this.GetTabBlank();
                g.DrawString(clBlank + clName, clFont, Brushes.White, 52, this.mLineNo * FormControl.CELL_HEIGHT + 2);
            }
        }

        /// <summary>
        /// エレメントのタイムライン描画処理
        /// </summary>
        /// <param name="g">描画管理クラス</param>
        /// <param name="inSelectLineNo">選択中のライン番号</param>
        /// <param name="inSelectFrameNo">選択中のフレーム</param>
        /// <param name="inMaxFrameNum">最大フレーム数</param>
        /// <param name="inWidth">描画先の幅</param>
        /// <param name="inHeight">描画先の高さ</param>
        public void DrawTime(Graphics g, int inSelectLineNo, int inSelectFrameNo, int inMaxFrameNum, int inWidth, int inHeight)
        {
            //以下、表示しなくてはいけない種別かどうかチェックする処理
            bool isDraw = ClsDatOption.IsDraw(this.mTypeOption);
            if (!isDraw) return;

            //以下、背景を塗る処理
            SolidBrush clBrush;
            int inX = inSelectFrameNo * FormControl.CELL_WIDTH;
            int inY = this.mLineNo * FormControl.CELL_HEIGHT;
            if (inSelectLineNo == this.mLineNo)
            {
                //選択中Elementsの背景強調
                clBrush = new SolidBrush(Color.DarkGreen);
                g.FillRectangle(clBrush, 0, inY, inWidth, FormControl.CELL_HEIGHT);
            }
            else
            {
                if (this.mLineNo % 2 == 0)
                {
                    clBrush = new SolidBrush(Color.Black);
                    g.FillRectangle(clBrush, 0, inY, inWidth, FormControl.CELL_HEIGHT);
                }
                else
                {
                    clBrush = new SolidBrush(Color.FromArgb(0xFF, 20, 20, 30));
                    g.FillRectangle(clBrush, 0, inY, inWidth, FormControl.CELL_HEIGHT);
                }
            }

            //以下、フレームの背景（親の影響を受けるかどうかと、Tweenの影響下にあるかどうか）を表示する処理
            Color stColorParent = Color.FromArgb(128, Color.LightPink);
            SolidBrush clBrushParent = new SolidBrush(stColorParent);
            Color stColorTween = Color.FromArgb(128, Color.LightBlue);
            SolidBrush clBrushTween = new SolidBrush(stColorTween);
            bool isParentFlag = ClsParam.GetDefaultParentFlag(this.mElem, this.mTypeOption);
            int inFrameNo = 0;
            for (inFrameNo = 0; inFrameNo < inMaxFrameNum; inFrameNo++)
            {
                if (this.mTypeOption == TYPE_OPTION.POSITION ||
                    this.mTypeOption == TYPE_OPTION.ROTATION ||
                    this.mTypeOption == TYPE_OPTION.SCALE)
                {
                    isParentFlag = (this.mElem.mElem != null);
                }
                else
                {
                    bool isExist = this.mDicKeyFrame.ContainsKey(inFrameNo);
                    if (isExist)
                    {
                        ClsDatKeyFrame clKeyFrame = this.mDicKeyFrame[inFrameNo];
                        isParentFlag = clKeyFrame.mParentFlag;
                    }
                }
                if (!isParentFlag) continue;

                inX = inFrameNo * FormControl.CELL_WIDTH;
                inY = this.mLineNo * FormControl.CELL_HEIGHT;
                g.FillRectangle(clBrushParent, inX, inY + 2, FormControl.CELL_WIDTH, FormControl.CELL_HEIGHT / 2 - 4);
            }

            //以下、選択中のフレーム描画処理
            inX = inSelectFrameNo * FormControl.CELL_WIDTH;
            inY = this.mLineNo * FormControl.CELL_HEIGHT;
            if (inSelectLineNo == this.mLineNo)
            {
                Color stColor = Color.FromArgb(128, Color.Green);
                clBrush = new SolidBrush(stColor);
            }
            else
            {
                Color stColor = Color.FromArgb(128, Color.DarkGreen);
                clBrush = new SolidBrush(stColor);
            }
            g.FillRectangle(clBrush, inX, inY, FormControl.CELL_WIDTH, FormControl.CELL_HEIGHT);

            //以下、境界線描画処理
            Pen clPen = new Pen(Color.Green);
            inY = this.mLineNo * FormControl.CELL_HEIGHT + FormControl.CELL_HEIGHT - 1;
            g.DrawLine(clPen, 0, inY, inWidth, inY);

            //以下、キーフレーム表示処理
            for(inFrameNo= 0;inFrameNo< inMaxFrameNum;inFrameNo++)
            {
                bool isExist = this.mDicKeyFrame.ContainsKey(inFrameNo);
                if (!isExist) continue;

                ClsDatKeyFrame clKeyFrame = this.mDicKeyFrame[inFrameNo];
                isParentFlag = clKeyFrame.mParentFlag;
                g.DrawImage(Properties.Resources.markRed, inFrameNo * FormControl.CELL_WIDTH + 2, this.mLineNo * FormControl.CELL_HEIGHT + 1);
            }
        }
    }
}