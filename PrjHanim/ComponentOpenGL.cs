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
        public static float mScale;
        public static float mX;
        public static float mY;
        public static int mWidth;
        public static int mHeight;

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

            //以下、初期化処理
            ComponentOpenGL.mScale = 1.0f;
            ComponentOpenGL.mX = 0.0f;
            ComponentOpenGL.mY = 0.0f;
            ComponentOpenGL.mWidth = this.Width;
            ComponentOpenGL.mHeight = this.Height;
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
  			SetupOpenGL();		//OpenGLの初期設定
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
  			//レンダリングコンテキストをカレントにする
  			Wgl.wglMakeCurrent(this.hDC, this.hRC);
  
  			//バッファをクリア
  			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            //ビューポートの設定
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
  
  			//射影行列の設定
  			Gl.glMatrixMode(Gl.GL_PROJECTION);
  			Gl.glLoadIdentity();
  
  			//モデルビュー行列の設定
  			Gl.glMatrixMode(Gl.GL_MODELVIEW);
  			Gl.glLoadIdentity();

            //以下、グリッドライン描画処理
            Gl.glBegin(Gl.GL_LINES);

            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glVertex3f(-1.0f, 0.0f, 0.0f);

            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glVertex3f(1.0f, 0.0f, 0.0f);

            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glVertex3f(0.0f, -1.0f, 0.0f);

            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glVertex3f(0.0f, 1.0f, 0.0f);

            Gl.glEnd();

            //三角形を描画
            Gl.glBegin(Gl.GL_TRIANGLES);

  			Gl.glColor3f(1.0f, 0.0f, 0.0f);
  			Gl.glVertex3f(0.0f, 0.0f, 0.0f);

  			Gl.glColor3f(0.0f, 1.0f, 0.0f);
  			Gl.glVertex3f(0.0f, 1.0f, 0.0f);

  			Gl.glColor3f(0.0f, 0.0f, 1.0f);
  			Gl.glVertex3f(1.0f, 1.0f, 0.0f);

  			Gl.glEnd();

            //以下、中心ライン描画処理

            //以下、グリッド表示処理

            //以下、各エレメント表示処理

            //ダブルバッファ
            Wgl.wglSwapBuffers(this.hDC);
  		}

        /// <summary>
        /// ワールド座標系からカメラ座標系に変換する
        /// </summary>
        /// <param name="flPosX">Ｘ座標</param>
        /// <returns>カメラ座標</returns>
        public static float WorldPosX2CameraPosX(float flPosX)
        {
            float flPosXNew = ComponentOpenGL.mX + flPosX * ComponentOpenGL.mScale + ComponentOpenGL.mWidth / 2;
            return (flPosXNew);
        }

        /// <summary>
        /// ワールド座標系からカメラ座標系に変換する
        /// </summary>
        /// <param name="flPosY">Ｙ座標</param>
        /// <returns>カメラ座標</returns>
        public static float WorldPosY2CameraPosY(float flPosY)
        {
            float flPosYNew = ComponentOpenGL.mY + flPosY * ComponentOpenGL.mScale + ComponentOpenGL.mHeight / 2;
            return (flPosYNew);
        }

        /// <summary>
        /// カメラ座標系からワールド座標系に変換する
        /// </summary>
        /// <param name="flPosX">Ｘ座標</param>
        /// <returns>ワールド座標</returns>
        public static float CameraPosX2WorldPosX(float flPosX)
        {
            float flPosXNew = (flPosX - ComponentOpenGL.mWidth / 2 - ComponentOpenGL.mX) / ComponentOpenGL.mScale;
            return (flPosXNew);
        }

        /// <summary>
        /// カメラ座標系からワールド座標系に変換する
        /// </summary>
        /// <param name="inPosY">Ｙ座標</param>
        /// <returns>ワールド座標</returns>
        public static float CameraPosY2WorldPosY(float flPosY)
        {
            float flPosYNew = (flPosY - ComponentOpenGL.mHeight / 2 - ComponentOpenGL.mY) / ComponentOpenGL.mScale;
            return (flPosYNew);
        }
    }
}
