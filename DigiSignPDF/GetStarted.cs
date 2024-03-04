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
    public partial class GetStarted : UserControl
    {
        public GetStarted()
        {
            InitializeComponent();
        }

        private void btnGetStarted_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsLogin = false;
            Properties.Settings.Default.isOtpSubmitted = false;
            Properties.Settings.Default.Save();

            Home homeForm = (Home)this.FindForm();
            homeForm.btnRbnSettings.Enabled = false;
            homeForm.btnRbnUndoSign.Enabled = false;

            var authenticationUserControl = new DigiSignPDF.ADAuthentication();

            Panel pnl = this.Parent as Panel;
            if (pnl != null)
            {
                pnl.Controls.Clear();
                pnl.Controls.Add(authenticationUserControl);
            }

        }
    }
}
