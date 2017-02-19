using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Collections;

namespace PrjHikariwoAnim
{
    public class ImageManagerBase
    {
        /*
         取説:
            プロジェクトで使うイメージリスト
            重複ファイルは登録しない
            パスと検索用のファイル名を記録しIDで管理

            private List<ImageChip>ImageChipList
            class:ImageChip:画像データ(参照カウンタ完備)

            SAVE&LOADについて
            ImageChipListをXMLシリアライズにてストリームで読み書き

            
        */
        // member:

        //2017 1/17 やっぱ危なそうなので　privateに変更
        private List<ImageChip> ImageChipList;
        //private Hashtable ImageHashTable;//<MD5,Image> 画像重複チェック用

        // method:
        public ImageManagerBase()
        {
            ImageChipList = new List<ImageChip>();
            //ImageHashTable = new Hashtable();//<MD5,Image>
        }

        //Image
        public int GetIndexFromPath(string fullPath)
        {
            return ImageChipList.FindIndex((ImageChip f) => (f.Path == fullPath));
        }
        public bool Remove(int ID)
        {
            if (ID >= ImageChipList.Count) return false;
            ImageChipList.RemoveAt(ID);
            //CellListの該当も自動削除するか悩む

            return true;
        }
        /// <summary>
        /// psthからの削除
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool Remove(string path)
        {
            var s = GetIndexFromPath(path);
            if (s < 0) return false;
            if(ImageChipList[s].refCountDown(1))
            {
                //参照0以下
                ImageChipList.RemoveAt(s);
                return true;
            }
            //他参照在り
            return false;
        }
        public void RemoveAll()
        {
            ImageChipList.Clear();
            //ImageHashTable.Clear();
        }
        /*
        public Image GetImageFromIndex(int index)
        {
            return ImageChipList[index].Img;
        }
        */
        /// <summary>
        /// イメージリストの保存
        /// </summary>
        /// <param name="FileName">ファイル名</param>
        /// <param name="ext">独自拡張子等</param>
        public void SaveToFile(string FileName,string ext)
        {
            //privateはシリアライズ不可なので配列に入れ直し
            ImageChip[] il = ImageChipList.ToArray();
            XmlSerializer serializer = new XmlSerializer(typeof(ImageChip[]));
            //スキーマを無しに
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(String.Empty, String.Empty);

            //UTF8 Bom無
            using (StreamWriter sw = new StreamWriter(FileName+ext, false, new System.Text.UTF8Encoding(false)))
            {
                serializer.Serialize(sw, il);
                sw.Close();
            }
        }
        public void ToStream(Stream stm)
        {

        }
        public void LoadFromFile(string FileName,string ext)
        {
            ImageChip[] work;
            XmlSerializer serializer = new XmlSerializer(typeof(IList<ImageChip[]>));
            //UTF-8 BOM無し
            using (StreamReader sr = new StreamReader(FileName+ext, new UTF8Encoding(false)))
            {
                work = (ImageChip[])serializer.Deserialize(sr);
                sr.Close();
                //イメージ再読み込みとmd5生成
                foreach(ImageChip ic in work)
                {
                    ic.FromPath(ic.Path,false);
                }
                //仮配列から戻し
                ImageChipList.Clear();
                ImageChipList.AddRange(work);
            }
        }

        /// <summary>
        /// ImageChipを追加します　既存があれば帰り値として既存ImageChipが帰ります
        /// </summary>
        /// <param name="c"></param>
        /// <returns>既存のImageChip</returns>
        public ImageChip AddImageChip(ImageChip c)
        {
            ImageChip retChip = GetImageChipFromMD5(c.StrMD5);
            if(retChip==null)
            {
                //新規
                c.ID = c.GetHashCode();
                c.refCountUp(1);
                ImageChipList.Add(c);
                retChip = c;
            }
            else
            {
                //既存
                retChip.refCountUp(1);//参照カウントアップ
            }
            return retChip;
        }

