using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace PrjHikariwoAnim
{
    public class ClsSystem
    {
        public static string FILE_TAG = "  ";
        public static string mHeader;
        public static int mVer;
        public static ClsSetting mSetting = null;   //保存データ
        public static int mMotionSelectKey;  //現在選択中のモーションキー
        public static Dictionary<int, ClsDatImage> mDicImage;   //キーはランダム値　値はClsDatImage
        public static Dictionary<int, ClsDatMotion> mDicMotion; //キーはランダム値　値はモーション管理クラス
        public static StringBuilder mFileBuffer;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public static void Init()
        {
            //以下、保存データ読み込み処理
            ClsSystem.mSetting = null;
            string clFileName = ClsTool.GetAppFileName();
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

            //以下、データ初期化処理
            ClsSystem.mDicImage = new Dictionary<int, ClsDatImage>();
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
        /// 新しいイメージキー
        /// </summary>
        /// <returns>イメージキー</returns>
        public static int GetNewID()
        {
            Random clRand = new Random();
            int inKey = clRand.Next();
            bool isLoop = true;
            while(isLoop)
            {
                bool isExist = ClsSystem.mDicImage.ContainsKey(inKey);
                if(isExist)
                {
                    inKey = clRand.Next();
                    continue;
                }

                isExist = ClsSystem.mDicMotion.ContainsKey(inKey);
                if (isExist)
                {
                    inKey = clRand.Next();
                    continue;
                }

                break;
            }

            return (inKey);
        }

        /// <summary>
        /// イメージ削除処理
        /// </summary>
        /// <param name="inKey">イメージキー</param>
        public static void RemoveImage(int inKey)
        {
            if (inKey <= 0) return;

            bool isExist = ClsSystem.mDicImage.ContainsKey(inKey);
            if (!isExist) return;

            ClsDatImage clDatImage = ClsSystem.mDicImage[inKey];
            clDatImage.Remove();

            ClsSystem.mDicImage.Remove(inKey);
        }

        /// <summary>
        /// イメージ全削除処理
        /// </summary>
        public static void RemoveAllImage()
        {
            foreach(int inKey in ClsSystem.mDicImage.Keys)
            {
                ClsDatImage clDatImage = ClsSystem.mDicImage[inKey];
                clDatImage.Remove();
            }
            ClsSystem.mDicImage.Clear();
        }

        /// <summary>
        /// モーション全削除処理
        /// </summary>
        public static void RemoveAllMotion()
        {
            //以下、モーションクリア処理
            foreach (int inKey in ClsSystem.mDicMotion.Keys)
            {
                ClsDatMotion clDatMotion = ClsSystem.mDicMotion[inKey];
                clDatMotion.Remove();
            }
            ClsSystem.mDicMotion.Clear();
        }

        /// <summary>
        /// イメージを作成する
        /// イメージファイルパスからファイルを読み込んでSystem.mListImageに追加して、インデックスを返します
        /// ただし、すでにSystem.mListImageに存在していた場合は、リストに追加せずに、そのインデックスを返します
        /// </summary>
        /// <param name="clFilePath">イメージファイルパス</param>
        /// <returns>イメージキー</returns>
        public static int CreateImageFromFile(string clFilePath)
        {
            Image clImage = Bitmap.FromFile(clFilePath);

            int inKey = ClsSystem.CreateImageFromImage(clImage);
            ClsDatImage clDatImage = ClsSystem.mDicImage[inKey];
            clDatImage.mPath = clFilePath;

            return (inKey);
        }

        /// <summary>
        /// イメージを作成する
        /// イメージをSystem.mListImageに追加して、インデックスを返します
        /// ただし、すでにSystem.mListImageに存在していた場合は、リストに追加せずに、そのインデックスを返します
        /// </summary>
        /// <param name="clImage">イメージ</param>
        /// <returns>イメージキー</returns>
        public static int CreateImageFromImage(Image clImage)
        {
            int inKey = ClsSystem.GetImageIndexFromImage(clImage);
            if (inKey >= 0) return (inKey);

            //以下、イメージを新規作成して、そのインデックスを返す処理
            ClsDatImage clDatImage = new ClsDatImage();
            clDatImage.SetImage(clImage);
            clDatImage.mID = ClsSystem.GetNewID();
            ClsSystem.mDicImage.Add(clDatImage.mID, clDatImage);

            return (clDatImage.mID);
        }

        /// <summary>
        /// 選択中のインデックスのリストを取得する
        /// </summary>
        /// <returns>選択中のインデックスのリスト</returns>
        public static List<int> GetImageSelectIndex()
        {
            List<int> clListIndex = new List<int>();

            foreach(int inKey in ClsSystem.mDicImage.Keys)
            {
                ClsDatImage clDatImage = ClsSystem.mDicImage[inKey];
                if (!clDatImage.mSelect) continue;

                clListIndex.Add(inKey);
            }

            return (clListIndex);
        }

        /// <summary>
        /// イメージインデックスを取得する
        /// </summary>
        /// <param name="clFilePath">イメージファイルパス</param>
        /// <returns>イメージインデックス</returns>
        public static int GetImageIndexFromFile(string clFilePath)
        {
            Image clImage = Bitmap.FromFile(clFilePath);
            int inKey = ClsSystem.GetImageIndexFromImage(clImage);
            return (inKey);
        }

        /// <summary>
        /// イメージインデックスを取得する
        /// </summary>
        /// <param name="clImage">イメージ</param>
        /// <returns>イメージインデックス</returns>
        public static int GetImageIndexFromImage(Image clImage)
        {
            if (clImage == null) return (-1);

            string clHash = ClsTool.GetMD5FromImage(clImage);

            foreach(int inKey in ClsSystem.mDicImage.Keys)
            {
                ClsDatImage clDatImage = ClsSystem.mDicImage[inKey];
                if (clDatImage.mImgOrigin == null) continue;

                string clHashTmp = ClsTool.GetMD5FromImage(clDatImage.mImgOrigin);
                if (!clHashTmp.Equals(clHash)) continue;

                return (inKey);
            }

            return (-1);
        }

        /// <summary>
        /// 同じイメージが存在するかチェックする
        /// </summary>
        /// <param name="clFilePath">イメージファイルパス</param>
        /// <returns>存在フラグ</returns>
        public static bool IsExistImageFromFile(string clFilePath)
        {
            Image clImage = Bitmap.FromFile(clFilePath);
            bool isExist = ClsSystem.IsExistImageFromImage(clImage);
            return (isExist);
        }

        /// <summary>
        /// 同じイメージが存在するかチェックする
        /// </summary>
        /// <param name="clImage">イメージ</param>
        /// <returns>存在フラグ</returns>
        public static bool IsExistImageFromImage(Image clImage)
        {
            if (clImage == null) return (false);

            string clHash = ClsTool.GetMD5FromImage(clImage);

            foreach(int inKey in ClsSystem.mDicImage.Keys)
            {
                ClsDatImage clDatImage = ClsSystem.mDicImage[inKey];
                if (clDatImage.mImgOrigin == null) continue;

                string clHashTmp = ClsTool.GetMD5FromImage(clDatImage.mImgOrigin);
                if (!clHashTmp.Equals(clHash)) continue;

                return (true);
            }

            return (false);
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
                        ClsDatImage clDatImage = new ClsDatImage();
                        clDatImage.Load(clXmlElem);

                        ClsSystem.mDicImage.Add(clDatImage.mID, clDatImage);
                        continue;
                    }

                    if ("Motion".Equals(clXmlElem.Name))
                    {
                        ClsDatMotion clDatMotion = new ClsDatMotion(0, "");
                        clDatMotion.Load(clXmlElem);

                        ListViewItem clListViewItem = new ListViewItem(clDatMotion.mName, 2);
                        clListView.Items.Add(clListViewItem);
                        clListViewItem.Tag = ClsSystem.mDicMotion.Count;

                        clDatMotion.mItemHashCode = clListViewItem.GetHashCode();
                        clDatMotion.Restore();     //モーションの親子関連付け再構築処理
                        clDatMotion.Assignment();  //行番号などを設定する処理
                        ClsSystem.mDicMotion.Add(clDatMotion.mID, clDatMotion);
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
            ClsTool.AppendElementStart("", clLine);

            clLine = "HanimProjectData xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"";
            ClsTool.AppendElementStart("", clLine);

            string clHeader = ClsSystem.FILE_TAG;
            string clHeaderName = "hap";
            ClsTool.AppendElement(clHeader, "Header", clHeaderName);

            int inVersion = 1;
            ClsTool.AppendElement(clHeader, "Ver", inVersion);

            //以下、イメージリスト保存処理
            foreach(int inKey in ClsSystem.mDicImage.Keys)
            {
                ClsDatImage clDatImage = ClsSystem.mDicImage[inKey];
                clDatImage.Save(clHeader);
            }

            //以下、モーションリスト保存処理
            foreach (int inKey in ClsSystem.mDicMotion.Keys)
            {
                ClsDatMotion clDatMotion = ClsSystem.mDicMotion[inKey];
                clDatMotion.Save(clHeader);
            }

            ClsTool.AppendElementEnd("", "HanimProjectData");

            //以下、プロジェクトファイル保存処理
            string clBuffer = ClsSystem.mFileBuffer.ToString();
            File.WriteAllText(clFilePath, clBuffer, Encoding.UTF8);
        }
    }
}
