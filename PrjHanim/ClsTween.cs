using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    [Serializable]
    public class ClsTween
    {
        public EnmParam mParam;
        public int mFrmStart;
        public int mFrmEnd;
        public Vector3 mPos;
        public Vector3[] mListVec;

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
