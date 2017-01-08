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

        public void RemoveAll()
        {
            //以下、キーフレーム全削除処理
            int inCnt, inMax = this.mListKeyFrame.Count;
            for (inCnt = 0; inCnt < inMax; inCnt++)
            {
                ClsDatKeyFrame clKeyFrame = this.mListKeyFrame[inCnt];
                clKeyFrame.RemoveAll();
            }
            this.mListKeyFrame.Clear();
        }
    }
}
