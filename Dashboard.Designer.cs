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
            this.vehicleCountLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.studentsCountLabel = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.driverCountLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // vehicleCountLabel
            // 
            this.vehicleCountLabel.AutoSize = true;
            this.vehicleCountLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vehicleCountLabel.Location = new System.Drawing.Point(278, 320);
            this.vehicleCountLabel.Name = "vehicleCountLabel";
            this.vehicleCountLabel.Size = new System.Drawing.Size(230, 46);
            this.vehicleCountLabel.TabIndex = 2;
            this.vehicleCountLabel.Text = "Vehicles num";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(109, 320);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 46);
            this.label2.TabIndex = 5;
            this.label2.Text = "Vehicles";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(109, 442);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 46);
            this.label3.TabIndex = 6;
            this.label3.Text = "Students";
            // 
            // studentsCountLabel
            // 
            this.studentsCountLabel.AutoSize = true;
            this.studentsCountLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.studentsCountLabel.Location = new System.Drawing.Point(278, 442);
            this.studentsCountLabel.Name = "studentsCountLabel";
            this.studentsCountLabel.Size = new System.Drawing.Size(239, 46);
            this.studentsCountLabel.TabIndex = 7;
            this.studentsCountLabel.Text = "students num";
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(774, 239);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(345, 262);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // driverCountLabel
            // 
            this.driverCountLabel.AutoSize = true;
            this.driverCountLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.driverCountLabel.Location = new System.Drawing.Point(278, 222);
            this.driverCountLabel.Name = "driverCountLabel";
            this.driverCountLabel.Size = new System.Drawing.Size(216, 46);
            this.driverCountLabel.TabIndex = 3;
            this.driverCountLabel.Text = "Drivers num";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(109, 222);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 46);
            this.label1.TabIndex = 4;
            this.label1.Text = "Drivers";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(364, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(453, 95);
            this.label4.TabIndex = 0;
            this.label4.Text = "Dashboard";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(34)))), ((int)(((byte)(11)))));
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1191, 123);
            this.panel1.TabIndex = 9;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1191, 690);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.driverCountLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.studentsCountLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.vehicleCountLabel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label vehicleCountLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label studentsCountLabel;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label driverCountLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
    }
}