        /// <summary>
        /// イメージからchip情報のrect範囲を抜き出し追加 Add Chip.Rect From Image
        /// </summary>
        /// <param name="img">Image</param>
        /// <param name="chip">Rectangle</param>
        public void AddImageChipFromImage(Image img,ImageChip chip)
        {
            Bitmap srcBmp = new Bitmap(img);
            //Rectangle srcRect = new Rectangle(cell.Rect.Left,cell.Rect.Top,(cell.Rect.Right-cell.Rect.Left)-1,(cell.Rect.Bottom-cell.Rect.Top)-1); 
            Bitmap dstBmp = srcBmp.Clone(chip.Rect, srcBmp.PixelFormat);
            chip.Img = dstBmp;
            chip.StrMD5 = ClsSystem.GetMD5FromImage(dstBmp);
            chip.Name = "Cut:" + chip.Name;
            AddImageChip(chip);//追加
        }
        /// <summary>
        /// イメージが既存かを確認
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public bool isImageStored(Image img)
        {
            ImageChip work = GetImageChipFromHash(img.GetHashCode());
            return work!=null;// ImageHashTable.ContainsValue(img);
        }
        /// <summary>
        /// MD5が既存かを確認
        /// </summary>
        /// <param name="md5"></param>
        /// <returns></returns>
        public bool isImageSrtoredMD5(string md5)
        {
            ImageChip work = GetImageChipFromMD5(md5);
            return work!=null;//ImageHashTable.ContainsKey(md5);
        }

        /// <summary>
        /// PNGファイルから読み込みCELLを作成
        /// 既存CELLがあればその番号を返す
        /// </summary>
        /// <param name="path">イメージパス</param>
        /// <returns>CellIndex</returns>
        public int AddCellFromPath(string path)
        {
            //既存チェック
            int idx = GetIndexFromPath(path);
            if(idx<=0)
            {
                //it's new
                ImageChip c = new ImageChip();
                c.FromPath(path,true);
                ImageChipList.Add(c);
                idx = ImageChipList.Count;
            }

            return idx;
        }

        /// <summary>
        /// PNGファイルから読み込みCELLを作成
        /// 既存CELLがあっても追加するので注意（エラー画像読み込みに利用している）
        /// </summary>
        /// <param name="clBmp">イメージ管理クラス</param>
        /// <returns>CellIndex</returns>
        public int AddCellFromImage(Bitmap clBmp)
        {
            //it's new
            ImageChip c = new ImageChip();
            c.FromImage("", clBmp, true);
            ImageChipList.Add(c);
            int idx = ImageChipList.Count;

            return idx;
        }

        /// <summary>
        /// index番号で削除
        /// </summary>
        /// <param name="index">Index</param>
        public void RemoveImageChip(int index)
        {
            ImageChipList.RemoveAt(index);
        }
        /// <summary>
        /// imgID(hash)で削除
        /// </summary>
        /// <param name="imgID">int imgID(hash)</param>
        public void RemoveImageChipID(int imgID)
        {
            //
            for(int cnt=ImageChipList.Count-1;cnt >= 0;cnt--)
            {
                if(ImageChipList[cnt].SrcID == imgID)
                {
                    ImageChipList.RemoveAt(cnt);
                }
            }
        }
        /// <summary>
        /// md5で削除
        /// </summary>
        /// <param name="md5">string</param>
        public void RemoveImageChipMD5(string md5)
        {
            //
            for (int cnt = ImageChipList.Count-1; cnt >= 0; cnt--)
            {
                if (ImageChipList[cnt].StrMD5 == md5)
                {
                    ImageChipList.RemoveAt(cnt);
                }
            }
        }
        /// <summary>
        /// selectフラグがtrueな物をすべて削除
        /// </summary>
        public void RemoveSelectedCell()
        {
            for (int cnt = ImageChipList.Count; cnt > 0; cnt--)
            {
                if (ImageChipList[cnt-1].Selected)
                {
                    ImageChipList.RemoveAt(cnt-1);
                }
            }
        }
        
        /// <summary>
        /// ImageChip登録数取得
        /// </summary>
        /// <returns></returns>
        public int ChipCount()
        {
            return ImageChipList.Count;
        }
        public ImageChip[] ToArray()
        {
            return ImageChipList.ToArray();
        }

