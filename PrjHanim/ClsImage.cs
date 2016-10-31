using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsImage
    {
        public Image Origin;
        public Image Big;
        public Image Small;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClsImage()
        {
            this.Origin = null;
            this.Big = null;
            this.Small = null;
        }
    }
}
