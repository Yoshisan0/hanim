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
            this.label1 = new System.Windows.Forms.Label();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.checkBox_View = new System.Windows.Forms.CheckBox();
            this.button_OpenMyDoc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Add
            // 
            this.button_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Add.Location = new System.Drawing.Point(12, 438);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(75, 23);
            this.button_Add.TabIndex = 0;
            this.button_Add.Text = "追加";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // button_Delete
            // 
            this.button_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Delete.Location = new System.Drawing.Point(93, 438);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(75, 23);
            this.button_Delete.TabIndex = 1;
            this.button_Delete.Text = "削除";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // button_Cut
            // 
            this.button_Cut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cut.Location = new System.Drawing.Point(281, 438);
            this.button_Cut.Name = "button_Cut";
            this.button_Cut.Size = new System.Drawing.Size(75, 23);
            this.button_Cut.TabIndex = 2;
            this.button_Cut.Text = "一部切取";
            this.button_Cut.UseVisualStyleBackColor = true;
            this.button_Cut.Click += new System.EventHandler(this.button_Cut_Click);
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.LargeImageList = this.imageList;
            this.listView.Location = new System.Drawing.Point(12, 27);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(344, 408);
            this.listView.SmallImageList = this.imageList;
            this.listView.TabIndex = 3;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            this.listView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDown);
            this.listView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView_MouseMove);
            this.listView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "画像";
            this.columnHeader1.Width = 136;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "パス";
            this.columnHeader2.Width = 203;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(128, 128);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "ココに png ファイルをドラッグ＆ドロップしてください";
            // 
            // timerMain
            // 
            this.timerMain.Enabled = true;
            this.timerMain.Interval = 16;
            // 
            // checkBox_View
            // 
            this.checkBox_View.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_View.AutoSize = true;
            this.checkBox_View.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_View.Location = new System.Drawing.Point(247, 4);
            this.checkBox_View.Name = "checkBox_View";
            this.checkBox_View.Size = new System.Drawing.Size(40, 22);
            this.checkBox_View.TabIndex = 6;
            this.checkBox_View.Text = "View";
            this.checkBox_View.UseVisualStyleBackColor = true;
            this.checkBox_View.CheckedChanged += new System.EventHandler(this.View_Checked_Changed);
            // 
            // button_OpenMyDoc
            // 
            this.button_OpenMyDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_OpenMyDoc.Location = new System.Drawing.Point(174, 438);
            this.button_OpenMyDoc.Name = "button_OpenMyDoc";
            this.button_OpenMyDoc.Size = new System.Drawing.Size(75, 23);
            this.button_OpenMyDoc.TabIndex = 7;
            this.button_OpenMyDoc.Text = "MyDoc";
            this.button_OpenMyDoc.UseVisualStyleBackColor = true;
            this.button_OpenMyDoc.Click += new System.EventHandler(this.button_OpenMyDoc_Click);
            // 
            // FormImageList
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 473);
            this.Controls.Add(this.button_OpenMyDoc);
            this.Controls.Add(this.checkBox_View);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.button_Cut);
            this.Controls.Add(this.button_Delete);
            this.Controls.Add(this.button_Add);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.Button button_Cut;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.CheckBox checkBox_View;
        private System.Windows.Forms.Button button_OpenMyDoc;
        private System.Windows.Forms.ImageList imageList;
    }
}