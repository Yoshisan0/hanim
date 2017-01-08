using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsDatKeyFrame
    {
        public ClsTween mTween; //トゥイーン管理クラス

        public ClsDatKeyFrame()
        {
        }

        public Dictionary<string, object> Export()
        {
            return (null);
        }

        public void RemoveAll()
        {
            //以下、トゥイーン全削除処理
            if (this.mTween != null)
            {
                this.mTween.RemoveAll();
                this.mTween = null;
            }
        }
    }
}
