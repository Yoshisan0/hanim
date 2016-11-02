using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;


namespace PrjHikariwoAnim
{
    [Serializable]
    [DataContract]

    ///<summary>
    ///位置情報クラスベース
    ///親子関係や付随する上位情報は継承先か管理クラスで行う
    ///</summary>
    public class AttributeBase
    {
        //enum ImageType { Origin, Cut };
        //enum ItemType { Image, Joint, Bone };
        //enum Style { Line, Rect, Circle, Cube, Triangle };

        //位置情報クラスベース
        //親子関係や付随する上位情報は継承先か管理クラスで行う
        //xml,Json化可能にする為 public
        [DataMember]
        public int CellID;
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
        public int Transparency;//0-255
        [DataMember]
        public bool Enable;//有効無効
        [DataMember]
        public bool Visible;//みえるかどうか
        [DataMember]
        public bool Colition;//Hit判定があるかどうか
        [DataMember]
        public int Color;
        [DataMember]
        public string Text;//UserData

        private Matrix DrawMatrix;//描画用マトリックス
        private PointF[] DrawPointF3;//描画用座標配列

        //method
        public AttributeBase()
        {
            Position = new Vector3(0,0,0);
            Radius   = new Vector3(0,0,0);
            Scale    = new Vector3(1,1,1);
            Offset   = new Vector3(0,0,0);

            Enable   = true;
            Visible  = true;
            Colition = true;

        }
        public AttributeBase(AttributeBase src)
        {
            //return (AttributeBase)MemberwiseClone();
            CellID = src.CellID;
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
            Transparency = src.Transparency;
            Enable = src.Enable;
            Visible = src.Visible;
            Colition = src.Colition;
            Color = src.Color;
            Text = src.Text;
        }
        public AttributeBase Copy(AttributeBase src)
        {
            return (AttributeBase)MemberwiseClone();
        }

        public AttributeBase Clone()
        {
            return new AttributeBase(this);
        }
        public string ToJson()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AttributeBase));
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, this);
            return Encoding.UTF8.GetString(ms.ToArray());
        }
        public string ToXML()
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(AttributeBase));
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
            if (!Colition) return false;
            if (px < (Position.X - ((Width * Scale.X) / 2))*Scale.X) return false;
            if (px > (Position.X + ((Width * Scale.X) / 2))*Scale.X) return false;
            if (py < (Position.Y - ((Height* Scale.Y) / 2))*Scale.Y) return false;
            if (py > (Position.Y + ((Height* Scale.Y) / 2))*Scale.Y) return false;
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
        public void CalcPosWithRot(AttributeBase atr)
        {

        }
        /// <summary>
        /// Position Radiun(360丸め)単純加算
        /// </summary>
        /// <param name="atr"></param>
        public void Add(AttributeBase atr)
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
        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }
        public static Vector3 operator *(Vector3 v1, float f)
        {
            return new Vector3(v1.X * f, v1.Y * f, v1.Z * f);
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
        public static Vector3 Linear(Vector3 v1,Vector3 v2,float rate)
        {
            Vector3 ret = new Vector3();
            if (rate < 0.0f) rate = 0.0f;
            if (rate > 1.0f) rate = 1.0f;//clamp
            //ret = v1 * (1.0f - rate) + v2 * rate;
            ret = v1 +(v2 - v1) * rate;
            return ret;
        }
        //3次補完 Lerp
        public static Vector3 Lerp(Vector3 v1, Vector3 v2, float rate)
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
