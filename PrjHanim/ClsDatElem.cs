﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    //とりあえず作成したバージョン

    public class ClsDatElem
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
        public List<ClsDatElem> mListElem;  //エレメント管理クラスのリスト
        public Dictionary<ClsDatOption.TYPE, ClsDatOption> mDicOption;  //キーはアトリビュートのタイプ 値はオプション管理クラス

        public ClsDatElem(string clName)
        {
            this.mName = clName;
            this.mType = ELEMENTSTYPE.Image;
            this.mStyle = ELEMENTSSTYLE.Rect;
            this.isVisible = true;  //表示非表示(目)
            this.isLocked = false;  //ロック状態(鍵)
            this.isOpen = false;    //属性開閉状態(+-)
            this.isSelect = false;  //選択状態
            this.mListElem = new List<ClsDatElem>();
            this.mDicOption = new Dictionary<ClsDatOption.TYPE, ClsDatOption>();
        }

        public Dictionary<string, object> Export()
        {
            /*
            Dictionary<string, object> clDic = new Dictionary<string, object>();
            clDic["Atr_0"] = this.Atr.Export();
            return (clDic);
            */

            return (null);
        }

        public void RemoveAll()
        {
            //以下、子供のエレメントリスト全削除処理
            int inCnt, inMax = this.mListElem.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++) {
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
    }
}
