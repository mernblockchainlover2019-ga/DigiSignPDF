using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigiSignPDF.AD;

using System.IO;
using Org.BouncyCastle.X509;
using System.Security.Cryptography.X509Certificates;

namespace DigiSignPDF
{
    public partial class ADAuthentication : UserControl
    {
        bool isCertificateVerified = false;
        Timer certificateVerifyTimer = new Timer();
        int validatorTimeLeft = 0;

        public ADAuthentication()
        {
            InitializeComponent();

            txtUserName.GotFocus += RemoveUserNamePlaceholder;
            txtUserName.LostFocus += AddUserNamePlaceholder;
            

            if (Properties.Settings.Default.rememberMe == true)
            {
                txtUserName.Text = Properties.Settings.Default.UserName;
                txtPassword.Text = Properties.Settings.Default.Password;
                chkRememberMe.Checked = true;
            }
            else
            {
                txtUserName.Text = "Enter PS No.";
            }

            //txtUserName.Text = "91348381";
            //txtPassword.Text = "Abcd@1234";
        }

        public void RemoveUserNamePlaceholder(object sender, EventArgs e)
        {
            if (txtUserName.Text == "Enter PS No.")
            {
                txtUserName.Text = "";
            }
        }
        public void AddUserNamePlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
                txtUserName.Text = "Enter PS No.";
        }



        private void CertificateVerifyTimer_Tick(object sender, EventArgs e)
        {

            validatorTimeLeft--;
            if (validatorTimeLeft < 0)
                validatorTimeLeft = 0;

            if (validatorTimeLeft <= 0)
            {
                certificateVerifyTimer.Stop();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Time out, Please try again.";
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
            else
            {
                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadWrite);
                var certificates = store.Certificates.Find(X509FindType.FindBySubjectName, Properties.Settings.Default.FullName, true);
                if (certificates.Count > 0)
                {
                    System.Diagnostics.Process[] word_apps = System.Diagnostics.Process.GetProcessesByName("winword");
                    if (word_apps.Count() == 0)
                    {
                        foreach (var item in certificates)
                        {
                            store.Remove(item);
                        }
                    }

                    isCertificateVerified = true;
                }

                if (isCertificateVerified == true && Properties.Settings.Default.IsLogin)
                {
                    certificateVerifyTimer.Stop();

                    var digisignForm = new DigiSignPDF.DigiSignForm();

                    Panel pnl = this.Parent as Panel;
                    if (pnl != null)
                    {
                        Home homeForm = (Home)this.FindForm();
                        homeForm.btnRbnSettings.Enabled = true;
                        homeForm.btnRbnUndoSign.Enabled = true;

                        pnl.Controls.Clear();
                        pnl.Controls.Add(digisignForm);
                    }

                    this.Cursor = System.Windows.Forms.Cursors.Default;
                }
                else if (Properties.Settings.Default.IsLogin)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Please wait while windows verify your certificate.";
                }
            }


        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            Login();
            //this.Cursor = System.Windows.Forms.Cursors.Default;
        }


        private async System.Threading.Tasks.Task<bool> InstallUserCertificate()
        {
            Utility.GetUserCertificate();
            return true;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                Login();
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }


        private void Login()
        {
            try
            {
                validatorTimeLeft = 180;

                string username = txtUserName.Text;
                string password = txtPassword.Text;

                DirectoryService directoryService = null;// Globals.ThisAddIn.directoryService;
                if (directoryService == null)
                    directoryService = new DirectoryService();

                bool isValid = true; // Static directoryService.getAuthentication(username, password);

                if (isValid)
                {
                    Properties.Settings.Default.UserName = txtUserName.Text;
                    Properties.Settings.Default.Password = txtPassword.Text;
                    Properties.Settings.Default.FullName = "AD User"; // Static  directoryService.GetDisplayNamefromUserID(username);
                    Properties.Settings.Default.mobileNo = "98794220022"; // Static directoryService.GetMobileNoFromUserID(Properties.Settings.Default.UserName);
                    Properties.Settings.Default.email = "abc@gmail.com"; // Static directoryService.GetEmailIDfromUserID(Properties.Settings.Default.UserName);
                    Properties.Settings.Default.departmentName = "QC";// Static directoryService.GetDepartmentNameFromUserID(Properties.Settings.Default.UserName);
                    Properties.Settings.Default.designation = "Manager"; // Static directoryService.GetDesignationFromUserID(Properties.Settings.Default.UserName);

                    Properties.Settings.Default.IsLogin = true;

                    if (chkRememberMe.Checked == true)
                        Properties.Settings.Default.rememberMe = true;
                    else
                        Properties.Settings.Default.rememberMe = false;

                    Properties.Settings.Default.Save();

                    //bool isCertificateGenerated = Utility.GenerateUserCertificate();
                    //if (!isCertificateGenerated)
                    //{
                    //    lblMessage.Text = "Server connection is not available. Please contact your administrator.";
                    //}
                    if (false)
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Server connection is not available. Please contact your administrator.";
                    }
                    else
                    {
                        //InstallUserCertificate();

                        //certificateVerifyTimer.Interval = 1000;
                        //certificateVerifyTimer.Tick += CertificateVerifyTimer_Tick;
                        //certificateVerifyTimer.Start();


                        var digisignForm = new DigiSignPDF.DigiSignForm();

                        Panel pnl = this.Parent as Panel;
                        if (pnl != null)
                        {
                            Home homeForm = (Home)this.FindForm();
                            homeForm.btnRbnSettings.Enabled = true;
                            homeForm.btnRbnUndoSign.Enabled = true;

                            pnl.Controls.Clear();
                            pnl.Controls.Add(digisignForm);
                        }


                    }

                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Invalid Username or Password.";
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    //txtUserName.Focus();
                }

            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;

                if (ex.InnerException != null && ex.InnerException.Message.Contains("server is unavailable"))
                    lblMessage.Text = "Active directory connection is not available.";
                else
                    //lblMessage.Text = ex.InnerException.Message;
                    lblMessage.Text = "Server connection is not available. Please contact your administrator.";

                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }





    }
}
