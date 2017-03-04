using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{
    public class ClsView
    {
        public static int mX;
        public static int mY;
        public static int mWidth;
        public static int mHeight;

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="clControl">キャンバスとなるパネル</param>
        public static void Init(Control clControl)
        {
            ClsView.mX = 0;
            ClsView.mY = 0;
            ClsView.mWidth = clControl.Width;
            ClsView.mHeight = clControl.Height;
        }

        /// <summary>
        /// ワールド座標系からカメラ座標系に変換する
        /// </summary>
        /// <param name="inPosX">Ｘ座標</param>
        /// <returns>カメラ座標</returns>
        public static int WorldPosX2CameraPosX(int inPosX)
        {
            int inPosXNew = ClsView.mX + inPosX + ClsView.mWidth / 2;
            return (inPosXNew);
        }

        /// <summary>
        /// ワールド座標系からカメラ座標系に変換する
        /// </summary>
        /// <param name="inPosY">Ｙ座標</param>
        /// <returns>カメラ座標</returns>
        public static int WorldPosY2CameraPosY(int inPosY)
        {
            int inPosYNew = ClsView.mY + inPosY + ClsView.mHeight / 2;
            return (inPosYNew);
        }

        /// <summary>
        /// カメラ座標系からワールド座標系に変換する
        /// </summary>
        /// <param name="inPosX">Ｘ座標</param>
        /// <returns>ワールド座標</returns>
        public static int CameraPosX2WorldPosX(int inPosX)
        {
            int inPosXNew = inPosX - ClsView.mWidth / 2 - ClsView.mX;
            return (inPosXNew);
        }

        /// <summary>
        /// カメラ座標系からワールド座標系に変換する
        /// </summary>
        /// <param name="inPosY">Ｙ座標</param>
        /// <returns>ワールド座標</returns>
        public static int CameraPosY2WorldPosY(int inPosY)
        {
            int inPosYNew = inPosY - ClsView.mHeight / 2 - ClsView.mY;
            return (inPosYNew);
        }
    }
}
