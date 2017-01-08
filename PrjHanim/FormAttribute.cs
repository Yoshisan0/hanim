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
    public partial class FormAttribute : Form
    {

        //一時記録用
        //AllSet,AllGet時に更新
        private AttributeBase ValuesPool;
        private bool isLocked;//パラメータ変更中にロック
        private FormMain mFormMain;

        public bool isChanged;//パラメータ変更があった時True 読み出し後False

        public FormAttribute(FormMain form)
        {
            InitializeComponent();
            this.mFormMain = form;
        }

        private void FormAttribute_Load(object sender, EventArgs e)
        {
            //以下、ウィンドウの設定
            this.Location = ClsSystem.mSetting.mWindowAttribute.mLocation;
            this.Size = ClsSystem.mSetting.mWindowAttribute.mSize;
        }

        public void SetName(string clName)
        {
            this.Text = "Attribute (" + clName + ")";
        }

        /// <summary>
        /// フォームにパラメータをセットします
        /// </summary>
        /// <param name="atr"></param>
        public void SetAllParam(AttributeBase atr)
        {
            //変更終わるまでロックしないと毎回ChangeValueが発生してしまう
            isLocked = true;
            ValuesPool = atr;

            UDnumX.Value = (int)atr.Position.X;
            UDnumY.Value = (int)atr.Position.Y;
            //UDnumZ.Value =(int)atr.Position.Z;

            //UDnumRotX.Value = (decimal)atr.Radius.X;
            //UDnumRotY.Value = (decimal)atr.Radius.Y;
            UDnumRot.Value = (decimal)atr.Radius.Z;

            UDnumSX.Value = (decimal)atr.Scale.X;
            UDnumSY.Value = (decimal)atr.Scale.Y;
            //UDnumSZ.Value = (decimal)atr.Scale.Z;

            checkX.Checked = atr.isX;
            checkY.Checked = atr.isY;
            //checkZ.Checked = atr.isZ;

            checkRot.Checked = atr.isRZ;

            checkSX.Checked = atr.isSX;
            checkSY.Checked = atr.isSY;
            //checkSZ.Checked = atr.isSZ;

            checkFlipH.Checked = atr.FlipH;
            checkFlipV.Checked = atr.FlipV;
            //checkEnable.Checked = atr.Enable;
            checkVisible.Checked = atr.Visible;

            checkT.Checked = atr.isTransparrency;
            UDnumT.Value = atr.Transparency;

            checkColor.Checked = atr.isColor;
            ColorCode.Text = $"{atr.Color:X8}";
            ColorCode.Tag = atr.Color;

            UDnumColRate.Value = (decimal)atr.ColorRate;

            UDnumXoff.Value = (int)atr.Offset.X;
            UDnumYoff.Value = (int)atr.Offset.Y;
            //UDnumZoff.Value = (int)atr.Offset.Z;

            UserText.Text = atr.Text;

            //変更完了
            isLocked = false;
        }

        /// <summary>
        /// フォーム上パラメータを取得します
        /// </summary>
        /// <param name="atr">参照</param>
        public AttributeBase GetAllParam(ref AttributeBase atr)
        {
            //isLocked = true;
            //パラメータ手動変更があった時のみ取得出来る
            if (isChanged)
            {
                //AttributeBase atr = new AttributeBase();
                atr.Position.X = (int)UDnumX.Value;
                atr.Position.Y = (int)UDnumY.Value;
                //ret.Position.Z = Decimal.ToInt32(UDnumZ.Value);

                //atr.Radius.X = (float)UDnumRotX.Value;
                //atr.Radius.Y = (float)UDnumRotY.Value;
                atr.Radius.Z = (float)UDnumRot.Value;

                atr.Scale.X = (float)UDnumSX.Value;
                atr.Scale.Y = (float)UDnumSY.Value;
                //ret.Scale.Z = (float)UDnumSZ.Value;

                atr.FlipH = checkFlipH.Checked;
                atr.FlipV = checkFlipV.Checked;

                //ret.Enable = checkEnable.Checked;
                atr.Visible = checkVisible.Checked;

                atr.isTransparrency = checkT.Checked;
                atr.Transparency = (int)UDnumT.Value;

                atr.isColor = checkColor.Checked;
                if (ColorCode.Tag != null) atr.Color = (int)ColorCode.Tag;
                if (ColorCode.Text != "")
                { 
                    //atr.Color = int.Parse(ColorCode.Text, System.Globalization.NumberStyles.HexNumber);
                }
                atr.ColorRate = (int)UDnumColRate.Value;

                atr.Offset.X = (int)UDnumXoff.Value;
                atr.Offset.Y = (int)UDnumYoff.Value;
                //ret.Offset.Z = (int)UDnumZoff.Value;

                atr.Text = UserText.Text;

                ValuesPool = atr;
                isLocked = false;
                isChanged = false;
            }
            return atr;
        }

        private void ColorCode_TextChanged(object sender, EventArgs e)
        {
            //PreViewColor
            if (ColorCode.Text != "")
            {
                ColorPanel.BackColor = Color.FromArgb(int.Parse(ColorCode.Text, System.Globalization.NumberStyles.HexNumber));
            }
            mFormMain.AttributeUpdate();
        }

        private void ColorPanel_Click(object sender, EventArgs e)
        {
            //PickUP ColorDialog
            ColorDialog dlg = new ColorDialog();
            dlg.Color = ColorPanel.BackColor;
            if(dlg.ShowDialog()==DialogResult.OK)
            {
                ColorPanel.BackColor = dlg.Color;
                ColorCode.Tag = dlg.Color.ToArgb();
                ColorCode.Text = $"{dlg.Color.ToArgb():X8}";//ARGB
                //この代入ではバリデードが発生しないらしく更新通知
                if (!isLocked) { isChanged = true; mFormMain.AttributeUpdate(); }
                //ColorCode.Text =  dlg.Color.R.ToString("X2") + dlg.Color.G.ToString("X2") + dlg.Color.B.ToString("X2"); //RGB 6
            }
        }

        private void checkUserText_CheckStateChanged(object sender, EventArgs e)
        {
            //チェックボックス系
            //any Param Update どれかのチェックが変更された通知をメインに送る
            if (!isLocked) { isChanged = true; mFormMain.AttributeUpdate(); }
        }

        private void UDnum_ValueChanged(object sender, EventArgs e)
        {
            //アップダウンコントロール系
            //any Param Update どれかのチェックが変更された通知をメインに送る
            if (!isLocked) { isChanged = true; mFormMain.AttributeUpdate(); }
        }

        private void FormAttribute_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Closeキャンセルして非表示にするだけ
            e.Cancel = true;

            //this.Visible = false; //自身で消さなくても下の操作で消える
            this.mFormMain.checkBox_Attribute.Checked = false;
        }

        FRAME test = null;

        private void button_X_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsTween.EnmParam.POSITION_X, 10, 20, 15);
            clForm.Show();
        }

        private void button_Y_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsTween.EnmParam.POSITION_Y, 10, 20, 15);
            clForm.Show();
        }

        private void button_RX_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsTween.EnmParam.ROTATION, 10, 20, 15);
            clForm.Show();
        }

        private void button_SX_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsTween.EnmParam.SCALE_X, 10, 20, 15);
            clForm.Show();
        }

        private void button_SY_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsTween.EnmParam.SCALE_Y, 10, 20, 15);
            clForm.Show();
        }

        private void button_T_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsTween.EnmParam.TRANS, 10, 20, 15);
            clForm.Show();
        }

        private void button_Color_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsTween.EnmParam.COLOR, 10, 20, 15);
            clForm.Show();
        }

        private void button_Xoff_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsTween.EnmParam.SCALE_X, 10, 20, 15);
            clForm.Show();
        }

        private void button_Yoff_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsTween.EnmParam.SCALE_Y, 10, 20, 15);
            clForm.Show();
        }
    }
}
