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

        /// <summary>
        /// フレーム数変更処理
        /// </summary>
        /// <param name="inFrameNum">フレーム数</param>
        public void SetFrameNum(int inFrameNum)
        {
            if (inFrameNum <= 0) return;
            if (inFrameNum > 65535) return; //あまりに大きな値は怪しいのでバグ対策としてはじく

            int inFrameNumNow = this.mListKeyFrame.Count;
            if (inFrameNumNow == inFrameNum) return;

            if (inFrameNumNow < inFrameNum)
            {
                //以下、フレーム数を増やす処理
                while (this.mListKeyFrame.Count < inFrameNum)
                {
                    this.mListKeyFrame.Add(null);
                }
            }
            else
            {
                //以下、フレーム数を減らす処理
                while (this.mListKeyFrame.Count > inFrameNum)
                {
                    int inIndex = this.mListKeyFrame.Count - 1;
                    ClsDatKeyFrame clKeyFrame = this.mListKeyFrame[inIndex];
                    clKeyFrame.RemoveAll();
                    this.mListKeyFrame.RemoveAt(inIndex);
                }
            }
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
