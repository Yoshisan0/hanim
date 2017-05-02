using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace PrjHikariwoAnim
{
    public partial class ComponentOpenGL : UserControl
    {
        public float mCenterX;
        public float mCenterY;
        public float mScale;
        public float mCanvasWidth;
        public float mCanvasHeight;
        public bool mCrossBarVisible;
        public bool mGridVisible;
        public float mGridSpan;

        public uint[] texture;

        #region Fields
        //
        // Fields
        //
        private IntPtr hRC = IntPtr.Zero;
  		private IntPtr hDC = IntPtr.Zero;
  
  		#endregion

        public ComponentOpenGL()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 生成時の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
  		{
  			base.OnHandleCreated(e);
  
  			this.SetStyle(ControlStyles.UserPaint, true);
  			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
  			this.SetStyle(ControlStyles.DoubleBuffer, false);
  			this.SetStyle(ControlStyles.Opaque, true);
  			this.SetStyle(ControlStyles.ResizeRedraw, true);
  
  			SetupPixelFormat();	//ピクセルフォーマットの設定
  			SetupOpenGL();      //OpenGLの初期設定



            /*
            texture = new uint[6];

            Bitmap image = new Bitmap("test2.png");
            image.RotateFlip(RotateFlipType.RotateNoneFlipY);
            System.Drawing.Imaging.BitmapData bitmapdata;
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            Gl.glGenTextures(1, texture);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[0]);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, (int)Gl.GL_RGB8, image.Width, image.Height, 0, Gl.GL_BGR_EXT, Gl.GL_UNSIGNED_BYTE, bitmapdata.Scan0);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);   // Gl.GL_POINT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);   // Gl.GL_POINT);
            */
        }

  		/// <summary>
  		/// ピクセルフォーマットの設定をする
  		/// </summary>
  		private void SetupPixelFormat()
  		{
  			//PIXELFORMATDESCRIPTORの設定
  			Gdi.PIXELFORMATDESCRIPTOR pfd = new Gdi.PIXELFORMATDESCRIPTOR();
  			pfd.dwFlags = Gdi.PFD_SUPPORT_OPENGL |
  				Gdi.PFD_DRAW_TO_WINDOW |
  				Gdi.PFD_DOUBLEBUFFER;
  			pfd.iPixelType = Gdi.PFD_TYPE_RGBA;
  			pfd.cColorBits = 32;
  			pfd.cAlphaBits = 8;
  			pfd.cDepthBits = 16;
  
  			//デバイスコンテキストハンドルの収録
  			this.hDC = User.GetDC(this.Handle);
  
  			//ピクセルフォーマットを選択
  			int pixelFormat = Gdi.ChoosePixelFormat(this.hDC, ref pfd);
  			if (pixelFormat == 0)
  				throw new Exception("Error: Cant't Find A Suitable PixelFormat.");
  
  			//ピクセルフォーマットを設定
  			if (!Gdi.SetPixelFormat(this.hDC, pixelFormat, ref pfd))
  				throw new Exception("Error: Cant't Set The PixelFormat."); 
  
  			//レンダリングコンテキストを生成
  			this.hRC = Wgl.wglCreateContext(this.hDC);
  			if (this.hRC == IntPtr.Zero)
  				throw new Exception("Error: Cant Create A GLRendering Context.");
  
  			//レンダリングコンテキストをカレントにする
  			Wgl.wglMakeCurrent(this.hDC, this.hRC);
  
  			//GLエラーのチェック
  			int err = Gl.glGetError();
  			if (err != Gl.GL_NO_ERROR)
  				throw new Exception("GL Error:" + err.ToString());
  
  		}
  
  		/// <summary>
  		/// OpenGLの初期設定
  		/// </summary>
  		private void SetupOpenGL()
  		{
  			//バッファをクリアする色
  			Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
  
  			//深度テストを有効
  			Gl.glEnable(Gl.GL_DEPTH_TEST);
  			Gl.glDepthFunc(Gl.GL_LEQUAL);
  
  			//スムースシェイディング
  			Gl.glShadeModel(Gl.GL_SMOOTH);

            //以下、テクスチャー表示有効化設定
            Gl.glEnable(Gl.GL_TEXTURE_2D);

            //以下、初期化処理
            this.mCenterX = 0;
            this.mCenterY = 0;
            this.mScale = 1.0f;
            this.mCanvasWidth = this.Width;
            this.mCanvasHeight = this.Height;
            this.mCrossBarVisible = true;
            this.mGridVisible = true;
            this.mGridSpan = 16.0f;
        }
  
  		/// <summary>
  		/// 後片付け
  		/// </summary>
  		private void ReleaseOpenGL()
  		{
  			Wgl.wglMakeCurrent(this.hDC, IntPtr.Zero);
  			Wgl.wglDeleteContext(this.hRC);
  		}

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            this.mCanvasWidth = this.Width;
            this.mCanvasHeight = this.Height;
            int inWidth = this.Width;
            int inHeight = this.Height;

            //レンダリングコンテキストをカレントにする
            Wgl.wglMakeCurrent(this.hDC, this.hRC);

            //バッファをクリア
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            //以下、ビューポートの設定
            Gl.glViewport(0, 0, inWidth, inHeight);
            /*
            if (this.Width > this.Height)
            {
                int inY = -(this.Width - this.Height) / 2;
                Gl.glViewport(0, inY, this.Width, this.Width);
            }
            else
            {
                int inX = -(this.Height - this.Width) / 2;
                Gl.glViewport(inX, 0, this.Height, this.Height);
            }
            */

            //射影行列の設定
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(-inWidth / 2, inWidth / 2, -inHeight / 2, inHeight / 2, -1.0, 1.0);

            //モデルビュー行列の設定
            //Gl.glMatrixMode(Gl.GL_MODELVIEW);
            //Gl.glLoadIdentity();

            //以下、グリッドライン描画処理
            if (this.mGridVisible)
            {
                this.DrawGridLine(Color.Green, this.mGridSpan * this.mScale);
            }

            //以下、中心ライン描画処理
            if (this.mCrossBarVisible)
            {
                this.DrawCrossBarLine(Color.Red);
            }

            //以下、各エレメント表示処理

            /*
            //以下、矩形ライン描画テスト（グリーンの矩形）
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);

            Gl.glBegin(Gl.GL_LINE_STRIP);

            Gl.glColor3f(0.0f, 1.0f, 0.0f);
            Gl.glVertex3f(10, 10, 0.0f);
            Gl.glVertex3f(10, 200, 0.0f);
            Gl.glVertex3f(200, 200, 0.0f);
            Gl.glVertex3f(200, 10, 0.0f);
            Gl.glVertex3f(10, 10, 0.0f);

            Gl.glEnd();
            Gl.glFlush();
            */

            /*
            //プリミティブ描画テスト
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture[0]);

            Gl.glBegin(Gl.GL_POLYGON);

            Gl.glColor3f(1.0f, 1.0f, 1.0f);
            Gl.glTexCoord2f(0, 0); Gl.glVertex2f(-0.9f, -0.9f);
            Gl.glTexCoord2f(0, 1); Gl.glVertex2f(-0.9f, 0.9f);
            Gl.glTexCoord2f(1, 1); Gl.glVertex2f(0.9f, 0.9f);
            Gl.glTexCoord2f(1, 0); Gl.glVertex2f(0.9f, -0.9f);

            Gl.glEnd();
            Gl.glFlush();
            */

            //ダブルバッファ
            Wgl.wglSwapBuffers(this.hDC);
  		}

        public void DrawCrossBarLine(Color stCol)
        {
            int inWidth = (int)this.mCanvasWidth;
            int inHeight = (int)this.mCanvasHeight;

            //以下、垂直ラインを描画する処理
            this.DrawLine(stCol, this.mCenterX, -inHeight / 2, this.mCenterX, inHeight / 2);

            //以下、水平ラインを描画する処理
            this.DrawLine(stCol, -inWidth / 2, this.mCenterY, inWidth / 2, this.mCenterY);
        }

        public void DrawGridLine(Color stCol, float flGridSpan)
        {
            int inWidth = (int)this.mCanvasWidth;
            int inHeight = (int)this.mCanvasHeight;

            //以下、センターラインよりも右側の垂直ラインを描画する処理
            float flX;
            for (flX = this.mCenterX; flX < inWidth; flX+= flGridSpan)
            {
                this.DrawLine(stCol, flX, -inHeight / 2, flX, inHeight / 2);
            }

            //以下、センターラインよりも左側の垂直ラインを描画する処理
            for (flX = this.mCenterX - flGridSpan; flX > -inWidth; flX -= flGridSpan)
            {
                this.DrawLine(stCol, flX, -inHeight / 2, flX, inHeight / 2);
            }

            //以下、センターラインよりも上側の水平ラインを描画する処理
            float flY;
            for (flY = this.mCenterY; flY < inHeight; flY += flGridSpan)
            {
                this.DrawLine(stCol, -inWidth / 2, flY, inWidth / 2, flY);
            }

            //以下、センターラインよりも下側の水平ラインを描画する処理
            for (flY = this.mCenterY - flGridSpan; flY > -inHeight; flY -= flGridSpan)
            {
                this.DrawLine(stCol, -inWidth / 2, flY, inWidth / 2, flY);
            }
        }

        public void DrawLine(Color stCol, float flX1, float flY1, float flX2, float flY2)
        {
            //以下、テクスチャー初期化処理
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);

            //以下、色設定処理
            Gl.glColor3f(stCol.R / 255.0f, stCol.G / 255.0f, stCol.B / 255.0f);

            //以下、ライン描画処理
            Gl.glBegin(Gl.GL_LINES);
            
            Gl.glVertex3f(flX1, flY1, 0.0f);
            Gl.glVertex3f(flX2, flY2, 0.0f);

            Gl.glEnd();
            Gl.glFlush();
        }

        public void DrawImage(int inX, int inY, int inW, int inH)
        {
        }
    }
}
