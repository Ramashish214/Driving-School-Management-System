using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace driving_school_management_system
{
    public partial class Learners : Form
    {
        public Learners()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\BSc.Electrical Engineering\\SEM 04\\CodeDnM\\C#\\driving_school_management_system\\dbSystemDSMS.mdf\";Integrated Security=True");

        void BindData()
        {
            SqlCommand cmd = new SqlCommand("select * from Learner", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridViewLearners.DataSource = dt;

            dt.Columns.Remove("National ID");
            dt.Columns.Remove("Birth Certificate");
            dt.Columns.Remove("Medical Report");
        }

        private byte[] GetPDFFileBytes(string id, string columnName)
        {
            byte[] fileBytes = null;
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT [{columnName}] FROM Learner WHERE Id = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", id);
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    fileBytes = (byte[])result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving PDF file: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return fileBytes;
        }

        private void ViewPDF(string id, string columnName)
        {
            byte[] fileBytes = GetPDFFileBytes(id, columnName);
            if (fileBytes != null)
            {
                // Save the PDF file to a temporary location
                string tempFilePath = Path.GetTempFileName() + ".pdf";
                File.WriteAllBytes(tempFilePath, fileBytes);

                // Check if the file was created successfully
                if (File.Exists(tempFilePath))
                {
                    // Open the PDF file using the default viewer
                    try
                    {
                        System.Diagnostics.Process.Start(tempFilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error opening PDF file: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to save PDF file to temporary location.");
                }
            }
            else
            {
                MessageBox.Show("No PDF file available for this ID.");
            }
        }


        private void InsertPDF(string id, byte[] nationalId, byte[] birthCertificate, byte[] MedicalReport)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Learner (Id, [National ID], [Birth Certificate], [Medical Report]) VALUES (@Id, @FileBytes1, @FileBytes2,@FileBytes3)", connection);
                cmd.Parameters.AddWithValue("@Id", id);

                // Check if fileBytes is null and add DBNull.Value if it is
                if (nationalId == null)
                {
                    cmd.Parameters.Add("@FileBytes1", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@FileBytes1", SqlDbType.VarBinary, -1).Value = nationalId;
                }

                // Check if fileBytes1 is null and add DBNull.Value if it is
                if (birthCertificate == null)
                {
                    cmd.Parameters.Add("@FileBytes2", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@FileBytes2", SqlDbType.VarBinary, -1).Value = birthCertificate;
                }

                if (MedicalReport == null)
                {
                    cmd.Parameters.Add("@FileBytes3", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@FileBytes3", SqlDbType.VarBinary, -1).Value = MedicalReport;
                }

                cmd.ExecuteNonQuery();
                MessageBox.Show("PDF file inserted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting PDF file: " + ex.Message);
            }
            finally
            {
                connection.Close();
                BindData(); // Refresh DataGridView
            }
        }

        private void Learners_Load(object sender, EventArgs e)
        {
            BindData();
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "National ID";
            buttonColumn.Name = "National ID";
            buttonColumn.Text = "View";
            buttonColumn.UseColumnTextForButtonValue = true;
            dataGridViewLearners.Columns.Add(buttonColumn);

            DataGridViewButtonColumn buttonColumn1 = new DataGridViewButtonColumn();
            buttonColumn1.HeaderText = "Birth Certificate";
            buttonColumn1.Name = "pdfFile1";
            buttonColumn1.Text = "View";
            buttonColumn1.UseColumnTextForButtonValue = true;
            dataGridViewLearners.Columns.Add(buttonColumn1);

            DataGridViewButtonColumn buttonColumn2 = new DataGridViewButtonColumn();
            buttonColumn2.HeaderText = "Medical Report";
            buttonColumn2.Name = "pdfFile2";
            buttonColumn2.Text = "View";
            buttonColumn2.UseColumnTextForButtonValue = true;
            dataGridViewLearners.Columns.Add(buttonColumn2);

            /*DataGridViewButtonColumn buttonColumn3 = new DataGridViewButtonColumn();
            buttonColumn3.HeaderText = "Written Exam";
            buttonColumn3.Name = "pdfFile1";
            buttonColumn3.Text = "View";
            buttonColumn3.UseColumnTextForButtonValue = true;
            dataGridViewLearners.Columns.Add(buttonColumn3);*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nationalId.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = nationalId.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (birthCertificate.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = birthCertificate.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (medicalReport.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = medicalReport.FileName;
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            connection.Open();

            // Check if the primary key already exists
            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Learner WHERE [Id] = @PrimaryKeyValue", connection);
            checkCmd.Parameters.AddWithValue("@PrimaryKeyValue", textBox1.Text);

            int existingCount = (int)checkCmd.ExecuteScalar();
            if (existingCount > 0)
            {
                MessageBox.Show("Primary key already exists. Please enter a different value.");
                connection.Close();
                return; // Exit the event handler
            }

            // Proceed with insertion if the primary key does not exist


            try
            {
                SqlCommand insertCmd = new SqlCommand("INSERT INTO Learner (Id, Name, [License Type], Address,[Written Exam]) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5)", connection);
                insertCmd.Parameters.AddWithValue("@Value1", textBox1.Text);
                insertCmd.Parameters.AddWithValue("@Value2", textBox2.Text);
                insertCmd.Parameters.AddWithValue("@Value3", comboBox1.Text);
                insertCmd.Parameters.AddWithValue("@Value4", textBox7.Text);
                insertCmd.Parameters.AddWithValue("@Value5", comboBox2.Text);
                insertCmd.ExecuteNonQuery();
                MessageBox.Show("Inserted");
                BindData();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                MessageBox.Show("An error occurred while inserting data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            //BindData();

            // Check if both ID and PDF file are provided
            /*if (!string.IsNullOrEmpty(textBox1.Text)) // && !string.IsNullOrEmpty(textBox2.Text))
            {
                // Read PDF file bytes
                byte[] fileBytes = File.ReadAllBytes(textBox3.Text);
                byte[] fileBytes1 = File.ReadAllBytes(textBox4.Text);
                byte[] fileBytes2 = File.ReadAllBytes(textBox5.Text);
                // Insert PDF file bytes into database
                InsertPDF(textBox1.Text, fileBytes, fileBytes1, fileBytes2);
                //InsertPDF(textBox1.Text, fileBytes1);
            }
            else
            {
                //MessageBox.Show("Please provide both ID and PDF file path.");
                //byte[] fileBytes = File.ReadAllBytes(null);
                //byte[] fileBytes1 = File.ReadAllBytes(null);
                InsertPDF(textBox1.Text, null, null, null);
            }*/
        }

        private void dataGridViewLearners_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == dataGridViewLearners.Columns["National ID"].Index || e.ColumnIndex == dataGridViewLearners.Columns["pdfFile1"].Index || e.ColumnIndex == dataGridViewLearners.Columns["pdfFile2"].Index && e.RowIndex  >= 0))
            {
                // Get the ID from the clicked row
                string id = dataGridViewLearners.Rows[e.RowIndex].Cells["Id"].Value.ToString();

                // Determine the column name based on the clicked cell
                string columnName = dataGridViewLearners.Columns[e.ColumnIndex].Name;

                // View the PDF document
                ViewPDF(id, columnName);
            }
        }

        private void updateIdPDFBtn_Click(object sender, EventArgs e)
        {
            // Check if both ID and PDF file are provided
            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrEmpty(textBox3.Text))
            {
                // Read PDF file bytes
                byte[] fileBytes = File.ReadAllBytes(textBox3.Text);
                //byte[] fileBytes1 = File.ReadAllBytes(textBox4.Text);
                //byte[] fileBytes2 = File.ReadAllBytes(textBox5.Text);
                // Insert PDF file bytes into database
                //InsertPDF(textBox1.Text, fileBytes, null, null);
                //InsertPDF(textBox1.Text, fileBytes1);
                UpdateRecord(textBox6.Text, fileBytes, "National ID");
            }
            else
            {
                MessageBox.Show("Please provide both ID and PDF file path.");
                //byte[] fileBytes = File.ReadAllBytes(null);
                //byte[] fileBytes1 = File.ReadAllBytes(null);
                //InsertPDF(textBox1.Text, null, null, null);
            }
        }

        private void UpdateRecord(string id, byte[] fileBytes,string name)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE Learner SET [{name}] = @FileBytes WHERE Id = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", id);

                // Check if fileBytes is null and add DBNull.Value if it is
                if (fileBytes == null)
                {
                    cmd.Parameters.Add("@FileBytes", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@FileBytes", SqlDbType.VarBinary, -1).Value = fileBytes;
                }

               

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record: " + ex.Message);
            }
            finally
            {
                connection.Close();
                BindData(); // Refresh DataGridView
            }
        }
    }
}
