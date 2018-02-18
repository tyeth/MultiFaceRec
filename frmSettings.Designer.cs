namespace MultiFaceRec
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnResetPrivacyList = new System.Windows.Forms.Button();
            this.btnDeletePrivacyItem = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMongoUrl = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSettings = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTrusted = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtScanned = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtVillains = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(390, 177);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 39);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.Button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(471, 177);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 39);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "outlook",
            "microsoftedgecp",
            "microsoftedge",
            "edge",
            "firefox",
            "chrome",
            "winword",
            "msteams",
            "teams",
            "skype",
            "hangouts",
            "thunderbirdeudora",
            "mail",
            "hxoutlook"});
            this.comboBox1.Location = new System.Drawing.Point(0, 30);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(148, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox1_KeyPress);
            this.comboBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboBox1_KeyUp);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(1, 53);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(194, 251);
            this.listBox1.TabIndex = 3;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(146, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 22);
            this.button1.TabIndex = 4;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnResetPrivacyList
            // 
            this.btnResetPrivacyList.Location = new System.Drawing.Point(1, 303);
            this.btnResetPrivacyList.Name = "btnResetPrivacyList";
            this.btnResetPrivacyList.Size = new System.Drawing.Size(128, 19);
            this.btnResetPrivacyList.TabIndex = 5;
            this.btnResetPrivacyList.Text = "RESET TO DEFAULTS";
            this.btnResetPrivacyList.UseVisualStyleBackColor = true;
            this.btnResetPrivacyList.Click += new System.EventHandler(this.btnResetPrivacyList_Click);
            // 
            // btnDeletePrivacyItem
            // 
            this.btnDeletePrivacyItem.Enabled = false;
            this.btnDeletePrivacyItem.Location = new System.Drawing.Point(135, 303);
            this.btnDeletePrivacyItem.Name = "btnDeletePrivacyItem";
            this.btnDeletePrivacyItem.Size = new System.Drawing.Size(60, 19);
            this.btnDeletePrivacyItem.TabIndex = 6;
            this.btnDeletePrivacyItem.Text = "DELETE";
            this.btnDeletePrivacyItem.UseVisualStyleBackColor = true;
            this.btnDeletePrivacyItem.Click += new System.EventHandler(this.btnDeletePrivacyItem_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(40, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 26);
            this.label1.TabIndex = 7;
            this.label1.Text = "Privacy List : Apps that should cause a Minimize All Event";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 24);
            this.label2.TabIndex = 8;
            this.label2.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(213, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 24);
            this.label3.TabIndex = 9;
            this.label3.Text = "2";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(239, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Facial Recognition Database Settings:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(203, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 333);
            this.label5.TabIndex = 11;
            // 
            // txtMongoUrl
            // 
            this.txtMongoUrl.Location = new System.Drawing.Point(214, 53);
            this.txtMongoUrl.Name = "txtMongoUrl";
            this.txtMongoUrl.Size = new System.Drawing.Size(332, 20);
            this.txtMongoUrl.TabIndex = 12;
            this.txtMongoUrl.Text = "local";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(298, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(198, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "MongoDb Url / ConnectionString ";
            this.label6.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(214, 97);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(156, 20);
            this.txtDatabaseName.TabIndex = 14;
            this.txtDatabaseName.Text = "faces";
            this.txtDatabaseName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(224, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "MongoDb Database Name";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // txtSettings
            // 
            this.txtSettings.Location = new System.Drawing.Point(390, 97);
            this.txtSettings.Name = "txtSettings";
            this.txtSettings.Size = new System.Drawing.Size(156, 20);
            this.txtSettings.TabIndex = 16;
            this.txtSettings.Text = "settings";
            this.txtSettings.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(390, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(156, 16);
            this.label8.TabIndex = 15;
            this.label8.Text = "MongoDb Settings Collection";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // txtTrusted
            // 
            this.txtTrusted.Location = new System.Drawing.Point(214, 147);
            this.txtTrusted.Name = "txtTrusted";
            this.txtTrusted.Size = new System.Drawing.Size(156, 20);
            this.txtTrusted.TabIndex = 18;
            this.txtTrusted.Text = "trustedGrey";
            this.txtTrusted.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(214, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(156, 16);
            this.label9.TabIndex = 17;
            this.label9.Text = "Trusted Faces Collection";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // txtScanned
            // 
            this.txtScanned.Location = new System.Drawing.Point(390, 147);
            this.txtScanned.Name = "txtScanned";
            this.txtScanned.Size = new System.Drawing.Size(156, 20);
            this.txtScanned.TabIndex = 20;
            this.txtScanned.Text = "scanned";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(390, 128);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(156, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "Detected Faces Collection";
            // 
            // txtVillains
            // 
            this.txtVillains.Location = new System.Drawing.Point(214, 196);
            this.txtVillains.Name = "txtVillains";
            this.txtVillains.Size = new System.Drawing.Size(156, 20);
            this.txtVillains.TabIndex = 22;
            this.txtVillains.Text = "villains";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(214, 177);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(156, 16);
            this.label11.TabIndex = 21;
            this.label11.Text = "Prying Eyes Collection";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.HorizontalScrollbar = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Use MongoDb instead of %APP_PATH%\\TrainedFaces.txt",
            "Enable Training Mode for 5secs when I Logon or Unlock the pc.",
            "Lock the computer if I leave for more than 20 seconds",
            "Enable minimize all functionality",
            "Use Balloon Tips (Windows Notifications)"});
            this.checkedListBox1.Location = new System.Drawing.Point(214, 223);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(332, 94);
            this.checkedListBox1.TabIndex = 23;
            // 
            // frmSettings
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(551, 322);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.txtVillains);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtScanned);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtTrusted);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtSettings);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtDatabaseName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMongoUrl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDeletePrivacyItem);
            this.Controls.Add(this.btnResetPrivacyList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(567, 361);
            this.MinimumSize = new System.Drawing.Size(567, 361);
            this.Name = "frmSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSettings_FormClosing);
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.Shown += new System.EventHandler(this.frmSettings_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnResetPrivacyList;
        private System.Windows.Forms.Button btnDeletePrivacyItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMongoUrl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSettings;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTrusted;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtScanned;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtVillains;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
    }
}