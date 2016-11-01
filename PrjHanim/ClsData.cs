using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsData
    {
        public Color mColorBack;
        public bool mDrawGrid;
        public Color mColorGrid;
        public bool mDrawCross;
        public Color mColorCross;
        public int mWidthGrid;
        public bool mGridSnap;
        public bool mViewImageList;
        public bool mViewControl;
        public bool mViewAttribute;
        public bool mViewCellList;

        public ClsData()
        {
            //以下、デフォルト値設定
            this.mColorBack = Color.Black;
            this.mDrawCross = true;
            this.mColorCross = Color.DarkRed;
            this.mDrawGrid = true;
            this.mColorGrid = Color.DarkGreen;
            this.mWidthGrid = 10;
            this.mGridSnap = true;
            this.mViewImageList = true;
            this.mViewControl = true;
            this.mViewAttribute = true;
            this.mViewCellList = true;
        }

        public void Load()
        {
        }

        public void Save()
        {
        }
    }
}
