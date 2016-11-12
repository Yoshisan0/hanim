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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel_ToolBase = new System.Windows.Forms.Panel();
            this.panel_PreView = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel_ToolBase);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel_PreView);
            this.splitContainer1.Size = new System.Drawing.Size(688, 464);
            this.splitContainer1.SplitterDistance = 229;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel_ToolBase
            // 
            this.panel_ToolBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ToolBase.Location = new System.Drawing.Point(0, 0);
            this.panel_ToolBase.Name = "panel_ToolBase";
            this.panel_ToolBase.Size = new System.Drawing.Size(688, 229);
            this.panel_ToolBase.TabIndex = 0;
            // 
            // panel_PreView
            // 
            this.panel_PreView.BackColor = System.Drawing.Color.Black;
            this.panel_PreView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_PreView.Location = new System.Drawing.Point(0, 0);
            this.panel_PreView.Name = "panel_PreView";
            this.panel_PreView.Size = new System.Drawing.Size(688, 231);
            this.panel_PreView.TabIndex = 0;
            // 
            // FormRateGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 464);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormRateGraph";
            this.Text = "レートグラフ";
            this.TopMost = true;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel_ToolBase;
        private System.Windows.Forms.Panel panel_PreView;
    }
}