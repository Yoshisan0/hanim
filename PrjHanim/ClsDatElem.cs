﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using Tao.OpenGl;

namespace PrjHikariwoAnim
{
    public class ClsDatElem : ClsDatItem
    {
        public static readonly int MAX_NAME = 16;   //エレメントの名前は最大16文字

        public ClsDatMotion mMotion;        //親モーション
        public ClsDatElem mElem;            //親エレメント
        public List<ClsDatElem> mListElem;  //子エレメント
        public string mName;                //エレメント名
        public bool isDisplay;              //表示非表示(目)
        public bool isLocked;               //ロック状態(鍵)
        public bool isOpen;                 //属性開閉状態(+-)
        public int mImageKey;               //イメージインデックス
        public Dictionary<TYPE_OPTION, ClsDatOption> mDicOption;  //キーはアトリビュートのタイプ 値はオプション管理クラス
        public MARK_ELEMENT mInsertMark = MARK_ELEMENT.NONE;

        //以下、ＵＶ値
        public ClsVector2[] mListUV;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="clMotion">親モーション</param>
        /// <param name="clElem">親エレメント</param>
        /// <param name="flX">Ｘ座標</param>
        /// <param name="flY">Ｙ座標</param>
        public ClsDatElem(ClsDatMotion clMotion, ClsDatElem clElem, float flX, float flY)
        {
            this.mTypeItem = TYPE_ITEM.ELEM;

            //以下、初期化処理
            this.mMotion = clMotion;
            this.mElem = clElem;
            this.mListElem = new List<ClsDatElem>();
            this.mName = this.GetHashCode().ToString("X8");//仮名
            this.isDisplay = true;  //表示非表示(目)
            this.isLocked = false;  //ロック状態(鍵)
            this.isOpen = false;    //属性開閉状態(+-)
            this.mImageKey = -1;

            this.mDicOption = new Dictionary<TYPE_OPTION, ClsDatOption>();
            bool isParentFlag = ClsParam.GetDefaultParentFlag(TYPE_OPTION.DISPLAY);
            object clValue1 = ClsParam.GetDefaultValue1(TYPE_OPTION.DISPLAY);
            object clValue2 = ClsParam.GetDefaultValue2(TYPE_OPTION.DISPLAY);
            this.SetOption(TYPE_OPTION.DISPLAY, isParentFlag, clValue1, clValue2);
            isParentFlag = ClsParam.GetDefaultParentFlag(TYPE_OPTION.POSITION);
            this.SetOption(TYPE_OPTION.POSITION, isParentFlag, flX, flY);

            //以下、UV値初期化処理
            this.mListUV = new ClsVector2[4];
            this.mListUV[0] = new ClsVector2(0.0f, 0.0f);
            this.mListUV[1] = new ClsVector2(0.0f, 1.0f);
            this.mListUV[2] = new ClsVector2(1.0f, 1.0f);
            this.mListUV[3] = new ClsVector2(1.0f, 0.0f);
        }

        /// <summary>
        /// イメージ設定処理
        /// </summary>
        /// <param name="clDatImage">イメージ管理クラス</param>
        public void SetImage(ClsDatImage clDatImage)
        {
            this.mImageKey = clDatImage.mID;
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
                ClsDatElem clDatElem = this.mListElem[inCnt];
                clDatElem.RemoveAll();
            }
            this.mListElem.Clear();

