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

        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\BSc.Electrical Engineering\\SEM 04\\CodeDnM\\C#\\driving_school_management_system\\dbSystemDSMS.mdf\";Integrated Security=True");

        void BindData()
        {
            SqlCommand cmd = new SqlCommand("select * from Vehicle", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridViewVehicles.DataSource = dt;
            //dataGridViewVehicles.ClearSelection();
            //dataGridViewVehicles.CurrentCell = null;
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
                                    .FirstOrDefault(column => column.HeaderText == "Next Service Date") // Replace "DateColumnName" with your actual column header text
                                    ?.Index ?? -1;

            if (dateColumnIndex != -1)
            {
                // Extract all dates from the DataGridView
                var dates = dataGridViewVehicles.Rows.Cast<DataGridViewRow>()
                                .Where(row => row.Cells[dateColumnIndex].Value != null && row.Cells[dateColumnIndex].Value != DBNull.Value)
                                .Select(row => Convert.ToDateTime(row.Cells[dateColumnIndex].Value));

                // Find the closest date to the current date
                DateTime closestDate = dates.OrderBy(date => Math.Abs((date - DateTime.Now).TotalDays)).First();

                // Display the closest date in the label
                label7.Text = closestDate.ToString("yyyy-MM-dd"); // Adjust the format as needed
            }
            else
            {
                // Handle if the date column is not found
                label7.Text = "No Date Column Found";
            }
        }

        private void Vehicles_Load(object sender, EventArgs e)
        {
            BindData();
            LoadDriverIDs();
            FindClosestDate();
            //
            //
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            connection.Open();

            // Check if the primary key already exists
            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Vehicle WHERE [Vehicle No] = @PrimaryKeyValue", connection);
            checkCmd.Parameters.AddWithValue("@PrimaryKeyValue", vehicleNo.Text);

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

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim();
            dataGridViewVehicles.ClearSelection();
            // Iterate through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridViewVehicles.Rows)
            {
                // Assuming the unique identifier is in the first column, adjust column index if necessary
                string cellValue = row.Cells[0].Value.ToString();

                // If the cell value matches the search text, select the row and exit the loop
                if (cellValue.Equals(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    row.Selected = true;
                    dataGridViewVehicles.FirstDisplayedScrollingRowIndex = row.Index; // Scroll to the selected row
                    break;
                }
            }
        }
    }   
}
