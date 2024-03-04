namespace DigiSignPDF
{
    partial class ManageSignature
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.btnUploadSignature = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.LinkLabel();
            this.btnBack = new System.Windows.Forms.LinkLabel();
            this.pbSignatureImage = new System.Windows.Forms.PictureBox();
            this.ofdSignatureImage = new System.Windows.Forms.OpenFileDialog();
            this.ddlSignatureType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSignatureImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(0, 299);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(278, 65);
            this.lblMessage.TabIndex = 16;
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(87)))), ((int)(((byte)(123)))));
            this.btnSaveImage.FlatAppearance.BorderSize = 0;
            this.btnSaveImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveImage.ForeColor = System.Drawing.Color.White;
            this.btnSaveImage.Location = new System.Drawing.Point(6, 245);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(268, 30);
            this.btnSaveImage.TabIndex = 15;
            this.btnSaveImage.Text = "SAVE";
            this.btnSaveImage.UseVisualStyleBackColor = false;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // btnUploadSignature
            // 
            this.btnUploadSignature.Location = new System.Drawing.Point(34, 193);
            this.btnUploadSignature.Name = "btnUploadSignature";
            this.btnUploadSignature.Size = new System.Drawing.Size(210, 23);
            this.btnUploadSignature.TabIndex = 14;
            this.btnUploadSignature.Text = "Upload Signature Image";
            this.btnUploadSignature.UseVisualStyleBackColor = true;
            this.btnUploadSignature.Click += new System.EventHandler(this.btnUploadSignature_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(87)))), ((int)(((byte)(123)))));
            this.btnLogout.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.btnLogout.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(87)))), ((int)(((byte)(123)))));
            this.btnLogout.Location = new System.Drawing.Point(7, 12);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(41, 17);
            this.btnLogout.TabIndex = 13;
            this.btnLogout.TabStop = true;
            this.btnLogout.Text = "Logout";
            this.btnLogout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnLogout_LinkClicked);
            // 
            // btnBack
            // 
            this.btnBack.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(87)))), ((int)(((byte)(123)))));
            this.btnBack.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.btnBack.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(87)))), ((int)(((byte)(123)))));
            this.btnBack.Location = new System.Drawing.Point(233, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(41, 17);
            this.btnBack.TabIndex = 12;
            this.btnBack.TabStop = true;
            this.btnBack.Text = "Back";
            this.btnBack.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnBack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnBack_LinkClicked);
            // 
            // pbSignatureImage
            // 
            this.pbSignatureImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSignatureImage.Location = new System.Drawing.Point(35, 83);
            this.pbSignatureImage.Name = "pbSignatureImage";
            this.pbSignatureImage.Size = new System.Drawing.Size(208, 110);
            this.pbSignatureImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSignatureImage.TabIndex = 11;
            this.pbSignatureImage.TabStop = false;
            // 
            // ddlSignatureType
            // 
            this.ddlSignatureType.FormattingEnabled = true;
            this.ddlSignatureType.Items.AddRange(new object[] {
            "Long Signature",
            "Short Signature"});
            this.ddlSignatureType.Location = new System.Drawing.Point(35, 44);
            this.ddlSignatureType.Name = "ddlSignatureType";
            this.ddlSignatureType.Size = new System.Drawing.Size(208, 21);
            this.ddlSignatureType.TabIndex = 17;
            this.ddlSignatureType.SelectedIndexChanged += new System.EventHandler(this.ddlSignatureType_SelectedIndexChanged);
            // 
            // ManageSignature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.ddlSignatureType);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnSaveImage);
            this.Controls.Add(this.btnUploadSignature);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.pbSignatureImage);
            this.Name = "ManageSignature";
            this.Size = new System.Drawing.Size(281, 461);
            ((System.ComponentModel.ISupportInitialize)(this.pbSignatureImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.Button btnUploadSignature;
        private System.Windows.Forms.LinkLabel btnLogout;
        private System.Windows.Forms.LinkLabel btnBack;
        private System.Windows.Forms.PictureBox pbSignatureImage;
        private System.Windows.Forms.OpenFileDialog ofdSignatureImage;
        private System.Windows.Forms.ComboBox ddlSignatureType;
    }
}
