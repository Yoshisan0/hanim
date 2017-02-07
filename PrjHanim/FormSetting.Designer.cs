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
            this.panel2 = new System.Windows.Forms.Panel();
            this.HistoryClear = new System.Windows.Forms.Button();
            this.check_AutoReload = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label_BackColor = new System.Windows.Forms.Label();
            this.label_CenterLineColor = new System.Windows.Forms.Label();
            this.panel_MainBackColor = new System.Windows.Forms.Panel();
            this.panel_MainCenterLineColor = new System.Windows.Forms.Panel();
            this.panel_MainGridColor = new System.Windows.Forms.Panel();
            this.label_GridColor = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
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
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.button_Apply = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button_cancel = new System.Windows.Forms.Button();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_Dir = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Controls.Add(this.tabPage6);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(474, 305);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.HistoryClear);
            this.tabPage1.Controls.Add(this.check_AutoReload);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(466, 279);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.textBox_Dir);
            this.panel2.Controls.Add(this.textBox_name);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(6, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(228, 185);
            this.panel2.TabIndex = 8;
            // 
            // HistoryClear
            // 
            this.HistoryClear.Location = new System.Drawing.Point(8, 250);
            this.HistoryClear.Name = "HistoryClear";
            this.HistoryClear.Size = new System.Drawing.Size(127, 23);
            this.HistoryClear.TabIndex = 5;
            this.HistoryClear.Text = "Clear FileHistory";
            this.HistoryClear.UseVisualStyleBackColor = true;
            this.HistoryClear.Click += new System.EventHandler(this.button1_Click);
            // 
            // check_AutoReload
            // 
            this.check_AutoReload.AutoSize = true;
            this.check_AutoReload.Location = new System.Drawing.Point(6, 197);
            this.check_AutoReload.Name = "check_AutoReload";
            this.check_AutoReload.Size = new System.Drawing.Size(127, 16);
            this.check_AutoReload.TabIndex = 4;
            this.check_AutoReload.Text = "Project Auto Reload";
            this.check_AutoReload.UseVisualStyleBackColor = true;
            this.check_AutoReload.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.label8.Location = new System.Drawing.Point(11, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(5, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "Project";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label_BackColor);
            this.panel1.Controls.Add(this.label_CenterLineColor);
            this.panel1.Controls.Add(this.panel_MainBackColor);
            this.panel1.Controls.Add(this.panel_MainCenterLineColor);
            this.panel1.Controls.Add(this.panel_MainGridColor);
            this.panel1.Controls.Add(this.label_GridColor);
            this.panel1.Location = new System.Drawing.Point(240, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 185);
            this.panel1.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "PreViewScreen";
            // 
            // label_BackColor
            // 
            this.label_BackColor.AutoSize = true;
            this.label_BackColor.Location = new System.Drawing.Point(14, 32);
            this.label_BackColor.Name = "label_BackColor";
            this.label_BackColor.Size = new System.Drawing.Size(60, 12);
            this.label_BackColor.TabIndex = 0;
            this.label_BackColor.Text = "Back color";
            // 
            // label_CenterLineColor
            // 
            this.label_CenterLineColor.AutoSize = true;
            this.label_CenterLineColor.Location = new System.Drawing.Point(14, 88);
            this.label_CenterLineColor.Name = "label_CenterLineColor";
            this.label_CenterLineColor.Size = new System.Drawing.Size(90, 12);
            this.label_CenterLineColor.TabIndex = 5;
            this.label_CenterLineColor.Text = "Center line color";
            // 
            // panel_MainBackColor
            // 
            this.panel_MainBackColor.BackColor = System.Drawing.Color.Black;
            this.panel_MainBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_MainBackColor.Location = new System.Drawing.Point(110, 29);
            this.panel_MainBackColor.Name = "panel_MainBackColor";
            this.panel_MainBackColor.Size = new System.Drawing.Size(49, 22);
            this.panel_MainBackColor.TabIndex = 1;
            this.panel_MainBackColor.Click += new System.EventHandler(this.panel_Color_Click);
            // 
            // panel_MainCenterLineColor
            // 
            this.panel_MainCenterLineColor.BackColor = System.Drawing.Color.DarkRed;
            this.panel_MainCenterLineColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_MainCenterLineColor.Location = new System.Drawing.Point(110, 80);
            this.panel_MainCenterLineColor.Name = "panel_MainCenterLineColor";
            this.panel_MainCenterLineColor.Size = new System.Drawing.Size(49, 20);
            this.panel_MainCenterLineColor.TabIndex = 4;
            this.panel_MainCenterLineColor.Click += new System.EventHandler(this.panel_Color_Click);
            // 
            // panel_MainGridColor
            // 
            this.panel_MainGridColor.BackColor = System.Drawing.Color.DarkGreen;
            this.panel_MainGridColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_MainGridColor.Location = new System.Drawing.Point(110, 54);
            this.panel_MainGridColor.Name = "panel_MainGridColor";
            this.panel_MainGridColor.Size = new System.Drawing.Size(49, 20);
            this.panel_MainGridColor.TabIndex = 2;
            this.panel_MainGridColor.Click += new System.EventHandler(this.panel_Color_Click);
            // 
            // label_GridColor
            // 
            this.label_GridColor.AutoSize = true;
            this.label_GridColor.Location = new System.Drawing.Point(14, 62);
            this.label_GridColor.Name = "label_GridColor";
            this.label_GridColor.Size = new System.Drawing.Size(55, 12);
            this.label_GridColor.TabIndex = 3;
            this.label_GridColor.Text = "Grid color";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(466, 279);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Control";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.tabPage3.Size = new System.Drawing.Size(466, 279);
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
            this.panel_RateGraphForceColor.Click += new System.EventHandler(this.panel_Color_Click);
            // 
            // panel_RateGraphGraphColor
            // 
            this.panel_RateGraphGraphColor.BackColor = System.Drawing.Color.Lime;
            this.panel_RateGraphGraphColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_RateGraphGraphColor.Location = new System.Drawing.Point(102, 96);
            this.panel_RateGraphGraphColor.Name = "panel_RateGraphGraphColor";
            this.panel_RateGraphGraphColor.Size = new System.Drawing.Size(75, 24);
            this.panel_RateGraphGraphColor.TabIndex = 6;
            this.panel_RateGraphGraphColor.Click += new System.EventHandler(this.panel_Color_Click);
            // 
            // panel_RateGraphCenterLineColor
            // 
            this.panel_RateGraphCenterLineColor.BackColor = System.Drawing.Color.DarkRed;
            this.panel_RateGraphCenterLineColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_RateGraphCenterLineColor.Location = new System.Drawing.Point(102, 66);
            this.panel_RateGraphCenterLineColor.Name = "panel_RateGraphCenterLineColor";
            this.panel_RateGraphCenterLineColor.Size = new System.Drawing.Size(75, 24);
            this.panel_RateGraphCenterLineColor.TabIndex = 5;
            this.panel_RateGraphCenterLineColor.Click += new System.EventHandler(this.panel_Color_Click);
            // 
            // panel_RateGraphGridColor
            // 
            this.panel_RateGraphGridColor.BackColor = System.Drawing.Color.DarkGreen;
            this.panel_RateGraphGridColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_RateGraphGridColor.Location = new System.Drawing.Point(102, 36);
            this.panel_RateGraphGridColor.Name = "panel_RateGraphGridColor";
            this.panel_RateGraphGridColor.Size = new System.Drawing.Size(75, 24);
            this.panel_RateGraphGridColor.TabIndex = 4;
            this.panel_RateGraphGridColor.Click += new System.EventHandler(this.panel_Color_Click);
            // 
            // panel_RateGraphBackColor
            // 
            this.panel_RateGraphBackColor.BackColor = System.Drawing.Color.Black;
            this.panel_RateGraphBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_RateGraphBackColor.Location = new System.Drawing.Point(102, 6);
            this.panel_RateGraphBackColor.Name = "panel_RateGraphBackColor";
            this.panel_RateGraphBackColor.Size = new System.Drawing.Size(75, 24);
            this.panel_RateGraphBackColor.TabIndex = 3;
            this.panel_RateGraphBackColor.Click += new System.EventHandler(this.panel_Color_Click);
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
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(466, 279);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Image cutter";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(466, 279);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "Image list";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(466, 279);
            this.tabPage6.TabIndex = 6;
            this.tabPage6.Text = "Cell list";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // button_Apply
            // 
            this.button_Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Apply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Apply.Location = new System.Drawing.Point(376, 3);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new System.Drawing.Size(91, 23);
            this.button_Apply.TabIndex = 1;
            this.button_Apply.Text = "Apply";
            this.button_Apply.UseVisualStyleBackColor = true;
            this.button_Apply.Click += new System.EventHandler(this.button_Apply_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button_cancel);
            this.panel3.Controls.Add(this.button_Apply);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 305);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(474, 30);
            this.panel3.TabIndex = 2;
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(311, 3);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(59, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(52, 29);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(173, 19);
            this.textBox_name.TabIndex = 6;
            // 
            // textBox_Dir
            // 
            this.textBox_Dir.Location = new System.Drawing.Point(52, 53);
            this.textBox_Dir.Multiline = true;
            this.textBox_Dir.Name = "textBox_Dir";
            this.textBox_Dir.Size = new System.Drawing.Size(173, 65);
            this.textBox_Dir.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 12);
            this.label11.TabIndex = 9;
            this.label11.Text = "Memo";
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 335);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormSetting";
            this.ShowIcon = false;
            this.Text = "Setting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSetting_FormClosing);
            this.Load += new System.EventHandler(this.FormSetting_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel3.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox check_AutoReload;
        private System.Windows.Forms.Button HistoryClear;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_Dir;
        private System.Windows.Forms.TextBox textBox_name;
    }
}