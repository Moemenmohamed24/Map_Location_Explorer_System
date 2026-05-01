namespace GoogleMapsProject1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtType = new TextBox();
            chkWater = new CheckBox();
            isNatural = new CheckBox();
            airQuality = new ComboBox();
            numberOfPlaces = new NumericUpDown();
            btnSearch = new Button();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numberOfPlaces).BeginInit();
            SuspendLayout();
            // 
            // txtType
            // 
            txtType.BackColor = SystemColors.Highlight;
            txtType.Cursor = Cursors.No;
            txtType.ForeColor = SystemColors.Window;
            txtType.Location = new Point(12, 58);
            txtType.Name = "txtType";
            txtType.Size = new Size(125, 27);
            txtType.TabIndex = 0;
            txtType.Text = "Type_of_Place";
            txtType.TextChanged += txtType_TextChanged;
            // 
            // chkWater
            // 
            chkWater.AutoSize = true;
            chkWater.BackColor = SystemColors.MenuHighlight;
            chkWater.Location = new Point(12, 108);
            chkWater.Name = "chkWater";
            chkWater.Size = new Size(86, 24);
            chkWater.TabIndex = 1;
            chkWater.Text = "Is_Warer";
            chkWater.UseVisualStyleBackColor = false;
            chkWater.CheckedChanged += chkWater_CheckedChanged;
            // 
            // isNatural
            // 
            isNatural.AutoSize = true;
            isNatural.ForeColor = SystemColors.ActiveCaption;
            isNatural.Location = new Point(12, 162);
            isNatural.Name = "isNatural";
            isNatural.Size = new Size(96, 24);
            isNatural.TabIndex = 2;
            isNatural.Text = "Is_Natural";
            isNatural.UseVisualStyleBackColor = true;
            isNatural.CheckedChanged += isNatural_CheckedChanged;
            // 
            // airQuality
            // 
            airQuality.FormattingEnabled = true;
            airQuality.Items.AddRange(new object[] { "Good", "Medium", "Bad" });
            airQuality.Location = new Point(12, 211);
            airQuality.Name = "airQuality";
            airQuality.Size = new Size(151, 28);
            airQuality.TabIndex = 3;
            airQuality.Text = "Air_Quality";
            // 
            // numberOfPlaces
            // 
            numberOfPlaces.Location = new Point(12, 273);
            numberOfPlaces.Name = "numberOfPlaces";
            numberOfPlaces.Size = new Size(150, 27);
            numberOfPlaces.TabIndex = 4;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(12, 12);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Search ";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(264, 70);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(480, 218);
            textBox1.TabIndex = 6;
            textBox1.Text = "Result_of_Search:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox1);
            Controls.Add(btnSearch);
            Controls.Add(numberOfPlaces);
            Controls.Add(airQuality);
            Controls.Add(isNatural);
            Controls.Add(chkWater);
            Controls.Add(txtType);
            Name = "Form1";
            Text = "Form1";
            Load += txtType_TextChanged;
            ((System.ComponentModel.ISupportInitialize)numberOfPlaces).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtType;
        private CheckBox chkWater;
        private CheckBox isNatural;
        private ComboBox airQuality;
        private NumericUpDown numberOfPlaces;
        private Button btnSearch;
        private ListBox listBoxResults;
        private TextBox textBox1;
    }
}
