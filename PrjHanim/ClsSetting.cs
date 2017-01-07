using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;

namespace PrjHikariwoAnim
{
    [Serializable]
    public class ClsSetting
    {
        public ClsSettingWindow mWindowMain;
        public ClsSettingWindow mWindowAttribute;
        public ClsSettingWindow mWindowCell;
        public ClsSettingWindow mWindowControl;
        public ClsSettingWindow mWindowImageCut;
        public ClsSettingWindow mWindowImageList;
        public ClsSettingWindow mWindowRateGraph;
        public ClsSettingWindow mWindowSetting;
        public Color mMainColorBack;
        public Color mMainColorGrid;
        public Color mMainColorCenterLine;
        public Color mRateGraphColorBack;
        public Color mRateGraphColorGrid;
        public Color mRateGraphColorCenterLine;
        public Color mRateGraphColorGraph;
        public Color mRateGraphColorForce;
        public bool mWindowMain_DrawGird;
        public bool mWindowMain_DrawCross;
        public bool mWindowMain_GridSnap;
        public bool mWindowMain_ImageList;
        public bool mWindowMain_Control;
        public bool mWindowMain_Attribute;
        public bool mWindowMain_CellList;
        public int mWindowMain_WidthGrid;
        public bool mWindowRateGraph_DrawGrid;
        public string mLastImageDirectory;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClsSetting()
        {
            this.mWindowMain = new ClsSettingWindow(0, 0, 700, 512);
            this.mWindowAttribute = new ClsSettingWindow(0, 0, 272, 512);
            this.mWindowCell = new ClsSettingWindow(0, 0, 188, 280);
            this.mWindowControl = new ClsSettingWindow(0, 0, 700, 280);
            this.mWindowImageCut = new ClsSettingWindow(0, 0, 627, 360);
            this.mWindowImageList = new ClsSettingWindow(0, 0, 512, 512);
            this.mWindowRateGraph = new ClsSettingWindow(0, 0, 568, 621);
            this.mWindowSetting = new ClsSettingWindow(0, 0, 640, 480);
            this.mMainColorBack = Color.Black;
            this.mMainColorGrid = Color.DarkGreen;
            this.mMainColorCenterLine = Color.DarkRed;
            this.mRateGraphColorBack = Color.Black;
            this.mRateGraphColorGrid = Color.DarkGreen;
            this.mRateGraphColorCenterLine = Color.DarkRed;
            this.mRateGraphColorGraph = Color.Lime;
            this.mRateGraphColorForce = Color.DeepSkyBlue;
            this.mWindowMain_DrawCross = true;
            this.mWindowMain_WidthGrid = 10;
            this.mWindowMain_GridSnap = true;
            this.mWindowMain_ImageList = true;
            this.mWindowMain_Control = true;
            this.mWindowMain_Attribute = true;
            this.mWindowMain_CellList = true;
            this.mWindowRateGraph_DrawGrid = true;
            this.mLastImageDirectory = "";
        }

        /// <summary>
        /// 保存処理
        /// </summary>
        public void Save()
        {
            //以下、パス生成処理
            string clFileName = ClsSystem.GetAppFileName();
            string clPath = ClsPath.GetPath(clFileName + ".setting");

            //以下、Json作成処理
            DataContractJsonSerializer clSerializer = new DataContractJsonSerializer(typeof(ClsSetting));
            MemoryStream clMemStream = new MemoryStream();
            clSerializer.WriteObject(clMemStream, this);
            string clBuffer = Encoding.UTF8.GetString(clMemStream.ToArray());

            //以下、保存処理
            File.WriteAllText(clPath, clBuffer);
        }
    }

    [Serializable]
    public class ClsSettingWindow
    {
        public Point mLocation;
        public Size mSize;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="inX">Ｘ座標</param>
        /// <param name="inY">Ｙ座標</param>
        /// <param name="inW">幅</param>
        /// <param name="inH">高さ</param>
        public ClsSettingWindow(int inX, int inY, int inW, int inH)
        {
            this.mLocation = new Point(inX, inY);
            this.mSize = new Size(inW, inH);
        }
    }
}
