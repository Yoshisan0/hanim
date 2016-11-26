using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsTween
    {
        public EnmParam mParam;
        public int mFrmStart;
        public int mFrmEnd;
        public Vector3 mPos;
        public Vector3[] mListVec;

        public ClsTween(EnmParam enParam, int inFrmStart, int inFrmEnd, Vector3 clPos, Vector3[] pclVec)
        {
            this.mParam = enParam;
            this.mFrmStart = inFrmStart;
            this.mFrmEnd = inFrmEnd;

            this.mPos = new Vector3(clPos.X, clPos.Y, 0.0f);

            int inCnt;
            for (inCnt = 0; inCnt < 3; inCnt++)
            {
                this.mListVec[inCnt] = new Vector3(pclVec[inCnt].X, pclVec[inCnt].Y, 0.0f);
            }
        }
    }
}
