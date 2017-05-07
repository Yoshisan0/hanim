namespace PrjHikariwoAnim
{
    partial class FormAttribute
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
            this.checkBox_EnableTrans = new System.Windows.Forms.CheckBox();
            this.checkBox_EnableFlipH = new System.Windows.Forms.CheckBox();
            this.checkBox_EnableFlipV = new System.Windows.Forms.CheckBox();
            this.checkBox_EnableColor = new System.Windows.Forms.CheckBox();
            this.checkBox_EnableCX = new System.Windows.Forms.CheckBox();
            this.checkBox_EnableCY = new System.Windows.Forms.CheckBox();
            this.checkBox_EnableText = new System.Windows.Forms.CheckBox();
            this.checkBox_EnableSY = new System.Windows.Forms.CheckBox();
            this.checkBox_EnableSX = new System.Windows.Forms.CheckBox();
            this.checkBox_EnableRZ = new System.Windows.Forms.CheckBox();
            this.UDnumYoff = new System.Windows.Forms.NumericUpDown();
            this.UDnumXoff = new System.Windows.Forms.NumericUpDown();
            this.UDnumT = new System.Windows.Forms.NumericUpDown();
            this.UDnumSY = new System.Windows.Forms.NumericUpDown();
            this.UDnumSX = new System.Windows.Forms.NumericUpDown();
            this.UDnumRot = new System.Windows.Forms.NumericUpDown();
            this.UDnumY = new System.Windows.Forms.NumericUpDown();
            this.UDnumX = new System.Windows.Forms.NumericUpDown();
            this.ColorPanel = new System.Windows.Forms.Panel();
            this.textBox_User = new System.Windows.Forms.TextBox();
            this.ColorCode = new System.Windows.Forms.TextBox();
            this.panel_Attribute_Base = new System.Windows.Forms.Panel();
            this.groupBox_Param = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_Display = new System.Windows.Forms.CheckBox();
            this.checkBox_FlipV = new System.Windows.Forms.CheckBox();
            this.checkBox_FlipH = new System.Windows.Forms.CheckBox();
            this.button_Yoff = new System.Windows.Forms.Button();
            this.button_Xoff = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button_Color = new System.Windows.Forms.Button();
            this.button_T = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button_SY = new System.Windows.Forms.Button();
            this.button_SX = new System.Windows.Forms.Button();
            this.button_Rot = new System.Windows.Forms.Button();
            this.button_Y = new System.Windows.Forms.Button();
            this.button_X = new System.Windows.Forms.Button();
            this.UDnumColRate = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumYoff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumXoff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumSY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumSX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumRot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumX)).BeginInit();
            this.panel_Attribute_Base.SuspendLayout();
            this.groupBox_Param.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumColRate)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox_EnableTrans
            // 
            this.checkBox_EnableTrans.AutoSize = true;
            this.checkBox_EnableTrans.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_EnableTrans.Location = new System.Drawing.Point(15, 170);
            this.checkBox_EnableTrans.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_EnableTrans.Name = "checkBox_EnableTrans";
            this.checkBox_EnableTrans.Size = new System.Drawing.Size(119, 20);
            this.checkBox_EnableTrans.TabIndex = 7;
            this.checkBox_EnableTrans.Text = "Transparency";
            this.checkBox_EnableTrans.UseVisualStyleBackColor = true;
            this.checkBox_EnableTrans.CheckStateChanged += new System.EventHandler(this.checkBox_CheckStateChanged);
            // 
            // checkBox_EnableFlipH
            // 
            this.checkBox_EnableFlipH.AutoSize = true;
            this.checkBox_EnableFlipH.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_EnableFlipH.Location = new System.Drawing.Point(15, 195);
            this.checkBox_EnableFlipH.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_EnableFlipH.Name = "checkBox_EnableFlipH";
            this.checkBox_EnableFlipH.Size = new System.Drawing.Size(118, 20);
            this.checkBox_EnableFlipH.TabIndex = 10;
            this.checkBox_EnableFlipH.Text = "Horizontal flip";
            this.checkBox_EnableFlipH.UseVisualStyleBackColor = true;
            this.checkBox_EnableFlipH.CheckStateChanged += new System.EventHandler(this.checkBox_CheckStateChanged);
            // 
            // checkBox_EnableFlipV
            // 
            this.checkBox_EnableFlipV.AutoSize = true;
            this.checkBox_EnableFlipV.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_EnableFlipV.Location = new System.Drawing.Point(15, 220);
            this.checkBox_EnableFlipV.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_EnableFlipV.Name = "checkBox_EnableFlipV";
            this.checkBox_EnableFlipV.Size = new System.Drawing.Size(103, 20);
            this.checkBox_EnableFlipV.TabIndex = 12;
            this.checkBox_EnableFlipV.Text = "Vertical flip";
            this.checkBox_EnableFlipV.UseVisualStyleBackColor = true;
            this.checkBox_EnableFlipV.CheckStateChanged += new System.EventHandler(this.checkBox_CheckStateChanged);
            // 
            // checkBox_EnableColor
            // 
            this.checkBox_EnableColor.AutoSize = true;
            this.checkBox_EnableColor.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_EnableColor.Location = new System.Drawing.Point(15, 245);
            this.checkBox_EnableColor.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_EnableColor.Name = "checkBox_EnableColor";
            this.checkBox_EnableColor.Size = new System.Drawing.Size(63, 20);
            this.checkBox_EnableColor.TabIndex = 16;
            this.checkBox_EnableColor.Text = "Color";
            this.checkBox_EnableColor.UseVisualStyleBackColor = true;
            this.checkBox_EnableColor.CheckStateChanged += new System.EventHandler(this.checkBox_CheckStateChanged);
            // 
            // checkBox_EnableCX
            // 
            this.checkBox_EnableCX.AutoSize = true;
            this.checkBox_EnableCX.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_EnableCX.Location = new System.Drawing.Point(15, 270);
            this.checkBox_EnableCX.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_EnableCX.Name = "checkBox_EnableCX";
            this.checkBox_EnableCX.Size = new System.Drawing.Size(84, 20);
            this.checkBox_EnableCX.TabIndex = 20;
            this.checkBox_EnableCX.Text = "Offset X";
            this.checkBox_EnableCX.UseVisualStyleBackColor = true;
            this.checkBox_EnableCX.CheckStateChanged += new System.EventHandler(this.checkBox_CheckStateChanged);
            // 
            // checkBox_EnableCY
            // 
            this.checkBox_EnableCY.AutoSize = true;
            this.checkBox_EnableCY.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_EnableCY.Location = new System.Drawing.Point(15, 295);
            this.checkBox_EnableCY.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_EnableCY.Name = "checkBox_EnableCY";
            this.checkBox_EnableCY.Size = new System.Drawing.Size(83, 20);
            this.checkBox_EnableCY.TabIndex = 22;
            this.checkBox_EnableCY.Text = "Offset Y";
            this.checkBox_EnableCY.UseVisualStyleBackColor = true;
            this.checkBox_EnableCY.CheckStateChanged += new System.EventHandler(this.checkBox_CheckStateChanged);
            // 
            // checkBox_EnableText
            // 
            this.checkBox_EnableText.AutoSize = true;
            this.checkBox_EnableText.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_EnableText.Location = new System.Drawing.Point(15, 320);
            this.checkBox_EnableText.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_EnableText.Name = "checkBox_EnableText";
            this.checkBox_EnableText.Size = new System.Drawing.Size(135, 20);
            this.checkBox_EnableText.TabIndex = 24;
            this.checkBox_EnableText.Text = "User data (text)";
            this.checkBox_EnableText.UseVisualStyleBackColor = true;
            this.checkBox_EnableText.CheckStateChanged += new System.EventHandler(this.checkBox_CheckStateChanged);
            // 
            // checkBox_EnableSY
            // 
            this.checkBox_EnableSY.AutoSize = true;
            this.checkBox_EnableSY.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_EnableSY.Location = new System.Drawing.Point(15, 145);
            this.checkBox_EnableSY.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_EnableSY.Name = "checkBox_EnableSY";
            this.checkBox_EnableSY.Size = new System.Drawing.Size(78, 20);
            this.checkBox_EnableSY.TabIndex = 25;
            this.checkBox_EnableSY.Text = "Scale Y";
            this.checkBox_EnableSY.UseVisualStyleBackColor = true;
            this.checkBox_EnableSY.CheckStateChanged += new System.EventHandler(this.checkBox_CheckStateChanged);
            // 
            // checkBox_EnableSX
            // 
            this.checkBox_EnableSX.AutoSize = true;
            this.checkBox_EnableSX.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_EnableSX.Location = new System.Drawing.Point(15, 120);
            this.checkBox_EnableSX.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_EnableSX.Name = "checkBox_EnableSX";
            this.checkBox_EnableSX.Size = new System.Drawing.Size(79, 20);
            this.checkBox_EnableSX.TabIndex = 26;
            this.checkBox_EnableSX.Text = "Scale X";
            this.checkBox_EnableSX.UseVisualStyleBackColor = true;
            this.checkBox_EnableSX.CheckStateChanged += new System.EventHandler(this.checkBox_CheckStateChanged);
            // 
            // checkBox_EnableRZ
            // 
            this.checkBox_EnableRZ.AutoSize = true;
            this.checkBox_EnableRZ.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_EnableRZ.Location = new System.Drawing.Point(15, 95);
            this.checkBox_EnableRZ.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_EnableRZ.Name = "checkBox_EnableRZ";
            this.checkBox_EnableRZ.Size = new System.Drawing.Size(84, 20);
            this.checkBox_EnableRZ.TabIndex = 29;
            this.checkBox_EnableRZ.Text = "Rotation";
            this.checkBox_EnableRZ.UseVisualStyleBackColor = true;
            this.checkBox_EnableRZ.CheckStateChanged += new System.EventHandler(this.checkBox_CheckStateChanged);
            // 
            // UDnumYoff
            // 
            this.UDnumYoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UDnumYoff.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UDnumYoff.Location = new System.Drawing.Point(191, 294);
            this.UDnumYoff.Margin = new System.Windows.Forms.Padding(0);
            this.UDnumYoff.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.UDnumYoff.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.UDnumYoff.Name = "UDnumYoff";
            this.UDnumYoff.Size = new System.Drawing.Size(70, 23);
            this.UDnumYoff.TabIndex = 33;
            this.UDnumYoff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UDnumYoff.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.UDnumYoff.ValueChanged += new System.EventHandler(this.UDnum_ValueChanged);
            // 
            // UDnumXoff
            // 
            this.UDnumXoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UDnumXoff.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UDnumXoff.Location = new System.Drawing.Point(191, 269);
            this.UDnumXoff.Margin = new System.Windows.Forms.Padding(0);
            this.UDnumXoff.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.UDnumXoff.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.UDnumXoff.Name = "UDnumXoff";
            this.UDnumXoff.Size = new System.Drawing.Size(70, 23);
            this.UDnumXoff.TabIndex = 34;
            this.UDnumXoff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UDnumXoff.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.UDnumXoff.ValueChanged += new System.EventHandler(this.UDnum_ValueChanged);
            // 
            // UDnumT
            // 
            this.UDnumT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UDnumT.DecimalPlaces = 3;
            this.UDnumT.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UDnumT.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.UDnumT.Location = new System.Drawing.Point(191, 169);
            this.UDnumT.Margin = new System.Windows.Forms.Padding(0);
            this.UDnumT.Name = "UDnumT";
            this.UDnumT.Size = new System.Drawing.Size(70, 23);
            this.UDnumT.TabIndex = 37;
            this.UDnumT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UDnumT.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UDnumT.ValueChanged += new System.EventHandler(this.UDnum_ValueChanged);
            // 
            // UDnumSY
            // 
            this.UDnumSY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UDnumSY.DecimalPlaces = 3;
            this.UDnumSY.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UDnumSY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.UDnumSY.Location = new System.Drawing.Point(191, 144);
            this.UDnumSY.Margin = new System.Windows.Forms.Padding(0);
            this.UDnumSY.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UDnumSY.Name = "UDnumSY";
            this.UDnumSY.Size = new System.Drawing.Size(70, 23);
            this.UDnumSY.TabIndex = 38;
            this.UDnumSY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UDnumSY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UDnumSY.ValueChanged += new System.EventHandler(this.UDnum_ValueChanged);
            // 
            // UDnumSX
            // 
            this.UDnumSX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UDnumSX.DecimalPlaces = 3;
            this.UDnumSX.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UDnumSX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.UDnumSX.Location = new System.Drawing.Point(191, 119);
            this.UDnumSX.Margin = new System.Windows.Forms.Padding(0);
            this.UDnumSX.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UDnumSX.Name = "UDnumSX";
            this.UDnumSX.Size = new System.Drawing.Size(70, 23);
            this.UDnumSX.TabIndex = 39;
            this.UDnumSX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UDnumSX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UDnumSX.ValueChanged += new System.EventHandler(this.UDnum_ValueChanged);
            // 
            // UDnumRot
            // 
            this.UDnumRot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UDnumRot.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UDnumRot.Location = new System.Drawing.Point(191, 94);
            this.UDnumRot.Margin = new System.Windows.Forms.Padding(0);
            this.UDnumRot.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.UDnumRot.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.UDnumRot.Name = "UDnumRot";
            this.UDnumRot.Size = new System.Drawing.Size(70, 23);
            this.UDnumRot.TabIndex = 42;
            this.UDnumRot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UDnumRot.ValueChanged += new System.EventHandler(this.UDnum_ValueChanged);
            // 
            // UDnumY
            // 
            this.UDnumY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UDnumY.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UDnumY.Location = new System.Drawing.Point(191, 68);
            this.UDnumY.Margin = new System.Windows.Forms.Padding(0);
            this.UDnumY.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.UDnumY.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.UDnumY.Name = "UDnumY";
            this.UDnumY.Size = new System.Drawing.Size(70, 23);
            this.UDnumY.TabIndex = 43;
            this.UDnumY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UDnumY.ValueChanged += new System.EventHandler(this.UDnum_ValueChanged);
            // 
            // UDnumX
            // 
            this.UDnumX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UDnumX.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UDnumX.Location = new System.Drawing.Point(191, 43);
            this.UDnumX.Margin = new System.Windows.Forms.Padding(0);
            this.UDnumX.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.UDnumX.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.UDnumX.Name = "UDnumX";
            this.UDnumX.Size = new System.Drawing.Size(70, 23);
            this.UDnumX.TabIndex = 44;
            this.UDnumX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UDnumX.ValueChanged += new System.EventHandler(this.UDnum_ValueChanged);
            // 
            // ColorPanel
            // 
            this.ColorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ColorPanel.BackColor = System.Drawing.Color.White;
            this.ColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorPanel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ColorPanel.Location = new System.Drawing.Point(112, 244);
            this.ColorPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ColorPanel.Name = "ColorPanel";
            this.ColorPanel.Size = new System.Drawing.Size(29, 23);
            this.ColorPanel.TabIndex = 45;
            this.ColorPanel.Click += new System.EventHandler(this.ColorPanel_Click);
            // 
            // textBox_User
            // 
            this.textBox_User.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_User.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_User.Location = new System.Drawing.Point(3, 345);
            this.textBox_User.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_User.Multiline = true;
            this.textBox_User.Name = "textBox_User";
            this.textBox_User.Size = new System.Drawing.Size(287, 114);
            this.textBox_User.TabIndex = 47;
            // 
            // ColorCode
            // 
            this.ColorCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ColorCode.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ColorCode.Location = new System.Drawing.Point(145, 244);
            this.ColorCode.Margin = new System.Windows.Forms.Padding(0);
            this.ColorCode.Name = "ColorCode";
            this.ColorCode.Size = new System.Drawing.Size(60, 23);
            this.ColorCode.TabIndex = 48;
            this.ColorCode.Text = "FFFFFF";
            this.ColorCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ColorCode.TextChanged += new System.EventHandler(this.ColorCode_TextChanged);
            // 
            // panel_Attribute_Base
            // 
            this.panel_Attribute_Base.Controls.Add(this.label4);
            this.panel_Attribute_Base.Controls.Add(this.groupBox_Param);
            this.panel_Attribute_Base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Attribute_Base.Location = new System.Drawing.Point(0, 0);
            this.panel_Attribute_Base.Name = "panel_Attribute_Base";
            this.panel_Attribute_Base.Size = new System.Drawing.Size(317, 498);
            this.panel_Attribute_Base.TabIndex = 49;
            // 
            // groupBox_Param
            // 
            this.groupBox_Param.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Param.Controls.Add(this.label1);
            this.groupBox_Param.Controls.Add(this.checkBox_EnableTrans);
            this.groupBox_Param.Controls.Add(this.checkBox_Display);
            this.groupBox_Param.Controls.Add(this.checkBox_FlipV);
            this.groupBox_Param.Controls.Add(this.checkBox_EnableFlipH);
            this.groupBox_Param.Controls.Add(this.checkBox_FlipH);
            this.groupBox_Param.Controls.Add(this.checkBox_EnableFlipV);
            this.groupBox_Param.Controls.Add(this.button_Yoff);
            this.groupBox_Param.Controls.Add(this.checkBox_EnableColor);
            this.groupBox_Param.Controls.Add(this.button_Xoff);
            this.groupBox_Param.Controls.Add(this.label3);
            this.groupBox_Param.Controls.Add(this.button_Color);
            this.groupBox_Param.Controls.Add(this.checkBox_EnableCX);
            this.groupBox_Param.Controls.Add(this.button_T);
            this.groupBox_Param.Controls.Add(this.label2);
            this.groupBox_Param.Controls.Add(this.button_SY);
            this.groupBox_Param.Controls.Add(this.checkBox_EnableCY);
            this.groupBox_Param.Controls.Add(this.button_SX);
            this.groupBox_Param.Controls.Add(this.checkBox_EnableText);
            this.groupBox_Param.Controls.Add(this.button_Rot);
            this.groupBox_Param.Controls.Add(this.checkBox_EnableSY);
            this.groupBox_Param.Controls.Add(this.button_Y);
            this.groupBox_Param.Controls.Add(this.checkBox_EnableSX);
            this.groupBox_Param.Controls.Add(this.button_X);
            this.groupBox_Param.Controls.Add(this.checkBox_EnableRZ);
            this.groupBox_Param.Controls.Add(this.UDnumColRate);
            this.groupBox_Param.Controls.Add(this.textBox_User);
            this.groupBox_Param.Controls.Add(this.ColorCode);
            this.groupBox_Param.Controls.Add(this.UDnumYoff);
            this.groupBox_Param.Controls.Add(this.ColorPanel);
            this.groupBox_Param.Controls.Add(this.UDnumXoff);
            this.groupBox_Param.Controls.Add(this.UDnumX);
            this.groupBox_Param.Controls.Add(this.UDnumT);
            this.groupBox_Param.Controls.Add(this.UDnumY);
            this.groupBox_Param.Controls.Add(this.UDnumSY);
            this.groupBox_Param.Controls.Add(this.UDnumRot);
            this.groupBox_Param.Controls.Add(this.UDnumSX);
            this.groupBox_Param.Location = new System.Drawing.Point(12, 24);
            this.groupBox_Param.Name = "groupBox_Param";
            this.groupBox_Param.Size = new System.Drawing.Size(293, 462);
            this.groupBox_Param.TabIndex = 67;
            this.groupBox_Param.TabStop = false;
            this.groupBox_Param.Text = "Parameter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(31, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 61;
            this.label1.Text = "Display";
            // 
            // checkBox_Display
            // 
            this.checkBox_Display.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Display.AutoSize = true;
            this.checkBox_Display.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_Display.Location = new System.Drawing.Point(219, 19);
            this.checkBox_Display.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_Display.Name = "checkBox_Display";
            this.checkBox_Display.Size = new System.Drawing.Size(71, 20);
            this.checkBox_Display.TabIndex = 65;
            this.checkBox_Display.Text = "Enable";
            this.checkBox_Display.UseVisualStyleBackColor = true;
            // 
            // checkBox_FlipV
            // 
            this.checkBox_FlipV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_FlipV.AutoSize = true;
            this.checkBox_FlipV.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_FlipV.Location = new System.Drawing.Point(216, 220);
            this.checkBox_FlipV.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_FlipV.Name = "checkBox_FlipV";
            this.checkBox_FlipV.Size = new System.Drawing.Size(71, 20);
            this.checkBox_FlipV.TabIndex = 65;
            this.checkBox_FlipV.Text = "Enable";
            this.checkBox_FlipV.UseVisualStyleBackColor = true;
            this.checkBox_FlipV.CheckedChanged += new System.EventHandler(this.UDnum_ValueChanged);
            // 
            // checkBox_FlipH
            // 
            this.checkBox_FlipH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_FlipH.AutoSize = true;
            this.checkBox_FlipH.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_FlipH.Location = new System.Drawing.Point(216, 195);
            this.checkBox_FlipH.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox_FlipH.Name = "checkBox_FlipH";
            this.checkBox_FlipH.Size = new System.Drawing.Size(71, 20);
            this.checkBox_FlipH.TabIndex = 64;
            this.checkBox_FlipH.Text = "Enable";
            this.checkBox_FlipH.UseVisualStyleBackColor = true;
            this.checkBox_FlipH.CheckedChanged += new System.EventHandler(this.UDnum_ValueChanged);
            // 
            // button_Yoff
            // 
            this.button_Yoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Yoff.Location = new System.Drawing.Point(264, 295);
            this.button_Yoff.Name = "button_Yoff";
            this.button_Yoff.Size = new System.Drawing.Size(23, 23);
            this.button_Yoff.TabIndex = 60;
            this.button_Yoff.UseVisualStyleBackColor = true;
            this.button_Yoff.Click += new System.EventHandler(this.button_Yoff_Click);
            // 
            // button_Xoff
            // 
            this.button_Xoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Xoff.Location = new System.Drawing.Point(264, 270);
            this.button_Xoff.Name = "button_Xoff";
            this.button_Xoff.Size = new System.Drawing.Size(23, 23);
            this.button_Xoff.TabIndex = 59;
            this.button_Xoff.UseVisualStyleBackColor = true;
            this.button_Xoff.Click += new System.EventHandler(this.button_Xoff_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(31, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 63;
            this.label3.Text = "Position Y";
            // 
            // button_Color
            // 
            this.button_Color.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Color.Location = new System.Drawing.Point(264, 245);
            this.button_Color.Name = "button_Color";
            this.button_Color.Size = new System.Drawing.Size(23, 23);
            this.button_Color.TabIndex = 58;
            this.button_Color.UseVisualStyleBackColor = true;
            this.button_Color.Click += new System.EventHandler(this.button_Color_Click);
            // 
            // button_T
            // 
            this.button_T.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_T.Location = new System.Drawing.Point(264, 170);
            this.button_T.Name = "button_T";
            this.button_T.Size = new System.Drawing.Size(23, 23);
            this.button_T.TabIndex = 57;
            this.button_T.UseVisualStyleBackColor = true;
            this.button_T.Click += new System.EventHandler(this.button_T_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(31, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 62;
            this.label2.Text = "Position X";
            // 
            // button_SY
            // 
            this.button_SY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SY.Location = new System.Drawing.Point(264, 145);
            this.button_SY.Name = "button_SY";
            this.button_SY.Size = new System.Drawing.Size(23, 23);
            this.button_SY.TabIndex = 56;
            this.button_SY.UseVisualStyleBackColor = true;
            this.button_SY.Click += new System.EventHandler(this.button_SY_Click);
            // 
            // button_SX
            // 
            this.button_SX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SX.Location = new System.Drawing.Point(264, 120);
            this.button_SX.Name = "button_SX";
            this.button_SX.Size = new System.Drawing.Size(23, 23);
            this.button_SX.TabIndex = 55;
            this.button_SX.UseVisualStyleBackColor = true;
            this.button_SX.Click += new System.EventHandler(this.button_SX_Click);
            // 
            // button_Rot
            // 
            this.button_Rot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Rot.Location = new System.Drawing.Point(264, 95);
            this.button_Rot.Name = "button_Rot";
            this.button_Rot.Size = new System.Drawing.Size(23, 23);
            this.button_Rot.TabIndex = 52;
            this.button_Rot.UseVisualStyleBackColor = true;
            this.button_Rot.Click += new System.EventHandler(this.button_RX_Click);
            // 
            // button_Y
            // 
            this.button_Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Y.Location = new System.Drawing.Point(264, 69);
            this.button_Y.Name = "button_Y";
            this.button_Y.Size = new System.Drawing.Size(23, 23);
            this.button_Y.TabIndex = 51;
            this.button_Y.UseVisualStyleBackColor = true;
            this.button_Y.Click += new System.EventHandler(this.button_Y_Click);
            // 
            // button_X
            // 
            this.button_X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_X.Location = new System.Drawing.Point(264, 44);
            this.button_X.Name = "button_X";
            this.button_X.Size = new System.Drawing.Size(23, 23);
            this.button_X.TabIndex = 50;
            this.button_X.UseVisualStyleBackColor = true;
            this.button_X.Click += new System.EventHandler(this.button_X_Click);
            // 
            // UDnumColRate
            // 
            this.UDnumColRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UDnumColRate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UDnumColRate.Location = new System.Drawing.Point(209, 244);
            this.UDnumColRate.Margin = new System.Windows.Forms.Padding(0);
            this.UDnumColRate.Name = "UDnumColRate";
            this.UDnumColRate.Size = new System.Drawing.Size(52, 23);
            this.UDnumColRate.TabIndex = 49;
            this.UDnumColRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UDnumColRate.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.UDnumColRate.ValueChanged += new System.EventHandler(this.UDnum_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(209, 12);
            this.label4.TabIndex = 68;
            this.label4.Text = "そのフレーム番号でのエレメントの情報となる";
            // 
            // FormAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 498);
            this.Controls.Add(this.panel_Attribute_Base);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(300, 470);
            this.Name = "FormAttribute";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Attribute";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAttribute_FormClosing);
            this.Load += new System.EventHandler(this.FormAttribute_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UDnumYoff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumXoff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumSY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumSX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumRot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumX)).EndInit();
            this.panel_Attribute_Base.ResumeLayout(false);
            this.panel_Attribute_Base.PerformLayout();
            this.groupBox_Param.ResumeLayout(false);
            this.groupBox_Param.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumColRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_EnableTrans;
        private System.Windows.Forms.CheckBox checkBox_EnableFlipH;
        private System.Windows.Forms.CheckBox checkBox_EnableFlipV;
        private System.Windows.Forms.CheckBox checkBox_EnableColor;
        private System.Windows.Forms.CheckBox checkBox_EnableCX;
        private System.Windows.Forms.CheckBox checkBox_EnableCY;
        private System.Windows.Forms.CheckBox checkBox_EnableText;
        private System.Windows.Forms.CheckBox checkBox_EnableSY;
        private System.Windows.Forms.CheckBox checkBox_EnableSX;
        private System.Windows.Forms.CheckBox checkBox_EnableRZ;
        private System.Windows.Forms.NumericUpDown UDnumYoff;
        private System.Windows.Forms.NumericUpDown UDnumXoff;
        private System.Windows.Forms.NumericUpDown UDnumT;
        private System.Windows.Forms.NumericUpDown UDnumSY;
        private System.Windows.Forms.NumericUpDown UDnumSX;
        private System.Windows.Forms.NumericUpDown UDnumRot;
        private System.Windows.Forms.NumericUpDown UDnumY;
        private System.Windows.Forms.NumericUpDown UDnumX;
        private System.Windows.Forms.Panel ColorPanel;
        private System.Windows.Forms.TextBox textBox_User;
        private System.Windows.Forms.TextBox ColorCode;
        private System.Windows.Forms.Panel panel_Attribute_Base;
        private System.Windows.Forms.NumericUpDown UDnumColRate;
        private System.Windows.Forms.Button button_Yoff;
        private System.Windows.Forms.Button button_Xoff;
        private System.Windows.Forms.Button button_Color;
        private System.Windows.Forms.Button button_T;
        private System.Windows.Forms.Button button_SY;
        private System.Windows.Forms.Button button_SX;
        private System.Windows.Forms.Button button_Rot;
        private System.Windows.Forms.Button button_Y;
        private System.Windows.Forms.Button button_X;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_FlipH;
        private System.Windows.Forms.CheckBox checkBox_FlipV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_Display;
        private System.Windows.Forms.GroupBox groupBox_Param;
        private System.Windows.Forms.Label label4;
    }
}