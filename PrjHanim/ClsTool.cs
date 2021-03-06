﻿using System;
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
        public static string CnvTypeOption2Name(EnmTypeOption enTypeOption)
        {
            string clName = "";

            switch (enTypeOption)
            {
            case EnmTypeOption.NONE:
            case EnmTypeOption.DISPLAY:
                clName = "";
                break;
            case EnmTypeOption.POSITION:
                clName = "Position";
                break;
            case EnmTypeOption.ROTATION:
                clName = "Rotation";
                break;
            case EnmTypeOption.SCALE:
                clName = "Scale";
                break;
            case EnmTypeOption.OFFSET:
                clName = "Offset";
                break;
            case EnmTypeOption.FLIP:
                clName = "Flip";
                break;
            case EnmTypeOption.TRANSPARENCY:
                clName = "Transparency";
                break;
            case EnmTypeOption.COLOR:
                clName = "Color";
                break;
            case EnmTypeOption.USER_DATA:
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
        public static string CnvTypeParam2Name(EnmTypeParam enTypeParam)
        {
            string clName = "";

            switch (enTypeParam)
            {
            case EnmTypeParam.NONE:
            case EnmTypeParam.DISPLAY:
                clName = "";
                break;
            case EnmTypeParam.POSITION_X:
                clName = "Position X";
                break;
            case EnmTypeParam.POSITION_Y:
                clName = "Position Y";
                break;
            case EnmTypeParam.ROTATION_Z:
                clName = "Rotation Z";
                break;
            case EnmTypeParam.SCALE_X:
                clName = "Scale X";
                break;
            case EnmTypeParam.SCALE_Y:
                clName = "Scale Y";
                break;
            case EnmTypeParam.OFFSET_X:
                clName = "Offset X";
                break;
            case EnmTypeParam.OFFSET_Y:
                clName = "Offset Y";
                break;
            case EnmTypeParam.FLIP_HORIZONAL:
                clName = "Flip Horizonal";
                break;
            case EnmTypeParam.FLIP_VERTICAL:
                clName = "Flip Vertical";
                break;
            case EnmTypeParam.TRANSPARENCY:
                clName = "Transparency";
                break;
            case EnmTypeParam.COLOR:
                clName = "Color";
                break;
            case EnmTypeParam.USER_DATA:
                clName = "User data(text)";
                break;
            default:
                break;
            }

            return (clName);
        }
    }
}
