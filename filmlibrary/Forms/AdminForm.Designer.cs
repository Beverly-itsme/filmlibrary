using System.Windows.Forms;
using System.ComponentModel;

namespace FilmLibrary.Forms
{
    partial class AdminForm
    {
        private IContainer components = null;
        private Label lblWelcome;
        private ListBox lstMovies;
        private Button btnLogout;

        private void InitializeComponent()
        {
            this.lblWelcome = new Label();
            this.lstMovies = new ListBox();
            this.btnLogout = new Button();
            this.SuspendLayout();

            // Initialize lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(12, 9);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(70, 15);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome,";

            // Initialize lstMovies
            this.lstMovies.FormattingEnabled = true;
            this.lstMovies.ItemHeight = 15;
            this.lstMovies.Location = new System.Drawing.Point(12, 40);
            this.lstMovies.Name = "lstMovies";
            this.lstMovies.Size = new System.Drawing.Size(360, 184);
            this.lstMovies.TabIndex = 1;

            // Initialize btnLogout
            this.btnLogout.Location = new System.Drawing.Point(297, 230);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 25);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;

            // UserForm
            this.ClientSize = new System.Drawing.Size(384, 271);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lstMovies);
            this.Controls.Add(this.lblWelcome);
            this.Name = "UserForm";
            this.Text = "User Menu";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

