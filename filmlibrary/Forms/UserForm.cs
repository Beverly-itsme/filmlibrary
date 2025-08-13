using System;
using System.Windows.Forms;
using FilmLibrary.Data; // For User class and possible DAOs

namespace FilmLibrary.Forms
{
    public partial class UserForm : Form
    {
        private readonly User _currentUser;

        public UserForm(User user)
        {
            InitializeComponent();
            _currentUser = user;

            lblWelcome.Text = $"Welcome, {_currentUser.Username}!";

            LoadMovies();
            btnLogout.Click += BtnLogout_Click;
        }

        private void LoadMovies()
        {
            // TODO: Replace with DB call to load movies
            // For now, dummy list

            lstMovies.Items.Clear();

            lstMovies.Items.Add("The Shawshank Redemption");
            lstMovies.Items.Add("The Godfather");
            lstMovies.Items.Add("Inception");
            lstMovies.Items.Add("The Dark Knight");
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            // Close this form and show LoginForm again
            this.Close();
            Application.OpenForms["LoginForm"]?.Show();
        }
    }
}


