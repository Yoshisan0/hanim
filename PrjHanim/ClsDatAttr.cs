using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml;

namespace PrjHikariwoAnim
{
    [Serializable]
    [DataContract]

    ///<summary>
    ///位置情報クラスベース
    ///親子関係や付随する上位情報は継承先か管理クラスで行う
    ///</summary>
    public class ClsDatAttr
    {
        //public enum ImageType { Origin, Cut };
        //public enum ObjectType { Image, Joint, Bone };
        //public enum Style { Line, Rect, Circle, Cube, Triangle };
        
        //Rect 回転影響無視の純粋なサイズ
        //Box 回転の影響を受ける(対応はソフト次第)
        //Circle 円形(正円)
        //楕円
        //public enum CollisionStyle {Rect, Box,Circle,ellipse};//collision

        //位置情報クラスベース
        //親子関係や付随する上位情報は継承先か管理クラスで行う
        //xml,Json化可能にする為 public
        [DataMember]
        //public int CellID;
        public int ElementID;
        [DataMember]
        public int Value;
        [DataMember]
        public Vector3 Position;//LocalPosition
        [DataMember]
        public bool isX, isY, isZ;
        [DataMember]
        public Vector3 Radius;//Radius
        [DataMember]
        public bool isRX, isRY, isRZ;
        [DataMember]
        public Vector3 Scale;//Scale
        [DataMember]
        public bool isSX, isSY, isSZ;
        [DataMember]
        public Vector3 Offset;//offset
        [DataMember]
        public int Width;//work(not ImageSize)not Scaled
        [DataMember]
        public int Height;//work(not ImageSize)not Scaled
        [DataMember]
        public bool FlipH;//FlipH
        [DataMember]
        public bool FlipV;//FlipV
        [DataMember]
        public bool isTransparrency;//透明有効化フラグ
        [DataMember]
        public int Transparency;//0-100%
        [DataMember]
        public bool Enable;//有効無効
        [DataMember]
        public bool Visible;//みえるかどうか
        [DataMember]
        public bool Collision;//Hit判定があるかどうか
        [DataMember]
        public bool isColor;//カラー有効化フラグ
        [DataMember]
        public int Color;
        [DataMember]
        public int ColorRate;//0-100%
        [DataMember]
        public string Text;//UserData

        private Matrix DrawMatrix;//描画用マトリックス
        private PointF[] DrawPointF3;//描画用座標配列

        //method
        public ClsDatAttr()
        {
            Position = new Vector3(0,0,0);
            Radius   = new Vector3(0,0,0);
            Scale    = new Vector3(1,1,1);
            Offset   = new Vector3(0,0,0);
            Transparency = 100;
            Color = -1;//White 0xFFFFFFFF
            ColorRate = 100;
            Enable   = true;
            Visible  = true;
            Collision = true;
        }

