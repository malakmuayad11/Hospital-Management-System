namespace Hospital_System
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlRequiredTextBoxPassword = new Hospital_System.ctrlRequiredTextBox();
            this.ctrlRequiredTextBoxUsername = new Hospital_System.ctrlRequiredTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala Text", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(135, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(347, 37);
            this.label1.TabIndex = 4;
            this.label1.Text = "Welcome to Clinic System";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.chkRememberMe);
            this.panel1.Controls.Add(this.ctrlRequiredTextBoxPassword);
            this.panel1.Controls.Add(this.ctrlRequiredTextBoxUsername);
            this.panel1.Location = new System.Drawing.Point(90, 152);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(423, 235);
            this.panel1.TabIndex = 5;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(81, 190);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(273, 23);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // chkRememberMe
            // 
            this.chkRememberMe.AutoSize = true;
            this.chkRememberMe.Location = new System.Drawing.Point(81, 136);
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Size = new System.Drawing.Size(98, 17);
            this.chkRememberMe.TabIndex = 2;
            this.chkRememberMe.Text = "Remember Me.";
            this.chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(523, -2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 45);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlRequiredTextBoxPassword
            // 
            this.ctrlRequiredTextBoxPassword.Location = new System.Drawing.Point(81, 106);
            this.ctrlRequiredTextBoxPassword.Multiline = false;
            this.ctrlRequiredTextBoxPassword.Name = "ctrlRequiredTextBoxPassword";
            this.ctrlRequiredTextBoxPassword.PasswordChar = '\0';
            this.ctrlRequiredTextBoxPassword.Size = new System.Drawing.Size(273, 23);
            this.ctrlRequiredTextBoxPassword.TabIndex = 1;
            this.ctrlRequiredTextBoxPassword.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlRequiredTextBoxPassword_Validating);
            // 
            // ctrlRequiredTextBoxUsername
            // 
            this.ctrlRequiredTextBoxUsername.Location = new System.Drawing.Point(81, 35);
            this.ctrlRequiredTextBoxUsername.Multiline = false;
            this.ctrlRequiredTextBoxUsername.Name = "ctrlRequiredTextBoxUsername";
            this.ctrlRequiredTextBoxUsername.PasswordChar = '\0';
            this.ctrlRequiredTextBoxUsername.Size = new System.Drawing.Size(273, 23);
            this.ctrlRequiredTextBoxUsername.TabIndex = 0;
            this.ctrlRequiredTextBoxUsername.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlRequiredTextBoxUsername_Validating);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(597, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private ctrlRequiredTextBox ctrlRequiredTextBoxUsername;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private ctrlRequiredTextBox ctrlRequiredTextBoxPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}