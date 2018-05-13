using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace PrjHikariwoAnim
{
    [Serializable]
    public class ClsDatTween
    {
        public static float POS_X0 = 0.0f;          //開始座標Ｘ
        public static float POS_Y0 = 1.0f;          //開始座標Ｙ
        public static float POS_X1 = 0.5f;          //中心座標Ｘ
        public static float POS_Y1 = 0.5f;          //中心座標Ｙ
        public static float POS_X2 = 1.0f;          //終了座標Ｘ
        public static float POS_Y2 = 0.0f;          //終了座標Ｙ
        public static float VEC_X0 = 0.08f;         //開始座標のベクトルＸ
        public static float VEC_Y0 = -0.08f;        //開始座標のベクトルＹ
        public static float VEC_X1 = 0.08f;         //中心座標のベクトルＸ
        public static float VEC_Y1 = -0.08f;        //中心座標のベクトルＹ
        public static float VEC_X2 = 0.08f;         //終了座標のベクトルＸ
        public static float VEC_Y2 = -0.08f;        //終了座標のベクトルＹ
        public static float SIZE_ELLIPSE = 15.0f;   //円の直径
        public static int MAX_WEIGHT = 256;         //保存用データの長さ

        public EnmParam mParam;
        public int mLength;     //継続フレーム数
        public ClsVector3 mPos;
        public Image mImage;
        public List<ClsVector3> mListVec;
        public byte[] mRate = null; //要素数MAX_X個のリスト

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="enParam">種別</param>
        /// <param name="inLength">対象フレーム数</param>
        /// <param name="clPos">座標</param>
        /// <param name="pclVec">各ベクトル</param>
        public ClsDatTween(EnmParam enParam, int inLength)
        {
            this.mParam = enParam;
            this.mLength = inLength;

            this.mPos = new ClsVector3();
            this.mPos.X = ClsDatTween.POS_X1;
            this.mPos.Y = ClsDatTween.POS_Y1;

            this.mListVec = new List<ClsVector3>();
            ClsVector3 clVec = new ClsVector3(ClsDatTween.VEC_X0, ClsDatTween.VEC_Y0, 0.0f);
            this.mListVec.Add(clVec);
            clVec = new ClsVector3(ClsDatTween.VEC_X1, ClsDatTween.VEC_Y1, 0.0f);
            this.mListVec.Add(clVec);
            clVec = new ClsVector3(ClsDatTween.VEC_X2, ClsDatTween.VEC_Y2, 0.0f);
            this.mListVec.Add(clVec);

            this.mImage = ClsDatTween.CreateImage(this, 15, 15);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="enParam">種別</param>
        /// <param name="inLength">対象フレーム数</param>
        /// <param name="clPos">座標</param>
        /// <param name="pclVec">各ベクトル</param>
        public ClsDatTween(EnmParam enParam, int inLength, ClsVector3 clPos, List<ClsVector3> pclVec)
        {
            this.mParam = enParam;
            this.mLength = inLength;

            this.mPos = new ClsVector3();
            if (clPos != null)
            {
                this.mPos.X = clPos.X;
                this.mPos.Y = clPos.Y;
            }

            this.mListVec = new List<ClsVector3>();
            if (pclVec != null)
            {
                int inCnt;
                for (inCnt = 0; inCnt < 3; inCnt++)
                {
                    ClsVector3 clVec = new ClsVector3(pclVec[inCnt].X, pclVec[inCnt].Y, 0.0f);
                    this.mListVec.Add(clVec);
                }
            }

            this.mImage = ClsDatTween.CreateImage(this, 15, 15);
        }

        public void RemoveAll()
        {
            if (this.mListVec != null)
            {
                this.mListVec.Clear();
                this.mListVec = null;
            }
        }

        /// <summary>
        /// トゥイーン情報をアイコン画像に変換する処理
        /// </summary>
        /// <param name="clTween">トゥイーン情報</param>
        /// <param name="inWidth">イメージ幅</param>
        /// <param name="inHeight">イメージ高さ</param>
        /// <returns>画像</returns>
        public static Bitmap CreateImage(ClsDatTween clTween, int inWidth, int inHeight)
        {
            Pen clPen = new Pen(Color.Green);
            Bitmap clImage = new Bitmap(inWidth, inHeight);

            //以下、ペン作成処理
            Pen stPen = new Pen(Color.Red);

//このデータの取得方法だとまずい
//ちゃんと緑のラインから取得するようにする

            //以下、ライン作成処理
            float flPosX0 = (float)(ClsDatTween.POS_X0 * inWidth);
            float flPosY0 = (float)(ClsDatTween.POS_Y0 * inHeight)-1.0f;
            float flPosX1 = (float)(clTween.mPos.X * inWidth);
            float flPosY1 = (float)(clTween.mPos.Y * inHeight);
            float flPosX2 = (float)(ClsDatTween.POS_X2 * inWidth)-1.0f;
            float flPosY2 = (float)(ClsDatTween.POS_Y2 * inHeight);
            float flVecX0 = flPosX0 + (float)(clTween.mListVec[0].X * inWidth);
            float flVecY0 = flPosY0 + (float)(clTween.mListVec[0].Y * inHeight);
            float flVecX1 = flPosX1 - (float)(clTween.mListVec[1].X * inWidth);
            float flVecY1 = flPosY1 - (float)(clTween.mListVec[1].Y * inHeight);
            float flVecX2 = flPosX1 + (float)(clTween.mListVec[1].X * inWidth);
            float flVecY2 = flPosY1 + (float)(clTween.mListVec[1].Y * inHeight);
            float flVecX3 = flPosX2 - (float)(clTween.mListVec[2].X * inWidth);
            float flVecY3 = flPosY2 - (float)(clTween.mListVec[2].Y * inHeight);
            Point stPos0 = new Point((int)Math.Round(flPosX0), (int)Math.Round(flPosY0));
            Point stVec0 = new Point((int)Math.Round(flVecX0), (int)Math.Round(flVecY0));
            Point stVec1 = new Point((int)Math.Round(flVecX1), (int)Math.Round(flVecY1));
            Point stPos1 = new Point((int)Math.Round(flPosX1), (int)Math.Round(flPosY1));
            Point stVec2 = new Point((int)Math.Round(flVecX2), (int)Math.Round(flVecY2));
            Point stVec3 = new Point((int)Math.Round(flVecX3), (int)Math.Round(flVecY3));
            Point stPos2 = new Point((int)Math.Round(flPosX2), (int)Math.Round(flPosY2));
            Point[] pclListPos = { stPos0, stVec0, stVec1, stPos1, stVec2, stVec3, stPos2 };

            //以下、画像作成処理
            using (Graphics g = Graphics.FromImage(clImage))
            {
                g.Clear(Color.Transparent);
                g.DrawBeziers(stPen, pclListPos);
            }

            return (clImage);
        }

        /// <summary>
        /// クローン処理
        /// </summary>
        /// <returns>トゥイーン情報</returns>
        public ClsDatTween Clone()
        {
            ClsDatTween clTween = new ClsDatTween(this.mParam, this.mLength, this.mPos, this.mListVec);
            return (clTween);
        }

        /// <summary>
        /// 読み込み処理
        /// </summary>
        /// <param name="clXmlNode">xmlノード</param>
        public void Load(XmlNode clXmlNode)
        {
            XmlNodeList clListNode = clXmlNode.ChildNodes;
            string clParam = ClsTool.GetStringFromXmlNodeList(clListNode, "Param");
            this.mParam = (EnmParam)Enum.Parse(typeof(EnmParam), clParam);
            this.mLength = ClsTool.GetIntFromXmlNodeList(clListNode, "Length");
            this.mPos = ClsTool.GetVecFromXmlNodeList(clListNode, "Pos");

            //以下、各管理クラス作成処理
            foreach (XmlNode clNode in clListNode)
            {
                if ("Vec".Equals(clNode.Name))
                {
                    ClsVector3 clVec = ClsTool.GetVecFromXmlNode(clNode);
                    this.mListVec.Add(clVec);
                    continue;
                }
            }
        }

        /// <summary>
        /// 保存処理
        /// </summary>
        /// <param name="clName">名前</param>
        /// <param name="clHeader">ヘッダー</param>
        public void Save(string clName, string clHeader)
        {
            //以下、トゥイーン保存処理
            ClsTool.AppendElementStart(clHeader, clName);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Param", this.mParam.ToString());
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Length", this.mLength);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Pos", this.mPos);

            //以下、ベクトルリスト保存処理
            int inMax = this.mListVec.Count;
            if (inMax >= 1)
            {
                int inCnt;
                for (inCnt = 0; inCnt < inMax; inCnt++)
                {
                    ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Vec", this.mListVec[inCnt]);
                }
            }

            ClsTool.AppendElementEnd(clHeader, clName);
        }

        public void Load()
        {
        }
    }
}