        public ClsDatAttr(ClsDatAttr src)
        {
            //return (AttributeBase)MemberwiseClone();
            //CellID = src.CellID;
            ElementID = src.ElementID;
            Value = src.Value;
            Position = src.Position;
            isX = src.isX;
            isY = src.isY;
            isZ = src.isZ;
            Radius = src.Radius;
            isRX = src.isRX;
            isRY = src.isRY;
            isRZ = src.isRZ;
            Scale = src.Scale;
            isSX = src.isSX;
            isSY = src.isSY;
            isSZ = src.isSZ;
            Offset = src.Offset;
            Width = src.Width;
            Height = src.Height;
            FlipH = src.FlipH;
            FlipV = src.FlipV;
            isTransparrency = src.isTransparrency;
            Transparency = src.Transparency;
            Enable = src.Enable;
            Visible = src.Visible;
            Collision = src.Collision;
            Color = src.Color;
            ColorRate = src.ColorRate;
            isColor = src.isColor;
            Text = src.Text;
        }
        public Dictionary<string, object> Export()
        {
            Dictionary<string, object> clDic = new Dictionary<string, object>();
            //clDic["cid"] = this.CellID;
            clDic["eid"] = this.ElementID;
            clDic["val"] = this.Value;
            if (this.isX) clDic["x"] = this.Position.X;
            if (this.isY) clDic["y"] = this.Position.Y;
            if (this.isZ) clDic["z"] = this.Position.Z;
            if (this.isRX) clDic["rx"] = this.Radius.X;
            if (this.isRY) clDic["ry"] = this.Radius.Y;
            if (this.isRZ) clDic["rz"] = this.Radius.Z;
            if (this.isSX) clDic["sx"] = this.Scale.X;
            if (this.isSY) clDic["sy"] = this.Scale.Y;
            if (this.isSZ) clDic["sz"] = this.Scale.Z;
            clDic["ox"] = this.Offset.X;
            clDic["oy"] = this.Offset.Y;
            clDic["w"] = this.Width;
            clDic["h"] = this.Height;
            clDic["fh"] = this.FlipH;
            clDic["fv"] = this.FlipV;
            if (this.isTransparrency) clDic["ts"] = this.Transparency;
            clDic["ebl"] = this.Enable;
            clDic["vis"] = this.Visible;
            clDic["cll"] = this.Collision;
            if (this.isColor) clDic["c"] = this.Color;
            clDic["cr"] = this.ColorRate;
            clDic["tex"] = this.Text;

            return (clDic);
        }
        public ClsDatAttr Copy(ClsDatAttr src)
        {
            return (ClsDatAttr)MemberwiseClone();
        }

