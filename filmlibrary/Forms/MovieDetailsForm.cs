using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using FilmLibrary.Data;

namespace FilmLibrary.Forms
{
    public partial class MovieDetailsForm : Form
    {
        private readonly int _movieId;
        private readonly User _currentUser; // ✅ Mantém o objeto User completo

        // ✅ ÚNICO construtor válido
        public MovieDetailsForm(User user, int movieId)
        {
            InitializeComponent();
            _currentUser = user;
            _movieId = movieId;
        }

        private async void MovieDetailsForm_Load(object sender, EventArgs e)
        {
            // ✅ Busca detalhes do filme com o userId correto
            var details = await MovieDao.GetMovieDetailsAsync(_movieId, _currentUser.Id);
            if (details != null)
            {
                lblTitle.Text = details.Title;
                lblYear.Text = $"Year: {details.Year?.ToString() ?? "-"}";
                lblGenres.Text = $"Genres: {details.GenresCsv}";
                txtSynopsis.Text = details.Synopsis;
                lblAvgRating.Text = $"Avg: {details.AvgRating?.ToString("0.00") ?? "-"} ★";
                cboMyStatus.SelectedItem = details.MyStatus ?? "";
                numMyRating.Value = details.MyRating ?? 3;
                txtTrailerId.Text = details.TrailerId ?? "";

                if (!string.IsNullOrEmpty(details.PosterPath))
                    picPoster.Load(details.PosterPath);
            }

            await LoadCommentsAsync();
        }

        private async Task LoadCommentsAsync()
        {
            lstComments.Items.Clear();
            var comments = await MovieDao.GetCommentsAsync(_movieId);
            foreach (var comment in comments)
                lstComments.Items.Add(comment.ToString());
        }

        private async void btnSaveRating_Click(object sender, EventArgs e)
        {
            await MovieDao.UpsertRatingAsync(_currentUser.Id, _movieId, (int)numMyRating.Value);
            await MovieDao.SetUserStatusAsync(_currentUser.Id, _movieId, cboMyStatus.Text);

            MessageBox.Show("Your rating/status has been saved.",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnAddComment_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewComment.Text))
            {
                await MovieDao.AddCommentAsync(_currentUser.Id, _movieId, txtNewComment.Text);
                txtNewComment.Clear();
                await LoadCommentsAsync();
            }
        }

        private void btnOpenTrailer_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTrailerId.Text))
            {
                var url = $"https://www.youtube.com/watch?v={txtTrailerId.Text}";
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
