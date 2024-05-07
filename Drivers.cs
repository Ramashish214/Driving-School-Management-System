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

        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\BSc.Electrical Engineering\\SEM 04\\CodeDnM\\C#\\driving_school_management_system\\dbSystemDSMS.mdf\";Integrated Security=True");

        void BindData()
        {
            SqlCommand cmd = new SqlCommand("select * from Driver", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridViewDriver.DataSource = dt;
        }

        private void LoadVehicleNos()
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Drivers_Load(object sender, EventArgs e)
        {
            BindData();
            LoadVehicleNos();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            connection.Open();

            // Check if the primary key already exists
            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Driver WHERE Id = @PrimaryKeyValue", connection);
            checkCmd.Parameters.AddWithValue("@PrimaryKeyValue", driverId.Text);

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
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                MessageBox.Show("An error occurred while inserting data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            BindData();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            // Check if the driverId TextBox is not empty
            if (!string.IsNullOrEmpty(driverId.Text))
            {
                // Confirm with the user
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Driver WHERE Id = @ID", connection);
                        cmd.Parameters.AddWithValue("@ID", driverId.Text);
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

        private void dataGridViewDriver_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewDriver.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewDriver.SelectedRows[0];

                // Assuming txtBox1, txtBox2, txtBox3 are your TextBox controls
                driverId.Text = selectedRow.Cells["Id"].Value.ToString();
                driverName.Text = selectedRow.Cells["Driver Name"].Value.ToString();
                licenseNo.Text = selectedRow.Cells["License No"].Value.ToString();
                licenseType.Text = selectedRow.Cells["License Type"].Value.ToString();
                bloodType.Text = selectedRow.Cells["Blood Type"].Value.ToString();
                comboBox1.Text = selectedRow.Cells["Vehicle Incharge"].Value.ToString();
                textBox1.Text = selectedRow.Cells["Contact No"].Value.ToString();
                // Add more lines if you have more text boxes and columns
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            connection.Open();

            try
            {
                // Check if the record exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Driver WHERE Id = @PrimaryKeyValue", connection);
                checkCmd.Parameters.AddWithValue("@PrimaryKeyValue", driverId.Text);
                int existingCount = (int)checkCmd.ExecuteScalar();

                if (existingCount == 0)
                {
                    MessageBox.Show("Record does not exist. Please enter a valid ID.");
                    connection.Close();
                    return; // Exit the event handler
                }

                // Proceed with the update
                SqlCommand updateCmd = new SqlCommand("UPDATE Driver SET [Driver Name] = @Value2, [License No] = @Value3, [License Type] = @Value4, [Blood Type] = @Value5, [Vehicle Incharge] = @Value6, [Contact No] = @Value7 WHERE Id = @PrimaryKeyValue", connection);
                updateCmd.Parameters.AddWithValue("@PrimaryKeyValue", driverId.Text);
                updateCmd.Parameters.AddWithValue("@Value2", driverName.Text);
                updateCmd.Parameters.AddWithValue("@Value3", int.Parse(licenseNo.Text));
                updateCmd.Parameters.AddWithValue("@Value4", licenseType.Text);
                updateCmd.Parameters.AddWithValue("@Value5", bloodType.Text);
                updateCmd.Parameters.AddWithValue("@Value6", comboBox1.Text);
                updateCmd.Parameters.AddWithValue("@Value7", textBox1.Text);
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
    }
}
