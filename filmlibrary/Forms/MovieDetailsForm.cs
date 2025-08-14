using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using FilmLibrary.Data;

namespace FilmLibrary.Forms
{
    public partial class MovieDetailsForm : Form
    {
        private readonly User _user;
        private readonly int _movieId;

        public MovieDetailsForm(User user, int movieId)
        {
            InitializeComponent();
            _user = user;
            _movieId = movieId;

            btnClose.Click += (_, __) => Close();
            btnOpenTrailer.Click += (_, __) =>
            {
                if (!string.IsNullOrWhiteSpace(txtTrailerId.Text))
                    Process.Start(new ProcessStartInfo($"https://www.youtube.com/watch?v={txtTrailerId.Text}") { UseShellExecute = true });
            };

            // TODO buttons:
            // btnSaveRating.Click += async ...
            // btnAddComment.Click += async ...
            // cboMyStatus.SelectedIndexChanged += async ...
            Shown += async (_, __) => await LoadDetailsAsync();
        }

        private async Task LoadDetailsAsync()
        {
            // TODO: Create a MovieDao.GetMovieDetailsAsync(...) that returns title, year, synopsis, poster_path, genres, avg rating etc.
            // For now, just stub UI:
            lblTitle.Text = "Loading...";
            // await call → fill labels, picture (poster), comments list, avg rating, etc.
        }

        private void btnSaveRating_Click(object sender, EventArgs e)
        {

        }
    }
}

