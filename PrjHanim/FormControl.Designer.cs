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
            this.button_ElemRemove = new System.Windows.Forms.Button();
            this.button_ElemDown = new System.Windows.Forms.Button();
            this.button_ElemUp = new System.Windows.Forms.Button();
            this.button_ElemChild = new System.Windows.Forms.Button();
            this.button_ElemParent = new System.Windows.Forms.Button();
            this.SubMenuTimeLine = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_AddKey = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_DelKey = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_DelFrame = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_InsertFrame = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_OverWrite = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Insert = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_Time = new System.Windows.Forms.Panel();
            this.LineHeader = new System.Windows.Forms.Panel();
            this.再生ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MediaBase = new System.Windows.Forms.Panel();
            this.button_Fore = new System.Windows.Forms.Button();
            this.button_Play = new System.Windows.Forms.Button();
            this.button_Prev = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numericUpDown_NowFlame = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_MaxFrame = new System.Windows.Forms.NumericUpDown();
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_NowFlame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaxFrame)).BeginInit();
            this.Panel_LineControl_Base.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 27);
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
            this.splitContainer.Size = new System.Drawing.Size(684, 189);
            this.splitContainer.SplitterDistance = 157;
            this.splitContainer.TabIndex = 0;
            // 
            // panel_Control
            // 
            this.panel_Control.AllowDrop = true;
            this.panel_Control.AutoScroll = true;
            this.panel_Control.BackColor = System.Drawing.Color.Black;
            this.panel_Control.BackgroundImage = global::PrjHikariwoAnim.Properties.Resources.Blank;
            this.panel_Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Control.Location = new System.Drawing.Point(0, 20);
            this.panel_Control.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Control.Name = "panel_Control";
            this.panel_Control.Size = new System.Drawing.Size(155, 167);
            this.panel_Control.TabIndex = 1;
            this.panel_Control.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel_Control_DragDrop);
            this.panel_Control.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Control_Paint);
            this.panel_Control.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_Control_MouseClick);
            this.panel_Control.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel_Control_MouseDoubleClick);
            this.panel_Control.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Control_MouseDown);
            this.panel_Control.MouseEnter += new System.EventHandler(this.panel_Control_MouseEnter);
            this.panel_Control.MouseLeave += new System.EventHandler(this.panel_Control_MouseLeave);
            this.panel_Control.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_Control_MouseMove);
            // 
            // TreeHeader
            // 
            this.TreeHeader.BackColor = System.Drawing.Color.MidnightBlue;
            this.TreeHeader.Controls.Add(this.button_ElemRemove);
            this.TreeHeader.Controls.Add(this.button_ElemDown);
            this.TreeHeader.Controls.Add(this.button_ElemUp);
            this.TreeHeader.Controls.Add(this.button_ElemChild);
            this.TreeHeader.Controls.Add(this.button_ElemParent);
            this.TreeHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.TreeHeader.Location = new System.Drawing.Point(0, 0);
            this.TreeHeader.Name = "TreeHeader";
            this.TreeHeader.Size = new System.Drawing.Size(155, 20);
            this.TreeHeader.TabIndex = 2;
            // 
            // button_ElemRemove
            // 
            this.button_ElemRemove.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_ElemRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_ElemRemove.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_ElemRemove.Location = new System.Drawing.Point(108, 0);
            this.button_ElemRemove.Name = "button_ElemRemove";
            this.button_ElemRemove.Size = new System.Drawing.Size(27, 20);
            this.button_ElemRemove.TabIndex = 12;
            this.button_ElemRemove.Text = "🚮";
            this.button_ElemRemove.UseVisualStyleBackColor = true;
            this.button_ElemRemove.Click += new System.EventHandler(this.button_ElemRemove_Click);
            // 
            // button_ElemDown
            // 
            this.button_ElemDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_ElemDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_ElemDown.Location = new System.Drawing.Point(81, 0);
            this.button_ElemDown.Name = "button_ElemDown";
            this.button_ElemDown.Size = new System.Drawing.Size(27, 20);
            this.button_ElemDown.TabIndex = 11;
            this.button_ElemDown.Text = "▽";
            this.button_ElemDown.UseVisualStyleBackColor = true;
            this.button_ElemDown.Click += new System.EventHandler(this.button_ElemDown_Click);
            // 
            // button_ElemUp
            // 
            this.button_ElemUp.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_ElemUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_ElemUp.Location = new System.Drawing.Point(54, 0);
            this.button_ElemUp.Name = "button_ElemUp";
            this.button_ElemUp.Size = new System.Drawing.Size(27, 20);
            this.button_ElemUp.TabIndex = 10;
            this.button_ElemUp.Text = "△";
            this.button_ElemUp.UseVisualStyleBackColor = true;
            this.button_ElemUp.Click += new System.EventHandler(this.button_ElemUp_Click);
            // 
            // button_ElemChild
            // 
            this.button_ElemChild.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_ElemChild.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_ElemChild.Location = new System.Drawing.Point(27, 0);
            this.button_ElemChild.Name = "button_ElemChild";
            this.button_ElemChild.Size = new System.Drawing.Size(27, 20);
            this.button_ElemChild.TabIndex = 9;
            this.button_ElemChild.Text = "子";
            this.button_ElemChild.UseVisualStyleBackColor = true;
            this.button_ElemChild.Click += new System.EventHandler(this.button_ElemChild_Click);
            // 
            // button_ElemParent
            // 
            this.button_ElemParent.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_ElemParent.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_ElemParent.Location = new System.Drawing.Point(0, 0);
            this.button_ElemParent.Name = "button_ElemParent";
            this.button_ElemParent.Size = new System.Drawing.Size(27, 20);
            this.button_ElemParent.TabIndex = 1;
            this.button_ElemParent.Text = "親";
            this.button_ElemParent.UseVisualStyleBackColor = true;
            this.button_ElemParent.Click += new System.EventHandler(this.button_ElemParent_Click);
            // 
            // SubMenuTimeLine
            // 
            this.SubMenuTimeLine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_AddKey,
            this.ToolStripMenuItem_DelKey,
            this.toolStripMenuItem_DelFrame,
            this.toolStripMenuItem_InsertFrame,
            this.toolStripSeparator1,
            this.ToolStripMenuItem_Cut,
            this.ToolStripMenuItem_Copy,
            this.ToolStripMenuItem_OverWrite,
            this.ToolStripMenuItem_Insert});
            this.SubMenuTimeLine.Name = "SubMenuTimeLine";
            this.SubMenuTimeLine.Size = new System.Drawing.Size(149, 186);
            // 
            // ToolStripMenuItem_AddKey
            // 
            this.ToolStripMenuItem_AddKey.Name = "ToolStripMenuItem_AddKey";
            this.ToolStripMenuItem_AddKey.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_AddKey.Text = "キーフレーム登録";
            this.ToolStripMenuItem_AddKey.Click += new System.EventHandler(this.ToolStripMenuItem_AddKey_Click);
            // 
            // ToolStripMenuItem_DelKey
            // 
            this.ToolStripMenuItem_DelKey.Name = "ToolStripMenuItem_DelKey";
            this.ToolStripMenuItem_DelKey.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_DelKey.Text = "キーフレーム削除";
            this.ToolStripMenuItem_DelKey.Click += new System.EventHandler(this.ToolStripMenuItem_DelKey_Click);
            // 
            // toolStripMenuItem_DelFrame
            // 
            this.toolStripMenuItem_DelFrame.Name = "toolStripMenuItem_DelFrame";
            this.toolStripMenuItem_DelFrame.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem_DelFrame.Text = "フレーム削除";
            this.toolStripMenuItem_DelFrame.Click += new System.EventHandler(this.toolStripMenuItem_DelFrame_Click);
            // 
            // toolStripMenuItem_InsertFrame
            // 
            this.toolStripMenuItem_InsertFrame.Name = "toolStripMenuItem_InsertFrame";
            this.toolStripMenuItem_InsertFrame.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem_InsertFrame.Text = "フレーム挿入";
            this.toolStripMenuItem_InsertFrame.Click += new System.EventHandler(this.toolStripMenuItem_InsertFrame_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // ToolStripMenuItem_Cut
            // 
            this.ToolStripMenuItem_Cut.Name = "ToolStripMenuItem_Cut";
            this.ToolStripMenuItem_Cut.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_Cut.Text = "切り取り(X)";
            this.ToolStripMenuItem_Cut.Click += new System.EventHandler(this.ToolStripMenuItem_Cut_Click);
            // 
            // ToolStripMenuItem_Copy
            // 
            this.ToolStripMenuItem_Copy.Name = "ToolStripMenuItem_Copy";
            this.ToolStripMenuItem_Copy.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_Copy.Text = "コピー(C)";
            this.ToolStripMenuItem_Copy.Click += new System.EventHandler(this.ToolStripMenuItem_Copy_Click);
            // 
            // ToolStripMenuItem_OverWrite
            // 
            this.ToolStripMenuItem_OverWrite.Name = "ToolStripMenuItem_OverWrite";
            this.ToolStripMenuItem_OverWrite.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_OverWrite.Text = "貼付上書(V)";
            this.ToolStripMenuItem_OverWrite.Click += new System.EventHandler(this.ToolStripMenuItem_OverWrite_Click);
            // 
            // ToolStripMenuItem_Insert
            // 
            this.ToolStripMenuItem_Insert.Name = "ToolStripMenuItem_Insert";
            this.ToolStripMenuItem_Insert.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_Insert.Text = "貼付挿入(B)";
            this.ToolStripMenuItem_Insert.Click += new System.EventHandler(this.ToolStripMenuItem_Insert_Click);
            // 
            // panel_Time
            // 
            this.panel_Time.AutoScroll = true;
            this.panel_Time.BackgroundImage = global::PrjHikariwoAnim.Properties.Resources.Blank;
            this.panel_Time.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Time.Location = new System.Drawing.Point(0, 20);
            this.panel_Time.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Time.Name = "panel_Time";
            this.panel_Time.Size = new System.Drawing.Size(521, 167);
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
            this.LineHeader.Size = new System.Drawing.Size(521, 20);
            this.LineHeader.TabIndex = 1;
            this.LineHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.LineHeader_Paint);
            this.LineHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LineHeader_MouseDown);
            this.LineHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LineHeader_MouseUp);
            // 
            // 再生ToolStripMenuItem
            // 
            this.再生ToolStripMenuItem.Name = "再生ToolStripMenuItem";
            this.再生ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.再生ToolStripMenuItem.Text = "再生";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 216);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 25);
            this.panel1.TabIndex = 5;
            // 
            // MediaBase
            // 
            this.MediaBase.Controls.Add(this.button_Fore);
            this.MediaBase.Controls.Add(this.button_Play);
            this.MediaBase.Controls.Add(this.button_Prev);
            this.MediaBase.Dock = System.Windows.Forms.DockStyle.Left;
            this.MediaBase.Location = new System.Drawing.Point(152, 0);
            this.MediaBase.Name = "MediaBase";
            this.MediaBase.Size = new System.Drawing.Size(122, 27);
            this.MediaBase.TabIndex = 12;
            // 
            // button_Fore
            // 
            this.button_Fore.BackColor = System.Drawing.Color.DarkGray;
            this.button_Fore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_Fore.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Fore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Fore.Image = ((System.Drawing.Image)(resources.GetObject("button_Fore.Image")));
            this.button_Fore.Location = new System.Drawing.Point(73, 0);
            this.button_Fore.Margin = new System.Windows.Forms.Padding(0);
            this.button_Fore.Name = "button_Fore";
            this.button_Fore.Size = new System.Drawing.Size(36, 27);
            this.button_Fore.TabIndex = 5;
            this.button_Fore.UseVisualStyleBackColor = false;
            this.button_Fore.Click += new System.EventHandler(this.ButtonFore_Click);
            // 
            // button_Play
            // 
            this.button_Play.BackColor = System.Drawing.Color.OliveDrab;
            this.button_Play.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Play.Image = ((System.Drawing.Image)(resources.GetObject("button_Play.Image")));
            this.button_Play.Location = new System.Drawing.Point(31, 0);
            this.button_Play.Margin = new System.Windows.Forms.Padding(0);
            this.button_Play.Name = "button_Play";
            this.button_Play.Size = new System.Drawing.Size(42, 27);
            this.button_Play.TabIndex = 3;
            this.button_Play.UseVisualStyleBackColor = false;
            // 
            // button_Prev
            // 
            this.button_Prev.BackColor = System.Drawing.Color.DarkGray;
            this.button_Prev.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Prev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Prev.Image = ((System.Drawing.Image)(resources.GetObject("button_Prev.Image")));
            this.button_Prev.Location = new System.Drawing.Point(0, 0);
            this.button_Prev.Margin = new System.Windows.Forms.Padding(0);
            this.button_Prev.Name = "button_Prev";
            this.button_Prev.Size = new System.Drawing.Size(31, 27);
            this.button_Prev.TabIndex = 0;
            this.button_Prev.UseVisualStyleBackColor = false;
            this.button_Prev.Click += new System.EventHandler(this.ButtonPrev_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Controls.Add(this.numericUpDown_NowFlame);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.numericUpDown_MaxFrame);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(539, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(145, 27);
            this.panel2.TabIndex = 13;
            // 
            // numericUpDown_NowFlame
            // 
            this.numericUpDown_NowFlame.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDown_NowFlame.Location = new System.Drawing.Point(3, 3);
            this.numericUpDown_NowFlame.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_NowFlame.Name = "numericUpDown_NowFlame";
            this.numericUpDown_NowFlame.Size = new System.Drawing.Size(52, 23);
            this.numericUpDown_NowFlame.TabIndex = 14;
            this.numericUpDown_NowFlame.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_NowFlame.ValueChanged += new System.EventHandler(this.NowFrame_ValueChanged);
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
            // numericUpDown_MaxFrame
            // 
            this.numericUpDown_MaxFrame.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDown_MaxFrame.Location = new System.Drawing.Point(83, 3);
            this.numericUpDown_MaxFrame.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_MaxFrame.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_MaxFrame.Name = "numericUpDown_MaxFrame";
            this.numericUpDown_MaxFrame.Size = new System.Drawing.Size(52, 23);
            this.numericUpDown_MaxFrame.TabIndex = 10;
            this.numericUpDown_MaxFrame.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_MaxFrame.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDown_MaxFrame.ValueChanged += new System.EventHandler(this.MaxFrame_ValueChanged);
            // 
            // Panel_LineControl_Base
            // 
            this.Panel_LineControl_Base.BackColor = System.Drawing.Color.Gray;
            this.Panel_LineControl_Base.Controls.Add(this.panel2);
            this.Panel_LineControl_Base.Controls.Add(this.MediaBase);
            this.Panel_LineControl_Base.Controls.Add(this.panel3);
            this.Panel_LineControl_Base.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_LineControl_Base.Location = new System.Drawing.Point(0, 0);
            this.Panel_LineControl_Base.Name = "Panel_LineControl_Base";
            this.Panel_LineControl_Base.Size = new System.Drawing.Size(684, 27);
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
            this.ClientSize = new System.Drawing.Size(684, 241);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.Panel_LineControl_Base);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(16, 118);
            this.Name = "FormControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Control";
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_NowFlame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaxFrame)).EndInit();
            this.Panel_LineControl_Base.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripMenuItem 再生ToolStripMenuItem;
        private System.Windows.Forms.Panel panel_Time;
        private System.Windows.Forms.Panel panel_Control;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel MediaBase;
        private System.Windows.Forms.Button button_Fore;
        private System.Windows.Forms.Button button_Play;
        private System.Windows.Forms.Button button_Prev;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown numericUpDown_NowFlame;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_MaxFrame;
        private System.Windows.Forms.Panel Panel_LineControl_Base;
        private System.Windows.Forms.Panel TreeHeader;
        private System.Windows.Forms.Panel LineHeader;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ContextMenuStrip SubMenuTimeLine;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AddKey;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_DelFrame;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_InsertFrame;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Cut;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Copy;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_OverWrite;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Insert;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_DelKey;
        private System.Windows.Forms.Button button_ElemParent;
        private System.Windows.Forms.Button button_ElemRemove;
        private System.Windows.Forms.Button button_ElemDown;
        private System.Windows.Forms.Button button_ElemUp;
        private System.Windows.Forms.Button button_ElemChild;
    }
}