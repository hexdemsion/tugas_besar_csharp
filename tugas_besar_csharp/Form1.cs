using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace tugas_besar_csharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ambil_data();
        }

        public MySqlConnection buat_koneksi()
        {
            MySqlBaseConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "tugas_besar";

            String connString = builder.ToString();
            builder = null;
            MySqlConnection dbConn = new MySqlConnection(connString);
            return dbConn;
        }

        private void ambil_data()
        {
            DataTable tabelmhs = new DataTable();
            MySqlConnection koneksi = buat_koneksi();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM biodata", koneksi);
                koneksi.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                adapter.Fill(tabelmhs);
                koneksi.Close();
                dataGridView1.DataSource = tabelmhs;
            }
            catch (Exception)
            {
                MessageBox.Show("Error mengambil data");
            }
        }

        private void insert_data(object sender, EventArgs e)
        {
            String stb = textBox1.Text;
            String nama = textBox2.Text;
            String prodi = comboBox4.Text;
            String agama = comboBox1.Text;
            String tempat_lahir = textBox4.Text;
            String tanggal_lahir = dateTimePicker1.Text;
            String jenis_kelamin = comboBox2.Text;
            String alamat = textBox5.Text;
            String kota = textBox6.Text;
            String provinsi = textBox7.Text;
            String kode_pos = textBox8.Text;
            String telepon = textBox9.Text;
            String handphone = textBox10.Text;
            String hobi = textBox11.Text;
            String wali = textBox12.Text;
            String alamat_wali = textBox13.Text;
            String telepon_wali = textBox14.Text;
            String tahun_masuk = comboBox3.Text;

            try
            {
                String query_input =
                    "INSERT INTO biodata (stb,nama,prodi,agama,tempat_lahir,tanggal_lahir,jenis_kelamin,alamat,kota,provinsi,kode_pos,telepon,handphone,hobi,wali,alamat_wali,telepon_wali,tahun_masuk) " +
                    "VALUES ('" + stb + "', '" + nama + "', '" + prodi + "', '" + agama + "', '" + tempat_lahir + "', '" + tanggal_lahir +
                    "', '" + jenis_kelamin + "', '" + alamat + "', '" + kota + "', '" + provinsi + "', '" + kode_pos + "', '" + telepon + "', '" + handphone + "', '" + hobi + "', '" + wali + "', '" + alamat_wali + "', '" + telepon_wali + "', '" + tahun_masuk + "') ";

                MySqlConnection koneksi = buat_koneksi();
                MySqlCommand cmd = new MySqlCommand(query_input, koneksi);
                koneksi.Open();
                cmd.ExecuteNonQuery();
                koneksi.Close();
                MessageBox.Show("Data berhasil di-input");
                ambil_data();
            }
            catch (Exception)
            {
                MessageBox.Show("Data gagal di-input");
            }
        }



    }
}
