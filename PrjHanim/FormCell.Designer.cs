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
            this.button_OpenDoc = new System.Windows.Forms.Button();
            this.button_LoadPic = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel_listBase = new System.Windows.Forms.Panel();
            this.panel_list = new System.Windows.Forms.Panel();
            this.panel_menu.SuspendLayout();
            this.panel_listBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_menu
            // 
            this.panel_menu.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel_menu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_menu.Controls.Add(this.button_OpenDoc);
            this.panel_menu.Controls.Add(this.button_LoadPic);
            this.panel_menu.Controls.Add(this.button1);
            this.panel_menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_menu.Location = new System.Drawing.Point(0, 0);
            this.panel_menu.Name = "panel_menu";
            this.panel_menu.Size = new System.Drawing.Size(112, 26);
            this.panel_menu.TabIndex = 0;
            // 
            // button_OpenDoc
            // 
            this.button_OpenDoc.BackColor = System.Drawing.Color.DimGray;
            this.button_OpenDoc.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_OpenDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_OpenDoc.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_OpenDoc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_OpenDoc.Image = global::PrjHikariwoAnim.Properties.Resources.folder;
            this.button_OpenDoc.Location = new System.Drawing.Point(26, 0);
            this.button_OpenDoc.Name = "button_OpenDoc";
            this.button_OpenDoc.Size = new System.Drawing.Size(26, 24);
            this.button_OpenDoc.TabIndex = 2;
            this.button_OpenDoc.UseVisualStyleBackColor = false;
            this.button_OpenDoc.Click += new System.EventHandler(this.button_Doc_Click);
            // 
            // button_LoadPic
            // 
            this.button_LoadPic.BackColor = System.Drawing.Color.DimGray;
            this.button_LoadPic.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_LoadPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_LoadPic.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_LoadPic.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_LoadPic.Image = global::PrjHikariwoAnim.Properties.Resources.folder;
            this.button_LoadPic.Location = new System.Drawing.Point(0, 0);
            this.button_LoadPic.Name = "button_LoadPic";
            this.button_LoadPic.Size = new System.Drawing.Size(26, 24);
            this.button_LoadPic.TabIndex = 1;
            this.button_LoadPic.UseVisualStyleBackColor = false;
            this.button_LoadPic.Click += new System.EventHandler(this.button_LoadPic_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(78, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 24);
            this.button1.TabIndex = 0;
            this.button1.Text = "Del";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_Del_Click);
            // 
            // panel_listBase
            // 
            this.panel_listBase.AllowDrop = true;
            this.panel_listBase.AutoScroll = true;
            this.panel_listBase.Controls.Add(this.panel_list);
            this.panel_listBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_listBase.Location = new System.Drawing.Point(0, 26);
            this.panel_listBase.MinimumSize = new System.Drawing.Size(64, 64);
            this.panel_listBase.Name = "panel_listBase";
            this.panel_listBase.Size = new System.Drawing.Size(112, 215);
            this.panel_listBase.TabIndex = 1;
            // 
            // panel_list
            // 
            this.panel_list.AllowDrop = true;
            this.panel_list.BackgroundImage = global::PrjHikariwoAnim.Properties.Resources.Blank;
            this.panel_list.Location = new System.Drawing.Point(0, 0);
            this.panel_list.Name = "panel_list";
            this.panel_list.Size = new System.Drawing.Size(79, 78);
            this.panel_list.TabIndex = 1;
            this.panel_list.Click += new System.EventHandler(this.panel_list_Click);
            this.panel_list.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel_list_DragDrop);
            this.panel_list.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel_list_DragEnter);
            this.panel_list.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_list_Paint);
            this.panel_list.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel_list_MouseDoubleClick);
            this.panel_list.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_list_MouseDown);
            this.panel_list.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_list_MouseMove);
            this.panel_list.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_list_MouseUp);
            // 
            // FormCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(112, 241);
            this.Controls.Add(this.panel_listBase);
            this.Controls.Add(this.panel_menu);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(128, 128);
            this.Name = "FormCell";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ChipList";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCell_FormClosing);
            this.Load += new System.EventHandler(this.FormCell_Load);
            this.Resize += new System.EventHandler(this.FormCell_Resize);
            this.panel_menu.ResumeLayout(false);
            this.panel_listBase.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_menu;
        private System.Windows.Forms.Panel panel_listBase;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel_list;
        private System.Windows.Forms.Button button_LoadPic;
        private System.Windows.Forms.Button button_OpenDoc;
    }
}