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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\BSc.Electrical Engineering\\SEM 04\\CodeDnM\\C#\\driving_school_management_system\\dbSystemDSMS.mdf\";Integrated Security=True");

        private void UpdateCounts()
        {
            try
            {
                connection.Open();

                // Get count of drivers
                SqlCommand driverCmd = new SqlCommand("SELECT COUNT(*) FROM Driver", connection);
                int driverCount = Convert.ToInt32(driverCmd.ExecuteScalar());

                // Get count of vehicles
                SqlCommand vehicleCmd = new SqlCommand("SELECT COUNT(*) FROM Vehicle", connection);
                int vehicleCount = Convert.ToInt32(vehicleCmd.ExecuteScalar());

                SqlCommand learnerCmd = new SqlCommand("SELECT COUNT(*) FROM Learner", connection);
                int learnerCount = Convert.ToInt32(learnerCmd.ExecuteScalar());

                // Display counts on labels
                driverCountLabel.Text = driverCount.ToString();
                vehicleCountLabel.Text = vehicleCount.ToString();
                studentsCountLabel.Text = learnerCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            UpdateCounts();
        }
    }
}
