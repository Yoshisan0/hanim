using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    //とりあえず作成したバージョン

    public class ClsDatOption
    {
        public enum TYPE {
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
        public List<ClsDatKeyFrame> mListKeyFrame;  //フレーム数分Countが存在する nullは存在しない事にする

        public ClsDatOption()
        {
        }

        public Dictionary<string, object> Export()
        {
            return (null);
        }
    }
}
