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

        /*private void SetBlackTheme()
        {
            BackColor = Color.Black;
            ForeColor = Color.White;
            foreach (Control control in Controls)
            {
                control.BackColor = Color.Black;
                control.ForeColor = Color.White;
            }
        }

        private void SetWhiteTheme()
        {
            BackColor = Color.White;
            ForeColor = Color.Black;
            foreach (Control control in Controls)
            {
                control.BackColor = Color.White;
                control.ForeColor = Color.Black;
            }
        }*/

        private void black_Click(object sender, EventArgs e)
        {
            //SetBlackTheme();
        }

        private void white_Click(object sender, EventArgs e)
        {
            //SetWhiteTheme();
        }
        /*
        private Color _backgroundColor;
        private Color _foregroundColor;

        private void SetBlackTheme()
        {
            _backgroundColor = Color.Black;
            _foregroundColor = Color.White;
            ApplyTheme(this.Controls);
            // Apply theme to forms in the right panel
            ApplyThemeToRightPanel();
        }

        private void SetWhiteTheme()
        {
            _backgroundColor = Color.White;
            _foregroundColor = Color.Black;
            ApplyTheme(this.Controls);
            // Apply theme to forms in the right panel
            ApplyThemeToRightPanel();
        }

        private void ApplyTheme(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                control.BackColor = _backgroundColor;
                control.ForeColor = _foregroundColor;

                if (control.HasChildren)
                {
                    ApplyTheme(control.Controls);
                }
            }
        }

        private void ApplyThemeToRightPanel()
        {
            foreach (Control control in panelRight.Controls)
            {
                if (control is Form)
                {
                    control.BackColor = _backgroundColor;
                    control.ForeColor = _foregroundColor;

                    // Apply theme to controls within the form
                    ApplyTheme(control.Controls);
                }
            }
        }*/

    }
}
