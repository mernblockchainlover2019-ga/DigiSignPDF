namespace DigiSignPDF
{
    partial class DigiSignForm
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
            this.components = new System.ComponentModel.Container();
            this.otpValidTimer = new System.Windows.Forms.Timer(this.components);
            this.lblMessage = new System.Windows.Forms.Label();
            this.imgSignature = new System.Windows.Forms.PictureBox();
            this.btnAddSign = new System.Windows.Forms.Button();
            this.lblLogOut = new System.Windows.Forms.LinkLabel();
            this.otpTimer = new System.Windows.Forms.Timer(this.components);
            this.lblUserProfile = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblExtraBottom = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.imgShortSignature = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgSignature)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgShortSignature)).BeginInit();
            this.SuspendLayout();
            // 
            // otpValidTimer
            // 
            this.otpValidTimer.Interval = 1000;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(3, 315);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(272, 87);
            this.lblMessage.TabIndex = 23;
            // 
            // imgSignature
            // 
            this.imgSignature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgSignature.Location = new System.Drawing.Point(45, 66);
            this.imgSignature.Name = "imgSignature";
            this.imgSignature.Size = new System.Drawing.Size(191, 91);
            this.imgSignature.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgSignature.TabIndex = 22;
            this.imgSignature.TabStop = false;
            // 
            // btnAddSign
            // 
            this.btnAddSign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(87)))), ((int)(((byte)(123)))));
            this.btnAddSign.FlatAppearance.BorderSize = 0;
            this.btnAddSign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSign.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSign.ForeColor = System.Drawing.Color.White;
            this.btnAddSign.Location = new System.Drawing.Point(6, 262);
            this.btnAddSign.Name = "btnAddSign";
            this.btnAddSign.Size = new System.Drawing.Size(261, 30);
            this.btnAddSign.TabIndex = 21;
            this.btnAddSign.Text = "ADD SIGN";
            this.btnAddSign.UseVisualStyleBackColor = false;
            this.btnAddSign.Click += new System.EventHandler(this.btnAddSign_Click);
            // 
            // lblLogOut
            // 
            this.lblLogOut.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(87)))), ((int)(((byte)(123)))));
            this.lblLogOut.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblLogOut.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(87)))), ((int)(((byte)(123)))));
            this.lblLogOut.Location = new System.Drawing.Point(228, 12);
            this.lblLogOut.Name = "lblLogOut";
            this.lblLogOut.Size = new System.Drawing.Size(45, 17);
            this.lblLogOut.TabIndex = 20;
            this.lblLogOut.TabStop = true;
            this.lblLogOut.Text = "Logout";
            this.lblLogOut.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblLogOut.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLogOut_LinkClicked);
            // 
            // otpTimer
            // 
            this.otpTimer.Interval = 1000;
            // 
            // lblUserProfile
            // 
            this.lblUserProfile.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(87)))), ((int)(((byte)(123)))));
            this.lblUserProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserProfile.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblUserProfile.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(87)))), ((int)(((byte)(123)))));
            this.lblUserProfile.Location = new System.Drawing.Point(3, 12);
            this.lblUserProfile.Name = "lblUserProfile";
            this.lblUserProfile.Size = new System.Drawing.Size(100, 17);
            this.lblUserProfile.TabIndex = 28;
            this.lblUserProfile.TabStop = true;
            this.lblUserProfile.Text = "User Profile";
            this.lblUserProfile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblUserProfile_LinkClicked);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(42, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Long Signature";
            // 
            // lblExtraBottom
            // 
            this.lblExtraBottom.AutoSize = true;
            this.lblExtraBottom.Location = new System.Drawing.Point(251, 408);
            this.lblExtraBottom.Name = "lblExtraBottom";
            this.lblExtraBottom.Size = new System.Drawing.Size(16, 13);
            this.lblExtraBottom.TabIndex = 27;
            this.lblExtraBottom.Text = "   ";
            this.lblExtraBottom.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Short Signature";
            // 
            // imgShortSignature
            // 
            this.imgShortSignature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgShortSignature.Location = new System.Drawing.Point(45, 187);
            this.imgShortSignature.Name = "imgShortSignature";
            this.imgShortSignature.Size = new System.Drawing.Size(76, 54);
            this.imgShortSignature.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgShortSignature.TabIndex = 33;
            this.imgShortSignature.TabStop = false;
            // 
            // DigiSignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.imgShortSignature);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblUserProfile);
            this.Controls.Add(this.lblExtraBottom);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.imgSignature);
            this.Controls.Add(this.btnAddSign);
            this.Controls.Add(this.lblLogOut);
            this.Name = "DigiSignForm";
            this.Size = new System.Drawing.Size(281, 461);
            ((System.ComponentModel.ISupportInitialize)(this.imgSignature)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgShortSignature)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer otpValidTimer;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox imgSignature;
        private System.Windows.Forms.Button btnAddSign;
        private System.Windows.Forms.LinkLabel lblLogOut;
        private System.Windows.Forms.Timer otpTimer;
        private System.Windows.Forms.LinkLabel lblUserProfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblExtraBottom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox imgShortSignature;
    }
}
