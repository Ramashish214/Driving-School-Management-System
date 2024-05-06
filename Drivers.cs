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


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Drivers_Load(object sender, EventArgs e)
        {
            BindData();
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
                SqlCommand insertCmd = new SqlCommand("INSERT INTO Driver VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6)", connection);
                insertCmd.Parameters.AddWithValue("@Value1", driverId.Text);
                insertCmd.Parameters.AddWithValue("@Value2", driverName.Text);
                insertCmd.Parameters.AddWithValue("@Value3", int.Parse(licenseNo.Text));
                insertCmd.Parameters.AddWithValue("@Value4", licenseType.Text);
                insertCmd.Parameters.AddWithValue("@Value5", bloodType.Text);
                insertCmd.Parameters.AddWithValue("@Value6", vehicleInCharge.Text);
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
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DELETE  Driver  WHERE Id = @ID", connection);
                cmd.Parameters.AddWithValue("@ID", driverId.Text);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Deleted");
                BindData();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Enter ID");
            }
            
        }
    }
}
