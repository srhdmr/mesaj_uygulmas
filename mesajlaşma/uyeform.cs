using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace mesajlaşma
{
    public partial class uyeform : Form
    {
        public uyeform()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=SerhatDemir\SQLEXPRESS;Initial Catalog=mesajlaşma;Integrated Security=True;");
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Kisiler(AD,SOYAD,NUMARA,SİFRE) values(@P1,@P2,@P3,@P4)", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            
            komut.Parameters.AddWithValue("@P3", TxtNumara.Text);
            komut.Parameters.AddWithValue("@P4", TxtSifre.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("ÜYE KAYDINIZ GERÇEKLEŞTİRİLDİ!");

        }

        private void TxtNumara_TextChanged(object sender, EventArgs e)
        {
            if (TxtNumara.TextLength > 4)
            {
                MessageBox.Show("NUMARA UZUNLUĞUNUZ 4 KARAKTER OLMALIDIR!");
            }
           
        }
    }
}
