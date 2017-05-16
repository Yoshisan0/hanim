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
        public Color mBackColor = Color.Black;
        public float mCenterX = 0.0f;
        public float mCenterY = 0.0f;
        public float mScale = 1.0f;
        public bool mCrossBarVisible = true;
        public Color mCrossColor = Color.Red;
        public bool mGridVisible = true;
        public Color mGridColor = Color.Green;
        public float mGridSpan = 16.0f;
        public float mCanvasWidth = 800.0f;
        public float mCanvasHeight = 600.0f;

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
        }

  		/// <summary>
  		/// ピクセルフォーマットの設定をする
  		/// </summary>
  		private void SetupPixelFormat()
  		{
  			//PIXELFORMATDESCRIPTORの設定
  			Gdi.PIXELFORMATDESCRIPTOR pfd = new Gdi.PIXELFORMATDESCRIPTOR();
  			pfd.dwFlags = Gdi.PFD_SUPPORT_OPENGL | Gdi.PFD_DRAW_TO_WINDOW | Gdi.PFD_DOUBLEBUFFER;
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
            //以下、初期化処理
            this.mCanvasWidth = this.Width;
            this.mCanvasHeight = this.Height;

            //以下、背景色初期化処理
            Gl.glClearColor(this.mBackColor.R / 255.0f, this.mBackColor.G / 255.0f, this.mBackColor.B / 255.0f, 1.0f);

  			//以下、深度テストを有効化
  			Gl.glEnable(Gl.GL_DEPTH_TEST);
  			Gl.glDepthFunc(Gl.GL_LEQUAL);

            //以下、半透明設定
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);

            //以下、スムースシェイディング設定
            Gl.glShadeModel(Gl.GL_SMOOTH);

            //以下、テクスチャー表示有効化設定
            Gl.glEnable(Gl.GL_TEXTURE_2D);
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
            int inFrameNoNow = 0;
            int inMaxFrameNum = ClsSystem.DEFAULT_FRAME_NUM;
            ClsDatMotion clMotion = ClsSystem.GetSelectMotion();
            if (clMotion != null)
            {
                inFrameNoNow = clMotion.GetSelectFrameNo();
                inMaxFrameNum = clMotion.GetMaxFrameNum();
                clMotion.DrawPreview(this, inFrameNoNow, inMaxFrameNum);
            }

            //以下、キャンバスサイズ設定処理
            this.mCanvasWidth = this.Width;
            this.mCanvasHeight = this.Height;
            int inWidth = this.Width;
            int inHeight = this.Height;

            //以下、一番引きのスクリーンでのグリッド表示処理を決める
            //（一番引きのスクリーンでは、グリッド間隔を最大にしても密度が濃くてラインで描く必要がないのと、ラインを大量に描画する必要があり重いため）
            bool isDammyGrid = false;
            if (this.mGridVisible)
            {
                if (this.mScale <= 0.125f)
                {
                    isDammyGrid = true;
                }
            }

            //レンダリングコンテキストをカレントにする
            Wgl.wglMakeCurrent(this.hDC, this.hRC);

            //以下、背景色設定処理
            if (isDammyGrid)
            {
                Gl.glClearColor(this.mGridColor.R / 255.0f, this.mGridColor.G / 255.0f, this.mGridColor.B / 255.0f, 1.0f);
            }
            else
            {
                Gl.glClearColor(this.mBackColor.R / 255.0f, this.mBackColor.G / 255.0f, this.mBackColor.B / 255.0f, 1.0f);
            }

            //以下、バッファをクリアする処理
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            //以下、ビューポートの設定処理
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

            //以下、射影行列の設定処理
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(-inWidth / 2, inWidth / 2, -inHeight / 2, inHeight / 2, 0.0, 4.0);

            //以下、カメラの位置を設定する処理
            Glu.gluLookAt(0.0, 0.0, 2.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0); //視点の設定

            //以下、モデルビュー行列の設定
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            //以下、グリッドライン描画処理
            if (this.mGridVisible)
            {
                if (!isDammyGrid)
                {
                    this.DrawGridLine(this.mGridColor, this.mGridSpan * this.mScale);
                }
            }

            //以下、中心ライン描画処理
            if (this.mCrossBarVisible)
            {
                this.DrawCrossBarLine(this.mCrossColor);
            }

            //以下、モーション描画処理
            if (clMotion != null)
            {
                clMotion.DrawPreview(this, inFrameNoNow, inMaxFrameNum);
            }

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

            //ダブルバッファ
            Wgl.wglSwapBuffers(this.hDC);
        }

        /// <summary>
        /// 中心ライン描画処理
        /// </summary>
        /// <param name="stCol">中心ラインの色</param>
        public void DrawCrossBarLine(Color stCol)
        {
            int inWidth = (int)this.mCanvasWidth;
            int inHeight = (int)this.mCanvasHeight;

            //以下、垂直ラインを描画する処理
            this.DrawLine(stCol, this.mCenterX, -inHeight, this.mCenterX, inHeight);

            //以下、水平ラインを描画する処理
            this.DrawLine(stCol, -inWidth, this.mCenterY, inWidth, this.mCenterY);
        }

        /// <summary>
        /// グリッドライン描画処理
        /// </summary>
        /// <param name="stCol">グリッドラインの色</param>
        /// <param name="flGridSpan">グリッドラインの間隔</param>
        public void DrawGridLine(Color stCol, float flGridSpan)
        {
            int inWidth = (int)this.mCanvasWidth;
            int inHeight = (int)this.mCanvasHeight;

            //以下、センターラインよりも右側の垂直ラインを描画する処理
            float flX;
            for (flX = this.mCenterX; flX < inWidth; flX += flGridSpan)
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

        /// <summary>
        /// 線分描画処理
        /// </summary>
        /// <param name="stCol">線分の色</param>
        /// <param name="flX1">線分始点Ｘ座標</param>
        /// <param name="flY1">線分始点Ｙ座標</param>
        /// <param name="flX2">線分終点Ｘ座標</param>
        /// <param name="flY2">線分終点Ｙ座標</param>
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

        /// <summary>
        /// テクスチャー設定処理
        /// </summary>
        /// <param name="uinTexNo">テクスチャー番号</param>
        public void SetTexture(uint uinTexNo)
        {
            //以下、テクスチャー設定
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, uinTexNo);
        }

        /// <summary>
        /// マトリクス設定処理
        /// </summary>
        /// <param name="clParam">各パラメーター管理クラス</param>
        public void SetParam(ClsParam clParam)
        {
            Gl.glLoadIdentity();

            //以下、マテリアル色設定
            float flAlpha = 1.0f;
            if (clParam.mExistTransOption) flAlpha = clParam.mTrans / 255.0f;
            Color stColor = Color.White;
            if (clParam.mExistColorOption) stColor = Color.FromArgb(clParam.mColor);
            Gl.glColor4f(stColor.R / 255.0f, stColor.G / 255.0f, stColor.B / 255.0f, flAlpha);

            //以下、ワールド座標設定
            Gl.glTranslatef(this.mCenterX, this.mCenterY, 0.0f);

            //以下、ワールドスケール設定
            Gl.glScalef(this.mScale, this.mScale, 1.0f);

            //以下、ローカル座標設定
            float flX = clParam.mX;
            float flY = clParam.mY;
            Gl.glTranslatef(flX, flY, 0.0f);

            //以下、ローカル回転設定
            float flRZ = 0.0f;
            if (clParam.mExistRotationOption)
            {
                flRZ = clParam.mRZ;
            }
            Gl.glRotatef(flRZ, 0.0f, 0.0f, 1.0f);

            //以下、ローカルスケール設定
            float flSX = 1.0f;
            float flSY = 1.0f;
            if (clParam.mExistScaleOption)
            {
                flSX = clParam.mSX;
                flSY = clParam.mSY;
            }
            Gl.glScalef(flSX, flSY, 1.0f);
        }

        /// <summary>
        /// ポリゴン描画処理
        /// </summary>
        /// <param name="pclListUV">ＵＶ座標のリスト</param>
        /// <param name="flOffsetX">オフセットＸ座標</param>
        /// <param name="flOffsetY">オフセットＹ座標</param>
        /// <param name="flW">幅</param>
        /// <param name="flH">高さ</param>
        public void DrawPolygon(ClsVector2[] pclListUV, float flOffsetX, float flOffsetY, float flW, float flH)
        {
            //以下、ポリゴン描画処理
            Gl.glBegin(Gl.GL_POLYGON);
            
            Gl.glTexCoord2f(pclListUV[0].X, pclListUV[0].Y); Gl.glVertex3f(flOffsetX - flW / 2.0f, flOffsetY - flH / 2.0f, 0.0f);
            Gl.glTexCoord2f(pclListUV[1].X, pclListUV[1].Y); Gl.glVertex3f(flOffsetX - flW / 2.0f, flOffsetY + flH / 2.0f, 0.0f);
            Gl.glTexCoord2f(pclListUV[2].X, pclListUV[2].Y); Gl.glVertex3f(flOffsetX + flW / 2.0f, flOffsetY + flH / 2.0f, 0.0f);
            Gl.glTexCoord2f(pclListUV[3].X, pclListUV[3].Y); Gl.glVertex3f(flOffsetX + flW / 2.0f, flOffsetY - flH / 2.0f, 0.0f);

            Gl.glEnd();
            Gl.glFlush();

            /*
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
        }
    }
}
