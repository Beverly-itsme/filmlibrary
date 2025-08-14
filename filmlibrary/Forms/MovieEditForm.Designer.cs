using System.Windows.Forms;
using System.ComponentModel;

namespace FilmLibrary.Forms
{
    partial class MovieEditForm
    {
        private IContainer components = null;
        private TextBox txtTitle;
        private TextBox txtYear;
        private TextBox txtSynopsis;
        private TextBox txtPoster;
        private TextBox txtTrailer;
        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            txtTitle = new TextBox();
            txtYear = new TextBox();
            txtSynopsis = new TextBox();
            txtPoster = new TextBox();
            txtTrailer = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(12, 12);
            txtTitle.Name = "txtTitle";
            txtTitle.PlaceholderText = "Title *";
            txtTitle.Size = new Size(320, 27);
            txtTitle.TabIndex = 6;
            // 
            // txtYear
            // 
            txtYear.Location = new Point(12, 41);
            txtYear.Name = "txtYear";
            txtYear.PlaceholderText = "Year";
            txtYear.Size = new Size(100, 27);
            txtYear.TabIndex = 5;
            // 
            // txtSynopsis
            // 
            txtSynopsis.Location = new Point(12, 128);
            txtSynopsis.Multiline = true;
            txtSynopsis.Name = "txtSynopsis";
            txtSynopsis.PlaceholderText = "Synopsis";
            txtSynopsis.Size = new Size(320, 120);
            txtSynopsis.TabIndex = 2;
            // 
            // txtPoster
            // 
            txtPoster.Location = new Point(12, 70);
            txtPoster.Name = "txtPoster";
            txtPoster.PlaceholderText = "Poster path (URL/local)";
            txtPoster.Size = new Size(320, 27);
            txtPoster.TabIndex = 4;
            // 
            // txtTrailer
            // 
            txtTrailer.Location = new Point(12, 99);
            txtTrailer.Name = "txtTrailer";
            txtTrailer.PlaceholderText = "YouTube ID";
            txtTrailer.Size = new Size(320, 27);
            txtTrailer.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(176, 257);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 31);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(257, 257);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 27);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            // 
            // MovieEditForm
            // 
            ClientSize = new Size(344, 292);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtSynopsis);
            Controls.Add(txtTrailer);
            Controls.Add(txtPoster);
            Controls.Add(txtYear);
            Controls.Add(txtTitle);
            Name = "MovieEditForm";
            Text = "Movie";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
