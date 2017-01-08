using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    //とりあえず作成したバージョン

    public class ClsDatElem
    {
        public string mName;                //エレメント名
        public bool isVisible;              //表示非表示(目)
        public bool isLocked;               //ロック状態(鍵)
        public bool isOpen;                 //属性開閉状態(+-)
        public List<ClsDatElem> mListElem;  //エレメント管理クラスのリスト
        public Dictionary<ClsDatOption.TYPE, ClsDatOption> mDicOption;  //キーはアトリビュートのタイプ 値はオプション管理クラス

        public ClsDatElem()
        {
            this.isVisible = true;  //表示非表示(目)
            this.isLocked = false;  //ロック状態(鍵)
            this.isOpen = false;    //属性開閉状態(+-)
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
    }
}
