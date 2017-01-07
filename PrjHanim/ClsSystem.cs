using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PrjHikariwoAnim
{
    public class ClsSystem
    {
        public static Hashtable mTblImage; //キーはstringのMD5 値はClsImage
        public static ClsSetting mSetting = null;   //保存データ

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
            ClsSystem.mTblImage = new Hashtable();
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

        public static string DictionaryToString(Dictionary<string, object> clDic)
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
                    clJsonData += DictionaryToString(clVal as Dictionary<string, object>);
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
