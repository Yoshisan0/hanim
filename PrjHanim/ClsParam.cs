﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsParam
    {
        public string mName;                    //名前

        public bool mDisplayKeyFrame;           //表示キーフレーム存在フラグ
        public bool mDisplayParent;             //親の設定依存フラグ
        public bool mDisplay;                   //表示フラグ

        public bool mPositionKeyFrame;          //座標キーフレーム存在フラグ
        public bool mEnablePositionXTween;      //座標Ｘトゥイーン有効フラグ
        public bool mEnablePositionYTween;      //座標Ｙトゥイーン有効フラグ
        public bool mPositionXTween;            //座標Ｘトゥイーン存在フラグ
        public bool mPositionYTween;            //座標Ｙトゥイーン存在フラグ
        public ClsDatTween mTweenPositionX;     //座標Ｘトゥイーン
        public ClsDatTween mTweenPositionY;     //座標Ｙトゥイーン
        public float mX;                        //Ｘ座標（常に有効）
        public float mY;                        //Ｙ座標（常に有効）

        public bool mRotationOption;            //回転オプション存在フラグ
        public bool mRotationKeyFrame;          //回転キーフレーム存在フラグ
        public bool mEnableRotationTween;       //回転トゥイーン有効フラグ
        public bool mRotationTween;             //回転トゥイーン存在フラグ
        public ClsDatTween mTweenRotation;      //回転トゥイーン
        public float mRZ;                       //回転値

        public bool mScaleOption;               //スケールオプション存在フラグ
        public bool mScaleKeyFrame;             //スケールキーフレーム存在フラグ
        public bool mEnableScaleXTween;         //スケールＸトゥイーン有効フラグ
        public bool mEnableScaleYTween;         //スケールＹトゥイーン有効フラグ
        public bool mScaleXTween;               //スケールＸトゥイーン存在フラグ
        public bool mScaleYTween;               //スケールＹトゥイーン存在フラグ
        public ClsDatTween mTweenScaleX;        //スケールＸトゥイーン
        public ClsDatTween mTweenScaleY;        //スケールＹトゥイーン
        public float mSX;                       //スケールＸ
        public float mSY;                       //スケールＹ

        public bool mOffsetOption;              //オフセットオプション存在フラグ
        public bool mOffsetKeyFrame;            //オフセットキーフレーム存在フラグ
        public bool mOffsetParent;              //親の設定依存フラグ
        public bool mEnableOffsetXTween;        //オフセットＸトゥイーン有効フラグ
        public bool mEnableOffsetYTween;        //オフセットＹトゥイーン有効フラグ
        public bool mOffsetXTween;              //オフセットＸトゥイーン存在フラグ
        public bool mOffsetYTween;              //オフセットＹトゥイーン存在フラグ
        public ClsDatTween mTweenOffsetX;       //オフセットＸトゥイーン
        public ClsDatTween mTweenOffsetY;       //オフセットＹトゥイーン
        public float mCX;                       //オフセットＸ座標
        public float mCY;                       //オフセットＹ座標

        public bool mFlipOption;                //反転オプション存在フラグ
        public bool mFlipKeyFrame;              //反転キーフレーム存在フラグ
        public bool mFlipParent;                //親の設定依存フラグ
        public bool mFlipH;                     //水平反転フラグ
        public bool mFlipV;                     //垂直反転フラグ

        public bool mTransOption;               //透明オプション存在フラグ
        public bool mTransKeyFrame;             //透明キーフレーム存在フラグ
        public bool mTransParent;               //親の設定依存フラグ
        public bool mEnableTransTween;          //透明トゥイーン有効フラグ
        public bool mTransTween;                //透明トゥイーン存在フラグ
        public ClsDatTween mTweenTrans;         //透明トゥイーン
        public int mTrans;                      //透明透明値0～255

        public bool mColorOption;               //マテリアルカラーオプション存在フラグ
        public bool mColorKeyFrame;             //マテリアルカラーキーフレーム存在フラグ
        public bool mColorParent;               //親の設定依存フラグ
        public bool mEnableColorTween;          //マテリアルカラートゥイーン有効フラグ
        public bool mColorTween;                //マテリアルカラートゥイーン存在フラグ
        public ClsDatTween mTweenColor;         //マテリアルカラートゥイーン
        public int mColor;                      //マテリアルカラー値（α無し RGBのみ）

        public bool mUserDataOption;            //ユーザーデータオプション存在フラグ
        public bool mEnableUserDataKeyFrame;    //ユーザーデータキーフレーム有効フラグ
        public bool mUserDataKeyFrame;          //ユーザーデータキーフレーム存在フラグ
        public string mUserData;                //ユーザーデータ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClsParam()
        {
            this.mDisplayKeyFrame = false;
            this.mDisplay = (bool)ClsParam.GetDefaultValue(EnmTypeParam.DISPLAY);

            this.mPositionKeyFrame = false;
            this.mX = (float)ClsParam.GetDefaultValue(EnmTypeParam.POSITION_X);
            this.mY = (float)ClsParam.GetDefaultValue(EnmTypeParam.POSITION_Y);

            this.mRotationOption = false;
            this.mRotationKeyFrame = false;
            this.mRZ = (float)ClsParam.GetDefaultValue(EnmTypeParam.ROTATION_Z);

            this.mScaleOption = false;
            this.mScaleKeyFrame = false;
            this.mSX = (float)ClsParam.GetDefaultValue(EnmTypeParam.SCALE_X);
            this.mSY = (float)ClsParam.GetDefaultValue(EnmTypeParam.SCALE_Y);

            this.mOffsetOption = false;
            this.mOffsetKeyFrame = false;
            this.mCX = (float)ClsParam.GetDefaultValue(EnmTypeParam.OFFSET_X);
            this.mCY = (float)ClsParam.GetDefaultValue(EnmTypeParam.OFFSET_Y);

            this.mFlipOption = false;
            this.mFlipKeyFrame = false;
            this.mFlipH = (bool)ClsParam.GetDefaultValue(EnmTypeParam.FLIP_HORIZONAL);
            this.mFlipV = (bool)ClsParam.GetDefaultValue(EnmTypeParam.FLIP_VERTICAL);

            this.mTransOption = false;
            this.mTransKeyFrame = false;
            this.mTrans = (int)ClsParam.GetDefaultValue(EnmTypeParam.TRANSPARENCY);

            this.mColorOption = false;
            this.mColorKeyFrame = false;
            this.mColor = (int)ClsParam.GetDefaultValue(EnmTypeParam.COLOR);

            this.mUserDataOption = false;
            this.mUserDataKeyFrame = false;
            this.mUserData = (string)ClsParam.GetDefaultValue(EnmTypeParam.USER_DATA);
        }

        /// <summary>
        /// デフォルトの親に影響を受けるかどうか
        /// </summary>
        /// <param name="clElem">親エレメント</param>
        /// <param name="enTypeOption">オプションタイプ</param>
        /// <returns>デフォルトの値</returns>
        public static bool GetDefaultParentFlag(ClsDatElem clElem, EnmTypeOption enTypeOption)
        {
            if (clElem == null) return (false);

            bool isParent = false;

            switch (enTypeOption)
            {
            case EnmTypeOption.NONE:
                isParent = false;
                break;
            case EnmTypeOption.DISPLAY:
                isParent = true;
                break;
            case EnmTypeOption.POSITION:
                isParent = true;
                break;
            case EnmTypeOption.ROTATION:
                isParent = true;
                break;
            case EnmTypeOption.SCALE:
                isParent = true;
                break;
            case EnmTypeOption.OFFSET:
                isParent = false;
                break;
            case EnmTypeOption.FLIP:
                isParent = true;
                break;
            case EnmTypeOption.TRANSPARENCY:
                isParent = true;
                break;
            case EnmTypeOption.COLOR:
                isParent = false;
                break;
            case EnmTypeOption.USER_DATA:
                isParent = false;
                break;
            }

            return (isParent);
        }

        /// <summary>
        /// デフォルトの値取得処理
        /// </summary>
        /// <param name="enTypeParam">パラメータータイプ</param>
        /// <returns>デフォルトの値</returns>
        private static object GetDefaultValue(EnmTypeParam enTypeParam)
        {
            object clValue = null;
            switch (enTypeParam)
            {
            case EnmTypeParam.NONE:
                clValue = null;
                break;
            case EnmTypeParam.DISPLAY:
                clValue = true;
                break;
            case EnmTypeParam.POSITION_X:
                clValue = 0.0f;
                break;
            case EnmTypeParam.POSITION_Y:
                clValue = 0.0f;
                break;
            case EnmTypeParam.ROTATION_Z:
                clValue = 0.0f;
                break;
            case EnmTypeParam.SCALE_X:
                clValue = 1.0f;
                break;
            case EnmTypeParam.SCALE_Y:
                clValue = 1.0f;
                break;
            case EnmTypeParam.OFFSET_X:
                clValue = 0.0f;
                break;
            case EnmTypeParam.OFFSET_Y:
                clValue = 0.0f;
                break;
            case EnmTypeParam.FLIP_HORIZONAL:
                clValue = false;
                break;
            case EnmTypeParam.FLIP_VERTICAL:
                clValue = false;
                break;
            case EnmTypeParam.TRANSPARENCY:
                clValue = 255;
                break;
            case EnmTypeParam.COLOR:
                clValue = (int)0xFFFFFF;
                break;
            case EnmTypeParam.USER_DATA:
                clValue = "";
                break;
            }

            return (clValue);
        }

        /// <summary>
        /// デフォルトの値１取得処理
        /// </summary>
        /// <param name="enTypeOption">オプションタイプ</param>
        /// <returns>デフォルトの値１</returns>
        public static object GetDefaultValue1(EnmTypeOption enTypeOption)
        {
            object clValue = null;
            switch (enTypeOption)
            {
            case EnmTypeOption.NONE:
                clValue = ClsParam.GetDefaultValue(EnmTypeParam.NONE);
                break;
            case EnmTypeOption.DISPLAY:
                clValue = ClsParam.GetDefaultValue(EnmTypeParam.DISPLAY);
                break;
            case EnmTypeOption.POSITION:
                clValue = ClsParam.GetDefaultValue(EnmTypeParam.POSITION_X);
                break;
            case EnmTypeOption.ROTATION:
                clValue = ClsParam.GetDefaultValue(EnmTypeParam.ROTATION_Z);
                break;
            case EnmTypeOption.SCALE:
                clValue = ClsParam.GetDefaultValue(EnmTypeParam.SCALE_X);
                break;
            case EnmTypeOption.OFFSET:
                clValue = ClsParam.GetDefaultValue(EnmTypeParam.OFFSET_X);
                break;
            case EnmTypeOption.FLIP:
                clValue = ClsParam.GetDefaultValue(EnmTypeParam.FLIP_HORIZONAL);
                break;
            case EnmTypeOption.TRANSPARENCY:
                clValue = ClsParam.GetDefaultValue(EnmTypeParam.TRANSPARENCY);
                break;
            case EnmTypeOption.COLOR:
                clValue = ClsParam.GetDefaultValue(EnmTypeParam.COLOR);
                break;
            case EnmTypeOption.USER_DATA:
                clValue = "";
                break;
            }

            return (clValue);
        }

        /// <summary>
        /// デフォルトの値２取得処理
        /// </summary>
        /// <param name="enTypeOption">オプションタイプ</param>
        /// <returns>デフォルトの値２</returns>
        public static object GetDefaultValue2(EnmTypeOption enTypeOption)
        {
            object clValue = null;
            switch (enTypeOption)
            {
            case EnmTypeOption.NONE:
                clValue = null;
                break;
            case EnmTypeOption.DISPLAY:
                clValue = null;
                break;
            case EnmTypeOption.POSITION:
                clValue = ClsParam.GetDefaultValue(EnmTypeParam.POSITION_Y);
                break;
            case EnmTypeOption.ROTATION:
                clValue = null;
                break;
            case EnmTypeOption.SCALE:
                clValue = ClsParam.GetDefaultValue(EnmTypeParam.SCALE_Y);
                break;
            case EnmTypeOption.OFFSET:
                clValue = ClsParam.GetDefaultValue(EnmTypeParam.OFFSET_Y);
                break;
            case EnmTypeOption.FLIP:
                clValue = ClsParam.GetDefaultValue(EnmTypeParam.FLIP_VERTICAL);
                break;
            case EnmTypeOption.TRANSPARENCY:
                clValue = null;
                break;
            case EnmTypeOption.COLOR:
                clValue = null;
                break;
            case EnmTypeOption.USER_DATA:
                clValue = null;
                break;
            }

            return (clValue);
        }
    }
}