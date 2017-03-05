using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{
    public class ClsView
    {
        public static float mScale;
        public static float mX;
        public static float mY;
        public static float mWidth;
        public static float mHeight;

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="clControl">キャンバスとなるパネル</param>
        public static void Init(Control clControl)
        {
            ClsView.mScale = 1.0f;
            ClsView.mX = 0.0f;
            ClsView.mY = 0.0f;
            ClsView.mWidth = clControl.Width;
            ClsView.mHeight = clControl.Height;
        }

        /// <summary>
        /// ワールド座標系からカメラ座標系に変換する
        /// </summary>
        /// <param name="flPosX">Ｘ座標</param>
        /// <returns>カメラ座標</returns>
        public static float WorldPosX2CameraPosX(float flPosX)
        {
            float flPosXNew = ClsView.mX + flPosX * ClsView.mScale + ClsView.mWidth / 2;
            return (flPosXNew);
        }

        /// <summary>
        /// ワールド座標系からカメラ座標系に変換する
        /// </summary>
        /// <param name="flPosY">Ｙ座標</param>
        /// <returns>カメラ座標</returns>
        public static float WorldPosY2CameraPosY(float flPosY)
        {
            float flPosYNew = ClsView.mY + flPosY * ClsView.mScale + ClsView.mHeight / 2;
            return (flPosYNew);
        }

        /// <summary>
        /// カメラ座標系からワールド座標系に変換する
        /// </summary>
        /// <param name="flPosX">Ｘ座標</param>
        /// <returns>ワールド座標</returns>
        public static float CameraPosX2WorldPosX(float flPosX)
        {
            float flPosXNew = (flPosX - ClsView.mWidth / 2 - ClsView.mX) / ClsView.mScale;
            return (flPosXNew);
        }

        /// <summary>
        /// カメラ座標系からワールド座標系に変換する
        /// </summary>
        /// <param name="inPosY">Ｙ座標</param>
        /// <returns>ワールド座標</returns>
        public static float CameraPosY2WorldPosY(float flPosY)
        {
            float flPosYNew = (flPosY - ClsView.mHeight / 2 - ClsView.mY) / ClsView.mScale;
            return (flPosYNew);
        }
    }
}
