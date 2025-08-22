using FilmLibrary.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FilmLibrary.Data.MovieDao;

namespace FilmLibrary.Forms
{
    public partial class UserForm : Form
    {
        private readonly User _currentUser;
        private List<GenreItem> _genres = new();

        public UserForm(User user)
        {
            InitializeComponent();
            _currentUser = user;

            lblWelcome.Text = $"Welcome, {_currentUser.Username}!";

            // Wire up events
            btnSearch.Click += async (_, __) => await RefreshListAsync();
            btnClear.Click += async (_, __) => { ResetFilters(); await RefreshListAsync(); };
            lstMovies.DoubleClick += LstMovies_DoubleClick;
            btnLogout.Click += (_, __) =>
            {
                Close();
                Application.OpenForms["LoginForm"]?.Show();
            };

            Shown += async (_, __) =>
            {
                await LoadGenresAsync();
                SetupStatusFilter();
                await RefreshListAsync();
            };
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
            foreach (var g in _genres)
                cboGenre.Items.Add(g.Name); // ✅ Só adiciona nomes (display)
            cboGenre.SelectedIndex = 0;
        }

        private async Task RefreshListAsync()
        {
            // 🔹 Search term
            string searchTerm = string.IsNullOrWhiteSpace(txtSearchTitle.Text)
                ? ""
                : txtSearchTitle.Text.Trim();

            // 🔹 Genre (string? porque SearchMoviesAsync espera string, não id)
            string? genre = null;
            if (cboGenre.SelectedIndex > 0)
                genre = _genres[cboGenre.SelectedIndex - 1].Name;

            // 🔹 Status
            string? status = null;
            if (cboStatus.SelectedIndex > 0)
                status = cboStatus.SelectedItem?.ToString();

            // ✅ Chamada correta
            var items = await MovieDao.SearchMoviesAsync(
                searchTerm,        // string
                genre,             // string?
                status,            // string?
                _currentUser.Id    // int
            );

            lstMovies.DataSource = null;
            lstMovies.DisplayMember = "Title";
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
                // ✅ Agora passa o User inteiro + movieId
                var details = new MovieDetailsForm(_currentUser, row.Id);
                details.ShowDialog();
            }
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            // não precisa de lógica extra aqui
        }
    }
}






