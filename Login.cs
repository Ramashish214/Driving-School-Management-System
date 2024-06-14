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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        //connection string
        //SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\BSc.Electrical Engineering\\SEM 04\\CodeDnM\\C#\\driving_school_management_system\\dbSystemDSMS.mdf\";Integrated Security=True");
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\dbSystemDSMS.mdf;Integrated Security=True");
        private void LoginBtn_Click(object sender, EventArgs e)
        {
        
            string username = textBox1.Text;
            string password = textBox2.Text;
            //check login details
            try
            {
                
                connection.Open();

                string query = "SELECT COUNT(1) FROM Login WHERE username=@Username AND password=@Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count == 1)
                {
                    //MessageBox.Show("Login successful!");
                    this.Hide();
                    Main form = new Main();
                    form.Show();
                    
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect.","L Tracker Plus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                
                connection.Close();
            }
            catch (SqlException sqlEx)   // handle exceptions
            {
                MessageBox.Show($"A database error occurred: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();                    // proceed to change password form
            ChangeLogin changeLogin = new ChangeLogin();
            changeLogin.Show();
        }
    }
}
