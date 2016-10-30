namespace PrjHikariwoAnim
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Images");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Cells");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Project", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Motion", 2, 2);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.PanelPreView = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新規プロジェクトToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.プロジェクトの読込ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.プロジェクト保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nowFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cellListToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nowAttributeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ウインドウToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSmenu_ImageList = new System.Windows.Forms.ToolStripMenuItem();
            this.TSmenu_Control = new System.Windows.Forms.ToolStripMenuItem();
            this.TSmenu_Attribute = new System.Windows.Forms.ToolStripMenuItem();
            this.cellListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PanelToolBase = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AlingForm = new System.Windows.Forms.Button();
            this.CB_CellList = new System.Windows.Forms.CheckBox();
            this.CB_Attribute = new System.Windows.Forms.CheckBox();
            this.CB_Control = new System.Windows.Forms.CheckBox();
            this.CB_ImageList = new System.Windows.Forms.CheckBox();
            this.Spaceer1 = new System.Windows.Forms.Panel();
            this.SnapCheck = new System.Windows.Forms.CheckBox();
            this.ZoomLevel = new System.Windows.Forms.HScrollBar();
            this.Num_Grid = new System.Windows.Forms.NumericUpDown();
            this.GridColor = new System.Windows.Forms.Button();
            this.GridCheck = new System.Windows.Forms.CheckBox();
            this.CrossColor = new System.Windows.Forms.Button();
            this.CrossBarCheck = new System.Windows.Forms.CheckBox();
            this.panel_ProjectTree_base = new System.Windows.Forms.Panel();
            this.panel_Project_Bottom = new System.Windows.Forms.Panel();
            this.BottonTest = new System.Windows.Forms.Button();
            this.treeView_Project = new System.Windows.Forms.TreeView();
            this.imageList_Thumb = new System.Windows.Forms.ImageList(this.components);
            this.Panel_ProjectTopBase = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SubMenu_Prpject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.PanelToolBase.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Grid)).BeginInit();
            this.panel_ProjectTree_base.SuspendLayout();
            this.panel_Project_Bottom.SuspendLayout();
            this.Panel_ProjectTopBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.StatusLabel2});
            this.statusStrip.Location = new System.Drawing.Point(0, 389);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(656, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(27, 17);
            this.StatusLabel.Text = "Test";
            // 
            // StatusLabel2
            // 
            this.StatusLabel2.Name = "StatusLabel2";
            this.StatusLabel2.Size = new System.Drawing.Size(32, 17);
            this.StatusLabel2.Text = "Pos2";
            // 
            // timerMain
            // 
            this.timerMain.Enabled = true;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // PanelPreView
            // 
            this.PanelPreView.AllowDrop = true;
            this.PanelPreView.BackColor = System.Drawing.Color.Black;
            this.PanelPreView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelPreView.Location = new System.Drawing.Point(0, 30);
            this.PanelPreView.Margin = new System.Windows.Forms.Padding(0);
            this.PanelPreView.Name = "PanelPreView";
            this.PanelPreView.Size = new System.Drawing.Size(523, 335);
            this.PanelPreView.TabIndex = 2;
            this.PanelPreView.DragDrop += new System.Windows.Forms.DragEventHandler(this.PanelPreView_DragDrop);
            this.PanelPreView.DragEnter += new System.Windows.Forms.DragEventHandler(this.PanelPreView_DragEnter);
            this.PanelPreView.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelPreView_Paint);
            this.PanelPreView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelPreView_MouseDown);
            this.PanelPreView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelPreView_MouseMove);
            this.PanelPreView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelPreView_MouseUp);
            this.PanelPreView.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PanelPreView_MouseWheel);
            this.PanelPreView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PanelPreView_PreviewKeyDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルToolStripMenuItem,
            this.ウインドウToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(656, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルToolStripMenuItem
            // 
            this.ファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規プロジェクトToolStripMenuItem,
            this.プロジェクトの読込ToolStripMenuItem,
            this.プロジェクト保存ToolStripMenuItem,
            this.exportsToolStripMenuItem});
            this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.ファイルToolStripMenuItem.Text = "ファイル";
            // 
            // 新規プロジェクトToolStripMenuItem
            // 
            this.新規プロジェクトToolStripMenuItem.Name = "新規プロジェクトToolStripMenuItem";
            this.新規プロジェクトToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.新規プロジェクトToolStripMenuItem.Text = "新規プロジェクト";
            // 
            // プロジェクトの読込ToolStripMenuItem
            // 
            this.プロジェクトの読込ToolStripMenuItem.Name = "プロジェクトの読込ToolStripMenuItem";
            this.プロジェクトの読込ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.プロジェクトの読込ToolStripMenuItem.Text = "プロジェクト読込";
            this.プロジェクトの読込ToolStripMenuItem.Click += new System.EventHandler(this.LoadProject_Click);
            // 
            // プロジェクト保存ToolStripMenuItem
            // 
            this.プロジェクト保存ToolStripMenuItem.Name = "プロジェクト保存ToolStripMenuItem";
            this.プロジェクト保存ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.プロジェクト保存ToolStripMenuItem.Text = "プロジェクト保存";
            this.プロジェクト保存ToolStripMenuItem.Click += new System.EventHandler(this.SaveProject_Click);
            // 
            // exportsToolStripMenuItem
            // 
            this.exportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nowFrameToolStripMenuItem,
            this.cellListToolStripMenuItem1,
            this.nowAttributeToolStripMenuItem});
            this.exportsToolStripMenuItem.Name = "exportsToolStripMenuItem";
            this.exportsToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.exportsToolStripMenuItem.Text = "Exports";
            // 
            // nowFrameToolStripMenuItem
            // 
            this.nowFrameToolStripMenuItem.Name = "nowFrameToolStripMenuItem";
            this.nowFrameToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.nowFrameToolStripMenuItem.Text = "NowFrame";
            // 
            // cellListToolStripMenuItem1
            // 
            this.cellListToolStripMenuItem1.Name = "cellListToolStripMenuItem1";
            this.cellListToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.cellListToolStripMenuItem1.Text = "CellList";
            // 
            // nowAttributeToolStripMenuItem
            // 
            this.nowAttributeToolStripMenuItem.Name = "nowAttributeToolStripMenuItem";
            this.nowAttributeToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.nowAttributeToolStripMenuItem.Text = "NowAttribute";
            // 
            // ウインドウToolStripMenuItem
            // 
            this.ウインドウToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSmenu_ImageList,
            this.TSmenu_Control,
            this.TSmenu_Attribute,
            this.cellListToolStripMenuItem});
            this.ウインドウToolStripMenuItem.Name = "ウインドウToolStripMenuItem";
            this.ウインドウToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.ウインドウToolStripMenuItem.Text = "ウインドウ";
            // 
            // TSmenu_ImageList
            // 
            this.TSmenu_ImageList.CheckOnClick = true;
            this.TSmenu_ImageList.Name = "TSmenu_ImageList";
            this.TSmenu_ImageList.Size = new System.Drawing.Size(124, 22);
            this.TSmenu_ImageList.Text = "ImageList";
            this.TSmenu_ImageList.Click += new System.EventHandler(this.TSMenu_ImageList_Click);
            // 
            // TSmenu_Control
            // 
            this.TSmenu_Control.CheckOnClick = true;
            this.TSmenu_Control.Name = "TSmenu_Control";
            this.TSmenu_Control.Size = new System.Drawing.Size(124, 22);
            this.TSmenu_Control.Text = "Control";
            this.TSmenu_Control.Click += new System.EventHandler(this.TSMenu_Control_Click);
            // 
            // TSmenu_Attribute
            // 
            this.TSmenu_Attribute.CheckOnClick = true;
            this.TSmenu_Attribute.Name = "TSmenu_Attribute";
            this.TSmenu_Attribute.Size = new System.Drawing.Size(124, 22);
            this.TSmenu_Attribute.Text = "Attribute";
            this.TSmenu_Attribute.Click += new System.EventHandler(this.TSMenu_Attribute_Click);
            // 
            // cellListToolStripMenuItem
            // 
            this.cellListToolStripMenuItem.Name = "cellListToolStripMenuItem";
            this.cellListToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.cellListToolStripMenuItem.Text = "CellList";
            this.cellListToolStripMenuItem.Click += new System.EventHandler(this.TSMenu_CellList_Click);
            // 
            // PanelToolBase
            // 
            this.PanelToolBase.BackColor = System.Drawing.SystemColors.Control;
            this.PanelToolBase.Controls.Add(this.panel1);
            this.PanelToolBase.Controls.Add(this.SnapCheck);
            this.PanelToolBase.Controls.Add(this.ZoomLevel);
            this.PanelToolBase.Controls.Add(this.Num_Grid);
            this.PanelToolBase.Controls.Add(this.GridColor);
            this.PanelToolBase.Controls.Add(this.GridCheck);
            this.PanelToolBase.Controls.Add(this.CrossColor);
            this.PanelToolBase.Controls.Add(this.CrossBarCheck);
            this.PanelToolBase.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelToolBase.Location = new System.Drawing.Point(0, 0);
            this.PanelToolBase.Margin = new System.Windows.Forms.Padding(0);
            this.PanelToolBase.Name = "PanelToolBase";
            this.PanelToolBase.Size = new System.Drawing.Size(523, 30);
            this.PanelToolBase.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.AlingForm);
            this.panel1.Controls.Add(this.CB_CellList);
            this.panel1.Controls.Add(this.CB_Attribute);
            this.panel1.Controls.Add(this.CB_Control);
            this.panel1.Controls.Add(this.CB_ImageList);
            this.panel1.Controls.Add(this.Spaceer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(191, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(171, 30);
            this.panel1.TabIndex = 8;
            // 
            // AlingForm
            // 
            this.AlingForm.Dock = System.Windows.Forms.DockStyle.Left;
            this.AlingForm.Image = global::PrjHikariwoAnim.Properties.Resources.alingment;
            this.AlingForm.Location = new System.Drawing.Point(130, 0);
            this.AlingForm.Margin = new System.Windows.Forms.Padding(0);
            this.AlingForm.Name = "AlingForm";
            this.AlingForm.Size = new System.Drawing.Size(30, 30);
            this.AlingForm.TabIndex = 5;
            this.AlingForm.UseVisualStyleBackColor = true;
            this.AlingForm.Click += new System.EventHandler(this.Botton_AlingForm_Click);
            // 
            // CB_CellList
            // 
            this.CB_CellList.Appearance = System.Windows.Forms.Appearance.Button;
            this.CB_CellList.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CB_CellList.Checked = true;
            this.CB_CellList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_CellList.Dock = System.Windows.Forms.DockStyle.Left;
            this.CB_CellList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CB_CellList.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CB_CellList.Image = global::PrjHikariwoAnim.Properties.Resources.partslist;
            this.CB_CellList.Location = new System.Drawing.Point(100, 0);
            this.CB_CellList.Margin = new System.Windows.Forms.Padding(0);
            this.CB_CellList.Name = "CB_CellList";
            this.CB_CellList.Size = new System.Drawing.Size(30, 30);
            this.CB_CellList.TabIndex = 4;
            this.CB_CellList.UseVisualStyleBackColor = false;
            this.CB_CellList.CheckedChanged += new System.EventHandler(this.CB_CellList_CheckedChanged);
            // 
            // CB_Attribute
            // 
            this.CB_Attribute.Appearance = System.Windows.Forms.Appearance.Button;
            this.CB_Attribute.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CB_Attribute.Checked = true;
            this.CB_Attribute.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_Attribute.Dock = System.Windows.Forms.DockStyle.Left;
            this.CB_Attribute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CB_Attribute.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CB_Attribute.Image = global::PrjHikariwoAnim.Properties.Resources.property;
            this.CB_Attribute.Location = new System.Drawing.Point(70, 0);
            this.CB_Attribute.Margin = new System.Windows.Forms.Padding(0);
            this.CB_Attribute.Name = "CB_Attribute";
            this.CB_Attribute.Size = new System.Drawing.Size(30, 30);
            this.CB_Attribute.TabIndex = 3;
            this.CB_Attribute.UseVisualStyleBackColor = false;
            this.CB_Attribute.CheckedChanged += new System.EventHandler(this.CB_Attribute_CheckedChanged);
            // 
            // CB_Control
            // 
            this.CB_Control.Appearance = System.Windows.Forms.Appearance.Button;
            this.CB_Control.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CB_Control.Checked = true;
            this.CB_Control.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_Control.Dock = System.Windows.Forms.DockStyle.Left;
            this.CB_Control.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CB_Control.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CB_Control.Image = global::PrjHikariwoAnim.Properties.Resources.control;
            this.CB_Control.Location = new System.Drawing.Point(40, 0);
            this.CB_Control.Margin = new System.Windows.Forms.Padding(0);
            this.CB_Control.Name = "CB_Control";
            this.CB_Control.Size = new System.Drawing.Size(30, 30);
            this.CB_Control.TabIndex = 2;
            this.CB_Control.UseVisualStyleBackColor = false;
            this.CB_Control.CheckedChanged += new System.EventHandler(this.CB_Control_CheckedChanged);
            // 
            // CB_ImageList
            // 
            this.CB_ImageList.Appearance = System.Windows.Forms.Appearance.Button;
            this.CB_ImageList.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CB_ImageList.Checked = true;
            this.CB_ImageList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_ImageList.Dock = System.Windows.Forms.DockStyle.Left;
            this.CB_ImageList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CB_ImageList.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CB_ImageList.Location = new System.Drawing.Point(10, 0);
            this.CB_ImageList.Margin = new System.Windows.Forms.Padding(0);
            this.CB_ImageList.Name = "CB_ImageList";
            this.CB_ImageList.Size = new System.Drawing.Size(30, 30);
            this.CB_ImageList.TabIndex = 0;
            this.CB_ImageList.Text = "絵";
            this.CB_ImageList.UseVisualStyleBackColor = false;
            this.CB_ImageList.CheckedChanged += new System.EventHandler(this.CB_ImageList_CheckedChanged);
            // 
            // Spaceer1
            // 
            this.Spaceer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.Spaceer1.Location = new System.Drawing.Point(0, 0);
            this.Spaceer1.Name = "Spaceer1";
            this.Spaceer1.Size = new System.Drawing.Size(10, 30);
            this.Spaceer1.TabIndex = 1;
            // 
            // SnapCheck
            // 
            this.SnapCheck.Appearance = System.Windows.Forms.Appearance.Button;
            this.SnapCheck.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SnapCheck.BackgroundImage = global::PrjHikariwoAnim.Properties.Resources.magnet2;
            this.SnapCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SnapCheck.Dock = System.Windows.Forms.DockStyle.Left;
            this.SnapCheck.Location = new System.Drawing.Point(161, 0);
            this.SnapCheck.Margin = new System.Windows.Forms.Padding(0);
            this.SnapCheck.Name = "SnapCheck";
            this.SnapCheck.Size = new System.Drawing.Size(30, 30);
            this.SnapCheck.TabIndex = 7;
            this.SnapCheck.UseVisualStyleBackColor = false;
            // 
            // ZoomLevel
            // 
            this.ZoomLevel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ZoomLevel.LargeChange = 1;
            this.ZoomLevel.Location = new System.Drawing.Point(443, 0);
            this.ZoomLevel.Maximum = 80;
            this.ZoomLevel.Minimum = 2;
            this.ZoomLevel.Name = "ZoomLevel";
            this.ZoomLevel.Size = new System.Drawing.Size(80, 30);
            this.ZoomLevel.TabIndex = 6;
            this.ZoomLevel.Value = 10;
            this.ZoomLevel.ValueChanged += new System.EventHandler(this.ZoomLevel_ValueChanged);
            // 
            // Num_Grid
            // 
            this.Num_Grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Num_Grid.Dock = System.Windows.Forms.DockStyle.Left;
            this.Num_Grid.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Num_Grid.Location = new System.Drawing.Point(120, 0);
            this.Num_Grid.Margin = new System.Windows.Forms.Padding(0);
            this.Num_Grid.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.Num_Grid.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.Num_Grid.Name = "Num_Grid";
            this.Num_Grid.Size = new System.Drawing.Size(41, 27);
            this.Num_Grid.TabIndex = 3;
            this.Num_Grid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Num_Grid.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // GridColor
            // 
            this.GridColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.GridColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.GridColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GridColor.Location = new System.Drawing.Point(90, 0);
            this.GridColor.Name = "GridColor";
            this.GridColor.Size = new System.Drawing.Size(30, 30);
            this.GridColor.TabIndex = 5;
            this.GridColor.UseVisualStyleBackColor = false;
            this.GridColor.Click += new System.EventHandler(this.Button_Color_Click);
            // 
            // GridCheck
            // 
            this.GridCheck.Appearance = System.Windows.Forms.Appearance.Button;
            this.GridCheck.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.GridCheck.BackgroundImage = global::PrjHikariwoAnim.Properties.Resources.grid;
            this.GridCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.GridCheck.Checked = true;
            this.GridCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GridCheck.Dock = System.Windows.Forms.DockStyle.Left;
            this.GridCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GridCheck.Location = new System.Drawing.Point(60, 0);
            this.GridCheck.Margin = new System.Windows.Forms.Padding(0);
            this.GridCheck.Name = "GridCheck";
            this.GridCheck.Size = new System.Drawing.Size(30, 30);
            this.GridCheck.TabIndex = 2;
            this.GridCheck.UseVisualStyleBackColor = false;
            this.GridCheck.Click += new System.EventHandler(this.CrossBarCheck_Click);
            // 
            // CrossColor
            // 
            this.CrossColor.BackColor = System.Drawing.Color.DarkRed;
            this.CrossColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.CrossColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CrossColor.Location = new System.Drawing.Point(30, 0);
            this.CrossColor.Name = "CrossColor";
            this.CrossColor.Size = new System.Drawing.Size(30, 30);
            this.CrossColor.TabIndex = 4;
            this.CrossColor.UseVisualStyleBackColor = false;
            this.CrossColor.Click += new System.EventHandler(this.Button_Color_Click);
            // 
            // CrossBarCheck
            // 
            this.CrossBarCheck.Appearance = System.Windows.Forms.Appearance.Button;
            this.CrossBarCheck.AutoSize = true;
            this.CrossBarCheck.BackColor = System.Drawing.Color.Black;
            this.CrossBarCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CrossBarCheck.Checked = true;
            this.CrossBarCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CrossBarCheck.Dock = System.Windows.Forms.DockStyle.Left;
            this.CrossBarCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CrossBarCheck.Image = global::PrjHikariwoAnim.Properties.Resources.cross;
            this.CrossBarCheck.Location = new System.Drawing.Point(0, 0);
            this.CrossBarCheck.Margin = new System.Windows.Forms.Padding(0);
            this.CrossBarCheck.Name = "CrossBarCheck";
            this.CrossBarCheck.Size = new System.Drawing.Size(30, 30);
            this.CrossBarCheck.TabIndex = 1;
            this.CrossBarCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CrossBarCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.CrossBarCheck.UseVisualStyleBackColor = false;
            this.CrossBarCheck.Click += new System.EventHandler(this.CrossBarCheck_Click);
            // 
            // panel_ProjectTree_base
            // 
            this.panel_ProjectTree_base.BackColor = System.Drawing.Color.Black;
            this.panel_ProjectTree_base.Controls.Add(this.panel_Project_Bottom);
            this.panel_ProjectTree_base.Controls.Add(this.treeView_Project);
            this.panel_ProjectTree_base.Controls.Add(this.Panel_ProjectTopBase);
            this.panel_ProjectTree_base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ProjectTree_base.Location = new System.Drawing.Point(0, 0);
            this.panel_ProjectTree_base.Name = "panel_ProjectTree_base";
            this.panel_ProjectTree_base.Size = new System.Drawing.Size(129, 365);
            this.panel_ProjectTree_base.TabIndex = 5;
            // 
            // panel_Project_Bottom
            // 
            this.panel_Project_Bottom.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel_Project_Bottom.Controls.Add(this.BottonTest);
            this.panel_Project_Bottom.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel_Project_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Project_Bottom.Location = new System.Drawing.Point(0, 231);
            this.panel_Project_Bottom.Name = "panel_Project_Bottom";
            this.panel_Project_Bottom.Size = new System.Drawing.Size(129, 134);
            this.panel_Project_Bottom.TabIndex = 2;
            // 
            // BottonTest
            // 
            this.BottonTest.Location = new System.Drawing.Point(3, 3);
            this.BottonTest.Name = "BottonTest";
            this.BottonTest.Size = new System.Drawing.Size(78, 23);
            this.BottonTest.TabIndex = 0;
            this.BottonTest.Text = "Img+Test";
            this.BottonTest.UseVisualStyleBackColor = true;
            this.BottonTest.Click += new System.EventHandler(this.BottonTest_Click);
            // 
            // treeView_Project
            // 
            this.treeView_Project.BackColor = System.Drawing.Color.Black;
            this.treeView_Project.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Project.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.treeView_Project.ForeColor = System.Drawing.Color.White;
            this.treeView_Project.FullRowSelect = true;
            this.treeView_Project.HotTracking = true;
            this.treeView_Project.ImageIndex = 0;
            this.treeView_Project.ImageList = this.imageList_Thumb;
            this.treeView_Project.LabelEdit = true;
            this.treeView_Project.Location = new System.Drawing.Point(0, 30);
            this.treeView_Project.Name = "treeView_Project";
            treeNode1.Name = "Images";
            treeNode1.Text = "Images";
            treeNode2.Name = "Cells";
            treeNode2.Text = "Cells";
            treeNode3.Name = "ProjectName";
            treeNode3.Text = "Project";
            treeNode4.ImageIndex = 2;
            treeNode4.Name = "Motion";
            treeNode4.SelectedImageIndex = 2;
            treeNode4.Tag = "1";
            treeNode4.Text = "Motion";
            this.treeView_Project.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            this.treeView_Project.SelectedImageIndex = 0;
            this.treeView_Project.ShowRootLines = false;
            this.treeView_Project.Size = new System.Drawing.Size(129, 335);
            this.treeView_Project.TabIndex = 1;
            this.treeView_Project.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_Project_AfterLabelEdit);
            this.treeView_Project.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_Project_NodeMouseClick);
            // 
            // imageList_Thumb
            // 
            this.imageList_Thumb.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Thumb.ImageStream")));
            this.imageList_Thumb.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_Thumb.Images.SetKeyName(0, "cells.PNG");
            this.imageList_Thumb.Images.SetKeyName(1, "image.PNG");
            this.imageList_Thumb.Images.SetKeyName(2, "motion.PNG");
            this.imageList_Thumb.Images.SetKeyName(3, "elements.PNG");
            this.imageList_Thumb.Images.SetKeyName(4, "elements2.PNG");
            // 
            // Panel_ProjectTopBase
            // 
            this.Panel_ProjectTopBase.Controls.Add(this.button1);
            this.Panel_ProjectTopBase.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_ProjectTopBase.Location = new System.Drawing.Point(0, 0);
            this.Panel_ProjectTopBase.Name = "Panel_ProjectTopBase";
            this.Panel_ProjectTopBase.Size = new System.Drawing.Size(129, 30);
            this.Panel_ProjectTopBase.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 20);
            this.button1.TabIndex = 0;
            this.button1.Text = "+Motion";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_MotionAdd_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel_ProjectTree_base);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.PanelPreView);
            this.splitContainer1.Panel2.Controls.Add(this.PanelToolBase);
            this.splitContainer1.Size = new System.Drawing.Size(656, 365);
            this.splitContainer1.SplitterDistance = 129;
            this.splitContainer1.TabIndex = 6;
            // 
            // SubMenu_Prpject
            // 
            this.SubMenu_Prpject.Name = "SubMenu_Prpject";
            this.SubMenu_Prpject.ShowImageMargin = false;
            this.SubMenu_Prpject.Size = new System.Drawing.Size(36, 4);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 411);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::PrjHikariwoAnim.Properties.Settings.Default, "FormMainLocate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Location = global::PrjHikariwoAnim.Properties.Settings.Default.FormMainLocate;
            this.Name = "FormMain";
            this.Text = "PrjHikariwoAnim";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.PanelToolBase.ResumeLayout(false);
            this.PanelToolBase.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_Grid)).EndInit();
            this.panel_ProjectTree_base.ResumeLayout(false);
            this.panel_Project_Bottom.ResumeLayout(false);
            this.Panel_ProjectTopBase.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.Panel PanelPreView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ウインドウToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSmenu_ImageList;
        private System.Windows.Forms.ToolStripMenuItem TSmenu_Control;
        private System.Windows.Forms.ToolStripMenuItem TSmenu_Attribute;
        private System.Windows.Forms.Panel PanelToolBase;
        private System.Windows.Forms.CheckBox SnapCheck;
        private System.Windows.Forms.HScrollBar ZoomLevel;
        private System.Windows.Forms.NumericUpDown Num_Grid;
        private System.Windows.Forms.Button GridColor;
        private System.Windows.Forms.CheckBox GridCheck;
        private System.Windows.Forms.Button CrossColor;
        private System.Windows.Forms.CheckBox CrossBarCheck;
        private System.Windows.Forms.ToolStripMenuItem cellListToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel Spaceer1;
        private System.Windows.Forms.Button AlingForm;
        private System.Windows.Forms.Panel panel_ProjectTree_base;
        private System.Windows.Forms.Panel panel_Project_Bottom;
        private System.Windows.Forms.TreeView treeView_Project;
        private System.Windows.Forms.Panel Panel_ProjectTopBase;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip SubMenu_Prpject;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新規プロジェクトToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem プロジェクトの読込ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem プロジェクト保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.Button BottonTest;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel2;
        private System.Windows.Forms.ImageList imageList_Thumb;
        private System.Windows.Forms.ToolStripMenuItem exportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nowFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cellListToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nowAttributeToolStripMenuItem;
        public System.Windows.Forms.CheckBox CB_CellList;
        public System.Windows.Forms.CheckBox CB_Attribute;
        public System.Windows.Forms.CheckBox CB_Control;
        public System.Windows.Forms.CheckBox CB_ImageList;
    }
}

