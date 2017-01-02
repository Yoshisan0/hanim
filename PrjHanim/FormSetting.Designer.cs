namespace PrjHikariwoAnim
{
    partial class FormSetting
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label_CenterLineColor = new System.Windows.Forms.Label();
            this.panel_MainCenterLineColor = new System.Windows.Forms.Panel();
            this.label_GridColor = new System.Windows.Forms.Label();
            this.panel_MainGridColor = new System.Windows.Forms.Panel();
            this.panel_MainBackColor = new System.Windows.Forms.Panel();
            this.label_BackColor = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_RateGraphForceColor = new System.Windows.Forms.Panel();
            this.panel_RateGraphGraphColor = new System.Windows.Forms.Panel();
            this.panel_RateGraphCenterLineColor = new System.Windows.Forms.Panel();
            this.panel_RateGraphGridColor = new System.Windows.Forms.Panel();
            this.panel_RateGraphBackColor = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Apply = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Controls.Add(this.tabPage6);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(600, 385);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label_CenterLineColor);
            this.tabPage1.Controls.Add(this.panel_MainCenterLineColor);
            this.tabPage1.Controls.Add(this.label_GridColor);
            this.tabPage1.Controls.Add(this.panel_MainGridColor);
            this.tabPage1.Controls.Add(this.panel_MainBackColor);
            this.tabPage1.Controls.Add(this.label_BackColor);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(592, 359);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label_CenterLineColor
            // 
            this.label_CenterLineColor.AutoSize = true;
            this.label_CenterLineColor.Location = new System.Drawing.Point(6, 72);
            this.label_CenterLineColor.Name = "label_CenterLineColor";
            this.label_CenterLineColor.Size = new System.Drawing.Size(90, 12);
            this.label_CenterLineColor.TabIndex = 5;
            this.label_CenterLineColor.Text = "Center line color";
            // 
            // panel_MainCenterLineColor
            // 
            this.panel_MainCenterLineColor.BackColor = System.Drawing.Color.DarkRed;
            this.panel_MainCenterLineColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_MainCenterLineColor.Location = new System.Drawing.Point(102, 66);
            this.panel_MainCenterLineColor.Name = "panel_MainCenterLineColor";
            this.panel_MainCenterLineColor.Size = new System.Drawing.Size(75, 24);
            this.panel_MainCenterLineColor.TabIndex = 4;
            this.panel_MainCenterLineColor.Click += new System.EventHandler(this.panel_MainCenterLineColor_Click);
            // 
            // label_GridColor
            // 
            this.label_GridColor.AutoSize = true;
            this.label_GridColor.Location = new System.Drawing.Point(6, 42);
            this.label_GridColor.Name = "label_GridColor";
            this.label_GridColor.Size = new System.Drawing.Size(55, 12);
            this.label_GridColor.TabIndex = 3;
            this.label_GridColor.Text = "Grid color";
            // 
            // panel_MainGridColor
            // 
            this.panel_MainGridColor.BackColor = System.Drawing.Color.DarkGreen;
            this.panel_MainGridColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_MainGridColor.Location = new System.Drawing.Point(102, 36);
            this.panel_MainGridColor.Name = "panel_MainGridColor";
            this.panel_MainGridColor.Size = new System.Drawing.Size(75, 24);
            this.panel_MainGridColor.TabIndex = 2;
            this.panel_MainGridColor.Click += new System.EventHandler(this.panel_MainGridColor_Click);
            // 
            // panel_MainBackColor
            // 
            this.panel_MainBackColor.BackColor = System.Drawing.Color.Black;
            this.panel_MainBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_MainBackColor.Location = new System.Drawing.Point(102, 6);
            this.panel_MainBackColor.Name = "panel_MainBackColor";
            this.panel_MainBackColor.Size = new System.Drawing.Size(75, 24);
            this.panel_MainBackColor.TabIndex = 1;
            this.panel_MainBackColor.Click += new System.EventHandler(this.panel_MainBackColor_Click);
            // 
            // label_BackColor
            // 
            this.label_BackColor.AutoSize = true;
            this.label_BackColor.Location = new System.Drawing.Point(6, 12);
            this.label_BackColor.Name = "label_BackColor";
            this.label_BackColor.Size = new System.Drawing.Size(60, 12);
            this.label_BackColor.TabIndex = 0;
            this.label_BackColor.Text = "Back color";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.panel_RateGraphForceColor);
            this.tabPage3.Controls.Add(this.panel_RateGraphGraphColor);
            this.tabPage3.Controls.Add(this.panel_RateGraphCenterLineColor);
            this.tabPage3.Controls.Add(this.panel_RateGraphGridColor);
            this.tabPage3.Controls.Add(this.panel_RateGraphBackColor);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(592, 359);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Rate graph";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "Force color";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Graph color";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Center line color";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Grid color";
            // 
            // panel_RateGraphForceColor
            // 
            this.panel_RateGraphForceColor.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel_RateGraphForceColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_RateGraphForceColor.Location = new System.Drawing.Point(102, 126);
            this.panel_RateGraphForceColor.Name = "panel_RateGraphForceColor";
            this.panel_RateGraphForceColor.Size = new System.Drawing.Size(75, 24);
            this.panel_RateGraphForceColor.TabIndex = 7;
            this.panel_RateGraphForceColor.Click += new System.EventHandler(this.panel_RateGraphForceColor_Click);
            // 
            // panel_RateGraphGraphColor
            // 
            this.panel_RateGraphGraphColor.BackColor = System.Drawing.Color.Lime;
            this.panel_RateGraphGraphColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_RateGraphGraphColor.Location = new System.Drawing.Point(102, 96);
            this.panel_RateGraphGraphColor.Name = "panel_RateGraphGraphColor";
            this.panel_RateGraphGraphColor.Size = new System.Drawing.Size(75, 24);
            this.panel_RateGraphGraphColor.TabIndex = 6;
            this.panel_RateGraphGraphColor.Click += new System.EventHandler(this.panel_RateGraphGraphColor_Click);
            // 
            // panel_RateGraphCenterLineColor
            // 
            this.panel_RateGraphCenterLineColor.BackColor = System.Drawing.Color.DarkRed;
            this.panel_RateGraphCenterLineColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_RateGraphCenterLineColor.Location = new System.Drawing.Point(102, 66);
            this.panel_RateGraphCenterLineColor.Name = "panel_RateGraphCenterLineColor";
            this.panel_RateGraphCenterLineColor.Size = new System.Drawing.Size(75, 24);
            this.panel_RateGraphCenterLineColor.TabIndex = 5;
            this.panel_RateGraphCenterLineColor.Click += new System.EventHandler(this.panel_RateGraphCenterLineColor_Click);
            // 
            // panel_RateGraphGridColor
            // 
            this.panel_RateGraphGridColor.BackColor = System.Drawing.Color.DarkGreen;
            this.panel_RateGraphGridColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_RateGraphGridColor.Location = new System.Drawing.Point(102, 36);
            this.panel_RateGraphGridColor.Name = "panel_RateGraphGridColor";
            this.panel_RateGraphGridColor.Size = new System.Drawing.Size(75, 24);
            this.panel_RateGraphGridColor.TabIndex = 4;
            this.panel_RateGraphGridColor.Click += new System.EventHandler(this.panel_RateGraphGridColor_Click);
            // 
            // panel_RateGraphBackColor
            // 
            this.panel_RateGraphBackColor.BackColor = System.Drawing.Color.Black;
            this.panel_RateGraphBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_RateGraphBackColor.Location = new System.Drawing.Point(102, 6);
            this.panel_RateGraphBackColor.Name = "panel_RateGraphBackColor";
            this.panel_RateGraphBackColor.Size = new System.Drawing.Size(75, 24);
            this.panel_RateGraphBackColor.TabIndex = 3;
            this.panel_RateGraphBackColor.Click += new System.EventHandler(this.panel_RateGraphBackColor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Back color";
            // 
            // button_Apply
            // 
            this.button_Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Apply.Location = new System.Drawing.Point(533, 406);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new System.Drawing.Size(75, 23);
            this.button_Apply.TabIndex = 1;
            this.button_Apply.Text = "Apply";
            this.button_Apply.UseVisualStyleBackColor = true;
            this.button_Apply.Click += new System.EventHandler(this.button_Apply_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(592, 359);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Control";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(592, 359);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Image cutter";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(592, 359);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "Image list";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(592, 359);
            this.tabPage6.TabIndex = 6;
            this.tabPage6.Text = "Cell list";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.button_Apply);
            this.Controls.Add(this.tabControl);
            this.Name = "FormSetting";
            this.Text = "Setting";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label_BackColor;
        private System.Windows.Forms.Label label_GridColor;
        private System.Windows.Forms.Panel panel_MainGridColor;
        private System.Windows.Forms.Panel panel_MainBackColor;
        private System.Windows.Forms.Label label_CenterLineColor;
        private System.Windows.Forms.Panel panel_MainCenterLineColor;
        private System.Windows.Forms.Panel panel_RateGraphBackColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_RateGraphForceColor;
        private System.Windows.Forms.Panel panel_RateGraphGraphColor;
        private System.Windows.Forms.Panel panel_RateGraphCenterLineColor;
        private System.Windows.Forms.Panel panel_RateGraphGridColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
    }
}