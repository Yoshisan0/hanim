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
            this.panel_PreView = new System.Windows.Forms.Panel();
            this.button_BackColor = new System.Windows.Forms.Button();
            this.panel_ToolBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_ToolBase
            // 
            this.panel_ToolBase.Controls.Add(this.button_BackColor);
            this.panel_ToolBase.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_ToolBase.Location = new System.Drawing.Point(0, 0);
            this.panel_ToolBase.Name = "panel_ToolBase";
            this.panel_ToolBase.Size = new System.Drawing.Size(688, 30);
            this.panel_ToolBase.TabIndex = 1;
            // 
            // panel_PreView
            // 
            this.panel_PreView.BackColor = global::PrjHikariwoAnim.Properties.Settings.Default.FormRateGraph_BackColor;
            this.panel_PreView.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::PrjHikariwoAnim.Properties.Settings.Default, "FormRateGraph_BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.panel_PreView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_PreView.Location = new System.Drawing.Point(0, 30);
            this.panel_PreView.Name = "panel_PreView";
            this.panel_PreView.Size = new System.Drawing.Size(688, 434);
            this.panel_PreView.TabIndex = 0;
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
            this.ClientSize = new System.Drawing.Size(688, 464);
            this.Controls.Add(this.panel_PreView);
            this.Controls.Add(this.panel_ToolBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormRateGraph";
            this.Text = "レートグラフ";
            this.TopMost = true;
            this.panel_ToolBase.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_PreView;
        private System.Windows.Forms.Panel panel_ToolBase;
        private System.Windows.Forms.Button button_BackColor;
    }
}