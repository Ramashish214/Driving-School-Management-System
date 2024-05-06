using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace driving_school_management_system
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void LoadForm(Form form)
        {
            // Clear existing controls from the panel
            panelRight.Controls.Clear();

            // Set the properties of the form being loaded
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // Add the form to the panel and display it
            panelRight.Controls.Add(form);
            form.Show();
        }

        private void DashboardBtn_Click(object sender, EventArgs e)
        {
            LoadForm(new Dashboard());
        }

        private void DriversBtn_Click(object sender, EventArgs e)
        {
            LoadForm(new Drivers());
        }

        private void LearnersBtn_Click(object sender, EventArgs e)
        {
            LoadForm(new Learners());
        }

        private void VehiclesBtn_Click(object sender, EventArgs e)
        {
            LoadForm(new Vehicles());
        }

        private void ScheduleBtn_Click(object sender, EventArgs e)
        {
            LoadForm(new Schedule());
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadForm(new Dashboard());
        }
    }
}
