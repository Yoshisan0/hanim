using System;
using System.Collections.Generic;
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
                if (!this.mLocked)
                {
                    ClsParam clParam = clElem.GetParamNow(inSelectFrameNo, inMaxFrameNum);
                    this.SetParam(clParam, inSelectFrameNo);
                }
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
                this.checkBox_DisplayKeyFrame.Enabled = false;
                this.checkBox_PositionKeyFrame.Enabled = false;
                this.checkBox_RotationKeyFrame.Enabled = false;
                this.checkBox_ScaleKeyFrame.Enabled = false;
                this.checkBox_FlipKeyFrame.Enabled = false;
                this.checkBox_TransKeyFrame.Enabled = false;
                this.checkBox_ColorKeyFrame.Enabled = false;
                this.checkBox_OffsetKeyFrame.Enabled = false;
                this.checkBox_UserDataKeyFrame.Enabled = false;
            }
            else
            {
                this.checkBox_DisplayKeyFrame.Enabled = true;
                this.checkBox_PositionKeyFrame.Enabled = true;
            }

            this.checkBox_DisplayKeyFrame.Checked = clParam.mDisplayKeyFrame;
            this.checkBox_DisplayParent.Checked = clParam.mDisplayParent;
            this.checkBox_Display.Checked = clParam.mDisplay;

            this.checkBox_PositionKeyFrame.Checked = clParam.mPositionKeyFrame;
            this.checkBox_TweenPositionX.Enabled = clParam.mEnablePositionXTween;
            this.checkBox_TweenPositionY.Enabled = clParam.mEnablePositionYTween;
            this.checkBox_TweenPositionX.Checked = clParam.mPositionXTween;
            this.checkBox_TweenPositionY.Checked = clParam.mPositionYTween;
            this.button_TweenX.Image = (clParam.mTweenPositionX == null) ? null : clParam.mTweenPositionX.mImage;
            this.button_TweenY.Image = (clParam.mTweenPositionY == null) ? null : clParam.mTweenPositionY.mImage;
            this.button_TweenX.Tag = clParam.mTweenPositionX;
            this.button_TweenY.Tag = clParam.mTweenPositionY;
            this.UDnumX.Value = (int)clParam.mX;
            this.UDnumY.Value = (int)clParam.mY;

            this.checkBox_RotationKeyFrame.Checked = clParam.mRotationKeyFrame;
            this.checkBox_TweenRotation.Enabled = clParam.mEnableRotationTween;
            this.checkBox_TweenRotation.Checked = clParam.mRotationTween;
            this.button_TweenRZ.Image = (clParam.mTweenRotation == null) ? null : clParam.mTweenRotation.mImage;
            this.button_TweenRZ.Tag = clParam.mTweenRotation;
            this.UDnumRot.Value = (decimal)clParam.mRZ;

            this.checkBox_ScaleKeyFrame.Checked = clParam.mScaleKeyFrame;
            this.checkBox_TweenScaleX.Enabled = clParam.mEnableScaleXTween;
            this.checkBox_TweenScaleY.Enabled = clParam.mEnableScaleYTween;
            this.checkBox_TweenScaleX.Checked = clParam.mScaleXTween;
            this.checkBox_TweenScaleY.Checked = clParam.mScaleYTween;
            this.button_TweenSX.Image = (clParam.mTweenScaleX == null) ? null : clParam.mTweenScaleX.mImage;
            this.button_TweenSY.Image = (clParam.mTweenScaleY == null) ? null : clParam.mTweenScaleY.mImage;
            this.button_TweenSX.Tag = clParam.mTweenScaleX;
            this.button_TweenSY.Tag = clParam.mTweenScaleY;
            this.UDnumSX.Value = (decimal)clParam.mSX;
            this.UDnumSY.Value = (decimal)clParam.mSY;

            this.checkBox_OffsetKeyFrame.Checked = clParam.mOffsetKeyFrame;
            this.checkBox_OffsetParent.Checked = clParam.mOffsetParent;
            this.checkBox_TweenOffsetX.Enabled = clParam.mEnableOffsetXTween;
            this.checkBox_TweenOffsetY.Enabled = clParam.mEnableOffsetYTween;
            this.checkBox_TweenOffsetX.Checked = clParam.mOffsetXTween;
            this.checkBox_TweenOffsetY.Checked = clParam.mOffsetYTween;
            this.button_TweenCX.Image = (clParam.mTweenOffsetX == null) ? null : clParam.mTweenOffsetX.mImage;
            this.button_TweenCY.Image = (clParam.mTweenOffsetY == null) ? null : clParam.mTweenOffsetY.mImage;
            this.button_TweenCX.Tag = clParam.mTweenOffsetX;
            this.button_TweenCY.Tag = clParam.mTweenOffsetY;
            this.UDnumXoff.Value = (int)clParam.mCX;
            this.UDnumYoff.Value = (int)clParam.mCY;

            this.checkBox_FlipKeyFrame.Checked = clParam.mFlipKeyFrame;
            this.checkBox_FlipParent.Checked = clParam.mFlipParent;
            this.checkBox_FlipH.Checked = clParam.mFlipH;
            this.checkBox_FlipV.Checked = clParam.mFlipV;

            this.checkBox_TransKeyFrame.Checked = clParam.mTransKeyFrame;
            this.checkBox_TransParent.Checked = clParam.mTransParent;
            this.checkBox_TweenTrans.Enabled = clParam.mEnableTransTween;
            this.checkBox_TweenTrans.Checked = clParam.mTransTween;
            this.button_TweenT.Image = (clParam.mTweenTrans == null) ? null : clParam.mTweenTrans.mImage;
            this.button_TweenT.Tag = clParam.mTweenTrans;
            this.UDnumT.Value = (decimal)clParam.mTrans;

            this.checkBox_ColorKeyFrame.Checked = clParam.mColorKeyFrame;
            this.checkBox_ColorParent.Checked = clParam.mColorParent;
            this.checkBox_TweenColor.Enabled = clParam.mEnableColorTween;
            this.checkBox_TweenColor.Checked = clParam.mColorTween;
            this.button_TweenC.Image = (clParam.mTweenColor == null) ? null : clParam.mTweenColor.mImage;
            this.button_TweenC.Tag = clParam.mTweenColor;
            this.textBox_C.Text = $"{clParam.mColor:X6}";

            this.checkBox_UserDataKeyFrame.Checked = clParam.mUserDataKeyFrame;
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

            clParam.mDisplayKeyFrame = this.checkBox_DisplayKeyFrame.Checked;
            clParam.mDisplayParent = this.checkBox_DisplayParent.Checked;
            clParam.mDisplay = this.checkBox_Display.Checked;

            clParam.mPositionKeyFrame = this.checkBox_PositionKeyFrame.Checked;
            clParam.mPositionXTween = this.checkBox_TweenPositionX.Checked;
            clParam.mTweenPositionX = this.button_TweenX.Tag as ClsDatTween;
            clParam.mX = (int)this.UDnumX.Value;

            clParam.mPositionYTween = this.checkBox_TweenPositionY.Checked;
            clParam.mTweenPositionY = this.button_TweenY.Tag as ClsDatTween;
            clParam.mY = (int)this.UDnumY.Value;

            clParam.mRotationKeyFrame = this.checkBox_RotationKeyFrame.Checked;
            clParam.mRotationTween = this.checkBox_TweenRotation.Checked;
            clParam.mTweenRotation = this.button_TweenRZ.Tag as ClsDatTween;
            clParam.mRZ = (float)this.UDnumRot.Value;

            clParam.mScaleKeyFrame = this.checkBox_ScaleKeyFrame.Checked;
            clParam.mScaleXTween = this.checkBox_TweenScaleX.Checked;
            clParam.mTweenScaleX = this.button_TweenSX.Tag as ClsDatTween;
            clParam.mSX = (float)this.UDnumSX.Value;

            clParam.mScaleYTween = this.checkBox_TweenScaleY.Checked;
            clParam.mTweenScaleY = this.button_TweenSY.Tag as ClsDatTween;
            clParam.mSY = (float)this.UDnumSY.Value;

            clParam.mOffsetKeyFrame = this.checkBox_OffsetKeyFrame.Checked;
            clParam.mOffsetParent = this.checkBox_OffsetParent.Checked;
            clParam.mOffsetXTween = this.checkBox_TweenOffsetX.Checked;
            clParam.mTweenOffsetX = this.button_TweenCX.Tag as ClsDatTween;
            clParam.mCX = (int)this.UDnumXoff.Value;

            clParam.mOffsetYTween = this.checkBox_TweenOffsetY.Checked;
            clParam.mTweenOffsetY = this.button_TweenCY.Tag as ClsDatTween;
            clParam.mCY = (int)this.UDnumYoff.Value;

            clParam.mFlipKeyFrame = this.checkBox_FlipKeyFrame.Checked;
            clParam.mFlipParent = this.checkBox_FlipParent.Checked;
            clParam.mFlipH = this.checkBox_FlipH.Checked;
            clParam.mFlipV = this.checkBox_FlipV.Checked;

            clParam.mTransKeyFrame = this.checkBox_TransKeyFrame.Checked;
            clParam.mTransParent = this.checkBox_TransParent.Checked;
            clParam.mTransTween = this.checkBox_TweenTrans.Checked;
            clParam.mTweenTrans = this.button_TweenT.Tag as ClsDatTween;
            clParam.mTrans = (int)this.UDnumT.Value;

            clParam.mColorKeyFrame = this.checkBox_ColorKeyFrame.Checked;
            clParam.mColorParent = this.checkBox_ColorParent.Checked;
            clParam.mColorTween = this.checkBox_TweenColor.Checked;
            clParam.mTweenColor = this.button_TweenC.Tag as ClsDatTween;
            clParam.mColor = this.button_C.BackColor.ToArgb() & 0x00FFFFFF;

            clParam.mUserDataKeyFrame = this.checkBox_UserDataKeyFrame.Checked;
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
        /// <param name="isExistKeyFrame">キーフレーム存在フラグ</param>
        /// <param name="isParentFlag">親の設定に依存するかどうか</param>
        /// <param name="clValue1">値１</param>
        /// <param name="clValue2">値２</param>
        /// <param name="clTween1">トゥイーン１</param>
        /// <param name="clTween2">トゥイーン２</param>
        private void ChangeElem(ClsDatElem clElem, EnmTypeOption enTypeOption, int inSelectFrameNo, bool isExistKeyFrame, bool isParentFlag, object clValue1, object clValue2, ClsDatTween clTween1, ClsDatTween clTween2)
        {
            ClsDatOption clOption = null;

            if (inSelectFrameNo == 0)
            {
                clElem.SetOption(enTypeOption, isParentFlag, clValue1, clValue2, clTween1, clTween2);
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
                    clElem.SetOption(enTypeOption, isParentFlag, clValue1, clValue2, clTween1, clTween2);
                    clOption = clElem.GetOption(enTypeOption);
                }

                if (isExistKeyFrame)
                {
                    clOption.SetKeyFrame(inSelectFrameNo, isParentFlag, clValue1, clValue2, clTween1, clTween2);  //追加または更新
                }
                else
                {
                    clOption.RemoveKeyFrame(inSelectFrameNo);
                }
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
            object clValue2 = ClsParam.GetDefaultValue2(EnmTypeOption.DISPLAY);
            this.ChangeElem(clElem, EnmTypeOption.DISPLAY, inSelectFrameNo, clParam.mDisplayKeyFrame, clParam.mDisplayParent, clParam.mDisplay, clValue2, null, null);

            //以下、座標設定
            bool isParentFlag = ClsParam.GetDefaultParentFlag(clElem.mElem, EnmTypeOption.POSITION);
            this.ChangeElem(clElem, EnmTypeOption.POSITION, inSelectFrameNo, clParam.mPositionKeyFrame, isParentFlag, clParam.mX, clParam.mY, clParam.mTweenPositionX, clParam.mTweenPositionY);

            //以下、回転設定
            isParentFlag = ClsParam.GetDefaultParentFlag(clElem.mElem, EnmTypeOption.ROTATION);
            clValue2 = ClsParam.GetDefaultValue2(EnmTypeOption.ROTATION);
            this.ChangeElem(clElem, EnmTypeOption.ROTATION, inSelectFrameNo, clParam.mRotationKeyFrame, isParentFlag, clParam.mRZ, clValue2, clParam.mTweenRotation, null);

            //以下、スケール設定
            isParentFlag = ClsParam.GetDefaultParentFlag(clElem.mElem, EnmTypeOption.SCALE);
            this.ChangeElem(clElem, EnmTypeOption.SCALE, inSelectFrameNo, clParam.mScaleKeyFrame, isParentFlag, clParam.mSX, clParam.mSY, clParam.mTweenScaleX, clParam.mTweenScaleY);

            //以下、オフセット設定
            this.ChangeElem(clElem, EnmTypeOption.OFFSET, inSelectFrameNo, clParam.mOffsetKeyFrame, clParam.mOffsetParent, clParam.mCX, clParam.mCY, clParam.mTweenOffsetX, clParam.mTweenOffsetY);

            //以下、反転設定
            this.ChangeElem(clElem, EnmTypeOption.FLIP, inSelectFrameNo, clParam.mFlipKeyFrame, clParam.mFlipParent, clParam.mFlipH, clParam.mFlipV, null, null);

            //以下、透明設定
            clValue2 = ClsParam.GetDefaultValue2(EnmTypeOption.TRANSPARENCY);
            this.ChangeElem(clElem, EnmTypeOption.TRANSPARENCY, inSelectFrameNo, clParam.mTransKeyFrame, clParam.mTransParent, clParam.mTrans, clValue2, clParam.mTweenTrans, null);

            //以下、カラー設定 
            clValue2 = ClsParam.GetDefaultValue2(EnmTypeOption.COLOR);
            this.ChangeElem(clElem, EnmTypeOption.COLOR, inSelectFrameNo, clParam.mColorKeyFrame, clParam.mColorParent, clParam.mColor, clValue2, clParam.mTweenColor, null);

            //以下、ユーザーデータ設定 
            isParentFlag = ClsParam.GetDefaultParentFlag(clElem.mElem, EnmTypeOption.USER_DATA);
            clValue2 = ClsParam.GetDefaultValue2(EnmTypeOption.USER_DATA);
            this.ChangeElem(clElem, EnmTypeOption.USER_DATA, inSelectFrameNo, clParam.mUserDataKeyFrame, isParentFlag, clParam.mUserData, clValue2, null, null);

            //以下、行番号を振り直す処理
            clMotion.RefreshLineNo();

            //以下、メインウィンドウ更新処理
            this.mFormMain.RefreshAll();
        }

        /// <summary>
        /// 各コントロールが変更になったら呼ばれる関数
        /// </summary>
        /// <param name="sender">コントロール</param>
        /// <param name="e">イベント引数</param>
        private void Param_ValueChanged(object sender, EventArgs e)
        {
            if (this.mSelectFrameNo == 0)
            {
                this.checkBox_DisplayKeyFrame.Checked = true;
                this.checkBox_PositionKeyFrame.Checked = true;
                this.checkBox_RotationKeyFrame.Checked = true;
                this.checkBox_ScaleKeyFrame.Checked = true;
                this.checkBox_OffsetKeyFrame.Checked = true;
                this.checkBox_FlipKeyFrame.Checked = true;
                this.checkBox_TransKeyFrame.Checked = true;
                this.checkBox_ColorKeyFrame.Checked = true;
                this.checkBox_UserDataKeyFrame.Checked = true;
            }

            if (!this.mLocked)
            {
                ClsParam clParam = this.GetParam();
                this.ChangeElemFromParam(clParam);
            }

            //以下、表示設定
            bool isCheckOption;
            bool isCheckKeyFrame = this.checkBox_DisplayKeyFrame.Checked;
            this.checkBox_DisplayParent.Enabled = isCheckKeyFrame;
            this.label_Display.Enabled = isCheckKeyFrame;
            this.checkBox_Display.Enabled = isCheckKeyFrame;

            //以下、座標設定
            isCheckKeyFrame = this.checkBox_PositionKeyFrame.Checked;
            this.label_X.Enabled = isCheckKeyFrame;
            this.label_Y.Enabled = isCheckKeyFrame;
            this.UDnumX.Enabled = isCheckKeyFrame;
            this.UDnumY.Enabled = isCheckKeyFrame;
            this.checkBox_TweenPositionX.Enabled = isCheckKeyFrame;
            this.checkBox_TweenPositionY.Enabled = isCheckKeyFrame;
            this.button_TweenX.Enabled = this.checkBox_TweenPositionX.Checked;
            this.button_TweenY.Enabled = this.checkBox_TweenPositionY.Checked;

            Console.WriteLine("Param_ValueChanged checkBox_TweenPositionX.Enable = " + this.checkBox_TweenPositionX.Enabled);

            //以下、回転設定
            isCheckKeyFrame = this.checkBox_RotationKeyFrame.Checked;
            this.checkBox_RotationKeyFrame.Enabled = (this.mSelectFrameNo!= 0);
            this.label_RZ.Enabled = (isCheckKeyFrame);
            this.UDnumRot.Enabled = (isCheckKeyFrame);
            this.checkBox_TweenRotation.Enabled = (isCheckKeyFrame);
            this.button_TweenRZ.Enabled = this.checkBox_TweenRotation.Checked;

            //以下、スケール設定
            isCheckKeyFrame = this.checkBox_ScaleKeyFrame.Checked;
            this.checkBox_ScaleKeyFrame.Enabled = (this.mSelectFrameNo != 0);
            this.label_SX.Enabled = (isCheckKeyFrame);
            this.label_SY.Enabled = (isCheckKeyFrame);
            this.UDnumSX.Enabled = (isCheckKeyFrame);
            this.UDnumSY.Enabled = (isCheckKeyFrame);
            this.checkBox_TweenScaleX.Enabled = (isCheckKeyFrame);
            this.checkBox_TweenScaleY.Enabled = (isCheckKeyFrame);
            this.button_TweenSX.Enabled = this.checkBox_TweenScaleX.Checked;
            this.button_TweenSY.Enabled = this.checkBox_TweenScaleY.Checked;

            //以下、オフセット設定
            isCheckKeyFrame = this.checkBox_OffsetKeyFrame.Checked;
            this.checkBox_OffsetKeyFrame.Enabled = (this.mSelectFrameNo != 0);
            this.checkBox_OffsetParent.Enabled = (isCheckKeyFrame);
            this.label_CX.Enabled = (isCheckKeyFrame);
            this.label_CY.Enabled = (isCheckKeyFrame);
            this.UDnumXoff.Enabled = (isCheckKeyFrame);
            this.UDnumYoff.Enabled = (isCheckKeyFrame);
            this.checkBox_TweenOffsetX.Enabled = (isCheckKeyFrame);
            this.checkBox_TweenOffsetY.Enabled = (isCheckKeyFrame);
            this.button_TweenCX.Enabled = this.checkBox_TweenOffsetX.Checked;
            this.button_TweenCY.Enabled = this.checkBox_TweenOffsetY.Checked;

            //以下、反転設定
            isCheckKeyFrame = this.checkBox_FlipKeyFrame.Checked;
            this.checkBox_FlipKeyFrame.Enabled = (this.mSelectFrameNo != 0);
            this.checkBox_FlipParent.Enabled = (isCheckKeyFrame);
            this.label_FlipH.Enabled = (isCheckKeyFrame);
            this.label_FlipV.Enabled = (isCheckKeyFrame);
            this.checkBox_FlipH.Enabled = (isCheckKeyFrame);
            this.checkBox_FlipV.Enabled = (isCheckKeyFrame);

            //以下、透明値設定
            isCheckKeyFrame = this.checkBox_TransKeyFrame.Checked;
            this.checkBox_TransKeyFrame.Enabled = (this.mSelectFrameNo != 0);
            this.checkBox_TransParent.Enabled = (isCheckKeyFrame);
            this.label_T.Enabled = (isCheckKeyFrame);
            this.UDnumT.Enabled = (isCheckKeyFrame);
            this.checkBox_TweenTrans.Enabled = (isCheckKeyFrame);
            this.button_TweenT.Enabled = this.checkBox_TweenTrans.Checked;

            //以下、色設定
            isCheckKeyFrame = this.checkBox_ColorKeyFrame.Checked;
            this.checkBox_ColorKeyFrame.Enabled = (this.mSelectFrameNo != 0);
            this.checkBox_ColorParent.Enabled = (isCheckKeyFrame);
            this.label_C.Enabled = (isCheckKeyFrame);
            this.button_C.Enabled = (isCheckKeyFrame);
            this.textBox_C.Enabled = (isCheckKeyFrame);
            this.checkBox_TweenColor.Enabled = (isCheckKeyFrame);
            this.button_TweenC.Enabled = this.checkBox_TweenColor.Checked;

            //以下、ユーザーデータ設定
            isCheckKeyFrame = this.checkBox_UserDataKeyFrame.Checked;
            this.checkBox_UserDataKeyFrame.Enabled = (this.mSelectFrameNo != 0);
            this.label_UT.Enabled = (isCheckKeyFrame);
            this.textBox_UT.Enabled = (isCheckKeyFrame);
        }

        private void FormAttribute_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Closeキャンセルして非表示にするだけ
            e.Cancel = true;

            //this.Visible = false; //自身で消さなくても下の操作で消える
            this.mFormMain.checkBox_Attribute.Checked = false;
        }

        private void button_Tween_Click(object sender, EventArgs e)
        {
            Button clButton = sender as Button;

            EnmParam enParam = EnmParam.NONE;
            if ("button_TweenX".Equals(clButton.Name)) enParam = EnmParam.POSITION_X;
            else if ("button_TweenY".Equals(clButton.Name)) enParam = EnmParam.POSITION_Y;
            else if ("button_TweenRZ".Equals(clButton.Name)) enParam = EnmParam.ROTATION;
            else if ("button_TweenSX".Equals(clButton.Name)) enParam = EnmParam.SCALE_X;
            else if ("button_TweenSY".Equals(clButton.Name)) enParam = EnmParam.SCALE_Y;
            else if ("button_TweenCX".Equals(clButton.Name)) enParam = EnmParam.OFFSET_X;
            else if ("button_TweenCY".Equals(clButton.Name)) enParam = EnmParam.OFFSET_Y;
            else if ("button_TweenT".Equals(clButton.Name)) enParam = EnmParam.TRANS;
            else if ("button_TweenC".Equals(clButton.Name)) enParam = EnmParam.COLOR;
            if (enParam == EnmParam.NONE) return;

            //以下、トゥイーンウィンドウ設定処理
            int inFrameNo = ClsSystem.GetSelectFrameNo();
            FormTween clForm = null;
            ClsDatTween clTween = clButton.Tag as ClsDatTween;
            if (clTween == null)
            {
                clForm = new FormTween(this.mFormMain, enParam, 10, 20, inFrameNo);
            }
            else
            {
                clForm = new FormTween(this.mFormMain, enParam, 10, 20, inFrameNo, clTween.mPos, clTween.mListVec);
            }
            clForm.ShowDialog();
            if (clForm.DialogResult != DialogResult.OK) return;

            //以下、トゥイーン設定
            clTween = clForm.GetTween();
            clButton.Image = clTween.mImage;
            clButton.Tag = clTween;

            this.Param_ValueChanged(sender, e);
        }
    }
}
