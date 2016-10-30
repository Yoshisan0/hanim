namespace PrjHikariwoAnim
{
    partial class FormImageCut
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
            this.panel = new System.Windows.Forms.Panel();
            this.panel_Image = new System.Windows.Forms.Panel();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.PanelMenu = new System.Windows.Forms.Panel();
            this.button_Divid = new System.Windows.Forms.Button();
            this.button_Cut = new System.Windows.Forms.Button();
            this.GridMenuBase = new System.Windows.Forms.Panel();
            this.panel_ColorGrid = new System.Windows.Forms.Panel();
            this.checkBox_Grid = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_ColorRect = new System.Windows.Forms.Panel();
            this.comboBox_Mag = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UD_DivY = new System.Windows.Forms.NumericUpDown();
            this.UD_DivX = new System.Windows.Forms.NumericUpDown();
            this.checkBox_Magnet = new System.Windows.Forms.CheckBox();
            this.splitContainerBase = new System.Windows.Forms.SplitContainer();
            this.panel_CellList = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_Clear = new System.Windows.Forms.Button();
            this.button_CellDelete = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.CellSave = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.PanelMenu.SuspendLayout();
            this.GridMenuBase.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UD_DivY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UD_DivX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBase)).BeginInit();
            this.splitContainerBase.Panel1.SuspendLayout();
            this.splitContainerBase.Panel2.SuspendLayout();
            this.splitContainerBase.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.panel_Image);
            this.panel.Cursor = System.Windows.Forms.Cursors.Cross;
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(611, 184);
            this.panel.TabIndex = 6;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // panel_Image
            // 
            this.panel_Image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Image.Location = new System.Drawing.Point(12, 5);
            this.panel_Image.Name = "panel_Image";
            this.panel_Image.Size = new System.Drawing.Size(64, 64);
            this.panel_Image.TabIndex = 0;
            this.panel_Image.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Image_Paint);
            this.panel_Image.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Image_MouseDown);
            this.panel_Image.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_Image_MouseUp);
            // 
            // timerMain
            // 
            this.timerMain.Enabled = true;
            this.timerMain.Interval = 16;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 32);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(611, 0);
            this.flowLayoutPanel1.TabIndex = 17;
            // 
            // PanelMenu
            // 
            this.PanelMenu.Controls.Add(this.button_Divid);
            this.PanelMenu.Controls.Add(this.button_Cut);
            this.PanelMenu.Controls.Add(this.GridMenuBase);
            this.PanelMenu.Controls.Add(this.panel1);
            this.PanelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelMenu.Location = new System.Drawing.Point(0, 0);
            this.PanelMenu.Margin = new System.Windows.Forms.Padding(0);
            this.PanelMenu.Name = "PanelMenu";
            this.PanelMenu.Size = new System.Drawing.Size(611, 32);
            this.PanelMenu.TabIndex = 1;
            // 
            // button_Divid
            // 
            this.button_Divid.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_Divid.Location = new System.Drawing.Point(483, 0);
            this.button_Divid.Name = "button_Divid";
            this.button_Divid.Size = new System.Drawing.Size(53, 32);
            this.button_Divid.TabIndex = 30;
            this.button_Divid.Text = "Divid";
            this.button_Divid.UseVisualStyleBackColor = true;
            this.button_Divid.Click += new System.EventHandler(this.button_Divid_Click);
            // 
            // button_Cut
            // 
            this.button_Cut.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button_Cut.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_Cut.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Cut.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Cut.Location = new System.Drawing.Point(536, 0);
            this.button_Cut.Name = "button_Cut";
            this.button_Cut.Size = new System.Drawing.Size(75, 32);
            this.button_Cut.TabIndex = 29;
            this.button_Cut.Text = "切り出し";
            this.button_Cut.UseVisualStyleBackColor = false;
            this.button_Cut.Click += new System.EventHandler(this.button_Cut_Click);
            // 
            // GridMenuBase
            // 
            this.GridMenuBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GridMenuBase.Controls.Add(this.panel_ColorGrid);
            this.GridMenuBase.Controls.Add(this.checkBox_Grid);
            this.GridMenuBase.Controls.Add(this.label2);
            this.GridMenuBase.Controls.Add(this.panel_ColorRect);
            this.GridMenuBase.Controls.Add(this.comboBox_Mag);
            this.GridMenuBase.Dock = System.Windows.Forms.DockStyle.Left;
            this.GridMenuBase.Location = new System.Drawing.Point(175, 0);
            this.GridMenuBase.Margin = new System.Windows.Forms.Padding(0);
            this.GridMenuBase.Name = "GridMenuBase";
            this.GridMenuBase.Size = new System.Drawing.Size(227, 32);
            this.GridMenuBase.TabIndex = 27;
            // 
            // panel_ColorGrid
            // 
            this.panel_ColorGrid.BackColor = System.Drawing.Color.Silver;
            this.panel_ColorGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_ColorGrid.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_ColorGrid.Location = new System.Drawing.Point(30, 0);
            this.panel_ColorGrid.Name = "panel_ColorGrid";
            this.panel_ColorGrid.Size = new System.Drawing.Size(30, 30);
            this.panel_ColorGrid.TabIndex = 14;
            this.panel_ColorGrid.Click += new System.EventHandler(this.panel_ColorGrid_Click);
            // 
            // checkBox_Grid
            // 
            this.checkBox_Grid.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_Grid.AutoSize = true;
            this.checkBox_Grid.BackColor = System.Drawing.Color.Black;
            this.checkBox_Grid.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_Grid.Image = global::PrjHikariwoAnim.Properties.Resources.grid;
            this.checkBox_Grid.Location = new System.Drawing.Point(0, 0);
            this.checkBox_Grid.Name = "checkBox_Grid";
            this.checkBox_Grid.Size = new System.Drawing.Size(30, 30);
            this.checkBox_Grid.TabIndex = 10;
            this.checkBox_Grid.UseVisualStyleBackColor = false;
            this.checkBox_Grid.CheckedChanged += new System.EventHandler(this.checkBox_Grid_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "選択色";
            // 
            // panel_ColorRect
            // 
            this.panel_ColorRect.BackColor = System.Drawing.Color.Lime;
            this.panel_ColorRect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_ColorRect.Location = new System.Drawing.Point(119, 0);
            this.panel_ColorRect.Name = "panel_ColorRect";
            this.panel_ColorRect.Size = new System.Drawing.Size(30, 30);
            this.panel_ColorRect.TabIndex = 18;
            this.panel_ColorRect.Click += new System.EventHandler(this.panel_ColorRect_Click);
            // 
            // comboBox_Mag
            // 
            this.comboBox_Mag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Mag.FormattingEnabled = true;
            this.comboBox_Mag.Items.AddRange(new object[] {
            "100%",
            "200%",
            "400%",
            "800%",
            "1600%"});
            this.comboBox_Mag.Location = new System.Drawing.Point(155, 5);
            this.comboBox_Mag.Name = "comboBox_Mag";
            this.comboBox_Mag.Size = new System.Drawing.Size(55, 20);
            this.comboBox_Mag.TabIndex = 19;
            this.comboBox_Mag.SelectedIndexChanged += new System.EventHandler(this.comboBox_Mag_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.UD_DivY);
            this.panel1.Controls.Add(this.UD_DivX);
            this.panel1.Controls.Add(this.checkBox_Magnet);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 32);
            this.panel1.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(108, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(34, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "X:";
            // 
            // UD_DivY
            // 
            this.UD_DivY.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UD_DivY.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UD_DivY.Location = new System.Drawing.Point(129, 4);
            this.UD_DivY.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.UD_DivY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UD_DivY.Name = "UD_DivY";
            this.UD_DivY.Size = new System.Drawing.Size(40, 22);
            this.UD_DivY.TabIndex = 18;
            this.UD_DivY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UD_DivY.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.UD_DivY.ValueChanged += new System.EventHandler(this.UD_DivXY_ValueChanged);
            // 
            // UD_DivX
            // 
            this.UD_DivX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UD_DivX.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UD_DivX.Location = new System.Drawing.Point(54, 4);
            this.UD_DivX.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.UD_DivX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UD_DivX.Name = "UD_DivX";
            this.UD_DivX.Size = new System.Drawing.Size(49, 22);
            this.UD_DivX.TabIndex = 17;
            this.UD_DivX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UD_DivX.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.UD_DivX.ValueChanged += new System.EventHandler(this.UD_DivXY_ValueChanged);
            // 
            // checkBox_Magnet
            // 
            this.checkBox_Magnet.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_Magnet.AutoSize = true;
            this.checkBox_Magnet.BackColor = System.Drawing.Color.Black;
            this.checkBox_Magnet.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_Magnet.Image = global::PrjHikariwoAnim.Properties.Resources.magnet2;
            this.checkBox_Magnet.Location = new System.Drawing.Point(0, 0);
            this.checkBox_Magnet.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_Magnet.Name = "checkBox_Magnet";
            this.checkBox_Magnet.Size = new System.Drawing.Size(30, 30);
            this.checkBox_Magnet.TabIndex = 16;
            this.checkBox_Magnet.UseVisualStyleBackColor = false;
            // 
            // splitContainerBase
            // 
            this.splitContainerBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerBase.Location = new System.Drawing.Point(0, 32);
            this.splitContainerBase.Name = "splitContainerBase";
            this.splitContainerBase.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerBase.Panel1
            // 
            this.splitContainerBase.Panel1.Controls.Add(this.panel);
            // 
            // splitContainerBase.Panel2
            // 
            this.splitContainerBase.Panel2.AutoScroll = true;
            this.splitContainerBase.Panel2.Controls.Add(this.panel_CellList);
            this.splitContainerBase.Panel2.Controls.Add(this.panel2);
            this.splitContainerBase.Size = new System.Drawing.Size(611, 289);
            this.splitContainerBase.SplitterDistance = 184;
            this.splitContainerBase.TabIndex = 18;
            // 
            // panel_CellList
            // 
            this.panel_CellList.AutoScroll = true;
            this.panel_CellList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_CellList.Location = new System.Drawing.Point(0, 0);
            this.panel_CellList.Margin = new System.Windows.Forms.Padding(0);
            this.panel_CellList.Name = "panel_CellList";
            this.panel_CellList.Size = new System.Drawing.Size(611, 71);
            this.panel_CellList.TabIndex = 7;
            this.panel_CellList.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_CellList_Paint);
            this.panel_CellList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_CellList_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CellSave);
            this.panel2.Controls.Add(this.button_Clear);
            this.panel2.Controls.Add(this.button_CellDelete);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(611, 30);
            this.panel2.TabIndex = 6;
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(85, 3);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(75, 23);
            this.button_Clear.TabIndex = 2;
            this.button_Clear.Text = "ClearAll";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // button_CellDelete
            // 
            this.button_CellDelete.Location = new System.Drawing.Point(4, 3);
            this.button_CellDelete.Name = "button_CellDelete";
            this.button_CellDelete.Size = new System.Drawing.Size(75, 23);
            this.button_CellDelete.TabIndex = 1;
            this.button_CellDelete.Text = "Delete";
            this.button_CellDelete.UseVisualStyleBackColor = true;
            this.button_CellDelete.Click += new System.EventHandler(this.button_CellDelete_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button_Cancel);
            this.panel3.Controls.Add(this.button_OK);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(447, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(164, 30);
            this.panel3.TabIndex = 0;
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(84, 4);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 1;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_OK.Location = new System.Drawing.Point(3, 4);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 0;
            this.button_OK.Text = "Add OK";
            this.button_OK.UseVisualStyleBackColor = true;
            // 
            // CellSave
            // 
            this.CellSave.Location = new System.Drawing.Point(166, 3);
            this.CellSave.Name = "CellSave";
            this.CellSave.Size = new System.Drawing.Size(75, 23);
            this.CellSave.TabIndex = 3;
            this.CellSave.Text = "CellSave";
            this.CellSave.UseVisualStyleBackColor = true;
            this.CellSave.Click += new System.EventHandler(this.CellSave_Click);
            // 
            // FormImageCut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 321);
            this.Controls.Add(this.splitContainerBase);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.PanelMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "FormImageCut";
            this.Text = "イメージカッター";
            this.Load += new System.EventHandler(this.FormImageCut_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormImageCut_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormImageCut_KeyUp);
            this.panel.ResumeLayout(false);
            this.PanelMenu.ResumeLayout(false);
            this.GridMenuBase.ResumeLayout(false);
            this.GridMenuBase.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UD_DivY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UD_DivX)).EndInit();
            this.splitContainerBase.Panel1.ResumeLayout(false);
            this.splitContainerBase.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBase)).EndInit();
            this.splitContainerBase.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.Panel panel_Image;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel PanelMenu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox_Magnet;
        private System.Windows.Forms.Button button_Cut;
        private System.Windows.Forms.Panel GridMenuBase;
        private System.Windows.Forms.Panel panel_ColorGrid;
        private System.Windows.Forms.CheckBox checkBox_Grid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel_ColorRect;
        private System.Windows.Forms.ComboBox comboBox_Mag;
        private System.Windows.Forms.SplitContainer splitContainerBase;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_CellDelete;
        private System.Windows.Forms.Panel panel_CellList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown UD_DivY;
        private System.Windows.Forms.NumericUpDown UD_DivX;
        private System.Windows.Forms.Button button_Divid;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.Button CellSave;
    }
}