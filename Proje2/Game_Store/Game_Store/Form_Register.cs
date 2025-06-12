using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Game_Store
{
    public partial class Form_Register : Form
    {
        CustomerDbContext db = new CustomerDbContext();
        public Form_Register()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_name"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_surname"].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_mail"].Value.ToString();
                maskedTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Customer_tel"].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var customers = db.Customers.ToList();
                dataGridView1.DataSource = customers;

                
                dataGridView1.Columns["Customer_id"].HeaderText = "Müşteri ID";
                dataGridView1.Columns["Customer_name"].HeaderText = "Müşteri Adı";
                dataGridView1.Columns["Customer_surname"].HeaderText = "Müşteri Soyadı";
                dataGridView1.Columns["Customer_mail"].HeaderText = "E-Posta";
                dataGridView1.Columns["Customer_tel"].HeaderText = "Telefon";

                
                dataGridView1.Columns["Customer_Id"].Visible = false;

              
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Customer customer = new Customer()
                {
                    Customer_name = textBox1.Text,
                    Customer_surname = textBox2.Text,
                    Customer_mail = textBox3.Text,
                    Customer_tel = maskedTextBox1.Text
                };

                db.Customers.Add(customer);
                db.SaveChanges();

                MessageBox.Show("Yeni Müşteri Kaydedildi!");
                button3.PerformClick();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Customer_Id"].Value);
                    Customer customer = db.Customers.Find(selectedId);
                    if (customer != null)
                    {
                        customer.Customer_name = textBox1.Text;
                        customer.Customer_surname = textBox2.Text;
                        customer.Customer_mail = textBox3.Text;
                        customer.Customer_tel = maskedTextBox1.Text;

                        db.SaveChanges();

                        MessageBox.Show("Müşteri güncellendi!");

                        button3.PerformClick();
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult sonuc = MessageBox.Show(
                            "Silmek istediğinize emin misiniz?",
                            "Silme Onayı",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                if (sonuc == DialogResult.Yes)
                {
                    if (dataGridView1.CurrentRow != null)
                    {
                        int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Customer_Id"].Value);
                        Customer customer = db.Customers.Find(selectedId);
                        if (customer != null)
                        {
                            db.Customers.Remove(customer);
                            db.SaveChanges();

                            MessageBox.Show("Müşteri Silindi!");
                            button3.PerformClick();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form_User_login form1 = new Form_User_login();
            this.Hide();
            form1.ShowDialog();
            this.Close();
        }
    }
}
