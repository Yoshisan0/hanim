using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsDatItem
    {
        public enum TYPE_ITEM
        {
            NONE,
            ELEM,
            OPTION,
        }

        public int mLineNo;         //コントロールウィンドウの行番号
        public int mTab;            //コントロールウィンドウの先頭タブの数
        public TYPE_ITEM mTypeItem; //アイテム種別

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClsDatItem()
        {
            this.mLineNo = -1;
            this.mTab = 0;
            this.mTypeItem = TYPE_ITEM.NONE;
        }
    }
}
