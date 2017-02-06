using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;

namespace PrjHikariwoAnim
{
    public class ClsDatElem : ClsDatItem
    {
        public static readonly int MAX_NAME = 16;   //エレメントの名前は最大16文字

        // コントロール左側ペインに相当する部分
        public enum ELEMENTS_TYPE
        {
            Image,
            Shape,
            Joint,
            Effect,
            Accessory,
            FX
        }

        public enum ELEMENTS_STYLE
        {
            Rect,
            Circle,
            Point
        }

        public enum ELEMENTS_MARK
        {
            NONE,
            UP,
            IN
        }

        public ClsDatMotion mMotion;        //親モーション
        public ClsDatElem mElem;            //親エレメント
        public List<ClsDatElem> mListElem;  //子エレメント
        public string mName;                //エレメント名
        public ELEMENTS_TYPE mType;         //Default Image
        public ELEMENTS_STYLE mStyle;       //Default Rect
        public bool isVisible;              //表示非表示(目)
        public bool isLocked;               //ロック状態(鍵)
        public bool isOpen;                 //属性開閉状態(+-)
        public int mImageChipID;            //イメージID
        public Dictionary<ClsDatOption.TYPE_OPTION, ClsDatOption> mDicOption;  //キーはアトリビュートのタイプ 値はオプション管理クラス
        public AttributeBase mAttInit;      //初期情報
        public ELEMENTS_MARK mInsertMark = ELEMENTS_MARK.NONE;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="clFileElem">親エレメントの保存データ</param>
        /// <param name="clMotion">親モーション</param>
        /// <param name="clElem">親エレメント</param>
        public ClsDatElem(ClsFileElem clFileElem, ClsDatMotion clMotion, ClsDatElem clElem)
        {
            this.mTypeItem = TYPE_ITEM.ELEM;

            this.mMotion = clMotion;
            this.mElem = clElem;
            this.mListElem = new List<ClsDatElem>();
            this.mName = clFileElem.mName;
            this.mType = ELEMENTS_TYPE.Image;
            this.mStyle = ELEMENTS_STYLE.Rect;
            this.isVisible = clFileElem.isVisible;  //表示非表示(目)
            this.isLocked = clFileElem.isLocked;    //ロック状態(鍵)
            this.isOpen = clFileElem.isOpen;        //属性開閉状態(+-)
            this.mAttInit = new AttributeBase();
            this.mImageChipID = clFileElem.mImageChipID;

            this.mDicOption = new Dictionary<ClsDatOption.TYPE_OPTION, ClsDatOption>();
            this.AddOption(ClsDatOption.TYPE_OPTION.DISPLAY);
            this.AddOption(ClsDatOption.TYPE_OPTION.POSITION_X);
            this.AddOption(ClsDatOption.TYPE_OPTION.POSITION_Y);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="clMotion">親モーション</param>
        /// <param name="clElem">親エレメント</param>
        public ClsDatElem(ClsDatMotion clMotion, ClsDatElem clElem)
        {
            this.mTypeItem = TYPE_ITEM.ELEM;

            this.mMotion = clMotion;
            this.mElem = clElem;
            this.mListElem = new List<ClsDatElem>();
            this.mName = this.GetHashCode().ToString("X8");//仮名
            this.mType = ELEMENTS_TYPE.Image;
            this.mStyle = ELEMENTS_STYLE.Rect;
            this.isVisible = true;  //表示非表示(目)
            this.isLocked = false;  //ロック状態(鍵)
            this.isOpen = false;    //属性開閉状態(+-)
            this.mAttInit = new AttributeBase();
            this.mImageChipID = 0;

            this.mDicOption = new Dictionary<ClsDatOption.TYPE_OPTION, ClsDatOption>();
            this.AddOption(ClsDatOption.TYPE_OPTION.DISPLAY);
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
        /// ハッシュコードからエレメントを削除する処理
        /// ※これを読んだ後は ClsDatMotion.Assignment を呼んで行番号を割り振りなおさなければならない
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
        /// 行番号からエレメントを削除する処理
        /// ※これを読んだ後は ClsDatMotion.Assignment を呼んで行番号を割り振りなおさなければならない
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
                    if (isRemove)
                    {
                        clElem.RemoveAll();
                    }

                    this.mListElem.RemoveAt(inCnt);
                    return;
                }

                clElem.RemoveElemFromLineNo(inLineNo, isRemove);
            }
        }

        /// <summary>
        /// 行番号からオプションを削除する処理
        /// ※これを読んだ後は ClsDatMotion.Assignment を呼んで行番号を割り振りなおさなければならない
        /// </summary>
        /// <param name="inLineNo">行番号</param>
        /// <param name="isForce">強制フラグ</param>
        /// <param name="isRemove">実体削除フラグ</param>
        public void RemoveOptionFromLineNo(int inLineNo, bool isForce, bool isRemove)
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

