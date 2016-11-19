using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsMoveHermite
    {
        double mNowPos;
        double mNowVec;
        double mStPos;
        double mStVec;
        double mEdPos;
        double mEdVec;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClsMoveHermite()
        {
            this.mNowPos = 0.0;
            this.mNowVec = 0.0;
            this.mStPos = 0.0;
            this.mStVec = 0.0;
            this.mEdPos = 0.0;
            this.mEdVec = 0.0;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="doStPos">始点値</param>
        /// <param name="doStVec">始点ベクトル</param>
        /// <param name="doEdPos">終点値</param>
        /// <param name="doEdVec">終点ベクトル</param>
        public ClsMoveHermite(double doStPos, double doStVec, double doEdPos, double doEdVec)
        {
            this.mNowPos = doStPos;
            this.mNowVec = 0.0;
            this.mStPos = doStPos;
            this.mStVec = doStVec;
            this.mEdPos = doEdPos;
            this.mEdVec = doEdVec;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Finish()
        {

        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="doEdPos">終点値</param>
        public void Init(double doEdPos)
        {
            this.mStPos = doEdPos;
            this.mEdPos = doEdPos;
        }

        /// <summary>
        /// ターゲット設定
        /// </summary>
        /// <param name="doEdPos">終点値</param>
        /// <param name="doEdVec">終点ベクトル</param>
        public void SetTgtPos(double doEdPos, double doEdVec)
        {
            this.mStPos = this.mNowPos;
            this.mStVec = this.mNowVec;
            this.mEdPos = doEdPos;
            this.mEdVec = doEdVec;
        }

        /// <summary>
        /// 実行処理
        /// </summary>
        /// <param name="doElapsedTime">差分時間</param>
        /// <returns>Ｘ座標</returns>
        public double Exec(double doPos)
        {
            if (doPos< 0.0) doPos = 0.0;
            if (doPos > 1.0) doPos = 1.0;

            double k = doPos;
            double s0 = k * k * k;
            double s1 = k * k;
            double s2 = k;

            double doNewPos = ((2 * this.mStPos) + (this.mStVec) - (2 * this.mEdPos) + (this.mEdVec)) * s0 + ((-3 * this.mStPos) - (2 * this.mStVec) + (3 * this.mEdPos) - (this.mEdVec)) * s1 + (this.mStVec) * s2 + (this.mStPos);
            this.mNowVec = doNewPos - this.mNowPos;
            this.mNowPos = doNewPos;

            return (doNewPos);
        }
    }
}
