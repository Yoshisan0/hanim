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
            this.panel_menu = new System.Windows.Forms.Panel();
            this.panel_list = new System.Windows.Forms.Panel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.button1 = new System.Windows.Forms.Button();
            this.panel_menu.SuspendLayout();
            this.panel_list.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_menu
            // 
            this.panel_menu.Controls.Add(this.button1);
            this.panel_menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_menu.Location = new System.Drawing.Point(0, 0);
            this.panel_menu.Name = "panel_menu";
            this.panel_menu.Size = new System.Drawing.Size(172, 26);
            this.panel_menu.TabIndex = 0;
            // 
            // panel_list
            // 
            this.panel_list.AllowDrop = true;
            this.panel_list.Controls.Add(this.vScrollBar1);
            this.panel_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_list.Location = new System.Drawing.Point(0, 26);
            this.panel_list.Name = "panel_list";
            this.panel_list.Size = new System.Drawing.Size(172, 215);
            this.panel_list.TabIndex = 1;
            this.panel_list.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormCell_DragDrop);
            this.panel_list.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormCell_DragEnter);
            this.panel_list.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            this.panel_list.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormCell_MouseDown);
            this.panel_list.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormCell_MouseMove);
            this.panel_list.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_list_MouseUp);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(155, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 215);
            this.vScrollBar1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(136, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Del";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Delbutton_Click);
            // 
            // FormCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(172, 241);
            this.Controls.Add(this.panel_list);
            this.Controls.Add(this.panel_menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCell";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ChipList";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCell_FormClosing);
            this.Load += new System.EventHandler(this.FormCell_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormCell_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormCell_DragEnter);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormCell_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormCell_MouseMove);
            this.panel_menu.ResumeLayout(false);
            this.panel_list.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_menu;
        private System.Windows.Forms.Panel panel_list;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Button button1;
    }
}