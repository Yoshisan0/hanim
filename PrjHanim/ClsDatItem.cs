using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsDatItem
    {
        public int mLineNo; //コントロールウィンドウの行番号
        public int mTab;    //コントロールウィンドウの先頭タブの数

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClsDatItem()
        {
            this.mLineNo = -1;
            this.mTab = 0;
        }
    }
}