        public ClsDatAttr Clone()
        {
            return new ClsDatAttr(this);
        }
        public string ToJson()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ClsDatAttr));
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, this);
            return Encoding.UTF8.GetString(ms.ToArray());
        }
        public string ToXML()
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(ClsDatAttr));
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, this);
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        /// <summary>
        /// hit判定(world座標)
        /// </summary>
        /// <param name="px">World:X</param>
        /// <param name="py">World:Y</param>
        /// <returns></returns>
        public bool IsHit(int px,int py)
        {
            //回転考慮せず
            if (!Enable) return false;
            if (!Collision) return false;
            if (px < (Position.X - ((Width / 2)*Scale.X))) return false;
            if (px > (Position.X + ((Width / 2)*Scale.X))) return false;
            if (py < (Position.Y - ((Height / 2)*Scale.Y))) return false;
            if (py > (Position.Y + ((Height / 2)*Scale.Y))) return false;
            return true;
        }
        /// <summary>
        /// この属性で描画されるサイズを返します
        /// </summary>
        /// <returns></returns>
        public Size GetDrawSize()
        {
            return new Size((int)(Width * Scale.X), (int)(Height * Scale.Y));
        }       

        /// <summary>
        /// 入力サイズと比較し大きい数字のほう選択し返す
        /// </summary>
        /// <param name="src">SrcSize</param>
        /// <returns>Inclusion</returns>
        public Size GetInclusion(Size src)
        {
            Size ret;
            ret = GetDrawSize();
            ret.Width  = (ret.Width  > src.Width ) ? ret.Width : src.Width;
            ret.Height = (ret.Height > src.Height) ? ret.Height: src.Height;
            return ret;
        }

        /// <summary>
        /// Set後DrawRect再計算
        /// </summary>
        /// <param name="v"></param>
        public void SetPosition(Vector3 v)
        {
            Position = v;
            //CalcDrawRect
            CalcRot(Radius.X,0,0,1);
        }
        public void CalcRot(float angle,int offx,int offy,float zoom)
        {
            Matrix mat = new Matrix(new RectangleF(Position.X,Position.Y,Width,Height),DrawPointF3);
            mat.Scale(Scale.X + zoom,Scale.Y + zoom);
            mat.RotateAt(angle, new PointF(Width / 2, Height / 2));
            mat.Translate(offx, offy);

            //Rectangle ans = new Rectangle((int)Position.X, (int)Position.Y,(int) (Width*Scale.X),(int)( Height*Scale.Y));
            PointF[] ret = new PointF[3]; ;
            ret[0] = PointFRotate(0,0,Position.X,Position.Y,angle);
            ret[1] = PointFRotate(Width, 0,Position.X,Position.Y,angle);
            ret[2] = PointFRotate(Width, Height, Position.X, Position.Y, angle);
            //ret[3] = PointFRotate(0, Height, Position.X, Position.Y, angle);
            //= ret;
        }
        public PointF PointFRotate(float x,float y,float offx,float offy, float angle)
        {
            PointF ans = new PointF();
            ans.X = (int)(x * (float)Math.Cos(angle) - y * (float)Math.Sin(angle)) + offx;
            ans.Y = (int)(x * (float)Math.Sin(angle) + y * (float)Math.Cos(angle)) + offy;
            return ans;
        }
        public Matrix GetMatrix_X(float offx=0,float offy=0,float zoom=1)
        {
            Matrix mat = new Matrix();
            mat.Translate(Width / 2, Height / 2);
            mat.Scale(Scale.X * zoom, Scale.Y * zoom);
            mat.RotateAt(Radius.X,new Point(0,0));
            //mat.RotateAt(Radius.X, new PointF(Position.X, Position.Y));
            //mat.Translate(offx, offy);
            DrawMatrix = mat;
            return mat;
        }
        /// <summary>
        /// 指定属性を親とした計算
        /// </summary>
        /// <param name="atr">計算元とする属性</param>
        public void CalcPosWithRot(ClsDatAttr atr)
        {

        }
        /// <summary>
        /// Position Radiun(360丸め)単純加算
        /// </summary>
        /// <param name="atr"></param>
        public void Add(ClsDatAttr atr)
        {
            Position += atr.Position;
            Radius 　+= atr.Radius;
        }
        public void AddPosition(Vector3 vec)
        {
            Position += vec;
        }
        public void SubPosition(Vector3 vec)
        {
            Position -= vec;
        }
        public void AddRadius(Vector3 vec)
        {
            Radius += vec;
            Radius.X %= 360f;
            Radius.Y %= 360f;
            Radius.Z %= 360f; 
        }

        /// <summary>
        /// 読み込み処理
        /// </summary>
        /// <param name="clXmlNode">xmlノード</param>
        public void Load(XmlNode clXmlNode)
        {
            XmlNodeList clListNode = clXmlNode.ChildNodes;
            this.ElementID = ClsTool.GetIntFromXmlNodeList(clListNode, "ElemID");
            this.Value = ClsTool.GetIntFromXmlNodeList(clListNode, "Val");
            this.Position = ClsTool.GetVecFromXmlNodeList(clListNode, "Pos");
            this.isX = ClsTool.GetBoolFromXmlNodeList(clListNode, "isX");
            this.isY = ClsTool.GetBoolFromXmlNodeList(clListNode, "isY");
            this.isZ = ClsTool.GetBoolFromXmlNodeList(clListNode, "isZ");
            this.Radius = ClsTool.GetVecFromXmlNodeList(clListNode, "Rad");
            this.isRX = ClsTool.GetBoolFromXmlNodeList(clListNode, "isRX");
            this.isRY = ClsTool.GetBoolFromXmlNodeList(clListNode, "isRY");
            this.isRZ = ClsTool.GetBoolFromXmlNodeList(clListNode, "isRZ");
            this.Scale = ClsTool.GetVecFromXmlNodeList(clListNode, "Scl");
            this.isSX = ClsTool.GetBoolFromXmlNodeList(clListNode, "isSX");
            this.isSY = ClsTool.GetBoolFromXmlNodeList(clListNode, "isSY");
            this.isSZ = ClsTool.GetBoolFromXmlNodeList(clListNode, "isSZ");
            this.Offset = ClsTool.GetVecFromXmlNodeList(clListNode, "Offset");
            this.Width = ClsTool.GetIntFromXmlNodeList(clListNode, "W");
            this.Height = ClsTool.GetIntFromXmlNodeList(clListNode, "H");
            this.FlipH = ClsTool.GetBoolFromXmlNodeList(clListNode, "FlipH");
            this.FlipV = ClsTool.GetBoolFromXmlNodeList(clListNode, "FlipV");
            this.isTransparrency = ClsTool.GetBoolFromXmlNodeList(clListNode, "isTrans");
            this.Transparency = ClsTool.GetIntFromXmlNodeList(clListNode, "Trans");
            this.Enable = ClsTool.GetBoolFromXmlNodeList(clListNode, "Enable");
            this.Visible = ClsTool.GetBoolFromXmlNodeList(clListNode, "Visible");
            this.Collision = ClsTool.GetBoolFromXmlNodeList(clListNode, "Collision");
            this.isColor = ClsTool.GetBoolFromXmlNodeList(clListNode, "isColor");
            this.Color = ClsTool.GetIntFromXmlNodeList(clListNode, "Color");
            this.ColorRate = ClsTool.GetIntFromXmlNodeList(clListNode, "ColorRate");
            this.Text = ClsTool.GetStringFromXmlNodeList(clListNode, "Text");
        }

        /// <summary>
        /// 保存処理
        /// </summary>
        /// <param name="clHeader">ヘッダー</param>
        public void Save(string clHeader)
        {
            //以下、アトリビュート保存処理
            ClsTool.AppendElementStart(clHeader, "Attr");
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "ElemID", this.ElementID);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Val", this.Value);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Pos", this.Position);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "isX", this.isX);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "isY", this.isY);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "isZ", this.isZ);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Rad", this.Radius);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "isRX", this.isRX);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "isRY", this.isRY);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "isRZ", this.isRZ);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Scl", this.Scale);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "isSX", this.isSX);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "isSY", this.isSY);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "isSZ", this.isSZ);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Offset", this.Offset);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "W", this.Width);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "H", this.Height);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "FlipH", this.FlipH);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "FlipV", this.FlipV);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "isTrans", this.isTransparrency);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Trans", this.Transparency);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Enable", this.Enable);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Visible", this.Visible);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Collision", this.Collision);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "isColor", this.isColor);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Color", this.Color);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "ColorRate", this.ColorRate);
            ClsTool.AppendElement(clHeader + ClsSystem.FILE_TAG, "Text", this.Text);
            ClsTool.AppendElementEnd(clHeader, "Attr");
        }
    }

    //floatで誤差でるかなぁ・・9/20 amami
    //うーん・・・まぁいいか

    [Serializable]
    public class Vector3
    {
        [DataMember]
        public float X;
        [DataMember]
        public float Y;
        [DataMember]
        public float Z;

        public Vector3()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }
        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        //Copy
        public Vector3(Vector3 c)
        {
            this.X = c.X;
            this.Y = c.Y;
            this.Z = c.Z;
        }
        public Vector3 Clone()
        {
            return new Vector3(this);
        }
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        //v2の要素0はv1を適用
        public static Vector3 operator /(Vector3 v1,Vector3 v2)
        {
            Vector3 ret = new Vector3();
            ret.X = (v2.X == 0) ? v1.X :( v1.X / v2.X);
            ret.Y = (v2.Y == 0) ? v1.Y : (v1.Y / v2.Y);
            ret.Z = (v2.Z == 0) ? v1.Z : (v1.Z / v2.Z);
            return ret;
        }
        //f=0はV1を返す
        public static Vector3 operator /(Vector3 v1,float f)
        {
            if (f == 0) return v1;
            return new Vector3(v1.X/f,v1.Y/f,v1.Z/f);
        }
        public static Vector3 operator /(Vector3 v1,double d)
        {
            if (d == 0) return v1;
            return new Vector3((float)(v1.X / d), (float)(v1.Y / d),(float) (v1.Z / d));
        }
        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }
        public static Vector3 operator *(Vector3 v1, float f)
        {
            return new Vector3(v1.X * f, v1.Y * f, v1.Z * f);
        }
        public static Vector3 operator *(Vector3 v1, double d)
        {
            return new Vector3((float)(v1.X * d),(float) (v1.Y * d), (float)(v1.Z * d));
        }
        public static float Angle(Vector3 v1,Vector3 v2)
        {
            double fs = Math.Sqrt(v1.Length() * v2.Length());
            if(fs != 0f)return 0;
            return (float)Math.Acos(v1.Dot(v1,v2) / fs);
        }

        //
        public void Set(float x,float y,float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public void Add(Vector3 b)
        {
            X += b.X;
            Y += b.Y;
            Z += b.Z;
        }
        public Vector3 Add(Vector3 a,Vector3 b)
        {
            Vector3 ret =  new Vector3(a);
            ret.Add(b);
            return ret;
        }

        //Maxim
        public void Max(Vector3 c)
        {
            this.X = (this.X > c.X) ? this.X : c.X;
            this.Y = (this.Y > c.Y) ? this.Y : c.Y;
            this.Z = (this.Z > c.Z) ? this.X : c.Z;
        }
        //Minimum
        public void Min(Vector3 c)
        {
            this.X = (this.X < c.X) ? this.X : c.X;
            this.Y = (this.Y < c.Y) ? this.Y : c.Y;
            this.Z = (this.Z < c.Z) ? this.X : c.Z;
        }
        //ret:new Vector = this - B
        public void Distance(Vector3 b)
        {
            X -= b.X;
            Y -= b.Y;
            Z -= b.Z;
        }
        //this*Vector3(*= Vector3)
        public void Scale(Vector3 s)
        {
            X *= s.X;
            Y *= s.Y;
            Z *= s.Z;
        }
        //this*float(*= float)
        public void Scale(float s)
        {
            X *= s;
            Y *= s;
            Z *= s;
        }
        //内積
        public float Dot(Vector3 v1,Vector3 v2)
        {
            return (v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z);
        }
        //外積
        public static Vector3 Cross(Vector3 v1,Vector3 v2)
        {
            return new Vector3(
            v1.Y * v2.Z - v1.Z * v2.Y,
            v1.Z * v2.X - v1.X * v2.Z,
            v1.X * v2.Y - v1.Y * v2.X);
        }

        //正規化
        public Vector3 Normalize()
        {
            Vector3 v = new Vector3();
            float leng = this.Length();
            if(leng!=0)  v = this / leng;           
            return v;
        }
        //累乗
        public float Power()
        {
            return (X * X + Y * Y + Z * Z);
        }
        public float Length()
        {
            return (float)Math.Sqrt(X*X + Y*Y + Z*Z);
        }

        //2点間線形補完Linear(rate 0-1)
        public static Vector3 Linear(Vector3 v1,Vector3 v2,double rate)
        {
            Vector3 ret = new Vector3();
            if (rate < 0.0f) rate = 0.0f;
            if (rate > 1.0f) rate = 1.0f;//clamp
            //ret = v1 * (1.0f - rate) + v2 * rate;
            ret = v1 +(v2 - v1) * rate;
            return ret;
        }
        //3次補完 Lerp
        public static Vector3 Lerp(Vector3 v1, Vector3 v2, double rate)
        {
            Vector3 ret = new Vector3();
            if (rate < 0.0f) rate = 0.0f;
            if (rate > 1.0f) rate = 1.0f;//clamp
            rate=rate * rate * (3.0f - 2.0f * rate);//ここの違いで色々?
            ret = v1 * (1.0f - rate) + v2 * rate;
            return ret;
        }
        //角度計算
        public static float ToAngle(Vector3 from,Vector3 to)
        {
            //note: Degress = radians * (180/Math.PI);
            //2D θ=arcTan(x,y)
            //3D r=sqrt(x*x+y*y+z*z)
            //   θ=arcTan(sqrt(x*x+y*y),z)
            //   Φ=arcTan(y,x)
            
            //まだ
            return 0;
        }
        //回転
        public static void Rotate(float Radian)
        {

        }

        public String ToString2()
        {
            return $"{X},{Y},{Z},";
        }
    }

}
