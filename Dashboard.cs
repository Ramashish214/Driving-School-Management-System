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
        //connection string to database
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\dbSystemDSMS.mdf;Integrated Security=True");
        
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e) // form loading with these functions
        {
            UpdateCounts();
            LoadTomorrowsEventsToListView();
        }

        private void UpdateCounts()
        {
            try
            {
                connection.Open();

                // get count of drivers
                SqlCommand driverCmd = new SqlCommand("SELECT COUNT(*) FROM Driver", connection);
                int driverCount = Convert.ToInt32(driverCmd.ExecuteScalar());

                // get count of vehicles
                SqlCommand vehicleCmd = new SqlCommand("SELECT COUNT(*) FROM Vehicle", connection);
                int vehicleCount = Convert.ToInt32(vehicleCmd.ExecuteScalar());

                // get count of learners
                SqlCommand learnerCmd = new SqlCommand("SELECT COUNT(*) FROM Learner", connection);
                int learnerCount = Convert.ToInt32(learnerCmd.ExecuteScalar());

                // display counts
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

        private void LoadTomorrowsEventsToListView()
        {
            // get tomorrow's date
            DateTime tomorrow = DateTime.Today.AddDays(1);

            // store events for tomorrow
            List<string> events = GetEventsForDate(tomorrow);

            // display events in ListView
            listView1.Items.Clear();
            foreach (string ev in events)
            {
                listView1.Items.Add(ev);
            }
        }
        public List<string> GetEventsForDate(DateTime selectedDate)
        {
            List<string> events = new List<string>();

            //get schedule details
            SqlCommand checkCmd = new SqlCommand("SELECT Details FROM Schedule WHERE Date = @SelectedDate", connection);
            checkCmd.Parameters.AddWithValue("@SelectedDate", selectedDate);

            connection.Open();
            SqlDataReader reader = checkCmd.ExecuteReader();

            while (reader.Read())
            {
                events.Add(reader["Details"].ToString());
            }

            connection.Close();
            return events;
        }

    }
}
