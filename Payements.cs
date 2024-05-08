using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace driving_school_management_system
{
    public partial class Payements : Form
    {
        public Payements()
        {
            InitializeComponent();
            printDocument1.PrintPage += printDocument1_PrintPage;
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A5", 583, 827);
        }

        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\BSc.Electrical Engineering\\SEM 04\\CodeDnM\\C#\\driving_school_management_system\\dbSystemDSMS.mdf\";Integrated Security=True");

        private void searchBtn_Click(object sender, EventArgs e)
        {
            // Check if the text box has a value
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a search term.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string searchText = textBox1.Text.Trim();

        

            
                // Open the connection
            connection.Open();

             
                
            SqlCommand command = new SqlCommand("SELECT * FROM Learner WHERE Id = @SearchText", connection);
               
            command.Parameters.AddWithValue("@SearchText", searchText);


            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read()) // Check if a row is found
                {
                    // Load details into textboxes
                    textBox2.Text = reader["Name"].ToString();
                    textBox3.Text = reader["Address"].ToString();
                    textBox4.Text = reader["Contact No"].ToString();
                    textBox5.Text = reader["License Type"].ToString();                
                    connection.Close();

                }
                else
                {
                    // If no matching record is found, show a message
                    MessageBox.Show("No record found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    connection.Close();

                }

            }
        }

        private void calculateBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))
            {
                var total = double.Parse(textBox6.Text)-double.Parse(textBox6.Text)*double.Parse(textBox7.Text)/100;
                textBox8.Text = total.ToString();
            }
            else
            {
                MessageBox.Show("Enter Training Cost and Discount");
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font font = new Font("Arial", 12);
            Brush brush = Brushes.Black;

            // Example: Print textbox values with labels
            string nameLabelText = label1.Text;
            string nameValueText = textBox1.Text; // Assuming textBoxName is the textbox for name
            string formattedText = $"{nameLabelText} {nameValueText}";
            g.DrawString(formattedText, font, brush, 100, 100);

        }

        private void printBtn_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // Start printing
                printDocument1.Print();
            }
        }
    }
}
