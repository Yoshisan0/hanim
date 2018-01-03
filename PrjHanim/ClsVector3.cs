using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsVector3
    {
        public float X;
        public float Y;
        public float Z;

        public ClsVector3()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }

        public ClsVector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        //Copy
        public ClsVector3(ClsVector3 c)
        {
            this.X = c.X;
            this.Y = c.Y;
            this.Z = c.Z;
        }
        public ClsVector3 Clone()
        {
            return new ClsVector3(this);
        }
        public static ClsVector3 operator +(ClsVector3 v1, ClsVector3 v2)
        {
            return new ClsVector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static ClsVector3 operator -(ClsVector3 v1, ClsVector3 v2)
        {
            return new ClsVector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        //v2の要素0はv1を適用
        public static ClsVector3 operator /(ClsVector3 v1, ClsVector3 v2)
        {
            ClsVector3 ret = new ClsVector3();
            ret.X = (v2.X == 0) ? v1.X : (v1.X / v2.X);
            ret.Y = (v2.Y == 0) ? v1.Y : (v1.Y / v2.Y);
            ret.Z = (v2.Z == 0) ? v1.Z : (v1.Z / v2.Z);
            return ret;
        }
        //f=0はV1を返す
        public static ClsVector3 operator /(ClsVector3 v1, float f)
        {
            if (f == 0) return v1;
            return new ClsVector3(v1.X / f, v1.Y / f, v1.Z / f);
        }
        public static ClsVector3 operator /(ClsVector3 v1, double d)
        {
            if (d == 0) return v1;
            return new ClsVector3((float)(v1.X / d), (float)(v1.Y / d), (float)(v1.Z / d));
        }
        public static ClsVector3 operator *(ClsVector3 v1, ClsVector3 v2)
        {
            return new ClsVector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }
        public static ClsVector3 operator *(ClsVector3 v1, float f)
        {
            return new ClsVector3(v1.X * f, v1.Y * f, v1.Z * f);
        }
        public static ClsVector3 operator *(ClsVector3 v1, double d)
        {
            return new ClsVector3((float)(v1.X * d), (float)(v1.Y * d), (float)(v1.Z * d));
        }
        public static float Angle(ClsVector3 v1, ClsVector3 v2)
        {
            double fs = Math.Sqrt(v1.Length() * v2.Length());
            if (fs != 0f) return 0;
            return (float)Math.Acos(v1.Dot(v1, v2) / fs);
        }

        //
        public void Set(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public void Add(ClsVector3 b)
        {
            X += b.X;
            Y += b.Y;
            Z += b.Z;
        }
        public ClsVector3 Add(ClsVector3 a, ClsVector3 b)
        {
            ClsVector3 ret = new ClsVector3(a);
            ret.Add(b);
            return ret;
        }

        //Maxim
        public void Max(ClsVector3 c)
        {
            this.X = (this.X > c.X) ? this.X : c.X;
            this.Y = (this.Y > c.Y) ? this.Y : c.Y;
            this.Z = (this.Z > c.Z) ? this.X : c.Z;
        }
        //Minimum
        public void Min(ClsVector3 c)
        {
            this.X = (this.X < c.X) ? this.X : c.X;
            this.Y = (this.Y < c.Y) ? this.Y : c.Y;
            this.Z = (this.Z < c.Z) ? this.X : c.Z;
        }
        //ret:new Vector = this - B
        public void Distance(ClsVector3 b)
        {
            X -= b.X;
            Y -= b.Y;
            Z -= b.Z;
        }
        //this*Vector3(*= Vector3)
        public void Scale(ClsVector3 s)
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
        public float Dot(ClsVector3 v1, ClsVector3 v2)
        {
            return (v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z);
        }
        //外積
        public static ClsVector3 Cross(ClsVector3 v1, ClsVector3 v2)
        {
            return new ClsVector3(
            v1.Y * v2.Z - v1.Z * v2.Y,
            v1.Z * v2.X - v1.X * v2.Z,
            v1.X * v2.Y - v1.Y * v2.X);
        }

        //正規化
        public ClsVector3 Normalize()
        {
            ClsVector3 v = new ClsVector3();
            float leng = this.Length();
            if (leng != 0) v = this / leng;
            return v;
        }
        //累乗
        public float Power()
        {
            return (X * X + Y * Y + Z * Z);
        }
        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        //2点間線形補完Linear(rate 0-1)
        public static ClsVector3 Linear(ClsVector3 v1, ClsVector3 v2, double rate)
        {
            ClsVector3 ret = new ClsVector3();
            if (rate < 0.0f) rate = 0.0f;
            if (rate > 1.0f) rate = 1.0f;//clamp
            //ret = v1 * (1.0f - rate) + v2 * rate;
            ret = v1 + (v2 - v1) * rate;
            return ret;
        }
        //3次補完 Lerp
        public static ClsVector3 Lerp(ClsVector3 v1, ClsVector3 v2, double rate)
        {
            ClsVector3 ret = new ClsVector3();
            if (rate < 0.0f) rate = 0.0f;
            if (rate > 1.0f) rate = 1.0f;//clamp
            rate = rate * rate * (3.0f - 2.0f * rate);//ここの違いで色々?
            ret = v1 * (1.0f - rate) + v2 * rate;
            return ret;
        }
        //角度計算
        public static float ToAngle(ClsVector3 from, ClsVector3 to)
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
