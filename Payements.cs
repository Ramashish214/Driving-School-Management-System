using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace driving_school_management_system
{
    public partial class Payements : Form
    {

        private string filePath = Path.Combine(Environment.CurrentDirectory, "DrivingSchoolDetails.txt"); // zbsolute path to the file to store the values
        private string descriptionFilePath = "ServiceDescription.txt";

        public Payements()
        {
            InitializeComponent();
            
            printDocument1.PrintPage += printDocument1_PrintPage;
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A6", 413, 583); // for print bill
        }

        //connection string
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\BSc.Electrical Engineering\\SEM 04\\CodeDnM\\C#\\driving_school_management_system\\dbSystemDSMS.mdf\";Integrated Security=True");
        private void LoadTextBoxValues()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    if (lines.Length >= 3) // load name, address and contact no
                    {
                        textBox9.Text = lines[0];
                        textBox10.Text = lines[1];
                        textBox11.Text = lines[2];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading textbox values: " + ex.Message);
            }
        }
        private void searchBtn_Click(object sender, EventArgs e)
        {
            // check if the text box has a value
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a search term.", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string searchText = textBox1.Text.Trim();
                 
            //open the connection
            connection.Open();             
                
            SqlCommand command = new SqlCommand("SELECT * FROM Learner WHERE Id = @SearchText", connection);
               
            command.Parameters.AddWithValue("@SearchText", searchText);


            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read()) // check if a row is found
                {
                    // load details into textboxes
                    textBox2.Text = reader["Name"].ToString();
                    textBox3.Text = reader["Address"].ToString();
                    textBox4.Text = reader["Contact No"].ToString();
                    textBox5.Text = reader["License Type"].ToString();                
                    connection.Close();

                }
                else
                {
                    // if no matching record is found, show a message
                    MessageBox.Show("No record found.", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    connection.Close();

                }

            }
        }

        private void calculateBtn_Click(object sender, EventArgs e) // bill calculation funtion
        {
            if (!string.IsNullOrWhiteSpace(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))
            {
                var total = double.Parse(textBox6.Text)-double.Parse(textBox6.Text)*double.Parse(textBox7.Text)/100;
                textBox8.Text = total.ToString();
            }
            else
            {
                MessageBox.Show("Enter Training Cost and Discount", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font titleFont = new Font("Times New Roman", 14, FontStyle.Bold);
            Font bodyFont = new Font("Times New Roman", 12);
            Brush brush = Brushes.Black;
            int startX = 50;
            int startY = 50;
            int lineHeight = 25;
            int rectanglePadding = 10; // padding around the rectangles

            // define labels and textboxes array
            Label[] labels = { label1, label2, label3, label4, label5, label6, label7, label8 };
            TextBox[] textBoxes = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8 };

            // driving school details
            string schoolName = textBox9.Text;
            string schoolAddress = textBox10.Text;
            string schoolContact = textBox11.Text;
            string description = File.ReadAllText(descriptionFilePath);
            description = description.Replace("🌟", "\u2605");
            
            // print driving school details at the top
            g.DrawString(schoolName, titleFont, brush, startX, startY);
            g.DrawString(schoolAddress, bodyFont, brush, startX, startY + lineHeight);
            g.DrawString(schoolContact, bodyFont, brush, startX, startY + 2 * lineHeight);

            // draw separator line
            g.DrawLine(new Pen(Color.Black), startX, startY + 3 * lineHeight, e.MarginBounds.Right - startX, startY + 3 * lineHeight);

            // draw rectangles for two columns
            int columnWidth = (e.MarginBounds.Right - startX) / 2 - 2 * rectanglePadding;
            int rectangleHeight = 250;//labels.Length * 2 * lineHeight + 2 * rectanglePadding;

            Rectangle leftColumnRect = new Rectangle(startX, startY + 4 * lineHeight, columnWidth, rectangleHeight);
            Rectangle rightColumnRect = new Rectangle(startX + columnWidth + 2 * rectanglePadding, startY + 4 * lineHeight, columnWidth, rectangleHeight);

            // draw rectangles for two columns
            g.DrawRectangle(Pens.Black, leftColumnRect);
            g.DrawRectangle(Pens.Black, rightColumnRect);

            // print student details in the left column
            PrintSection("Student Details", labels.Take(4).ToArray(), textBoxes.Take(4).ToArray(), startX + rectanglePadding, startY + 4 * lineHeight + rectanglePadding, lineHeight, columnWidth);

            // print course details in the right column
            PrintSection("Training Details", labels.Skip(4).ToArray(), textBoxes.Skip(4).ToArray(), startX + columnWidth + 3 * rectanglePadding, startY + 4 * lineHeight + rectanglePadding, lineHeight, columnWidth);

            g.DrawString(description, bodyFont, brush, startX, 450);

            // set the PDF document size
            e.PageSettings.PaperSize = new PaperSize("Custom", e.MarginBounds.Right - startX, startY + rectangleHeight + 4 * lineHeight);

            // print generated time and date at the bottom
            string generatedDateTime = $"PDF Generated: {DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt")} - L Tracker Plus";
            SizeF generatedDateTimeSize = g.MeasureString(generatedDateTime, bodyFont);
            g.DrawString(generatedDateTime, bodyFont, brush, startX, e.MarginBounds.Bottom - generatedDateTimeSize.Height);

            // helper method to print each section
            void PrintSection(string sectionTitle, Label[] sectionLabels, TextBox[] sectionTextBoxes, int x, int y, int lineSpacing, int width)
            {
                g.DrawString(sectionTitle, titleFont, brush, x, y);
                y += lineHeight;

                for (int i = 0; i < sectionLabels.Length; i++)
                {
                    string labelText = sectionLabels[i].Text;
                    string textBoxValue = sectionTextBoxes[i].Text;
                    PrintDetail(g, labelText, textBoxValue, x + rectanglePadding, ref y, titleFont, bodyFont, brush, lineSpacing);
                }
            }

            // helper method to print each detail
            void PrintDetail(Graphics graphics, string label, string value, int x, ref int y, Font labelFont, Font valueFont, Brush color, int lineSpacing)
            {
                graphics.DrawString(label, labelFont, color, x, y);
                y += lineSpacing;
                graphics.DrawString(value, valueFont, color, x, y);
                y += lineSpacing;
            }
        }

        private void printBtn_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // start printing
                printDocument1.Print();
            }
        }

        private void Payements_Load(object sender, EventArgs e)
        {
            LoadTextBoxValues();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lines = { textBox9.Text, textBox10.Text, textBox11.Text };
                File.WriteAllLines(filePath, lines);
                MessageBox.Show("Updated Successfully", "L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving textbox values: " + ex.Message);
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // display confirmation message box
            DialogResult result = MessageBox.Show("Are you sure you want to mark this payment as 'paid'?", "L Tracker Plus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // check if the user clicked Yes
            if (result == DialogResult.Yes)
            {
               

                try
                {
                    connection.Open();

                    int id = int.Parse(textBox1.Text);

                    string sql = "UPDATE Learner SET Payments = @PaymentStatus WHERE Id = @ID";

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@PaymentStatus", "paid");
                    command.Parameters.AddWithValue("@ID", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Payment status updated to 'paid' for ID: " + id);
                    }
                    else
                    {
                        MessageBox.Show("No rows updated. ID: " + id + " not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
