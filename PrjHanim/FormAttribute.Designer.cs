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
            this.checkT = new System.Windows.Forms.CheckBox();
            this.checkFlipH = new System.Windows.Forms.CheckBox();
            this.checkFlipV = new System.Windows.Forms.CheckBox();
            this.checkVisible = new System.Windows.Forms.CheckBox();
            this.checkColor = new System.Windows.Forms.CheckBox();
            this.checkXoff = new System.Windows.Forms.CheckBox();
            this.checkYoff = new System.Windows.Forms.CheckBox();
            this.checkUserText = new System.Windows.Forms.CheckBox();
            this.checkSY = new System.Windows.Forms.CheckBox();
            this.checkSX = new System.Windows.Forms.CheckBox();
            this.checkRot = new System.Windows.Forms.CheckBox();
            this.checkY = new System.Windows.Forms.CheckBox();
            this.checkX = new System.Windows.Forms.CheckBox();
            this.UDnumYoff = new System.Windows.Forms.NumericUpDown();
            this.UDnumXoff = new System.Windows.Forms.NumericUpDown();
            this.UDnumT = new System.Windows.Forms.NumericUpDown();
            this.UDnumSY = new System.Windows.Forms.NumericUpDown();
            this.UDnumSX = new System.Windows.Forms.NumericUpDown();
            this.UDnumRot = new System.Windows.Forms.NumericUpDown();
            this.UDnumY = new System.Windows.Forms.NumericUpDown();
            this.UDnumX = new System.Windows.Forms.NumericUpDown();
            this.ColorPanel = new System.Windows.Forms.Panel();
            this.UserText = new System.Windows.Forms.TextBox();
            this.ColorCode = new System.Windows.Forms.TextBox();
            this.panel_Attribute_Base = new System.Windows.Forms.Panel();
            this.button_Yoff = new System.Windows.Forms.Button();
            this.button_Xoff = new System.Windows.Forms.Button();
            this.button_Color = new System.Windows.Forms.Button();
            this.button_T = new System.Windows.Forms.Button();
            this.button_SY = new System.Windows.Forms.Button();
            this.button_SX = new System.Windows.Forms.Button();
            this.button_Rot = new System.Windows.Forms.Button();
            this.button_Y = new System.Windows.Forms.Button();
            this.button_X = new System.Windows.Forms.Button();
            this.UDnumColRate = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumYoff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumXoff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumSY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumSX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumRot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumX)).BeginInit();
            this.panel_Attribute_Base.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UDnumColRate)).BeginInit();
            this.SuspendLayout();
            // 
            // checkT
            // 
            this.checkT.AutoSize = true;
            this.checkT.Checked = true;
            this.checkT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkT.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkT.Location = new System.Drawing.Point(5, 130);
            this.checkT.Margin = new System.Windows.Forms.Padding(0);
            this.checkT.Name = "checkT";
            this.checkT.Size = new System.Drawing.Size(119, 20);
            this.checkT.TabIndex = 7;
            this.checkT.Text = "Transparency";
            this.checkT.UseVisualStyleBackColor = true;
            this.checkT.CheckStateChanged += new System.EventHandler(this.checkUserText_CheckStateChanged);
            // 
            // checkFlipH
            // 
            this.checkFlipH.AutoSize = true;
            this.checkFlipH.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkFlipH.Location = new System.Drawing.Point(5, 155);
            this.checkFlipH.Margin = new System.Windows.Forms.Padding(0);
            this.checkFlipH.Name = "checkFlipH";
            this.checkFlipH.Size = new System.Drawing.Size(118, 20);
            this.checkFlipH.TabIndex = 10;
            this.checkFlipH.Text = "Horizontal flip";
            this.checkFlipH.UseVisualStyleBackColor = true;
            this.checkFlipH.CheckStateChanged += new System.EventHandler(this.checkUserText_CheckStateChanged);
            // 
            // checkFlipV
            // 
            this.checkFlipV.AutoSize = true;
            this.checkFlipV.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkFlipV.Location = new System.Drawing.Point(5, 180);
            this.checkFlipV.Margin = new System.Windows.Forms.Padding(0);
            this.checkFlipV.Name = "checkFlipV";
            this.checkFlipV.Size = new System.Drawing.Size(103, 20);
            this.checkFlipV.TabIndex = 12;
            this.checkFlipV.Text = "Vertical flip";
            this.checkFlipV.UseVisualStyleBackColor = true;
            this.checkFlipV.CheckStateChanged += new System.EventHandler(this.checkUserText_CheckStateChanged);
            // 
            // checkVisible
            // 
            this.checkVisible.AutoSize = true;
            this.checkVisible.Checked = true;
            this.checkVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkVisible.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkVisible.Location = new System.Drawing.Point(5, 205);
            this.checkVisible.Margin = new System.Windows.Forms.Padding(0);
            this.checkVisible.Name = "checkVisible";
            this.checkVisible.Size = new System.Drawing.Size(72, 20);
            this.checkVisible.TabIndex = 14;
            this.checkVisible.Text = "display";
            this.checkVisible.UseVisualStyleBackColor = true;
            this.checkVisible.CheckStateChanged += new System.EventHandler(this.checkUserText_CheckStateChanged);
            // 
            // checkColor
            // 
            this.checkColor.AutoSize = true;
            this.checkColor.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkColor.Location = new System.Drawing.Point(5, 230);
            this.checkColor.Margin = new System.Windows.Forms.Padding(0);
            this.checkColor.Name = "checkColor";
            this.checkColor.Size = new System.Drawing.Size(63, 20);
            this.checkColor.TabIndex = 16;
            this.checkColor.Text = "Color";
            this.checkColor.UseVisualStyleBackColor = true;
            this.checkColor.CheckStateChanged += new System.EventHandler(this.checkUserText_CheckStateChanged);
            // 
            // checkXoff
            // 
            this.checkXoff.AutoSize = true;
            this.checkXoff.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkXoff.Location = new System.Drawing.Point(5, 283);
            this.checkXoff.Margin = new System.Windows.Forms.Padding(0);
            this.checkXoff.Name = "checkXoff";
            this.checkXoff.Size = new System.Drawing.Size(84, 20);
            this.checkXoff.TabIndex = 20;
            this.checkXoff.Text = "Offset X";
            this.checkXoff.UseVisualStyleBackColor = true;
            this.checkXoff.CheckStateChanged += new System.EventHandler(this.checkUserText_CheckStateChanged);
            // 
            // checkYoff
            // 
            this.checkYoff.AutoSize = true;
            this.checkYoff.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkYoff.Location = new System.Drawing.Point(5, 308);
            this.checkYoff.Margin = new System.Windows.Forms.Padding(0);
            this.checkYoff.Name = "checkYoff";
            this.checkYoff.Size = new System.Drawing.Size(83, 20);
            this.checkYoff.TabIndex = 22;
            this.checkYoff.Text = "Offset Y";
            this.checkYoff.UseVisualStyleBackColor = true;
            this.checkYoff.CheckStateChanged += new System.EventHandler(this.checkUserText_CheckStateChanged);
            // 
            // checkUserText
            // 
            this.checkUserText.AutoSize = true;
            this.checkUserText.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkUserText.Location = new System.Drawing.Point(5, 331);
            this.checkUserText.Margin = new System.Windows.Forms.Padding(0);
            this.checkUserText.Name = "checkUserText";
            this.checkUserText.Size = new System.Drawing.Size(135, 20);
            this.checkUserText.TabIndex = 24;
            this.checkUserText.Text = "User data (text)";
            this.checkUserText.UseVisualStyleBackColor = true;
            this.checkUserText.CheckStateChanged += new System.EventHandler(this.checkUserText_CheckStateChanged);
            // 
            // checkSY
            // 
            this.checkSY.AutoSize = true;
            this.checkSY.Checked = true;
            this.checkSY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkSY.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkSY.Location = new System.Drawing.Point(5, 105);
            this.checkSY.Margin = new System.Windows.Forms.Padding(0);
            this.checkSY.Name = "checkSY";
            this.checkSY.Size = new System.Drawing.Size(78, 20);
            this.checkSY.TabIndex = 25;
            this.checkSY.Text = "Scale Y";
            this.checkSY.UseVisualStyleBackColor = true;
            this.checkSY.CheckStateChanged += new System.EventHandler(this.checkUserText_CheckStateChanged);
            // 
            // checkSX
            // 
            this.checkSX.AutoSize = true;
            this.checkSX.Checked = true;
            this.checkSX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkSX.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkSX.Location = new System.Drawing.Point(5, 80);
            this.checkSX.Margin = new System.Windows.Forms.Padding(0);
            this.checkSX.Name = "checkSX";
            this.checkSX.Size = new System.Drawing.Size(79, 20);
            this.checkSX.TabIndex = 26;
            this.checkSX.Text = "Scale X";
            this.checkSX.UseVisualStyleBackColor = true;
            this.checkSX.CheckStateChanged += new System.EventHandler(this.checkUserText_CheckStateChanged);
            // 
            // checkRot
            // 
            this.checkRot.AutoSize = true;
            this.checkRot.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkRot.Location = new System.Drawing.Point(5, 55);
            this.checkRot.Margin = new System.Windows.Forms.Padding(0);
            this.checkRot.Name = "checkRot";
            this.checkRot.Size = new System.Drawing.Size(84, 20);
            this.checkRot.TabIndex = 29;
            this.checkRot.Text = "Rotation";
            this.checkRot.UseVisualStyleBackColor = true;
            this.checkRot.CheckStateChanged += new System.EventHandler(this.checkUserText_CheckStateChanged);
            // 
            // checkY
            // 
            this.checkY.AutoSize = true;
            this.checkY.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkY.Location = new System.Drawing.Point(5, 30);
            this.checkY.Margin = new System.Windows.Forms.Padding(0);
            this.checkY.Name = "checkY";
            this.checkY.Size = new System.Drawing.Size(94, 20);
            this.checkY.TabIndex = 30;
            this.checkY.Text = "Position Y";
            this.checkY.UseVisualStyleBackColor = true;
            this.checkY.CheckStateChanged += new System.EventHandler(this.checkUserText_CheckStateChanged);
            // 
            // checkX
            // 
            this.checkX.AutoSize = true;
            this.checkX.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkX.Location = new System.Drawing.Point(5, 5);
            this.checkX.Margin = new System.Windows.Forms.Padding(0);
            this.checkX.Name = "checkX";
            this.checkX.Size = new System.Drawing.Size(95, 20);
            this.checkX.TabIndex = 31;
            this.checkX.Text = "Position X";
            this.checkX.UseVisualStyleBackColor = true;
            this.checkX.CheckStateChanged += new System.EventHandler(this.checkUserText_CheckStateChanged);
            // 
            // UDnumYoff
            // 
            this.UDnumYoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UDnumYoff.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UDnumYoff.Location = new System.Drawing.Point(156, 307);
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
            this.UDnumXoff.Location = new System.Drawing.Point(156, 282);
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
            this.UDnumT.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UDnumT.Location = new System.Drawing.Point(156, 129);
            this.UDnumT.Margin = new System.Windows.Forms.Padding(0);
            this.UDnumT.Name = "UDnumT";
            this.UDnumT.Size = new System.Drawing.Size(70, 23);
            this.UDnumT.TabIndex = 37;
            this.UDnumT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UDnumT.Value = new decimal(new int[] {
            100,
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
            this.UDnumSY.Location = new System.Drawing.Point(156, 104);
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
            this.UDnumSX.Location = new System.Drawing.Point(156, 79);
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
            this.UDnumRot.DecimalPlaces = 3;
            this.UDnumRot.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UDnumRot.Location = new System.Drawing.Point(156, 54);
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
            this.UDnumY.Location = new System.Drawing.Point(156, 29);
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
            this.UDnumX.Location = new System.Drawing.Point(156, 4);
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
            this.ColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorPanel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ColorPanel.Location = new System.Drawing.Point(70, 228);
            this.ColorPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ColorPanel.Name = "ColorPanel";
            this.ColorPanel.Size = new System.Drawing.Size(29, 23);
            this.ColorPanel.TabIndex = 45;
            this.ColorPanel.Click += new System.EventHandler(this.ColorPanel_Click);
            // 
            // UserText
            // 
            this.UserText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserText.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UserText.Location = new System.Drawing.Point(5, 355);
            this.UserText.Margin = new System.Windows.Forms.Padding(0);
            this.UserText.Multiline = true;
            this.UserText.Name = "UserText";
            this.UserText.Size = new System.Drawing.Size(245, 113);
            this.UserText.TabIndex = 47;
            // 
            // ColorCode
            // 
            this.ColorCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ColorCode.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ColorCode.Location = new System.Drawing.Point(99, 228);
            this.ColorCode.Margin = new System.Windows.Forms.Padding(0);
            this.ColorCode.Name = "ColorCode";
            this.ColorCode.Size = new System.Drawing.Size(75, 23);
            this.ColorCode.TabIndex = 48;
            this.ColorCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ColorCode.TextChanged += new System.EventHandler(this.ColorCode_TextChanged);
            // 
            // panel_Attribute_Base
            // 
            this.panel_Attribute_Base.Controls.Add(this.button_Yoff);
            this.panel_Attribute_Base.Controls.Add(this.button_Xoff);
            this.panel_Attribute_Base.Controls.Add(this.button_Color);
            this.panel_Attribute_Base.Controls.Add(this.button_T);
            this.panel_Attribute_Base.Controls.Add(this.button_SY);
            this.panel_Attribute_Base.Controls.Add(this.button_SX);
            this.panel_Attribute_Base.Controls.Add(this.button_Rot);
            this.panel_Attribute_Base.Controls.Add(this.button_Y);
            this.panel_Attribute_Base.Controls.Add(this.button_X);
            this.panel_Attribute_Base.Controls.Add(this.UDnumColRate);
            this.panel_Attribute_Base.Controls.Add(this.ColorCode);
            this.panel_Attribute_Base.Controls.Add(this.UserText);
            this.panel_Attribute_Base.Controls.Add(this.ColorPanel);
            this.panel_Attribute_Base.Controls.Add(this.UDnumX);
            this.panel_Attribute_Base.Controls.Add(this.UDnumY);
            this.panel_Attribute_Base.Controls.Add(this.UDnumRot);
            this.panel_Attribute_Base.Controls.Add(this.UDnumSX);
            this.panel_Attribute_Base.Controls.Add(this.UDnumSY);
            this.panel_Attribute_Base.Controls.Add(this.UDnumT);
            this.panel_Attribute_Base.Controls.Add(this.UDnumXoff);
            this.panel_Attribute_Base.Controls.Add(this.UDnumYoff);
            this.panel_Attribute_Base.Controls.Add(this.checkX);
            this.panel_Attribute_Base.Controls.Add(this.checkY);
            this.panel_Attribute_Base.Controls.Add(this.checkRot);
            this.panel_Attribute_Base.Controls.Add(this.checkSX);
            this.panel_Attribute_Base.Controls.Add(this.checkSY);
            this.panel_Attribute_Base.Controls.Add(this.checkUserText);
            this.panel_Attribute_Base.Controls.Add(this.checkYoff);
            this.panel_Attribute_Base.Controls.Add(this.checkXoff);
            this.panel_Attribute_Base.Controls.Add(this.checkColor);
            this.panel_Attribute_Base.Controls.Add(this.checkVisible);
            this.panel_Attribute_Base.Controls.Add(this.checkFlipV);
            this.panel_Attribute_Base.Controls.Add(this.checkFlipH);
            this.panel_Attribute_Base.Controls.Add(this.checkT);
            this.panel_Attribute_Base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Attribute_Base.Location = new System.Drawing.Point(0, 0);
            this.panel_Attribute_Base.Name = "panel_Attribute_Base";
            this.panel_Attribute_Base.Size = new System.Drawing.Size(256, 473);
            this.panel_Attribute_Base.TabIndex = 49;
            // 
            // button_Yoff
            // 
            this.button_Yoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Yoff.Location = new System.Drawing.Point(229, 306);
            this.button_Yoff.Name = "button_Yoff";
            this.button_Yoff.Size = new System.Drawing.Size(23, 23);
            this.button_Yoff.TabIndex = 60;
            this.button_Yoff.UseVisualStyleBackColor = true;
            this.button_Yoff.Click += new System.EventHandler(this.button_Yoff_Click);
            // 
            // button_Xoff
            // 
            this.button_Xoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Xoff.Location = new System.Drawing.Point(229, 281);
            this.button_Xoff.Name = "button_Xoff";
            this.button_Xoff.Size = new System.Drawing.Size(23, 23);
            this.button_Xoff.TabIndex = 59;
            this.button_Xoff.UseVisualStyleBackColor = true;
            this.button_Xoff.Click += new System.EventHandler(this.button_Xoff_Click);
            // 
            // button_Color
            // 
            this.button_Color.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Color.Location = new System.Drawing.Point(229, 227);
            this.button_Color.Name = "button_Color";
            this.button_Color.Size = new System.Drawing.Size(23, 23);
            this.button_Color.TabIndex = 58;
            this.button_Color.UseVisualStyleBackColor = true;
            this.button_Color.Click += new System.EventHandler(this.button_Color_Click);
            // 
            // button_T
            // 
            this.button_T.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_T.Location = new System.Drawing.Point(229, 130);
            this.button_T.Name = "button_T";
            this.button_T.Size = new System.Drawing.Size(23, 23);
            this.button_T.TabIndex = 57;
            this.button_T.UseVisualStyleBackColor = true;
            this.button_T.Click += new System.EventHandler(this.button_T_Click);
            // 
            // button_SY
            // 
            this.button_SY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SY.Location = new System.Drawing.Point(229, 105);
            this.button_SY.Name = "button_SY";
            this.button_SY.Size = new System.Drawing.Size(23, 23);
            this.button_SY.TabIndex = 56;
            this.button_SY.UseVisualStyleBackColor = true;
            this.button_SY.Click += new System.EventHandler(this.button_SY_Click);
            // 
            // button_SX
            // 
            this.button_SX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SX.Location = new System.Drawing.Point(229, 80);
            this.button_SX.Name = "button_SX";
            this.button_SX.Size = new System.Drawing.Size(23, 23);
            this.button_SX.TabIndex = 55;
            this.button_SX.UseVisualStyleBackColor = true;
            this.button_SX.Click += new System.EventHandler(this.button_SX_Click);
            // 
            // button_Rot
            // 
            this.button_Rot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Rot.Location = new System.Drawing.Point(229, 55);
            this.button_Rot.Name = "button_Rot";
            this.button_Rot.Size = new System.Drawing.Size(23, 23);
            this.button_Rot.TabIndex = 52;
            this.button_Rot.UseVisualStyleBackColor = true;
            this.button_Rot.Click += new System.EventHandler(this.button_RX_Click);
            // 
            // button_Y
            // 
            this.button_Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Y.Location = new System.Drawing.Point(229, 30);
            this.button_Y.Name = "button_Y";
            this.button_Y.Size = new System.Drawing.Size(23, 23);
            this.button_Y.TabIndex = 51;
            this.button_Y.UseVisualStyleBackColor = true;
            this.button_Y.Click += new System.EventHandler(this.button_Y_Click);
            // 
            // button_X
            // 
            this.button_X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_X.Location = new System.Drawing.Point(229, 5);
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
            this.UDnumColRate.Location = new System.Drawing.Point(174, 228);
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
            // FormAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 473);
            this.Controls.Add(this.panel_Attribute_Base);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormAttribute";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Attribute";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAttribute_FormClosing);
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
            ((System.ComponentModel.ISupportInitialize)(this.UDnumColRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkT;
        private System.Windows.Forms.CheckBox checkFlipH;
        private System.Windows.Forms.CheckBox checkFlipV;
        private System.Windows.Forms.CheckBox checkVisible;
        private System.Windows.Forms.CheckBox checkColor;
        private System.Windows.Forms.CheckBox checkXoff;
        private System.Windows.Forms.CheckBox checkYoff;
        private System.Windows.Forms.CheckBox checkUserText;
        private System.Windows.Forms.CheckBox checkSY;
        private System.Windows.Forms.CheckBox checkSX;
        private System.Windows.Forms.CheckBox checkRot;
        private System.Windows.Forms.CheckBox checkY;
        private System.Windows.Forms.CheckBox checkX;
        private System.Windows.Forms.NumericUpDown UDnumYoff;
        private System.Windows.Forms.NumericUpDown UDnumXoff;
        private System.Windows.Forms.NumericUpDown UDnumT;
        private System.Windows.Forms.NumericUpDown UDnumSY;
        private System.Windows.Forms.NumericUpDown UDnumSX;
        private System.Windows.Forms.NumericUpDown UDnumRot;
        private System.Windows.Forms.NumericUpDown UDnumY;
        private System.Windows.Forms.NumericUpDown UDnumX;
        private System.Windows.Forms.Panel ColorPanel;
        private System.Windows.Forms.TextBox UserText;
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
    }
}