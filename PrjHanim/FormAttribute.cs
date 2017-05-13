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

            this.checkBox_EnableDisplayKeyFrame.Checked = clParam.mExistDisplayKeyFrame;
            this.checkBox_Display.Checked = clParam.mDisplay;

            this.checkBox_EnablePositionKeyFrame.Checked = clParam.mExistPositionKeyFrame;
            this.UDnumX.Value = (int)clParam.mX;
            this.UDnumY.Value = (int)clParam.mY;

            this.checkBox_EnableRotationOption.Checked = clParam.mExistRotationOption;
            this.checkBox_EnableRotationKeyFrame.Checked = clParam.mExistRotationKeyFrame;
            this.UDnumRot.Value = (decimal)clParam.mRZ;

            this.checkBox_EnableScaleOption.Checked = clParam.mExistScaleOption;
            this.checkBox_EnableScaleKeyFrame.Checked = clParam.mExistScaleKeyFrame;
            this.UDnumSX.Value = (decimal)clParam.mSX;
            this.UDnumSY.Value = (decimal)clParam.mSY;

            this.checkBox_EnableFlipOption.Checked = clParam.mExistFlipOption;
            this.checkBox_EnableFlipKeyFrame.Checked = clParam.mExistFlipKeyFrame;
            this.checkBox_FlipH.Checked = clParam.mFlipH;
            this.checkBox_FlipV.Checked = clParam.mFlipV;

            this.checkBox_EnableTransOption.Checked = clParam.mExistTransOption;
            this.checkBox_EnableTransKeyFrame.Checked = clParam.mExistTransKeyFrame;
            this.UDnumT.Value = (decimal)clParam.mTrans;

            this.checkBox_EnableColorOption.Checked = clParam.mExistColorOption;
            this.checkBox_EnableColorKeyFrame.Checked = clParam.mExistColorKeyFrame;
            this.textBox_C.Text = $"{clParam.mColor:X6}";

            this.checkBox_EnableOffsetOption.Checked = clParam.mExistOffsetOption;
            this.checkBox_EnableOffsetKeyFrame.Checked = clParam.mExistOffsetKeyFrame;
            this.UDnumXoff.Value = (int)clParam.mCX;
            this.UDnumYoff.Value = (int)clParam.mCY;

            this.checkBox_EnableUserDataOption.Checked = clParam.mExistUserDataOption;
            this.checkBox_EnableUserDataKeyFrame.Checked = clParam.mExistUserDataKeyFrame;
            this.textBox_UT.Text = clParam.mUserData;

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

            clParam.mExistDisplayKeyFrame = this.checkBox_EnableDisplayKeyFrame.Checked;
            clParam.mDisplay = this.checkBox_Display.Checked;

            clParam.mExistPositionKeyFrame = this.checkBox_EnablePositionKeyFrame.Checked;
            clParam.mX = (int)UDnumX.Value;
            clParam.mY = (int)UDnumY.Value;

            clParam.mExistRotationOption = checkBox_EnableRotationOption.Checked;
            clParam.mExistRotationKeyFrame = checkBox_EnableRotationKeyFrame.Checked;
            clParam.mRZ = (float)UDnumRot.Value;

            clParam.mExistScaleOption = checkBox_EnableScaleOption.Checked;
            clParam.mExistScaleKeyFrame = checkBox_EnableScaleKeyFrame.Checked;
            clParam.mSX = (float)UDnumSX.Value;
            clParam.mSY = (float)UDnumSY.Value;

            clParam.mExistFlipOption = checkBox_EnableFlipOption.Checked;
            clParam.mExistFlipKeyFrame = checkBox_EnableFlipKeyFrame.Checked;
            clParam.mFlipH = checkBox_FlipH.Checked;
            clParam.mFlipV = checkBox_FlipV.Checked;

            clParam.mExistOffsetOption = checkBox_EnableOffsetOption.Checked;
            clParam.mExistOffsetKeyFrame = checkBox_EnableOffsetKeyFrame.Checked;
            clParam.mCX = (int)UDnumXoff.Value;
            clParam.mCY = (int)UDnumYoff.Value;

            clParam.mExistTransOption = checkBox_EnableTransOption.Checked;
            clParam.mExistTransKeyFrame = checkBox_EnableTransKeyFrame.Checked;
            clParam.mTrans = (int)UDnumT.Value;

            clParam.mExistColorOption = checkBox_EnableColorOption.Checked;
            clParam.mExistColorKeyFrame = checkBox_EnableColorKeyFrame.Checked;
            clParam.mColor = button_C.BackColor.ToArgb() & 0x00FFFFFF;

            clParam.mExistUserDataOption = checkBox_EnableUserDataOption.Checked;
            clParam.mExistUserDataKeyFrame = checkBox_EnableUserDataKeyFrame.Checked;
            clParam.mUserData = textBox_UT.Text;

            isLocked = false;

            return clParam;
        }

        private void ColorCode_TextChanged(object sender, EventArgs e)
        {
            //PreViewColor
            string clTextSrc = textBox_C.Text;
            if (string.IsNullOrEmpty(textBox_C.Text))
            {
                clTextSrc = "";
            }
            if (clTextSrc.Length > 6)
            {
                clTextSrc = clTextSrc.Substring(0, 6);
                textBox_C.Text = clTextSrc;
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
            this.button_C.BackColor = Color.FromArgb(255, inR, inG, inB);

            ClsParam clParam = this.GetParam();
            this.mFormMain.ChangeElemFromParam(clParam);
        }

        private void ColorPanel_Click(object sender, EventArgs e)
        {
            //PickUP ColorDialog
            ColorDialog dlg = new ColorDialog();
            dlg.Color = this.button_C.BackColor;
            DialogResult enREsult = dlg.ShowDialog();
            if (enREsult != DialogResult.OK) return;

            this.button_C.BackColor = dlg.Color;
            int inColor = dlg.Color.ToArgb();
            inColor &= 0x00FFFFFF;
            textBox_C.Text = $"{inColor:X6}";   //RGB

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

            bool isCheckOption;
            bool isCheckKeyFrame = this.checkBox_EnableDisplayKeyFrame.Checked;
            this.label_Display.Enabled = isCheckKeyFrame;
            this.checkBox_Display.Enabled = isCheckKeyFrame;

            isCheckKeyFrame = this.checkBox_EnablePositionKeyFrame.Checked;
            this.label_X.Enabled = isCheckKeyFrame;
            this.label_Y.Enabled = isCheckKeyFrame;
            this.UDnumX.Enabled = isCheckKeyFrame;
            this.UDnumY.Enabled = isCheckKeyFrame;
            this.button_TweenX.Enabled = isCheckKeyFrame;
            this.button_TweenY.Enabled = isCheckKeyFrame;

            isCheckOption = this.checkBox_EnableRotationOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableRotationKeyFrame.Checked;
            this.checkBox_EnableRotationKeyFrame.Enabled = isCheckOption;
            this.label_RZ.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumRot.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_TweenRZ.Enabled = (isCheckOption && isCheckKeyFrame);

            isCheckOption = this.checkBox_EnableScaleOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableScaleKeyFrame.Checked;
            this.checkBox_EnableScaleKeyFrame.Enabled = isCheckOption;
            this.label_SX.Enabled = (isCheckOption && isCheckKeyFrame);
            this.label_SY.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumSX.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumSY.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_TweenSX.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_TweenSY.Enabled = (isCheckOption && isCheckKeyFrame);

            isCheckOption = this.checkBox_EnableOffsetOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableOffsetKeyFrame.Checked;
            this.checkBox_EnableOffsetKeyFrame.Enabled = isCheckOption;
            this.label_CX.Enabled = (isCheckOption && isCheckKeyFrame);
            this.label_CY.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumXoff.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumYoff.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_TweenCX.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_TweenCY.Enabled = (isCheckOption && isCheckKeyFrame);

            isCheckOption = this.checkBox_EnableFlipOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableFlipKeyFrame.Checked;
            this.checkBox_EnableFlipKeyFrame.Enabled = isCheckOption;
            this.label_FlipH.Enabled = (isCheckOption && isCheckKeyFrame);
            this.label_FlipV.Enabled = (isCheckOption && isCheckKeyFrame);
            this.checkBox_FlipH.Enabled = (isCheckOption && isCheckKeyFrame);
            this.checkBox_FlipV.Enabled = (isCheckOption && isCheckKeyFrame);

            isCheckOption = this.checkBox_EnableTransOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableTransKeyFrame.Checked;
            this.checkBox_EnableTransKeyFrame.Enabled = isCheckOption;
            this.label_T.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumT.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_TweenT.Enabled = (isCheckOption && isCheckKeyFrame);

            isCheckOption = this.checkBox_EnableColorOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableColorKeyFrame.Checked;
            this.checkBox_EnableColorKeyFrame.Enabled = isCheckOption;
            this.label_C.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_C.Enabled = (isCheckOption && isCheckKeyFrame);
            this.textBox_C.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_TweenC.Enabled = (isCheckOption && isCheckKeyFrame);

            isCheckOption = this.checkBox_EnableUserDataOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableUserDataKeyFrame.Checked;
            this.checkBox_EnableUserDataKeyFrame.Enabled = isCheckOption;
            this.label_UT.Enabled = (isCheckOption && isCheckKeyFrame);
            this.textBox_UT.Enabled = (isCheckOption && isCheckKeyFrame);
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
