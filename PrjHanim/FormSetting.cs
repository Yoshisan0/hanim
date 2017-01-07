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
            this.panel_RateGraphGraphColor.BackColor = ClsSystem.mSetting.mRateGraphColorGrid;
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
            ClsSystem.mSetting.mRateGraphColorGrid = this.panel_RateGraphGraphColor.BackColor;
            ClsSystem.mSetting.mRateGraphColorForce = this.panel_RateGraphForceColor.BackColor;

            this.DialogResult = DialogResult.OK;
        }

        private void panel_MainBackColor_Click(object sender, EventArgs e)
        {
        }

        private void panel_MainGridColor_Click(object sender, EventArgs e)
        {
        }

        private void panel_MainCenterLineColor_Click(object sender, EventArgs e)
        {
        }

        private void panel_RateGraphBackColor_Click(object sender, EventArgs e)
        {
        }

        private void panel_RateGraphGridColor_Click(object sender, EventArgs e)
        {
        }

        private void panel_RateGraphCenterLineColor_Click(object sender, EventArgs e)
        {
        }

        private void panel_RateGraphGraphColor_Click(object sender, EventArgs e)
        {
        }

        private void panel_RateGraphForceColor_Click(object sender, EventArgs e)
        {
        }
    }
}
