using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsDatElem : ClsDatItem
    {
        public static readonly int MAX_NAME = 16;   //エレメントの名前は最大16文字

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

        public ClsDatMotion mMotion;        //親モーション
        public string mName;                //エレメント名
        public ELEMENTSTYPE mType;          //Default Image
        public ELEMENTSSTYLE mStyle;        //Default Rect
        public bool isVisible;              //表示非表示(目)
        public bool isLocked;               //ロック状態(鍵)
        public bool isOpen;                 //属性開閉状態(+-)
        public int ImageChipID;             //イメージID
        public List<ClsDatElem> mListElem;  //エレメント管理クラスのリスト
        public Dictionary<ClsDatOption.TYPE_OPTION, ClsDatOption> mDicOption;  //キーはアトリビュートのタイプ 値はオプション管理クラス
        public AttributeBase mAttInit;      //初期情報

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="clMotion">親モーション</param>
        public ClsDatElem(ClsDatMotion clMotion)
        {
            this.mTypeItem = TYPE_ITEM.ELEM;

            this.mMotion = clMotion;
            this.mName = this.GetHashCode().ToString("X8");//仮名
            this.mType = ELEMENTSTYPE.Image;
            this.mStyle = ELEMENTSSTYLE.Rect;
            this.isVisible = true;  //表示非表示(目)
            this.isLocked = false;  //ロック状態(鍵)
            this.isOpen = false;    //属性開閉状態(+-)
            this.mListElem = new List<ClsDatElem>();
            this.mDicOption = new Dictionary<ClsDatOption.TYPE_OPTION, ClsDatOption>();
            this.mAttInit = new AttributeBase();

            this.AddOption(ClsDatOption.TYPE_OPTION.POSITION_X);
            this.AddOption(ClsDatOption.TYPE_OPTION.POSITION_Y);
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
            foreach (ClsDatOption.TYPE_OPTION enKey in this.mDicOption.Keys)
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
        /// <param name="isForce">強制フラグ</param>
        public void RemoveOptionFromLineNo(int inLineNo, bool isForce)
        {
            if (inLineNo < 0) return;
            if (!this.isOpen) return;

            foreach (ClsDatOption.TYPE_OPTION enType in this.mDicOption.Keys)
            {
                ClsDatOption clOption = this.mDicOption[enType];
                if (clOption.mLineNo != inLineNo) continue;

                if (!isForce)
                {
                    bool isRemoveOK = clOption.IsRemoveOK();
                    if (!isRemoveOK) continue;
                }

                clOption.RemoveAll();
                this.mDicOption.Remove(enType);
                return;
            }

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.RemoveOptionFromLineNo(inLineNo, isForce);
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
        /// エレメント名取得処理
        /// </summary>
        /// <returns>エレメント名</returns>
        public string GetName()
        {
            return (this.mName);
        }

        /// <summary>
        /// オプション追加処理
        /// </summary>
        /// <param name="enType">オプションのタイプ</param>
        public void AddOption(ClsDatOption.TYPE_OPTION enType)
        {
            //以下、オプション追加処理
            bool isExist = this.mDicOption.ContainsKey(enType);
            if (!isExist) {
                this.mDicOption[enType] = new ClsDatOption(this, enType);
            }
        }

        /// <summary>
        /// ハッシュコードからエレメント管理クラスを検索する処理
        /// </summary>
        /// <param name="inHashCode">ハッシュコード</param>
        /// <returns>エレメント管理クラス</returns>
        public ClsDatElem GetElemFromHashCode(int inHashCode)
        {
            if (inHashCode < 0) return (null);

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt] as ClsDatElem;
                int inHashCodeTmp = clElem.GetHashCode();
                if (inHashCodeTmp == inHashCode) return (clElem);

                clElem = clElem.GetElemFromHashCode(inHashCode);
                if (clElem != null) return (clElem);
            }

            return (null);
        }

        /// <summary>
        /// ライン番号からエレメント管理クラスを検索する処理
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
        /// フレーム数変更処理
        /// </summary>
        /// <param name="inFrameNum">フレーム数</param>
        public void SetFrameNum(int inFrameNum)
        {
            foreach (ClsDatOption.TYPE_OPTION enType in this.mDicOption.Keys)
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

            foreach (ClsDatOption.TYPE_OPTION enType in this.mDicOption.Keys)
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
            if (this.mLineNo == inLineNo)
            {
                clMotion.mWorkElem = this;
                return;
            }

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

            foreach (ClsDatOption.TYPE_OPTION enType in this.mDicOption.Keys)
            {
                ClsDatOption clOption = this.mDicOption[enType];
                if (clOption.mLineNo != inLineNo) continue;

                clMotion.mWorkOption = clOption;
                return;
            }

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                if (clElem.mLineNo == inLineNo) return;  //Optionを検索したかったのだが、該当のItemがElementだった

                clElem.FindOptionFromLineNo(clMotion, inLineNo);
            }
        }

        /// <summary>
        /// 行番号からアイテム取得する処理
        /// </summary>
        /// <param name="clMotion">モーション管理クラス</param>
        /// <param name="inLineNo">行番号</param>
        public void FindItemFromLineNo(ClsDatMotion clMotion, int inLineNo)
        {
            if (this.mLineNo == inLineNo)
            {
                clMotion.mWorkItem = this;
                return;
            }

            if (!this.isOpen) return;   //開いていなかったら子供エレメントと子供オプションを見に行かない

            foreach (ClsDatOption.TYPE_OPTION enType in this.mDicOption.Keys)
            {
                ClsDatOption clOption = this.mDicOption[enType];
                if (clOption.mLineNo == inLineNo)
                {
                    clMotion.mWorkItem = clOption;
                    return;
                }
            }

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.FindOptionFromLineNo(clMotion, inLineNo);
            }
        }

        /// <summary>
        /// パーツの描画処理
        /// </summary>
        /// <param name="g">描画管理クラス</param>
        /// <param name="inCX">中心Ｘ座標</param>
        /// <param name="inCY">中心Ｙ座標</param>
        public void DrawPreview(Graphics g, int inCX, int inCY)
        {
            AttributeBase atr = this.mAttInit;

            Matrix Back = g.Transform;
            Matrix MatObj = new Matrix();//今はgのMatrixを使っているので未使用

            //以後 将来親子関係が付く場合は親をあわせた処理にする事となる

            //スケールにあわせた部品の大きさを算出
            float vsx = atr.Width * atr.Scale.X;// * zoom;//SizeX 画面ズームは1段手前でmatrixで行っている
            float vsy = atr.Height * atr.Scale.Y;// * zoom;//SizeY

            //パーツの中心点
            float pcx = atr.Position.X + atr.Offset.X;
            float pcy = atr.Position.X + atr.Offset.X;
            Color Col = Color.FromArgb(atr.Color);

            //カラーマトリックス作成
            System.Drawing.Imaging.ColorMatrix colmat = new System.Drawing.Imaging.ColorMatrix();
            if (atr.isColor)
            {
                colmat.Matrix00 = (float)(Col.R * (atr.ColorRate / 100f));//Red  Col.R * Col.Rate
                colmat.Matrix11 = (float)(Col.G * (atr.ColorRate / 100f));//Green
                colmat.Matrix22 = (float)(Col.B * (atr.ColorRate / 100f));//Blue
            }
            else
            {
                colmat.Matrix00 = 1;//Red
                colmat.Matrix11 = 1;//Green
                colmat.Matrix22 = 1;//Blue
            }
            if (atr.isTransparrency)
            {
                colmat.Matrix33 = (atr.Transparency / 100f);
            }
            else
            {
                colmat.Matrix33 = 1;
            }
            colmat.Matrix44 = 1;//w
            System.Drawing.Imaging.ImageAttributes ia = new System.Drawing.Imaging.ImageAttributes();
            ia.SetColorMatrix(colmat);

            //Cell画像存在確認 画像の無いサポート部品の場合もありえるかも
            ImageChip c = ClsSystem.ImageMan.GetImageChipFromHash(this.ImageChipID);
            if (c == null) { Console.WriteLine("Image:null"); return; }

            //原点を部品中心に
            //g.TranslateTransform(   vcx + (atr.Position.X + atr.Width/2)  * atr.Scale.X *zoom,
            //                        vcy + (atr.Position.Y + atr.Height/2) * atr.Scale.Y *zoom);//部品中心座標か？

            //中心に平行移動
            g.TranslateTransform(inCX + atr.Position.X + atr.Offset.X, inCY + atr.Position.Y + atr.Offset.Y);

            //回転角指定
            g.RotateTransform(atr.Radius.Z);

            //スケーリング調
            g.ScaleTransform(atr.Scale.X, atr.Scale.X);
            //g.TranslateTransform(vcx + (atr.Position.X * atr.Scale.X), vcy + (atr.Position.Y * atr.Scale.Y));

            //MatObj.Translate(-(vcx + atr.Position.X +(atr.Width /2))*atr.Scale.X,-(vcy + atr.Position.Y +(atr.Height/2))*atr.Scale.Y,MatrixOrder.Append);
            //MatObj.Translate(0, 0);
            //MatObj.Scale(atr.Scale.X,atr.Scale.Y,MatrixOrder.Append);
            //MatObj.Rotate(atr.Radius.X,MatrixOrder.Append);
            //MatObj.Translate((vcx + atr.Position.X + (atr.Width / 2)) * atr.Scale.Y, (vcy + atr.Position.Y + (atr.Height / 2)) * atr.Scale.Y,MatrixOrder.Append);

            //g.TranslateTransform(vcx, vcy);
            //描画

            //g.DrawImage(c.Img,
            //    -(atr.Width  * atr.Scale.X * zoom )/2,
            //    -(atr.Height * atr.Scale.Y * zoom )/2,
            //    vsx,vsy);
            //g.DrawImage(c.Img,vcx+ (now.Position.X*zoom)-(vsx/2),vcy+ (now.Position.Y*zoom)-(vsy/2),vsx,vsy);
            //g.Transform = MatObj;

            //Draw
            if (atr.isTransparrency || atr.isColor)
            {
                g.DrawImage(c.Img, new Rectangle((int)(atr.Offset.X - (atr.Width * atr.Scale.X) / 2), (int)(atr.Offset.Y - (atr.Height * atr.Scale.Y) / 2), (int)vsx, (int)vsy), 0, 0, c.Img.Width, c.Img.Height, GraphicsUnit.Pixel, ia);
            }
            else
            {
                //透明化カラー補正なし
                g.DrawImage(c.Img, new Rectangle((int)(atr.Offset.X - (atr.Width * atr.Scale.X) / 2), (int)(atr.Offset.Y - (atr.Height * atr.Scale.Y) / 2), (int)vsx, (int)vsy));
            }

/*
            //Draw Helper
            if (checkBox_Helper.Checked)
            {
                //中心点やその他のサポート表示
                //CenterPosition
                g.DrawEllipse(Pens.OrangeRed, -4, -4, 8, 8);

                //Selected DrawBounds
                if (e.isSelect)
                {
                    g.DrawRectangle(Pens.DarkCyan, atr.Offset.X - (atr.Width * atr.Scale.X) / 2, atr.Offset.Y - (atr.Height * atr.Scale.Y) / 2, vsx - 1, vsy - 1);
                }
                //test Hit範囲をボックス描画
                //ElementsType
                if (e.Style == ELEMENTS.ELEMENTSSTYLE.Rect)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.Orange)), (-(atr.Width * atr.Scale.X) / 2), (-(atr.Height * atr.Scale.Y) / 2), vsx - 1, vsy - 1);
                }
                if (e.Style == ELEMENTS.ELEMENTSSTYLE.Circle)
                {
                    g.FillEllipse(new SolidBrush(Color.FromArgb(128, Color.Orange)), (-(atr.Width * atr.Scale.X) / 2), (-(atr.Height * atr.Scale.Y) / 2), vsx - 1, vsy - 1);
                }
            }
*/
            g.Transform = Back;//restore Matrix

            //Cuurent Draw Grip

            //以下、子供描画処理
            if (!this.isOpen) return;

            //以下、子供のエレメント描画処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.DrawPreview(g, inCX, inCY);
            }
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

            //以下、「目」アイコン表示処理
            if (this.isVisible)
            {
                g.DrawImage(Properties.Resources.see, 2, this.mLineNo * FormControl.CELL_HEIGHT);
            }
            else
            {
                g.DrawImage(Properties.Resources.unSee, 2, this.mLineNo * FormControl.CELL_HEIGHT);
            }

            //以下、「鍵」アイコン表示処理
            if (this.isLocked)
            {
                g.DrawImage(Properties.Resources.locked, 2 + 16, this.mLineNo * FormControl.CELL_HEIGHT);
            }
            else
            {
                g.DrawImage(Properties.Resources.unLock, 2 + 16, this.mLineNo * FormControl.CELL_HEIGHT);
            }

            //以下、「開閉」アイコン表示処理
            if (this.isOpen)
            {
                g.DrawImage(Properties.Resources.minus, 2 + 32, this.mLineNo * FormControl.CELL_HEIGHT);
            }
            else
            {
                g.DrawImage(Properties.Resources.plus, 2 + 32, this.mLineNo * FormControl.CELL_HEIGHT);
            }

            //以下、名前描画処理
            if (!string.IsNullOrEmpty(this.mName))
            {
                g.DrawString(this.mName, clFont, Brushes.White, 2 + 48, this.mLineNo * FormControl.CELL_HEIGHT + 2);
            }

            //g.FillRectangle(Brushes.Lime, new Rectangle(0, 0, 500, 500));

            //以下、オプション描画処理
            if (this.isOpen)
            {
                foreach (ClsDatOption.TYPE_OPTION enType in this.mDicOption.Keys)
                {
                    ClsDatOption clOption = this.mDicOption[enType];
                    clOption.DrawControl(g, inSelectLineNo, inWidth, inHeight, clFont);
                }
            }

            //以下、子供のエレメント描画処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.DrawControl(g, inSelectLineNo, inWidth, inHeight, clFont);
            }
        }

        /// <summary>
        /// エレメントのタイムライン描画処理
        /// </summary>
        /// <param name="g">描画管理クラス</param>
        /// <param name="inSelectLineNo">選択中のライン番号</param>
        /// <param name="inSelectFrame">選択中のフレーム</param>
        /// <param name="inWidth">描画先の幅</param>
        /// <param name="ginHeight">描画先の高さ</param>
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

            //以下、オプション描画処理
            if (this.isOpen)
            {
                foreach (ClsDatOption.TYPE_OPTION enType in this.mDicOption.Keys)
                {
                    ClsDatOption clOption = this.mDicOption[enType];
                    clOption.DrawTime(g, inSelectLineNo, inSelectFrame, inWidth, inHeight);
                }
            }

            //以下、子供のエレメント描画処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.DrawTime(g, inSelectLineNo, inSelectFrame, inWidth, inHeight);
            }
        }
    }
}
