using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using FilmLibrary.Data;

namespace FilmLibrary.Forms
{
    public partial class AdminForm : Form
    {
        private readonly User _currentUser;

        public AdminForm(User user)
        {
            InitializeComponent();
            _currentUser = user;
            lblWelcome.Text = $"Welcome, Admin {_currentUser.Username}!";

            btnLogout.Click += (_, __) => { Close(); Application.OpenForms["LoginForm"]?.Show(); };
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;

            Shown += async (_, __) => await LoadMoviesAsync();
        }

        private async Task LoadMoviesAsync()
        {
            // Reuse SearchMoviesAsync without filters (userId not relevant for admin status here; pass any valid id)
            var rows = await MovieDao.SearchMoviesAsync(_currentUser.Id, null, null, null);
            dgvMovies.DataSource = rows;
            if (dgvMovies.Columns.Contains("GenresCsv")) dgvMovies.Columns["GenresCsv"].HeaderText = "Genres";
            if (dgvMovies.Columns.Contains("AvgRating")) dgvMovies.Columns["AvgRating"].HeaderText = "Avg ★";
            if (dgvMovies.Columns.Contains("UserStatus")) dgvMovies.Columns["UserStatus"].Visible = false;
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            var f = new MovieEditForm(null);
            if (f.ShowDialog() == DialogResult.OK) _ = LoadMoviesAsync();
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (dgvMovies.CurrentRow?.DataBoundItem is MovieListItem row)
            {
                var f = new MovieEditForm(row.Id);
                if (f.ShowDialog() == DialogResult.OK) _ = LoadMoviesAsync();
            }
            else MessageBox.Show("Select a movie.");
        }

        private async void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvMovies.CurrentRow?.DataBoundItem is MovieListItem row)
            {
                if (MessageBox.Show($"Delete '{row.Title}'?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    await MovieAdminDao.DeleteMovieAsync(row.Id);
                    await LoadMoviesAsync();
                }
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }
    }
}


