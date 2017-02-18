using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    [Serializable]
    public class ClsDatKeyFrame
    {
        public ClsDatTween mTween; //トゥイーン管理クラス

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

        /// <summary>
        /// 保存処理
        /// </summary>
        /// <param name="clHeader">ヘッダー</param>
        public void Save(string clHeader)
        {
            //以下、キーフレーム保存処理
            ClsSystem.SaveElementStart(clHeader, "KeyFrame");

            //以下、トゥイーン保存処理
            if (this.mTween != null)
            {
                this.mTween.Save(clHeader + "\t");
            }

            ClsSystem.SaveElementEnd(clHeader, "KeyFrame");
        }
    }
}
