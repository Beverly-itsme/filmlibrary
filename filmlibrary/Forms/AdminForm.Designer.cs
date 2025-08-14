using System.Windows.Forms;
using System.ComponentModel;

namespace FilmLibrary.Forms
{
    partial class AdminForm
    {
        private IContainer components = null;
        private Label lblWelcome;
        private DataGridView dgvMovies;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnLogout;

        private void InitializeComponent()
        {
            lblWelcome = new Label();
            dgvMovies = new DataGridView();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnLogout = new Button();
            ((ISupportInitialize)dgvMovies).BeginInit();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(12, 9);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(122, 20);
            lblWelcome.TabIndex = 5;
            lblWelcome.Text = "Welcome, Admin";
            // 
            // dgvMovies
            // 
            dgvMovies.AllowUserToAddRows = false;
            dgvMovies.AllowUserToDeleteRows = false;
            dgvMovies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMovies.ColumnHeadersHeight = 29;
            dgvMovies.Location = new Point(12, 35);
            dgvMovies.MultiSelect = false;
            dgvMovies.Name = "dgvMovies";
            dgvMovies.ReadOnly = true;
            dgvMovies.RowHeadersWidth = 51;
            dgvMovies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovies.Size = new Size(1321, 514);
            dgvMovies.TabIndex = 4;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(21, 564);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 40);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Add";
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(113, 564);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 40);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Edit";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(209, 564);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 40);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "Delete";
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(1211, 555);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(75, 40);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "Logout";
            // 
            // AdminForm
            // 
            ClientSize = new Size(1362, 648);
            Controls.Add(btnLogout);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(dgvMovies);
            Controls.Add(lblWelcome);
            Name = "AdminForm";
            Text = "Admin";
            Load += AdminForm_Load;
            ((ISupportInitialize)dgvMovies).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}


