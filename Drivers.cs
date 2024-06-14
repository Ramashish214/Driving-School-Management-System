using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace driving_school_management_system
{
    public partial class Drivers : Form
    {
        public Drivers()
        {
            InitializeComponent();
        }

        private void Drivers_Load(object sender, EventArgs e)
        {
            BindData();
            LoadVehicleNos();
        }

        // connection to database      
        //SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\BSc.Electrical Engineering\\SEM 04\\CodeDnM\\C#\\driving_school_management_system\\dbSystemDSMS.mdf\";Integrated Security=True");
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\dbSystemDSMS.mdf;Integrated Security=True");
        //load exisiting data from database to dataGridView
        void BindData()
        {
            SqlCommand cmd = new SqlCommand("select * from Driver", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridViewDriver.DataSource = dt;
        }

        private void LoadVehicleNos()  //load veheicle numbers to comboBox
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT [Vehicle No] FROM Vehicle", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["Vehicle No"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading driver IDs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }
      
        
        private void AddBtn_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(driverId.Text))
            {
                MessageBox.Show("Please enter the driver ID.", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // exit the event handler
            }

            connection.Open();

            // check if the primary key already exists
            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Driver WHERE Id = @PrimaryKeyValue", connection);
            checkCmd.Parameters.AddWithValue("@PrimaryKeyValue", driverId.Text);

            int existingCount = (int)checkCmd.ExecuteScalar();
            if (existingCount > 0)
            {
                MessageBox.Show("Driver ID already exists.", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                return; // exit the event handler
            }

            // proceed with insertion if the primary key does not exist
            

            try
            {
                SqlCommand insertCmd = new SqlCommand("INSERT INTO Driver VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7)", connection);
                insertCmd.Parameters.AddWithValue("@Value1", driverId.Text);
                insertCmd.Parameters.AddWithValue("@Value2", driverName.Text);
                insertCmd.Parameters.AddWithValue("@Value3", int.Parse(licenseNo.Text));
                insertCmd.Parameters.AddWithValue("@Value4", licenseType.Text);
                insertCmd.Parameters.AddWithValue("@Value5", bloodType.Text);
                insertCmd.Parameters.AddWithValue("@Value6", comboBox1.Text);
                insertCmd.Parameters.AddWithValue("@Value7", textBox1.Text);
                insertCmd.ExecuteNonQuery();
                MessageBox.Show("Inserted");
                BindData();
            }
            catch (Exception)
            {
                //for sql exceptions
                MessageBox.Show("An error occurred while inserting data");
            }
            finally
            {
                connection.Close();
            }

            BindData();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            // check if the driverId TextBox is not empty
            if (!string.IsNullOrEmpty(driverId.Text))
            {
                // confirm with the delete
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Driver WHERE Id = @ID", connection);
                        cmd.Parameters.AddWithValue("@ID", driverId.Text);
                        cmd.ExecuteNonQuery();
                        //connection.Close();
                        MessageBox.Show("Deleted", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BindData();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("An error occurred while deleting record");
                    }
                    finally {
                        connection.Close();
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Please enter driver ID to delete.", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridViewDriver_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewDriver.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewDriver.SelectedRows[0];
                
                driverId.Text = selectedRow.Cells["Id"].Value.ToString();
                driverName.Text = selectedRow.Cells["Driver Name"].Value.ToString();
                licenseNo.Text = selectedRow.Cells["License No"].Value.ToString();
                licenseType.Text = selectedRow.Cells["License Type"].Value.ToString();
                bloodType.Text = selectedRow.Cells["Blood Type"].Value.ToString();
                comboBox1.Text = selectedRow.Cells["Vehicle Incharge"].Value.ToString();
                textBox1.Text = selectedRow.Cells["Contact No"].Value.ToString();
                
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            

            try
            {
                connection.Open();
                // check if the record exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Driver WHERE Id = @PrimaryKeyValue", connection);
                checkCmd.Parameters.AddWithValue("@PrimaryKeyValue", driverId.Text);
                int existingCount = (int)checkCmd.ExecuteScalar();

                if (existingCount == 0)
                {
                    MessageBox.Show("Record does not exist. Please enter a valid ID.", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    connection.Close();
                    return; // exit the event handler
                }

                // proceed with the update
                SqlCommand updateCmd = new SqlCommand("UPDATE Driver SET [Driver Name] = @Value2, [License No] = @Value3, [License Type] = @Value4, [Blood Type] = @Value5, [Vehicle Incharge] = @Value6, [Contact No] = @Value7 WHERE Id = @PrimaryKeyValue", connection);
                updateCmd.Parameters.AddWithValue("@PrimaryKeyValue", driverId.Text);
                updateCmd.Parameters.AddWithValue("@Value2", driverName.Text);
                updateCmd.Parameters.AddWithValue("@Value3", int.Parse(licenseNo.Text));
                updateCmd.Parameters.AddWithValue("@Value4", licenseType.Text);
                updateCmd.Parameters.AddWithValue("@Value5", bloodType.Text);
                updateCmd.Parameters.AddWithValue("@Value6", comboBox1.Text);
                updateCmd.Parameters.AddWithValue("@Value7", textBox1.Text);
                updateCmd.ExecuteNonQuery();

                MessageBox.Show("Updated", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindData();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while updating data.");
            }
            finally
            {
                connection.Close();
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            // check if the text box has a value
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                // show a message to notify the user
                MessageBox.Show("Please enter the driver ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // exit the event handler
            }

            // proceed with the search operation
            string searchText = textBox2.Text.Trim();
            dataGridViewDriver.ClearSelection();

            // iterate through each row in the DataGridView
            var status = false;
            if (status == false)
            {
                foreach (DataGridViewRow row in dataGridViewDriver.Rows)
                {

                    string cellValue = row.Cells[0].Value.ToString();


                    if (cellValue.Equals(searchText, StringComparison.OrdinalIgnoreCase))
                    {
                        row.Selected = true;
                        dataGridViewDriver.FirstDisplayedScrollingRowIndex = row.Index; // go to the selected row
                        status = true;
                        break;
                    }

                }
            }
            if(status == false)
            {
                MessageBox.Show("Not Found", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }       
    }
}
