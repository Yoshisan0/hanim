using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace PrjHikariwoAnim
{
    public class ImageManagerBase
    {
        /*
            プロジェクトで使うイメージリスト
            重複ファイルは登録しない
            パスと検索用のファイル名を記録しIDで管理

            CellListはさらにその画像の一部分を表現
            CellとImage分離するか悩む
        */
        // member:

        //CellのGetSetは遠回しになるのでCellList自体を公開
        //取扱い注意
        public List<ImageChip> ImageChipList;

        // method:
        public ImageManagerBase()
        {
            ImageChipList = new List<ImageChip>();
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
        public bool Remove(string path)
        {
            var s = GetIndexFromPath(path);
            if (s < 0) return false;
            ImageChipList.RemoveAt(s);
            //CellListの該当も削除するか悩む
            return true;
        }
        public Image GetImageFromID(int ID)
        {
            return ImageChipList[ID].Img;
        }
        public void SaveToFile(string FileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImageInfo));
            //UTF-8 BOM無し
            using (Stream stm = new FileStream(FileName, FileMode.Create))
            using (BinaryWriter bw = new BinaryWriter(stm, new UTF8Encoding(false)))
            {
                //serializer.Serialize(sw, CellList);
                //案の定シリアライズ失敗する
                //ストリームに直書きするかぁ・・
                bw.Write(ImageChipList.Count);//Cell数

                foreach (ImageChip c in ImageChipList)
                {
                    c.BinaryToStream(bw);
                }
                //sw.Close();
            
            //stm.Close();
            }
            ImageChipList[0].ToXMLFile(FileName + ".xml");

        }
        public void LoadFromFile(string FileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(IList<ImageChip>));
            //UTF-8 BOM無し
            Stream stm = new  FileStream(FileName, FileMode.Open);
            StreamReader sr = new StreamReader(FileName, new UTF8Encoding(false));
            //CellList = (List<CELL>)serializer.Deserialize(sw);
            string wstr = sr.ReadLine();
            int cnt;
            if(int.TryParse(wstr,out cnt))
            {
                for (int mcnt = 0; mcnt < cnt;mcnt++)
                {
                    ImageChip c = new ImageChip();
                    c.BinaryFromStream(stm);//エラーチェックはあとで
                    ImageChipList.Add(c);
                }
            }

            sr.Close();
        }

        public void AddImageChip(ImageChip c)
        {
            
            ImageChipList.Add(c);
        }
        /// <summary>
        /// Add Cell from Cell.ImageID
        /// </summary>
        /// <param name="cell">Rectangle</param> 
        public void AddImageChipFromID(ImageChip a)
        {
            if (a.SrcID > ImageChipList.Count)
            {
                Console.Out.WriteLine("[ImageID]OutofRange");
                return;
            }
            //Cellにイメージ格納
            Bitmap srcBmp = new Bitmap(ImageChipList[a.SrcID].Img);
            Bitmap dstBmp = srcBmp.Clone(a.Rect, srcBmp.PixelFormat);
            a.Img = dstBmp;
            ImageChipList.Add(a);//追加
        }
        /// <summary>
        /// Add Cell From Image
        /// </summary>
        /// <param name="img">Image</param>
        /// <param name="cell">Rectangle</param>
        public void AddImageChipFromImage(Image img,ImageChip cell)
        {
            //重複チェック
            Bitmap srcBmp = new Bitmap(img);
            //Rectangle srcRect = new Rectangle(cell.Rect.Left,cell.Rect.Top,(cell.Rect.Right-cell.Rect.Left)-1,(cell.Rect.Bottom-cell.Rect.Top)-1); 
            Bitmap dstBmp = srcBmp.Clone(cell.Rect, srcBmp.PixelFormat);
            cell.Img = dstBmp;
            ImageChipList.Add(cell);//追加
        }

        /// <summary>
        /// PNGファイルから読み込みCELLを作成
        /// 既存CELLがあればその番号を返す
        /// </summary>
        /// <param name="path"></param>
        /// <returns>CellIndex</returns>
        public int AddCellFromPNG(string path)
        {
            //既存チェック
            int idx = GetIndexFromPath(path);
            if(idx<=0)
            {
                //it's new
                ImageChip c = new ImageChip();
                c.FromPngFile(path);
                ImageChipList.Add(c);
                idx = ImageChipList.Count;
            }
            return idx;
        }
        public void RemoveImageChip(int index)
        {
            ImageChipList.RemoveAt(index);
        }
        public void RemoveImageChipID(int imgID)
        {
            //
            for(int cnt=ImageChipList.Count;cnt > 0;cnt--)
            {
                if(ImageChipList[cnt].SrcID == imgID)
                {
                    ImageChipList.RemoveAt(cnt);
                }
            }
        }
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

        
        public int CellCount()
        {
            return ImageChipList.Count;
        }
        public ImageChip GetImageChip(int index)
        {
            if (index <= ImageChipList.Count)
            {
                return ImageChipList[index];
            }
            return null;
        }
        public ImageChip GetImageChipFromImageID(int ID)
        {
            return  ImageChipList.Find((ImageChip c) =>(c.SrcID==ID));
        }
        public ImageChip GetImageChipFromHash(int ID)
        {
            return ImageChipList.Find((ImageChip c) => (c.GetHashCode() == ID));
        }

    }

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

    //イメージリスト中のどれかの画像の一部の範囲
    [Serializable]
    public class ImageChip
    {
        public bool Selected;
        public string Path;//画像パス情報 null時は別画像(Cell)の一部を利用
        public int ID;//識別コード(hash?)
        public string Name;
        public int SrcID;//どの画像の (自身がオリジナルの場合:0)
	    public Rectangle Rect;//どの部分か(オリジナルは必ず全体を指定)
        public Bitmap Img;//汎用性と速度の為ここでも保持
        public string ImgStrBase64;//XML JSON用テスト

        public ImageChip()
        {
            SrcID = 0;
            Name = "Noname";
            Rect = new Rectangle(0, 0, 0, 0);
        }
        public ImageChip(ImageChip a)
        {
            SrcID = a.SrcID;
            Rect = a.Rect;
        }
        /// <summary>
        /// 既存CellIDの一部を登録
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rect"></param>
        public ImageChip(ImageChip srcCell,Rectangle rect)
        {
            SrcID = srcCell.ID;
            Name = "Parts";
            Rect = rect;
            //切り抜いてimageを登録
            Bitmap dst = srcCell.Img.Clone(rect,srcCell.Img.PixelFormat);
            Img = dst;
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
        public ImageChip FromPngFile(string path)
        {
            Bitmap work = new Bitmap(path);//FileLockの可能性？
            Img = work;
            SrcID = this.GetHashCode();
            Rect = new Rectangle(0, 0, work.Width, work.Height);
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
            return this; 
        }
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

        public void ToXMLFile(string fname)
        {
            ImgStrBase64 = ClsSystem.ImageToBase64(Img); 
            FileStream fs = new FileStream(fname, FileMode.Create);
            ToStreamXML(fs);
            fs.Close();
        }
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
