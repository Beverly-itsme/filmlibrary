using System;
using System.Windows.Forms;
using FilmLibrary.Data;

namespace FilmLibrary.Forms
{
    public partial class MovieEditForm : Form
    {
        private readonly int? _movieId;

        // ctor for Add
        public MovieEditForm(int? movieId)
        {
            InitializeComponent();
            _movieId = movieId;

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (_, __) => DialogResult = DialogResult.Cancel;

            if (_movieId == null) Text = "Add Movie";
            else { Text = "Edit Movie"; /* TODO: load movie into fields */ }
        }

        private async void BtnSave_Click(object? sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            int? year = int.TryParse(txtYear.Text, out var y) ? y : (int?)null;
            string synopsis = txtSynopsis.Text.Trim();
            string poster = txtPoster.Text.Trim();
            string trailer = txtTrailer.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Title is required.");
                return;
            }

            if (_movieId == null)
                await MovieAdminDao.AddMovieAsync(title, year, synopsis, poster, trailer);
            else
                await MovieAdminDao.UpdateMovieAsync(_movieId.Value, title, year, synopsis, poster, trailer);

            DialogResult = DialogResult.OK;
        }
    }
}

