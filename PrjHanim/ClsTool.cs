using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace PrjHikariwoAnim
{
    public class ClsTool
    {
        public static string GetAppFileName()
        {
            Assembly clAssembly = Assembly.GetExecutingAssembly();
            string clFileName = Path.GetFileNameWithoutExtension(clAssembly.Location);
            return (clFileName);
        }

        public static string GetMD5FromFile(string clPath)
        {
            byte[] pchBuffer = File.ReadAllBytes(clPath);
            string clHash = ClsTool.GetMD5FromMemory(pchBuffer);
            string stHash = clPath.GetHashCode().ToString();
            return (clHash);
        }

        public static string GetMD5FromMemory(byte[] pchBuffer)
        {
            MD5CryptoServiceProvider clMD5 = new MD5CryptoServiceProvider();
            byte[] pchHash = clMD5.ComputeHash(pchBuffer);
            clMD5.Clear();

            StringBuilder clResult = new StringBuilder();
            foreach (byte b in pchHash)
            {
                clResult.Append(b.ToString("x2"));
            }

            string clHash = clResult.ToString();
            return (clHash);
        }
        public static string GetMD5FromImage(Image clImage)
        {
            byte[] work = ImageToByteArray(clImage);
            return GetMD5FromMemory(work);
        }

        public static byte[] ImageToByteArray(Image clImage)
        {
            ImageConverter clImgConv = new ImageConverter();
            byte[] pchBuffer = (byte[])clImgConv.ConvertTo(clImage, typeof(byte[]));
            return (pchBuffer);
        }

        public static string ImageToBase64(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                byte[] imgByte = ms.ToArray();
                return Convert.ToBase64String(imgByte);
            }
        }
        public static Bitmap ImageFromBase64(string strBase64)
        {
            byte[] imgByte = Convert.FromBase64String(strBase64);
            MemoryStream ms = new MemoryStream(imgByte, 0, imgByte.Length);
            Bitmap img = (Bitmap)Image.FromStream(ms);
            return img;
        }

        /// <summary>
        /// ウィンドウ名取得処理
        /// </summary>
        /// <param name="clWindowName">ウィンドウ名</param>
        /// <param name="clMotion">モーション管理クラス</param>
        /// <returns>ウィンドウ名</returns>
        public static string GetWindowName(string clWindowName, ClsDatMotion clMotion)
        {
            string clName = "";
            if (clMotion != null)
            {
                clName = " ( " + clMotion.mName + " )";
            }

            string clResultName = clWindowName + clName;
            return (clResultName);
        }

        /// <summary>
        /// ウィンドウ名取得処理
        /// </summary>
        /// <param name="clWindowName">ウィンドウ名</param>
        /// <param name="clElem">エレメント管理クラス</param>
        /// <param name="inSelectFrameNo">選択中のフレーム番号</param>
        /// <returns>ウィンドウ名</returns>
        public static string GetWindowName(string clWindowName, ClsDatElem clElem, int inSelectFrameNo)
        {
            string clName = "";
            if (clElem != null && clElem.mMotion != null)
            {
                clName = " ( " + clElem.mMotion.mName + " [ " + clElem.mName + " , " + inSelectFrameNo + " ] )";
            }

            string clResultName = clWindowName + clName;
            return (clResultName);
        }

        /// <summary>
        /// xml形式で開始要素を ClsSystem.mFileBuffer に出力する処理
        /// </summary>
        /// <param name="clHeader">ヘッダー</param>
        /// <param name="clName">要素名</param>
        public static void AppendElementStart(string clHeader, string clName)
        {
            string clLine = string.Format("{0}<{1}>", clHeader, clName);
            ClsSystem.mFileBuffer.Append(clLine + Environment.NewLine);
        }

        /// <summary>
        /// xml形式で終了要素を ClsSystem.mFileBuffer に出力する処理
        /// </summary>
        /// <param name="clHeader">ヘッダー</param>
        /// <param name="clName">要素名</param>
        public static void AppendElementEnd(string clHeader, string clName)
        {
            string clLine = string.Format("{0}</{1}>", clHeader, clName);
            ClsSystem.mFileBuffer.Append(clLine + Environment.NewLine);
        }

        /// <summary>
        /// xml形式で要素を ClsSystem.mFileBuffer に出力する処理
        /// </summary>
        /// <param name="clHeader">ヘッダー</param>
        /// <param name="clName">要素名</param>
        /// <param name="clValue">値</param>
        public static void AppendElement(string clHeader, string clName, string clValue)
        {
            string clValueEscape = SecurityElement.Escape(clValue);
            string clLine = string.Format("{0}<{1}>{2}</{1}>", clHeader, clName, clValueEscape);
            ClsSystem.mFileBuffer.Append(clLine + Environment.NewLine);
        }

        /// <summary>
        /// xml形式で要素を ClsSystem.mFileBuffer に出力する処理
        /// </summary>
        /// <param name="clHeader">ヘッダー</param>
        /// <param name="clName">要素名</param>
        /// <param name="flValue">値</param>
        public static void AppendElement(string clHeader, string clName, float flValue)
        {
            string clLine = string.Format("{0}<{1}>{2}</{1}>", clHeader, clName, flValue);
            ClsSystem.mFileBuffer.Append(clLine + Environment.NewLine);
        }

        /// <summary>
        /// xml形式で要素を ClsSystem.mFileBuffer に出力する処理
        /// </summary>
        /// <param name="clHeader">ヘッダー</param>
        /// <param name="clName">要素名</param>
        /// <param name="isValue">値</param>
        public static void AppendElement(string clHeader, string clName, bool isValue)
        {
            string clLine = string.Format("{0}<{1}>{2}</{1}>", clHeader, clName, isValue);
            ClsSystem.mFileBuffer.Append(clLine + Environment.NewLine);
        }

        /// <summary>
        /// xml形式で要素を ClsSystem.mFileBuffer に出力する処理
        /// </summary>
        /// <param name="clHeader">ヘッダー</param>
        /// <param name="clName">要素名</param>
        /// <param name="inValue">値</param>
        public static void AppendElement(string clHeader, string clName, int inValue)
        {
            string clLine = string.Format("{0}<{1}>{2}</{1}>", clHeader, clName, inValue);
            ClsSystem.mFileBuffer.Append(clLine + Environment.NewLine);
        }

        /// <summary>
        /// xml形式でベクター３を ClsSystem.mFileBuffer に出力する処理
        /// </summary>
        /// <param name="clHeader">ヘッダー</param>
        /// <param name="clName">要素名</param>
        /// <param name="clVec">ベクトル</param>
        public static void AppendElement(string clHeader, string clName, ClsVector3 clVec)
        {
            ClsTool.AppendElementStart(clHeader, clName);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "X", clVec.X);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Y", clVec.Y);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Z", clVec.Z);
            ClsTool.AppendElementEnd(clHeader, clName);
        }

        /// <summary>
        /// XmlNodeからVector3を生成して返す
        /// </summary>
        /// <param name="clXmlNode">xmlノード</param>
        /// <returns>Vector3</returns>
        public static ClsVector3 GetVecFromXmlNode(XmlNode clXmlNode)
        {
            ClsVector3 clVec = new ClsVector3();

            XmlNodeList clListNode = clXmlNode.ChildNodes;
            foreach (XmlNode clNode in clListNode)
            {
                if ("X".Equals(clNode.Name))
                {
                    clVec.X = Convert.ToSingle(clNode.InnerText);
                    continue;
                }

                if ("Y".Equals(clNode.Name))
                {
                    clVec.Y = Convert.ToSingle(clNode.InnerText);
                    continue;
                }

                if ("Z".Equals(clNode.Name))
                {
                    clVec.Z = Convert.ToSingle(clNode.InnerText);
                    continue;
                }

                throw new Exception("this is not normal Vec.");
            }

            return (clVec);
        }

        /// <summary>
        /// XmlNodeListからintを生成して返す
        /// </summary>
        /// <param name="clListNode">ノードリスト</param>
        /// <param name="clName">要素名</param>
        /// <returns>int値</returns>
        public static int GetIntFromXmlNodeList(XmlNodeList clListNode, string clName)
        {
            foreach (XmlNode clNode in clListNode)
            {
                if (clName.Equals(clNode.Name))
                {
                    int inValue = Convert.ToInt32(clNode.InnerText);
                    return (inValue);
                }
            }

            return (0);
        }

        /// <summary>
        /// XmlNodeListからboolを生成して返す
        /// </summary>
        /// <param name="clListNode">ノードリスト</param>
        /// <param name="clName">要素名</param>
        /// <returns>int値</returns>
        public static bool GetBoolFromXmlNodeList(XmlNodeList clListNode, string clName)
        {
            foreach (XmlNode clNode in clListNode)
            {
                if (clName.Equals(clNode.Name))
                {
                    bool isValue = Convert.ToBoolean(clNode.InnerText);
                    return (isValue);
                }
            }

            return (false);
        }

        /// <summary>
        /// XmlNodeListからfloatを生成して返す
        /// </summary>
        /// <param name="clListNode">ノードリスト</param>
        /// <param name="clName">要素名</param>
        /// <returns>int値</returns>
        public static float GetFloatFromXmlNodeList(XmlNodeList clListNode, string clName)
        {
            foreach (XmlNode clNode in clListNode)
            {
                if (clName.Equals(clNode.Name))
                {
                    float flValue = Convert.ToSingle(clNode.InnerText);
                    return (flValue);
                }
            }

            return (0.0f);
        }

        /// <summary>
        /// XmlNodeListからVector3を生成して返す
        /// </summary>
        /// <param name="clListNode">ノードリスト</param>
        /// <param name="clName">要素名</param>
        /// <returns>int値</returns>
        public static ClsVector3 GetVecFromXmlNodeList(XmlNodeList clListNode, string clName)
        {
            ClsVector3 clValue = null;

            foreach (XmlNode clNode in clListNode)
            {
                if (clName.Equals(clNode.Name))
                {
                    clValue = ClsTool.GetVecFromXmlNode(clNode);
                    return (clValue);
                }
            }

            clValue = new ClsVector3();
            return (clValue);
        }

        /// <summary>
        /// XmlNodeListからstringを生成して返す
        /// </summary>
        /// <param name="clListNode">ノードリスト</param>
        /// <param name="clName">要素名</param>
        /// <returns>int値</returns>
        public static string GetStringFromXmlNodeList(XmlNodeList clListNode, string clName)
        {
            foreach (XmlNode clNode in clListNode)
            {
                if (clName.Equals(clNode.Name))
                {
                    return (clNode.InnerText);
                }
            }

            return ("");
        }

        /// <summary>
        /// 辞書をJsonに変更する処理
        /// </summary>
        /// <param name="clDic">辞書</param>
        /// <returns>Json</returns>
        public static string DictionaryToJson(Dictionary<string, object> clDic)
        {
            string clJsonData = "";

            clJsonData += "{";

            int inCnt = 0;
            int inMax = clDic.Keys.Count;
            foreach (string clKey in clDic.Keys)
            {
                object clVal = clDic[clKey] as object;
                if (clVal is Dictionary<string, object>)
                {
                    clJsonData += "\"" + clKey + "\":";
                    clJsonData += ClsTool.DictionaryToJson(clVal as Dictionary<string, object>);
                }
                else if (clVal is string)
                {
                    clJsonData += "\"" + clKey + "\":\"" + clVal + "\"";
                }
                else if (clVal is int?)
                {
                    if (clVal == null) clVal = 0;
                    clJsonData += "\"" + clKey + "\":" + clVal;
                }
                else
                {
                    clJsonData += "\"" + clKey + "\":" + clVal;
                }

                inCnt++;
                if (inCnt < inMax) clJsonData += ",";
            }

            clJsonData += "}";

            return (clJsonData);
        }

        /// <summary>
        /// オプションタイプから文字列に変更する処理
        /// </summary>
        /// <param name="enTypeOption">オプションタイプ</param>
        /// <returns>オプションタイプの名称</returns>
        public static string CnvTypeOption2Name(TYPE_OPTION enTypeOption)
        {
            string clName = "";

            switch (enTypeOption)
            {
            case TYPE_OPTION.NONE:
            case TYPE_OPTION.DISPLAY:
                clName = "";
                break;
            case TYPE_OPTION.POSITION:
                clName = "Position";
                break;
            case TYPE_OPTION.ROTATION:
                clName = "Rotation";
                break;
            case TYPE_OPTION.SCALE:
                clName = "Scale";
                break;
            case TYPE_OPTION.OFFSET:
                clName = "Offset";
                break;
            case TYPE_OPTION.FLIP:
                clName = "Flip";
                break;
            case TYPE_OPTION.TRANSPARENCY:
                clName = "Transparency";
                break;
            case TYPE_OPTION.COLOR:
                clName = "Color";
                break;
            case TYPE_OPTION.USER_DATA:
                clName = "User data(text)";
                break;
            default:
                break;
            }

            return (clName);
        }

        /// <summary>
        /// パラメータータイプから文字列に変更する処理
        /// </summary>
        /// <param name="enTypeParam">パラメータータイプ</param>
        /// <returns>パラメータータイプの名称</returns>
        public static string CnvTypeParam2Name(TYPE_PARAM enTypeParam)
        {
            string clName = "";

            switch (enTypeParam)
            {
            case TYPE_PARAM.NONE:
            case TYPE_PARAM.DISPLAY:
                clName = "";
                break;
            case TYPE_PARAM.POSITION_X:
                clName = "Position X";
                break;
            case TYPE_PARAM.POSITION_Y:
                clName = "Position Y";
                break;
            case TYPE_PARAM.ROTATION_Z:
                clName = "Rotation Z";
                break;
            case TYPE_PARAM.SCALE_X:
                clName = "Scale X";
                break;
            case TYPE_PARAM.SCALE_Y:
                clName = "Scale Y";
                break;
            case TYPE_PARAM.OFFSET_X:
                clName = "Offset X";
                break;
            case TYPE_PARAM.OFFSET_Y:
                clName = "Offset Y";
                break;
            case TYPE_PARAM.FLIP_HORIZONAL:
                clName = "Flip Horizonal";
                break;
            case TYPE_PARAM.FLIP_VERTICAL:
                clName = "Flip Vertical";
                break;
            case TYPE_PARAM.TRANSPARENCY:
                clName = "Transparency";
                break;
            case TYPE_PARAM.COLOR:
                clName = "Color";
                break;
            case TYPE_PARAM.USER_DATA:
                clName = "User data(text)";
                break;
            default:
                break;
            }

            return (clName);
        }
    }

    public class ClsParam
    {
        public bool mEnableDisplayKeyFrame;     //表示キーフレーム存在フラグ
        public bool mEnableDisplayParent;       //親の設定依存フラグ
        public bool mDisplay;                   //表示フラグ

        public bool mEnablePositionKeyFrame;    //座標キーフレーム存在フラグ
        public float mX;                        //Ｘ座標（常に有効）
        public float mY;                        //Ｙ座標（常に有効）

        public bool mEnableRotationOption;      //回転オプション存在フラグ
        public bool mEnableRotationKeyFrame;    //回転キーフレーム存在フラグ
        public float mRZ;                       //回転値

        public bool mEnableScaleOption;         //スケールオプション存在フラグ
        public bool mEnableScaleKeyFrame;       //スケールキーフレーム存在フラグ
        public float mSX;                       //スケールＸ
        public float mSY;                       //スケールＹ

        public bool mEnableOffsetOption;        //オフセットオプション存在フラグ
        public bool mEnableOffsetKeyFrame;      //オフセットキーフレーム存在フラグ
        public bool mEnableOffsetParent;        //親の設定依存フラグ
        public float mCX;                       //オフセットＸ座標
        public float mCY;                       //オフセットＹ座標

        public bool mEnableFlipOption;          //反転オプション存在フラグ
        public bool mEnableFlipKeyFrame;        //反転キーフレーム存在フラグ
        public bool mEnableFlipParent;          //親の設定依存フラグ
        public bool mFlipH;                     //水平反転フラグ
        public bool mFlipV;                     //垂直反転フラグ

        public bool mEnableTransOption;         //透明オプション存在フラグ
        public bool mEnableTransKeyFrame;       //透明キーフレーム存在フラグ
        public bool mEnableTransParent;         //親の設定依存フラグ
        public int mTrans;                      //透明透明値0～255

        public bool mEnableColorOption;         //マテリアルカラーオプション存在フラグ
        public bool mEnableColorKeyFrame;       //マテリアルカラーキーフレーム存在フラグ
        public bool mEnableColorParent;         //親の設定依存フラグ
        public int mColor;                      //マテリアルカラー値（α無し RGBのみ）

        public bool mEnableUserDataOption;      //ユーザーデータオプション存在フラグ
        public bool mEnableUserDataKeyFrame;    //ユーザーデータキーフレーム存在フラグ
        public string mUserData;                //ユーザーデータ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClsParam()
        {
            this.mEnableDisplayKeyFrame = false;
            this.mDisplay = (bool)ClsParam.GetDefaultValue(TYPE_PARAM.DISPLAY);

            this.mEnablePositionKeyFrame = false;
            this.mX = (float)ClsParam.GetDefaultValue(TYPE_PARAM.POSITION_X);
            this.mY = (float)ClsParam.GetDefaultValue(TYPE_PARAM.POSITION_Y);

            this.mEnableRotationOption = false;
            this.mEnableRotationKeyFrame = false;
            this.mRZ = (float)ClsParam.GetDefaultValue(TYPE_PARAM.ROTATION_Z);

            this.mEnableScaleOption = false;
            this.mEnableScaleKeyFrame = false;
            this.mSX = (float)ClsParam.GetDefaultValue(TYPE_PARAM.SCALE_X);
            this.mSY = (float)ClsParam.GetDefaultValue(TYPE_PARAM.SCALE_Y);

            this.mEnableOffsetOption = false;
            this.mEnableOffsetKeyFrame = false;
            this.mCX = (float)ClsParam.GetDefaultValue(TYPE_PARAM.OFFSET_X);
            this.mCY = (float)ClsParam.GetDefaultValue(TYPE_PARAM.OFFSET_Y);

            this.mEnableFlipOption = false;
            this.mEnableFlipKeyFrame = false;
            this.mFlipH = (bool)ClsParam.GetDefaultValue(TYPE_PARAM.FLIP_HORIZONAL);
            this.mFlipV = (bool)ClsParam.GetDefaultValue(TYPE_PARAM.FLIP_VERTICAL);

            this.mEnableTransOption = false;
            this.mEnableTransKeyFrame = false;
            this.mTrans = (int)ClsParam.GetDefaultValue(TYPE_PARAM.TRANSPARENCY);

            this.mEnableColorOption = false;
            this.mEnableColorKeyFrame = false;
            this.mColor = (int)ClsParam.GetDefaultValue(TYPE_PARAM.COLOR);

            this.mEnableUserDataOption = false;
            this.mEnableUserDataKeyFrame = false;
            this.mUserData = (string)ClsParam.GetDefaultValue(TYPE_PARAM.USER_DATA);
        }

        /// <summary>
        /// デフォルトの親に影響を受けるかどうか
        /// </summary>
        /// <param name="enTypeOption">オプションタイプ</param>
        /// <returns>デフォルトの値</returns>
        public static bool GetDefaultParentFlag(TYPE_OPTION enTypeOption)
        {
            bool isParent = false;

            switch (enTypeOption)
            {
            case TYPE_OPTION.NONE:
                isParent = false;
                break;
            case TYPE_OPTION.DISPLAY:
                isParent = true;
                break;
            case TYPE_OPTION.POSITION:
                isParent = true;
                break;
            case TYPE_OPTION.ROTATION:
                isParent = true;
                break;
            case TYPE_OPTION.SCALE:
                isParent = true;
                break;
            case TYPE_OPTION.OFFSET:
                isParent = false;
                break;
            case TYPE_OPTION.FLIP:
                isParent = true;
                break;
            case TYPE_OPTION.TRANSPARENCY:
                isParent = true;
                break;
            case TYPE_OPTION.COLOR:
                isParent = false;
                break;
            case TYPE_OPTION.USER_DATA:
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
        private static object GetDefaultValue(TYPE_PARAM enTypeParam)
        {
            object clValue = null;
            switch (enTypeParam)
            {
            case TYPE_PARAM.NONE:
                clValue = null;
                break;
            case TYPE_PARAM.DISPLAY:
                clValue = true;
                break;
            case TYPE_PARAM.POSITION_X:
                clValue = 0.0f;
                break;
            case TYPE_PARAM.POSITION_Y:
                clValue = 0.0f;
                break;
            case TYPE_PARAM.ROTATION_Z:
                clValue = 0.0f;
                break;
            case TYPE_PARAM.SCALE_X:
                clValue = 1.0f;
                break;
            case TYPE_PARAM.SCALE_Y:
                clValue = 1.0f;
                break;
            case TYPE_PARAM.OFFSET_X:
                clValue = 0.0f;
                break;
            case TYPE_PARAM.OFFSET_Y:
                clValue = 0.0f;
                break;
            case TYPE_PARAM.FLIP_HORIZONAL:
                clValue = false;
                break;
            case TYPE_PARAM.FLIP_VERTICAL:
                clValue = false;
                break;
            case TYPE_PARAM.TRANSPARENCY:
                clValue = 255;
                break;
            case TYPE_PARAM.COLOR:
                clValue = (int)0xFFFFFF;
                break;
            case TYPE_PARAM.USER_DATA:
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
        public static object GetDefaultValue1(TYPE_OPTION enTypeOption)
        {
            object clValue = null;
            switch (enTypeOption)
            {
            case TYPE_OPTION.NONE:
                clValue = ClsParam.GetDefaultValue(TYPE_PARAM.NONE);
                break;
            case TYPE_OPTION.DISPLAY:
                clValue = ClsParam.GetDefaultValue(TYPE_PARAM.DISPLAY);
                break;
            case TYPE_OPTION.POSITION:
                clValue = ClsParam.GetDefaultValue(TYPE_PARAM.POSITION_X);
                break;
            case TYPE_OPTION.ROTATION:
                clValue = ClsParam.GetDefaultValue(TYPE_PARAM.ROTATION_Z);
                break;
            case TYPE_OPTION.SCALE:
                clValue = ClsParam.GetDefaultValue(TYPE_PARAM.SCALE_X);
                break;
            case TYPE_OPTION.OFFSET:
                clValue = ClsParam.GetDefaultValue(TYPE_PARAM.OFFSET_X);
                break;
            case TYPE_OPTION.FLIP:
                clValue = ClsParam.GetDefaultValue(TYPE_PARAM.FLIP_HORIZONAL);
                break;
            case TYPE_OPTION.TRANSPARENCY:
                clValue = ClsParam.GetDefaultValue(TYPE_PARAM.TRANSPARENCY);
                break;
            case TYPE_OPTION.COLOR:
                clValue = ClsParam.GetDefaultValue(TYPE_PARAM.COLOR);
                break;
            case TYPE_OPTION.USER_DATA:
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
        public static object GetDefaultValue2(TYPE_OPTION enTypeOption)
        {
            object clValue = null;
            switch (enTypeOption)
            {
            case TYPE_OPTION.NONE:
                clValue = null;
                break;
            case TYPE_OPTION.DISPLAY:
                clValue = null;
                break;
            case TYPE_OPTION.POSITION:
                clValue = ClsParam.GetDefaultValue(TYPE_PARAM.POSITION_Y);
                break;
            case TYPE_OPTION.ROTATION:
                clValue = null;
                break;
            case TYPE_OPTION.SCALE:
                clValue = ClsParam.GetDefaultValue(TYPE_PARAM.SCALE_Y);
                break;
            case TYPE_OPTION.OFFSET:
                clValue = ClsParam.GetDefaultValue(TYPE_PARAM.OFFSET_Y);
                break;
            case TYPE_OPTION.FLIP:
                clValue = ClsParam.GetDefaultValue(TYPE_PARAM.FLIP_VERTICAL);
                break;
            case TYPE_OPTION.TRANSPARENCY:
                clValue = null;
                break;
            case TYPE_OPTION.COLOR:
                clValue = null;
                break;
            case TYPE_OPTION.USER_DATA:
                clValue = null;
                break;
            }

            return (clValue);
        }
    }

    public class ClsVector2
    {
        public float X;
        public float Y;

        public ClsVector2(float flX, float flY)
        {
            this.X = flX;
            this.Y = flY;
        }
    }

    public class ClsVector3
    {
        public float X;
        public float Y;
        public float Z;

        public ClsVector3()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }
        public ClsVector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        //Copy
        public ClsVector3(ClsVector3 c)
        {
            this.X = c.X;
            this.Y = c.Y;
            this.Z = c.Z;
        }
        public ClsVector3 Clone()
        {
            return new ClsVector3(this);
        }
        public static ClsVector3 operator +(ClsVector3 v1, ClsVector3 v2)
        {
            return new ClsVector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static ClsVector3 operator -(ClsVector3 v1, ClsVector3 v2)
        {
            return new ClsVector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        //v2の要素0はv1を適用
        public static ClsVector3 operator /(ClsVector3 v1, ClsVector3 v2)
        {
            ClsVector3 ret = new ClsVector3();
            ret.X = (v2.X == 0) ? v1.X : (v1.X / v2.X);
            ret.Y = (v2.Y == 0) ? v1.Y : (v1.Y / v2.Y);
            ret.Z = (v2.Z == 0) ? v1.Z : (v1.Z / v2.Z);
            return ret;
        }
        //f=0はV1を返す
        public static ClsVector3 operator /(ClsVector3 v1, float f)
        {
            if (f == 0) return v1;
            return new ClsVector3(v1.X / f, v1.Y / f, v1.Z / f);
        }
        public static ClsVector3 operator /(ClsVector3 v1, double d)
        {
            if (d == 0) return v1;
            return new ClsVector3((float)(v1.X / d), (float)(v1.Y / d), (float)(v1.Z / d));
        }
        public static ClsVector3 operator *(ClsVector3 v1, ClsVector3 v2)
        {
            return new ClsVector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }
        public static ClsVector3 operator *(ClsVector3 v1, float f)
        {
            return new ClsVector3(v1.X * f, v1.Y * f, v1.Z * f);
        }
        public static ClsVector3 operator *(ClsVector3 v1, double d)
        {
            return new ClsVector3((float)(v1.X * d), (float)(v1.Y * d), (float)(v1.Z * d));
        }
        public static float Angle(ClsVector3 v1, ClsVector3 v2)
        {
            double fs = Math.Sqrt(v1.Length() * v2.Length());
            if (fs != 0f) return 0;
            return (float)Math.Acos(v1.Dot(v1, v2) / fs);
        }

        //
        public void Set(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public void Add(ClsVector3 b)
        {
            X += b.X;
            Y += b.Y;
            Z += b.Z;
        }
        public ClsVector3 Add(ClsVector3 a, ClsVector3 b)
        {
            ClsVector3 ret = new ClsVector3(a);
            ret.Add(b);
            return ret;
        }

        //Maxim
        public void Max(ClsVector3 c)
        {
            this.X = (this.X > c.X) ? this.X : c.X;
            this.Y = (this.Y > c.Y) ? this.Y : c.Y;
            this.Z = (this.Z > c.Z) ? this.X : c.Z;
        }
        //Minimum
        public void Min(ClsVector3 c)
        {
            this.X = (this.X < c.X) ? this.X : c.X;
            this.Y = (this.Y < c.Y) ? this.Y : c.Y;
            this.Z = (this.Z < c.Z) ? this.X : c.Z;
        }
        //ret:new Vector = this - B
        public void Distance(ClsVector3 b)
        {
            X -= b.X;
            Y -= b.Y;
            Z -= b.Z;
        }
        //this*Vector3(*= Vector3)
        public void Scale(ClsVector3 s)
        {
            X *= s.X;
            Y *= s.Y;
            Z *= s.Z;
        }
        //this*float(*= float)
        public void Scale(float s)
        {
            X *= s;
            Y *= s;
            Z *= s;
        }
        //内積
        public float Dot(ClsVector3 v1, ClsVector3 v2)
        {
            return (v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z);
        }
        //外積
        public static ClsVector3 Cross(ClsVector3 v1, ClsVector3 v2)
        {
            return new ClsVector3(
            v1.Y * v2.Z - v1.Z * v2.Y,
            v1.Z * v2.X - v1.X * v2.Z,
            v1.X * v2.Y - v1.Y * v2.X);
        }

        //正規化
        public ClsVector3 Normalize()
        {
            ClsVector3 v = new ClsVector3();
            float leng = this.Length();
            if (leng != 0) v = this / leng;
            return v;
        }
        //累乗
        public float Power()
        {
            return (X * X + Y * Y + Z * Z);
        }
        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        //2点間線形補完Linear(rate 0-1)
        public static ClsVector3 Linear(ClsVector3 v1, ClsVector3 v2, double rate)
        {
            ClsVector3 ret = new ClsVector3();
            if (rate < 0.0f) rate = 0.0f;
            if (rate > 1.0f) rate = 1.0f;//clamp
            //ret = v1 * (1.0f - rate) + v2 * rate;
            ret = v1 + (v2 - v1) * rate;
            return ret;
        }
        //3次補完 Lerp
        public static ClsVector3 Lerp(ClsVector3 v1, ClsVector3 v2, double rate)
        {
            ClsVector3 ret = new ClsVector3();
            if (rate < 0.0f) rate = 0.0f;
            if (rate > 1.0f) rate = 1.0f;//clamp
            rate = rate * rate * (3.0f - 2.0f * rate);//ここの違いで色々?
            ret = v1 * (1.0f - rate) + v2 * rate;
            return ret;
        }
        //角度計算
        public static float ToAngle(ClsVector3 from, ClsVector3 to)
        {
            //note: Degress = radians * (180/Math.PI);
            //2D θ=arcTan(x,y)
            //3D r=sqrt(x*x+y*y+z*z)
            //   θ=arcTan(sqrt(x*x+y*y),z)
            //   Φ=arcTan(y,x)

            //まだ
            return 0;
        }
        //回転
        public static void Rotate(float Radian)
        {

        }

        public String ToString2()
        {
            return $"{X},{Y},{Z},";
        }
    }
}
