using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Game_Store
{
    public partial class Form_User_login : Form
    {
        public Form_User_login()
        {
            InitializeComponent();
        }

        private void Form_User_login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_Register form2 = new Form_Register();
            this.Hide();
            form2.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string isim = textBox1.Text.Trim();
            string soyisim = textBox2.Text.Trim();
            string mail = textBox3.Text.Trim();

            if (string.IsNullOrWhiteSpace(isim) || string.IsNullOrWhiteSpace(soyisim) || string.IsNullOrWhiteSpace(mail))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IsValidUser(isim, soyisim, mail))
            {
                MessageBox.Show("Giriş başarılı! Satın alım sayfasına yönlendiriliyorsunuz.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form_Game_Sale rentalsForm = new Form_Game_Sale();
                this.Hide();
                rentalsForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Kullanıcı bilgileri hatalı! Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool IsValidUser(string isim, string soyisim, string mail)
        {

            using (var context = new CustomerDbContext())
            {
                return context.Customers.Any(c =>
                    c.Customer_name == isim &&
                    c.Customer_surname == soyisim &&
                    c.Customer_mail == mail);
            }
        }


    }
}
