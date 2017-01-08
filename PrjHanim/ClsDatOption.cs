using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsDatOption
    {
        //とりあえず作成したバージョン

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

        public TYPE mType;
        public List<ClsDatKeyFrame> mListKeyFrame;

        public ClsDatOption()
        {
        }

        public Dictionary<string, object> Export()
        {
            return (null);
        }
    }
}
