using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace driving_school_management_system
{
    public partial class ChangeLogin : Form
    {
        public ChangeLogin()
        {
            InitializeComponent();
        }
        //database connection
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\BSc.Electrical Engineering\\SEM 04\\CodeDnM\\C#\\driving_school_management_system\\dbSystemDSMS.mdf\";Integrated Security=True");
        
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string favoriteVehicle = textBox1.Text;

            // read the value from the text file
            string savedFavoriteVehicle = File.ReadAllText("SecurityQuestion.txt").Trim();

            if (string.Equals(favoriteVehicle, savedFavoriteVehicle, StringComparison.OrdinalIgnoreCase))
            {               
                UsernameBtn.Visible=true;           //active visibility of change password option
                PasswordBtn.Visible = true;
                label2.Visible=true;
                textBox4.Visible=true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                button1.Visible = true;
            }
            else
            {
                MessageBox.Show("Incorrect answer. Please try again.");
            }
        }

        private void ChangeLogin_Load(object sender, EventArgs e)
        {            
            UsernameBtn.Hide();   //hide visibility of change password option
            PasswordBtn.Hide();
            textBox2.Hide();
            textBox3.Hide();
            button1.Hide();
            label2.Hide();
            textBox4.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                       
            string currentUsername = textBox4.Text;
            string newUsername = textBox3.Text;
            string newPassword = textBox2.Text;

            try
            {
                
                connection.Open();

                string query = "UPDATE Login SET username=@NewUsername, password=@NewPassword WHERE Username=@CurrentUsername";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CurrentUsername", currentUsername);
                command.Parameters.AddWithValue("@NewUsername", newUsername);
                command.Parameters.AddWithValue("@NewPassword", newPassword);

                int result = command.ExecuteNonQuery();

                if (result == 1)
                {
                    MessageBox.Show("Login details updated successfully!");
                    connection.Close();
                    this.Hide();
                    Login form = new Login();
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Failed to update credentials. Please check the current username.");
                }

                connection.Close();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"A database error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)   // back to login portal
        {
            this.Hide();    
            Login form = new Login();
            form.Show();
        }
    }
}