        /// <summary>
        /// 全てのImageChipListを再読込する
        /// </summary>
        public void RestoreIamgeList()
        {
            foreach(ImageChip ic in ImageChipList)
            {
                ic.RestoreImage();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public void AddArray(ImageChip[] icl)
        {
            if (icl == null) return;
            ImageChipList.AddRange(icl);
        }
        public ImageChip GetImageChipFromIndex(int idx)
        {
            if (idx < 0 || idx >= ImageChipList.Count) return null;
            return ImageChipList[idx];
        }
        public ImageChip GetImageChipFromID(int ID)
        {
            return  ImageChipList.Find((ImageChip c) =>(c.ID==ID));
        }
        public ImageChip GetImageChipFromHash(int ID)
        {
            return ImageChipList.Find((ImageChip c) => (c.GetHashCode() == ID));
        }
        public ImageChip GetImageChipFromMD5(string md5)
        {
            return ImageChipList.Find((ImageChip c) => (c.StrMD5 == md5));
        }
    }

    //未使用　なぜ作ったのか・・・
    [Serializable]
    public class ImageInfo
    {
        public string FullPath;//With out Filename
        public string Directory;//
        public string FileName;//***.***
        public string Name;//With out Extis

        public Image ImageOrg;

        public ImageInfo() { }
        /// <summary>
        /// fullpPathから各要素を分解し格納
        /// </summary>
        /// <param name="fullPath"></param>
        public ImageInfo(string fullPath)
        {
            using (FileStream fs = File.OpenRead(fullPath))
            using (ImageOrg = Image.FromStream(fs))

            FullPath = fullPath;
            Directory = Path.GetDirectoryName(fullPath);
            FileName = Path.GetFileName(fullPath);
            Name = Path.GetFileNameWithoutExtension(fullPath);
        }
    }

    //ImageChip
    //イメージ情報
    //イメージや他イメージの一部の範囲
    [Serializable]
    [XmlRoot("Imagechip")]
    public class ImageChip
    {

        [XmlAttribute("src")]
        public int SrcID;//どの画像の (自身がオリジナルの場合:0)
        [XmlAttribute("ref")]
        private int Refcount;//参照カウンタ　ImageManagerへの追加と削除時に更新
        [XmlAttribute("id")]
        public int ID;//識別コード(hash?)
        [XmlAttribute("name")]
        public string Name;
        [XmlText]
        public string Path;//画像パス情報 null時は別画像(Cell)の一部を利用
        public Rectangle Rect;//どの部分か(オリジナルは必ず全体を指定)

        [XmlIgnore]
        public bool Selected;//
        [XmlIgnore]
        public string StrMD5;//ImageからのMD5 重複厳重チェック用
        [XmlIgnore]
        public Bitmap Img;//汎用性と速度の為ここでも保持
        [XmlIgnore]
        public string ImgStrBase64;//XML JSON用テスト

        public ImageChip()
        {
            SrcID = 0;
            Name = "Noname";
            StrMD5 = "";
            Path = "";
            Rect = new Rectangle(0, 0, 0, 0);
            Refcount = 0;//参照カウンタ
        }

        /// <summary>
        /// 既存ImageChipの一部を登録
        /// </summary>
        /// <param name="srcChip"></param>
        /// <param name="rect"></param>
        public Bitmap ImageCut(ImageChip srcChip, Rectangle rect)
        {
            this.SrcID = srcChip.ID;
            this.Name = "Parts";
            this.Rect = rect;
            //切り抜いてimageを登録
            Bitmap dst = srcChip.Img.Clone(rect, srcChip.Img.PixelFormat);
            this.StrMD5 = ClsSystem.GetMD5FromImage(dst);
            this.Img = dst;
            this.ID = this.GetHashCode();
            return dst;
        }
        public ImageChip(Image img)
        {
            SrcID = this.GetHashCode();
            Name = "";
            Rect = new Rectangle(0, 0, img.Width, img.Height);
            Img = (Bitmap)img;
            StrMD5 = ClsSystem.GetMD5FromImage(img);
        }

        //参照カウンタ回り
        /// <summary>
        /// 参照カウンタ
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public bool refCountUp(int i)
        {
            Refcount += i;
            return true;
        }
        public bool refCountDown(int i)
        {
            Refcount -= i;
            if (Refcount <= 0) return false;
            return true;
        }
        //おもいっきり使わないきがするがまぁ・・
        public int getRefCount()
        {
            return Refcount;
        }
        
        public Dictionary<string, object> Export()
        {
            Dictionary<string, object> clDic = new Dictionary<string, object>();
            clDic["id"] = this.ID;
            clDic["nm"] = this.Name;
            clDic["sel"] = this.Selected;
            clDic["sid"] = this.SrcID;
            clDic["x"] = this.Rect.X;
            clDic["y"] = this.Rect.Y;   //上下ひっくり返さないとダメかも？
            clDic["w"] = this.Rect.Width;
            clDic["h"] = this.Rect.Height;
            clDic["img"] = this.ImgStrBase64;

            return (clDic);
        }

        /// <summary>
        /// *Cell単体を出力する必要がある場合のみ使用
        /// </summary>
        /// <param name="fname"></param>
        public void ToBinaryFile(string fname)
        {
            FileStream fs = new FileStream(fname, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            BinaryToStream(bw);
            fs.Close();
            
        }

        //事前にImageManagerレベルで重複チェックすること
        //通常はImagemanager.AddPngFileを使う事！
        /// <summary>
        /// pathからファイルを読み込む
        /// </summary>
        /// <param name="path">filePath</param>
        /// <param name="hashset">SrcIDにhashを入れるか指定</param>
        /// <returns></returns>
        public ImageChip FromPath(string path,bool hashset)
        {
            Bitmap work = new Bitmap(path);//FileLockの可能性？
            this.FromImage(path, work, hashset);
            return this ;
        }

        /// <summary>
        /// イメージ管理クラスを設定する
        /// </summary>
        /// <param name="path">filepath</param>
        /// <param name="clBmp">イメージ管理クラス</param>
        /// <param name="hashset">SrcIDにhashを入れるか指定</param>
        /// <returns></returns>
        public ImageChip FromImage(string path, Bitmap clBmp, bool hashset)
        {
            Img = clBmp;
            StrMD5 = ClsSystem.GetMD5FromImage(clBmp);
            Path = path;
            //重複チェック
            if (hashset) SrcID = this.GetHashCode();
            Rect = new Rectangle(0, 0, clBmp.Width, clBmp.Height);
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
            return this;
        }

        /// <summary>
        /// Pathからイメージの再読込
        /// </summary>
        public void RestoreImage()
        {
            this.FromPath(this.Path,false);
            //部分取込
            if (Img.Width != Rect.Width || Img.Height != Rect.Height)
            {
                this.Img = this.ImageCut(this, this.Rect);
            }
        }

        /// <summary>
        /// ImaghChipのバイナリ書込
        /// </summary>
        /// <param name="bw">BinaryWriter</param>
        public void BinaryToStream(BinaryWriter bw)
        {
                bw.Write(ID);
                bw.Write(Name);
                bw.Write(SrcID);//hashの重複あり得る？ならLoad時再割り当てするか？
                bw.Write(Rect.X);
                bw.Write(Rect.Y);
                bw.Write(Rect.Width);
                bw.Write(Rect.Height);
                Img.Save(bw.BaseStream, System.Drawing.Imaging.ImageFormat.Png);         
        }
        /// <summary>
        /// ImageChipのバイナリ読出
        /// </summary>
        /// <param name="stm"></param>
        public void BinaryFromStream(Stream stm)
        {
            StreamReader sw = new StreamReader(stm);
            int.TryParse(sw.ReadLine(),out ID);
            Name = sw.ReadLine();
            int.TryParse(sw.ReadLine(),out SrcID);
            int work;
            int.TryParse(sw.ReadLine(), out work);
            Rect.X = work;
            int.TryParse(sw.ReadLine(), out work);
            Rect.Y = work;
            int.TryParse(sw.ReadLine(), out work);
            Rect.Width = work;
            int.TryParse(sw.ReadLine(), out work);
            Rect.Height = work;

            //Byte[] buff;

            Img.Save(stm, System.Drawing.Imaging.ImageFormat.Png);
            sw.Close();
        }   
        /// <summary>
        /// ImageChipのXML保存
        /// </summary>
        /// <param name="fname"></param>
        public void ToXMLFile(string fname)
        {
            ImgStrBase64 = ClsSystem.ImageToBase64(Img); 
            FileStream fs = new FileStream(fname, FileMode.Create);
            ToStreamXML(fs);
            fs.Close();
        }
        /// <summary>
        /// ImageChipのXML読出
        /// </summary>
        /// <param name="stm"></param>
        public void ToStreamXML(Stream stm)
        {
            //bitmapはシリアライズされない
            XmlSerializer xs = new XmlSerializer(typeof(ImageChip));
            xs.Serialize(stm, this);
            //imgを元に戻す
            this.Img = ClsSystem.ImageFromBase64(this.ImgStrBase64);
        }
    }

}
