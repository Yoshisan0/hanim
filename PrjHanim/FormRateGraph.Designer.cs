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
            this.panel_ToolBase = new System.Windows.Forms.Panel();
            this.button_Rate3 = new System.Windows.Forms.Button();
            this.button_Rate2 = new System.Windows.Forms.Button();
            this.button_Rate1 = new System.Windows.Forms.Button();
            this.panel_PreView = new System.Windows.Forms.Panel();
            this.button_GridColor = new System.Windows.Forms.Button();
            this.checkBox_GridCheck = new System.Windows.Forms.CheckBox();
            this.button_GraphColor = new System.Windows.Forms.Button();
            this.button_BackColor = new System.Windows.Forms.Button();
            this.panel_ToolBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_ToolBase
            // 
            this.panel_ToolBase.Controls.Add(this.button_Rate3);
            this.panel_ToolBase.Controls.Add(this.button_Rate2);
            this.panel_ToolBase.Controls.Add(this.button_Rate1);
            this.panel_ToolBase.Controls.Add(this.button_GridColor);
            this.panel_ToolBase.Controls.Add(this.checkBox_GridCheck);
            this.panel_ToolBase.Controls.Add(this.button_GraphColor);
            this.panel_ToolBase.Controls.Add(this.button_BackColor);
            this.panel_ToolBase.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_ToolBase.Location = new System.Drawing.Point(0, 0);
            this.panel_ToolBase.Name = "panel_ToolBase";
            this.panel_ToolBase.Size = new System.Drawing.Size(540, 30);
            this.panel_ToolBase.TabIndex = 1;
            // 
            // button_Rate3
            // 
            this.button_Rate3.BackColor = System.Drawing.SystemColors.Control;
            this.button_Rate3.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Rate3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//          this.button_Rate3.Image = global::PrjHikariwoAnim.Properties.Resources.rate3;
            this.button_Rate3.Location = new System.Drawing.Point(180, 0);
            this.button_Rate3.Name = "button_Rate3";
            this.button_Rate3.Size = new System.Drawing.Size(30, 30);
            this.button_Rate3.TabIndex = 20;
            this.button_Rate3.UseVisualStyleBackColor = false;
            this.button_Rate3.Click += new System.EventHandler(this.button_Rate3_Click);
            // 
            // button_Rate2
            // 
            this.button_Rate2.BackColor = System.Drawing.SystemColors.Control;
            this.button_Rate2.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Rate2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//          this.button_Rate2.Image = global::PrjHikariwoAnim.Properties.Resources.rate2;
            this.button_Rate2.Location = new System.Drawing.Point(150, 0);
            this.button_Rate2.Name = "button_Rate2";
            this.button_Rate2.Size = new System.Drawing.Size(30, 30);
            this.button_Rate2.TabIndex = 19;
            this.button_Rate2.UseVisualStyleBackColor = false;
            this.button_Rate2.Click += new System.EventHandler(this.button_Rate2_Click);
            // 
            // button_Rate1
            // 
            this.button_Rate1.BackColor = System.Drawing.SystemColors.Control;
            this.button_Rate1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Rate1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//          this.button_Rate1.Image = global::PrjHikariwoAnim.Properties.Resources.rate1;
            this.button_Rate1.Location = new System.Drawing.Point(120, 0);
            this.button_Rate1.Name = "button_Rate1";
            this.button_Rate1.Size = new System.Drawing.Size(30, 30);
            this.button_Rate1.TabIndex = 18;
            this.button_Rate1.UseVisualStyleBackColor = false;
            this.button_Rate1.Click += new System.EventHandler(this.button_Rate1_Click);
            // 
            // panel_PreView
            // 
            this.panel_PreView.BackColor = global::PrjHikariwoAnim.Properties.Settings.Default.FormRateGraph_BackColor;
            this.panel_PreView.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::PrjHikariwoAnim.Properties.Settings.Default, "FormRateGraph_BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.panel_PreView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_PreView.Location = new System.Drawing.Point(0, 30);
            this.panel_PreView.Name = "panel_PreView";
            this.panel_PreView.Size = new System.Drawing.Size(540, 540);
            this.panel_PreView.TabIndex = 0;
            this.panel_PreView.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_PreView_Paint);
            this.panel_PreView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_PreView_MouseDown);
            this.panel_PreView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_PreView_MouseMove);
            this.panel_PreView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_PreView_MouseUp);
            // 
            // button_GridColor
            // 
            this.button_GridColor.BackColor = global::PrjHikariwoAnim.Properties.Settings.Default.FormRateGraph_ColorGrid;
            this.button_GridColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::PrjHikariwoAnim.Properties.Settings.Default, "FormRateGraph_ColorGrid", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.button_GridColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_GridColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_GridColor.Location = new System.Drawing.Point(90, 0);
            this.button_GridColor.Name = "button_GridColor";
            this.button_GridColor.Size = new System.Drawing.Size(30, 30);
            this.button_GridColor.TabIndex = 17;
            this.button_GridColor.UseVisualStyleBackColor = false;
            this.button_GridColor.Click += new System.EventHandler(this.button_GridColor_Click);
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
            this.checkBox_GridCheck.Location = new System.Drawing.Point(60, 0);
            this.checkBox_GridCheck.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_GridCheck.Name = "checkBox_GridCheck";
            this.checkBox_GridCheck.Size = new System.Drawing.Size(30, 30);
            this.checkBox_GridCheck.TabIndex = 16;
            this.checkBox_GridCheck.UseVisualStyleBackColor = false;
            // 
            // button_GraphColor
            // 
            this.button_GraphColor.BackColor = global::PrjHikariwoAnim.Properties.Settings.Default.FormRateGraph_GraphColor;
            this.button_GraphColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::PrjHikariwoAnim.Properties.Settings.Default, "FormRateGraph_GraphColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.button_GraphColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_GraphColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_GraphColor.Location = new System.Drawing.Point(30, 0);
            this.button_GraphColor.Name = "button_GraphColor";
            this.button_GraphColor.Size = new System.Drawing.Size(30, 30);
            this.button_GraphColor.TabIndex = 15;
            this.button_GraphColor.UseVisualStyleBackColor = false;
            this.button_GraphColor.Click += new System.EventHandler(this.button_GraphColor_Click);
            // 
            // button_BackColor
            // 
            this.button_BackColor.BackColor = global::PrjHikariwoAnim.Properties.Settings.Default.FormRateGraph_BackColor;
            this.button_BackColor.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::PrjHikariwoAnim.Properties.Settings.Default, "FormRateGraph_BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.button_BackColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_BackColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_BackColor.Location = new System.Drawing.Point(0, 0);
            this.button_BackColor.Name = "button_BackColor";
            this.button_BackColor.Size = new System.Drawing.Size(30, 30);
            this.button_BackColor.TabIndex = 14;
            this.button_BackColor.UseVisualStyleBackColor = false;
            this.button_BackColor.Click += new System.EventHandler(this.button_BackColor_Click);
            // 
            // FormRateGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 570);
            this.Controls.Add(this.panel_PreView);
            this.Controls.Add(this.panel_ToolBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormRateGraph";
            this.Text = "レートグラフ";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormRateGraph_Load);
            this.Resize += new System.EventHandler(this.FormRateGraph_Resize);
            this.panel_ToolBase.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_PreView;
        private System.Windows.Forms.Panel panel_ToolBase;
        private System.Windows.Forms.Button button_BackColor;
        private System.Windows.Forms.Button button_GraphColor;
        private System.Windows.Forms.Button button_GridColor;
        private System.Windows.Forms.CheckBox checkBox_GridCheck;
        private System.Windows.Forms.Button button_Rate1;
        private System.Windows.Forms.Button button_Rate2;
        private System.Windows.Forms.Button button_Rate3;
    }
}
