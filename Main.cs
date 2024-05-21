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

            // make styles in loading form
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // display correct form in panel
            panelRight.Controls.Add(form);
            form.Show();
           
        }

        private void DashboardBtn_Click(object sender, EventArgs e)
        {
            LoadForm(new Dashboard()); // load dashborad form to right panel
        }

        private void DriversBtn_Click(object sender, EventArgs e)
        {
            LoadForm(new Drivers()); // load drivers form to right panel
        }

        private void LearnersBtn_Click(object sender, EventArgs e)
        {
            LoadForm(new Learners()); // load learners form to right panel
        }

        private void VehiclesBtn_Click(object sender, EventArgs e)
        {
            LoadForm(new Vehicles()); // load vehicles form to right panel
        }

        private void ScheduleBtn_Click(object sender, EventArgs e)
        {
            LoadForm(new Schedule()); // load schedule form to right panel
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadForm(new Dashboard()); // load dashborad to right panel when starting window
        }   

        private void paymentBtn_Click(object sender, EventArgs e)
        {
            LoadForm(new Payements()); // load payements form to right panel
        }       

    }
}
