using System;
using System.Windows.Forms;
using FilmLibrary.Data; // Your User class and DAO

namespace FilmLibrary.Forms
{
    public partial class AdminForm : Form
    {
        private readonly User _currentUser;

        public AdminForm(User user)
        {
            InitializeComponent();
            _currentUser = user;

            lblWelcome.Text = $"Welcome, {_currentUser.Username}!";

            LoadMovies();
            btnLogout.Click += BtnLogout_Click;
        }

        private void LoadMovies()
        {
            // Replace this with real DB call later
            lstMovies.Items.Clear();

            lstMovies.Items.Add("The Shawshank Redemption");
            lstMovies.Items.Add("The Godfather");
            lstMovies.Items.Add("Inception");
            lstMovies.Items.Add("The Dark Knight");
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.OpenForms["LoginForm"]?.Show();
        }
    }
}

