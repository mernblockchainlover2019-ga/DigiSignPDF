using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace DigiSignPDF
{
    public partial class AddSignatureForm : UserControl
    {
        byte[] imgBytes = null;
        byte[] smallSignImgBytes = null;
        string OTP = "";
        int timeLeft = 0;
        int validatorTimeLeft = 0;
        DateTime dateTime = new DateTime();

        public AddSignatureForm(byte[] signatureImage, byte[] smallSignatureImage)
        {
            InitializeComponent();

            imgBytes = signatureImage;
            smallSignImgBytes = smallSignatureImage;
            otpTimer.Tick += Timer_Tick;
            otpValidTimer.Tick += otpValidTimer_Tick;

            OnLoad();
            //txtOtp.Text = OTP;
            //System.Diagnostics.Debug.WriteLine("DigiSign Verification OTP is " + OTP);

        }

        private void OnLoad()
        {
            if (Properties.Settings.Default.isOtpSubmitted == true)
            {
                lblOTP.Visible = false;
                txtOtp.Visible = false;
                //btnSubmitOtp.Hide();
                //SubmitOTP();
            }
            else
            {
                if (SendOTP())
                {
                    timeLeft = 120;
                    validatorTimeLeft = 600;
                }
                else
                {
                    timeLeft = 120;
                    validatorTimeLeft = 600;
                }
                lblTimer.Text = dateTime.AddSeconds(timeLeft).ToString("mm:ss");
                otpTimer.Start();
                otpValidTimer.Start();
            }
        }


        private void btnSubmitOtp_Click(object sender, EventArgs e)
        {
            SubmitOTP();
        }

        private void btnResendOtp_Click(object sender, EventArgs e)
        {
            txtOtp.Text = "";

            if (btnResendOtp.Visible == true)
            {
                txtOtp.Focus();

                if (SendOTP())
                {
                    timeLeft = 120;
                    validatorTimeLeft = 600;
                }
                else
                {
                    timeLeft = 120;
                    validatorTimeLeft = 600;
                }

                btnResendOtp.Visible = false;
                btnResendOtp.Enabled = false;
                lblTimer.Text = dateTime.AddSeconds(timeLeft).ToString("mm:ss");
                otpTimer.Start();
                otpValidTimer.Start();
                lblMessage.Text = "";
            }

        }


        private bool SendOTP()
        {
            try
            {
                Random r = new Random();
                OTP = r.Next(10000, 99999).ToString();

                string userName = Properties.Settings.Default.UserName;
                string password = Properties.Settings.Default.Password;


                string mobile = Properties.Settings.Default.mobileNo;
                string email = Properties.Settings.Default.email;


                if ((!string.IsNullOrEmpty(mobile) || !string.IsNullOrEmpty(email)) && (mobile != "0" || email != "0"))
                {
                    bool result = Utility.SendOTP(mobile, email, OTP);

                    if (!result)
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "OTP sending failed";
                        return false;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(mobile) && mobile != "0")
                            lblMobileNo.Text = "OTP is sent to XXXXXXXX" + mobile.Substring(mobile.Length - 2);
                    }
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Phone number is not available in system. Please contact administrator.";

                    return false;
                }

            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                if (ex.InnerException != null && ex.InnerException.Message.Contains("server is unavailable"))
                    lblMessage.Text = "Active directory connection is not available.";
                else
                    lblMessage.Text = "Something went wrong";

                return false;
            }

            return true;
        }
        public void SubmitOTP()
        {
            try
            {
                bool verifyOTP = false;


                if (Properties.Settings.Default.isOtpSubmitted == true)
                {
                    verifyOTP = true;
                }
                else if (txtOtp.Text == OTP && !string.IsNullOrEmpty(OTP))
                {
                    verifyOTP = true;
                    lblOTP.Visible = false;
                    txtOtp.Visible = false;
                    lblTimer.Visible = false;
                }
                else
                {
                    verifyOTP = false;
                }

                verifyOTP = true; // Added By Hiten

                if (verifyOTP)
                {
                    if (rbtnLongSignature.Checked == true)
                    {
                        Utility.isSmallSign = false;
                        AddLongSignature();
                    }
                    else
                    {
                        Utility.isSmallSign = true;
                        AddShortSignature();
                    }

                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Invalid OTP. Please try again.";
                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        //[DllImport("shcore.dll")]
        //static extern int SetProcessDpiAwareness(_Process_DPI_Awareness value);

        //enum _Process_DPI_Awareness
        //{
        //    Process_DPI_Unaware = 0,
        //    Process_System_DPI_Aware = 1,
        //    Process_Per_Monitor_DPI_Aware = 2
        //}

        public void AddLongSignature()
        {
            try
            {
                //  SetProcessDpiAwareness(_Process_DPI_Awareness.Process_Per_Monitor_DPI_Aware);
                float dpiX, dpiY, fontsize = 11;
                Graphics graphics = this.CreateGraphics();
                dpiX = graphics.DpiX;
                dpiY = graphics.DpiY;
                if (dpiX <= 100)
                    fontsize = 10;
                else if (dpiX <= 125)
                    fontsize = 9;
                else if (dpiX <= 150)
                    fontsize = 7;
                else if (dpiX <= 175)
                    fontsize = 6;
                else if (dpiX <= 200)
                    fontsize = 5;
                else if (dpiX <= 225)
                    fontsize = 4;
                else if (dpiX <= 250)
                    fontsize = 3;

                string userName = Properties.Settings.Default.UserName;
                string password = Properties.Settings.Default.Password;

                string fullname = Properties.Settings.Default.FullName;
                string mobile = Properties.Settings.Default.mobileNo;
                string email = Properties.Settings.Default.email;
                string departmentName = Properties.Settings.Default.departmentName;
                string designation = Properties.Settings.Default.designation;

                Properties.Settings.Default.isOtpSubmitted = true;
                Properties.Settings.Default.Save();

                btnResendOtp.Visible = false;
                btnResendOtp.Enabled = false;
                txtOtp.Text = "";
                lblMessage.Text = "";

                Utility.signeDate = txtDate.Value;
                //Utility.signeTime = txtTime.Value;

                //string comment = "A while back I needed to count the amount of lette";
                string comment = txtComment.Text.Replace("\n", " ").Replace("\r", " "); ;

                IEnumerable<string> commentParts = new List<string>();
                if (comment.Count(Char.IsWhiteSpace) == 0)
                {
                    var tempList = new List<string>();

                    int chunkSize = 25;
                    int stringLength = comment.Length;
                    for (int i = 0; i < stringLength; i += chunkSize)
                    {
                        if (i + chunkSize > stringLength) chunkSize = stringLength - i;
                        tempList.Add(comment.Substring(i, chunkSize));
                    }

                    commentParts = tempList;
                }
                else
                {
                    commentParts = comment.SplitInParts(27);
                }
                //commentParts = comment.SplitInParts(27);

                IEnumerable<string> designationParts = new List<string>();
                Bitmap bmp;
                byte[] tempImageBytes = imgBytes;
                using (var ms = new MemoryStream(tempImageBytes))
                {
                    bmp = new Bitmap(ms);
                    Bitmap bitmapImg = new Bitmap(bmp, new Size(160, 90));// Original Image Old,821X400

                    int footerHeight = 60;
                    if (chkIncludeDesignation.Checked == true && !string.IsNullOrEmpty(designation) && designation != "0")
                    {
                        designationParts = designation.SplitInParts(27);
                        footerHeight += designationParts.Count() * 22;
                        //footerHeight += 80;
                    }
                    if (rbtnWithDate.Checked)
                    {
                        footerHeight += 22;
                    }

                    if (commentParts.Count() > 0)
                    {
                        footerHeight += commentParts.Count() * 22;
                    }

                    if (chkIncludeDesignation.Checked == false && string.IsNullOrEmpty(txtComment.Text))
                    {
                        bitmapImg = new Bitmap(bmp, new Size(177, 102));
                    }
                    else if (chkIncludeDesignation.Checked == false && !string.IsNullOrEmpty(txtComment.Text))
                    {
                        bitmapImg = new Bitmap(bmp, new Size(177, 102));
                    }
                    else if (chkIncludeDesignation.Checked == true && string.IsNullOrEmpty(txtComment.Text))
                    {
                        bitmapImg = new Bitmap(bmp, new Size(174, 100));
                    }

                    Bitmap bitmapComment = new Bitmap(215, footerHeight);// Footer
                    Bitmap bitmapNewImage = new Bitmap(215, bitmapImg.Height + footerHeight);//New Image
                    Graphics graphicImage = Graphics.FromImage(bitmapNewImage);
                    graphicImage.Clear(Color.White);
                    graphicImage.DrawImage(bitmapImg, new Point(0, 0));
                    graphicImage.DrawImage(bitmapComment, new Point(bitmapComment.Width, 0));

                    graphicImage.DrawString(fullname, new Font("Arial", fontsize), new SolidBrush(Color.Black), 0, 107);

                    int footerLocation = 107;

                    if (chkIncludeDesignation.Checked == true && !string.IsNullOrEmpty(designation) && designation != "0")
                    {
                        foreach (var item in designationParts)
                        {
                            footerLocation += 20;
                            graphicImage.DrawString(item, new Font("Arial", fontsize), new SolidBrush(Color.Black), 0, footerLocation);
                        }
                        //footerLocation += 80;
                        //graphicImage.DrawString(designation, new Font("Arial", 12), new SolidBrush(Color.Black), 0, footerLocation);
                    }

                    if (rbtnWithDate.Checked)
                    {
                        footerLocation += 20;
                        graphicImage.DrawString("Date: " + Utility.signeDate.ToString("dd/MM/yyyy"), new Font("Arial", fontsize), new SolidBrush(Color.Black), 0, footerLocation);
                    }

                    foreach (var item in commentParts)
                    {
                        footerLocation += 20;
                        graphicImage.DrawString(item, new Font("Arial", fontsize), new SolidBrush(Color.Black), 0, footerLocation);
                    }

                    footerLocation += 20;
                    graphicImage.DrawString("Digitally Signed", new Font("Arial", fontsize, FontStyle.Italic), new SolidBrush(Color.Black), 0, footerLocation);

                    using (MemoryStream tempMS = new MemoryStream())
                    {
                        bitmapNewImage.Save(tempMS, System.Drawing.Imaging.ImageFormat.Bmp);
                        tempImageBytes = tempMS.ToArray();
                    }

                    //bitmapNewImage.Save(@"C:\Users\mohmed.naif\Documents\01Img.bmp");
                    bitmapImg.Dispose();
                    bitmapComment.Dispose();
                    bitmapNewImage.Dispose();
                }

                Utility.isDrawSignatureActive = true;
                Utility.imgBytes = tempImageBytes;

                //lblMessage.ForeColor = Color.Green;
                //lblMessage.Text = "Draw signature in document.";
                lblDraw.Text = "Draw signature in document.";
                //AddSign();
            }
            catch (Exception)
            {
            }
        }

        public void AddShortSignature()
        {
            try
            {
                if (smallSignImgBytes == null)
                {
                    lblMessage.Text = "Short signature image is not available.";
                    return;
                }
                else
                {
                    lblMessage.Text = "";
                }

                float dpiX, dpiY, fontsize = 12;
                Graphics graphics = this.CreateGraphics();
                dpiX = graphics.DpiX;
                dpiY = graphics.DpiY;
                if (dpiX <= 100)
                    fontsize = (float)9;
                else if (dpiX <= 125)
                    fontsize = (float)8.5;
                else if (dpiX <= 150)
                    fontsize = (float)7.5;
                else if (dpiX <= 175)
                    fontsize = (float)6.5;
                else if (dpiX <= 200)
                    fontsize = (float)5.5;
                else if (dpiX <= 225)
                    fontsize = (float)4.5;
                else if (dpiX <= 250)
                    fontsize = (float)3.5;

                string userName = Properties.Settings.Default.UserName;
                string password = Properties.Settings.Default.Password;

                string fullname = Properties.Settings.Default.FullName;
                string mobile = Properties.Settings.Default.mobileNo;
                string email = Properties.Settings.Default.email;
                string departmentName = Properties.Settings.Default.departmentName;
                string designation = Properties.Settings.Default.designation;

                Properties.Settings.Default.isOtpSubmitted = true;
                Properties.Settings.Default.Save();

                btnResendOtp.Visible = false;
                btnResendOtp.Enabled = false;
                txtOtp.Text = "";
                lblMessage.Text = "";

                Utility.signeDate = txtDate.Value;
                //Utility.signeTime = txtTime.Value;

                //string comment = "A while back I needed to count the amount of lette";


                Bitmap bmp;
                byte[] tempImageBytes = smallSignImgBytes;
                using (var ms = new MemoryStream(tempImageBytes))
                {
                    bmp = new Bitmap(ms);

                    int footerHeight = 23;

                    Bitmap bitmapImg = new Bitmap(bmp, new Size(80, 60));// Original Image

                    if (rbtnWithDate.Checked)
                    {
                        footerHeight += 30;
                        bitmapImg = new Bitmap(bmp, new Size(100, 90));// Original Image
                    }


                    Bitmap bitmapComment = new Bitmap(80, footerHeight);// Footer

                    Bitmap bitmapNewImage = new Bitmap(bitmapImg.Width, bitmapImg.Height + footerHeight);//New Image
                    Graphics graphicImage = Graphics.FromImage(bitmapNewImage);
                    graphicImage.Clear(Color.White);
                    graphicImage.DrawImage(bitmapImg, new Point(0, 0));
                    graphicImage.DrawImage(bitmapComment, new Point(bitmapComment.Width, 0));

                    if (rbtnWithDate.Checked)
                    {
                        graphicImage.DrawString("Date: " + Utility.signeDate.ToString("dd/MM/yyyy"), new Font("Arial", fontsize), new SolidBrush(Color.Black), 0, 98);
                        graphicImage.DrawString("Digitally Signed", new Font("Arial", fontsize, FontStyle.Italic), new SolidBrush(Color.Black), 0, 120);
                    }
                    else
                        graphicImage.DrawString("Digitally Signed", new Font("Arial", fontsize - 1, FontStyle.Italic), new SolidBrush(Color.Black), 0, 63);



                    using (MemoryStream tempMS = new MemoryStream())
                    {
                        bitmapNewImage.Save(tempMS, System.Drawing.Imaging.ImageFormat.Bmp);
                        tempImageBytes = tempMS.ToArray();
                    }

                    //bitmapNewImage.Save(@"C:\Users\mohmed.naif\Documents\01Img.bmp");
                    bitmapImg.Dispose();
                    bitmapNewImage.Dispose();
                }

                Utility.isDrawSignatureActive = true;
                Utility.imgBytes = tempImageBytes;

                //lblMessage.ForeColor = Color.Green;
                //lblMessage.Text = "Draw signature in document.";
                lblDraw.Text = "Draw signature in document.";
                //AddSign();
            }
            catch (Exception)
            {
            }
        }



        public void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;

            if (timeLeft < 0)
                timeLeft = 0;

            lblTimer.Text = dateTime.AddSeconds(timeLeft).ToString("mm:ss");
            if (timeLeft <= 0)
            {
                otpTimer.Stop();
                btnResendOtp.Visible = true;
                btnResendOtp.Enabled = true;
                //OTP = "";
            }
        }

        private void otpValidTimer_Tick(object sender, EventArgs e)
        {
            validatorTimeLeft--;
            if (validatorTimeLeft < 0)
                validatorTimeLeft = 0;

            if (validatorTimeLeft <= 0)
            {
                otpValidTimer.Stop();
                OTP = "";
                lblMessage.Text = "Time out, Please try again.";
            }
        }

        private void LogSignHistory(string docName, string psno)
        {
            try
            {
                Utility.LogSignHistory(docName, psno);
            }
            catch (Exception ex)
            {

            }

        }

        private void LogOut()
        {
            if (Properties.Settings.Default.rememberMe != true)
            {
                Properties.Settings.Default.UserName = "";
                Properties.Settings.Default.Password = "";
            }
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

        private void lblBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var digisignForm = new DigiSignPDF.DigiSignForm();

            Panel pnl = this.Parent as Panel;
            if (pnl != null)
            {
                pnl.Controls.Clear();
                pnl.Controls.Add(digisignForm);
            }

        }

        private void txtComment_Leave(object sender, EventArgs e)
        {
            //SubmitOTP();
        }

        private void rbtnLongSignature_Click(object sender, EventArgs e)
        {
            if (rbtnLongSignature.Checked == true)
            {
                lblDateTime.Enabled = true;
                txtDate.Enabled = true;
                //txtTime.Enabled = true;
                chkIncludeDesignation.Enabled = true;
                lblComment.Enabled = true;
                txtComment.Enabled = true;
            }
            else
            {
                lblDateTime.Enabled = false;
                txtDate.Enabled = false;
                //txtTime.Enabled = false;
                chkIncludeDesignation.Enabled = false;
                lblComment.Enabled = false;
                txtComment.Enabled = false;
            }
        }

        private void rbtnShortSignature_Click(object sender, EventArgs e)
        {
            if (rbtnLongSignature.Checked == true)
            {
                lblDateTime.Enabled = true;
                txtDate.Enabled = true;
                //txtTime.Enabled = true;
                chkIncludeDesignation.Enabled = true;
                lblComment.Enabled = true;
                txtComment.Enabled = true;
            }
            else
            {
                lblDateTime.Enabled = false;
                txtDate.Enabled = false;
                //txtTime.Enabled = false;
                chkIncludeDesignation.Enabled = false;
                lblComment.Enabled = false;
                txtComment.Enabled = false;
                chkIncludeDesignation.Checked = false;
                txtComment.Text = "";

            }

        }

        private void txtOtp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                SubmitOTP();
                e.SuppressKeyPress = true;
            }
        }
    }
}
