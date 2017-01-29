using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrjHikariwoAnim
{
    public partial class FormDragLabel : Form
    {
        private ClsDatElem mElem = null;

        public FormDragLabel(ClsDatElem clElem)
        {
            InitializeComponent();

            //以下、初期化処理
            this.mElem = clElem;

            //以下、ウィンドウ移動処理
            this.MoveWindowNearMouse();

        }

        private void FormDragItem_Load(object sender, EventArgs e)
        {
            if (this.mElem == null)
            {
                this.Close();
                return;
            }

            //以下、初期化処理
            this.label.Text = this.mElem.mName;
            int inW = this.label.Size.Width;
            int inH = this.label.Size.Height;
            this.Size = new Size(inW + 24, inH + 20);
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            //以下、ウィンドウ移動処理
            this.MoveWindowNearMouse();

            //以下、左ボタン監視処理
            if ((Control.MouseButtons & MouseButtons.Left) != MouseButtons.Left)
            {
                this.Close();
            }
        }

        /// <summary>
        /// マウスの右下にウィンドウを移動する処理
        /// </summary>
        private void MoveWindowNearMouse()
        {
            int inX = Cursor.Position.X;
            int inY = Cursor.Position.Y;
            this.Location = new Point(inX + 15, inY + 15);
        }

        /// <summary>
        /// エレメント取得処理
        /// </summary>
        /// <returns>エレメント</returns>
        public ClsDatElem GetElem()
        {
            return (this.mElem);
        }
    }
}
