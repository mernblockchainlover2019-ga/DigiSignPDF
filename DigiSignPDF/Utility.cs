using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace DigiSignPDF
{
    public class Utility
    {
        public static string baseURL = "https://ecom.lthed.com/portal/DigisignAPIPublishQA/api/";
        public static string baseWebURL = "https://ecom.lthed.com/portal/DigisignAPIPublishQA/";

        //public static string baseURL = "http://localhost:60304/api/";
        //public static string baseWebURL = "http://localhost:60304/";


        //Properties are used for AddSignatureForm usercontrol to home form
        public static bool isDrawSignatureActive = false;
        public static bool isSmallSign = false;
        public static DateTime signeDate = DateTime.Now;
        public static DateTime signeTime = DateTime.Now;
        public static byte[] imgBytes = null;
        //==================================================================





        public static bool SendOTP(string phone, string emailTo, string otp)
        {
            try
            {
                //phone = "8347617586";
                //emailTo = "";

                OTP objOtp = new OTP();
                objOtp.Phone = phone;
                objOtp.EmailTo = emailTo;
                objOtp.Otp = otp;

                var client = new RestSharp.RestClient(baseURL + "User/SendOTPPhoneEmail");
                var request = new RestSharp.RestRequest();

                request.Method = RestSharp.Method.POST;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddJsonBody(objOtp);
                var response = client.Execute(request);
                var content = response.Content; // raw content as string  

                if (response.IsSuccessful)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static async System.Threading.Tasks.Task<bool> LogSignHistory(string docName, string psno)
        {
            try
            {
                SignatureHistory signatureHistory = new SignatureHistory();
                signatureHistory.DocumentName = docName;
                signatureHistory.PsNo = psno;

                string Json = Newtonsoft.Json.JsonConvert.SerializeObject(signatureHistory);
                var client = new RestSharp.RestClient(baseURL + "SignatureHistory/SignatureHistoryCreate");
                var request = new RestSharp.RestRequest();

                request.Method = RestSharp.Method.POST;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddJsonBody(signatureHistory);
                var response = client.Execute(request);
                var content = response.Content;


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        public static bool GenerateUserCertificate()
        {
            try
            {
                UserModel userModelData = new UserModel();
                UserCredential userCredential = new UserCredential();
                userCredential.PsNo = Properties.Settings.Default.UserName;
                userCredential.Password = Properties.Settings.Default.Password;
                //userCredential.Password = "165484";

                var client = new RestSharp.RestClient(Utility.baseURL + "User/GenerateUserCertificateByCredential");
                var request = new RestSharp.RestRequest();

                request.Method = RestSharp.Method.POST;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddJsonBody(userCredential);

                var response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(response.Content);
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void GetUserCertificate()
        {
            try
            {
                UserModel userModelData = new UserModel();
                string userName = Properties.Settings.Default.UserName;
                string password = Properties.Settings.Default.Password;
                string fullname = Properties.Settings.Default.FullName;

                UserCredential userCredential = new UserCredential();
                userCredential.PsNo = Properties.Settings.Default.UserName;
                userCredential.Password = Properties.Settings.Default.Password;

                var client = new RestSharp.RestClient(Utility.baseURL + "User/GetUserCertificateByCredential");
                var request = new RestSharp.RestRequest();

                request.Method = RestSharp.Method.POST;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddJsonBody(userCredential);

                var response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    userModelData = Newtonsoft.Json.JsonConvert.DeserializeObject<UserModel>(response.Content);

                    if (userModelData.UserCertificateFile != null)
                    {
                        string filePath = Path.GetTempPath() + fullname + ".pfx";
                        WebClient webClient = new WebClient();
                        webClient.DownloadFile(Utility.baseWebURL + "UserCertificates/" + Properties.Settings.Default.FullName + ".pfx", filePath);

                        //password = "165484";
                        X509Certificate2 x509Certificate2 = new X509Certificate2(filePath, password, X509KeyStorageFlags.PersistKeySet);

                        X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                        store.Open(OpenFlags.ReadWrite);
                        var certificates = store.Certificates.Find(X509FindType.FindBySubjectName, fullname, true);


                        if (certificates.Count == 0)
                        {
                            string file = filePath;
                            //X509Certificate2 x509Certificate2 = new X509Certificate2(file, password, X509KeyStorageFlags.PersistKeySet);
                            store.Add(x509Certificate2);
                            store.Close();
                        }
                        //File.Delete(filePath);
                    }
                    else
                    {
                        Utility.GenerateUserCertificate();
                        GetUserCertificate();
                    }
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    Utility.GenerateUserCertificate();
                    GetUserCertificate();
                }


            }
            catch (Exception ex)
            {
                Utility.GenerateUserCertificate();
                GetUserCertificate();
                //lblMessage.Text = "Certificate can't fetch from server.";
            }
        }


        public static Tuple<int, int, int, int> GetNextSignLocation(int nextIndex, int height)
        {
            Tuple<int, int, int, int> location = new Tuple<int, int, int, int>(215, height, 20, height - 80);  //(,Top,,Left,)

            switch (nextIndex)
            {
                case 1:
                    location = new Tuple<int, int, int, int>(215, height, 20, height - 80);
                    break;
                case 2:
                    location = new Tuple<int, int, int, int>(410, height, 210, height - 80);
                    break;
                case 3:
                    location = new Tuple<int, int, int, int>(600, height, 405, height - 80);
                    break;
                case 4:
                    location = new Tuple<int, int, int, int>(215, height - 80, 20, height - 160);
                    break;
                case 5:
                    location = new Tuple<int, int, int, int>(410, height - 80, 210, height - 160);
                    break;
                case 6:
                    location = new Tuple<int, int, int, int>(600, height - 80, 405, height - 160);
                    break;

                case 7:
                    location = new Tuple<int, int, int, int>(215, height - 160, 20, height - 240);
                    break;

                case 8:
                    location = new Tuple<int, int, int, int>(410, height - 160, 210, height - 240);
                    break;

                case 9:
                    location = new Tuple<int, int, int, int>(600, height - 160, 405, height - 240);
                    break;



                //case 1:
                //    location = new Tuple<int, int, int, int>(215, 200, 20, 120);
                //    break;
                //case 2:
                //    location = new Tuple<int, int, int, int>(410, 200, 210, 120);
                //    break;
                //case 3:
                //    location = new Tuple<int, int, int, int>(600, 200, 405, 120);
                //    break;
                //case 4:
                //    location = new Tuple<int, int, int, int>(215, 120, 20, 40);
                //    break;
                //case 5:
                //    location = new Tuple<int, int, int, int>(410, 120, 210, 40);
                //    break;
                //case 6:
                //    location = new Tuple<int, int, int, int>(600, 120, 405, 40);
                //    break;

                //case 7:
                //    location = new Tuple<int, int, int, int>(215, 40, 20, 0);
                //    break;

                //case 8:
                //    location = new Tuple<int, int, int, int>(410, 40, 210, 0);
                //    break;

                //case 9:
                //    location = new Tuple<int, int, int, int>(600, 40, 405, 0);
                //    break;



                default:
                    break;
            }

            return location;
        }



    }

    static class StringExtensions
    {
        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
        {
            int i = 0;
            while (i + partLength < s.Length)
            {
                int index = s.LastIndexOf(' ', i + partLength);
                if (index <= 0) //if word length > maxLength.
                {
                    index = partLength;
                }
                yield return s.Substring(i, index - i);

                i = index + 1;
            }

            yield return s.Substring(i);
        }

    }



    public class OTP
    {
        public string Phone { get; set; }
        public string EmailTo { get; set; }
        public string Otp { get; set; }
    }

    public class SignatureHistory
    {
        public long Id { get; set; }
        public string DocumentName { get; set; }
        public Nullable<System.DateTime> SignatureDate { get; set; }
        public string SignatureBy { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<decimal> Mobile { get; set; }
        public string PsNo { get; set; }

    }

    public class UserModel
    {
        public int Id { get; set; }
        public string PsNo { get; set; }
        public string SignImageName { get; set; }
        public byte[] SignImage { get; set; }
        public byte[] SignImageSmall { get; set; }

        public byte[] UserCertificateFile { get; set; }
        public string FullName { get; set; }
        public string FullPathCertificate { get; set; }
        public bool isSignUploaded { get; set; }
        public bool isCertificateAvailable { get; set; }

        public string Password { get; set; }
        public string SignImageExtension { get; set; }

    }



    public class UserCredential
    {
        public string PsNo { get; set; }
        public string Password { get; set; }
    }
}
