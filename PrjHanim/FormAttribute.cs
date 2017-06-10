﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{
    public partial class FormAttribute : Form
    {
        private bool mLocked;           //パラメータ変更中にロック
        private FormMain mFormMain;
        private ClsDatElem mSelectElem; //現在選択中のエレメント
        private int mSelectFrameNo;     //現在表示中のフレーム番号

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
                this.mSelectElem = null;
                this.mSelectFrameNo = 0;
            }
            else
            {
                this.Text = ClsTool.GetWindowName("Attribute", clElem, inSelectFrameNo);
                this.groupBox_Param.Enabled = true;
                this.mSelectElem = clElem;
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

            this.checkBox_EnableDisplayKeyFrame.Checked = clParam.mEnableDisplayKeyFrame;
            this.checkBox_EnableDisplayParent.Checked = clParam.mEnableDisplayParent;
            this.checkBox_Display.Checked = clParam.mDisplay;

            this.checkBox_EnablePositionKeyFrame.Checked = clParam.mEnablePositionKeyFrame;
            this.checkBox_EnablePositionXTween.Checked = clParam.mEnablePositionXTween;
            this.checkBox_EnablePositionYTween.Checked = clParam.mEnablePositionYTween;
            this.button_TweenX.BackgroundImage = (clParam.mTweenPositionX == null) ? null : clParam.mTweenPositionX.mImage;
            this.button_TweenY.BackgroundImage = (clParam.mTweenPositionY == null) ? null : clParam.mTweenPositionY.mImage;
            this.button_TweenX.Tag = clParam.mTweenPositionX;
            this.button_TweenY.Tag = clParam.mTweenPositionY;
            this.UDnumX.Value = (int)clParam.mX;
            this.UDnumY.Value = (int)clParam.mY;

            this.checkBox_EnableRotationOption.Checked = clParam.mEnableRotationOption;
            this.checkBox_EnableRotationKeyFrame.Checked = clParam.mEnableRotationKeyFrame;
            this.checkBox_EnableRotationTween.Checked = clParam.mEnableRotationTween;
            this.button_TweenRZ.BackgroundImage = (clParam.mTweenRotation == null) ? null : clParam.mTweenRotation.mImage;
            this.button_TweenRZ.Tag = clParam.mTweenRotation;
            this.UDnumRot.Value = (decimal)clParam.mRZ;

            this.checkBox_EnableScaleOption.Checked = clParam.mEnableScaleOption;
            this.checkBox_EnableScaleKeyFrame.Checked = clParam.mEnableScaleKeyFrame;
            this.checkBox_EnableScaleXTween.Checked = clParam.mEnableScaleXTween;
            this.checkBox_EnableScaleYTween.Checked = clParam.mEnableScaleYTween;
            this.button_TweenSX.BackgroundImage = (clParam.mTweenScaleX == null) ? null : clParam.mTweenScaleX.mImage;
            this.button_TweenSY.BackgroundImage = (clParam.mTweenScaleY == null) ? null : clParam.mTweenScaleY.mImage;
            this.button_TweenSX.Tag = clParam.mTweenScaleX;
            this.button_TweenSY.Tag = clParam.mTweenScaleY;
            this.UDnumSX.Value = (decimal)clParam.mSX;
            this.UDnumSY.Value = (decimal)clParam.mSY;

            this.checkBox_EnableOffsetOption.Checked = clParam.mEnableOffsetOption;
            this.checkBox_EnableOffsetKeyFrame.Checked = clParam.mEnableOffsetKeyFrame;
            this.checkBox_EnableOffsetParent.Checked = clParam.mEnableOffsetParent;
            this.checkBox_EnableOffsetXTween.Checked = clParam.mEnableOffsetXTween;
            this.checkBox_EnableOffsetYTween.Checked = clParam.mEnableOffsetYTween;
            this.button_TweenCX.BackgroundImage = (clParam.mTweenOffsetX == null) ? null : clParam.mTweenOffsetX.mImage;
            this.button_TweenCY.BackgroundImage = (clParam.mTweenOffsetY == null) ? null : clParam.mTweenOffsetY.mImage;
            this.button_TweenCX.Tag = clParam.mTweenOffsetX;
            this.button_TweenCY.Tag = clParam.mTweenOffsetY;
            this.UDnumXoff.Value = (int)clParam.mCX;
            this.UDnumYoff.Value = (int)clParam.mCY;

            this.checkBox_EnableFlipOption.Checked = clParam.mEnableFlipOption;
            this.checkBox_EnableFlipKeyFrame.Checked = clParam.mEnableFlipKeyFrame;
            this.checkBox_EnableFlipParent.Checked = clParam.mEnableFlipParent;
            this.checkBox_FlipH.Checked = clParam.mFlipH;
            this.checkBox_FlipV.Checked = clParam.mFlipV;

            this.checkBox_EnableTransOption.Checked = clParam.mEnableTransOption;
            this.checkBox_EnableTransKeyFrame.Checked = clParam.mEnableTransKeyFrame;
            this.checkBox_EnableTransParent.Checked = clParam.mEnableTransParent;
            this.checkBox_EnableTransTween.Checked = clParam.mEnableTransTween;
            this.button_TweenT.BackgroundImage = (clParam.mTweenTrans == null) ? null : clParam.mTweenTrans.mImage;
            this.button_TweenT.Tag = clParam.mTweenTrans;
            this.UDnumT.Value = (decimal)clParam.mTrans;

            this.checkBox_EnableColorOption.Checked = clParam.mEnableColorOption;
            this.checkBox_EnableColorKeyFrame.Checked = clParam.mEnableColorKeyFrame;
            this.checkBox_EnableColorParent.Checked = clParam.mEnableColorParent;
            this.checkBox_EnableColorTween.Checked = clParam.mEnableColorTween;
            this.button_TweenC.BackgroundImage = (clParam.mTweenColor == null) ? null : clParam.mTweenColor.mImage;
            this.button_TweenC.Tag = clParam.mTweenColor;
            this.textBox_C.Text = $"{clParam.mColor:X6}";

            this.checkBox_EnableUserDataOption.Checked = clParam.mEnableUserDataOption;
            this.checkBox_EnableUserDataKeyFrame.Checked = clParam.mEnableUserDataKeyFrame;
            this.textBox_UT.Text = clParam.mUserData;

            //以下、リフレッシュ処理
            this.Refresh();

            //変更完了
            this.mLocked = false;
        }

        /// <summary>
        /// フォーム上パラメータを取得します
        /// </summary>
        public ClsParam GetParam()
        {
            //以下、パラメーター取得処理
            ClsParam clParam = new ClsParam();

            clParam.mName = this.mSelectElem.mName;

            clParam.mEnableDisplayKeyFrame = this.checkBox_EnableDisplayKeyFrame.Checked;
            clParam.mEnableDisplayParent = this.checkBox_EnableDisplayParent.Checked;
            clParam.mDisplay = this.checkBox_Display.Checked;

            clParam.mEnablePositionKeyFrame = this.checkBox_EnablePositionKeyFrame.Checked;
            clParam.mEnablePositionXTween = this.checkBox_EnablePositionXTween.Checked;
            clParam.mEnablePositionYTween = this.checkBox_EnablePositionYTween.Checked;
            clParam.mTweenPositionX = this.button_TweenX.Tag as ClsDatTween;
            clParam.mTweenPositionY = this.button_TweenY.Tag as ClsDatTween;
            clParam.mX = (int)this.UDnumX.Value;
            clParam.mY = (int)this.UDnumY.Value;

            clParam.mEnableRotationOption = this.checkBox_EnableRotationOption.Checked;
            clParam.mEnableRotationKeyFrame = this.checkBox_EnableRotationKeyFrame.Checked;
            clParam.mEnableRotationTween = this.checkBox_EnableRotationTween.Checked;
            clParam.mTweenRotation = this.button_TweenRZ.Tag as ClsDatTween;
            clParam.mRZ = (float)this.UDnumRot.Value;

            clParam.mEnableScaleOption = this.checkBox_EnableScaleOption.Checked;
            clParam.mEnableScaleKeyFrame = this.checkBox_EnableScaleKeyFrame.Checked;
            clParam.mEnableScaleXTween = this.checkBox_EnableScaleXTween.Checked;
            clParam.mEnableScaleYTween = this.checkBox_EnableScaleYTween.Checked;
            clParam.mTweenScaleX = this.button_TweenSX.Tag as ClsDatTween;
            clParam.mTweenScaleY = this.button_TweenSY.Tag as ClsDatTween;
            clParam.mSX = (float)this.UDnumSX.Value;
            clParam.mSY = (float)this.UDnumSY.Value;

            clParam.mEnableOffsetOption = this.checkBox_EnableOffsetOption.Checked;
            clParam.mEnableOffsetKeyFrame = this.checkBox_EnableOffsetKeyFrame.Checked;
            clParam.mEnableOffsetParent = this.checkBox_EnableOffsetParent.Checked;
            clParam.mEnableOffsetXTween = this.checkBox_EnableOffsetXTween.Checked;
            clParam.mEnableOffsetYTween = this.checkBox_EnableOffsetYTween.Checked;
            clParam.mTweenOffsetX = this.button_TweenCX.Tag as ClsDatTween;
            clParam.mTweenOffsetY = this.button_TweenCY.Tag as ClsDatTween;
            clParam.mCX = (int)this.UDnumXoff.Value;
            clParam.mCY = (int)this.UDnumYoff.Value;

            clParam.mEnableFlipOption = this.checkBox_EnableFlipOption.Checked;
            clParam.mEnableFlipKeyFrame = this.checkBox_EnableFlipKeyFrame.Checked;
            clParam.mEnableFlipParent = this.checkBox_EnableFlipParent.Checked;
            clParam.mFlipH = this.checkBox_FlipH.Checked;
            clParam.mFlipV = this.checkBox_FlipV.Checked;

            clParam.mEnableTransOption = this.checkBox_EnableTransOption.Checked;
            clParam.mEnableTransKeyFrame = this.checkBox_EnableTransKeyFrame.Checked;
            clParam.mEnableTransParent = this.checkBox_EnableTransParent.Checked;
            clParam.mEnableTransTween = this.checkBox_EnableTransTween.Checked;
            clParam.mTweenTrans = this.button_TweenT.Tag as ClsDatTween;
            clParam.mTrans = (int)this.UDnumT.Value;

            clParam.mEnableColorOption = this.checkBox_EnableColorOption.Checked;
            clParam.mEnableColorKeyFrame = this.checkBox_EnableColorKeyFrame.Checked;
            clParam.mEnableColorParent = this.checkBox_EnableColorParent.Checked;
            clParam.mEnableColorTween = this.checkBox_EnableColorTween.Checked;
            clParam.mTweenColor = this.button_TweenC.Tag as ClsDatTween;
            clParam.mColor = this.button_C.BackColor.ToArgb() & 0x00FFFFFF;

            clParam.mEnableUserDataOption = this.checkBox_EnableUserDataOption.Checked;
            clParam.mEnableUserDataKeyFrame = this.checkBox_EnableUserDataKeyFrame.Checked;
            clParam.mUserData = this.textBox_UT.Text;

            return (clParam);
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

            if (!this.mLocked)
            {
                ClsParam clParam = this.GetParam();
                this.ChangeElemFromParam(clParam);
            }
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

            if (!this.mLocked)
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
        /// <param name="isParentFlag">親の設定に依存するかどうか</param>
        /// <param name="clValue1">値１</param>
        /// <param name="clValue2">値２</param>
        private void ChangeElem(ClsDatElem clElem, TYPE_OPTION enTypeOption, int inSelectFrameNo, bool isExistOption, bool isExistKeyFrame, bool isParentFlag, object clValue1, object clValue2)
        {
            if (isExistOption)
            {
                ClsDatOption clOption = null;

                if (inSelectFrameNo == 0)
                {
                    clElem.SetOption(enTypeOption, isParentFlag, clValue1, clValue2);
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
                        isParentFlag = ClsParam.GetDefaultParentFlag(clElem.mElem, enTypeOption);
                        clValue1 = ClsParam.GetDefaultValue1(enTypeOption);
                        clValue2 = ClsParam.GetDefaultValue2(enTypeOption);
                        clElem.SetOption(enTypeOption, isParentFlag, clValue1, clValue2);
                        clOption = clElem.GetOption(enTypeOption);
                    }

                    if (isExistKeyFrame)
                    {
                        clOption.SetKeyFrame(inSelectFrameNo, isParentFlag, clValue1, clValue2);  //追加または更新
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
            this.ChangeElem(clElem, TYPE_OPTION.DISPLAY, inSelectFrameNo, true, clParam.mEnableDisplayKeyFrame, clParam.mEnableDisplayParent, clParam.mDisplay, clValue2);

            //以下、座標設定
            bool isParentFlag = ClsParam.GetDefaultParentFlag(clElem.mElem, TYPE_OPTION.POSITION);
            this.ChangeElem(clElem, TYPE_OPTION.POSITION, inSelectFrameNo, true, clParam.mEnablePositionKeyFrame, isParentFlag, clParam.mX, clParam.mY);

            //以下、回転設定
            isParentFlag = ClsParam.GetDefaultParentFlag(clElem.mElem, TYPE_OPTION.ROTATION);
            clValue2 = ClsParam.GetDefaultValue2(TYPE_OPTION.ROTATION);
            this.ChangeElem(clElem, TYPE_OPTION.ROTATION, inSelectFrameNo, clParam.mEnableRotationOption, clParam.mEnableRotationKeyFrame, isParentFlag, clParam.mRZ, clValue2);

            //以下、スケール設定
            isParentFlag = ClsParam.GetDefaultParentFlag(clElem.mElem, TYPE_OPTION.SCALE);
            this.ChangeElem(clElem, TYPE_OPTION.SCALE, inSelectFrameNo, clParam.mEnableScaleOption, clParam.mEnableScaleKeyFrame, isParentFlag, clParam.mSX, clParam.mSY);

            //以下、オフセット設定
            this.ChangeElem(clElem, TYPE_OPTION.OFFSET, inSelectFrameNo, clParam.mEnableOffsetOption, clParam.mEnableOffsetKeyFrame, clParam.mEnableOffsetParent, clParam.mCX, clParam.mCY);

            //以下、反転設定
            this.ChangeElem(clElem, TYPE_OPTION.FLIP, inSelectFrameNo, clParam.mEnableFlipOption, clParam.mEnableFlipKeyFrame, clParam.mEnableFlipParent, clParam.mFlipH, clParam.mFlipV);

            //以下、透明設定
            clValue2 = ClsParam.GetDefaultValue2(TYPE_OPTION.TRANSPARENCY);
            this.ChangeElem(clElem, TYPE_OPTION.TRANSPARENCY, inSelectFrameNo, clParam.mEnableTransOption, clParam.mEnableTransKeyFrame, clParam.mEnableTransParent, clParam.mTrans, clValue2);

            //以下、カラー設定 
            clValue2 = ClsParam.GetDefaultValue2(TYPE_OPTION.COLOR);
            this.ChangeElem(clElem, TYPE_OPTION.COLOR, inSelectFrameNo, clParam.mEnableColorOption, clParam.mEnableColorKeyFrame, clParam.mEnableColorParent, clParam.mColor, clValue2);

            //以下、ユーザーデータ設定 
            isParentFlag = ClsParam.GetDefaultParentFlag(clElem.mElem, TYPE_OPTION.USER_DATA);
            clValue2 = ClsParam.GetDefaultValue2(TYPE_OPTION.USER_DATA);
            this.ChangeElem(clElem, TYPE_OPTION.USER_DATA, inSelectFrameNo, clParam.mEnableUserDataOption, clParam.mEnableUserDataKeyFrame, isParentFlag, clParam.mUserData, clValue2);

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

            if (!this.mLocked)
            {
                ClsParam clParam = this.GetParam();
                this.ChangeElemFromParam(clParam);
            }

            //以下、表示設定
            bool isCheckOption;
            bool isCheckTween;
            bool isCheckKeyFrame = this.checkBox_EnableDisplayKeyFrame.Checked;
            this.checkBox_EnableDisplayParent.Enabled = isCheckKeyFrame;
            this.label_Display.Enabled = isCheckKeyFrame;
            this.checkBox_Display.Enabled = isCheckKeyFrame;

            //以下、座標設定
            isCheckKeyFrame = this.checkBox_EnablePositionKeyFrame.Checked;
            this.label_X.Enabled = isCheckKeyFrame;
            this.label_Y.Enabled = isCheckKeyFrame;
            this.UDnumX.Enabled = isCheckKeyFrame;
            this.UDnumY.Enabled = isCheckKeyFrame;
            this.checkBox_EnablePositionXTween.Enabled = isCheckKeyFrame;
            this.checkBox_EnablePositionYTween.Enabled = isCheckKeyFrame;
            isCheckTween = this.checkBox_EnablePositionXTween.Checked;
            this.button_TweenX.Enabled = isCheckTween;
            isCheckTween = this.checkBox_EnablePositionYTween.Checked;
            this.button_TweenY.Enabled = isCheckTween;

            //以下、回転設定
            isCheckOption = this.checkBox_EnableRotationOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableRotationKeyFrame.Checked;
            this.checkBox_EnableRotationKeyFrame.Enabled = (this.mSelectFrameNo== 0) ? false : isCheckOption;
            this.label_RZ.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumRot.Enabled = (isCheckOption && isCheckKeyFrame);
            this.checkBox_EnableRotationTween.Enabled = (isCheckOption && isCheckKeyFrame);
            isCheckTween = this.checkBox_EnableRotationTween.Checked;
            this.button_TweenRZ.Enabled = isCheckTween;

            //以下、スケール設定
            isCheckOption = this.checkBox_EnableScaleOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableScaleKeyFrame.Checked;
            this.checkBox_EnableScaleKeyFrame.Enabled = (this.mSelectFrameNo == 0) ? false : isCheckOption;
            this.label_SX.Enabled = (isCheckOption && isCheckKeyFrame);
            this.label_SY.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumSX.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumSY.Enabled = (isCheckOption && isCheckKeyFrame);
            this.checkBox_EnableScaleXTween.Enabled = (isCheckOption && isCheckKeyFrame);
            this.checkBox_EnableScaleYTween.Enabled = (isCheckOption && isCheckKeyFrame);
            isCheckTween = this.checkBox_EnableScaleXTween.Checked;
            this.button_TweenSX.Enabled = isCheckTween;
            isCheckTween = this.checkBox_EnableScaleYTween.Checked;
            this.button_TweenSY.Enabled = isCheckTween;

            //以下、オフセット設定
            isCheckOption = this.checkBox_EnableOffsetOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableOffsetKeyFrame.Checked;
            this.checkBox_EnableOffsetKeyFrame.Enabled = (this.mSelectFrameNo == 0) ? false : isCheckOption;
            this.checkBox_EnableOffsetParent.Enabled = (isCheckOption && isCheckKeyFrame);
            this.label_CX.Enabled = (isCheckOption && isCheckKeyFrame);
            this.label_CY.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumXoff.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumYoff.Enabled = (isCheckOption && isCheckKeyFrame);
            this.checkBox_EnableOffsetXTween.Enabled = (isCheckOption && isCheckKeyFrame);
            this.checkBox_EnableOffsetYTween.Enabled = (isCheckOption && isCheckKeyFrame);
            isCheckTween = this.checkBox_EnableOffsetXTween.Checked;
            this.button_TweenCX.Enabled = isCheckTween;
            isCheckTween = this.checkBox_EnableOffsetYTween.Checked;
            this.button_TweenCY.Enabled = isCheckTween;

            //以下、反転設定
            isCheckOption = this.checkBox_EnableFlipOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableFlipKeyFrame.Checked;
            this.checkBox_EnableFlipKeyFrame.Enabled = (this.mSelectFrameNo == 0) ? false : isCheckOption;
            this.checkBox_EnableFlipParent.Enabled = (isCheckOption && isCheckKeyFrame);
            this.label_FlipH.Enabled = (isCheckOption && isCheckKeyFrame);
            this.label_FlipV.Enabled = (isCheckOption && isCheckKeyFrame);
            this.checkBox_FlipH.Enabled = (isCheckOption && isCheckKeyFrame);
            this.checkBox_FlipV.Enabled = (isCheckOption && isCheckKeyFrame);

            //以下、透明値設定
            isCheckOption = this.checkBox_EnableTransOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableTransKeyFrame.Checked;
            this.checkBox_EnableTransKeyFrame.Enabled = (this.mSelectFrameNo == 0) ? false : isCheckOption;
            this.checkBox_EnableTransParent.Enabled = (isCheckOption && isCheckKeyFrame);
            this.label_T.Enabled = (isCheckOption && isCheckKeyFrame);
            this.UDnumT.Enabled = (isCheckOption && isCheckKeyFrame);
            this.checkBox_EnableTransTween.Enabled = (isCheckOption && isCheckKeyFrame);
            isCheckTween = this.checkBox_EnableTransTween.Checked;
            this.button_TweenT.Enabled = isCheckTween;

            //以下、色設定
            isCheckOption = this.checkBox_EnableColorOption.Checked;
            isCheckKeyFrame = this.checkBox_EnableColorKeyFrame.Checked;
            this.checkBox_EnableColorKeyFrame.Enabled = (this.mSelectFrameNo == 0) ? false : isCheckOption;
            this.checkBox_EnableColorParent.Enabled = (isCheckOption && isCheckKeyFrame);
            this.label_C.Enabled = (isCheckOption && isCheckKeyFrame);
            this.button_C.Enabled = (isCheckOption && isCheckKeyFrame);
            this.textBox_C.Enabled = (isCheckOption && isCheckKeyFrame);
            this.checkBox_EnableColorTween.Enabled = (isCheckOption && isCheckKeyFrame);
            isCheckTween = this.checkBox_EnableColorTween.Checked;
            this.button_TweenC.Enabled = isCheckTween;

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

        private void button_TweenX_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.POSITION_X, 10, 20, 15);
            clForm.ShowDialog();
            if (clForm.DialogResult != DialogResult.OK) return;

            ClsDatTween clTween = clForm.GetTween();
            this.button_TweenX.BackgroundImage = clTween.mImage;
        }

        private void button_TweenY_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.POSITION_Y, 10, 20, 15);
            clForm.ShowDialog();
            if (clForm.DialogResult != DialogResult.OK) return;

            ClsDatTween clTween = clForm.GetTween();
            this.button_TweenY.BackgroundImage = clTween.mImage;
        }

        private void button_TweenRZ_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.ROTATION, 10, 20, 15);
            clForm.ShowDialog();
            if (clForm.DialogResult != DialogResult.OK) return;

            ClsDatTween clTween = clForm.GetTween();
            this.button_TweenRZ.BackgroundImage = clTween.mImage;
        }

        private void button_TweenSX_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.SCALE_X, 10, 20, 15);
            clForm.ShowDialog();
            if (clForm.DialogResult != DialogResult.OK) return;

            ClsDatTween clTween = clForm.GetTween();
            this.button_TweenSX.BackgroundImage = clTween.mImage;
        }

        private void button_TweenSY_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.SCALE_Y, 10, 20, 15);
            clForm.ShowDialog();
            if (clForm.DialogResult != DialogResult.OK) return;

            ClsDatTween clTween = clForm.GetTween();
            this.button_TweenSY.BackgroundImage = clTween.mImage;
        }

        private void button_TweenCX_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.SCALE_X, 10, 20, 15);
            clForm.ShowDialog();
            if (clForm.DialogResult != DialogResult.OK) return;

            ClsDatTween clTween = clForm.GetTween();
            this.button_TweenCX.BackgroundImage = clTween.mImage;
        }

        private void button_TweenCY_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.SCALE_Y, 10, 20, 15);
            clForm.ShowDialog();
            if (clForm.DialogResult != DialogResult.OK) return;

            ClsDatTween clTween = clForm.GetTween();
            this.button_TweenCY.BackgroundImage = clTween.mImage;
        }

        private void button_TweenT_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.TRANS, 10, 20, 15);
            clForm.ShowDialog();
            if (clForm.DialogResult != DialogResult.OK) return;

            ClsDatTween clTween = clForm.GetTween();
            this.button_TweenT.BackgroundImage = clTween.mImage;
        }

        private void button_TweenC_Click(object sender, EventArgs e)
        {
            FormRateGraph clForm = new FormRateGraph(this.mFormMain, ClsDatTween.EnmParam.COLOR, 10, 20, 15);
            clForm.ShowDialog();
            if (clForm.DialogResult != DialogResult.OK) return;

            ClsDatTween clTween = clForm.GetTween();
            this.button_TweenC.BackgroundImage = clTween.mImage;
        }
    }
}
