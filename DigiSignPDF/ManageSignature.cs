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
using System.Net.Http;

namespace DigiSignPDF
{
    public partial class ManageSignature : UserControl
    {

        byte[] signImageFile = null;
        byte[] ShortSignImageFile = null;
        byte[] newUploadedSignature = null;
        string newUploadedSignatureExtension = "";

        public ManageSignature(byte[] imgBytes = null, byte[] imgShortSignBytes = null)
        {
            signImageFile = imgBytes;
            ShortSignImageFile = imgShortSignBytes;

            InitializeComponent();
            ddlSignatureType.SelectedIndex = 0;

            if (signImageFile != null)
            {
                pbSignatureImage.Image = ByteToImage(signImageFile);
            }
            else
            {
                BindUserDetails();
            }

            if (ShortSignImageFile == null)
            {
                BindUserDetails();
            }

        }

        private void btnUploadSignature_Click(object sender, EventArgs e)
        {
            ofdSignatureImage.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.JPG; *.JPEG; *.PNG)|*.jpg; *.jpeg; *.png; *.JPG; *.JPEG; *.PNG";
            var output = ofdSignatureImage.ShowDialog();

            if (output == DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(ofdSignatureImage.FileName);

                if (fileInfo.Length > 500000)
                {
                    btnSaveImage.Enabled = false;
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Please upload file less then 500kb.";
                    return;
                }
                else
                {
                    btnSaveImage.Enabled = true;
                    lblMessage.Text = "";
                }

                newUploadedSignature = File.ReadAllBytes(ofdSignatureImage.FileName);
                newUploadedSignatureExtension = Path.GetExtension(ofdSignatureImage.FileName);
                pbSignatureImage.Image = new Bitmap(ofdSignatureImage.FileName);
            }

        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            if (string.IsNullOrEmpty(ofdSignatureImage.FileName))
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Please upload new signature file.";
                this.Cursor = System.Windows.Forms.Cursors.Default;
                return;
            }

            FileInfo fileInfo = new FileInfo(ofdSignatureImage.FileName);

            if (fileInfo.Length > 500000)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Please upload file less then 500kb.";
                this.Cursor = System.Windows.Forms.Cursors.Default;
                return;
            }
            else
            {
                lblMessage.Text = "";
            }

            string userName = Properties.Settings.Default.UserName;
            string password = Properties.Settings.Default.Password;

            UserModel userModel = new UserModel();
            userModel.PsNo = userName;
            userModel.Password = password;
            userModel.SignImageExtension = newUploadedSignatureExtension;

            if (ddlSignatureType.SelectedIndex == 0)
            {
                userModel.SignImage = newUploadedSignature;

                var client = new RestSharp.RestClient(Utility.baseURL + "User/UpdateSignatureImage");
                var request = new RestSharp.RestRequest();
                request.Method = RestSharp.Method.POST;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddJsonBody(userModel);
                var response = client.Execute(request);
                var content = response.Content; // raw content as string  

                if (response.IsSuccessful)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Long Signature image saved successfully.";
                    BindUserDetails();
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Something went wrong.";
                }
            }
            else
            {
                userModel.SignImageSmall = newUploadedSignature;

                var client = new RestSharp.RestClient(Utility.baseURL + "User/UpdateShortSignatureImage");
                var request = new RestSharp.RestRequest();
                request.Method = RestSharp.Method.POST;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddJsonBody(userModel);
                var response = client.Execute(request);
                var content = response.Content; // raw content as string  

                if (response.IsSuccessful)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Short Signature image saved successfully.";
                    BindUserDetails();
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Something went wrong.";
                }
            }

            this.Cursor = System.Windows.Forms.Cursors.Default;

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

        private void btnLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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




        public void BindUserDetails()
        {
            try
            {
                string userName = Properties.Settings.Default.UserName;
                string password = Properties.Settings.Default.Password;

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
                        signImageFile = userModelData.SignImage;
                        pbSignatureImage.Image = ByteToImage(signImageFile);
                    }
                    if (userModelData.SignImageSmall != null)
                    {
                        ShortSignImageFile = userModelData.SignImageSmall;
                        if (ddlSignatureType.SelectedIndex == 1)
                        {
                            pbSignatureImage.Image = ByteToImage(ShortSignImageFile);
                        }
                    }

                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "User is not available in DigiSign system.\nPlease contact your administrator.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Something went wrong.";
            }

        }


        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        private void ddlSignatureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ofdSignatureImage.FileName = "";

            if (ddlSignatureType.SelectedIndex == 0)
            {
                pbSignatureImage.Location = new Point(35, 83);

                if (signImageFile != null)
                {
                    pbSignatureImage.Height = 110;
                    pbSignatureImage.Width = 208;
                    pbSignatureImage.Image = ByteToImage(signImageFile);
                }
            }
            else if (ddlSignatureType.SelectedIndex == 1)
            {
                pbSignatureImage.Location = new Point(102, 105);

                if (ShortSignImageFile != null)
                {
                    pbSignatureImage.Height = 54;
                    pbSignatureImage.Width = 76;
                    pbSignatureImage.Image = ByteToImage(ShortSignImageFile);
                }
                else
                {
                    pbSignatureImage.Image = null;
                }
            }
        }


    }
}
