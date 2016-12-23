using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class Motion
    {
        //このクラスをList<Motion>という形式でFormMainに持つ感じになります？

        public List<TIMELINEbase> mListTimeLine;    //TIMELINEbaseが複数あって、それが同時に動いて１つのモーションとなる？
        public string mName;    //モーション名
    }
}
