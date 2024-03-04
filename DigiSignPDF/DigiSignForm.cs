using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;

namespace DigiSignPDF
{
    public partial class DigiSignForm : UserControl
    {
        byte[] imgBytes = null;
        byte[] smallSignImgBytes = null;
        string OTP = "";
        int timeLeft = 0;
        int validatorTimeLeft = 0;
        DateTime dateTime = new DateTime();

        public DigiSignForm()
        {
            InitializeComponent();

            BindUserDetails();
        }


        public void BindUserDetails()
        {
            try
            {
                string userName = Properties.Settings.Default.UserName;
                string password = Properties.Settings.Default.Password;

                string fullname = Properties.Settings.Default.FullName;
                string mobile = Properties.Settings.Default.mobileNo;
                string email = Properties.Settings.Default.email;
                string departmentName = Properties.Settings.Default.departmentName;
                string designation = Properties.Settings.Default.designation;

                //lblFullname.Text = fullname;
                //lblEmail.Text = email;
                //lblPhone.Text = mobile;
                //if (departmentName != "0")
                //    lblDepartment.Text = departmentName;

                UserModel userModelData = new UserModel();
                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri(Utility.baseURL);
                //    var responseTask = client.GetAsync("User/GetUserDetailsByPsNo?PsNo=" + userName + "&password=" + System.Web.HttpUtility.UrlEncode(password));
                //    responseTask.Wait();
                //    var result = responseTask.Result;
                //    if (result.IsSuccessStatusCode)
                //    {
                //        var readTask = result.Content.ReadAsAsync<UserModel>();
                //        readTask.Wait();

                //        userModelData = readTask.Result;
                //    }
                //}

                UserCredential userCredential = new UserCredential();
                userCredential.PsNo = userName;
                userCredential.Password = password;

                var client = new RestSharp.RestClient(Utility.baseURL + "User/GetUserDetailsByCredential");
                var request = new RestSharp.RestRequest();

                request.Method = RestSharp.Method.POST;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddJsonBody(userCredential);

                var response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    userModelData = Newtonsoft.Json.JsonConvert.DeserializeObject<UserModel>(response.Content);
                }


                if (userModelData.Id != 0)
                {
                    if (userModelData.SignImage != null)
                    {
                        imgBytes = userModelData.SignImage;
                        imgSignature.Image = ByteToImage(imgBytes);
                    }

                    if (userModelData.SignImageSmall != null)
                    {
                        smallSignImgBytes = userModelData.SignImageSmall;
                        imgShortSignature.Image = ByteToImage(smallSignImgBytes);
                    }

                    //GetUserCertificate();
                }
                else
                {
                    /*----------------- Added By Freelancer -----------------------------*/
                    imgBytes = ImageToByteArray(global::DigiSignPDF.Properties.Resources.logo_blue);
                    imgSignature.Image = ByteToImage(imgBytes);
                    smallSignImgBytes = ImageToByteArray(global::DigiSignPDF.Properties.Resources.logo_blue);
                    imgShortSignature.Image = ByteToImage(smallSignImgBytes);
                    /*-------------------------------------------------------------------*/

                    lblMessage.Text = "User is not available in DigiSign system.\nPlease contact your administrator.";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = "Something went wrong.";
            }

        }



        /*----------------- Added By Freelancer -----------------------------*/
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
        /*-------------------------------------------------------------------*/


        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        private void btnAddSign_Click(object sender, EventArgs e)
        {
            if (imgBytes == null)
            {
                lblMessage.Text = "Your signature image is not available in system. Please upload image signature from setting -> Manage Signature menu.";
            }
            else
            {
                Home homeForm = (Home)this.FindForm();
                var pathPdf = homeForm.openFileDialogPDF.FileName;
                if (string.IsNullOrEmpty(pathPdf))
                {
                    lblMessage.Text = "No file open to sign.";
                    return;
                }

                var addSignatureForm = new DigiSignPDF.AddSignatureForm(imgBytes, smallSignImgBytes);

                Panel pnl = this.Parent as Panel;
                if (pnl != null)
                {
                    pnl.Controls.Clear();
                    pnl.Controls.Add(addSignatureForm);
                }

                //Globals.ThisAddIn.addSignatureForm = new AddSignatureForm(imgBytes);
                //Utility.RemoveTaskpan();
                //Globals.ThisAddIn.taskPaneValue = Globals.ThisAddIn.CustomTaskPanes.Add(Globals.ThisAddIn.addSignatureForm, "Sign Document");
                //Globals.ThisAddIn.taskPaneValue.Visible = true;
                //Globals.ThisAddIn.taskPaneValue.Width = 300;
            }
        }

        private void lblLogOut_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void lblUserProfile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Home homeForm = (Home)this.FindForm();
            var userProfileUserControl = new DigiSignPDF.UserProfile();

            Panel pnl = this.Parent as Panel;
            if (pnl != null)
            {
                pnl.Controls.Clear();
                pnl.Controls.Add(userProfileUserControl);
            }
        }




    }

}
