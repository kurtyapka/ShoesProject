using Microsoft.EntityFrameworkCore;
using ShoesProject.Models;
using System.Data;

namespace ShoesProject
{
    public partial class FormLogin : Form
    {
        public ShoesProject.Models.User CurrentUser { get; private set; }
        public bool IsGuest { get; private set; }

        public FormLogin()
        {
            InitializeComponent();

            btnGuessed.Click += BtnGuest_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtLogin.Text) || String.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var optionsBuilder = new DbContextOptionsBuilder<ShopDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=shop_db;Username=postgres;Password=1111");
            using (var db = new ShopDbContext(optionsBuilder.Options))
            {
                string loginInput = txtLogin.Text.Trim();
                string passwordInput = txtPassword.Text.Trim();

                var user = db.Users
                    .AsEnumerable()
                    .FirstOrDefault(w => w.Login.Trim() == loginInput && w.Pass.Trim() == passwordInput);

                if (user != null)
                {
                    CurrentUser = user;
                    IsGuest = false;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnGuest_Click(object sender, EventArgs e)
        {
            CurrentUser = null;
            IsGuest = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
