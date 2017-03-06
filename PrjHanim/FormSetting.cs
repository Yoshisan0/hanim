using System;
using System.Drawing;
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
                panel_prepre.Refresh();
                panel_prerate.Refresh();
            }
            cdg.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClsSystem.mSetting.mFileHistory.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ClsSystem.mSetting.mProjectAutoReload = this.check_AutoReload.Checked;
        }

        private void panel_prepre_Paint(object sender, PaintEventArgs e)
        {
            //preView of PreView
            Size ps = panel_prepre.Size;
            e.Graphics.FillRectangle(new SolidBrush( panel_MainBackColor.BackColor), new Rectangle(0, 0, this.ClientSize.Width,this.ClientSize.Height));
            Pen pCol = new Pen(panel_MainGridColor.BackColor);
            for (int yCnt=0;yCnt <ps.Height;yCnt+=8)
            {
                e.Graphics.DrawLine(pCol,0,yCnt,ps.Width,yCnt);
            }
            for (int xCnt = 0; xCnt < ps.Width; xCnt += 8)
            {
                e.Graphics.DrawLine(pCol,xCnt,0,xCnt,ps.Height);
            }
            pCol = new Pen(panel_MainCenterLineColor.BackColor);
            e.Graphics.DrawLine(pCol,ps.Width /2,0,ps.Width/2,ps.Height);
            e.Graphics.DrawLine(pCol,0,ps.Height/2, ps.Width,ps.Height/2);
        }

        private void panel_prerate_Paint(object sender, PaintEventArgs e)
        {
            //preView of PreRAte
            Size ps = panel_prerate.Size;
            e.Graphics.FillRectangle(new SolidBrush(panel_RateGraphBackColor.BackColor), new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height));
            Pen pCol = new Pen(panel_RateGraphGridColor.BackColor);
            for (int yCnt = 0; yCnt < ps.Height; yCnt += 8)
            {
                e.Graphics.DrawLine(pCol, 0, yCnt, ps.Width, yCnt);
            }
            for (int xCnt = 0; xCnt < ps.Width; xCnt += 8)
            {
                e.Graphics.DrawLine(pCol, xCnt, 0, xCnt, ps.Height);
            }
            pCol = new Pen(panel_RateGraphCenterLineColor.BackColor);
            e.Graphics.DrawLine(pCol, ps.Width / 2, 0, ps.Width / 2, ps.Height);
            e.Graphics.DrawLine(pCol, 0, ps.Height / 2, ps.Width, ps.Height / 2);

            pCol = new Pen(panel_RateGraphGraphColor.BackColor);
            //e.Graphics.DrawLine(pCol, 0, ps.Height, ps.Width, 0);
            e.Graphics.DrawArc(pCol,-ps.Width/2,0, ps.Width, ps.Height, 0, 90);
            e.Graphics.DrawArc(pCol, ps.Width/2, 0, ps.Width, ps.Height,180,270);

            pCol = new Pen(panel_RateGraphForceColor.BackColor);
            e.Graphics.DrawLine(pCol,0,ps.Height, ps.Width/4, ps.Height/2);
            e.Graphics.DrawEllipse(pCol, ps.Width / 4-4, ps.Height / 2-4, 8, 8);
            e.Graphics.DrawLine(pCol,ps.Width,0,  ps.Width-ps.Width/4, ps.Height/2);
            e.Graphics.DrawEllipse(pCol,ps.Width - ps.Width / 4-4, ps.Height / 2-4, 8, 8);

        }
    }
}
