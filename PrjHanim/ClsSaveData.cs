using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace PrjHikariwoAnim
{
    [Serializable]
    [XmlRoot("HanimProjectData")]
    public class ClsFileData
    {
        [XmlIgnore]
        public int mIndex;

        [XmlElement("Header")]
        public string mHeader = "hap";
        [XmlElement("Ver")]
        public int mVer = 1;

        [XmlElement("MotionSelectKey")]
        public int mMotionSelectKey;        //現在編集中のモーションキー（TreeNodeのハッシュコード）
        [XmlElement("MotionCount")]
        public int mMotionCount;            //モーション数
        [XmlArray("ListMotion")]
        [XmlArrayItem("Motion")]
        public List<ClsFileMotion> mListMotion;     //各モーションのリスト
        [XmlArray("ListElem")]
        [XmlArrayItem("Elem")]
        public List<ClsFileElem> mListElem;         //各エレメントのリスト
        [XmlArray("ListOption")]
        [XmlArrayItem("Option")]
        public List<ClsFileOption> mListOption;     //各オプションのリスト
        [XmlArray("ListKeyFrame")]
        [XmlArrayItem("KeyFrame")]
        public List<ClsFileKeyFrame> mListKeyFrame; //各キーフレームのリスト
        [XmlArray("ListTween")]
        [XmlArrayItem("Tween")]
        public List<ClsFileTween> mListTween;       //各トゥイーンのリスト
        [XmlArray("ListImage")]
        [XmlArrayItem("Image")]
        public ImageChip[] mListImageChip;  //ImageChipList

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClsFileData()
        {
            this.mIndex = 1;
            this.mListMotion = new List<ClsFileMotion>();       //各モーションのリスト
            this.mListElem = new List<ClsFileElem>();           //各エレメントのリスト
            this.mListOption = new List<ClsFileOption>();       //各オプションのリスト
            this.mListKeyFrame = new List<ClsFileKeyFrame>();   //各キーフレームのリスト
            this.mListTween = new List<ClsFileTween>();         //各トゥイーンのリスト
            this.mListImageChip = null;         //ImageChipList
        }

        /// <summary>
        /// モーション管理クラス保存処理
        /// </summary>
        /// <param name="clMotion">モーション管理クラス</param>
        /// <returns>インデックス</returns>
        public int AddMotion(ClsDatMotion clMotion)
        {
            ClsFileMotion clSaveMotion = new ClsFileMotion(this.mIndex, clMotion);
            this.mListMotion.Add(clSaveMotion);

            this.mIndex++;
            return (this.mIndex);
        }

        /// <summary>
        /// エレメント管理クラス保存処理
        /// </summary>
        /// <param name="clElem">エレメント管理クラス</param>
        /// <returns>インデックス</returns>
        public int AddElem(ClsDatElem clElem)
        {
            ClsFileElem clSaveElem = new ClsFileElem(this.mIndex, clElem);
            this.mListElem.Add(clSaveElem);

            this.mIndex++;
            return (this.mIndex);
        }

        /// <summary>
        /// オプション管理クラス保存処理
        /// </summary>
        /// <param name="clOption">オプション管理クラス</param>
        /// <returns>インデックス</returns>
        public int AddOption(ClsDatOption clOption)
        {
            ClsFileOption clSaveOption = new ClsFileOption(this.mIndex, clOption);
            this.mListOption.Add(clSaveOption);

            this.mIndex++;
            return (this.mIndex);
        }

        /// <summary>
        /// キーフレーム管理クラス保存処理
        /// </summary>
        /// <param name="clKeyFrame">キーフレーム管理クラス</param>
        /// <returns>インデックス</returns>
        public int AddKeyFrame(ClsDatKeyFrame clKeyFrame)
        {
            ClsFileKeyFrame clSaveKeyFrame = new ClsFileKeyFrame(this.mIndex, clKeyFrame);
            this.mListKeyFrame.Add(clSaveKeyFrame);

            this.mIndex++;
            return (this.mIndex);
        }

        /// <summary>
        /// トゥイーン管理クラス保存処理
        /// </summary>
        /// <param name="clTween">トゥイーン管理クラス</param>
        /// <returns>インデックス</returns>
        public int AddTween(ClsDatTween clTween)
        {
            ClsFileTween clSaveTween = new ClsFileTween(this.mIndex, clTween);
            this.mListTween.Add(clSaveTween);

            this.mIndex++;
            return (this.mIndex);
        }
    }

    [Serializable]
    public class ClsFileObject
    {
        [XmlElement("Index")]
        public int mIndex;

        public ClsFileObject()
        {
            this.mIndex = 0;
        }

        public ClsFileObject(int inIndex)
        {
            this.mIndex = inIndex;
        }
    }

    [Serializable]
    public class ClsFileMotion : ClsFileObject
    {
        [XmlElement("Name")]
        public string mName;   //モーション名
        [XmlElement("FrameNum")]
        public int mFrameNum;  //トータルフレーム数

        public ClsFileMotion() : base(0)
        {
            this.mName = "";
            this.mFrameNum = 0;
        }

        public ClsFileMotion(int inIndex, ClsDatMotion clMotion) : base(inIndex)
        {
            this.mName = clMotion.mName;            //モーション名
            this.mFrameNum = clMotion.mFrameNum;    //トータルフレーム数
        }
    }

    [Serializable]
    public class ClsFileElem : ClsFileObject
    {
        [XmlElement("Name")]
        public string mName;       //エレメント名
        [XmlElement("Visible")]
        public bool isVisible;     //表示非表示(目)
        [XmlElement("Locked")]
        public bool isLocked;      //ロック状態(鍵)
        [XmlElement("Open")]
        public bool isOpen;        //属性開閉状態(+-)
        [XmlElement("ImageChipID")]
        public int mImageChipID;   //イメージID

        public ClsFileElem() : base(0)
        {
            this.mName = "";
            this.isVisible = false; //表示非表示(目)
            this.isLocked = false;  //ロック状態(鍵)
            this.isOpen = false;    //属性開閉状態(+-)
            this.mImageChipID = 0;  //イメージID
        }

        public ClsFileElem(int inIndex, ClsDatElem clElem) : base(inIndex)
        {
            this.mName = clElem.mName;                  //エレメント名
            this.isVisible = clElem.isVisible;          //表示非表示(目)
            this.isLocked = clElem.isLocked;            //ロック状態(鍵)
            this.isOpen = clElem.isOpen;                //属性開閉状態(+-)
            this.mImageChipID = clElem.mImageChipID;    //イメージID
        }
    }

    [Serializable]
    public class ClsFileOption : ClsFileObject
    {
        [XmlElement("TypeOption")]
        public ClsDatOption.TYPE_OPTION mTypeOption;  //タイプ

        public ClsFileOption() : base(0)
        {
            this.mTypeOption = ClsDatOption.TYPE_OPTION.NONE;
        }

        public ClsFileOption(int inIndex, ClsDatOption clOption) : base(inIndex)
        {
            this.mTypeOption = clOption.mTypeOption;
        }
    }

    [Serializable]
    public class ClsFileKeyFrame : ClsFileObject
    {
        public ClsFileKeyFrame() : base(0)
        {

        }

        public ClsFileKeyFrame(int inIndex, ClsDatKeyFrame clKeyFrame) : base(inIndex)
        {

        }
    }

    [Serializable]
    public class ClsFileTween : ClsFileObject
    {
        [XmlElement("Param")]
        public ClsDatTween.EnmParam mParam;
        [XmlElement("FrmStart")]
        public int mFrmStart;
        [XmlElement("FrmEnd")]
        public int mFrmEnd;
        [XmlElement("Pos")]
        public Vector3 mPos;
        [XmlElement("ListVec")]
        public Vector3[] mListVec;

        public ClsFileTween() : base(0)
        {
            this.mParam = ClsDatTween.EnmParam.NONE;
            this.mFrmStart = 0;
            this.mFrmEnd = 0;
            this.mPos = null;
            this.mListVec = null;
        }

        public ClsFileTween(int inIndex, ClsDatTween clTween) : base(inIndex)
        {
            this.mParam = clTween.mParam;
            this.mFrmStart = clTween.mFrmStart;
            this.mFrmEnd = clTween.mFrmEnd;
            this.mPos = new Vector3(clTween.mPos);

            int inMax = clTween.mListVec.Length;
            if (inMax >= 1) {
                this.mListVec = new Vector3[inMax];

                int inCnt;
                for (inCnt = 0; inCnt < inMax; inCnt++) {
                    this.mListVec[inCnt] = new Vector3(clTween.mListVec[inCnt]);
                }
            }
        }
    }
}
