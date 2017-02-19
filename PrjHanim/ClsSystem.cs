using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace PrjHikariwoAnim
{
     public class ClsSystem
    {
        public static string FILE_TAG = "  ";
        public static string mHeader;
        public static int mVer;
        public static Hashtable mTblImage;  //キーはstringのMD5　値はClsImage
        public static ClsSetting mSetting = null;   //保存データ
        public static int mMotionSelectKey;//現在編集中のモーションキー（TreeNodeのハッシュコード）
        public static Dictionary<int, ClsDatMotion> mDicMotion; //キーは TreeNode の HashCode　値はモーション管理クラス
        public static ImageManagerBase ImageMan;
        public static StringBuilder mFileBuffer;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public static void Init()
        {
            //以下、保存データ読み込み処理
            ClsSystem.mSetting = null;
            string clFileName = ClsSystem.GetAppFileName();
            string clPath = ClsPath.GetPath(clFileName + ".setting");
            bool isExist = File.Exists(clPath);
            if (isExist)
            {
                try
                {
                    using (FileStream clStream = new FileStream(clPath, FileMode.Open))
                    {
                        DataContractJsonSerializer clSerializer = new DataContractJsonSerializer(typeof(ClsSetting));
                        ClsSystem.mSetting = (ClsSetting)clSerializer.ReadObject(clStream);
                        clStream.Close();
                    }
                }
                catch (Exception)
                {
                    ClsSystem.mSetting = null;
                }
            }
            if (ClsSystem.mSetting == null)
            {
                ClsSystem.mSetting = new ClsSetting();
                ClsSystem.mSetting.Save();
            }

            //以下、イメージテーブル作成処理
            ClsSystem.ImageMan = new ImageManagerBase();
            ClsSystem.mTblImage = new Hashtable();

            //以下、データ初期化処理
            ClsSystem.mMotionSelectKey = -1;
            ClsSystem.mDicMotion = new Dictionary<int, ClsDatMotion>();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public static void Exit()
        {
            //以下、保存データ保存処理
            if (ClsSystem.mSetting != null)
            {
                ClsSystem.mSetting.Save();
            }
        }

        /// <summary>
        /// 読み込み処理
        /// </summary>
        /// <param name="clListView">モーションリストビュー</param>
        /// <param name="clFilePath">ファイルパス</param>
        public static void Load(ListView clListView, string clFilePath)
        {
            ClsSystem.mMotionSelectKey = -1;

            XmlDocument clXmlDoc = new XmlDocument();
            try
            {
                //以下、xmlファイル読み込み処理
                clXmlDoc.Load(clFilePath);

                //以下、プロジェクトファイル読み込み処理
                IEnumerator iEnum = clXmlDoc.DocumentElement.GetEnumerator();
                while (iEnum.MoveNext())
                {
                    XmlElement clXmlElem = iEnum.Current as XmlElement;

                    if ("Header".Equals(clXmlElem.Name))
                    {
                        if (!"hap".Equals(clXmlElem.InnerText))
                        {
                            throw new Exception("this is not hanim project file.");
                        }

                        ClsSystem.mHeader = clXmlElem.InnerText;
                        continue;
                    }

                    if ("Ver".Equals(clXmlElem.Name))
                    {
                        Match clMatch = Regex.Match(clXmlElem.InnerText, "^\\d+$");
                        if (!clMatch.Success)
                        {
                            throw new Exception("this is not allowed version.");
                        }

                        ClsSystem.mVer = Convert.ToInt32(clXmlElem.InnerText);
                        continue;
                    }

                    if ("Image".Equals(clXmlElem.Name))
                    {
                        string clPath = clXmlElem.InnerText;
                        bool isExist = File.Exists(clPath);
                        if (isExist)
                        {
                            ClsSystem.ImageMan.AddCellFromPath(clPath);
                        }
                        else
                        {
                            ClsSystem.ImageMan.AddCellFromImage(Properties.Resources.error);
                        }
                        continue;
                    }

                    if ("Motion".Equals(clXmlElem.Name))
                    {
                        ClsDatMotion clMotion = new ClsDatMotion(0, "");
                        clMotion.Load(clXmlElem);

                        ListViewItem clListViewItem = new ListViewItem(clMotion.mName, 2);
                        clListView.Items.Add(clListViewItem);
                        clListViewItem.Tag = ClsSystem.mDicMotion.Count;

                        clMotion.mID = clListViewItem.GetHashCode();
                        clMotion.Restore();     //モーションの親子関連付け再構築処理
                        clMotion.Assignment();  //行番号などを設定する処理
                        ClsSystem.mDicMotion.Add(clListViewItem.GetHashCode(), clMotion);
                        continue;
                    }

                    throw new Exception("this is abnormality format.");
                }

                //以下、デフォルトで選択しているモーションを設定する処理
                if (clListView.Items.Count >= 1)
                {
                    ClsSystem.mMotionSelectKey = clListView.Items[0].GetHashCode();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        /// <summary>
        /// 保存処理
        /// </summary>
        /// <param name="clFilePath">ファイルパス</param>
        public static void Save(string clFilePath)
        {
            ClsSystem.mFileBuffer = new StringBuilder();

            //以下、プロジェクトファイル保存処理
            string clLine = "?xml version=\"1.0\" encoding=\"utf-8\"?";
            ClsSystem.AppendElementStart("", clLine);

            clLine = "HanimProjectData xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"";
            ClsSystem.AppendElementStart("", clLine);

            string clHeader = ClsSystem.FILE_TAG;
            string clHeaderName = "hap";
            ClsSystem.AppendElement(clHeader, "Header", clHeaderName);

            int inVersion = 1;
            ClsSystem.AppendElement(clHeader, "Ver", inVersion);

            //以下、イメージリスト保存処理
            int inIndex = 0;
            while (true)
            {
                ImageChip clImage = ClsSystem.ImageMan.GetImageChipFromIndex(inIndex);
                if (clImage == null) break;

                ClsSystem.AppendElement(clHeader, "Image", clImage.Path);
                inIndex++;
            }

            //以下、モーションリスト保存処理
            foreach (int inKey in ClsSystem.mDicMotion.Keys)
            {
                ClsDatMotion clMotion = ClsSystem.mDicMotion[inKey];
                clMotion.Save(clHeader);
            }

            ClsSystem.AppendElementEnd("", "HanimProjectData");

            //以下、プロジェクトファイル保存処理
            string clBuffer = ClsSystem.mFileBuffer.ToString();
            File.WriteAllText(clFilePath, clBuffer, Encoding.UTF8);
        }

        public static string GetAppFileName()
        {
            Assembly clAssembly = Assembly.GetExecutingAssembly();
            string clFileName = Path.GetFileNameWithoutExtension(clAssembly.Location);
            return (clFileName);
        }

        public static string GetMD5FromFile(string clPath)
        {
            byte[] pchBuffer = File.ReadAllBytes(clPath);
            string clHash = ClsSystem.GetMD5FromMemory(pchBuffer);
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
        /// <returns>ウィンドウ名</returns>
        public static string GetWindowName(string clWindowName, ClsDatElem clElem)
        {
            string clName = "";
            if (clElem != null && clElem.mMotion != null)
            {
                clName = " ( " + clElem.mMotion.mName + " [ " + clElem.mName + " ] )";
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
        public static void AppendElement(string clHeader, string clName, Vector3 clVec)
        {
            ClsSystem.AppendElementStart(clHeader, clName);
            ClsSystem.AppendElement(clHeader + ClsSystem.FILE_TAG, "X", clVec.X);
            ClsSystem.AppendElement(clHeader + ClsSystem.FILE_TAG, "Y", clVec.Y);
            ClsSystem.AppendElement(clHeader + ClsSystem.FILE_TAG, "Z", clVec.Z);
            ClsSystem.AppendElementEnd(clHeader, clName);
        }

        /// <summary>
        /// XmlNodeからVector3を生成して返す
        /// </summary>
        /// <param name="clXmlNode">xmlノード</param>
        /// <returns>Vector3</returns>
        public static Vector3 GetVecFromXmlNode(XmlNode clXmlNode)
        {
            Vector3 clVec = new Vector3();

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
                    clJsonData += ClsSystem.DictionaryToJson(clVal as Dictionary<string, object>);
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

    }
}
