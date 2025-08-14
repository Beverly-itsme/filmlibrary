using System.Windows.Forms;
using System.ComponentModel;

namespace FilmLibrary.Forms
{
    partial class UserForm
    {
        private IContainer components = null;
        private Label lblWelcome;
        private TextBox txtSearchTitle;
        private ComboBox cboGenre;
        private ComboBox cboStatus;
        private Button btnSearch;
        private Button btnClear;
        private ListBox lstMovies;
        private Button btnLogout;

        private void InitializeComponent()
        {
            lblWelcome = new Label();
            txtSearchTitle = new TextBox();
            cboGenre = new ComboBox();
            cboStatus = new ComboBox();
            btnSearch = new Button();
            btnClear = new Button();
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
            lblWelcome.TabIndex = 7;
            lblWelcome.Text = "Welcome,";
            // 
            // txtSearchTitle
            // 
            txtSearchTitle.Location = new Point(12, 35);
            txtSearchTitle.Name = "txtSearchTitle";
            txtSearchTitle.PlaceholderText = "Title contains...";
            txtSearchTitle.Size = new Size(200, 27);
            txtSearchTitle.TabIndex = 6;
            // 
            // cboGenre
            // 
            cboGenre.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGenre.Location = new Point(218, 35);
            cboGenre.Name = "cboGenre";
            cboGenre.Size = new Size(150, 28);
            cboGenre.TabIndex = 5;
            // 
            // cboStatus
            // 
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.Location = new Point(374, 35);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(140, 28);
            cboStatus.TabIndex = 4;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(520, 34);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 25);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "Search";
            // 
            // btnClear
            // 
            btnClear.Location = new Point(601, 34);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 25);
            btnClear.TabIndex = 2;
            btnClear.Text = "Clear";
            // 
            // lstMovies
            // 
            lstMovies.FormattingEnabled = true;
            lstMovies.Location = new Point(12, 69);
            lstMovies.Name = "lstMovies";
            lstMovies.Size = new Size(1031, 504);
            lstMovies.TabIndex = 1;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLogout.Location = new Point(968, 595);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(75, 36);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "Logout";
            // 
            // UserForm
            // 
            ClientSize = new Size(1055, 632);
            Controls.Add(btnLogout);
            Controls.Add(lstMovies);
            Controls.Add(btnClear);
            Controls.Add(btnSearch);
            Controls.Add(cboStatus);
            Controls.Add(cboGenre);
            Controls.Add(txtSearchTitle);
            Controls.Add(lblWelcome);
            Name = "UserForm";
            Text = "Movies";
            Load += UserForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

