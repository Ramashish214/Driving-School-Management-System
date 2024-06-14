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

        //conection to database
        //SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\BSc.Electrical Engineering\\SEM 04\\CodeDnM\\C#\\driving_school_management_system\\dbSystemDSMS.mdf\";Integrated Security=True");
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

        private byte[] GetPDFFileBytes(string id, string columnName)  //function to get PDF files
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

        private void ViewPDF(string id, string columnName)  //function for view pdf
        {
            byte[] fileBytes = GetPDFFileBytes(id, columnName);
            if (fileBytes != null)
            {
                // save the pdf file to a temporary location
                string tempFilePath = Path.GetTempFileName() + ".pdf";
                File.WriteAllBytes(tempFilePath, fileBytes);

                // check if the file was created successfully
                if (File.Exists(tempFilePath))
                {
                    // open the pdf file using the default viewer
                    try
                    {
                        System.Diagnostics.Process.Start(tempFilePath);
                    }
                    catch (Exception )  //ex deleted
                    {
                        //MessageBox.Show("Error opening PDF file: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to save PDF file to temporary location.");
                }
            }
            else
            {
                MessageBox.Show("No PDF file available for this ID.","L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void InsertPDF(string id, byte[] nationalId, byte[] birthCertificate, byte[] MedicalReport)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Learner (Id, [National ID], [Birth Certificate], [Medical Report]) VALUES (@Id, @FileBytes1, @FileBytes2,@FileBytes3)", connection);
                cmd.Parameters.AddWithValue("@Id", id);

                // check if fileBytes is null 
                if (nationalId == null)
                {
                    cmd.Parameters.Add("@FileBytes1", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@FileBytes1", SqlDbType.VarBinary, -1).Value = nationalId;
                }

                // check if fileBytes1 is null 
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
                MessageBox.Show("PDF file inserted successfully.", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting PDF file: " + ex.Message);
            }
            finally
            {
                connection.Close();
                BindData(); 
            }
        }

        private void Learners_Load(object sender, EventArgs e)
        {
            contentLoad();

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
                MessageBox.Show("Please enter a value for the Learner Id", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // exit the event handler
            }
            connection.Open();

            // check if the primary key already exists
            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Learner WHERE [Id] = @PrimaryKeyValue", connection);
            checkCmd.Parameters.AddWithValue("@PrimaryKeyValue", textBox1.Text);

            int existingCount = (int)checkCmd.ExecuteScalar();
            if (existingCount > 0)
            {
                MessageBox.Show("Learner's ID already exists.", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                return; // exit the event handler
            }

            // proceed with insertion if the primary key does not exist


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
                // handle SQL exceptions
                MessageBox.Show("An error occurred while inserting data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }           
        }

        private void dataGridViewLearners_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == dataGridViewLearners.Columns["National ID"].Index || e.ColumnIndex == dataGridViewLearners.Columns["pdfFile1"].Index || e.ColumnIndex == dataGridViewLearners.Columns["pdfFile2"].Index && e.RowIndex >= 0))
            {
                // get the ID from the clicked row
                string id = dataGridViewLearners.Rows[e.RowIndex].Cells["Id"].Value.ToString();

                // determine the column name based on the clicked cell
                string columnName = dataGridViewLearners.Columns[e.ColumnIndex].Name;

                // view the PDF document
                ViewPDF(id, columnName);
            }
        }

        private void updateIdPDFBtn_Click(object sender, EventArgs e)
        {
            // check if both ID and PDF file are provided
            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrEmpty(textBox3.Text))
            {
                // Read PDF file bytes
                byte[] fileBytes = File.ReadAllBytes(textBox3.Text);               
                UpdateRecord(textBox6.Text, fileBytes, "National ID");
            }
            else
            {
                MessageBox.Show("Please provide both ID and PDF file path.", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);               
            }
        }

        private void UpdateRecord(string id, byte[] fileBytes, string name)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE Learner SET [{name}] = @FileBytes WHERE Id = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", id);

                // check if fileBytes is null and add DBNull.Value if it is
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
                BindData(); // refresh dataGridView
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            // check if the driverId TextBox is not empty
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                // confirm with the user
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
                // if user clicks No, do nothing
            }
            else
            {
                MessageBox.Show("Please enter an ID to delete.", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            connection.Open();

            try
            {
                // check if the record exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Learner WHERE Id = @PrimaryKeyValue", connection);
                checkCmd.Parameters.AddWithValue("@PrimaryKeyValue", textBox1.Text);
                int existingCount = (int)checkCmd.ExecuteScalar();

                if (existingCount == 0)
                {
                    MessageBox.Show("Record does not exist. Please enter a valid ID.", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    connection.Close();
                    return; // exit the event handler
                }

                // proceed with the update
                SqlCommand updateCmd = new SqlCommand("UPDATE Learner SET Name = @Value3, Address = @Value4, [Contact No] = @Value5, [License Type] = @Value6, [Written Exam] = @Value7, [Attendance] = @Value8 WHERE Id = @PrimaryKeyValue", connection);
                updateCmd.Parameters.AddWithValue("@PrimaryKeyValue", textBox1.Text);
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
              
                textBox1.Text = selectedRow.Cells["Id"].Value.ToString();
                textBox2.Text = selectedRow.Cells["Name"].Value.ToString();
                comboBox1.Text = selectedRow.Cells["License Type"].Value.ToString();
                textBox7.Text = selectedRow.Cells["Address"].Value.ToString();
                textBox8.Text = selectedRow.Cells["Contact No"].Value.ToString();
                comboBox2.Text = selectedRow.Cells["Written Exam"].Value.ToString();

                object attendanceCellValue = selectedRow.Cells["Attendance"].Value;
                if (attendanceCellValue != null)
                {
                    if (int.TryParse(attendanceCellValue.ToString(), out int attendanceValue))
                    {
                        numericUpDown1.Value = attendanceValue;
                    }
                    else
                    {
                        // handle if the value cannot be parsed to an integer
                        numericUpDown1.Value=0;
                    }
                }
                else
                {
                    // handle if the cell value is null
                    MessageBox.Show("Attendance value is null.");
                }
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string searchText = textBox9.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                // loop through each row in the dataGridView
                foreach (DataGridViewRow row in dataGridViewLearners.Rows)
                {
                    // check if the "Id" column value matches the search text
                    if (row.Cells["Id"].Value != null && row.Cells["Id"].Value.ToString() == searchText)
                    {
                        // select the row and scroll it into view
                        row.Selected = true;
                        dataGridViewLearners.CurrentCell = row.Cells["Id"];
                        return; // exit the function once a match is found
                    }
                }

                MessageBox.Show("No matching record found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please enter a search value.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
      

        void contentLoad() // content load depend on control
        {
            label9.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            textBox6.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            Browse.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            updateIdPDFBtn.Visible = false;
            updateBCPDFBtn.Visible = false;
            updateMRPDFBtn.Visible = false;

            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label8.Visible = true;
            label10.Visible = true;
            label7.Visible = true;
            label11.Visible = true;
            addBtn.Visible = true;
            updateBtn.Visible = true;
            deleteBtn.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox7.Visible = true;
            textBox8.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            numericUpDown1.Visible = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            contentLoad();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label9.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            textBox6.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            Browse.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            updateIdPDFBtn.Visible = true;
            updateBCPDFBtn.Visible = true;
            updateMRPDFBtn.Visible = true;

            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label8.Visible = false;
            label10.Visible = false;
            label7.Visible = false;
            label11.Visible = false;
            addBtn.Visible = false;
            updateBtn.Visible = false;
            deleteBtn.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            numericUpDown1.Visible = false;
        }
    }
}