            //以下、オプション全削除処理
            foreach (TYPE_OPTION enTypeOption in this.mDicOption.Keys)
            {
                ClsDatOption clDatOption = this.mDicOption[enTypeOption];
                clDatOption.RemoveAll();
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
                ClsDatElem clDatElem = this.mListElem[inCnt];
                int inHashCodeTmp = clDatElem.GetHashCode();
                if (inHashCode == inHashCodeTmp)
                {
                    if (isRemove)
                    {
                        clDatElem.RemoveAll();
                    }

                    this.mListElem.RemoveAt(inCnt);
                    return;
                }

                clDatElem.RemoveElemFromHashCode(inHashCode, isRemove);
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
                ClsDatElem clDatElem = this.mListElem[inCnt];
                if (inLineNo == clDatElem.mLineNo)
                {
                    if (isRemove)
                    {
                        clDatElem.RemoveAll();
                    }

                    this.mListElem.RemoveAt(inCnt);
                    return;
                }

                clDatElem.RemoveElemFromLineNo(inLineNo, isRemove);
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

            foreach (TYPE_OPTION enTypeOption in this.mDicOption.Keys)
            {
                ClsDatOption clDatOption = this.mDicOption[enTypeOption];
                if (clDatOption.mLineNo != inLineNo) continue;

                if (!isForce)
                {
                    bool isRemoveOK = clDatOption.IsRemoveOK();
                    if (!isRemoveOK) continue;
                }

                if (isRemove)
                {
                    clDatOption.RemoveAll();
                }

                this.mDicOption.Remove(enTypeOption);
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
            foreach (ClsDatElem clDatElem in this.mListElem)
            {
                clDatElem.mElem = this;
                clDatElem.Restore();
            }

            //以下、オプションの親設定
            foreach (TYPE_OPTION enTypeOption in this.mDicOption.Keys)
            {
                ClsDatOption clDatOption = this.mDicOption[enTypeOption];
                clDatOption.mElem = this;
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
        /// 読み込み処理
        /// </summary>
        /// <param name="clXmlNode">xmlノード</param>
        public void Load(XmlNode clXmlNode)
        {
            XmlNodeList clListNode = clXmlNode.ChildNodes;
            this.mName = ClsTool.GetStringFromXmlNodeList(clListNode, "Name");
            this.isDisplay = ClsTool.GetBoolFromXmlNodeList(clListNode, "Visible");
            this.isLocked = ClsTool.GetBoolFromXmlNodeList(clListNode, "Locked");
            this.isOpen = ClsTool.GetBoolFromXmlNodeList(clListNode, "Open");
            this.mImageKey = ClsTool.GetIntFromXmlNodeList(clListNode, "ImageKey");

            //以下、各管理クラス作成処理
            foreach (XmlNode clNode in clListNode)
            {
                if ("Option".Equals(clNode.Name))
                {
                    bool isParentFlag = ClsParam.GetDefaultParentFlag(TYPE_OPTION.NONE);
                    ClsDatOption clDatOption = new ClsDatOption(null, TYPE_OPTION.NONE, isParentFlag, null, null);
                    clDatOption.Load(clNode);
                    clDatOption.mElem = this;

                    this.mDicOption[clDatOption.mTypeOption] = clDatOption;
                    continue;
                }

                if ("Elem".Equals(clNode.Name))
                {
                    ClsDatElem clDatElem = new ClsDatElem(null, this, 0.0f, 0.0f);
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
            //以下、エレメント保存処理
            ClsTool.AppendElementStart(clHeader, "Elem");
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Name", this.mName);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Visible", this.isDisplay);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Locked", this.isLocked);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Open", this.isOpen);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "ImageKey", this.mImageKey);

            //以下、オプションリスト保存処理
            foreach (TYPE_OPTION enTypeOption in this.mDicOption.Keys)
            {
                ClsDatOption clDatOption = this.mDicOption[enTypeOption];
                clDatOption.Save(clHeader + ClsSystem.FILE_TAG);
            }

            //以下、エレメントリスト保存処理
            foreach (ClsDatElem clDatElem in this.mListElem)
            {
                clDatElem.Save(clHeader + ClsSystem.FILE_TAG);
            }

            ClsTool.AppendElementEnd(clHeader, "Elem");
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
        /// オプション追加・更新処理
        /// </summary>
        /// <param name="enTypeOption">オプションのタイプ</param>
        /// <param name="isParentFlag">親の設定に依存するかどうか</param>
        /// <param name="clValue">値１</param>
        /// <param name="clValue">値２</param>
        public void SetOption(TYPE_OPTION enTypeOption, bool isParentFlag, object clValue1, object clValue2)
        {
            //以下、オプション追加処理
            bool isExist = this.mDicOption.ContainsKey(enTypeOption);
            if (isExist)
            {
                ClsDatOption clOption = this.mDicOption[enTypeOption];
                ClsDatKeyFrame clKeyFrame = clOption.GetKeyFrame(0);
                if (enTypeOption != clKeyFrame.mTypeOption) throw new Exception("type error.");
                clKeyFrame.mParentFlag = isParentFlag;
                clKeyFrame.mValue1 = clValue1;
                clKeyFrame.mValue2 = clValue2;
            }
            else
            {
                this.mDicOption[enTypeOption] = new ClsDatOption(this, enTypeOption, isParentFlag, clValue1, clValue2);
            }
        }

        /// <summary>
        /// オプション存在チェック
        /// </summary>
        /// <param name="enTypeOption">オプションタイプ</param>
        /// <returns>存在フラグ</returns>
        public bool IsExistOption(TYPE_OPTION enTypeOption)
        {
            bool isExist = this.mDicOption.ContainsKey(enTypeOption);
            return (isExist);
        }

        /// <summary>
        /// オプション取得処理
        /// </summary>
        /// <param name="enTypeOption">オプションタイプ</param>
        public ClsDatOption GetOption(TYPE_OPTION enTypeOption)
        {
            //以下、オプション追加処理
            bool isExist = this.mDicOption.ContainsKey(enTypeOption);
            if (!isExist) return (null);

            ClsDatOption clOption = this.mDicOption[enTypeOption];
            return (clOption);
        }

        /// <summary>
        /// キーフレーム番号を取得する
        /// </summary>
        /// <param name="enTypeOption">オプションタイプ</param>
        /// <param name="inFrameNo">現在のフレーム番号</param>
        /// <param name="inMaxFrameNo">フレーム数</param>
        /// <param name="inFrameNoBefore">前のフレーム番号</param>
        /// <param name="inFrameNoAfter">後のフレーム番号</param>
        private void GetKeyFrameNo(TYPE_OPTION enTypeOption, int inFrameNo, int inMaxFrameNo, out int inFrameNoBefore, out int inFrameNoAfter)
        {
            inFrameNoBefore = inFrameNo;
            inFrameNoAfter = inFrameNo;

            //以下、オプション追加処理
            ClsDatOption clOption = this.GetOption(enTypeOption);
            if (clOption == null) return;

            bool isExist = clOption.IsExistKeyFrame(inFrameNo);
            if (isExist) return;

            for (; inFrameNoBefore >= 0; inFrameNoBefore--)
            {
                isExist = clOption.IsExistKeyFrame(inFrameNoBefore);
                if (isExist) break;
            }

            for (; inFrameNoAfter < inMaxFrameNo; inFrameNoAfter++)
            {
                isExist = clOption.IsExistKeyFrame(inFrameNoAfter);
                if (isExist) break;
            }
        }

        /// <summary>
        /// フレーム番号のオプションの値を取得する処理
        /// </summary>
        /// <param name="enTypeOption">オプションのタイプ</param>
        /// <param name="inFrameNo">フレーム番号</param>
        /// <param name="inMaxFrameNum">フレーム数</param>
        /// <param name="isParentFlag">親の設定に依存するかどうか</param>
        /// <param name="clValue1">値１</param>
        /// <param name="clValue2">値２</param>
        private void GetOptionValueNow(TYPE_OPTION enTypeOption, int inFrameNo, int inMaxFrameNum, out bool isParentFlag, out object clValue1, out object clValue2)
        {
            isParentFlag = ClsParam.GetDefaultParentFlag(enTypeOption);
            clValue1 = ClsParam.GetDefaultValue1(enTypeOption);
            clValue2 = ClsParam.GetDefaultValue2(enTypeOption);

            //以下、オプション追加処理
            ClsDatOption clOption = this.GetOption(enTypeOption);
            if (clOption == null) return;

            bool isExist = clOption.IsExistKeyFrame(inFrameNo);
            if (!isExist)
            {
                int inFrameNoBefore;
                int inFrameNoAfter;
                this.GetKeyFrameNo(enTypeOption, inFrameNo, inMaxFrameNum, out inFrameNoBefore, out inFrameNoAfter);
                inFrameNo = inFrameNoBefore;
            }

            ClsDatKeyFrame clKeyFrame = clOption.GetKeyFrame(inFrameNo);
            isParentFlag = clKeyFrame.mParentFlag;
            clValue1 = clKeyFrame.mValue1;
            clValue2 = clKeyFrame.mValue2;
        }

        /// <summary>
        /// オプション削除処理
        /// </summary>
        /// <param name="enTypeOption">オプションのタイプ</param>
        /// <param name="isForce">強制削除フラグ</param>
        public void RemoveOption(TYPE_OPTION enTypeOption, bool isForce)
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
        /// キーフレーム存在チェック
        /// </summary>
        /// <param name="enTypeOption">オプションのタイプ</param>
        /// <param name="inFrameNo">フレーム番号</param>
        /// <returns>キーフレーム存在フラグ</returns>
        private bool IsExistKeyFrame(TYPE_OPTION enTypeOption, int inFrameNo)
        {
            //以下、オプション追加処理
            ClsDatOption clOption = this.GetOption(enTypeOption);
            if (clOption == null) return (false);

            bool isExist = clOption.IsExistKeyFrame(inFrameNo);
            return (isExist);
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
                foreach (TYPE_OPTION enTypeOption in Enum.GetValues(typeof(TYPE_OPTION)))
                {
                    bool isExist = this.mDicOption.ContainsKey(enTypeOption);
                    if (!isExist) continue;

                    ClsDatOption clOption = this.mDicOption[enTypeOption];
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
                foreach (TYPE_OPTION enTypeOption in this.mDicOption.Keys)
                {
                    ClsDatOption clOption = this.mDicOption[enTypeOption];
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
                foreach (TYPE_OPTION enTypeOption in this.mDicOption.Keys)
                {
                    ClsDatOption clOption = this.mDicOption[enTypeOption];
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
                foreach (TYPE_OPTION enTypeOption in this.mDicOption.Keys)
                {
                    ClsDatOption clOption = this.mDicOption[enTypeOption];
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
        /// オプションの情報をかき集めてまとめる処理
        /// ※Tweenの情報も含めた最新の情報となる
        /// </summary>
        /// <param name="inFrameNo">フレーム番号</param>
        /// <param name="inMaxFrameNum">フレーム数</param>
        /// <returns>パラメーター管理クラス</returns>
        public ClsParam GetParamNow(int inFrameNo, int inMaxFrameNum)
        {
            ClsParam clParam = new ClsParam();
            bool isParentFlag;
            object clValue1;
            object clValue2;

            clParam.mEnableDisplayKeyFrame = this.IsExistKeyFrame(TYPE_OPTION.DISPLAY, inFrameNo);
            this.GetOptionValueNow(TYPE_OPTION.DISPLAY, inFrameNo, inMaxFrameNum, out isParentFlag, out clValue1, out clValue2);
            clParam.mEnableDisplayParent = isParentFlag;
            clParam.mDisplay = Convert.ToBoolean(clValue1);

            clParam.mEnablePositionKeyFrame = this.IsExistKeyFrame(TYPE_OPTION.POSITION, inFrameNo);
            this.GetOptionValueNow(TYPE_OPTION.POSITION, inFrameNo, inMaxFrameNum, out isParentFlag, out clValue1, out clValue2);
            clParam.mEnablePositionParent = isParentFlag;
            clParam.mX = Convert.ToSingle(clValue1);
            clParam.mY = Convert.ToSingle(clValue2);

            clParam.mEnableRotationOption = this.IsExistOption(TYPE_OPTION.ROTATION);
            clParam.mEnableRotationKeyFrame = this.IsExistKeyFrame(TYPE_OPTION.ROTATION, inFrameNo);
            this.GetOptionValueNow(TYPE_OPTION.ROTATION, inFrameNo, inMaxFrameNum, out isParentFlag, out clValue1, out clValue2);
            clParam.mEnableRotationParent = isParentFlag;
            clParam.mRZ = Convert.ToSingle(clValue1);

            clParam.mEnableScaleOption = this.IsExistOption(TYPE_OPTION.SCALE);
            clParam.mEnableScaleKeyFrame = this.IsExistKeyFrame(TYPE_OPTION.SCALE, inFrameNo);
            this.GetOptionValueNow(TYPE_OPTION.SCALE, inFrameNo, inMaxFrameNum, out isParentFlag, out clValue1, out clValue2);
            clParam.mEnableScaleParent = isParentFlag;
            clParam.mSX = Convert.ToSingle(clValue1);
            clParam.mSY = Convert.ToSingle(clValue2);

            clParam.mEnableOffsetOption = this.IsExistOption(TYPE_OPTION.OFFSET);
            clParam.mEnableOffsetKeyFrame = this.IsExistKeyFrame(TYPE_OPTION.OFFSET, inFrameNo);
            this.GetOptionValueNow(TYPE_OPTION.OFFSET, inFrameNo, inMaxFrameNum, out isParentFlag, out clValue1, out clValue2);
            clParam.mEnableOffsetParent = isParentFlag;
            clParam.mCX = Convert.ToSingle(clValue1);
            clParam.mCY = Convert.ToSingle(clValue2);

            clParam.mEnableFlipOption = this.IsExistOption(TYPE_OPTION.FLIP);
            clParam.mEnableFlipKeyFrame = this.IsExistKeyFrame(TYPE_OPTION.FLIP, inFrameNo);
            this.GetOptionValueNow(TYPE_OPTION.FLIP, inFrameNo, inMaxFrameNum, out isParentFlag, out clValue1, out clValue2);
            clParam.mEnableFlipParent = isParentFlag;
            clParam.mFlipH = Convert.ToBoolean(clValue1);
            clParam.mFlipV = Convert.ToBoolean(clValue2);

            clParam.mEnableTransOption = this.IsExistOption(TYPE_OPTION.TRANSPARENCY);
            clParam.mEnableTransKeyFrame = this.IsExistKeyFrame(TYPE_OPTION.TRANSPARENCY, inFrameNo);
            this.GetOptionValueNow(TYPE_OPTION.TRANSPARENCY, inFrameNo, inMaxFrameNum, out isParentFlag, out clValue1, out clValue2);
            clParam.mEnableTransParent = isParentFlag;
            clParam.mTrans = Convert.ToInt32(clValue1);

            clParam.mEnableColorOption = this.IsExistOption(TYPE_OPTION.COLOR);
            clParam.mEnableColorKeyFrame = this.IsExistKeyFrame(TYPE_OPTION.COLOR, inFrameNo);
            this.GetOptionValueNow(TYPE_OPTION.COLOR, inFrameNo, inMaxFrameNum, out isParentFlag, out clValue1, out clValue2);
            clParam.mEnableColorParent = isParentFlag;
            clParam.mColor = Convert.ToInt32(clValue1);

            clParam.mEnableUserDataOption = this.IsExistOption(TYPE_OPTION.USER_DATA);
            clParam.mEnableUserDataKeyFrame = this.IsExistKeyFrame(TYPE_OPTION.USER_DATA, inFrameNo);
            this.GetOptionValueNow(TYPE_OPTION.USER_DATA, inFrameNo, inMaxFrameNum, out isParentFlag, out clValue1, out clValue2);
            clParam.mUserData = Convert.ToString(clValue1);

            return (clParam);
        }

        /// <summary>
        /// パーツの描画処理
        /// </summary>
        /// <param name="clGL">OpenGLコンポーネント</param>
        /// <param name="inFrameNo">フレーム番号</param>
        /// <param name="clParamParent">親のパラメータ</param>
        /// <param name="pflMatParent">親のマトリクス</param>
        public void DrawPreview(ComponentOpenGL clGL, int inFrameNo, int inMaxFrameNum, ClsParam clParamParent, float[] pflMatParent)
        {
            ClsParam clParamMe = new ClsParam();
            float[] pflMatMe = new float[16];

            //以下、現在の値をかき集めてまとめる処理
            ClsParam clParamNow = this.GetParamNow(inFrameNo, inMaxFrameNum);

            //以下、親元のパラメータとミックスする処理
            clParamMe.mDisplay = clParamNow.mDisplay;   //表示フラグ

            clParamMe.mX = clParamNow.mX;   //Ｘ座標（常に有効）
            clParamMe.mY = clParamNow.mY;   //Ｙ座標（常に有効）

            clParamMe.mRZ = clParamNow.mRZ; //回転値

            clParamMe.mSX = clParamNow.mSX; //スケールＸ
            clParamMe.mSY = clParamNow.mSY; //スケールＹ

            clParamMe.mCX = clParamNow.mCX; //オフセットＸ座標
            clParamMe.mCY = clParamNow.mCY; //オフセットＹ座標

            clParamMe.mFlipH = clParamNow.mFlipH; //水平反転フラグ

            clParamMe.mFlipV = clParamNow.mFlipV; //垂直反転フラグ

            int inTrans = clParamNow.mTrans;    //透明透明値0～255
            if (inTrans < 0) inTrans = 0;
            if (inTrans > 255) inTrans = 255;
            clParamMe.mTrans = inTrans;

            int inR = (clParamNow.mColor & 0x00FF0000) >> 16;
            int inG = (clParamNow.mColor & 0x0000FF00) >> 8;
            int inB = (clParamNow.mColor & 0x000000FF);
            if (inR < 0) inR = 0;
            if (inR > 255) inR = 255;
            if (inG < 0) inG = 0;
            if (inG > 255) inG = 255;
            if (inB < 0) inB = 0;
            if (inB > 255) inB = 255;
            clParamMe.mColor = (inR << 16) | (inG << 8) | inB;    //マテリアルカラー値（α無し RGBのみ）

            //以下、マテリアル設定
            clGL.SetMaterial(clParamMe);

            //以下、マトリクス設定
            pflMatMe = clGL.SetElemMatrix(clParamMe, pflMatParent);

            //以下、表示チェック処理
            if (clParamMe.mDisplay)
            {
                //以下、ポリゴン描画処理
                ClsDatImage clImage = ClsSystem.GetImage(this.mImageKey);
                if (clImage != null)
                {
                    //以下、テクスチャー設定
                    clGL.SetTexture(clImage.mListTex[0]);

                    //以下、ポリゴン描画
                    float flCX = clParamMe.mCX;
                    float flCY = clParamMe.mCY;

                    float flWidth = clImage.mImgOrigin.Width;
                    float flHeight = clImage.mImgOrigin.Height;
                    bool isFlipH = clParamMe.mFlipH;
                    bool isFlipV = clParamMe.mFlipV;
                    if (clImage.mRect == null)
                    {
                        clGL.DrawPolygon(this.mListUV, flCX, flCY, flWidth, flHeight, isFlipH, isFlipV);
                    }
                    else
                    {
                        //UVカットして表示する
                        clGL.DrawPolygon(this.mListUV, flCX, flCY, flWidth, flHeight, isFlipH, isFlipV);
                    }
                }
            }

            //以下、子供のエレメント描画処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.DrawPreview(clGL, inFrameNo, inMaxFrameNum, clParamMe, pflMatMe);
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
            if (this.mInsertMark == MARK_ELEMENT.IN)
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
            if (this.isDisplay)
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
            if (this.mInsertMark == MARK_ELEMENT.UP)
            {
                g.DrawLine(Pens.Orange, 0, this.mLineNo * FormControl.CELL_HEIGHT - 1, inWidth, this.mLineNo * FormControl.CELL_HEIGHT - 1);
                g.DrawLine(Pens.Orange, 0, this.mLineNo * FormControl.CELL_HEIGHT, inWidth, this.mLineNo * FormControl.CELL_HEIGHT);
            }

            //以下、オプション描画処理
            if (this.isOpen)
            {
                foreach (TYPE_OPTION enTypeOption in Enum.GetValues(typeof(TYPE_OPTION)))
                {
                    bool isExist = this.mDicOption.ContainsKey(enTypeOption);
                    if (!isExist) continue;

                    ClsDatOption clOption = this.mDicOption[enTypeOption];
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
        /// <param name="inSelectFrameNo">選択中のフレーム</param>
        /// <param name="inMaxFrameNum">最大フレーム数</param>
        /// <param name="inWidth">描画先の幅</param>
        /// <param name="ginHeight">描画先の高さ</param>
        public void DrawTime(Graphics g, int inSelectLineNo, int inSelectFrameNo, int inMaxFrameNum, int inWidth, int inHeight)
        {
            SolidBrush clBrush = null;
            int inX = inSelectFrameNo * FormControl.CELL_WIDTH;
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

            //以下、フレームの背景（親の影響を受けるかどうかと、Tweenの影響下にあるかどうか）を表示する処理
            bool isExist = this.mDicOption.ContainsKey(TYPE_OPTION.DISPLAY);
            if (isExist)
            {
                ClsDatOption clOption = this.mDicOption[TYPE_OPTION.DISPLAY];

                Color stColorParent = Color.FromArgb(128, Color.LightPink);
                SolidBrush clBrushParent = new SolidBrush(stColorParent);
                Color stColorTween = Color.FromArgb(128, Color.LightBlue);
                SolidBrush clBrushTween = new SolidBrush(stColorTween);
                bool isParentFlag = ClsParam.GetDefaultParentFlag(TYPE_OPTION.DISPLAY);
                int inFrameNo = 0;
                for (inFrameNo = 0; inFrameNo < inMaxFrameNum; inFrameNo++)
                {
                    ClsDatKeyFrame clKeyFrame = clOption.GetKeyFrame(inFrameNo);
                    if(clKeyFrame!= null)
                    {
                        isParentFlag = clKeyFrame.mParentFlag;
                    }

                    if (isParentFlag)
                    {
                        inX = inFrameNo * FormControl.CELL_WIDTH;
                        inY = this.mLineNo * FormControl.CELL_HEIGHT;
                        g.FillRectangle(clBrushParent, inX, inY + 2, FormControl.CELL_WIDTH, FormControl.CELL_HEIGHT / 2 - 4);
                    }
                }
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

            //以下、DISPLAYオプション表示処理
            int inCnt, inMax;
            isExist = this.mDicOption.ContainsKey(TYPE_OPTION.DISPLAY);
            if (isExist)
            {
                ClsDatOption clOption = this.mDicOption[TYPE_OPTION.DISPLAY];
                if (clOption != null)
                {
                    for (inCnt = 0; inCnt < inMaxFrameNum; inCnt++)
                    {
                        ClsDatKeyFrame clKeyFrame = clOption.GetKeyFrame(inCnt);
                        if (clKeyFrame == null) continue;

                        g.DrawImage(Properties.Resources.markRed, inCnt * FormControl.CELL_WIDTH + 2, this.mLineNo * FormControl.CELL_HEIGHT + 1);
                    }
                }
            }

            //以下、オプション描画処理
            if (this.isOpen)
            {
                foreach (TYPE_OPTION enTypeOption in Enum.GetValues(typeof(TYPE_OPTION)))
                {
                    isExist = this.mDicOption.ContainsKey(enTypeOption);
                    if (!isExist) continue;
                    ClsDatOption clOption = this.mDicOption[enTypeOption];
                    clOption.DrawTime(g, inSelectLineNo, inSelectFrameNo, inMaxFrameNum, inWidth, inHeight);
                }
            }

            //以下、子供のエレメント描画処理
            inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatElem clElem = this.mListElem[inCnt];
                clElem.DrawTime(g, inSelectLineNo, inSelectFrameNo, inMaxFrameNum, inWidth, inHeight);
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
        /// 挿入可能マークからエレメントを取得する処理
        /// </summary>
        /// <param name="clMotion">モーション管理クラス</param>
        /// <param name="enMark">挿入可能マーク</param>
        public void FindElemFromMark(ClsDatMotion clMotion, MARK_ELEMENT enMark)
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
        /// 挿入可能マークのクリア
        /// </summary>
        public void ClearInsertMark()
        {
            this.mInsertMark = MARK_ELEMENT.NONE;

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
        public void SetInsertMark(MARK_ELEMENT enMark)
        {
            this.mInsertMark = enMark;
        }
    }
}
