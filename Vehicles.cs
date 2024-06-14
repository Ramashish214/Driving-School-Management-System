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

namespace driving_school_management_system
{
    public partial class Vehicles : Form
    {
        public Vehicles()
        {
            InitializeComponent();
        }

        //connection string
        //SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\BSc.Electrical Engineering\\SEM 04\\CodeDnM\\C#\\driving_school_management_system\\dbSystemDSMS.mdf\";Integrated Security=True");
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\dbSystemDSMS.mdf;Integrated Security=True");
        void BindData()
        {
            SqlCommand cmd = new SqlCommand("select * from Vehicle", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridViewVehicles.DataSource = dt;          
        }

        private void LoadDriverIDs()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM Driver", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader["Id"].ToString());
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

        private void FindClosestDate()
        {
            int dateColumnIndex = dataGridViewVehicles.Columns.Cast<DataGridViewColumn>()
                                    .FirstOrDefault(column => column.HeaderText == "License Renewval Date") 
                                    ?.Index ?? -1;

            if (dateColumnIndex != -1)
            {
                // extract all dates from the dataGridView
                var dates = dataGridViewVehicles.Rows.Cast<DataGridViewRow>()
                                .Where(row => row.Cells[dateColumnIndex].Value != null && row.Cells[dateColumnIndex].Value != DBNull.Value)
                                .Select(row => Convert.ToDateTime(row.Cells[dateColumnIndex].Value));

                // find the closest date to the current date
                DateTime closestDate = dates.OrderBy(date => Math.Abs((date - DateTime.Now).TotalDays)).First();

                // display the closest date in the label
                label7.Text = closestDate.ToString("yyyy-MM-dd"); // adjust the format 
            }
            else
            {
                
                label7.Text = "No Date Column Found";
            }
        }

        private void Vehicles_Load(object sender, EventArgs e)
        {
            BindData();
            LoadDriverIDs();
            FindClosestDate();
            UpdateClosestServiceVehicle();
            UpdateClosestRenewvalVehicle();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(vehicleNo.Text))
            {
                MessageBox.Show("Please enter a value for the Vehicle No");
                return; // exit the event handler
            }

            connection.Open();

            // check if the primary key already exists
            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Vehicle WHERE [Vehicle No] = @PrimaryKeyValue", connection);
            checkCmd.Parameters.AddWithValue("@PrimaryKeyValue", vehicleNo.Text);

            int existingCount = (int)checkCmd.ExecuteScalar();
            if (existingCount > 0)
            {
                MessageBox.Show("Vehicle No already exists.", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                return; // exit the event handler
            }

            // proceed with insertion if the primary key does not exist


            try
            {
                SqlCommand insertCmd = new SqlCommand("INSERT INTO Vehicle VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6)", connection);
                insertCmd.Parameters.AddWithValue("@Value1", vehicleNo.Text);
                insertCmd.Parameters.AddWithValue("@Value2", comboBox1.Text);
                insertCmd.Parameters.AddWithValue("@Value3", dateTimePicker1.Value);
                insertCmd.Parameters.AddWithValue("@Value4", dateTimePicker2.Value);
                insertCmd.Parameters.AddWithValue("@Value5", comboBox2.Text);
                insertCmd.Parameters.AddWithValue("@Value6", dateTimePicker3.Value);
                insertCmd.ExecuteNonQuery();
                MessageBox.Show("Inserted");
                BindData();
            }
            catch (Exception ex)
            {
                // handle all exceptions
                MessageBox.Show("An error occurred while inserting data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            BindData();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            // check if the text box has a value
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                // show a message or perform any action to notify the user
                MessageBox.Show("Please enter a search term.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Exit the event handler
            }

            // proceed with the search operation
            string searchText = textBox1.Text.Trim();
            dataGridViewVehicles.ClearSelection();
            // iterate through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridViewVehicles.Rows)
            {
                
                string cellValue = row.Cells[0].Value.ToString();

                // if the cell value matches the search text, select the row and exit the loop
                if (cellValue.Equals(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    row.Selected = true;
                    dataGridViewVehicles.FirstDisplayedScrollingRowIndex = row.Index; // scroll to the selected row
                    break;
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            // check if the driverId TextBox is not empty
            if (!string.IsNullOrEmpty(vehicleNo.Text))
            {
                // confirm with the user
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    connection.Open();
                    try
                    {
                        
                        SqlCommand cmd = new SqlCommand("DELETE FROM Vehicle WHERE [Vehicle No] = @ID", connection);
                        cmd.Parameters.AddWithValue("@ID", vehicleNo.Text);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Deleted", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BindData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting record: " + ex.Message);
                    }
                    finally 
                    {
                        connection.Close();
                    }
                }
                // if user clicks No, do nothing
            }
            else
            {
                MessageBox.Show("Please enter an ID to delete.");
            }
        }

        private void dataGridViewVehicles_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewVehicles.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewVehicles.SelectedRows[0];

                
                vehicleNo.Text = selectedRow.Cells["Vehicle No"].Value.ToString();
                comboBox1.Text = selectedRow.Cells["Vehicle Type"].Value.ToString();
                dateTimePicker1.Text = selectedRow.Cells["Last Service Date"].Value.ToString();
                dateTimePicker2.Text = selectedRow.Cells["Next Service Date"].Value.ToString();
                comboBox2.Text = selectedRow.Cells["Driver Incharge"].Value.ToString();
                dateTimePicker3.Text = selectedRow.Cells["License Renewval Date"].Value.ToString();
                
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            connection.Open();

            try
            {
                // check if the record exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Vehicle WHERE [Vehicle No] = @PrimaryKeyValue", connection);
                checkCmd.Parameters.AddWithValue("@PrimaryKeyValue", vehicleNo.Text);
                int existingCount = (int)checkCmd.ExecuteScalar();

                if (existingCount == 0)
                {
                    MessageBox.Show("Record does not exist. Please enter a valid ID.");
                    connection.Close();
                    return; // exit the event handler
                }

                // proceed with the update
                SqlCommand updateCmd = new SqlCommand("UPDATE Vehicle SET  [Vehicle Type] = @Value2, [Last Service Date] = @Value3, [Next Service Date] = @Value4, [Driver Incharge] = @Value5, [License Renewval Date] = @Value6 WHERE [Vehicle No] = @PrimaryKeyValue", connection);
                updateCmd.Parameters.AddWithValue("@PrimaryKeyValue", vehicleNo.Text);
                updateCmd.Parameters.AddWithValue("@Value2", comboBox1.Text);
                updateCmd.Parameters.AddWithValue("@Value3", dateTimePicker1.Value);
                updateCmd.Parameters.AddWithValue("@Value4", dateTimePicker2.Value);
                updateCmd.Parameters.AddWithValue("@Value5", comboBox2.Text);
                updateCmd.Parameters.AddWithValue("@Value6", dateTimePicker3.Value);
                //updateCmd.Parameters.AddWithValue("@Value7", textBox1.Text);
                updateCmd.ExecuteNonQuery();

                MessageBox.Show("Updated", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void dataGridViewVehicles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UpdateClosestServiceVehicle()
        {
            try
            {
                connection.Open();

                // query to get the vehicle with the closest service date
                string query = @"
                    SELECT TOP 1 [Vehicle No], [Next Service Date]
                    FROM Vehicle
                    WHERE [Next Service Date] IS NOT NULL
                      AND [Next Service Date] >= GETDATE()
                    ORDER BY [Next Service Date] ASC;";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string vehicleNo = reader["Vehicle No"].ToString();
                    DateTime nextServiceDate = (DateTime)reader["Next Service Date"];

                    // display the vehicle ID on the dashboard
                    label11.Text = $"{vehicleNo}";
                }
                else
                {
                    // no upcoming service dates found
                    label11.Text = "Not Found";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching the closest service vehicle: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void UpdateClosestRenewvalVehicle()
        {
            try
            {
                connection.Open();

                // query to get the vehicle with the closest service date
                string query = @"
                    SELECT TOP 1 [Vehicle No], [License Renewval Date]
                    FROM Vehicle
                    WHERE [License Renewval Date] IS NOT NULL
                      AND [License Renewval Date] >= GETDATE()
                    ORDER BY [License Renewval Date] ASC;";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string vehicleNo = reader["Vehicle No"].ToString();
                    //DateTime nextServiceDate = (DateTime)reader["Next License Renewval Date"];

                    // display the vehicle ID on the dashboard
                    label7.Text = $"{vehicleNo}";
                }
                else
                {
                    // no upcoming service dates found
                    label7.Text = "Not Found";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching the closest renewval vehicle: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }   
}
