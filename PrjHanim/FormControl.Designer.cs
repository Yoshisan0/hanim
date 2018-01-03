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
            this.contextMenuStrip_Control = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_RemoveElement = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_AddOption = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_AddRotation = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_AddScale = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_AddOffset = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_AddFlip = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_AddTransparency = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_AddColor = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_AddUserDataText = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_RemoveOption = new System.Windows.Forms.ToolStripMenuItem();
            this.TreeHeader = new System.Windows.Forms.Panel();
            this.button_ItemRemove = new System.Windows.Forms.Button();
            this.button_ItemDown = new System.Windows.Forms.Button();
            this.button_ItemUp = new System.Windows.Forms.Button();
            this.button_ElemChild = new System.Windows.Forms.Button();
            this.button_ElemParent = new System.Windows.Forms.Button();
            this.panel_Time = new System.Windows.Forms.Panel();
            this.contextMenuStrip_Time = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_AddKeyFrame = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_RemoveKeyframe = new System.Windows.Forms.ToolStripMenuItem();
            this.LineHeader = new System.Windows.Forms.Panel();
            this.再生ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MediaBase = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
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
            this.contextMenuStrip_Control.SuspendLayout();
            this.TreeHeader.SuspendLayout();
            this.contextMenuStrip_Time.SuspendLayout();
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
            this.splitContainer.Panel2.Controls.Add(this.panel_Time);
            this.splitContainer.Panel2.Controls.Add(this.LineHeader);
            this.splitContainer.Panel2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.splitContainer_Panel2_Scroll);
            this.splitContainer.Panel2MinSize = 48;
            this.splitContainer.Size = new System.Drawing.Size(784, 209);
            this.splitContainer.SplitterDistance = 158;
            this.splitContainer.TabIndex = 0;
            // 
            // panel_Control
            // 
            this.panel_Control.AllowDrop = true;
            this.panel_Control.AutoScroll = true;
            this.panel_Control.BackColor = System.Drawing.Color.Black;
            this.panel_Control.BackgroundImage = global::PrjHikariwoAnim.Properties.Resources.Blank;
            this.panel_Control.ContextMenuStrip = this.contextMenuStrip_Control;
            this.panel_Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Control.Location = new System.Drawing.Point(0, 20);
            this.panel_Control.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Control.Name = "panel_Control";
            this.panel_Control.Size = new System.Drawing.Size(156, 187);
            this.panel_Control.TabIndex = 1;
            this.panel_Control.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel_Control_DragDrop);
            this.panel_Control.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Control_Paint);
            this.panel_Control.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_Control_MouseClick);
            this.panel_Control.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel_Control_MouseDoubleClick);
            this.panel_Control.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Control_MouseDown);
            this.panel_Control.MouseEnter += new System.EventHandler(this.panel_Control_MouseEnter);
            this.panel_Control.MouseLeave += new System.EventHandler(this.panel_Control_MouseLeave);
            this.panel_Control.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_Control_MouseMove);
            this.panel_Control.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_Control_MouseUp);
            // 
            // contextMenuStrip_Control
            // 
            this.contextMenuStrip_Control.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_RemoveElement,
            this.toolStripSeparator2,
            this.ToolStripMenuItem_AddOption,
            this.ToolStripMenuItem_RemoveOption});
            this.contextMenuStrip_Control.Name = "SubMenuTimeLine";
            this.contextMenuStrip_Control.Size = new System.Drawing.Size(162, 76);
            this.contextMenuStrip_Control.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // ToolStripMenuItem_RemoveElement
            // 
            this.ToolStripMenuItem_RemoveElement.Name = "ToolStripMenuItem_RemoveElement";
            this.ToolStripMenuItem_RemoveElement.Size = new System.Drawing.Size(161, 22);
            this.ToolStripMenuItem_RemoveElement.Text = "Remove element";
            this.ToolStripMenuItem_RemoveElement.Click += new System.EventHandler(this.ToolStripMenuItem_RemoveElement_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(158, 6);
            // 
            // ToolStripMenuItem_AddOption
            // 
            this.ToolStripMenuItem_AddOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_AddRotation,
            this.ToolStripMenuItem_AddScale,
            this.ToolStripMenuItem_AddOffset,
            this.ToolStripMenuItem_AddFlip,
            this.ToolStripMenuItem_AddTransparency,
            this.ToolStripMenuItem_AddColor,
            this.ToolStripMenuItem_AddUserDataText});
            this.ToolStripMenuItem_AddOption.Name = "ToolStripMenuItem_AddOption";
            this.ToolStripMenuItem_AddOption.Size = new System.Drawing.Size(161, 22);
            this.ToolStripMenuItem_AddOption.Text = "Add option";
            this.ToolStripMenuItem_AddOption.DropDownOpening += new System.EventHandler(this.ToolStripMenuItem_AddOption_DropDownOpening);
            // 
            // ToolStripMenuItem_AddRotation
            // 
            this.ToolStripMenuItem_AddRotation.Name = "ToolStripMenuItem_AddRotation";
            this.ToolStripMenuItem_AddRotation.Size = new System.Drawing.Size(154, 22);
            this.ToolStripMenuItem_AddRotation.Text = "Rotation";
            this.ToolStripMenuItem_AddRotation.Click += new System.EventHandler(this.ToolStripMenuItem_Add_Click);
            // 
            // ToolStripMenuItem_AddScale
            // 
            this.ToolStripMenuItem_AddScale.Name = "ToolStripMenuItem_AddScale";
            this.ToolStripMenuItem_AddScale.Size = new System.Drawing.Size(154, 22);
            this.ToolStripMenuItem_AddScale.Text = "Scale";
            this.ToolStripMenuItem_AddScale.Click += new System.EventHandler(this.ToolStripMenuItem_Add_Click);
            // 
            // ToolStripMenuItem_AddOffset
            // 
            this.ToolStripMenuItem_AddOffset.Name = "ToolStripMenuItem_AddOffset";
            this.ToolStripMenuItem_AddOffset.Size = new System.Drawing.Size(154, 22);
            this.ToolStripMenuItem_AddOffset.Text = "Offset";
            this.ToolStripMenuItem_AddOffset.Click += new System.EventHandler(this.ToolStripMenuItem_Add_Click);
            // 
            // ToolStripMenuItem_AddFlip
            // 
            this.ToolStripMenuItem_AddFlip.Name = "ToolStripMenuItem_AddFlip";
            this.ToolStripMenuItem_AddFlip.Size = new System.Drawing.Size(154, 22);
            this.ToolStripMenuItem_AddFlip.Text = "Flip";
            this.ToolStripMenuItem_AddFlip.Click += new System.EventHandler(this.ToolStripMenuItem_Add_Click);
            // 
            // ToolStripMenuItem_AddTransparency
            // 
            this.ToolStripMenuItem_AddTransparency.Name = "ToolStripMenuItem_AddTransparency";
            this.ToolStripMenuItem_AddTransparency.Size = new System.Drawing.Size(154, 22);
            this.ToolStripMenuItem_AddTransparency.Text = "Transparency";
            this.ToolStripMenuItem_AddTransparency.Click += new System.EventHandler(this.ToolStripMenuItem_Add_Click);
            // 
            // ToolStripMenuItem_AddColor
            // 
            this.ToolStripMenuItem_AddColor.Name = "ToolStripMenuItem_AddColor";
            this.ToolStripMenuItem_AddColor.Size = new System.Drawing.Size(154, 22);
            this.ToolStripMenuItem_AddColor.Text = "Color";
            this.ToolStripMenuItem_AddColor.Click += new System.EventHandler(this.ToolStripMenuItem_Add_Click);
            // 
            // ToolStripMenuItem_AddUserDataText
            // 
            this.ToolStripMenuItem_AddUserDataText.Name = "ToolStripMenuItem_AddUserDataText";
            this.ToolStripMenuItem_AddUserDataText.Size = new System.Drawing.Size(154, 22);
            this.ToolStripMenuItem_AddUserDataText.Text = "User data (text)";
            this.ToolStripMenuItem_AddUserDataText.Click += new System.EventHandler(this.ToolStripMenuItem_Add_Click);
            // 
            // ToolStripMenuItem_RemoveOption
            // 
            this.ToolStripMenuItem_RemoveOption.Name = "ToolStripMenuItem_RemoveOption";
            this.ToolStripMenuItem_RemoveOption.Size = new System.Drawing.Size(161, 22);
            this.ToolStripMenuItem_RemoveOption.Text = "Remove option";
            this.ToolStripMenuItem_RemoveOption.Click += new System.EventHandler(this.ToolStripMenuItem_RemoveOption_Click);
            // 
            // TreeHeader
            // 
            this.TreeHeader.BackColor = System.Drawing.Color.MidnightBlue;
            this.TreeHeader.Controls.Add(this.button_ItemRemove);
            this.TreeHeader.Controls.Add(this.button_ItemDown);
            this.TreeHeader.Controls.Add(this.button_ItemUp);
            this.TreeHeader.Controls.Add(this.button_ElemChild);
            this.TreeHeader.Controls.Add(this.button_ElemParent);
            this.TreeHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.TreeHeader.Location = new System.Drawing.Point(0, 0);
            this.TreeHeader.Name = "TreeHeader";
            this.TreeHeader.Size = new System.Drawing.Size(156, 20);
            this.TreeHeader.TabIndex = 2;
            // 
            // button_ItemRemove
            // 
            this.button_ItemRemove.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_ItemRemove.Enabled = false;
            this.button_ItemRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_ItemRemove.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_ItemRemove.Location = new System.Drawing.Point(108, 0);
            this.button_ItemRemove.Name = "button_ItemRemove";
            this.button_ItemRemove.Size = new System.Drawing.Size(27, 20);
            this.button_ItemRemove.TabIndex = 12;
            this.button_ItemRemove.Text = "🚮";
            this.button_ItemRemove.UseVisualStyleBackColor = true;
            this.button_ItemRemove.Click += new System.EventHandler(this.button_ItemRemove_Click);
            // 
            // button_ItemDown
            // 
            this.button_ItemDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_ItemDown.Enabled = false;
            this.button_ItemDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_ItemDown.Location = new System.Drawing.Point(81, 0);
            this.button_ItemDown.Name = "button_ItemDown";
            this.button_ItemDown.Size = new System.Drawing.Size(27, 20);
            this.button_ItemDown.TabIndex = 11;
            this.button_ItemDown.Text = "▽";
            this.button_ItemDown.UseVisualStyleBackColor = true;
            this.button_ItemDown.Click += new System.EventHandler(this.button_ItemDown_Click);
            // 
            // button_ItemUp
            // 
            this.button_ItemUp.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_ItemUp.Enabled = false;
            this.button_ItemUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_ItemUp.Location = new System.Drawing.Point(54, 0);
            this.button_ItemUp.Name = "button_ItemUp";
            this.button_ItemUp.Size = new System.Drawing.Size(27, 20);
            this.button_ItemUp.TabIndex = 10;
            this.button_ItemUp.Text = "△";
            this.button_ItemUp.UseVisualStyleBackColor = true;
            this.button_ItemUp.Click += new System.EventHandler(this.button_ItemUp_Click);
            // 
            // button_ElemChild
            // 
            this.button_ElemChild.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_ElemChild.Enabled = false;
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
            this.button_ElemParent.Enabled = false;
            this.button_ElemParent.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_ElemParent.Location = new System.Drawing.Point(0, 0);
            this.button_ElemParent.Name = "button_ElemParent";
            this.button_ElemParent.Size = new System.Drawing.Size(27, 20);
            this.button_ElemParent.TabIndex = 1;
            this.button_ElemParent.Text = "親";
            this.button_ElemParent.UseVisualStyleBackColor = true;
            this.button_ElemParent.Click += new System.EventHandler(this.button_ElemParent_Click);
            // 
            // panel_Time
            // 
            this.panel_Time.AutoScroll = true;
            this.panel_Time.BackgroundImage = global::PrjHikariwoAnim.Properties.Resources.Blank;
            this.panel_Time.ContextMenuStrip = this.contextMenuStrip_Time;
            this.panel_Time.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Time.Location = new System.Drawing.Point(0, 20);
            this.panel_Time.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Time.Name = "panel_Time";
            this.panel_Time.Size = new System.Drawing.Size(620, 187);
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
            // contextMenuStrip_Time
            // 
            this.contextMenuStrip_Time.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_AddKeyFrame,
            this.ToolStripMenuItem_RemoveKeyframe});
            this.contextMenuStrip_Time.Name = "SubMenuTimeLine";
            this.contextMenuStrip_Time.Size = new System.Drawing.Size(168, 48);
            this.contextMenuStrip_Time.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // ToolStripMenuItem_AddKeyFrame
            // 
            this.ToolStripMenuItem_AddKeyFrame.Name = "ToolStripMenuItem_AddKeyFrame";
            this.ToolStripMenuItem_AddKeyFrame.Size = new System.Drawing.Size(167, 22);
            this.ToolStripMenuItem_AddKeyFrame.Text = "Add keyframe";
            this.ToolStripMenuItem_AddKeyFrame.Click += new System.EventHandler(this.ToolStripMenuItem_AddKeyFrame_Click);
            // 
            // ToolStripMenuItem_RemoveKeyframe
            // 
            this.ToolStripMenuItem_RemoveKeyframe.Name = "ToolStripMenuItem_RemoveKeyframe";
            this.ToolStripMenuItem_RemoveKeyframe.Size = new System.Drawing.Size(167, 22);
            this.ToolStripMenuItem_RemoveKeyframe.Text = "Remove keyframe";
            this.ToolStripMenuItem_RemoveKeyframe.Click += new System.EventHandler(this.ToolStripMenuItem_RemoveKeyframe_Click);
            // 
            // LineHeader
            // 
            this.LineHeader.BackColor = System.Drawing.Color.MidnightBlue;
            this.LineHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.LineHeader.Location = new System.Drawing.Point(0, 0);
            this.LineHeader.Name = "LineHeader";
            this.LineHeader.Size = new System.Drawing.Size(620, 20);
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
            this.panel1.Location = new System.Drawing.Point(0, 236);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 25);
            this.panel1.TabIndex = 5;
            // 
            // MediaBase
            // 
            this.MediaBase.BackColor = System.Drawing.Color.Gray;
            this.MediaBase.Controls.Add(this.checkBox1);
            this.MediaBase.Controls.Add(this.button_Fore);
            this.MediaBase.Controls.Add(this.button_Play);
            this.MediaBase.Controls.Add(this.button_Prev);
            this.MediaBase.Dock = System.Windows.Forms.DockStyle.Left;
            this.MediaBase.Location = new System.Drawing.Point(156, 0);
            this.MediaBase.Name = "MediaBase";
            this.MediaBase.Size = new System.Drawing.Size(122, 27);
            this.MediaBase.TabIndex = 12;
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.BackColor = System.Drawing.Color.DarkGray;
            this.checkBox1.BackgroundImage = global::PrjHikariwoAnim.Properties.Resources.repeat;
            this.checkBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox1.Location = new System.Drawing.Point(81, 0);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(27, 27);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // button_Fore
            // 
            this.button_Fore.BackColor = System.Drawing.Color.DarkGray;
            this.button_Fore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_Fore.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Fore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Fore.Image = ((System.Drawing.Image)(resources.GetObject("button_Fore.Image")));
            this.button_Fore.Location = new System.Drawing.Point(54, 0);
            this.button_Fore.Margin = new System.Windows.Forms.Padding(0);
            this.button_Fore.Name = "button_Fore";
            this.button_Fore.Size = new System.Drawing.Size(27, 27);
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
            this.button_Play.Location = new System.Drawing.Point(27, 0);
            this.button_Play.Margin = new System.Windows.Forms.Padding(0);
            this.button_Play.Name = "button_Play";
            this.button_Play.Size = new System.Drawing.Size(27, 27);
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
            this.button_Prev.Size = new System.Drawing.Size(27, 27);
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
            this.panel2.Location = new System.Drawing.Point(639, 0);
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
            this.Panel_LineControl_Base.Size = new System.Drawing.Size(784, 27);
            this.Panel_LineControl_Base.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(156, 27);
            this.panel3.TabIndex = 14;
            // 
            // FormControl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 261);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.Panel_LineControl_Base);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(440, 130);
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
            this.contextMenuStrip_Control.ResumeLayout(false);
            this.TreeHeader.ResumeLayout(false);
            this.contextMenuStrip_Time.ResumeLayout(false);
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
        private System.Windows.Forms.Button button_ElemParent;
        private System.Windows.Forms.Button button_ItemRemove;
        private System.Windows.Forms.Button button_ItemDown;
        private System.Windows.Forms.Button button_ItemUp;
        private System.Windows.Forms.Button button_ElemChild;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Control;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AddOption;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_RemoveOption;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AddRotation;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AddScale;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AddTransparency;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AddFlip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AddColor;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AddOffset;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AddUserDataText;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_RemoveElement;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Time;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AddKeyFrame;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_RemoveKeyframe;
    }
}