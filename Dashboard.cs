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
        //SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\BSc.Electrical Engineering\\SEM 04\\CodeDnM\\C#\\driving_school_management_system\\dbSystemDSMS.mdf\";Integrated Security=True");
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\dbSystemDSMS.mdf;Integrated Security=True");
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e) // form loading with these functions
        {
            UpdateCounts();           
            LoadNext7DaysEventsToDataGridView();
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
                   
        private void LoadNext7DaysEventsToDataGridView()
        {
            // get today's date
            DateTime today = DateTime.Today;
            // get the date 7 days from now
            DateTime nextWeek = DateTime.Today.AddDays(7);

            // store events for the next 7 days
            List<(DateTime, string)> events = GetEventsForDateRange(today, nextWeek);

            // create a dataTable to hold the events
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Date", typeof(DateTime));
            dataTable.Columns.Add("Details", typeof(string));

            
            foreach (var (date, details) in events)
            {
                dataTable.Rows.Add(date, details);
            }

            // bind the dataTable to the dataGridView
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns["Date"].Width = 100;
        }

        public List<(DateTime, string)> GetEventsForDateRange(DateTime startDate, DateTime endDate)
        {
            List<(DateTime, string)> events = new List<(DateTime, string)>();

            // get schedule details for the next 7 days
            SqlCommand checkCmd = new SqlCommand("SELECT Date, Details FROM Schedule WHERE Date >= @StartDate AND Date < @EndDate", connection);
            checkCmd.Parameters.AddWithValue("@StartDate", startDate);
            checkCmd.Parameters.AddWithValue("@EndDate", endDate);

            connection.Open();
            SqlDataReader reader = checkCmd.ExecuteReader();

            while (reader.Read())
            {
                DateTime eventDate = Convert.ToDateTime(reader["Date"]);
                string details = reader["Details"].ToString();
                events.Add((eventDate, details));
            }

            connection.Close();
            return events;
        }


    }
}
