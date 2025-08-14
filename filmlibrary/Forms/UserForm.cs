using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using FilmLibrary.Data;

namespace FilmLibrary.Forms
{
    public partial class UserForm : Form
    {
        private readonly User _currentUser;
        private List<Genre> _genres = new();

        public UserForm(User user)
        {
            InitializeComponent();
            _currentUser = user;

            lblWelcome.Text = $"Welcome, {_currentUser.Username}!";

            // wire events
            btnSearch.Click += async (_, __) => await RefreshListAsync();
            btnClear.Click += async (_, __) => { ResetFilters(); await RefreshListAsync(); };
            lstMovies.DoubleClick += LstMovies_DoubleClick;
            btnLogout.Click += (_, __) => { Close(); Application.OpenForms["LoginForm"]?.Show(); };

            // init filters + list
            Shown += async (_, __) => { await LoadGenresAsync(); SetupStatusFilter(); await RefreshListAsync(); };
        }

        private void SetupStatusFilter()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.Add("All");
            cboStatus.Items.Add("want_to_watch");
            cboStatus.Items.Add("watching");
            cboStatus.Items.Add("watched");
            cboStatus.SelectedIndex = 0;
        }

        private async Task LoadGenresAsync()
        {
            _genres = await MovieDao.GetGenresAsync();
            cboGenre.Items.Clear();
            cboGenre.Items.Add("All");
            foreach (var g in _genres) cboGenre.Items.Add(g);
            cboGenre.SelectedIndex = 0;
        }

        private async Task RefreshListAsync()
        {
            int? genreId = null;
            if (cboGenre.SelectedIndex > 0)
                genreId = (_genres[cboGenre.SelectedIndex - 1]).Id;

            string? status = null;
            if (cboStatus.SelectedIndex > 0)
                status = cboStatus.SelectedItem?.ToString();

            var items = await MovieDao.SearchMoviesAsync(
                _currentUser.Id,
                string.IsNullOrWhiteSpace(txtSearchTitle.Text) ? null : txtSearchTitle.Text.Trim(),
                genreId,
                status
            );

            lstMovies.DataSource = null;
            lstMovies.DisplayMember = "Title"; // shows title in list
            lstMovies.ValueMember = "Id";
            lstMovies.DataSource = items;
        }

        private void ResetFilters()
        {
            txtSearchTitle.Clear();
            if (cboGenre.Items.Count > 0) cboGenre.SelectedIndex = 0;
            if (cboStatus.Items.Count > 0) cboStatus.SelectedIndex = 0;
        }

        private void LstMovies_DoubleClick(object? sender, EventArgs e)
        {
            if (lstMovies.SelectedItem is MovieListItem row)
            {
                var details = new MovieDetailsForm(_currentUser, row.Id);
                details.ShowDialog();
            }
        }

        private void UserForm_Load(object sender, EventArgs e)
        {

        }
    }
}



