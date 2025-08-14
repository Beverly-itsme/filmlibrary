using System.Windows.Forms;
using System.ComponentModel;

namespace FilmLibrary.Forms
{
    partial class MovieDetailsForm
    {
        private IContainer components = null;
        private Label lblTitle;
        private Label lblGenres;
        private Label lblYear;
        private TextBox txtSynopsis;
        private PictureBox picPoster;
        private Label lblAvgRating;
        private ComboBox cboMyStatus;
        private NumericUpDown numMyRating;
        private Button btnSaveRating;
        private ListBox lstComments;
        private TextBox txtNewComment;
        private Button btnAddComment;
        private TextBox txtTrailerId;
        private Button btnOpenTrailer;
        private Button btnClose;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblGenres = new Label();
            lblYear = new Label();
            txtSynopsis = new TextBox();
            picPoster = new PictureBox();
            lblAvgRating = new Label();
            cboMyStatus = new ComboBox();
            numMyRating = new NumericUpDown();
            btnSaveRating = new Button();
            lstComments = new ListBox();
            txtNewComment = new TextBox();
            btnAddComment = new Button();
            txtTrailerId = new TextBox();
            btnOpenTrailer = new Button();
            btnClose = new Button();
            ((ISupportInitialize)picPoster).BeginInit();
            ((ISupportInitialize)numMyRating).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(38, 20);
            lblTitle.TabIndex = 14;
            lblTitle.Text = "Title";
            // 
            // lblGenres
            // 
            lblGenres.AutoSize = true;
            lblGenres.Location = new Point(12, 50);
            lblGenres.Name = "lblGenres";
            lblGenres.Size = new Size(67, 20);
            lblGenres.TabIndex = 12;
            lblGenres.Text = "Genres: -";
            // 
            // lblYear
            // 
            lblYear.AutoSize = true;
            lblYear.Location = new Point(12, 30);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(50, 20);
            lblYear.TabIndex = 13;
            lblYear.Text = "Year: -";
            // 
            // txtSynopsis
            // 
            txtSynopsis.Location = new Point(190, 75);
            txtSynopsis.Multiline = true;
            txtSynopsis.Name = "txtSynopsis";
            txtSynopsis.ReadOnly = true;
            txtSynopsis.Size = new Size(360, 120);
            txtSynopsis.TabIndex = 10;
            // 
            // picPoster
            // 
            picPoster.Location = new Point(15, 75);
            picPoster.Name = "picPoster";
            picPoster.Size = new Size(160, 220);
            picPoster.SizeMode = PictureBoxSizeMode.Zoom;
            picPoster.TabIndex = 11;
            picPoster.TabStop = false;
            // 
            // lblAvgRating
            // 
            lblAvgRating.AutoSize = true;
            lblAvgRating.Location = new Point(190, 200);
            lblAvgRating.Name = "lblAvgRating";
            lblAvgRating.Size = new Size(48, 20);
            lblAvgRating.TabIndex = 9;
            lblAvgRating.Text = "Avg: -";
            // 
            // cboMyStatus
            // 
            cboMyStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMyStatus.Location = new Point(190, 225);
            cboMyStatus.Name = "cboMyStatus";
            cboMyStatus.Size = new Size(160, 28);
            cboMyStatus.TabIndex = 8;
            // 
            // numMyRating
            // 
            numMyRating.Location = new Point(356, 225);
            numMyRating.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numMyRating.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMyRating.Name = "numMyRating";
            numMyRating.Size = new Size(120, 27);
            numMyRating.TabIndex = 7;
            numMyRating.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // btnSaveRating
            // 
            btnSaveRating.Location = new Point(482, 226);
            btnSaveRating.Name = "btnSaveRating";
            btnSaveRating.Size = new Size(75, 28);
            btnSaveRating.TabIndex = 6;
            btnSaveRating.Text = "Save ★";
            btnSaveRating.Click += btnSaveRating_Click;
            // 
            // lstComments
            // 
            lstComments.Location = new Point(190, 260);
            lstComments.Name = "lstComments";
            lstComments.Size = new Size(360, 104);
            lstComments.TabIndex = 5;
            // 
            // txtNewComment
            // 
            txtNewComment.Location = new Point(196, 381);
            txtNewComment.Name = "txtNewComment";
            txtNewComment.Size = new Size(280, 27);
            txtNewComment.TabIndex = 4;
            // 
            // btnAddComment
            // 
            btnAddComment.Location = new Point(476, 385);
            btnAddComment.Name = "btnAddComment";
            btnAddComment.Size = new Size(75, 39);
            btnAddComment.TabIndex = 3;
            btnAddComment.Text = "Comment";
            // 
            // txtTrailerId
            // 
            txtTrailerId.Location = new Point(15, 305);
            txtTrailerId.Name = "txtTrailerId";
            txtTrailerId.PlaceholderText = "YouTube ID";
            txtTrailerId.Size = new Size(160, 27);
            txtTrailerId.TabIndex = 2;
            // 
            // btnOpenTrailer
            // 
            btnOpenTrailer.Location = new Point(12, 338);
            btnOpenTrailer.Name = "btnOpenTrailer";
            btnOpenTrailer.Size = new Size(75, 33);
            btnOpenTrailer.TabIndex = 1;
            btnOpenTrailer.Text = "Open Trailer";
            // 
            // btnClose
            // 
            btnClose.Location = new Point(475, 430);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 33);
            btnClose.TabIndex = 0;
            btnClose.Text = "Close";
            // 
            // MovieDetailsForm
            // 
            ClientSize = new Size(690, 627);
            Controls.Add(btnClose);
            Controls.Add(btnOpenTrailer);
            Controls.Add(txtTrailerId);
            Controls.Add(btnAddComment);
            Controls.Add(txtNewComment);
            Controls.Add(lstComments);
            Controls.Add(btnSaveRating);
            Controls.Add(numMyRating);
            Controls.Add(cboMyStatus);
            Controls.Add(lblAvgRating);
            Controls.Add(txtSynopsis);
            Controls.Add(picPoster);
            Controls.Add(lblGenres);
            Controls.Add(lblYear);
            Controls.Add(lblTitle);
            Name = "MovieDetailsForm";
            Text = "Movie Details";
            ((ISupportInitialize)picPoster).EndInit();
            ((ISupportInitialize)numMyRating).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