                if (isRemove)
                {
                    clOption.RemoveAll();
                }

                this.mDicOption.Remove(enType);
                return;
            }

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.RemoveOptionFromLineNo(inLineNo, isForce, isRemove);
            }
        }

        /// <summary>
        /// モーションロード後にエレメント・オプション内構造の再構築を行う
        /// </summary>
        public void Restore()
        {
            //以下、子エレメントの親設定
            foreach (ClsDatElem clElem in this.mListElem)
            {
                clElem.mElem = this;
                clElem.Restore();
            }

            //以下、オプションの親設定
            foreach (ClsDatOption.TYPE_OPTION enType in this.mDicOption.Keys)
            {
                ClsDatOption clOption = this.mDicOption[enType];
                clOption.mElem = this;
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
        /// 保存処理
        /// </summary>
        /// <param name="inIndexParent">親のインデックス</param>
        /// <returns>出力テーブル</returns>
        public void Save(int inIndexParent)
        {
            int inIndexElem = ClsSystem.mFileData.AddElem(inIndexParent, this);

            foreach (ClsDatOption.TYPE_OPTION enType in this.mDicOption.Keys)
            {
                ClsDatOption clDatOption = this.mDicOption[enType];
                clDatOption.Save(inIndexElem);
            }
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
        /// エレメントをこのエレメントの兄として登録する処理
        /// </summary>
        /// <param name="clElem"></param>
        public void AddElemBigBrother(ClsDatElem clElem)
        {
            if (clElem == null) return;

            //以下、自分を自分の兄として登録するのを回避する処理
            int inHashCode1 = this.GetHashCode();
            int inHashCode2 = clElem.GetHashCode();
            if (inHashCode1 == inHashCode2)
            {
                Console.WriteLine("Error AddElemBigBrother");
                return;
            }

            //以下、現在の親から削除する処理（関連付けの変更だけとする）
            int inHashCode = clElem.GetHashCode();
            if (clElem.mElem == null)
            {
                this.mMotion.RemoveElemFromHashCode(inHashCode, false);
            }
            else
            {
                clElem.mElem.RemoveElemFromHashCode(inHashCode, false);
            }

            //以下、自分の兄・自分の親の子供として登録する処理
            List<ClsDatElem> clListElem = null;
            if (this.mElem == null)
            {
                clListElem = this.mMotion.mListElem;
            }
            else
            {
                clListElem = this.mElem.mListElem;
            }
            inHashCode = this.GetHashCode();
            int inCnt;
            for (inCnt = 0; inCnt < clListElem.Count; inCnt++) {
                int inHashCodeTmp = clListElem[inCnt].GetHashCode();
                if (inHashCodeTmp != inHashCode) continue;  //自分を探す処理

                //以下、自分の兄として登録する処理
                clListElem.Insert(inCnt, clElem);
                break;
            }

            //以下、親として自分の親を登録する処理
            clElem.mElem = this.mElem;
        }

        /// <summary>
        /// エレメントを子供として追加する処理
        /// </summary>
        /// <param name="clElem">エレメント</param>
        public void AddElemChild(ClsDatElem clElem)
        {
            if (clElem == null) return;

            //以下、自分を自分の子供として登録するのを回避する処理
            int inHashCode1 = this.GetHashCode();
            int inHashCode2 = clElem.GetHashCode();
            if (inHashCode1 == inHashCode2)
            {
                Console.WriteLine("Error AddElemChild");
                return;
            }

            //以下、現在の親から削除する処理（関連付けの変更だけとする）
            int inHashCode = clElem.GetHashCode();
            if (clElem.mElem == null)
            {
                this.mMotion.RemoveElemFromHashCode(inHashCode, false);
            }
            else
            {
                clElem.mElem.RemoveElemFromHashCode(inHashCode, false);
            }

            //以下、子供に登録する処理
            this.mListElem.Insert(0, clElem);

            //以下、親として自分を登録する処理
            clElem.mElem = this;
        }

        /// <summary>
        /// オプション追加処理
        /// </summary>
        /// <param name="enTypeOption">オプションのタイプ</param>
        public void AddOption(ClsDatOption.TYPE_OPTION enTypeOption)
        {
            //以下、オプション追加処理
            bool isExist = this.mDicOption.ContainsKey(enTypeOption);
            if (!isExist) {
                this.mDicOption[enTypeOption] = new ClsDatOption(this, enTypeOption);
            }
        }

        /// <summary>
        /// オプション削除処理
        /// </summary>
        /// <param name="enTypeOption">オプションのタイプ</param>
        /// <param name="isForce">強制削除フラグ</param>
        public void RemoveOption(ClsDatOption.TYPE_OPTION enTypeOption, bool isForce)
        {
            //以下、オプション削除処理
            bool isExist = this.mDicOption.ContainsKey(enTypeOption);
            if (isExist)
            {
                ClsDatOption clOption = this.mDicOption[enTypeOption] as ClsDatOption;
                bool isRemoveOK = (isForce) ? true : clOption.IsRemoveOK();
                if (isRemoveOK)
                {
                    clOption.RemoveAll();
                    this.mDicOption.Remove(enTypeOption);
                }
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
        /// 行番号割り振り処理
        /// </summary>
        /// <param name="clMotion">モーション管理クラス</param>
        /// <param name="inTab">タブ値</param>
        public void Assignment(ClsDatMotion clMotion, int inTab)
        {
            this.mLineNo = clMotion.mWorkLineNo;
            clMotion.mWorkLineNo++;

            if (this.isOpen)
            {
                foreach (ClsDatOption.TYPE_OPTION enType in this.mDicOption.Keys)
                {
                    ClsDatOption clOption = this.mDicOption[enType];
                    clOption.mTab = inTab;  //タブ値設定
                    clOption.Assignment(clMotion);
                }
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
        /// 挿入可能マークからエレメントを取得する処理
        /// </summary>
        /// <param name="clMotion">モーション管理クラス</param>
        /// <param name="enMark">挿入可能マーク</param>
        public void FindElemFromMark(ClsDatMotion clMotion, ELEMENTS_MARK enMark)
        {
            if (this.mInsertMark == enMark)
            {
                clMotion.mWorkElem = this;
                return;
            }

            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                if (clElem.mInsertMark == enMark)
                {
                    clMotion.mWorkElem = clElem;
                    return;
                }

                clElem.FindElemFromMark(clMotion, enMark);
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
                    clMotion.mWorkElem = clElem;
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
            if (this.isOpen)
            {
                foreach (ClsDatOption.TYPE_OPTION enType in this.mDicOption.Keys)
                {
                    ClsDatOption clOption = this.mDicOption[enType];
                    if (clOption.mLineNo != inLineNo) continue;

                    clMotion.mWorkOption = clOption;
                    return;
                }
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
            //以下、自分をチェックする処理
            if (this.mLineNo == inLineNo)
            {
                clMotion.mWorkItem = this;
                return;
            }

            //以下、子供のオプションをチェックする処理
            if (this.isOpen)
            {
                foreach (ClsDatOption.TYPE_OPTION enType in this.mDicOption.Keys)
                {
                    ClsDatOption clOption = this.mDicOption[enType];
                    if (clOption.mLineNo == inLineNo)
                    {
                        clMotion.mWorkItem = clOption;
                        return;
                    }
                }
            }

            //以下、子供のエレメントをチェックする処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.FindItemFromLineNo(clMotion, inLineNo);
                if (clMotion.mWorkItem != null)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// ハッシュコードからアイテム取得する処理
        /// </summary>
        /// <param name="clMotion">モーション管理クラス</param>
        /// <param name="inHashCode">ハッシュコード</param>
        public void FindItemFromHashCode(ClsDatMotion clMotion, int inHashCode)
        {
            //以下、自分をチェックする処理
            if (this.GetHashCode() == inHashCode)
            {
                clMotion.mWorkItem = this;
                return;
            }

            //以下、子供のオプションをチェックする処理
            if (this.isOpen)
            {
                foreach (ClsDatOption.TYPE_OPTION enType in this.mDicOption.Keys)
                {
                    ClsDatOption clOption = this.mDicOption[enType];
                    if (clOption.GetHashCode() == inHashCode)
                    {
                        clMotion.mWorkItem = clOption;
                        return;
                    }
                }
            }

            //以下、子供のエレメントをチェックする処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.FindItemFromHashCode(clMotion, inHashCode);
                if (clMotion.mWorkItem != null)
                {
                    return;
                }
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
            ImageChip c = ClsSystem.ImageMan.GetImageChipFromID(this.mImageChipID);
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
            if (this.mInsertMark== ELEMENTS_MARK.IN)
            {
                //以下、挿入可能エレメント描画処理
                SolidBrush sb = new SolidBrush(Color.Orange);
                g.FillRectangle(sb, 0, this.mLineNo * FormControl.CELL_HEIGHT, inWidth, FormControl.CELL_HEIGHT);
            }
            else
            {
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
            }

            //以下、「目」アイコン表示処理
            if (this.isVisible)
            {
                g.DrawImage(Properties.Resources.see, 1, this.mLineNo * FormControl.CELL_HEIGHT + 1);
            }
            else
            {
                g.DrawImage(Properties.Resources.unSee, 1, this.mLineNo * FormControl.CELL_HEIGHT + 1);
            }

            //以下、「鍵」アイコン表示処理
            if (this.isLocked)
            {
                g.DrawImage(Properties.Resources.locked, 18, this.mLineNo * FormControl.CELL_HEIGHT + 1);
            }
            else
            {
                g.DrawImage(Properties.Resources.unLock, 18, this.mLineNo * FormControl.CELL_HEIGHT + 1);
            }

            //以下、「開閉」アイコン表示処理
            if (this.isOpen)
            {
                g.DrawImage(Properties.Resources.minus, 35, this.mLineNo * FormControl.CELL_HEIGHT + 1);
            }
            else
            {
                g.DrawImage(Properties.Resources.plus, 35, this.mLineNo * FormControl.CELL_HEIGHT + 1);
            }

            //以下、名前描画処理
            if (!string.IsNullOrEmpty(this.mName))
            {
                string clBlank = this.GetTabBlank();
                g.DrawString(clBlank + this.mName, clFont, Brushes.White, 52, this.mLineNo * FormControl.CELL_HEIGHT + 2);
            }

            //以下、挿入可能ライン描画処理
            if (this.mInsertMark== ELEMENTS_MARK.UP)
            {
                g.DrawLine(Pens.Orange, 0, this.mLineNo * FormControl.CELL_HEIGHT - 1, inWidth, this.mLineNo * FormControl.CELL_HEIGHT - 1);
                g.DrawLine(Pens.Orange, 0, this.mLineNo * FormControl.CELL_HEIGHT, inWidth, this.mLineNo * FormControl.CELL_HEIGHT);
            }

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

            //以下、DISPLAYオプション表示処理
            int inCnt, inMax;
            bool isExist = this.mDicOption.ContainsKey(ClsDatOption.TYPE_OPTION.DISPLAY);
            if (isExist) {
                ClsDatOption clOption = this.mDicOption[ClsDatOption.TYPE_OPTION.DISPLAY];
                if (clOption != null) {
                    foreach (int inKey in clOption.mDicKeyFrame.Keys)
                    {
                        ClsDatKeyFrame clKeyFrame = clOption.mDicKeyFrame[inKey];
                        if (clKeyFrame == null) continue;

                        g.DrawImage(Properties.Resources.markRed, inKey * FormControl.CELL_WIDTH + 2, this.mLineNo * FormControl.CELL_HEIGHT + 1);
                    }
                }
            }

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
            inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.DrawTime(g, inSelectLineNo, inSelectFrame, inWidth, inHeight);
            }
        }

        /// <summary>
        /// 指定のエレメントが上に移動できるかチェックする処理
        /// </summary>
        /// <param name="clElem">エレメント</param>
        /// <returns>移動可能フラグ</returns>
        public bool CanMoveElemUp(ClsDatElem clElem)
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
        public bool CanMoveElemDown(ClsDatElem clElem)
        {
            if (clElem == null) return (false);
            if (this.mListElem == null) return (false);
            if (this.mListElem.Count <= 0) return (false);

            int inHashCode1 = clElem.GetHashCode();

            ClsDatElem clElemEnd = this.mListElem[this.mListElem.Count - 1] as ClsDatElem;
            int inHashCode2 = clElemEnd.GetHashCode();
            if (inHashCode1 == inHashCode2) return (false);

            return (true);
        }

        /// <summary>
        /// エレメント検索処理
        /// </summary>
        /// <param name="clElem">エレメント</param>
        /// <returns>インデックス</returns>
        public int FindIndexFromOption(ClsDatElem clElem)
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
        public void MoveElemUp(ClsDatElem clElem)
        {
            bool isCanMove = this.CanMoveElemUp(clElem);
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
        public void MoveElemDown(ClsDatElem clElem)
        {
            bool isCanMove = this.CanMoveElemUp(clElem);
            if (!isCanMove) return;

            int inIndex = this.FindIndexFromElem(clElem);
            if (inIndex < 0) return;

            //以下、一つ下と入れ替える処理
            ClsDatElem clElemTmp = this.mListElem[inIndex + 1];
            this.mListElem[inIndex + 1] = clElem;
            this.mListElem[inIndex] = clElemTmp;
        }

        /// <summary>
        /// 挿入可能マークのクリア
        /// </summary>
        public void ClearInsertMark()
        {
            this.mInsertMark = ELEMENTS_MARK.NONE;

            //以下、子エレメントの挿入マークを消す処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.ClearInsertMark();
            }
        }

        /// <summary>
        /// 挿入可能マークの設定
        /// </summary>
        /// <param name="enMark">挿入可能マーク</param>
        public void SetInsertMark(ELEMENTS_MARK enMark)
        {
            this.mInsertMark = enMark;
        }
    }
}
