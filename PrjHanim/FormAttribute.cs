using System;
using System.Drawing;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{
    public partial class FormAttribute : Form
    {
        private bool mLocked;       //パラメータ変更中にロック
        private FormMain mFormMain;
        private int mSelectFrameNo; //現在表示中のフレーム番号

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="clFormMain">メインフォーム</param>
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
        /// リフレッシュ処理
        /// </summary>
        public void RefreshAll()
        {
            this.Refresh();
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="clElem">選択中のエレメント</param>
        /// <param name="inSelectFrameNo">選択中のフレーム番号</param>
        /// <param name="inMaxFrameNum">フレーム数</param>
        public void Init(ClsDatElem clElem, int inSelectFrameNo, int inMaxFrameNum)
        {
            if (clElem == null)
            {
                this.Text = "Attribute";
                this.groupBox_Param.Enabled = false;
                this.mSelectFrameNo = 0;
            }
            else
            {
                this.Text = ClsTool.GetWindowName("Attribute", clElem, inSelectFrameNo);
                this.groupBox_Param.Enabled = true;
                this.mSelectFrameNo = inSelectFrameNo;

                //以下、チェック状態の設定
                ClsParam clParam = clElem.GetParamNow(inSelectFrameNo, inMaxFrameNum);
                this.SetParam(clParam, inSelectFrameNo);
            }
        }

        /// <summary>
        /// フォームにパラメータをセットします
        /// </summary>
        /// <param name="clParam">パラメータ</param>
        /// <param name="inSelectFrameNo">選択中のフレーム番号</param>
        private void SetParam(ClsParam clParam, int inSelectFrameNo)
        {
            //変更終わるまでロックしないと毎回ChangeValueが発生してしまう
            this.mLocked = true;

            if (inSelectFrameNo == 0)
            {
                this.checkBox_EnableDisplayKeyFrame.Enabled = false;
                this.checkBox_EnablePositionKeyFrame.Enabled = false;

                this.checkBox_EnableRotationKeyFrame.Enabled = false;
                this.checkBox_EnableScaleKeyFrame.Enabled = false;
                this.checkBox_EnableFlipKeyFrame.Enabled = false;
                this.checkBox_EnableTransKeyFrame.Enabled = false;
                this.checkBox_EnableColorKeyFrame.Enabled = false;
                this.checkBox_EnableOffsetKeyFrame.Enabled = false;
                this.checkBox_EnableUserDataKeyFrame.Enabled = false;
            }
            else
            {
                this.checkBox_EnableDisplayKeyFrame.Enabled = true;
                this.checkBox_EnablePositionKeyFrame.Enabled = true;
            }

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
            this.mLocked = false;
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

            this.mLocked = false;

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
            this.ChangeElemFromParam(clParam);
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
            if (!mLocked)
            {
                ClsParam clParam = this.GetParam();
                this.ChangeElemFromParam(clParam);
            }
        }

        /// <summary>
        /// エレメント情報変更処理
        /// </summary>
        /// <param name="clElem">変更対象となるエレメント</param>
        /// <param name="enTypeOption">オプションタイプ</param>
        /// <param name="inSelectFrameNo">フレーム番号</param>
        /// <param name="isExistOption">オプション存在フラグ</param>
        /// <param name="isExistKeyFrame">キーフレーム存在フラグ</param>
        /// <param name="clValue1">値１</param>
        /// <param name="clValue2">値２</param>
        private void ChangeElem(ClsDatElem clElem, TYPE_OPTION enTypeOption, int inSelectFrameNo, bool isExistOption, bool isExistKeyFrame, object clValue1, object clValue2)
        {
            if (isExistOption)
            {
                ClsDatOption clOption = null;

                if (inSelectFrameNo == 0)
                {
                    clElem.SetOption(enTypeOption, clValue1, clValue2);
                }
                else
                {
                    bool isExist = clElem.IsExistOption(enTypeOption);
                    if (isExist)
                    {
                        clOption = clElem.GetOption(enTypeOption);
                    }
                    else
                    {
                        clValue1 = ClsParam.GetDefaultValue1(enTypeOption);
                        clValue2 = ClsParam.GetDefaultValue2(enTypeOption);
                        clElem.SetOption(enTypeOption, clValue1, clValue2);
                        clOption = clElem.GetOption(enTypeOption);
                    }

                    if (isExistKeyFrame)
                    {
                        clOption.SetKeyFrame(inSelectFrameNo, clValue1, clValue2);  //追加または更新
                    }
                    else
                    {
                        clOption.RemoveKeyFrame(inSelectFrameNo);
                    }
                }
            }
            else
            {
                clElem.RemoveOption(enTypeOption, false);
            }
        }

        /// <summary>
        /// オプションの情報を修正する処理
        /// </summary>
        /// <param name="clParam">パラメーター情報</param>
        public void ChangeElemFromParam(ClsParam clParam)
        {
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion == null) return;

            ClsDatElem clElem = ClsSystem.GetElemFromSelectLineNo();
            if (clElem == null) return;

            int inSelectFrameNo = ClsSystem.GetSelectFrameNo();

            //以下、表示設定
            object clValue2 = ClsParam.GetDefaultValue2(TYPE_OPTION.DISPLAY);
            this.ChangeElem(clElem, TYPE_OPTION.DISPLAY, inSelectFrameNo, true, clParam.mExistDisplayKeyFrame, clParam.mDisplay, clValue2);

            //以下、座標設定
            this.ChangeElem(clElem, TYPE_OPTION.POSITION, inSelectFrameNo, true, clParam.mExistPositionKeyFrame, clParam.mX, clParam.mY);

            //以下、回転設定
            clValue2 = ClsParam.GetDefaultValue2(TYPE_OPTION.ROTATION);
            this.ChangeElem(clElem, TYPE_OPTION.ROTATION, inSelectFrameNo, clParam.mExistRotationOption, clParam.mExistRotationKeyFrame, clParam.mRZ, clValue2);

            //以下、スケール設定
            this.ChangeElem(clElem, TYPE_OPTION.SCALE, inSelectFrameNo, clParam.mExistScaleOption, clParam.mExistScaleKeyFrame, clParam.mSX, clParam.mSY);

            //以下、オフセット設定
            this.ChangeElem(clElem, TYPE_OPTION.OFFSET, inSelectFrameNo, clParam.mExistOffsetOption, clParam.mExistOffsetKeyFrame, clParam.mCX, clParam.mCY);

            //以下、反転設定
            this.ChangeElem(clElem, TYPE_OPTION.FLIP, inSelectFrameNo, clParam.mExistFlipOption, clParam.mExistFlipKeyFrame, clParam.mFlipH, clParam.mFlipV);

            //以下、透明設定
            clValue2 = ClsParam.GetDefaultValue2(TYPE_OPTION.TRANSPARENCY);
            this.ChangeElem(clElem, TYPE_OPTION.TRANSPARENCY, inSelectFrameNo, clParam.mExistTransOption, clParam.mExistTransKeyFrame, clParam.mTrans, clValue2);

            //以下、カラー設定 
            clValue2 = ClsParam.GetDefaultValue2(TYPE_OPTION.COLOR);
            this.ChangeElem(clElem, TYPE_OPTION.COLOR, inSelectFrameNo, clParam.mExistColorOption, clParam.mExistColorKeyFrame, clParam.mColor, clValue2);

            //以下、ユーザーデータ設定 
            clValue2 = ClsParam.GetDefaultValue2(TYPE_OPTION.USER_DATA);
            this.ChangeElem(clElem, TYPE_OPTION.USER_DATA, inSelectFrameNo, clParam.mExistUserDataOption, clParam.mExistUserDataKeyFrame, clParam.mUserData, clValue2);

            //以下、行番号を振り直す処理
            clMotion.Assignment();

            //以下、メインウィンドウ更新処理
            this.mFormMain.RefreshAll();
        }

        private void Param_ValueChanged(object sender, EventArgs e)
        {
            if (this.mSelectFrameNo == 0)
            {
                this.checkBox_EnableDisplayKeyFrame.Checked = true;
                this.checkBox_EnablePositionKeyFrame.Checked = true;
                this.checkBox_EnableRotationKeyFrame.Checked = true;
                this.checkBox_EnableScaleKeyFrame.Checked = true;
                this.checkBox_EnableOffsetKeyFrame.Checked = true;
                this.checkBox_EnableFlipKeyFrame.Checked = true;
                this.checkBox_EnableTransKeyFrame.Checked = true;
                this.checkBox_EnableColorKeyFrame.Checked = true;
                this.checkBox_EnableUserDataKeyFrame.Checked = true;
            }

            //アップダウンコントロール系
            //any Param Update どれかのチェックが変更された通知をメインに送る
            if (!this.mLocked)
            {
                ClsParam clParam = this.GetParam();
                this.ChangeElemFromParam(clParam);
            }

            //以下、表示設定
            bool isCheckOption;
            bool isCheckKeyFrame = this.checkBox_EnableDisplayKeyFrame.Checked;
            this.label_Display.Enabled = isCheckKeyFrame;
            this.checkBox_Display.Enabled = isCheckKeyFrame;

            //以下、座標設定
            isCheckKeyFrame = this.checkBox_EnablePositionKeyFrame.Checked;
            this.label_X.Enabled = isCheckKeyFrame;
            this.label_Y.Enabled = isCheckKeyFrame;
            this.UDnumX.Enabled = isCheckKeyFrame;
            this.UDnumY.Enabled = isCheckKeyFrame;
            this.button_TweenX.Enabled = isCheckKeyFrame;
            this.button_TweenY.Enabled = isCheckKeyFrame;

            //以下、回転設定
            isCheckOption = this.checkBox_EnableRotationOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableRotationKeyFrame.Checked;
            this.checkBox_EnableRotationKeyFrame.Enabled = (this.mSelectFrameNo== 0) ? false : isCheckOption;
            this.label_RZ.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumRot.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_TweenRZ.Enabled = (isCheckOption && isCheckKeyFrame);

            //以下、スケール設定
            isCheckOption = this.checkBox_EnableScaleOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableScaleKeyFrame.Checked;
            this.checkBox_EnableScaleKeyFrame.Enabled = (this.mSelectFrameNo == 0) ? false : isCheckOption;
            this.label_SX.Enabled = (isCheckOption && isCheckKeyFrame);
            this.label_SY.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumSX.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumSY.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_TweenSX.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_TweenSY.Enabled = (isCheckOption && isCheckKeyFrame);

            //以下、オフセット設定
            isCheckOption = this.checkBox_EnableOffsetOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableOffsetKeyFrame.Checked;
            this.checkBox_EnableOffsetKeyFrame.Enabled = (this.mSelectFrameNo == 0) ? false : isCheckOption;
            this.label_CX.Enabled = (isCheckOption && isCheckKeyFrame);
            this.label_CY.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumXoff.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumYoff.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_TweenCX.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_TweenCY.Enabled = (isCheckOption && isCheckKeyFrame);

            //以下、反転設定
            isCheckOption = this.checkBox_EnableFlipOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableFlipKeyFrame.Checked;
            this.checkBox_EnableFlipKeyFrame.Enabled = (this.mSelectFrameNo == 0) ? false : isCheckOption;
            this.label_FlipH.Enabled = (isCheckOption && isCheckKeyFrame);
            this.label_FlipV.Enabled = (isCheckOption && isCheckKeyFrame);
            this.checkBox_FlipH.Enabled = (isCheckOption && isCheckKeyFrame);
            this.checkBox_FlipV.Enabled = (isCheckOption && isCheckKeyFrame);

            //以下、透明値設定
            isCheckOption = this.checkBox_EnableTransOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableTransKeyFrame.Checked;
            this.checkBox_EnableTransKeyFrame.Enabled = (this.mSelectFrameNo == 0) ? false : isCheckOption;
            this.label_T.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumT.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_TweenT.Enabled = (isCheckOption && isCheckKeyFrame);

            //以下、色設定
            isCheckOption = this.checkBox_EnableColorOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableColorKeyFrame.Checked;
            this.checkBox_EnableColorKeyFrame.Enabled = (this.mSelectFrameNo == 0) ? false : isCheckOption;
            this.label_C.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_C.Enabled = (isCheckOption && isCheckKeyFrame);
            this.textBox_C.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_TweenC.Enabled = (isCheckOption && isCheckKeyFrame);

            //以下、ユーザーデータ設定
            isCheckOption = this.checkBox_EnableUserDataOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableUserDataKeyFrame.Checked;
            this.checkBox_EnableUserDataKeyFrame.Enabled = (this.mSelectFrameNo == 0) ? false : isCheckOption;
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
