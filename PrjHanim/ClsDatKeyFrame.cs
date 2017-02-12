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
        /// <param name="inIndexParent">親のインデックス</param>
        public void Save(int inIndexParent)
        {
            //以下、キーフレーム保存処理
            int inIndexOption = ClsSystem.mFileData.AddKeyFrame(inIndexParent, this);

            //以下、トゥイーン保存処理
            if (this.mTween != null)
            {
                this.mTween.Save(inIndexOption);
            }
        }
    }
}
