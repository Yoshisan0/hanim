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
            this.components = new System.ComponentModel.Container();
            this.panel_menu = new System.Windows.Forms.Panel();
            this.button_Delete = new System.Windows.Forms.Button();
            this.button_Cut = new System.Windows.Forms.Button();
            this.button_OpenDoc = new System.Windows.Forms.Button();
            this.button_LoadPic = new System.Windows.Forms.Button();
            this.panel_listBase = new System.Windows.Forms.Panel();
            this.panel_list = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel_menu.SuspendLayout();
            this.panel_listBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_menu
            // 
            this.panel_menu.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel_menu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_menu.Controls.Add(this.button_Delete);
            this.panel_menu.Controls.Add(this.button_Cut);
            this.panel_menu.Controls.Add(this.button_OpenDoc);
            this.panel_menu.Controls.Add(this.button_LoadPic);
            this.panel_menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_menu.Location = new System.Drawing.Point(0, 0);
            this.panel_menu.Name = "panel_menu";
            this.panel_menu.Size = new System.Drawing.Size(284, 26);
            this.panel_menu.TabIndex = 0;
            // 
            // button_Delete
            // 
            this.button_Delete.BackColor = System.Drawing.Color.DimGray;
            this.button_Delete.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Delete.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Delete.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_Delete.Image = global::PrjHikariwoAnim.Properties.Resources.delete;
            this.button_Delete.Location = new System.Drawing.Point(256, 0);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(26, 24);
            this.button_Delete.TabIndex = 4;
            this.toolTip1.SetToolTip(this.button_Delete, "Open Doc");
            this.button_Delete.UseVisualStyleBackColor = false;
            this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // button_Cut
            // 
            this.button_Cut.BackColor = System.Drawing.Color.DimGray;
            this.button_Cut.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Cut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Cut.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Cut.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_Cut.Image = global::PrjHikariwoAnim.Properties.Resources.cut;
            this.button_Cut.Location = new System.Drawing.Point(52, 0);
            this.button_Cut.Name = "button_Cut";
            this.button_Cut.Size = new System.Drawing.Size(26, 24);
            this.button_Cut.TabIndex = 3;
            this.toolTip1.SetToolTip(this.button_Cut, "Open Doc");
            this.button_Cut.UseVisualStyleBackColor = false;
            this.button_Cut.Click += new System.EventHandler(this.button_Cut_Click);
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
            this.toolTip1.SetToolTip(this.button_OpenDoc, "Open Doc");
            this.button_OpenDoc.UseVisualStyleBackColor = false;
            this.button_OpenDoc.Click += new System.EventHandler(this.button_OpenDoc_Click);
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
            this.toolTip1.SetToolTip(this.button_LoadPic, "Open File");
            this.button_LoadPic.UseVisualStyleBackColor = false;
            this.button_LoadPic.Click += new System.EventHandler(this.button_LoadPic_Click);
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
            this.panel_listBase.Size = new System.Drawing.Size(284, 235);
            this.panel_listBase.TabIndex = 1;
            // 
            // panel_list
            // 
            this.panel_list.AllowDrop = true;
            this.panel_list.BackgroundImage = global::PrjHikariwoAnim.Properties.Resources.Blank;
            this.panel_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_list.Location = new System.Drawing.Point(0, 0);
            this.panel_list.Name = "panel_list";
            this.panel_list.Size = new System.Drawing.Size(284, 235);
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
            this.ClientSize = new System.Drawing.Size(284, 261);
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
        private System.Windows.Forms.Panel panel_list;
        private System.Windows.Forms.Button button_LoadPic;
        private System.Windows.Forms.Button button_OpenDoc;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_Cut;
        private System.Windows.Forms.Button button_Delete;
    }
}