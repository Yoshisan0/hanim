using System;
using System.Drawing;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{
    public partial class FormAttribute : Form
    {
        private bool isLocked;      //パラメータ変更中にロック
        private FormMain mFormMain;
        private ClsDatElem mElem;   //ターゲットとなるエレメント

        public bool isChanged;//パラメータ変更があった時True 読み出し後False

        public FormAttribute(FormMain form)
        {
            InitializeComponent();

            //以下、初期化処理
            this.mFormMain = form;
            this.mElem = null;
        }

        private void FormAttribute_Load(object sender, EventArgs e)
        {
            //以下、ウィンドウの設定
            this.Location = ClsSystem.mSetting.mWindowAttribute.mLocation;
            this.Size = ClsSystem.mSetting.mWindowAttribute.mSize;

            //以下、チェックボックスの名称を変更する処理
            //this.InitCheckBox(this.checkBox_X, TYPE_OPTION.POSITION_X);
            //this.InitCheckBox(this.checkBox_Y, TYPE_OPTION.POSITION_Y);
            this.InitCheckBox(this.checkBox_Rot, TYPE_OPTION.ROTATION);
            this.InitCheckBox(this.checkBox_SX, TYPE_OPTION.SCALE_X);
            this.InitCheckBox(this.checkBox_SY, TYPE_OPTION.SCALE_Y);
            this.InitCheckBox(this.checkBox_T, TYPE_OPTION.TRANSPARENCY);
            this.InitCheckBox(this.checkBox_FlipH, TYPE_OPTION.FLIP_HORIZONAL);
            this.InitCheckBox(this.checkBox_FlipV, TYPE_OPTION.FLIP_VERTICAL);
            this.InitCheckBox(this.checkBox_Color, TYPE_OPTION.COLOR);
            this.InitCheckBox(this.checkBox_Xoff, TYPE_OPTION.OFFSET_X);
            this.InitCheckBox(this.checkBox_Yoff, TYPE_OPTION.OFFSET_Y);
            this.InitCheckBox(this.checkBox_UserText, TYPE_OPTION.USER_DATA);
        }

        /// <summary>
        /// チェックボックス初期化処理
        /// </summary>
        /// <param name="clCheckBox">チェックボックス</param>
        /// <param name="enTypeOption">オプション種別</param>
        private void InitCheckBox(CheckBox clCheckBox, TYPE_OPTION enTypeOption)
        {
            clCheckBox.Text = ClsDatOption.CnvType2Name(enTypeOption);
            clCheckBox.Tag = enTypeOption;
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="clElem">選択中のエレメント</param>
        public void Init(ClsDatElem clElem)
        {
            this.mElem = clElem;

            if (clElem == null)
            {
                this.Text = "Attribute";
                this.panel_Attribute_Base.Enabled = false;
                //this.Enabled = false;
            }
            else
            {
                this.Text = ClsTool.GetWindowName("Attribute", clElem);
                this.panel_Attribute_Base.Enabled = true;
                //this.Enabled = true;

                //以下、チェック状態の設定
                //this.checkBox_Display.Checked = true;
                //this.checkBox_X.Checked = true;
                //this.checkBox_Y.Checked = true;
                this.checkBox_Rot.Checked = clElem.mDicOption.ContainsKey(TYPE_OPTION.ROTATION);
                this.checkBox_SX.Checked = clElem.mDicOption.ContainsKey(TYPE_OPTION.SCALE_X);
                this.checkBox_SY.Checked = clElem.mDicOption.ContainsKey(TYPE_OPTION.SCALE_Y);
                this.checkBox_T.Checked = clElem.mDicOption.ContainsKey(TYPE_OPTION.TRANSPARENCY);
                this.checkBox_FlipH.Checked = clElem.mDicOption.ContainsKey(TYPE_OPTION.FLIP_HORIZONAL);
                this.checkBox_FlipV.Checked = clElem.mDicOption.ContainsKey(TYPE_OPTION.FLIP_VERTICAL);
                this.checkBox_Color.Checked = clElem.mDicOption.ContainsKey(TYPE_OPTION.COLOR);
                this.checkBox_Xoff.Checked = clElem.mDicOption.ContainsKey(TYPE_OPTION.OFFSET_X);
                this.checkBox_Yoff.Checked = clElem.mDicOption.ContainsKey(TYPE_OPTION.OFFSET_Y);
                this.checkBox_UserText.Checked = clElem.mDicOption.ContainsKey(TYPE_OPTION.USER_DATA);
            }
        }

        /// <summary>
        /// フォームにパラメータをセットします
        /// </summary>
        /// <param name="clParam"></param>
        public void SetAllParam(ClsParam clParam)
        {
            //変更終わるまでロックしないと毎回ChangeValueが発生してしまう
            isLocked = true;

            UDnumX.Value = (int)clParam.Position.X;
            UDnumY.Value = (int)clParam.Position.Y;
            //UDnumZ.Value =(int)atr.Position.Z;

            //UDnumRotX.Value = (decimal)atr.Radius.X;
            //UDnumRotY.Value = (decimal)atr.Radius.Y;
            UDnumRot.Value = (decimal)clParam.Radius.Z;

            UDnumSX.Value = (decimal)clParam.Scale.X;
            UDnumSY.Value = (decimal)clParam.Scale.Y;
            //UDnumSZ.Value = (decimal)atr.Scale.Z;

            //checkBox_X.Checked = atr.isX;
            //checkBox_Y.Checked = atr.isY;
            //checkZ.Checked = atr.isZ;

            checkBox_Rot.Checked = clParam.isRZ;

            checkBox_SX.Checked = clParam.isSX;
            checkBox_SY.Checked = clParam.isSY;
            //checkSZ.Checked = atr.isSZ;

            checkBox_FlipH.Checked = clParam.FlipH;
            checkBox_FlipV.Checked = clParam.FlipV;
            //checkEnable.Checked = atr.Enable;
            //checkBox_Display.Checked = atr.Visible;

            checkBox_T.Checked = clParam.isTransparrency;
            UDnumT.Value = clParam.Transparency;

            checkBox_Color.Checked = clParam.isColor;
            ColorCode.Text = $"{clParam.Color:X8}";
            ColorCode.Tag = clParam.Color;

            UDnumColRate.Value = (decimal)clParam.ColorRate;

            UDnumXoff.Value = (int)clParam.Offset.X;
            UDnumYoff.Value = (int)clParam.Offset.Y;
            //UDnumZoff.Value = (int)atr.Offset.Z;

            textBox_User.Text = clParam.Text;

            //変更完了
            isLocked = false;
        }

        /// <summary>
        /// フォーム上パラメータを取得します
        /// </summary>
        /// <param name="clParam">参照</param>
        public ClsParam GetAllParam(ref ClsParam clParam)
        {
            //isLocked = true;
            //パラメータ手動変更があった時のみ取得出来る
            if (isChanged)
            {
                //AttributeBase atr = new AttributeBase();
                clParam.Position.X = (int)UDnumX.Value;
                clParam.Position.Y = (int)UDnumY.Value;
                //ret.Position.Z = Decimal.ToInt32(UDnumZ.Value);

                //atr.Radius.X = (float)UDnumRotX.Value;
                //atr.Radius.Y = (float)UDnumRotY.Value;
                clParam.Radius.Z = (float)UDnumRot.Value;

                clParam.Scale.X = (float)UDnumSX.Value;
                clParam.Scale.Y = (float)UDnumSY.Value;
                //ret.Scale.Z = (float)UDnumSZ.Value;

                clParam.FlipH = checkBox_FlipH.Checked;
                clParam.FlipV = checkBox_FlipV.Checked;

                //ret.Enable = checkEnable.Checked;
                //atr.Visible = checkBox_Display.Checked;

                clParam.isTransparrency = checkBox_T.Checked;
                clParam.Transparency = (int)UDnumT.Value;

                clParam.isColor = checkBox_Color.Checked;
                if (ColorCode.Tag != null) clParam.Color = (int)ColorCode.Tag;
                if (ColorCode.Text != "")
                { 
                    //atr.Color = int.Parse(ColorCode.Text, System.Globalization.NumberStyles.HexNumber);
                }
                clParam.ColorRate = (int)UDnumColRate.Value;

                clParam.Offset.X = (int)UDnumXoff.Value;
                clParam.Offset.Y = (int)UDnumYoff.Value;
                //ret.Offset.Z = (int)UDnumZoff.Value;

                clParam.Text = textBox_User.Text;

                isLocked = false;
                isChanged = false;
            }
            return clParam;
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

        private void checkBox_CheckStateChanged(object sender, EventArgs e)
        {
            //チェックボックス系
            //any Param Update どれかのチェックが変更された通知をメインに送る
            if (!isLocked) {
                isChanged = true;
                this.mFormMain.AttributeUpdate();
            }

            CheckBox clCheckBox = sender as CheckBox;
            if (this.mElem != null)
            {
                if (clCheckBox.Tag != null)
                {
                    TYPE_OPTION enTypeOption = (TYPE_OPTION)clCheckBox.Tag;
                    object clValue = ClsParam.GetDefaultValue(enTypeOption);
                    if (clCheckBox.Checked) this.mElem.AddOption(enTypeOption, clValue);
                    else this.mElem.RemoveOption(enTypeOption, false);

                    //以下、行番号振り直し処理
                    if (this.mElem.mMotion != null)
                    {
                        this.mElem.mMotion.Assignment();
                    }

                    //以下、コントロールリフレッシュ処理
                    this.mFormMain.mFormControl.RefreshAll();
                }
            }
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

//        FRAME test = null;
        //※FRAMEとはなんでしょう？

        private void button_X_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.POSITION_X, 10, 20, 15);
            clForm.Show();
        }

        private void button_Y_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.POSITION_Y, 10, 20, 15);
            clForm.Show();
        }

        private void button_RX_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.ROTATION, 10, 20, 15);
            clForm.Show();
        }

        private void button_SX_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.SCALE_X, 10, 20, 15);
            clForm.Show();
        }

        private void button_SY_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.SCALE_Y, 10, 20, 15);
            clForm.Show();
        }

        private void button_T_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.TRANS, 10, 20, 15);
            clForm.Show();
        }

        private void button_Color_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.COLOR, 10, 20, 15);
            clForm.Show();
        }

        private void button_Xoff_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.SCALE_X, 10, 20, 15);
            clForm.Show();
        }

        private void button_Yoff_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.SCALE_Y, 10, 20, 15);
            clForm.Show();
        }
    }
}
