using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.BouncyCastle.X509;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using System.Drawing.Drawing2D;

namespace DigiSignPDF
{
    public partial class Home : Form
    {

        Point location = Point.Empty;
        Point downLocation = Point.Empty;
        Graphics g = null;
        List<Rectangle> rectangles = new List<Rectangle>();
        
        //Structure containing each signature info
        struct DigiSign
        {
            public Bitmap bmp;  //Signature image
            public Rectangle rect;  //Signature Bounding Rectangle
            public Rectangle pageRect;  //Page rectangle on which signature is drawn
            public int pageNo;  //Page on which signature is drawn
        }
        List<DigiSign> signs = new List<DigiSign>();    //Signature info list
        int movingSign = -1;    //Currently moving signature index
        int resizeIndex = -1;   //Currently resizing index- 0:topleft, 1:topright, 2:bottomleft, 3:bottomright
        Rectangle[] resizeAnchors = new Rectangle[4];   //Rectangles containing 4 resizing anchors
        const int ANCHORSIZE = 15;   //Anchor rectangle width and height
        Point prevLocation = Point.Empty; //Used when moving. It stores previous location every time mouse moves.

        public Home()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;

            LoadHomeControls();

            this.radPdfViewer1.ViewerMode = Telerik.WinControls.UI.FixedDocumentViewerMode.None;
            this.radPdfViewer1.PageElementCreating += RadPdfViewer1_PageElementCreating;
            this.radPdfViewer1.ViewerModeChanged+= RadPdfViewer1_ViewModeChanged;
        }

        private void RadPdfViewer1_ViewModeChanged(object sender, System.EventArgs e)
        {
            radPdfViewer1.ViewerMode = Telerik.WinControls.UI.FixedDocumentViewerMode.None;
        }

        private void RadPdfViewer1_PageElementCreating(object sender, Telerik.WinControls.UI.RadFixedPageElementEventArgs e)
        {
            try
            {
                e.PageElement.ElementPainted += PageElement_ElementPainted;
                e.PageElement.MouseMove += PageElement_MouseMove;
                e.PageElement.MouseUp += PageElement_MouseUp;
            }
            catch (Exception ex)
            {
            }
        }


        //Here we deal with end of moving signature and adding signature to DigiSign list
        private void PageElement_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Telerik.WinControls.UI.RadFixedPageElement page = (sender as Telerik.WinControls.UI.RadFixedPageElement);
                Rectangle pageRect = page.ControlBoundingRectangle;

                /*If we are moving signature
                 * Init location and downLocation
                 * Init resizing index to none(-1)
                 * */
                if (movingSign != -1)
                {
                    location = Point.Empty;
                    downLocation = Point.Empty;
                    resizeIndex = -1;
                    return;
                }

                //After draw
                //System.Diagnostics.Debug.WriteLine("======================= Final ========================================");
                //System.Diagnostics.Debug.WriteLine("X--> " + location.X + " ," + downLocation.X + "   Y--> " + location.Y + " ," + downLocation.Y);

                /*Drawing rectangle of signature to be added is finished
                 * We add signature to the DigiSign list
                 * We don't use AddSignImage function bcz we stores signatures as images and draw it in PaintEvent.
                 * So autosaving in AddSignImage will not happen.
                */
                if (Utility.isDrawSignatureActive)
                {
                    var bmp = new Bitmap(new MemoryStream(Utility.imgBytes));
                    int signBoxHeight = bmp.Height - 60;
                    int signBoxWidth = bmp.Width - 70;

                    if (Utility.isSmallSign)
                    {
                        signBoxWidth = 56;
                        signBoxHeight = 59;
                    }

                    DigiSign sign;

                    sign.pageNo = page.Data.PageNo;
                    sign.bmp = ResizeImage(bmp, new Size(signBoxWidth, signBoxHeight));
                    sign.rect = new Rectangle( -pageRect.X + downLocation.X, -pageRect.Y + downLocation.Y, signBoxWidth, signBoxHeight);
                    sign.pageRect = pageRect;
                    signs.Add(sign);
                    btnRbnSave.Enabled = true;
                    btnRbnUndoSign.Enabled = true;

                    Utility.isDrawSignatureActive = false;

                    var digisignForm = new DigiSignPDF.DigiSignForm();
                    panel1.Controls.Clear();
                    panel1.Controls.Add(digisignForm);


                    //LogOut();
                }

                location = Point.Empty;
                downLocation = Point.Empty;

