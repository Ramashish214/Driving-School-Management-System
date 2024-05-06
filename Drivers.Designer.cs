namespace driving_school_management_system
{
    partial class Drivers
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
            this.dataGridViewDriver = new System.Windows.Forms.DataGridView();
            this.AddBtn = new System.Windows.Forms.Button();
            this.driverId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.driverName = new System.Windows.Forms.TextBox();
            this.licenseNo = new System.Windows.Forms.TextBox();
            this.vehicleInCharge = new System.Windows.Forms.TextBox();
            this.licenseType = new System.Windows.Forms.ComboBox();
            this.bloodType = new System.Windows.Forms.ComboBox();
            this.deleteBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDriver)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewDriver
            // 
            this.dataGridViewDriver.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDriver.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewDriver.Name = "dataGridViewDriver";
            this.dataGridViewDriver.RowHeadersWidth = 51;
            this.dataGridViewDriver.Size = new System.Drawing.Size(776, 259);
            this.dataGridViewDriver.TabIndex = 0;
            this.dataGridViewDriver.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // AddBtn
            // 
            this.AddBtn.Location = new System.Drawing.Point(325, 382);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(75, 23);
            this.AddBtn.TabIndex = 1;
            this.AddBtn.Text = "Add";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // driverId
            // 
            this.driverId.Location = new System.Drawing.Point(157, 297);
            this.driverId.Name = "driverId";
            this.driverId.Size = new System.Drawing.Size(121, 20);
            this.driverId.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 300);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 332);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 362);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "License No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 392);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "License Type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 419);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Blood Type";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(55, 445);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Vehicle InCharge";
            // 
            // driverName
            // 
            this.driverName.Location = new System.Drawing.Point(157, 329);
            this.driverName.Name = "driverName";
            this.driverName.Size = new System.Drawing.Size(121, 20);
            this.driverName.TabIndex = 9;
            // 
            // licenseNo
            // 
            this.licenseNo.Location = new System.Drawing.Point(157, 359);
            this.licenseNo.Name = "licenseNo";
            this.licenseNo.Size = new System.Drawing.Size(121, 20);
            this.licenseNo.TabIndex = 10;
            // 
            // vehicleInCharge
            // 
            this.vehicleInCharge.Location = new System.Drawing.Point(157, 445);
            this.vehicleInCharge.Name = "vehicleInCharge";
            this.vehicleInCharge.Size = new System.Drawing.Size(121, 20);
            this.vehicleInCharge.TabIndex = 11;
            // 
            // licenseType
            // 
            this.licenseType.FormattingEnabled = true;
            this.licenseType.Items.AddRange(new object[] {
            "Light Vehicles",
            "Heavy Vehicles"});
            this.licenseType.Location = new System.Drawing.Point(157, 385);
            this.licenseType.Name = "licenseType";
            this.licenseType.Size = new System.Drawing.Size(121, 21);
            this.licenseType.TabIndex = 12;
            // 
            // bloodType
            // 
            this.bloodType.FormattingEnabled = true;
            this.bloodType.Items.AddRange(new object[] {
            "A",
            "A+",
            "A-",
            "O+",
            "O-",
            "B",
            "B+"});
            this.bloodType.Location = new System.Drawing.Point(157, 411);
            this.bloodType.Name = "bloodType";
            this.bloodType.Size = new System.Drawing.Size(121, 21);
            this.bloodType.TabIndex = 13;
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(437, 382);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(75, 23);
            this.deleteBtn.TabIndex = 14;
            this.deleteBtn.Text = "Delete";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // Drivers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 475);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.bloodType);
            this.Controls.Add(this.licenseType);
            this.Controls.Add(this.vehicleInCharge);
            this.Controls.Add(this.licenseNo);
            this.Controls.Add(this.driverName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.driverId);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.dataGridViewDriver);
            this.Name = "Drivers";
            this.Text = "Drivers";
            this.Load += new System.EventHandler(this.Drivers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDriver)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewDriver;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.TextBox driverId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox driverName;
        private System.Windows.Forms.TextBox licenseNo;
        private System.Windows.Forms.TextBox vehicleInCharge;
        private System.Windows.Forms.ComboBox licenseType;
        private System.Windows.Forms.ComboBox bloodType;
        private System.Windows.Forms.Button deleteBtn;
    }
}