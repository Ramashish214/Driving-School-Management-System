namespace driving_school_management_system
{
    partial class Dashboard
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.vehicleCountLabel = new System.Windows.Forms.Label();
            this.driverCountLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.studentsCountLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(378, 68);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(251, 24);
            this.comboBox1.TabIndex = 0;
            // 
            // vehicleCountLabel
            // 
            this.vehicleCountLabel.AutoSize = true;
            this.vehicleCountLabel.Location = new System.Drawing.Point(291, 209);
            this.vehicleCountLabel.Name = "vehicleCountLabel";
            this.vehicleCountLabel.Size = new System.Drawing.Size(44, 16);
            this.vehicleCountLabel.TabIndex = 2;
            this.vehicleCountLabel.Text = "label2";
            // 
            // driverCountLabel
            // 
            this.driverCountLabel.AutoSize = true;
            this.driverCountLabel.Location = new System.Drawing.Point(291, 162);
            this.driverCountLabel.Name = "driverCountLabel";
            this.driverCountLabel.Size = new System.Drawing.Size(44, 16);
            this.driverCountLabel.TabIndex = 3;
            this.driverCountLabel.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(188, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "drivers";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "vehicles";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(188, 254);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "students";
            // 
            // studentsCountLabel
            // 
            this.studentsCountLabel.AutoSize = true;
            this.studentsCountLabel.Location = new System.Drawing.Point(291, 254);
            this.studentsCountLabel.Name = "studentsCountLabel";
            this.studentsCountLabel.Size = new System.Drawing.Size(57, 16);
            this.studentsCountLabel.TabIndex = 7;
            this.studentsCountLabel.Text = "students";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 555);
            this.Controls.Add(this.studentsCountLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.driverCountLabel);
            this.Controls.Add(this.vehicleCountLabel);
            this.Controls.Add(this.comboBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label vehicleCountLabel;
        private System.Windows.Forms.Label driverCountLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label studentsCountLabel;
    }
}