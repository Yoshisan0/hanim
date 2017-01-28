using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    [Serializable]
    public class ClsTween
    {
        //TweenのParamなので所在を明確にする為ここに移動しました amami 11/27
        //どのパラメータに対してかの指定
        //UserDataはStringやで
        public enum EnmParam    //オプションにもあるから、それ使った方が良いかも？ coment by yoshi 2017/01/08
        {
            NONE = 0,

            POSITION_X,
            POSITION_Y,
            ROTATION,
            SCALE_X,
            SCALE_Y,
            TRANS,
            FLIP_H,
            FLIP_V,
            VISIBLE,
            COLOR,
            OFFSET_X,
            OFFSET_Y,
            USERDATA,
        }

        public EnmParam mParam;
        public int mFrmStart;
        public int mFrmEnd;
        public Vector3 mPos;
        public Vector3[] mListVec;

        //シリアライザ用
        public ClsTween() { }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="enParam">種別</param>
        /// <param name="inFrmStart">開始フレーム</param>
        /// <param name="inFrmEnd">終了フレーム</param>
        /// <param name="clPos">座標</param>
        /// <param name="pclVec">各ベクトル</param>
        public ClsTween(EnmParam enParam, int inFrmStart, int inFrmEnd, Vector3 clPos, Vector3[] pclVec)
        {
            this.mParam = enParam;
            this.mFrmStart = inFrmStart;
            this.mFrmEnd = inFrmEnd;

            this.mPos = new Vector3(clPos.X, clPos.Y, 0.0f);

            this.mListVec = new Vector3[3];
            int inCnt;
            for (inCnt = 0; inCnt < 3; inCnt++)
            {
                this.mListVec[inCnt] = new Vector3(pclVec[inCnt].X, pclVec[inCnt].Y, 0.0f);
            }
        }

        public void RemoveAll()
        {
            this.mListVec = null;
        }

        /// <summary>
        /// クローン処理
        /// </summary>
        /// <returns>トゥイーン情報</returns>
        public ClsTween Clone()
        {
            ClsTween clTween = new ClsTween(this.mParam, this.mFrmStart, this.mFrmEnd, this.mPos, this.mListVec);
            return (clTween);
        }
    }
}
