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
        List<ImageInfo> Info;

        //CellのGetSetは遠回しになるのでCellList自体を公開
        //取扱い注意
        public List<CELL> CellList;

        // method:
        public ImageManagerBase()
        {
            Info = new List<ImageInfo>();
            CellList = new List<CELL>();
        }
        public bool SetImage(string Path)
        {
            Image img;
            ImageInfo info = new ImageInfo(Path);  

            Info.Add(info);
            return true;
        }

        //Image
        public int GetFromFileName(string FileName)
        {
            return Info.FindIndex( (ImageInfo f ) => (f.FileName == FileName) );
        }
        public int GetFromPath(string fullPath)
        {
            return Info.FindIndex((ImageInfo f) => (f.FullPath == fullPath));
        }
        public bool Remove(int ID)
        {
            if (ID >= Info.Count) return false;
            Info.RemoveAt(ID);
            //CellListの該当も自動削除するか悩む
            return true;
        }
        public bool Remove(string Filename)
        {
            var s = GetFromPath(Filename);
            if (s < 0) return false;
            Info.RemoveAt(s);
            //CellListの該当も削除するか悩む
            return true;
        }
        public Image GetImage_FromID(int ID)
        {
            return Info[ID].ImageOrg;
        }
        public void SaveImageInfo(string FileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImageInfo));
            //UTF-8 BOM無し
            StreamWriter sw = new StreamWriter(FileName, false, new UTF8Encoding(false));
            serializer.Serialize(sw,Info);
            sw.Close();
        }
        public void LoadImageInfo(string FileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(IList<ImageInfo>));
            //UTF-8 BOM無し
            StreamReader sw = new StreamReader(FileName, new UTF8Encoding(false));
            Info = (List<ImageInfo>) serializer.Deserialize(sw);
            sw.Close();
        }
        public void SaveImageCellList(string FileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImageInfo));
            //UTF-8 BOM無し
            StreamWriter sw = new StreamWriter(FileName, false, new UTF8Encoding(false));
            serializer.Serialize(sw, Info);
            sw.Close();
        }
        public void LoadImageCellList(string FileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(IList<CELL>));
            //UTF-8 BOM無し
            StreamReader sw = new StreamReader(FileName, new UTF8Encoding(false));
            CellList = (List<CELL>)serializer.Deserialize(sw);
            sw.Close();
        }

        public void AddCell(CELL c)
        {
            CellList.Add(c);
        }
        /// <summary>
        /// Add Cell from Cell.ImageID
        /// </summary>
        /// <param name="cell">Rectangle</param> 
        public void AddCellFromID(CELL a)
        {
            if (a.ImageID > Info.Count)
            {
                Console.Out.WriteLine("[ImageID]OutofRange");
                return;
            }
            //Cellにイメージ格納
            Bitmap srcBmp = new Bitmap(Info[a.ImageID].ImageOrg);
            Bitmap dstBmp = srcBmp.Clone(a.Rect, srcBmp.PixelFormat);
            a.Img = dstBmp;
            CellList.Add(a);//追加
        }
        /// <summary>
        /// Add Cell From Image
        /// </summary>
        /// <param name="img">Image</param>
        /// <param name="cell">Rectangle</param>
        public void AddCellFromImage(Image img,CELL cell)
        {
            Bitmap srcBmp = new Bitmap(img);
            //Rectangle srcRect = new Rectangle(cell.Rect.Left,cell.Rect.Top,(cell.Rect.Right-cell.Rect.Left)-1,(cell.Rect.Bottom-cell.Rect.Top)-1); 
            Bitmap dstBmp = srcBmp.Clone(cell.Rect, srcBmp.PixelFormat);
            cell.Img = dstBmp;
            CellList.Add(cell);//追加
        }
        public void RemoveCell(int index)
        {
            CellList.RemoveAt(index);
        }
        public void RemoveImageCell(int imgID)
        {
            //
            for(int cnt=CellList.Count;cnt > 0;cnt--)
            {
                if(CellList[cnt].ImageID == imgID)
                {
                    CellList.RemoveAt(cnt);
                }
            }
        }
        public void RemoveSelectedCell()
        {
            for (int cnt = CellList.Count; cnt > 0; cnt--)
            {
                if (CellList[cnt-1].Selected)
                {
                    CellList.RemoveAt(cnt-1);
                }
            }

        }

        
        public int CellCount()
        {
            return CellList.Count;
        }
        public CELL GetCell(int index)
        {
            if (index <= CellList.Count)
            {
                return CellList[index];
            }
            return null;
        }
        public CELL GetCellFromImageID(int ID)
        {
            return  CellList.Find((CELL c) =>(c.ImageID==ID));
        }
        public CELL GetCellFromHash(int ID)
        {
            return CellList.Find((CELL c) => (c.GetHashCode() == ID));
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
    public class CELL
    {
        public bool Selected;
        public int ID;//識別コード(hash?)
        public string Name;
        public int ImageID;//どの画像の
	    public Rectangle Rect;//どの部分か
        public Bitmap Img;//汎用性と速度の為ここでも保持

        public CELL()
        {
            ImageID = 0;
            Rect = new Rectangle(0, 0, 0, 0);
        }
        public CELL(CELL a)
        {
            ImageID = a.ImageID;
            Rect = a.Rect;
        }
        public CELL(int id,Rectangle rect)
        {
            ImageID = id;
            Rect = rect;
        }

        public void ToFile(string fname)
        {
            FileStream fs = new FileStream(fname,FileMode.Create);
            ToStream(fs);
            fs.Close();
        }
        public CELL FromPngFile(string path)
        {
            Bitmap work = new Bitmap(path);
            Img = work;
            ImageID = this.GetHashCode();
            Rect = new Rectangle(0, 0, work.Width, work.Height);
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
            return this; 
        }
        public void ToStream(Stream stm)
        {
            StreamWriter sw = new StreamWriter(stm);
            sw.WriteLine(ID);
            sw.WriteLine(Name);
            sw.WriteLine(ImageID);
            sw.WriteLine(Rect.X);
            sw.WriteLine(Rect.Y);
            sw.WriteLine(Rect.Width);
            sw.WriteLine(Rect.Height);

            //Byte[] buff;
            Img.Save(stm, System.Drawing.Imaging.ImageFormat.Png);
            sw.Close();
        }
        public void FromStream(Stream stm)
        {
            StreamReader sw = new StreamReader(stm);
            int.TryParse(sw.ReadLine(),out ID);
            Name = sw.ReadLine();
            int.TryParse(sw.ReadLine(),out ImageID);
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
            FileStream fs = new FileStream(fname, FileMode.Create);
            ToStreamXML(fs);
            fs.Close();
        }
        public void ToStreamXML(Stream stm)
        {
            //bitmapはシリアライズされない
            XmlSerializer xs = new XmlSerializer(typeof(CELL));
            xs.Serialize(stm, this);
        }
    }

}
