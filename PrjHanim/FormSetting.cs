using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{
    public partial class FormSetting : Form
    {
        public FormSetting()
        {
            InitializeComponent();

            //以下、初期化処理
            this.DialogResult = DialogResult.None;
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            //以下、ウィンドウの設定
            this.Location = ClsSystem.mSetting.mWindowSetting.mLocation;
            this.Size = ClsSystem.mSetting.mWindowSetting.mSize;

            //以下、各種色の情報を設定する処理
            this.panel_MainBackColor.BackColor = ClsSystem.mSetting.mMainColorBack;
            this.panel_MainGridColor.BackColor = ClsSystem.mSetting.mMainColorGrid;
            this.panel_MainCenterLineColor.BackColor = ClsSystem.mSetting.mMainColorCenterLine;
            this.panel_RateGraphBackColor.BackColor = ClsSystem.mSetting.mRateGraphColorBack;
            this.panel_RateGraphGridColor.BackColor = ClsSystem.mSetting.mRateGraphColorGrid;
            this.panel_RateGraphCenterLineColor.BackColor = ClsSystem.mSetting.mRateGraphColorCenterLine;
            this.panel_RateGraphGraphColor.BackColor = ClsSystem.mSetting.mRateGraphColorGraph;
            this.panel_RateGraphForceColor.BackColor = ClsSystem.mSetting.mRateGraphColorForce;
        }

        private void FormSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            //以下、ウィンドウ情報保存処理
            ClsSystem.mSetting.mWindowSetting.mLocation = this.Location;
            ClsSystem.mSetting.mWindowSetting.mSize = this.Size;
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            //以下、各種色の情報を設定する処理
            ClsSystem.mSetting.mMainColorBack = this.panel_MainBackColor.BackColor;
            ClsSystem.mSetting.mMainColorGrid = this.panel_MainGridColor.BackColor;
            ClsSystem.mSetting.mMainColorCenterLine = this.panel_MainCenterLineColor.BackColor;
            ClsSystem.mSetting.mRateGraphColorBack = this.panel_RateGraphBackColor.BackColor;
            ClsSystem.mSetting.mRateGraphColorGrid = this.panel_RateGraphGridColor.BackColor;
            ClsSystem.mSetting.mRateGraphColorCenterLine = this.panel_RateGraphCenterLineColor.BackColor;
            ClsSystem.mSetting.mRateGraphColorGraph = this.panel_RateGraphGraphColor.BackColor;
            ClsSystem.mSetting.mRateGraphColorForce = this.panel_RateGraphForceColor.BackColor;

            //以下、保存データ保存処理
            ClsSystem.mSetting.Save();

            this.DialogResult = DialogResult.OK;

//※ここでメインウィンドウをリフレッシュしたい
//※ここでRateGraphウィンドウをリフレッシュしたい
//※っていうか全ウィンドウリフレッシュしたい
        }

        private void panel_Color_Click(object sender, EventArgs e)
        {
            ColorDialog cdg = new ColorDialog();
            if (cdg.ShowDialog() == DialogResult.OK)
            {
                Panel clPanel = (Panel)sender;
                clPanel.BackColor = cdg.Color;
            }
            cdg.Dispose();
        }
    }
}
