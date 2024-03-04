namespace DigiSignPDF
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Sign = new System.Windows.Forms.ToolStripMenuItem();
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            this.ribbonTab1 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.ribbonPanel4 = new System.Windows.Forms.RibbonPanel();
            this.ribbonPanel3 = new System.Windows.Forms.RibbonPanel();
            this.ribbonPanel5 = new System.Windows.Forms.RibbonPanel();
            this.openFileDialogPDF = new System.Windows.Forms.OpenFileDialog();
            this.radPdfViewer1 = new Telerik.WinControls.UI.RadPdfViewer();
            this.btnRbnOpenPdf = new System.Windows.Forms.RibbonButton();
            this.btnRbnSettings = new System.Windows.Forms.RibbonButton();
            this.btnrbnPrintPDF = new System.Windows.Forms.RibbonButton();
            this.btnRbnUndoSign = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel();
            this.btnRbnSave = new System.Windows.Forms.RibbonButton();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPdfViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(707, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(287, 531);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(208, 505);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Version 4.2.3";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Sign});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 26);
            // 
            // Sign
            // 
            this.Sign.Name = "Sign";
            this.Sign.Size = new System.Drawing.Size(180, 22);
            this.Sign.Text = "toolStripMenuItem1";
            this.Sign.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // ribbon1
            // 
            this.ribbon1.BorderMode = System.Windows.Forms.RibbonWindowMode.NonClientAreaCustomDrawn;
            this.ribbon1.CaptionBarVisible = false;
            this.ribbon1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 72);
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.PanelCaptionHeight = 0;
            this.ribbon1.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ribbon1.Size = new System.Drawing.Size(997, 107);
            this.ribbon1.TabIndex = 2;
            this.ribbon1.Tabs.Add(this.ribbonTab1);
            this.ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Blue_2010;
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Panels.Add(this.ribbonPanel1);
            this.ribbonTab1.Panels.Add(this.ribbonPanel4);
            this.ribbonTab1.Panels.Add(this.ribbonPanel3);
            this.ribbonTab1.Panels.Add(this.ribbonPanel5);
            this.ribbonTab1.Panels.Add(this.ribbonPanel2);
            this.ribbonTab1.Text = "Home";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ButtonMoreVisible = false;
            this.ribbonPanel1.Items.Add(this.btnRbnOpenPdf);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Text = "";
            // 
            // ribbonPanel4
            // 
            this.ribbonPanel4.ButtonMoreVisible = false;
            this.ribbonPanel4.Items.Add(this.btnRbnSettings);
            this.ribbonPanel4.Name = "ribbonPanel4";
            this.ribbonPanel4.Text = "";
            this.ribbonPanel4.Visible = false;
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.ButtonMoreVisible = false;
            this.ribbonPanel3.Items.Add(this.btnrbnPrintPDF);
            this.ribbonPanel3.Name = "ribbonPanel3";
            this.ribbonPanel3.Text = "ribbonPanel3";
            // 
            // ribbonPanel5
            // 
            this.ribbonPanel5.ButtonMoreVisible = false;
            this.ribbonPanel5.Items.Add(this.btnRbnUndoSign);
            this.ribbonPanel5.Name = "ribbonPanel5";
            this.ribbonPanel5.Text = "ribbonPanel5";
            // 
            // radPdfViewer1
            // 
            this.radPdfViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radPdfViewer1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radPdfViewer1.Location = new System.Drawing.Point(2, 111);
            this.radPdfViewer1.Name = "radPdfViewer1";
            // 
            // 
            // 
            this.radPdfViewer1.RootElement.ControlBounds = new System.Drawing.Rectangle(2, 111, 703, 531);
            this.radPdfViewer1.Size = new System.Drawing.Size(703, 531);
            this.radPdfViewer1.TabIndex = 3;
            this.radPdfViewer1.ThumbnailsScaleFactor = 0.15F;
            // 
            // btnRbnOpenPdf
            // 
            this.btnRbnOpenPdf.Image = global::DigiSignPDF.Properties.Resources.reader;
            this.btnRbnOpenPdf.LargeImage = global::DigiSignPDF.Properties.Resources.reader;
            this.btnRbnOpenPdf.MinimumSize = new System.Drawing.Size(68, 0);
            this.btnRbnOpenPdf.Name = "btnRbnOpenPdf";
            this.btnRbnOpenPdf.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnRbnOpenPdf.SmallImage")));
            this.btnRbnOpenPdf.Text = "Open PDF";
            this.btnRbnOpenPdf.Click += new System.EventHandler(this.btnRbnOpenPdf_Click);
            // 
            // btnRbnSettings
            // 
            this.btnRbnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnRbnSettings.Image")));
            this.btnRbnSettings.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRbnSettings.LargeImage")));
            this.btnRbnSettings.Name = "btnRbnSettings";
            this.btnRbnSettings.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnRbnSettings.SmallImage")));
            this.btnRbnSettings.Text = "Manage Signature";
            this.btnRbnSettings.Click += new System.EventHandler(this.btnRbnSettings_Click);
            // 
            // btnrbnPrintPDF
            // 
            this.btnrbnPrintPDF.Image = ((System.Drawing.Image)(resources.GetObject("btnrbnPrintPDF.Image")));
            this.btnrbnPrintPDF.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnrbnPrintPDF.LargeImage")));
            this.btnrbnPrintPDF.MinimumSize = new System.Drawing.Size(63, 0);
            this.btnrbnPrintPDF.Name = "btnrbnPrintPDF";
            this.btnrbnPrintPDF.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnrbnPrintPDF.SmallImage")));
            this.btnrbnPrintPDF.Text = "Print PDF";
            this.btnrbnPrintPDF.Click += new System.EventHandler(this.btnrbnPrintPDF_Click);
            // 
            // btnRbnUndoSign
            // 
            this.btnRbnUndoSign.Image = global::DigiSignPDF.Properties.Resources.undo3;
            this.btnRbnUndoSign.LargeImage = global::DigiSignPDF.Properties.Resources.undo3;
            this.btnRbnUndoSign.MinimumSize = new System.Drawing.Size(70, 0);
            this.btnRbnUndoSign.Name = "btnRbnUndoSign";
            this.btnRbnUndoSign.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnRbnUndoSign.SmallImage")));
            this.btnRbnUndoSign.Text = "Undo Sign";
            this.btnRbnUndoSign.Click += new System.EventHandler(this.btnRbnUndoSign_Click);
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.ButtonMoreEnabled = false;
            this.ribbonPanel2.ButtonMoreVisible = false;
            this.ribbonPanel2.Items.Add(this.btnRbnSave);
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Text = "ribbonPanel2";
            // 
            // btnRbnSave
            // 
            this.btnRbnSave.Image = global::DigiSignPDF.Properties.Resources.save;
            this.btnRbnSave.LargeImage = global::DigiSignPDF.Properties.Resources.save;
            this.btnRbnSave.Name = "btnRbnSave";
            this.btnRbnSave.SmallImage = global::DigiSignPDF.Properties.Resources.save;
            this.btnRbnSave.Text = "Save";
            this.btnRbnSave.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Center;
            this.btnRbnSave.Click += new System.EventHandler(this.btnRbnSave_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 648);
            this.Controls.Add(this.radPdfViewer1);
            this.Controls.Add(this.ribbon1);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DigiSign PDF";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Home_FormClosing);
            this.Load += new System.EventHandler(this.Home_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radPdfViewer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Ribbon ribbon1;
        private System.Windows.Forms.RibbonTab ribbonTab1;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        public System.Windows.Forms.OpenFileDialog openFileDialogPDF;
        private System.Windows.Forms.RibbonPanel ribbonPanel4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Sign;
        private Telerik.WinControls.UI.RadPdfViewer radPdfViewer1;
        public System.Windows.Forms.RibbonButton btnRbnOpenPdf;
        private System.Windows.Forms.RibbonPanel ribbonPanel3;
        private System.Windows.Forms.RibbonButton btnrbnPrintPDF;
        public System.Windows.Forms.RibbonButton btnRbnSettings;
        private System.Windows.Forms.RibbonPanel ribbonPanel5;
        public System.Windows.Forms.RibbonButton btnRbnUndoSign;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RibbonPanel ribbonPanel2;
        public System.Windows.Forms.RibbonButton btnRbnSave;
    }
}

