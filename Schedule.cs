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
    public partial class Schedule : Form
    {
        public Schedule()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\BSc.Electrical Engineering\\SEM 04\\CodeDnM\\C#\\driving_school_management_system\\dbSystemDSMS.mdf\";Integrated Security=True");

        /*private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Value = monthCalendar1.SelectionStart;
        }*/

        private void addBtn_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand insertCmd = new SqlCommand("INSERT INTO Schedule VALUES (@Value2, @Value3)", connection);
                insertCmd.Parameters.AddWithValue("@Value2", monthCalendar1.SelectionStart);
                insertCmd.Parameters.AddWithValue("@Value3", textBox1.Text);

                insertCmd.ExecuteNonQuery();
                MessageBox.Show("Inserted");
                //BindData();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = monthCalendar1.SelectionStart;

            // Fetch events from the database for the selected date
            List<string> events = GetEventsForDate(selectedDate);

            // Display the events in your form
            listBox1.Items.Clear();
            foreach (string ev in events)
            {
                listBox1.Items.Add(ev);
            }
        }

        public List<string> GetEventsForDate(DateTime selectedDate)
        {
            List<string> events = new List<string>();
            //connection.Open();

            SqlCommand checkCmd = new SqlCommand("SELECT Details FROM Schedule WHERE Date = @SelectedDate",connection);
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
