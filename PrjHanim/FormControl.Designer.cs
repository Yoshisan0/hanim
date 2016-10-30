namespace PrjHikariwoAnim
{
    partial class FormControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormControl));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panel_Control = new System.Windows.Forms.Panel();
            this.TreeHeader = new System.Windows.Forms.Panel();
            this.ItemRemove = new System.Windows.Forms.Button();
            this.ItemDown = new System.Windows.Forms.Button();
            this.ItemUp = new System.Windows.Forms.Button();
            this.SubMenuTimeLine = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.キーフレーム追加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.切り取りToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.コピーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上書き貼付けToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.挿入貼付けToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_Time = new System.Windows.Forms.Panel();
            this.LineHeader = new System.Windows.Forms.Panel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.再生ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MediaBase = new System.Windows.Forms.Panel();
            this.ButtonFore = new System.Windows.Forms.Button();
            this.ButtonPlay = new System.Windows.Forms.Button();
            this.ButtonPrev = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.NowFlame = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.MaxFrame = new System.Windows.Forms.NumericUpDown();
            this.Panel_LineControl_Base = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.TreeHeader.SuspendLayout();
            this.SubMenuTimeLine.SuspendLayout();
            this.MediaBase.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NowFlame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxFrame)).BeginInit();
            this.Panel_LineControl_Base.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 51);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.AutoScroll = true;
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.Black;
            this.splitContainer.Panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("splitContainer.Panel1.BackgroundImage")));
            this.splitContainer.Panel1.Controls.Add(this.panel_Control);
            this.splitContainer.Panel1.Controls.Add(this.TreeHeader);
            this.splitContainer.Panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.splitContainer_Panel1_Scroll);
            this.splitContainer.Panel1MinSize = 48;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.AutoScroll = true;
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.Black;
            this.splitContainer.Panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("splitContainer.Panel2.BackgroundImage")));
            this.splitContainer.Panel2.ContextMenuStrip = this.SubMenuTimeLine;
            this.splitContainer.Panel2.Controls.Add(this.panel_Time);
            this.splitContainer.Panel2.Controls.Add(this.LineHeader);
            this.splitContainer.Panel2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.splitContainer_Panel2_Scroll);
            this.splitContainer.Panel2MinSize = 48;
            this.splitContainer.Size = new System.Drawing.Size(611, 124);
            this.splitContainer.SplitterDistance = 145;
            this.splitContainer.TabIndex = 0;
            // 
            // panel_Control
            // 
            this.panel_Control.AllowDrop = true;
            this.panel_Control.AutoScroll = true;
            this.panel_Control.BackColor = System.Drawing.Color.Black;
            this.panel_Control.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_Control.BackgroundImage")));
            this.panel_Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Control.Location = new System.Drawing.Point(0, 20);
            this.panel_Control.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Control.Name = "panel_Control";
            this.panel_Control.Size = new System.Drawing.Size(143, 102);
            this.panel_Control.TabIndex = 1;
            this.panel_Control.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel_Control_DragDrop);
            this.panel_Control.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Control_Paint);
            this.panel_Control.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_Control_MouseClick);
            this.panel_Control.MouseEnter += new System.EventHandler(this.panel_Control_MouseEnter);
            this.panel_Control.MouseLeave += new System.EventHandler(this.panel_Control_MouseLeave);
            this.panel_Control.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_Control_MouseMove);
            // 
            // TreeHeader
            // 
            this.TreeHeader.BackColor = System.Drawing.Color.MidnightBlue;
            this.TreeHeader.Controls.Add(this.ItemRemove);
            this.TreeHeader.Controls.Add(this.ItemDown);
            this.TreeHeader.Controls.Add(this.ItemUp);
            this.TreeHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.TreeHeader.Location = new System.Drawing.Point(0, 0);
            this.TreeHeader.Name = "TreeHeader";
            this.TreeHeader.Size = new System.Drawing.Size(143, 20);
            this.TreeHeader.TabIndex = 2;
            // 
            // ItemRemove
            // 
            this.ItemRemove.Dock = System.Windows.Forms.DockStyle.Left;
            this.ItemRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ItemRemove.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ItemRemove.Location = new System.Drawing.Point(54, 0);
            this.ItemRemove.Name = "ItemRemove";
            this.ItemRemove.Size = new System.Drawing.Size(27, 20);
            this.ItemRemove.TabIndex = 4;
            this.ItemRemove.Text = "🚮";
            this.ItemRemove.UseVisualStyleBackColor = true;
            // 
            // ItemDown
            // 
            this.ItemDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.ItemDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ItemDown.Location = new System.Drawing.Point(27, 0);
            this.ItemDown.Name = "ItemDown";
            this.ItemDown.Size = new System.Drawing.Size(27, 20);
            this.ItemDown.TabIndex = 3;
            this.ItemDown.Text = "▽";
            this.ItemDown.UseVisualStyleBackColor = true;
            // 
            // ItemUp
            // 
            this.ItemUp.Dock = System.Windows.Forms.DockStyle.Left;
            this.ItemUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ItemUp.Location = new System.Drawing.Point(0, 0);
            this.ItemUp.Name = "ItemUp";
            this.ItemUp.Size = new System.Drawing.Size(27, 20);
            this.ItemUp.TabIndex = 2;
            this.ItemUp.Text = "△";
            this.ItemUp.UseVisualStyleBackColor = true;
            // 
            // SubMenuTimeLine
            // 
            this.SubMenuTimeLine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.キーフレーム追加ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripSeparator1,
            this.切り取りToolStripMenuItem,
            this.コピーToolStripMenuItem,
            this.上書き貼付けToolStripMenuItem,
            this.挿入貼付けToolStripMenuItem});
            this.SubMenuTimeLine.Name = "SubMenuTimeLine";
            this.SubMenuTimeLine.Size = new System.Drawing.Size(153, 186);
            // 
            // キーフレーム追加ToolStripMenuItem
            // 
            this.キーフレーム追加ToolStripMenuItem.Name = "キーフレーム追加ToolStripMenuItem";
            this.キーフレーム追加ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.キーフレーム追加ToolStripMenuItem.Text = "キーフレーム登録";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "フレーム削除";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "フレーム挿入";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // 切り取りToolStripMenuItem
            // 
            this.切り取りToolStripMenuItem.Name = "切り取りToolStripMenuItem";
            this.切り取りToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.切り取りToolStripMenuItem.Text = "切取(X)";
            // 
            // コピーToolStripMenuItem
            // 
            this.コピーToolStripMenuItem.Name = "コピーToolStripMenuItem";
            this.コピーToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.コピーToolStripMenuItem.Text = "コピー(C)";
            // 
            // 上書き貼付けToolStripMenuItem
            // 
            this.上書き貼付けToolStripMenuItem.Name = "上書き貼付けToolStripMenuItem";
            this.上書き貼付けToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.上書き貼付けToolStripMenuItem.Text = "貼付上書(V)";
            // 
            // 挿入貼付けToolStripMenuItem
            // 
            this.挿入貼付けToolStripMenuItem.Name = "挿入貼付けToolStripMenuItem";
            this.挿入貼付けToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.挿入貼付けToolStripMenuItem.Text = "貼付挿入(B)";
            // 
            // panel_Time
            // 
            this.panel_Time.AutoScroll = true;
            this.panel_Time.Location = new System.Drawing.Point(0, 20);
            this.panel_Time.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Time.Name = "panel_Time";
            this.panel_Time.Size = new System.Drawing.Size(113, 100);
            this.panel_Time.TabIndex = 0;
            this.panel_Time.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Time_Paint);
            this.panel_Time.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_Time_MouseClick);
            this.panel_Time.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel_Time_MouseDoubleClick);
            this.panel_Time.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Time_MouseDown);
            this.panel_Time.MouseEnter += new System.EventHandler(this.panel_Time_MouseEnter);
            this.panel_Time.MouseLeave += new System.EventHandler(this.panel_Time_MouseLeave);
            this.panel_Time.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_Time_MouseMove);
            this.panel_Time.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_Time_MouseUp);
            // 
            // LineHeader
            // 
            this.LineHeader.BackColor = System.Drawing.Color.MidnightBlue;
            this.LineHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.LineHeader.Location = new System.Drawing.Point(0, 0);
            this.LineHeader.Name = "LineHeader";
            this.LineHeader.Size = new System.Drawing.Size(460, 20);
            this.LineHeader.TabIndex = 1;
            this.LineHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.LineHeader_Paint);
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(611, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // 再生ToolStripMenuItem
            // 
            this.再生ToolStripMenuItem.Name = "再生ToolStripMenuItem";
            this.再生ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.再生ToolStripMenuItem.Text = "再生";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 175);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(611, 31);
            this.panel1.TabIndex = 5;
            // 
            // MediaBase
            // 
            this.MediaBase.Controls.Add(this.ButtonFore);
            this.MediaBase.Controls.Add(this.ButtonPlay);
            this.MediaBase.Controls.Add(this.ButtonPrev);
            this.MediaBase.Dock = System.Windows.Forms.DockStyle.Left;
            this.MediaBase.Location = new System.Drawing.Point(152, 0);
            this.MediaBase.Name = "MediaBase";
            this.MediaBase.Size = new System.Drawing.Size(122, 27);
            this.MediaBase.TabIndex = 12;
            // 
            // ButtonFore
            // 
            this.ButtonFore.BackColor = System.Drawing.Color.DarkGray;
            this.ButtonFore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ButtonFore.Dock = System.Windows.Forms.DockStyle.Left;
            this.ButtonFore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFore.Image = ((System.Drawing.Image)(resources.GetObject("ButtonFore.Image")));
            this.ButtonFore.Location = new System.Drawing.Point(73, 0);
            this.ButtonFore.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonFore.Name = "ButtonFore";
            this.ButtonFore.Size = new System.Drawing.Size(36, 27);
            this.ButtonFore.TabIndex = 5;
            this.ButtonFore.UseVisualStyleBackColor = false;
            this.ButtonFore.Click += new System.EventHandler(this.ButtonFore_Click);
            // 
            // ButtonPlay
            // 
            this.ButtonPlay.BackColor = System.Drawing.Color.OliveDrab;
            this.ButtonPlay.Dock = System.Windows.Forms.DockStyle.Left;
            this.ButtonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonPlay.Image = ((System.Drawing.Image)(resources.GetObject("ButtonPlay.Image")));
            this.ButtonPlay.Location = new System.Drawing.Point(31, 0);
            this.ButtonPlay.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonPlay.Name = "ButtonPlay";
            this.ButtonPlay.Size = new System.Drawing.Size(42, 27);
            this.ButtonPlay.TabIndex = 3;
            this.ButtonPlay.UseVisualStyleBackColor = false;
            // 
            // ButtonPrev
            // 
            this.ButtonPrev.BackColor = System.Drawing.Color.DarkGray;
            this.ButtonPrev.Dock = System.Windows.Forms.DockStyle.Left;
            this.ButtonPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonPrev.Image = ((System.Drawing.Image)(resources.GetObject("ButtonPrev.Image")));
            this.ButtonPrev.Location = new System.Drawing.Point(0, 0);
            this.ButtonPrev.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonPrev.Name = "ButtonPrev";
            this.ButtonPrev.Size = new System.Drawing.Size(31, 27);
            this.ButtonPrev.TabIndex = 0;
            this.ButtonPrev.UseVisualStyleBackColor = false;
            this.ButtonPrev.Click += new System.EventHandler(this.ButtonPrev_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Controls.Add(this.NowFlame);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.MaxFrame);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(466, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(145, 27);
            this.panel2.TabIndex = 13;
            // 
            // NowFlame
            // 
            this.NowFlame.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NowFlame.Location = new System.Drawing.Point(3, 3);
            this.NowFlame.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NowFlame.Name = "NowFlame";
            this.NowFlame.Size = new System.Drawing.Size(52, 23);
            this.NowFlame.TabIndex = 14;
            this.NowFlame.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NowFlame.ValueChanged += new System.EventHandler(this.NowFrame_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(61, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "/";
            // 
            // MaxFrame
            // 
            this.MaxFrame.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MaxFrame.Location = new System.Drawing.Point(83, 3);
            this.MaxFrame.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.MaxFrame.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxFrame.Name = "MaxFrame";
            this.MaxFrame.Size = new System.Drawing.Size(52, 23);
            this.MaxFrame.TabIndex = 10;
            this.MaxFrame.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MaxFrame.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.MaxFrame.ValueChanged += new System.EventHandler(this.MaxFrame_ValueChanged);
            // 
            // Panel_LineControl_Base
            // 
            this.Panel_LineControl_Base.BackColor = System.Drawing.Color.Gray;
            this.Panel_LineControl_Base.Controls.Add(this.panel2);
            this.Panel_LineControl_Base.Controls.Add(this.MediaBase);
            this.Panel_LineControl_Base.Controls.Add(this.panel3);
            this.Panel_LineControl_Base.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_LineControl_Base.Location = new System.Drawing.Point(0, 24);
            this.Panel_LineControl_Base.Name = "Panel_LineControl_Base";
            this.Panel_LineControl_Base.Size = new System.Drawing.Size(611, 27);
            this.Panel_LineControl_Base.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(152, 27);
            this.panel3.TabIndex = 14;
            // 
            // FormControl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 206);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.Panel_LineControl_Base);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(16, 118);
            this.Name = "FormControl";
            this.Text = "コントロール";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormControl_FormClosing);
            this.Load += new System.EventHandler(this.FormControl_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormControl_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormControl_DragEnter);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.TreeHeader.ResumeLayout(false);
            this.SubMenuTimeLine.ResumeLayout(false);
            this.MediaBase.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NowFlame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxFrame)).EndInit();
            this.Panel_LineControl_Base.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem 再生ToolStripMenuItem;
        private System.Windows.Forms.Panel panel_Time;
        private System.Windows.Forms.Panel panel_Control;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel MediaBase;
        private System.Windows.Forms.Button ButtonFore;
        private System.Windows.Forms.Button ButtonPlay;
        private System.Windows.Forms.Button ButtonPrev;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown NowFlame;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown MaxFrame;
        private System.Windows.Forms.Panel Panel_LineControl_Base;
        private System.Windows.Forms.Panel TreeHeader;
        private System.Windows.Forms.Panel LineHeader;
        private System.Windows.Forms.Button ItemDown;
        private System.Windows.Forms.Button ItemUp;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button ItemRemove;
        private System.Windows.Forms.ContextMenuStrip SubMenuTimeLine;
        private System.Windows.Forms.ToolStripMenuItem キーフレーム追加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 切り取りToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem コピーToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上書き貼付けToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 挿入貼付けToolStripMenuItem;
    }
}