﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsDatKeyFrame
    {
        public ClsTween mTween; //トゥイーン管理クラス

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClsDatKeyFrame()
        {
        }

        /// <summary>
        /// キーフレームの全てを削除する処理
        /// </summary>
        public void RemoveAll()
        {
            //以下、トゥイーン全削除処理
            if (this.mTween != null)
            {
                this.mTween.RemoveAll();
                this.mTween = null;
            }
        }

        /// <summary>
        /// エクスポート
        /// </summary>
        /// <returns>出力情報</returns>
        public Dictionary<string, object> Export()
        {
            return (null);
        }
    }
}