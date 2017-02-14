namespace PrjHikariwoAnim
{
    partial class FormImageList
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
            this.button_Add = new System.Windows.Forms.Button();
            this.button_Delete = new System.Windows.Forms.Button();
            this.button_Cut = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.checkBox_View = new System.Windows.Forms.CheckBox();
            this.button_OpenMyDoc = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Add
            // 
            this.button_Add.Image = global::PrjHikariwoAnim.Properties.Resources.folder;
            this.button_Add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Add.Location = new System.Drawing.Point(3, 4);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(65, 23);
            this.button_Add.TabIndex = 0;
            this.button_Add.Text = "追加";
            this.button_Add.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // button_Delete
            // 
            this.button_Delete.Location = new System.Drawing.Point(215, 5);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(37, 23);
            this.button_Delete.TabIndex = 1;
            this.button_Delete.Text = "削除";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // button_Cut
            // 
            this.button_Cut.Location = new System.Drawing.Point(134, 5);
            this.button_Cut.Name = "button_Cut";
            this.button_Cut.Size = new System.Drawing.Size(75, 23);
            this.button_Cut.TabIndex = 2;
            this.button_Cut.Text = "一部切取";
            this.button_Cut.UseVisualStyleBackColor = true;
            this.button_Cut.Click += new System.EventHandler(this.button_Cut_Click);
            // 
            // listView
            // 
            this.listView.BackgroundImageTiled = true;
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.HideSelection = false;
            this.listView.LargeImageList = this.imageList;
            this.listView.Location = new System.Drawing.Point(0, 32);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(340, 441);
            this.listView.SmallImageList = this.imageList;
            this.listView.TabIndex = 3;
            this.listView.TileSize = new System.Drawing.Size(128, 128);
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            this.listView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDown);
            this.listView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView_MouseMove);
            this.listView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Image";
            this.columnHeader1.Width = 128;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "FilePath";
            this.columnHeader2.Width = 200;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(128, 128);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // timerMain
            // 
            this.timerMain.Enabled = true;
            this.timerMain.Interval = 16;
            // 
            // checkBox_View
            // 
            this.checkBox_View.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_View.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_View.AutoSize = true;
            this.checkBox_View.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_View.Location = new System.Drawing.Point(297, 6);
            this.checkBox_View.Name = "checkBox_View";
            this.checkBox_View.Size = new System.Drawing.Size(40, 22);
            this.checkBox_View.TabIndex = 6;
            this.checkBox_View.Text = "View";
            this.checkBox_View.UseVisualStyleBackColor = true;
            this.checkBox_View.CheckedChanged += new System.EventHandler(this.View_Checked_Changed);
            // 
            // button_OpenMyDoc
            // 
            this.button_OpenMyDoc.Location = new System.Drawing.Point(73, 5);
            this.button_OpenMyDoc.Name = "button_OpenMyDoc";
            this.button_OpenMyDoc.Size = new System.Drawing.Size(55, 23);
            this.button_OpenMyDoc.TabIndex = 7;
            this.button_OpenMyDoc.Text = "MyDoc";
            this.button_OpenMyDoc.UseVisualStyleBackColor = true;
            this.button_OpenMyDoc.Click += new System.EventHandler(this.button_OpenMyDoc_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Add);
            this.panel1.Controls.Add(this.checkBox_View);
            this.panel1.Controls.Add(this.button_OpenMyDoc);
            this.panel1.Controls.Add(this.button_Delete);
            this.panel1.Controls.Add(this.button_Cut);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 32);
            this.panel1.TabIndex = 8;
            // 
            // FormImageList
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 473);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.Name = "FormImageList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Image list";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormImageList_FormClosing);
            this.Load += new System.EventHandler(this.FormImageList_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormImageList_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormImageList_DragEnter);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.Button button_Cut;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.CheckBox checkBox_View;
        private System.Windows.Forms.Button button_OpenMyDoc;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Panel panel1;
    }
}