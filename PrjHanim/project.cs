using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{
    class project
    {
        //各種部品やモーションを統括し
        //プロジェクト全体で利用する設定情報等を管理？
        //メインすっきりするかなぁ
        /*
        project
            |->SETING 基準速度等？
            |->ImageManager LIST<CELL> 利用画像
            |->ELEMENTS parts 各種パーツ(非画像含む)
            |->Dictionary<key,Motion>mDicMotion
               |Motiin->List<FRAME> エレメントの利用リストと状態
                 |->FRAME->LIST<ELEMENTS(hash)> エレメントリスト
                    |->FRAME->LIST<ATRIBUTEbase> エレメントの状態情報
                    どの属性のデータなのか、格納方法等は要検証?
        */
        public string Name;//ProjectName FileName?

        //画像イメージ
        public ImageManagerBase mImage;
        //各種パーツ
        public List<ELEMENTS> mElements;

        //モーション と制御
        public Motion mMotion;
        private Dictionary<int, Motion> mDicMotion;

        //const
        public project(string pName)
        {
            Name = pName;
            mImage = new ImageManagerBase();
            mElements = new List<ELEMENTS>();
        }

        public bool ChangeMotion(int key)
        {
            if (mDicMotion.ContainsKey(key))
            {
                mMotion = mDicMotion[key];
                return true;
            }
            return false;//Key not found
        }
        public void RemoveMotion(int key)
        {
            if (mDicMotion.ContainsKey(key))
            {
                mDicMotion.Remove(key);
            }
        }
        public void AddNewMotion(int key,string name)
        {
        }
        public void AddNewMotion(Motion motion) { }
        public void AddMotion(int key,Motion motion)
        {
            mDicMotion.Add(key, motion);
        }
        /// <summary>
        /// TreeeViewを与えて内部mDicMotionのキー再構築
        /// </summary>
        /// <param name="tv"></param>
        public void BuildMotionKey(TreeView tv)
        {
            tv.SelectedNode = tv.TopNode;
            Dictionary<int, Motion> newDic = new Dictionary<int, Motion>();
            foreach(int key in mDicMotion.Keys)
            {
                TreeNode tn = tv.Nodes.Add( mDicMotion[key].Name);
                tn.ImageIndex = 2;
                tn.SelectedImageIndex = 2;
                newDic.Add(tn.GetHashCode(), mDicMotion[key]);
            }
            mDicMotion.Clear();
            mDicMotion = newDic;
        }
        
        public void Save(string fName)
        {
            FileStream fs = new FileStream(fName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            
        }
        public void Load(string fName) { }

    }
}