                this.radPdfViewer1.Invalidate();
            }
            catch (Exception ex)
            {
            }

        }

        //For Move, Resize Signature and draw rectangle of signature to be added
        private void PageElement_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //When we move without button pressed, the cursor changes for resizing when on resizing anchors
                if(Control.MouseButtons == System.Windows.Forms.MouseButtons.None && movingSign != -1)
                {
                    Telerik.WinControls.UI.RadFixedPageElement page = (sender as Telerik.WinControls.UI.RadFixedPageElement);
                    Rectangle pageRect = page.ControlBoundingRectangle;

                    Point pt = e.Location;
                    pt.Offset(-pageRect.X, -pageRect.Y);

                    int i;
                    for (i=0; i<4; i++)
                    {
                        if (resizeAnchors[i].Contains(pt))
                        {
                            if(i == 0 || i == 3)
                                Cursor.Current = Cursors.SizeNWSE;
                            else
                                Cursor.Current = Cursors.SizeNESW;
                            break;
                        }
                    }
                }

                if (Control.MouseButtons == System.Windows.Forms.MouseButtons.Left)
                {
                    if (downLocation == Point.Empty)
                    {
                        downLocation = e.Location;

                        /* If we are pressing when not adding signature
                         * See if we selected signature to move or resize anchors to resize
                         * And stores selected signature index to 'movingSign'
                         * resizing index to 'resizeIndex'
                         */
                        if (!Utility.isDrawSignatureActive)
                        {
                            Telerik.WinControls.UI.RadFixedPageElement page = (sender as Telerik.WinControls.UI.RadFixedPageElement);
                            Rectangle pageRect = page.ControlBoundingRectangle;
                            int pageNo = page.Data.PageNo;
                            int i, j;
                            Point pt = e.Location;

                            pt.Offset(new Point(-pageRect.X, -pageRect.Y));

                            //See if we selected signature. Selected index will be stored in 'i'
                            for (i = 0; i < signs.Count; i++)
                            {
                                if (signs[i].pageNo == pageNo && signs[i].rect.Contains(pt))
                                {
                                    break;
                                }
                            }

                            //See if we clicked resize anchors. Selected resizie index will be stored in 'j'
                            for (j = 0; j < 4; j++)
                            {
                                if (resizeAnchors[j].Contains(pt))
                                {
                                    resizeIndex = j;
                                    break;
                                }
                            }

                            /* If we select another signature when we currently selected one or not
                             * Change movingSign to selected index and init resize anchors rectangle
                             */
                            if (i < signs.Count && movingSign != i)
                            {
                                movingSign = i;

                                Rectangle rect = signs[movingSign].rect;
                                resizeAnchors[0] = new Rectangle(rect.X - ANCHORSIZE, rect.Y - ANCHORSIZE, ANCHORSIZE, ANCHORSIZE);
                                resizeAnchors[1] = new Rectangle(rect.Right, rect.Y - ANCHORSIZE, ANCHORSIZE, ANCHORSIZE);
                                resizeAnchors[2] = new Rectangle(rect.X - ANCHORSIZE, rect.Bottom, ANCHORSIZE, ANCHORSIZE);
                                resizeAnchors[3] = new Rectangle(rect.Right, rect.Bottom, ANCHORSIZE, ANCHORSIZE);
                            }

                            /* If we don't select anything and currently we selected one
                             * Init movingSign to none(-1) so make it none selected
                             */
                            if (i == signs.Count && j == 4 && movingSign != -1)
                                movingSign = -1;

                            prevLocation = downLocation;
                        }
                        else
                        {
                            movingSign = -1;
                        }
                    }

                    location = e.Location;

                    //If we selected signature and moving it
                    if (!Utility.isDrawSignatureActive && movingSign != -1)
                    {
                        //If we resizing
                        if (resizeIndex != -1)
                        {
                            if (resizeIndex == 0 || resizeIndex == 3)
                                Cursor.Current = Cursors.SizeNWSE;
                            else
                                Cursor.Current = Cursors.SizeNESW;

                            DigiSign sign = signs[movingSign];
                            Rectangle newRect = sign.rect;

                            if ((resizeIndex == 0 || resizeIndex == 2) && (location.X - prevLocation.X) < newRect.Width)
                            {
                                
                                newRect.X += (location.X - prevLocation.X);
                                newRect.Width -= (location.X - prevLocation.X);


                                resizeAnchors[0].Offset(location.X - prevLocation.X, 0);
                                resizeAnchors[2].Offset(location.X - prevLocation.X, 0);
                            }
                            if ((resizeIndex == 0 || resizeIndex == 1) && (location.Y - prevLocation.Y) < newRect.Height)
                            {
                                newRect.Y += (location.Y - prevLocation.Y);
                                newRect.Height -= (location.Y - prevLocation.Y);

                                resizeAnchors[0].Offset(0, location.Y - prevLocation.Y);
                                resizeAnchors[1].Offset(0, location.Y - prevLocation.Y);
                            }
                            if ((resizeIndex == 1 || resizeIndex == 3) && (prevLocation.X - location.X) < newRect.Width)
                            {
                                newRect.Width += (location.X - prevLocation.X);
                                resizeAnchors[1].Offset(location.X - prevLocation.X, 0);
                                resizeAnchors[3].Offset(location.X - prevLocation.X, 0);
                            }
                            if ((resizeIndex == 2 || resizeIndex == 3) && (prevLocation.Y - location.Y) < newRect.Height)
                            {
                                newRect.Height += (location.Y - prevLocation.Y);
                                resizeAnchors[2].Offset(0, location.Y - prevLocation.Y);
                                resizeAnchors[3].Offset(0, location.Y - prevLocation.Y);
                            }

                            sign.rect = newRect;
                            signs[movingSign] = sign;
                        }
                        /* If we are moving signature
                         * We move signature rectangle and resize anchors rectangle
                         */
                        else
                        {
                            DigiSign sign = signs[movingSign];
                            Rectangle newRect = sign.rect;

                            newRect.Offset(location.X - prevLocation.X, location.Y - prevLocation.Y);

                            for (int i = 0; i < 4; i++)
                            {
                                resizeAnchors[i].Offset(location.X - prevLocation.X, location.Y - prevLocation.Y);
                            }
                            sign.rect = newRect;
                            signs[movingSign] = sign;
                        }
                        prevLocation = location;
                    }
                    this.radPdfViewer1.Invalidate();
                }
            }
            catch (Exception ex)
            {
            }
        }

        //Draw signatures and anchors
        private void PageElement_ElementPainted(object sender, PaintEventArgs e)
        {
            try
            {
                Telerik.WinControls.UI.RadFixedPageElement page = (sender as Telerik.WinControls.UI.RadFixedPageElement);
                Rectangle pageRect = page.ControlBoundingRectangle;

                e.Graphics.ResetTransform();

                if (Utility.isDrawSignatureActive)
                {
                    SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(128, 255, 0, 0));

                    if (location != Point.Empty && downLocation != Point.Empty)
                    {
                        Rectangle r = new Rectangle(downLocation, new Size(Math.Abs(location.X - downLocation.X),
                            Math.Abs(location.Y - downLocation.Y)));
                        e.Graphics.FillRectangle(semiTransBrush, r);
                    }
                }

                for(int i=0; i<signs.Count; i++)
                {
                    if(signs[i].pageNo == page.Data.PageNo)
                    {
                        Rectangle rect = signs[i].rect;

                        rect.Offset(pageRect.X, pageRect.Y);
                        e.Graphics.DrawImage(signs[i].bmp, rect);

                        if (i == movingSign)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                Rectangle anchorRect = resizeAnchors[j];
                                SolidBrush anchorBrush = new SolidBrush(Color.FromArgb(255, 255, 136));

                                anchorRect.Offset(pageRect.X, pageRect.Y);
                                e.Graphics.FillRectangle(anchorBrush, anchorRect);
                                e.Graphics.DrawRectangle(new Pen(Color.Black, 2), anchorRect);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }



        public void LoadHomeControls()
        {
            btnRbnSettings.Enabled = false;
            btnRbnUndoSign.Enabled = false;
            btnRbnSave.Enabled = false;

            try
            {
                var getStartedUserControl = new DigiSignPDF.GetStarted();
                panel1.Controls.Add(getStartedUserControl);
            }
            catch (Exception ex)
            {
            }

        }

        private void btnRbnOpenPdf_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialogPDF.Filter = "Pdf Files|*.pdf";
                var output = openFileDialogPDF.ShowDialog();

                if (output == DialogResult.OK)
                {
                    btnRbnUndoSign.Enabled = false;
                    btnRbnSave.Enabled = false;
                    signs.Clear();
                    radPdfViewer1.LoadDocument(openFileDialogPDF.FileName);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void btnRbnSettings_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.IsLogin == true)
            {
                var manageSignatureUserControl = new DigiSignPDF.ManageSignature();
                panel1.Controls.Clear();
                panel1.Controls.Add(manageSignatureUserControl);
            }
            else
            {
                var adAuthenticationUserControl = new DigiSignPDF.ADAuthentication();
                panel1.Controls.Clear();
                panel1.Controls.Add(adAuthenticationUserControl);
            }
        }



        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogOut();
        }

        private void LogOut()
        {
            //RemoveUserCertificate();

            Properties.Settings.Default.IsLogin = false;
            Properties.Settings.Default.isOtpSubmitted = false;
            Properties.Settings.Default.Save();

            var authenticationUserControl = new DigiSignPDF.ADAuthentication();
            panel1.Controls.Clear();
            panel1.Controls.Add(authenticationUserControl);
        }

        public async System.Threading.Tasks.Task<bool> RemoveUserCertificate()
        {
            //bhadresh code start

            string userName = Properties.Settings.Default.UserName;
            string password = Properties.Settings.Default.Password;
            string fullname = Properties.Settings.Default.FullName;

            //DirectoryService directoryService = new DirectoryService();
            //string name = directoryService.GetDisplayNamefromUserID(Properties.Settings.Default.UserName);
            string name = fullname;

            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            var certificates1 = store.Certificates.Find(X509FindType.FindBySubjectName, name, true);
            X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindBySubjectName, name, true);

            if (certificates1.Count > 0)
            {
                foreach (var item in certificates)
                {
                    store.Remove(item);
                }
            }
            store.Close();
            //bhadresh code end

            return true;
        }

        private void btnRbnWord_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("winword");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }



        public void AddSignITEXT(float pageWidth, float pageHeight, float x, float y, int pageNo)
        {
            var pathPdf = "";
            string pathToSigPdf = "";
            radPdfViewer1.UnloadDocument();

            try
            {
                string fullname = Properties.Settings.Default.FullName;
                string email = Properties.Settings.Default.email;
                string password = Properties.Settings.Default.Password;
                string userName = Properties.Settings.Default.UserName;

                string pathToCert = Path.GetTempPath() + fullname + ".pfx";

                if (!File.Exists(pathToCert))
                    return;

                var pass = password.ToCharArray();
                FileStream fs;
                try
                {
                    fs = new FileStream(pathToCert, FileMode.Open);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var store = new Org.BouncyCastle.Pkcs.Pkcs12Store(fs, pass);
                fs.Close();
                var alias = "";
                foreach (string al in store.Aliases)
                    if (store.IsKeyEntry(al) && store.GetKey(al).Key.IsPrivate)
                    {
                        alias = al;
                        break;
                    }

                var pk = store.GetKey(alias);
                ICollection<Org.BouncyCastle.X509.X509Certificate> chain = store.GetCertificateChain(alias).Select(c => c.Certificate).ToList();
                var parameters = pk.Key as Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters;

                Home homeForm = (Home)this.FindForm();
                pathPdf = homeForm.openFileDialogPDF.FileName;

                if (string.IsNullOrEmpty(pathPdf))
                {
                    MessageBox.Show("No file open to sign.");
                    //lblMessage.Text = "No file open to sign.";
                    return;
                }

                pathToSigPdf = Path.GetTempPath() + Path.GetFileName(pathPdf);

                if (!File.Exists(pathPdf))
                    return;

                //File.Copy(pathPdf, Path.GetTempPath() + Path.GetFileName(pathPdf), true);
                ProcessPDF(pathPdf);

                FileStream fileStreamSigPdf;
                try
                {
                    fileStreamSigPdf = new FileStream(pathPdf, FileMode.OpenOrCreate);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    radPdfViewer1.LoadDocument(pathPdf);
                    return;
                }
                var reader = new iTextSharp.text.pdf.PdfReader(Path.GetTempPath() + Path.GetFileName(pathPdf));

                var ph = reader.GetPageSize(pageNo);
                var calculatedX = (ph.Width * x) / pageWidth;
                var calculatedY = (ph.Height * y) / pageHeight;

                var stamper = iTextSharp.text.pdf.PdfStamper.CreateSignature(reader, fileStreamSigPdf, '\0', null, true);

                //iTextSharp.text.pdf.parser.PdfReaderContentParser parser = new iTextSharp.text.pdf.parser.PdfReaderContentParser(reader);
                //iTextSharp.text.pdf.parser.TextMarginFinder finder;
                //finder = parser.ProcessContent(pageNo, new iTextSharp.text.pdf.parser.TextMarginFinder());
                //var height = finder.GetLly();

                var appearance = stamper.SignatureAppearance;
                //appearance.SignatureCreator = "Test Creator";
                //appearance.SignDate = new DateTime(Utility.signeDate.Year, Utility.signeDate.Month, Utility.signeDate.Day, Utility.signeTime.Hour, Utility.signeTime.Minute, Utility.signeTime.Second);
                //appearance.Reason = "Test Reason";
                //appearance.Contact = "Test Contact";
                //appearance.Location = "Test Location";

                int signBoxHeight = 160;
                int signBoxWidth = 200;

                if (Utility.imgBytes != null)
                {
                    Stream inputImageStream = new MemoryStream(Utility.imgBytes);
                    var bmp = new Bitmap(new MemoryStream(Utility.imgBytes));
                    signBoxHeight = bmp.Height - 60;
                    signBoxWidth = bmp.Width - 70;

                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(inputImageStream);
                    //appearance.Image = image;
                    appearance.SignatureRenderingMode = iTextSharp.text.pdf.PdfSignatureAppearance.RenderingMode.GRAPHIC;
                    appearance.SignatureGraphic = image;
                }

                if (Utility.isSmallSign)
                {
                    signBoxWidth = 56;
                    signBoxHeight = 59;
                }

                float YAxisValue = (ph.Height - (calculatedY + signBoxHeight));
                if (YAxisValue < 0)
                    YAxisValue = 0;
                if (calculatedX > ph.Width - signBoxWidth)
                    calculatedX = ph.Width - signBoxWidth;


                System.util.RectangleJ rectangleJ = new System.util.RectangleJ(calculatedX, YAxisValue, signBoxWidth, signBoxHeight);

                int signIndex = reader.AcroFields.GetSignatureNames().Count() + 1;
                appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(rectangleJ), pageNo, fullname + "-digisign" + signIndex);  //(,Top,,Left,)

                iTextSharp.text.pdf.security.IExternalSignature pks = new iTextSharp.text.pdf.security.PrivateKeySignature(parameters, iTextSharp.text.pdf.security.DigestAlgorithms.SHA256);
                iTextSharp.text.pdf.security.MakeSignature.SignDetached(appearance, pks, chain, null, null, null, 0, iTextSharp.text.pdf.security.CryptoStandard.CMS);

                fileStreamSigPdf.Close();
                reader.Close();
                stamper.Close();

                //LogOut();

                try
                {
                    LogSignHistory(Path.GetFileName(pathPdf), userName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                radPdfViewer1.LoadDocument(pathPdf);
            }
            catch (Exception ex)
            {
                radPdfViewer1.LoadDocument(pathPdf);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }




        public static void ProcessPDF(string sourcePath)
        {
            try
            {
                if (!File.Exists(sourcePath))
                    return;

                var reader = new iTextSharp.text.pdf.PdfReader(sourcePath);
                int signCount = reader.AcroFields.GetSignatureNames().Count();
                reader.Close();
                reader.Dispose();

                if (signCount >= 1)
                {
                    File.Copy(sourcePath, Path.GetTempPath() + Path.GetFileName(sourcePath), true);
                }
                else
                {
                    // Create the output document
                    PdfSharp.Pdf.PdfDocument outputDocument = new PdfSharp.Pdf.PdfDocument();

                    PdfSharp.Drawing.XGraphics gfx;

                    // Open the external documents as XPdfForm objects. Such objects are
                    // treated like images. By default the first page of the document is
                    // referenced by a new XPdfForm.
                    PdfSharp.Drawing.XPdfForm form1 = PdfSharp.Drawing.XPdfForm.FromFile(sourcePath);

                    int count = form1.PageCount;
                    for (int idx = 0; idx < count; idx++)
                    {
                        // Add two new pages to the output document
                        PdfSharp.Pdf.PdfPage page1 = outputDocument.AddPage();
                        page1.Height = form1.Height;
                        page1.Width = form1.Width;

                        if (form1.PageCount > idx)
                        {
                            // Get a graphics object for page1
                            gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(page1);
                            // Set page number (which is one-based)
                            form1.PageNumber = idx + 2;
                            // Draw the page identified by the page number like an image
                            gfx.DrawImage(form1, new PdfSharp.Drawing.XRect(0, 0, form1.PointWidth, form1.PointHeight));
                        }
                    }
                    // Save the document...
                    outputDocument.Save(Path.GetTempPath() + Path.GetFileName(sourcePath));
                    File.Copy(Path.GetTempPath() + Path.GetFileName(sourcePath), sourcePath, true);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    File.Copy(sourcePath, Path.GetTempPath() + Path.GetFileName(sourcePath), true);
                }
                catch (Exception ex1)
                {
                    throw ex1;
                }
            }



        }



        //public static void ProcePDFAgain(string sourcePath)
        //{
        //    string oldFile = sourcePath;
        //    string newFile = Path.GetTempPath() + Path.GetFileName(sourcePath);

        //    // open the reader
        //    iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(oldFile);
        //    var size = reader.GetPageSize(1);
        //    iTextSharp.text.Document document = new iTextSharp.text.Document(size);

        //    // open the writer
        //    FileStream fs = new FileStream(newFile, FileMode.Create, FileAccess.Write);
        //    iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, fs);
        //    document.Open();

        //    // the pdf content
        //    iTextSharp.text.pdf.PdfContentByte cb = writer.DirectContent;
        //    // select the font properties
        //    iTextSharp.text.pdf.BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA, iTextSharp.text.pdf.BaseFont.CP1252, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
        //    cb.SetFontAndSize(bf, 1);

        //    // write the text in the pdf content
        //    cb.BeginText();
        //    string text = "";
        //    // put the alignment and coordinates here
        //    cb.ShowTextAligned(1, text, 0, 0, 0);
        //    cb.EndText();
        //    // create the new page and add it to the pdf
        //    iTextSharp.text.pdf.PdfImportedPage page = writer.GetImportedPage(reader, 1);
        //    cb.AddTemplate(page, 0, 0);
        //    // close the streams and voilá the file should be changed :)
        //    document.Close();
        //    fs.Close();
        //    writer.Close();
        //    reader.Close();
        //}




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



        private void btnrbnPrintPDF_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(openFileDialogPDF.FileName))
            {
                MessageBox.Show("Please open pdf file to print.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //radPdfViewer1.Print();
            radPdfViewer1.Print(true);
            //radPdfViewer1.PrintPreview();
            //if (string.IsNullOrEmpty(openFileDialogPDF.FileName))
            //    return;

            //using (PrintDialog Dialog = new PrintDialog())
            //{
            //    Dialog.ShowDialog();

            //    System.Diagnostics.ProcessStartInfo printProcessInfo = new System.Diagnostics.ProcessStartInfo()
            //    {
            //        Verb = "print",
            //        CreateNoWindow = true,
            //        FileName = openFileDialogPDF.FileName,
            //        WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
            //    };

            //    System.Diagnostics.Process printProcess = new System.Diagnostics.Process();
            //    printProcess.StartInfo = printProcessInfo;
            //    printProcess.Start();

            //    printProcess.WaitForInputIdle();

            //    System.Threading.Thread.Sleep(3000);

            //    if (false == printProcess.CloseMainWindow())
            //    {
            //        printProcess.Kill();
            //    }
            //}

        }

        /* For save
         * We stores images drawn into PDF by converting Bitmap images to iTextSharp.text.Image
         */
        private void btnRbnSave_Click(object sender, EventArgs e)
        {
            radPdfViewer1.UnloadDocument();

            string pathPdf = openFileDialogPDF.FileName;
            string tempPath = Path.GetTempPath() + Path.GetFileName(pathPdf);
            
            File.Copy(pathPdf, tempPath, true);
            FileStream fs;

            try
            {
                fs = new FileStream(pathPdf, FileMode.OpenOrCreate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                radPdfViewer1.LoadDocument(pathPdf);
                return;
            }

            var reader = new iTextSharp.text.pdf.PdfReader(tempPath);
            var stamper = new iTextSharp.text.pdf.PdfStamper(reader, fs, '\0', true);

            for (int i=0; i<signs.Count; i++)
            {
                var ph = reader.GetPageSize(signs[i].pageNo);
                var rect = signs[i].rect;
                // We calculate signature's rectangle according to real page size
                var calculatedX = (ph.Width * rect.X) / signs[i].pageRect.Width;
                var calculatedY = (ph.Height * rect.Y) / signs[i].pageRect.Height;
                var calculatedWid = (ph.Width * rect.Width) / signs[i].pageRect.Width;
                var calculatedHei = (ph.Height * rect.Height) / signs[i].pageRect.Height;

                iTextSharp.text.pdf.PdfContentByte content = stamper.GetOverContent(signs[i].pageNo);
                Bitmap resizedBmp = ResizeImage(signs[i].bmp, rect.Size);
                MemoryStream ms = new MemoryStream();

                resizedBmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ms);

                calculatedY = ph.Height - (calculatedY + calculatedHei);
                
                image.SetAbsolutePosition(calculatedX, calculatedY);
                image.ScaleAbsolute(calculatedWid, calculatedHei);
                content.AddImage(image);
            }

            stamper.Close();
            fs.Close();
            reader.Close();
            
            signs.Clear();
            btnRbnSave.Enabled = false;
            btnRbnUndoSign.Enabled = false;
            radPdfViewer1.LoadDocument(pathPdf);
        }

        private void btnRbnUndoSign_Click(object sender, EventArgs e)
        {
            RemoveSignImage();
        }

        public void RemoveSign()
        {
            try
            {
                string fullname = Properties.Settings.Default.FullName;
                string pathPdf = openFileDialogPDF.FileName;

                if (!File.Exists(pathPdf))
                    return;
                File.Copy(pathPdf, Path.GetTempPath() + Path.GetFileName(pathPdf), true);
                var reader = new iTextSharp.text.pdf.PdfReader(Path.GetTempPath() + Path.GetFileName(pathPdf));
                int lastSignNo = reader.AcroFields.GetSignatureNames().Count();

                if (lastSignNo <= 0)
                {
                    reader.Close();
                    reader.Dispose();
                    return;
                }

                dynamic fieldName = null;
                if (lastSignNo > 1)
                    fieldName = reader.AcroForm.Fields.Where(x => x.Name.EndsWith("-digisign" + (lastSignNo - 1))).FirstOrDefault();
                else
                    fieldName = reader.AcroForm.Fields.Where(x => x.Name.EndsWith("-digisign" + (lastSignNo))).FirstOrDefault();

                if (fieldName != null)
                {
                    if (lastSignNo > 1 && reader.AcroForm.Fields.Where(x => x.Name.EndsWith("-digisign" + (lastSignNo))).FirstOrDefault().Name.Contains(fullname))
                    {
                        var strm = reader.AcroFields.ExtractRevision(fieldName.Name);
                        var fileStream = new FileStream(pathPdf, FileMode.Create, FileAccess.ReadWrite);
                        strm.CopyTo(fileStream);
                        fileStream.Dispose();
                    }
                    else if (lastSignNo == 1)
                    {
                        if (Convert.ToString(fieldName.Name).Contains(fullname))
                        {
                            reader.AcroFields.RemoveField(fieldName.Name);
                            byte[] newBuffer;
                            using (MemoryStream ms = new MemoryStream())
                            {
                                iTextSharp.text.pdf.PdfStamper stamp = new iTextSharp.text.pdf.PdfStamper(reader, ms);
                                stamp.Dispose();
                                newBuffer = ms.ToArray();
                                stamp.Close();
                                stamp.Dispose();
                            }
                            File.WriteAllBytes(pathPdf, newBuffer);
                        }
                    }
                }
                reader.Close();
                reader.Dispose();
                radPdfViewer1.LoadDocument(pathPdf);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }









        //Add static signature image in pdf
        public void AddSignImage(float pageWidth, float pageHeight, float x, float y, int pageNo)
        {
            var pathPdf = "";
            string pathToSigPdf = "";
            radPdfViewer1.UnloadDocument();

            try
            {
                string fullname = Properties.Settings.Default.FullName;
                string email = Properties.Settings.Default.email;
                string password = Properties.Settings.Default.Password;
                string userName = Properties.Settings.Default.UserName;


                Home homeForm = (Home)this.FindForm();
                pathPdf = homeForm.openFileDialogPDF.FileName;

                if (string.IsNullOrEmpty(pathPdf))
                {
                    MessageBox.Show("No file open to sign.");
                    //lblMessage.Text = "No file open to sign.";
                    return;
                }

                pathToSigPdf = Path.GetTempPath() + Path.GetFileName(pathPdf);

                if (!File.Exists(pathPdf))
                    return;

                //File.Copy(pathPdf, Path.GetTempPath() + Path.GetFileName(pathPdf), true);
                ProcessPDF(pathPdf);

                FileStream fileStreamSigPdf;
                try
                {
                    fileStreamSigPdf = new FileStream(pathPdf, FileMode.OpenOrCreate);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    radPdfViewer1.LoadDocument(pathPdf);
                    return;
                }
                var reader = new iTextSharp.text.pdf.PdfReader(Path.GetTempPath() + Path.GetFileName(pathPdf));

                var ph = reader.GetPageSize(pageNo);
                var calculatedX = (ph.Width * x) / pageWidth;
                var calculatedY = (ph.Height * y) / pageHeight;

                var stamper = new iTextSharp.text.pdf.PdfStamper(reader, fileStreamSigPdf, '\0', true);

                iTextSharp.text.pdf.PdfContentByte content = stamper.GetOverContent(pageNo);

                int signBoxHeight = 160;
                int signBoxWidth = 200;

                if (Utility.imgBytes != null)
                {
                    Stream inputImageStream = new MemoryStream(Utility.imgBytes);
                    var bmp = new Bitmap(new MemoryStream(Utility.imgBytes));
                    signBoxHeight = bmp.Height - 60;
                    signBoxWidth = bmp.Width - 70;

                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(inputImageStream);

                    if (Utility.isSmallSign)
                    {
                        signBoxWidth = 56;
                        signBoxHeight = 59;
                    }

                    float YAxisValue = (ph.Height - (calculatedY + signBoxHeight));
                    if (YAxisValue < 0)
                        YAxisValue = 0;
                    if (calculatedX > ph.Width - signBoxWidth)
                        calculatedX = ph.Width - signBoxWidth;


                    System.util.RectangleJ rectangleJ = new System.util.RectangleJ(calculatedX, YAxisValue, signBoxWidth, signBoxHeight);

                    int signIndex = reader.AcroFields.GetSignatureNames().Count() + 1;

                    image.SetAbsolutePosition(rectangleJ.X, rectangleJ.Y);
                    image.ScaleAbsolute(signBoxWidth, signBoxHeight);

                    //below line is for testing.
                    //iTextSharp.text.pdf.PdfImage pdfImg = new iTextSharp.text.pdf.PdfImage(image, "", null);
                    //pdfImg.Put(new iTextSharp.text.pdf.PdfName("ITXT_SigImageId"), new iTextSharp.text.pdf.PdfName("Naif2_img"));
                    //iTextSharp.text.pdf.PdfIndirectObject objRef = stamper.Writer.AddToBody(pdfImg);
                    //image.DirectReference = objRef.IndirectReference;

                    content.AddImage(image);

                    //appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(rectangleJ), pageNo, fullname + "-digisign" + signIndex);  //(,Top,,Left,)


                    stamper.Close();
                    fileStreamSigPdf.Close();
                    reader.Close();
                }
                else
                {
                    MessageBox.Show("Signature not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //LogOut();

                try
                {
                    LogSignHistory(Path.GetFileName(pathPdf), userName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                radPdfViewer1.LoadDocument(pathPdf);
            }
            catch (Exception ex)
            {
                radPdfViewer1.LoadDocument(pathPdf);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /*Resize bitmap image to the specified size
         * Parameters
         * image - Bitmap image to be resized
         * size - Desiring size
         */

        public Bitmap ResizeImage(Bitmap image, Size size)
        {
            return new Bitmap(image, size);
        }

        /* For undo
         * We delete last signature from DigiSign list
         * When there is selcted signature, we set movingSign to none(-1)
         */
        public void RemoveSignImage()
        {
            signs.RemoveAt(signs.Count - 1);

            if (signs.Count == 0)
                btnRbnUndoSign.Enabled = false;
            movingSign = -1;
            radPdfViewer1.Invalidate();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }
        /*public void RemoveSignImage()
        {
           var pathPdf = "";
           string pathToSigPdf = "";
           radPdfViewer1.UnloadDocument();

           try
           {
               string fullname = Properties.Settings.Default.FullName;
               string email = Properties.Settings.Default.email;
               string password = Properties.Settings.Default.Password;
               string userName = Properties.Settings.Default.UserName;


               Home homeForm = (Home)this.FindForm();
               pathPdf = homeForm.openFileDialogPDF.FileName;

               if (string.IsNullOrEmpty(pathPdf))
               {
                   MessageBox.Show("No file open to sign.");
                   //lblMessage.Text = "No file open to sign.";
                   return;
               }

               pathToSigPdf = Path.GetTempPath() + Path.GetFileName(pathPdf);

               if (!File.Exists(pathPdf))
                   return;

               //File.Copy(pathPdf, Path.GetTempPath() + Path.GetFileName(pathPdf), true);
               ProcessPDF(pathPdf);

               FileStream fileStreamSigPdf;
               try
               {
                   fileStreamSigPdf = new FileStream(pathPdf, FileMode.OpenOrCreate);
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   radPdfViewer1.LoadDocument(pathPdf);
                   return;
               }
               var reader = new iTextSharp.text.pdf.PdfReader(Path.GetTempPath() + Path.GetFileName(pathPdf));
               var stamper = new iTextSharp.text.pdf.PdfStamper(reader, fileStreamSigPdf, '\0', true);

               iTextSharp.text.pdf.PdfContentByte content = stamper.GetOverContent(1);



               iTextSharp.text.pdf.PdfName key = new iTextSharp.text.pdf.PdfName("ITXT_SigImageId");
               iTextSharp.text.pdf.PdfName value = new iTextSharp.text.pdf.PdfName("Naif1_img");
               iTextSharp.text.pdf.PdfObject obj;
               iTextSharp.text.pdf.PRStream stream;

               for (int i = 1; i < reader.XrefSize; i++)
               {
                   obj = reader.GetPdfObject(i);
                   if (obj == null || !obj.IsStream())
                   {
                       continue;
                   }
                   stream = (iTextSharp.text.pdf.PRStream)obj;
                   iTextSharp.text.pdf.PdfObject pdfSubtype = stream.Get(iTextSharp.text.pdf.PdfName.SUBTYPE);

                   if (pdfSubtype != null && pdfSubtype.ToString().Equals(iTextSharp.text.pdf.PdfName.IMAGE.ToString()))
                   {
                       var streamVal = stream.Get(key);
                       if (streamVal != null && value.Equals(streamVal))
                       {
                           stream.Clear();
                           iTextSharp.text.pdf.PdfReader.KillIndirect(stream);
                           //PdfReader.KillIndirect(obj);
                           //reader.RemoveUnusedObjects();
                       }
                   }
               }

               stamper.Close();
               fileStreamSigPdf.Close();
               reader.Close();

               radPdfViewer1.LoadDocument(pathPdf);
           }
           catch (Exception ex)
           {
               radPdfViewer1.LoadDocument(pathPdf);
               MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
        }*/
    }

    //Home homeForm = (Home)this.FindForm();
    //var x = homeForm.openFileDialogPDF.FileName;



}
