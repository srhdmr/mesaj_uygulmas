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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=SerhatDemir\SQLEXPRESS;Initial Catalog=mesajlaşma;Integrated Security=True;");
        void gelenkutusu()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("Select MESAJID, (AD+ ' ' +SOYAD) AS GONDEREN,BASLIK,ICERIK From Mesajlar inner join Kisiler on Mesajlar.GONDEREN = Kisiler.NUMARA Where ALICI = " + numara, baglanti);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }
        void gidenkutusu()
        {
            SqlDataAdapter da2 = new SqlDataAdapter(@"Select MESAJID, (AD+ ' ' +SOYAD) AS ALICI,BASLIK,ICERIK From Mesajlar inner join Kisiler on Mesajlar.ALICI = Kisiler.NUMARA Where GONDEREN = \" + numara, baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public string numara;
        private void Form2_Load(object sender, EventArgs e)
        {
            lblnumara.Text = numara;
            gelenkutusu();
            gidenkutusu();
            //Ad Soyadı çekme 
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select AD,SOYAD FROM Kisiler Where NUMARA="+numara,baglanti);
            SqlDataReader rd = komut.ExecuteReader();
            while (rd.Read())
            {
                lbladsoyad.Text = rd[0] + " " + rd[1]; 
            }
            baglanti.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Mesajlar (GONDEREN,ALICI,BASLIK,ICERIK) Values(@P1,@P2,@P3,@P4)", baglanti);
            komut.Parameters.AddWithValue("@P1", numara);
            komut.Parameters.AddWithValue("@P2", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@P3", textBox1.Text);
            komut.Parameters.AddWithValue("@P4", richTextBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Mesaj İletildi");
            maskedTextBox1.Text = "";
            textBox1.Text = "";
            richTextBox1.Text = "";
            gidenkutusu();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
           
            txtgonderici.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            richTextBox1.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            

        }

        private void txtalıcı_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtgonderici_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;

            txtalıcı.Text = dataGridView2.Rows[secilen].Cells[1].Value.ToString();
            textBox1.Text = dataGridView2.Rows[secilen].Cells[2].Value.ToString();
            richTextBox1.Text = dataGridView2.Rows[secilen].Cells[3].Value.ToString();
        }
    }
}
