namespace FilmLibrary.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtUserOrEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;

        private void InitializeComponent()
        {
            txtUserOrEmail = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnRegister = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // txtUserOrEmail
            // 
            txtUserOrEmail.Location = new Point(387, 98);
            txtUserOrEmail.Name = "txtUserOrEmail";
            txtUserOrEmail.Size = new Size(200, 27);
            txtUserOrEmail.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(387, 251);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(200, 27);
            txtPassword.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(294, 414);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(130, 70);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(561, 414);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(130, 70);
            btnRegister.TabIndex = 3;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(235, 105);
            label1.Name = "label1";
            label1.Size = new Size(99, 20);
            label1.TabIndex = 4;
            label1.Text = "Nome/E-mail";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(254, 258);
            label2.Name = "label2";
            label2.Size = new Size(49, 20);
            label2.TabIndex = 5;
            label2.Text = "Senha";
            // 
            // LoginForm
            // 
            ClientSize = new Size(1070, 546);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtUserOrEmail);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Controls.Add(btnRegister);
            Name = "LoginForm";
            Text = "Login";
            Load += LoginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
        private Label label1;
        private Label label2;
    }
}
