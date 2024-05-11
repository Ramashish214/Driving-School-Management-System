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

        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\dbSystemDSMS.mdf;Integrated Security=True");

        /*private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Value = monthCalendar1.SelectionStart;
        }*/

        private void addBtn_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                // Check if the record with the given ID exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Schedule WHERE ID = @ID", connection);
                checkCmd.Parameters.AddWithValue("@ID", textBox2.Text);
                int count = (int)checkCmd.ExecuteScalar();
                if (textBox2.Text!="")
                {
                    if (count > 0)
                    {
                        // If record exists, update it
                        SqlCommand updateCmd = new SqlCommand("UPDATE Schedule SET Details = @Value2 WHERE ID = @ID", connection);
                        updateCmd.Parameters.AddWithValue("@Value2", textBox1.Text);
                        updateCmd.Parameters.AddWithValue("@ID", textBox2.Text);
                        updateCmd.ExecuteNonQuery();
                        MessageBox.Show("Updated");
                    }
                    else
                    {
                        // If record doesn't exist, insert a new one
                        SqlCommand insertCmd = new SqlCommand("INSERT INTO Schedule VALUES (@Value2, @Value3)", connection);
                        insertCmd.Parameters.AddWithValue("@Value2", monthCalendar1.SelectionStart);
                        insertCmd.Parameters.AddWithValue("@Value3", textBox1.Text);
                        insertCmd.ExecuteNonQuery();
                        MessageBox.Show("Inserted");
                    }
                }
                else
                {
                    MessageBox.Show("Enter Details");
                }
                

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
            textBox1.Text = "";
            //textBox2.Text = "";
            /*if (events != null )
            {
                
            }*/
            foreach (string ev in events)
            {
                //listBox1.Items.Add(ev);
                textBox1.Text = ev;
                // Console.WriteLine(events);
            }
            /*else
            {
                textBox1.Multiline = true;
                textBox1.Text = "This is a multiline textbox.";
                //Console.WriteLine("dghds"+ events);
            }*/

        }

        public List<string> GetEventsForDate(DateTime selectedDate)
        {
            List<string> events = new List<string>();
            //connection.Open();

            SqlCommand checkCmd = new SqlCommand("SELECT Details FROM Schedule WHERE Date = @SelectedDate",connection);
            checkCmd.Parameters.AddWithValue("@SelectedDate", selectedDate);
            SqlCommand checkCmd1 = new SqlCommand("SELECT Id FROM Schedule WHERE Date = @SelectedDate", connection);
            checkCmd1.Parameters.AddWithValue("@SelectedDate", selectedDate);
            
            connection.Open();
            SqlDataReader reader = checkCmd.ExecuteReader();
            
            while (reader.Read())
            {
                events.Add(reader["Details"].ToString());
                //textBox2.Text = reader["Id"].ToString();  
            }
            reader.Close();
            SqlDataReader reader1 = checkCmd1.ExecuteReader();
            if (reader1.Read())
            {
                //events.Add(reader["Details"].ToString());
                
                textBox2.Text = reader1["Id"].ToString();
            }
            else
            {
                textBox2.Text = "";
            }
            
            reader1.Close();
            connection.Close();
            return events;
        }

        private void YourForm_Load(object sender, EventArgs e)
        {
            // Set the formatted text when the form loads
            textBox1.Multiline = true;
            textBox1.Text = "This is a multiline textbox.\r\n"
                          + "You can add formatted text like this:\r\n"
                          + "- Bullet Point 1\r\n"
                          + "- Bullet Point 2\r\n"
                          + "    - Sub Bullet Point\r\n"
                          + "More text here...";
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            // Check if the driverId TextBox is not empty
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                // Confirm with the user
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Schedule WHERE Id = @ID", connection);
                        cmd.Parameters.AddWithValue("@ID", textBox2.Text);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Deleted");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        //BindData();
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
                MessageBox.Show("No Events to Delete");
            }
        }

        private void Schedule_Load(object sender, EventArgs e)
        {

        }
    }
}
