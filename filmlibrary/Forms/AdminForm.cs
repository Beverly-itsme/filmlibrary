using FilmLibrary.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FilmLibrary.Data.MovieDao;

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

            // Make row selection predictable
            dgvMovies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovies.MultiSelect = false;
            dgvMovies.AutoGenerateColumns = true;

            Shown += async (_, __) => await LoadMoviesAsync();
        }

        private async Task LoadMoviesAsync()
        {
            // SearchMoviesAsync returns List<MovieItem>
            var rows = await MovieDao.SearchMoviesAsync("", null, null, _currentUser.Id);

            dgvMovies.DataSource = null;
            dgvMovies.DataSource = rows;

            if (dgvMovies.Columns.Contains("GenresCsv"))
                dgvMovies.Columns["GenresCsv"].HeaderText = "Genres";

            if (dgvMovies.Columns.Contains("AvgRating"))
                dgvMovies.Columns["AvgRating"].HeaderText = "Avg ★";

            if (dgvMovies.Columns.Contains("UserStatus"))
                dgvMovies.Columns["UserStatus"].Visible = false;

            // Optional: keep Id column visible for debugging, then hide if you want
            if (dgvMovies.Columns.Contains("Id"))
                dgvMovies.Columns["Id"].Visible = true;

            dgvMovies.Refresh();
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            var f = new MovieEditForm(null);
            if (f.ShowDialog() == DialogResult.OK)
                _ = LoadMoviesAsync();
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            var selectedId = GetSelectedMovieId();
            if (selectedId == null)
            {
                MessageBox.Show("⚠ Please select a movie to edit.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var f = new MovieEditForm(selectedId.Value);
            if (f.ShowDialog() == DialogResult.OK)
                _ = LoadMoviesAsync();
        }

        private async void BtnDelete_Click(object? sender, EventArgs e)
        {
            var selectedId = GetSelectedMovieId();
            if (selectedId == null)
            {
                MessageBox.Show("⚠ Please select a movie to delete.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Try to show a title if available
            var title = dgvMovies.CurrentRow?.Cells["Title"]?.Value?.ToString() ?? "this movie";
            var confirm = MessageBox.Show($"Delete '{title}'?", "Confirm Delete",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                await MovieAdminDao.DeleteMovieAsync(selectedId.Value);
                await LoadMoviesAsync();
            }
        }

        /// <summary>
        /// Safely gets the selected movie Id whether the row is a MovieItem or we have to read the Id cell.
        /// </summary>
        private int? GetSelectedMovieId()
        {
            // Case 1: bound to MovieItem (expected)
            if (dgvMovies.CurrentRow?.DataBoundItem is MovieItem mi)
                return mi.Id;

            // Case 2: try reading the "Id" cell if present
            if (dgvMovies.CurrentRow != null && dgvMovies.Columns.Contains("Id"))
            {
                var val = dgvMovies.CurrentRow.Cells["Id"]?.Value;
                if (val != null && int.TryParse(val.ToString(), out var id))
                    return id;
            }

            return null;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
        }
    }
}





