namespace DigiSignPDF
{
    partial class AddSignatureForm
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
            this.lblMobileNo = new System.Windows.Forms.Label();
            this.lblOTP = new System.Windows.Forms.Label();
            this.otpTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTimer = new System.Windows.Forms.Label();
            this.lblBack = new System.Windows.Forms.LinkLabel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnResendOtp = new System.Windows.Forms.Button();
            this.btnSubmitOtp = new System.Windows.Forms.Button();
            this.otpValidTimer = new System.Windows.Forms.Timer(this.components);
            this.lblDraw = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.chkIncludeDesignation = new System.Windows.Forms.CheckBox();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.txtOtp = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbtnWithoutDate = new System.Windows.Forms.RadioButton();
            this.rbtnWithDate = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbtnShortSignature = new System.Windows.Forms.RadioButton();
            this.rbtnLongSignature = new System.Windows.Forms.RadioButton();
            this.txtDate = new DigiSignPDF.BCDateTimePicker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMobileNo
            // 
            this.lblMobileNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMobileNo.Location = new System.Drawing.Point(7, 46);
            this.lblMobileNo.Name = "lblMobileNo";
            this.lblMobileNo.Size = new System.Drawing.Size(265, 23);
            this.lblMobileNo.TabIndex = 29;
            // 
            // lblOTP
            // 
            this.lblOTP.AutoSize = true;
            this.lblOTP.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOTP.Location = new System.Drawing.Point(5, 307);
            this.lblOTP.Name = "lblOTP";
            this.lblOTP.Size = new System.Drawing.Size(96, 22);
            this.lblOTP.TabIndex = 24;
            this.lblOTP.Text = "Enter OTP";
            // 
            // otpTimer
            // 
            this.otpTimer.Interval = 1000;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTimer);
            this.panel1.Location = new System.Drawing.Point(1, 371);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 60);
            this.panel1.TabIndex = 22;
            // 
            // lblTimer
            // 
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.Location = new System.Drawing.Point(2, 9);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(264, 42);
            this.lblTimer.TabIndex = 1;
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBack
            // 
            this.lblBack.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(87)))), ((int)(((byte)(123)))));
            this.lblBack.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblBack.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(87)))), ((int)(((byte)(123)))));
            this.lblBack.Location = new System.Drawing.Point(233, 12);
            this.lblBack.Name = "lblBack";
            this.lblBack.Size = new System.Drawing.Size(41, 17);
            this.lblBack.TabIndex = 28;
            this.lblBack.TabStop = true;
            this.lblBack.Text = "Back";
            this.lblBack.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblBack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblBack_LinkClicked);
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(1, 508);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(268, 37);
            this.lblMessage.TabIndex = 27;
            // 
            // btnResendOtp
            // 
            this.btnResendOtp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(87)))), ((int)(((byte)(123)))));
            this.btnResendOtp.FlatAppearance.BorderSize = 0;
            this.btnResendOtp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResendOtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResendOtp.ForeColor = System.Drawing.Color.White;
            this.btnResendOtp.Location = new System.Drawing.Point(8, 449);
            this.btnResendOtp.Name = "btnResendOtp";
            this.btnResendOtp.Size = new System.Drawing.Size(261, 27);
            this.btnResendOtp.TabIndex = 26;
            this.btnResendOtp.Text = "RESEND OTP";
            this.btnResendOtp.UseVisualStyleBackColor = false;
            this.btnResendOtp.Visible = false;
            this.btnResendOtp.Click += new System.EventHandler(this.btnResendOtp_Click);
            // 
            // btnSubmitOtp
            // 
            this.btnSubmitOtp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(87)))), ((int)(((byte)(123)))));
            this.btnSubmitOtp.FlatAppearance.BorderSize = 0;
            this.btnSubmitOtp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmitOtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmitOtp.ForeColor = System.Drawing.Color.White;
            this.btnSubmitOtp.Location = new System.Drawing.Point(139, 334);
            this.btnSubmitOtp.Name = "btnSubmitOtp";
            this.btnSubmitOtp.Size = new System.Drawing.Size(130, 27);
            this.btnSubmitOtp.TabIndex = 47;
            this.btnSubmitOtp.Text = "SUBMIT";
            this.btnSubmitOtp.UseVisualStyleBackColor = false;
            this.btnSubmitOtp.Click += new System.EventHandler(this.btnSubmitOtp_Click);
            // 
            // otpValidTimer
            // 
            this.otpValidTimer.Interval = 1000;
            // 
            // lblDraw
            // 
            this.lblDraw.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDraw.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDraw.ForeColor = System.Drawing.Color.Green;
            this.lblDraw.Location = new System.Drawing.Point(2, 483);
            this.lblDraw.Name = "lblDraw";
            this.lblDraw.Size = new System.Drawing.Size(267, 20);
            this.lblDraw.TabIndex = 33;
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComment.Location = new System.Drawing.Point(5, 232);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(86, 22);
            this.lblComment.TabIndex = 34;
            this.lblComment.Text = "Comment";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(8, 259);
            this.txtComment.MaxLength = 100;
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(261, 39);
            this.txtComment.TabIndex = 45;
            this.txtComment.Leave += new System.EventHandler(this.txtComment_Leave);
            // 
            // chkIncludeDesignation
            // 
            this.chkIncludeDesignation.AutoSize = true;
            this.chkIncludeDesignation.Location = new System.Drawing.Point(10, 206);
            this.chkIncludeDesignation.Name = "chkIncludeDesignation";
            this.chkIncludeDesignation.Size = new System.Drawing.Size(120, 17);
            this.chkIncludeDesignation.TabIndex = 44;
            this.chkIncludeDesignation.Text = "Include Designation";
            this.chkIncludeDesignation.UseVisualStyleBackColor = true;
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.Location = new System.Drawing.Point(5, 79);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(96, 22);
            this.lblDateTime.TabIndex = 39;
            this.lblDateTime.Text = "Enter Date";
            // 
            // txtOtp
            // 
            this.txtOtp.BackColor = System.Drawing.Color.Gainsboro;
            this.txtOtp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtp.Location = new System.Drawing.Point(8, 334);
            this.txtOtp.MaxLength = 10;
            this.txtOtp.Multiline = true;
            this.txtOtp.Name = "txtOtp";
            this.txtOtp.Size = new System.Drawing.Size(125, 27);
            this.txtOtp.TabIndex = 46;
            this.txtOtp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtp_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbtnWithoutDate);
            this.panel2.Controls.Add(this.rbtnWithDate);
            this.panel2.Location = new System.Drawing.Point(3, 145);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(275, 25);
            this.panel2.TabIndex = 48;
            // 
            // rbtnWithoutDate
            // 
            this.rbtnWithoutDate.AutoSize = true;
            this.rbtnWithoutDate.Location = new System.Drawing.Point(141, 4);
            this.rbtnWithoutDate.Name = "rbtnWithoutDate";
            this.rbtnWithoutDate.Size = new System.Drawing.Size(88, 17);
            this.rbtnWithoutDate.TabIndex = 51;
            this.rbtnWithoutDate.TabStop = true;
            this.rbtnWithoutDate.Text = "Without Date";
            this.rbtnWithoutDate.UseVisualStyleBackColor = true;
            // 
            // rbtnWithDate
            // 
            this.rbtnWithDate.AutoSize = true;
            this.rbtnWithDate.Checked = true;
            this.rbtnWithDate.Location = new System.Drawing.Point(7, 4);
            this.rbtnWithDate.Name = "rbtnWithDate";
            this.rbtnWithDate.Size = new System.Drawing.Size(73, 17);
            this.rbtnWithDate.TabIndex = 50;
            this.rbtnWithDate.TabStop = true;
            this.rbtnWithDate.Text = "With Date";
            this.rbtnWithDate.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbtnShortSignature);
            this.panel3.Controls.Add(this.rbtnLongSignature);
            this.panel3.Location = new System.Drawing.Point(3, 175);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(275, 25);
            this.panel3.TabIndex = 49;
            // 
            // rbtnShortSignature
            // 
            this.rbtnShortSignature.AutoSize = true;
            this.rbtnShortSignature.Location = new System.Drawing.Point(141, 4);
            this.rbtnShortSignature.Name = "rbtnShortSignature";
            this.rbtnShortSignature.Size = new System.Drawing.Size(98, 17);
            this.rbtnShortSignature.TabIndex = 45;
            this.rbtnShortSignature.TabStop = true;
            this.rbtnShortSignature.Text = "Short Signature";
            this.rbtnShortSignature.UseVisualStyleBackColor = true;
            this.rbtnShortSignature.Click += new System.EventHandler(this.rbtnShortSignature_Click);
            // 
            // rbtnLongSignature
            // 
            this.rbtnLongSignature.AutoSize = true;
            this.rbtnLongSignature.Checked = true;
            this.rbtnLongSignature.Location = new System.Drawing.Point(7, 4);
            this.rbtnLongSignature.Name = "rbtnLongSignature";
            this.rbtnLongSignature.Size = new System.Drawing.Size(97, 17);
            this.rbtnLongSignature.TabIndex = 44;
            this.rbtnLongSignature.TabStop = true;
            this.rbtnLongSignature.Text = "Long Signature";
            this.rbtnLongSignature.UseVisualStyleBackColor = true;
            this.rbtnLongSignature.Click += new System.EventHandler(this.rbtnLongSignature_Click);
            // 
            // txtDate
            // 
            this.txtDate.BackColor = System.Drawing.Color.Gainsboro;
            this.txtDate.BackDisabledColor = System.Drawing.SystemColors.Control;
            this.txtDate.CustomFormat = "dd/MM/yyyy";
            this.txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDate.Location = new System.Drawing.Point(8, 111);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(261, 27);
            this.txtDate.TabIndex = 40;
            // 
            // AddSignatureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtOtp);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.chkIncludeDesignation);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.lblDraw);
            this.Controls.Add(this.lblMobileNo);
            this.Controls.Add(this.lblOTP);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblBack);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnResendOtp);
            this.Controls.Add(this.btnSubmitOtp);
            this.Name = "AddSignatureForm";
            this.Size = new System.Drawing.Size(281, 562);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMobileNo;
        private System.Windows.Forms.Label lblOTP;
        private System.Windows.Forms.Timer otpTimer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.LinkLabel lblBack;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnResendOtp;
        private System.Windows.Forms.Button btnSubmitOtp;
        private System.Windows.Forms.Timer otpValidTimer;
        private System.Windows.Forms.Label lblDraw;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.CheckBox chkIncludeDesignation;
        private System.Windows.Forms.Label lblDateTime;
        private BCDateTimePicker txtDate;
        private System.Windows.Forms.TextBox txtOtp;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbtnWithoutDate;
        private System.Windows.Forms.RadioButton rbtnWithDate;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbtnShortSignature;
        private System.Windows.Forms.RadioButton rbtnLongSignature;
    }
}
