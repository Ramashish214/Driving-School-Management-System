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

        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\dbSystemDSMS.mdf;Integrated Security=True");

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
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a value for the Learner Id");
                return; // Exit the event handler
            }
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
                SqlCommand insertCmd = new SqlCommand("INSERT INTO Learner (Id, Name, Address,[Contact No],[License Type],[Written Exam],[Attendance]) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7)", connection);
                insertCmd.Parameters.AddWithValue("@Value1", textBox1.Text);
                insertCmd.Parameters.AddWithValue("@Value2", textBox2.Text);
                insertCmd.Parameters.AddWithValue("@Value5", comboBox1.Text);
                insertCmd.Parameters.AddWithValue("@Value3", textBox7.Text);
                insertCmd.Parameters.AddWithValue("@Value4", textBox8.Text);
                insertCmd.Parameters.AddWithValue("@Value6", comboBox2.Text);
                insertCmd.Parameters.AddWithValue("@Value7", numericUpDown1.Value);
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
            if ((e.ColumnIndex == dataGridViewLearners.Columns["National ID"].Index || e.ColumnIndex == dataGridViewLearners.Columns["pdfFile1"].Index || e.ColumnIndex == dataGridViewLearners.Columns["pdfFile2"].Index && e.RowIndex >= 0))
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

        private void UpdateRecord(string id, byte[] fileBytes, string name)
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

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            // Check if the driverId TextBox is not empty
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                // Confirm with the user
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Learner WHERE [Id] = @ID", connection);
                        cmd.Parameters.AddWithValue("@ID", textBox1.Text);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Deleted");
                        BindData();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error deleting record: " + ex.Message);
                    }
                }
                // If user clicks No, do nothing
            }
            else
            {
                MessageBox.Show("Please enter an ID to delete.");
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            connection.Open();

            try
            {
                // Check if the record exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Learner WHERE Id = @PrimaryKeyValue", connection);
                checkCmd.Parameters.AddWithValue("@PrimaryKeyValue", textBox1.Text);
                int existingCount = (int)checkCmd.ExecuteScalar();

                if (existingCount == 0)
                {
                    MessageBox.Show("Record does not exist. Please enter a valid ID.");
                    connection.Close();
                    return; // Exit the event handler
                }

                // Proceed with the update
                SqlCommand updateCmd = new SqlCommand("UPDATE Learner SET Name = @Value3, Address = @Value4, [Contact No] = @Value5, [License Type] = @Value6, [Written Exam] = @Value7, [Attendance] = @Value8 WHERE Id = @PrimaryKeyValue", connection);
                updateCmd.Parameters.AddWithValue("@PrimaryKeyValue", textBox1.Text);
                //updateCmd.Parameters.AddWithValue("@Value2", comboBox1.Text);
                updateCmd.Parameters.AddWithValue("@Value3", textBox2.Text);
                updateCmd.Parameters.AddWithValue("@Value4", textBox7.Text);
                updateCmd.Parameters.AddWithValue("@Value5", textBox8.Text);
                updateCmd.Parameters.AddWithValue("@Value6", comboBox1.Text);
                updateCmd.Parameters.AddWithValue("@Value7", comboBox2.Text);
                updateCmd.Parameters.AddWithValue("@Value8", numericUpDown1.Value);
                updateCmd.ExecuteNonQuery();
                MessageBox.Show("Updated");
                BindData();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred while updating data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void dataGridViewLearners_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewLearners.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewLearners.SelectedRows[0];

                // Assuming txtBox1, txtBox2, txtBox3 are your TextBox controls
                
                textBox1.Text = selectedRow.Cells["Id"].Value.ToString();
                textBox2.Text = selectedRow.Cells["Name"].Value.ToString();
                comboBox1.Text = selectedRow.Cells["License Type"].Value.ToString();
                textBox7.Text = selectedRow.Cells["Address"].Value.ToString();
                textBox8.Text = selectedRow.Cells["Contact No"].Value.ToString();
                comboBox2.Text = selectedRow.Cells["Written Exam"].Value.ToString();
                //numericUpDown1.Value = int.Parse(selectedRow.Cells["Attendance"].Value.ToString());
                object attendanceCellValue = selectedRow.Cells["Attendance"].Value;
                if (attendanceCellValue != null)
                {
                    if (int.TryParse(attendanceCellValue.ToString(), out int attendanceValue))
                    {
                        numericUpDown1.Value = attendanceValue;
                    }
                    else
                    {
                        // Handle if the value cannot be parsed to an integer
                        numericUpDown1.Value=0;
                    }
                }
                else
                {
                    // Handle if the cell value is null
                    MessageBox.Show("Attendance value is null.");
                }
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string searchText = textBox9.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                // Loop through each row in the DataGridView
                foreach (DataGridViewRow row in dataGridViewLearners.Rows)
                {
                    // Check if the "Id" column value matches the search text
                    if (row.Cells["Id"].Value != null && row.Cells["Id"].Value.ToString() == searchText)
                    {
                        // Select the row and scroll it into view
                        row.Selected = true;
                        dataGridViewLearners.CurrentCell = row.Cells["Id"];
                        return; // Exit the function once a match is found
                    }
                }

                MessageBox.Show("No matching record found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please enter a search value.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
