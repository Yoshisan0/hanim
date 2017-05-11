using System;
using System.Drawing;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{
    public partial class FormAttribute : Form
    {
        private bool isLocked;      //パラメータ変更中にロック
        private FormMain mFormMain;

        public FormAttribute(FormMain clFormMain)
        {
            InitializeComponent();

            //以下、初期化処理
            this.mFormMain = clFormMain;
        }

        private void FormAttribute_Load(object sender, EventArgs e)
        {
            //以下、ウィンドウの設定
            this.Location = ClsSystem.mSetting.mWindowAttribute.mLocation;
            this.Size = ClsSystem.mSetting.mWindowAttribute.mSize;

            //以下、チェックボックスの名称を変更する処理
            this.checkBox_EnableRotation.Tag = TYPE_OPTION.ROTATION;
            this.checkBox_EnableScale.Tag = TYPE_OPTION.SCALE;
            this.checkBox_EnableOffset.Tag = TYPE_OPTION.OFFSET;
            this.checkBox_EnableFlip.Tag = TYPE_OPTION.FLIP;
            this.checkBox_EnableTrans.Tag = TYPE_OPTION.TRANSPARENCY;
            this.checkBox_EnableColor.Tag = TYPE_OPTION.COLOR;
            this.checkBox_EnableUserData.Tag = TYPE_OPTION.USER_DATA;
        }

        /// <summary>
        /// チェックボックス初期化処理
        /// </summary>
        /// <param name="clCheckBox">チェックボックス</param>
        /// <param name="enTypeOption">オプションタイプ</param>
        /// <param name="enTypeParam">パラメータータイプ</param>
        private void InitCheckBox(CheckBox clCheckBox, TYPE_OPTION enTypeOption, TYPE_PARAM enTypeParam)
        {
            clCheckBox.Tag = enTypeOption;
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="clElem">選択中のエレメント</param>
        public void Init(ClsDatElem clElem)
        {
            if (clElem == null)
            {
                this.Text = "Attribute";
                this.groupBox_Param.Enabled = false;
            }
            else
            {
                this.Text = ClsTool.GetWindowName("Attribute", clElem);
                this.groupBox_Param.Enabled = true;

                //以下、チェック状態の設定
                ClsParam clParam = clElem.GetParam(0);
                this.SetParam(clParam);
            }
        }

        /// <summary>
        /// フォームにパラメータをセットします
        /// </summary>
        /// <param name="clParam"></param>
        public void SetParam(ClsParam clParam)
        {
            //変更終わるまでロックしないと毎回ChangeValueが発生してしまう
            isLocked = true;

            checkBox_Display.Checked = clParam.mEnableDisplay;

            UDnumX.Value = (int)clParam.mX;
            UDnumY.Value = (int)clParam.mY;

            checkBox_EnableRotation.Checked = clParam.mEnableRotation;
            UDnumRot.Value = (decimal)clParam.mRZ;

            checkBox_EnableScale.Checked = clParam.mEnableScale;
            UDnumSX.Value = (decimal)clParam.mSX;
            UDnumSY.Value = (decimal)clParam.mSY;

            checkBox_EnableFlip.Checked = clParam.mEnableFlip;
            checkBox_FlipH.Checked = clParam.mFlipH;
            checkBox_FlipV.Checked = clParam.mFlipV;

            checkBox_EnableTrans.Checked = clParam.mEnableTrans;
            UDnumT.Value = (decimal)clParam.mTrans;

            checkBox_EnableColor.Checked = clParam.mEnableColor;
            ColorCode.Text = $"{clParam.mColor:X6}";

            checkBox_EnableOffset.Checked = clParam.mEnableOffset;
            UDnumXoff.Value = (int)clParam.mCX;
            UDnumYoff.Value = (int)clParam.mCY;

            checkBox_EnableUserData.Checked = clParam.mEnableUserData;
            textBox_User.Text = clParam.mUserData;

            //変更完了
            isLocked = false;
        }

        /// <summary>
        /// フォーム上パラメータを取得します
        /// </summary>
        public ClsParam GetParam()
        {
            //isLocked = true;

            //以下、パラメーター取得処理
            ClsParam clParam = new ClsParam();

            clParam.mEnableDisplay = this.checkBox_Display.Checked;

            clParam.mX = (int)UDnumX.Value;
            clParam.mY = (int)UDnumY.Value;

            clParam.mEnableRotation = checkBox_EnableRotation.Checked;
            clParam.mRZ = (float)UDnumRot.Value;

            clParam.mEnableScale = checkBox_EnableScale.Checked;
            clParam.mSX = (float)UDnumSX.Value;
            clParam.mSY = (float)UDnumSY.Value;

            clParam.mEnableFlip = checkBox_EnableFlip.Checked;
            clParam.mFlipH = checkBox_FlipH.Checked;
            clParam.mFlipV = checkBox_FlipV.Checked;

            clParam.mEnableOffset = checkBox_EnableOffset.Checked;
            clParam.mCX = (int)UDnumXoff.Value;
            clParam.mCY = (int)UDnumYoff.Value;

            clParam.mEnableTrans = checkBox_EnableTrans.Checked;
            clParam.mTrans = (int)UDnumT.Value;

            clParam.mEnableColor = checkBox_EnableColor.Checked;
            clParam.mColor = ColorPanel.BackColor.ToArgb() & 0x00FFFFFF;

            clParam.mEnableUserData = checkBox_EnableUserData.Checked;
            clParam.mUserData = textBox_User.Text;

            isLocked = false;

            return clParam;
        }

        private void ColorCode_TextChanged(object sender, EventArgs e)
        {
            //PreViewColor
            string clTextSrc = ColorCode.Text;
            if (string.IsNullOrEmpty(ColorCode.Text))
            {
                clTextSrc = "";
            }
            if (clTextSrc.Length > 6)
            {
                clTextSrc = clTextSrc.Substring(0, 6);
                ColorCode.Text = clTextSrc;
            }

            //以下、色テキスト設定処理
            clTextSrc = clTextSrc.ToUpper();
            int inCnt;
            string clTextDst = "";
            for (inCnt = 0; inCnt < clTextSrc.Length; inCnt++)
            {
                char chChar = clTextSrc[inCnt];
                bool isOK = false;
                if (chChar == '0') isOK = true;
                if (chChar == '1') isOK = true;
                if (chChar == '2') isOK = true;
                if (chChar == '3') isOK = true;
                if (chChar == '4') isOK = true;
                if (chChar == '5') isOK = true;
                if (chChar == '6') isOK = true;
                if (chChar == '7') isOK = true;
                if (chChar == '8') isOK = true;
                if (chChar == '9') isOK = true;
                if (chChar == 'A') isOK = true;
                if (chChar == 'B') isOK = true;
                if (chChar == 'C') isOK = true;
                if (chChar == 'D') isOK = true;
                if (chChar == 'E') isOK = true;
                if (chChar == 'F') isOK = true;
                if (!isOK) continue;

                clTextDst += clTextSrc[inCnt];
            }
            string clTextColor = clTextDst.PadLeft(6, '0');

            //以下、色設定処理
            string clR = clTextColor.Substring(0, 2);
            int inR = Convert.ToInt32(clR, 16);
            string clG = clTextColor.Substring(2, 2);
            int inG = Convert.ToInt32(clG, 16);
            string clB = clTextColor.Substring(4, 2);
            int inB = Convert.ToInt32(clB, 16);
            ColorPanel.BackColor = Color.FromArgb(255, inR, inG, inB);

            ClsParam clParam = this.GetParam();
            this.mFormMain.ChangeElemFromParam(clParam);
        }

        private void ColorPanel_Click(object sender, EventArgs e)
        {
            //PickUP ColorDialog
            ColorDialog dlg = new ColorDialog();
            dlg.Color = ColorPanel.BackColor;
            DialogResult enREsult = dlg.ShowDialog();
            if (enREsult != DialogResult.OK) return;

            ColorPanel.BackColor = dlg.Color;
            int inColor = dlg.Color.ToArgb();
            inColor &= 0x00FFFFFF;
            ColorCode.Text = $"{inColor:X6}";   //RGB

            //この代入ではバリデードが発生しないらしく更新通知
            if (!isLocked)
            {
                ClsParam clParam = this.GetParam();
                this.mFormMain.ChangeElemFromParam(clParam);
            }
        }

        private void Param_ValueChanged(object sender, EventArgs e)
        {
            //アップダウンコントロール系
            //any Param Update どれかのチェックが変更された通知をメインに送る
            if (!isLocked)
            {
                ClsParam clParam = this.GetParam();
                this.mFormMain.ChangeElemFromParam(clParam);
            }
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
