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
using System.Data.Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Game_Store
{
    public partial class Form_Game_Sale : Form
    {
        CustomerDbContext db = new CustomerDbContext();
        public Form_Game_Sale()
        {
            InitializeComponent();
        }

        private void Form_Game_Sale_Load(object sender, EventArgs e)
        {
            try
            {
                label3.Visible = false;
                label4.Visible = false;

                comboBox1.DataSource = db.Customers
                    .OrderBy(c => c.Customer_name)
                    .Select(c => new
                    {
                        c.Customer_id,
                        FullName = c.Customer_name + " " + c.Customer_surname
                    })
                    .ToList();
                comboBox1.DisplayMember = "FullName";
                comboBox1.ValueMember = "Customer_id";

                comboBox2.DataSource = db.Games
                    .OrderBy(g => g.Game_Name)
                    .Select(g => new
                    {
                        g.Game_id,
                        GameInfo = g.Game_Name + " - " + g.Game_Price + "₺ (" + g.Game_Type + ")"
                    })
                    .ToList();
                comboBox2.DisplayMember = "GameInfo";
                comboBox2.ValueMember = "Game_id";

                button2.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}", "Veri Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var list = db.Gamecustomers
                    .Include(gc => gc.Customer)    
                    .Include(gc => gc.Game)      
                    .Select(gc => new
                    {
                        gc.GameCus_id,                   
                        MüşteriAdı = gc.Customer.Customer_name,
                        MüşteriSoyadı = gc.Customer.Customer_surname,
                        OyunAdı = gc.Game.Game_Name,
                        OyunTürü = gc.Game.Game_Type,
                        OyunFiyatı = gc.Game.Game_Price
                    })
                    .ToList();

                dataGridView1.DataSource = list;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["GameCus_id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Veri Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedCustomerId = (int)comboBox1.SelectedValue;
                int selectedGameId = (int)comboBox2.SelectedValue;

                var newRecord = new Gamecustomer
                {
                    Customer_id = selectedCustomerId,
                    Game_id = selectedGameId
                };

                db.Gamecustomers.Add(newRecord);
                db.SaveChanges();

                MessageBox.Show("Satın Alım Başarılı Bizi Tercih Ettiğiniz İçin Teşekkürler.");
                button2.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult sonuc = MessageBox.Show(
                    "İade etmek istediğinize emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (sonuc == DialogResult.Yes && dataGridView1.CurrentRow != null)
                {
                    int selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["GameCus_id"].Value);
                    var recordToDelete = db.Gamecustomers.Find(selectedId);

                    if (recordToDelete != null)
                    {
                        db.Gamecustomers.Remove(recordToDelete);
                        db.SaveChanges();
                        MessageBox.Show("İade Gerçekleştirildi.");
                        button2.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt bulunamadı.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata = {ex.Message}");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rd_1.Checked)
                {
                    var result = db.Gamecustomers
                        .GroupBy(gc => new
                        {
                            gc.Customer.Customer_id,
                            gc.Customer.Customer_name,
                            gc.Customer.Customer_surname
                        })
                        .Select(g => new
                        {
                            FullName = g.Key.Customer_name + " " + g.Key.Customer_surname,
                            TotalCount = g.Count()
                        })
                        .OrderByDescending(x => x.TotalCount)
                        .FirstOrDefault();

                    label3.Text = result != null
                        ? $"{result.FullName} adlı üyemiz toplamda {result.TotalCount} oyun alarak en çok oyun alan kişi olmuştur."
                        : "Herhangi bir kayıt bulunamadı.";
                    label3.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void rd_2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rd_2.Checked)
                {
                    var result = db.Gamecustomers
                        .Include(gc => gc.Game)
                        .GroupBy(gc => new
                        {
                            gc.Customer.Customer_id,
                            gc.Customer.Customer_name,
                            gc.Customer.Customer_surname
                        })
                        .Select(g => new
                        {
                            FullName = g.Key.Customer_name + " " + g.Key.Customer_surname,
                            TotalPrice = g.Sum(gc => gc.Game.Game_Price)
                        })
                        .OrderByDescending(x => x.TotalPrice)
                        .FirstOrDefault();

                    label4.Text = result != null
                        ? $"{result.FullName} adlı üyemiz toplamda {result.TotalPrice} TL harcayarak en çok yatırım yapan kişi olmuştur."
                        : "Herhangi bir kayıt bulunamadı.";
                    label4.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form_User_login form1 = new Form_User_login();
            this.Hide();
            form1.ShowDialog();
            this.Close();
        }
    }
}
