namespace PrjHikariwoAnim
{
    partial class FormCell
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(172, 26);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AllowDrop = true;
            this.panel2.Controls.Add(this.vScrollBar1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(172, 215);
            this.panel2.TabIndex = 1;
            this.panel2.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormCell_DragDrop);
            this.panel2.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormCell_DragEnter);
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormCell_MouseDown);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormCell_MouseMove);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(155, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 215);
            this.vScrollBar1.TabIndex = 0;
            // 
            // FormCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(172, 241);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCell";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormCell";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCell_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormCell_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormCell_DragEnter);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormCell_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormCell_MouseMove);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.VScrollBar vScrollBar1;
    }
}