using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigiSignPDF
{
    public partial class UserProfile : UserControl
    {
        public UserProfile()
        {
            InitializeComponent();

            string fullName = Properties.Settings.Default.FullName;
            string email = Properties.Settings.Default.email;
            string mobileNumber = Properties.Settings.Default.mobileNo;
            string designation = Properties.Settings.Default.designation;

            if (fullName.Length > 40)
            {
                lblFullName.Location = new Point(14, 45);
                lblFullName.Font = new Font(lblFullName.Font.FontFamily,11);
            }

            lblFullName.Text = fullName;

            if (email != "0")
                lblEmail.Text = email;
            else
                lblEmail.Text = "Email not available";

            if (mobileNumber != "0")
                lblPhone.Text = mobileNumber;
            else
                lblPhone.Text = "Phone number not available";

            if (designation != "0")
                lblDesignation.Text = designation;
            else
                lblDesignation.Text = "Designation not available";

        }

        private void btnBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var digisignForm = new DigiSignPDF.DigiSignForm();

            Panel pnl = this.Parent as Panel;
            if (pnl != null)
            {
                pnl.Controls.Clear();
                pnl.Controls.Add(digisignForm);
            }

        }
    }
}
