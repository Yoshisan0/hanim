using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace PrjHikariwoAnim
{
    [Serializable]
    public class ClsDatOption : ClsDatItem
    {
        public ClsDatElem mElem;    //親エレメント
        public TYPE_OPTION mTypeOption;  //タイプ
        public Dictionary<int, ClsDatKeyFrame> mDicKeyFrame;  //キーはフレーム番号　値はキーフレーム管理クラス
        public object mValue;   //値（何の値かはタイプに依存する）

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="clElem">親エレメント</param>
        /// <param name="enType">オプションタイプ</param>
        /// <param name="clValue">初期状態の値</param>
        public ClsDatOption(ClsDatElem clElem, TYPE_OPTION enType, object clValue)
        {
            this.mTypeItem = TYPE_ITEM.OPTION;

            this.mElem = clElem;
            this.mTypeOption = enType;
            this.mDicKeyFrame = new Dictionary<int, ClsDatKeyFrame>();
            this.mValue = clValue;

            //以下、0フレーム目にキーフレームを登録する処理（0フレーム目には必ずキーフレームが存在する）
            ClsDatKeyFrame clKeyFrame = new ClsDatKeyFrame(0);
            this.mDicKeyFrame.Add(0, clKeyFrame);
        }

        /// <summary>
        /// オプションの全てを削除する処理
        /// ※これを読んだ後は ClsDatMotion.Assignment を呼んで行番号を割り振りなおさなければならない
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
            if (enTypeOption == TYPE_OPTION.POSITION_X) return (false);
            if (enTypeOption == TYPE_OPTION.POSITION_Y) return (false);

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
        /// 読み込み処理
        /// </summary>
        /// <param name="clXmlNode">xmlノード</param>
        public void Load(XmlNode clXmlNode)
        {
            XmlNodeList clListNode = clXmlNode.ChildNodes;
            string clTypeOption = ClsTool.GetStringFromXmlNodeList(clListNode, "TypeOption");
            this.mTypeOption = (TYPE_OPTION)Enum.Parse(typeof(TYPE_OPTION), clTypeOption);

            string clValue = ClsTool.GetStringFromXmlNodeList(clListNode, "Value");
            switch (this.mTypeOption) {
            case TYPE_OPTION.NONE:
                this.mValue = null;
                break;
            case TYPE_OPTION.DISPLAY:
            case TYPE_OPTION.FLIP_HORIZONAL:
            case TYPE_OPTION.FLIP_VERTICAL:
                this.mValue = Convert.ToBoolean(clValue);
                break;
            case TYPE_OPTION.POSITION_X:
            case TYPE_OPTION.POSITION_Y:
            case TYPE_OPTION.ROTATION_Z:
            case TYPE_OPTION.SCALE_X:
            case TYPE_OPTION.SCALE_Y:
            case TYPE_OPTION.OFFSET_X:
            case TYPE_OPTION.OFFSET_Y:
            case TYPE_OPTION.TRANSPARENCY:
                this.mValue = Convert.ToSingle(clValue);
                break;
            case TYPE_OPTION.COLOR:
                this.mValue = Convert.ToUInt32(clValue);
                break;
            case TYPE_OPTION.USER_DATA:
                this.mValue = clValue;
                break;
            }

            //以下、各管理クラス作成処理
            foreach (XmlNode clNode in clListNode)
            {
                if ("KeyFrame".Equals(clNode.Name))
                {
                    ClsDatKeyFrame clDatKeyFrame = new ClsDatKeyFrame(0);
                    clDatKeyFrame.Load(clNode);

                    this.mDicKeyFrame[clDatKeyFrame.mFrame] = clDatKeyFrame;
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
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Value", this.mValue.ToString());

            //以下、キーフレーム保存処理
            foreach (int inKey in this.mDicKeyFrame.Keys)
            {
                ClsDatKeyFrame clDatKeyFrame = this.mDicKeyFrame[inKey];
                clDatKeyFrame.Save(clHeader + ClsSystem.FILE_TAG);
            }

            ClsTool.AppendElementEnd(clHeader, "Option");
        }

        /// <summary>
        /// オプションタイプから文字列に変更する処理
        /// </summary>
        /// <param name="enType">オプションタイプ</param>
        /// <returns>オプションタイプの名称</returns>
        public static string CnvType2Name(TYPE_OPTION enType)
        {
            string clName = "";

            switch (enType)
            {
            case TYPE_OPTION.DISPLAY:
                clName = "Display";
                break;
            case TYPE_OPTION.POSITION_X:
                clName = "Position X";
                break;
            case TYPE_OPTION.POSITION_Y:
                clName = "Position Y";
                break;
            case TYPE_OPTION.ROTATION_Z:
                clName = "Rotation";
                break;
            case TYPE_OPTION.SCALE_X:
                clName = "Scale X";
                break;
            case TYPE_OPTION.SCALE_Y:
                clName = "Scale Y";
                break;
            case TYPE_OPTION.TRANSPARENCY:
                clName = "Transparency";
                break;
            case TYPE_OPTION.FLIP_HORIZONAL:
                clName = "Horizontal flip";
                break;
            case TYPE_OPTION.FLIP_VERTICAL:
                clName = "Vertical flip";
                break;
            case TYPE_OPTION.COLOR:
                clName = "Color";
                break;
            case TYPE_OPTION.OFFSET_X:
                clName = "Offset X";
                break;
            case TYPE_OPTION.OFFSET_Y:
                clName = "Offset Y";
                break;
            case TYPE_OPTION.USER_DATA:
                clName = "User data(text)";
                break;
            default:
                break;
            }

            return (clName);
        }

        /// <summary>
        /// 表示するオプション種別かチェックする処理
        /// </summary>
        /// <param name="enType">オプション種別</param>
        /// <returns>表示フラグ</returns>
        public static bool IsDraw(TYPE_OPTION enType)
        {
            if (enType == TYPE_OPTION.DISPLAY) return (false);

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
            string clName = ClsDatOption.CnvType2Name(this.mTypeOption);
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
        /// <param name="inSelectFrame">選択中のフレーム</param>
        /// <param name="inWidth">描画先の幅</param>
        /// <param name="inHeight">描画先の高さ</param>
        public void DrawTime(Graphics g, int inSelectLineNo, int inSelectFrame, int inWidth, int inHeight)
        {
            //以下、表示しなくてはいけない種別かどうかチェックする処理
            bool isDraw = ClsDatOption.IsDraw(this.mTypeOption);
            if (!isDraw) return;

            SolidBrush clBrush = null;
            int inX = inSelectFrame * FormControl.CELL_WIDTH;
            int inY = this.mLineNo * FormControl.CELL_HEIGHT;

            //以下、背景を塗る処理
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

            //以下、選択中のフレーム描画処理
            if (inSelectLineNo == this.mLineNo)
            {
                clBrush = new SolidBrush(Color.Green);
            }
            else
            {
                clBrush = new SolidBrush(Color.DarkGreen);
            }
            g.FillRectangle(clBrush, inX, inY, FormControl.CELL_WIDTH, FormControl.CELL_HEIGHT);

            //以下、境界線描画処理
            Pen clPen = new Pen(Color.Green);
            inY = this.mLineNo * FormControl.CELL_HEIGHT + FormControl.CELL_HEIGHT - 1;
            g.DrawLine(clPen, 0, inY, inWidth, inY);

            //以下、0フレーム目のマーカー表示処理
            g.DrawImage(Properties.Resources.markRed, 2, this.mLineNo * FormControl.CELL_HEIGHT + 1);

            //以下、オプション表示処理
            foreach (int inKey in this.mDicKeyFrame.Keys)
            {
                ClsDatKeyFrame clKeyFrame = this.mDicKeyFrame[inKey];
                if (clKeyFrame == null) continue;

                g.DrawImage(Properties.Resources.markRed, inKey * FormControl.CELL_WIDTH + 2, this.mLineNo * FormControl.CELL_HEIGHT + 1);
            }
        }
    }
}