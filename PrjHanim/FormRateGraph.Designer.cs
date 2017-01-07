namespace PrjHikariwoAnim
{
    partial class FormRateGraph
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
            this.panel_ToolBase = new System.Windows.Forms.Panel();
            this.button_Rate3 = new System.Windows.Forms.Button();
            this.button_Rate2 = new System.Windows.Forms.Button();
            this.button_Rate1 = new System.Windows.Forms.Button();
            this.Spaceer1 = new System.Windows.Forms.Panel();
            this.button_ColorCurrent = new System.Windows.Forms.Button();
            this.button_ColorGrid = new System.Windows.Forms.Button();
            this.checkBox_GridCheck = new System.Windows.Forms.CheckBox();
            this.button_ColorLine = new System.Windows.Forms.Button();
            this.button_ColorGraph = new System.Windows.Forms.Button();
            this.button_ColorBack = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel_PreView = new System.Windows.Forms.Panel();
            this.panel_ToolBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_ToolBase
            // 
            this.panel_ToolBase.Controls.Add(this.button_Rate3);
            this.panel_ToolBase.Controls.Add(this.button_Rate2);
            this.panel_ToolBase.Controls.Add(this.button_Rate1);
            this.panel_ToolBase.Controls.Add(this.Spaceer1);
            this.panel_ToolBase.Controls.Add(this.button_ColorCurrent);
            this.panel_ToolBase.Controls.Add(this.button_ColorGrid);
            this.panel_ToolBase.Controls.Add(this.checkBox_GridCheck);
            this.panel_ToolBase.Controls.Add(this.button_ColorLine);
            this.panel_ToolBase.Controls.Add(this.button_ColorGraph);
            this.panel_ToolBase.Controls.Add(this.button_ColorBack);
            this.panel_ToolBase.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_ToolBase.Location = new System.Drawing.Point(0, 0);
            this.panel_ToolBase.Name = "panel_ToolBase";
            this.panel_ToolBase.Size = new System.Drawing.Size(552, 30);
            this.panel_ToolBase.TabIndex = 1;
            // 
            // button_Rate3
            // 
            this.button_Rate3.BackColor = System.Drawing.SystemColors.Control;
            this.button_Rate3.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Rate3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Rate3.Image = global::PrjHikariwoAnim.Properties.Resources.rate3;
            this.button_Rate3.Location = new System.Drawing.Point(250, 0);
            this.button_Rate3.Name = "button_Rate3";
            this.button_Rate3.Size = new System.Drawing.Size(30, 30);
            this.button_Rate3.TabIndex = 37;
            this.toolTip1.SetToolTip(this.button_Rate3, "定型加速");
            this.button_Rate3.UseVisualStyleBackColor = false;
            this.button_Rate3.Click += new System.EventHandler(this.button_Rate3_Click);
            // 
            // button_Rate2
            // 
            this.button_Rate2.BackColor = System.Drawing.SystemColors.Control;
            this.button_Rate2.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Rate2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Rate2.Image = global::PrjHikariwoAnim.Properties.Resources.rate2;
            this.button_Rate2.Location = new System.Drawing.Point(220, 0);
            this.button_Rate2.Name = "button_Rate2";
            this.button_Rate2.Size = new System.Drawing.Size(30, 30);
            this.button_Rate2.TabIndex = 36;
            this.toolTip1.SetToolTip(this.button_Rate2, "定型減速");
            this.button_Rate2.UseVisualStyleBackColor = false;
            this.button_Rate2.Click += new System.EventHandler(this.button_Rate2_Click);
            // 
            // button_Rate1
            // 
            this.button_Rate1.BackColor = System.Drawing.SystemColors.Control;
            this.button_Rate1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Rate1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Rate1.Image = global::PrjHikariwoAnim.Properties.Resources.rate1;
            this.button_Rate1.Location = new System.Drawing.Point(190, 0);
            this.button_Rate1.Name = "button_Rate1";
            this.button_Rate1.Size = new System.Drawing.Size(30, 30);
            this.button_Rate1.TabIndex = 35;
            this.toolTip1.SetToolTip(this.button_Rate1, "定型直線");
            this.button_Rate1.UseVisualStyleBackColor = false;
            this.button_Rate1.Click += new System.EventHandler(this.button_Rate1_Click);
            // 
            // Spaceer1
            // 
            this.Spaceer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.Spaceer1.Location = new System.Drawing.Point(180, 0);
            this.Spaceer1.Name = "Spaceer1";
            this.Spaceer1.Size = new System.Drawing.Size(10, 30);
            this.Spaceer1.TabIndex = 34;
            // 
            // button_ColorCurrent
            // 
            this.button_ColorCurrent.BackColor = global::PrjHikariwoAnim.Properties.Settings.Default.FormRateGraph_ColorCurrent;
            this.button_ColorCurrent.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::PrjHikariwoAnim.Properties.Settings.Default, "FormRateGraph_ColorCurrent", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.button_ColorCurrent.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_ColorCurrent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ColorCurrent.Location = new System.Drawing.Point(150, 0);
            this.button_ColorCurrent.Name = "button_ColorCurrent";
            this.button_ColorCurrent.Size = new System.Drawing.Size(30, 30);
            this.button_ColorCurrent.TabIndex = 33;
            this.toolTip1.SetToolTip(this.button_ColorCurrent, "カレントフレーム色");
            this.button_ColorCurrent.UseVisualStyleBackColor = false;
            this.button_ColorCurrent.Click += new System.EventHandler(this.button_ColorCurrent_Click);
            // 
            // button_ColorGrid
            // 
            this.button_ColorGrid.BackColor = global::PrjHikariwoAnim.Properties.Settings.Default.FormRateGraph_ColorGrid;
            this.button_ColorGrid.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::PrjHikariwoAnim.Properties.Settings.Default, "FormRateGraph_ColorGrid", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.button_ColorGrid.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_ColorGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ColorGrid.Location = new System.Drawing.Point(120, 0);
            this.button_ColorGrid.Name = "button_ColorGrid";
            this.button_ColorGrid.Size = new System.Drawing.Size(30, 30);
            this.button_ColorGrid.TabIndex = 28;
            this.toolTip1.SetToolTip(this.button_ColorGrid, "グリッド色");
            this.button_ColorGrid.UseVisualStyleBackColor = false;
            this.button_ColorGrid.Click += new System.EventHandler(this.button_ColorGrid_Click);
            // 
            // checkBox_GridCheck
            // 
            this.checkBox_GridCheck.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_GridCheck.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.checkBox_GridCheck.BackgroundImage = global::PrjHikariwoAnim.Properties.Resources.grid;
            this.checkBox_GridCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBox_GridCheck.Checked = global::PrjHikariwoAnim.Properties.Settings.Default.FormRateGraph_DrawGrid;
            this.checkBox_GridCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_GridCheck.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::PrjHikariwoAnim.Properties.Settings.Default, "FormRateGraph_DrawGrid", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_GridCheck.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_GridCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_GridCheck.Location = new System.Drawing.Point(90, 0);
            this.checkBox_GridCheck.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_GridCheck.Name = "checkBox_GridCheck";
            this.checkBox_GridCheck.Size = new System.Drawing.Size(30, 30);
            this.checkBox_GridCheck.TabIndex = 27;
            this.toolTip1.SetToolTip(this.checkBox_GridCheck, "グリッドオンオフ");
            this.checkBox_GridCheck.UseVisualStyleBackColor = false;
            this.checkBox_GridCheck.CheckedChanged += new System.EventHandler(this.checkBox_GridCheck_CheckedChanged);
            // 
            // button_ColorLine
            // 
            this.button_ColorLine.BackColor = global::PrjHikariwoAnim.Properties.Settings.Default.FormRateGraph_LineColor;
            this.button_ColorLine.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::PrjHikariwoAnim.Properties.Settings.Default, "FormRateGraph_LineColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.button_ColorLine.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_ColorLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ColorLine.Location = new System.Drawing.Point(60, 0);
            this.button_ColorLine.Name = "button_ColorLine";
            this.button_ColorLine.Size = new System.Drawing.Size(30, 30);
            this.button_ColorLine.TabIndex = 26;
            this.toolTip1.SetToolTip(this.button_ColorLine, "ライン色");
            this.button_ColorLine.UseVisualStyleBackColor = false;
            this.button_ColorLine.Click += new System.EventHandler(this.button_ColorLine_Click);
            // 
            // button_ColorGraph
            // 
            this.button_ColorGraph.BackColor = global::PrjHikariwoAnim.Properties.Settings.Default.FormRateGraph_GraphColor;
            this.button_ColorGraph.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::PrjHikariwoAnim.Properties.Settings.Default, "FormRateGraph_GraphColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.button_ColorGraph.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_ColorGraph.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ColorGraph.Location = new System.Drawing.Point(30, 0);
            this.button_ColorGraph.Name = "button_ColorGraph";
            this.button_ColorGraph.Size = new System.Drawing.Size(30, 30);
            this.button_ColorGraph.TabIndex = 15;
            this.toolTip1.SetToolTip(this.button_ColorGraph, "グリップ色");
            this.button_ColorGraph.UseVisualStyleBackColor = false;
            this.button_ColorGraph.Click += new System.EventHandler(this.button_ColorGraph_Click);
            // 
            // button_ColorBack
            // 
            this.button_ColorBack.BackColor = global::PrjHikariwoAnim.Properties.Settings.Default.FormRateGraph_BackColor;
            this.button_ColorBack.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::PrjHikariwoAnim.Properties.Settings.Default, "FormRateGraph_BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.button_ColorBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_ColorBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ColorBack.Location = new System.Drawing.Point(0, 0);
            this.button_ColorBack.Name = "button_ColorBack";
            this.button_ColorBack.Size = new System.Drawing.Size(30, 30);
            this.button_ColorBack.TabIndex = 14;
            this.toolTip1.SetToolTip(this.button_ColorBack, "背景色");
            this.button_ColorBack.UseVisualStyleBackColor = false;
            this.button_ColorBack.Click += new System.EventHandler(this.button_ColorBack_Click);
            // 
            // panel_PreView
            // 
            this.panel_PreView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_PreView.BackColor = global::PrjHikariwoAnim.Properties.Settings.Default.FormRateGraph_BackColor;
            this.panel_PreView.Location = new System.Drawing.Point(6, 36);
            this.panel_PreView.Name = "panel_PreView";
            this.panel_PreView.Size = new System.Drawing.Size(540, 540);
            this.panel_PreView.TabIndex = 0;
            this.panel_PreView.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_PreView_Paint);
            this.panel_PreView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_PreView_MouseDown);
            this.panel_PreView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_PreView_MouseMove);
            this.panel_PreView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_PreView_MouseUp);
            // 
            // FormRateGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 582);
            this.Controls.Add(this.panel_PreView);
            this.Controls.Add(this.panel_ToolBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormRateGraph";
            this.Text = "Rate graph";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRateGraph_FormClosing);
            this.Load += new System.EventHandler(this.FormRateGraph_Load);
            this.Resize += new System.EventHandler(this.FormRateGraph_Resize);
            this.panel_ToolBase.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_PreView;
        private System.Windows.Forms.Panel panel_ToolBase;
        private System.Windows.Forms.Button button_ColorBack;
        private System.Windows.Forms.Button button_ColorGraph;
        private System.Windows.Forms.Button button_ColorGrid;
        private System.Windows.Forms.CheckBox checkBox_GridCheck;
        private System.Windows.Forms.Button button_ColorLine;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_Rate3;
        private System.Windows.Forms.Button button_Rate2;
        private System.Windows.Forms.Button button_Rate1;
        private System.Windows.Forms.Panel Spaceer1;
        private System.Windows.Forms.Button button_ColorCurrent;
    }
}
