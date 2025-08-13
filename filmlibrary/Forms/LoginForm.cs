using System;
using System.Windows.Forms;
using FilmLibrary.Data;

namespace FilmLibrary.Forms
{
    public partial class LoginForm : Form
    {
        private readonly UserDao _userDao = new UserDao();

        public LoginForm()
        {
            InitializeComponent(); // Loads controls from Designer

            txtPassword.UseSystemPasswordChar = true;
            btnLogin.Click += btnLogin_Click;
            btnRegister.Click += btnRegister_Click;
        }

        private async void btnLogin_Click(object? sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            var input = txtUserOrEmail.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username/email and password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnLogin.Enabled = true;
                return;
            }

            try
            {
                var user = await _userDao.AuthenticateAsync(input, password);
                if (user != null)
                {
                    if (user.Role == "admin")
                    {
                        var adminForm = new AdminForm(user);
                        adminForm.Show();
                    }
                    else
                    {
                        var userForm = new UserForm(user);
                        userForm.Show();
                    }
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid credentials.", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        private void btnRegister_Click(object? sender, EventArgs e)
        {
            var reg = new RegisterForm();
            reg.ShowDialog();
        }
    }
}


