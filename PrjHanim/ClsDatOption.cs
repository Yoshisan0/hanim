using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsDatOption : ClsDatItem
    {
        public enum TYPE
        {
            NONE,
            POSITION_X,
            POSITION_Y,
            ROTATION,
            SCALE_X,
            SCALE_Y,
            TRANSPARENCY,
            FLIP_HORIZONAL,
            FLIP_VERTICAL,
            DISPLAY,
            COLOR,
            OFFSET_X,
            OFFSET_Y,
            USER_DATA,
        }

        public TYPE mType;  //タイプ
        public List<ClsDatKeyFrame> mListKeyFrame;  //フレーム数分Countが存在する（ null は存在しない事にする）

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="enType">オプションタイプ</param>
        public ClsDatOption(TYPE enType)
        {
            this.mType = enType;
            this.mListKeyFrame = new List<ClsDatKeyFrame>();
        }

        /// <summary>
        /// オプションの全てを削除する処理
        /// ※これを読んだ後は ClsDatMotion.Assignment を呼んで行番号を割り振りなおさなければならない
        /// </summary>
        public void RemoveAll()
        {
            //以下、キーフレーム全削除処理
            int inCnt, inMax = this.mListKeyFrame.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatKeyFrame clKeyFrame = this.mListKeyFrame[inCnt];
                clKeyFrame.RemoveAll();
            }
            this.mListKeyFrame.Clear();
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
        /// オプションタイプから文字列に変更する処理
        /// </summary>
        /// <param name="enType">オプションタイプ</param>
        /// <returns>オプションタイプの名称</returns>
        public static string CnvType2Name(TYPE enType)
        {
            string clName = "";

            switch (enType) {
            case TYPE.POSITION_X:
                clName = "Position X";
                break;
            case TYPE.POSITION_Y:
                clName = "Position Y";
                break;
            case TYPE.ROTATION:
                clName = "Rotation";
                break;
            case TYPE.SCALE_X:
                clName = "Scale X";
                break;
            case TYPE.SCALE_Y:
                clName = "Scale Y";
                break;
            case TYPE.TRANSPARENCY:
                clName = "Transparency";
                break;
            case TYPE.FLIP_HORIZONAL:
                clName = "Horizontal flip";
                break;
            case TYPE.FLIP_VERTICAL:
                clName = "Vertical flip";
                break;
            case TYPE.DISPLAY:
                clName = "Display";
                break;
            case TYPE.COLOR:
                clName = "Color";
                break;
            case TYPE.OFFSET_X:
                clName = "Offset X";
                break;
            case TYPE.OFFSET_Y:
                clName = "Offset Y";
                break;
            case TYPE.USER_DATA:
                clName = "User data(text)";
                break;
            default:
                break;
            }

            return (clName);
        }

        /// <summary>
        /// フレーム数変更処理
        /// </summary>
        /// <param name="inFrameNum">フレーム数</param>
        public void SetFrameNum(int inFrameNum)
        {
            if (inFrameNum <= 0) return;
            if (inFrameNum > 65535) return; //あまりに大きな値は怪しいのでバグ対策としてはじく

            int inFrameNumNow = this.mListKeyFrame.Count;
            if (inFrameNumNow == inFrameNum) return;

            if (inFrameNumNow < inFrameNum)
            {
                //以下、フレーム数を増やす処理
                while (this.mListKeyFrame.Count < inFrameNum)
                {
                    this.mListKeyFrame.Add(null);
                }
            }
            else
            {
                //以下、フレーム数を減らす処理
                while (this.mListKeyFrame.Count > inFrameNum)
                {
                    int inIndex = this.mListKeyFrame.Count - 1;
                    ClsDatKeyFrame clKeyFrame = this.mListKeyFrame[inIndex];
                    clKeyFrame.RemoveAll();
                    this.mListKeyFrame.RemoveAt(inIndex);
                }
            }
        }

        /// <summary>
        /// 行番号割り振り処理
        /// </summary>
        /// <param name="clMotion">モーション管理クラス</param>
        public void Assignment(ClsDatMotion clMotion)
        {
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
            string clName = ClsDatOption.CnvType2Name(this.mType);
            if (!string.IsNullOrEmpty(clName))
            {
                g.DrawString(clName, clFont, Brushes.White, 2 + 48, this.mLineNo * FormControl.CELL_HEIGHT + 2);
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
        }
    }
}