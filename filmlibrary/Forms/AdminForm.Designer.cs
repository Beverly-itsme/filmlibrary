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
            lblWelcome = new Label();
            lstMovies = new ListBox();
            btnLogout = new Button();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(12, 9);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(74, 20);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Welcome,";
            // 
            // lstMovies
            // 
            lstMovies.FormattingEnabled = true;
            lstMovies.Location = new Point(12, 40);
            lstMovies.Name = "lstMovies";
            lstMovies.Size = new Size(360, 184);
            lstMovies.TabIndex = 1;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(297, 230);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(75, 36);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            // 
            // AdminForm
            // 
            ClientSize = new Size(497, 295);
            Controls.Add(btnLogout);
            Controls.Add(lstMovies);
            Controls.Add(lblWelcome);
            Name = "AdminForm";
            Text = "User Menu";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

