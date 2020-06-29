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

            string connString = builder.ToString();
            builder = null;
            MySqlConnection dbConn = new MySqlConnection(connString);
            return dbConn;
        }

        DataTable tabelmhs = new DataTable();
        private void ambil_data()
        {
            MySqlConnection koneksi = buat_koneksi();

            try
            {
                string query_read = "SELECT * FROM biodata";
                MySqlCommand cmd = new MySqlCommand(query_read, koneksi);
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
            string stb = textBox1.Text;
            string nama = textBox2.Text;
            string prodi = comboBox4.Text;
            string agama = comboBox1.Text;
            string tempat_lahir = textBox4.Text;
            string tanggal_lahir = dateTimePicker1.Text;
            string jenis_kelamin = comboBox2.Text;
            string alamat = textBox5.Text;
            string kota = textBox6.Text;
            string provinsi = textBox7.Text;
            string kode_pos = textBox8.Text;
            string telepon = textBox9.Text;
            string handphone = textBox10.Text;
            string hobi = textBox11.Text;
            string wali = textBox12.Text;
            string alamat_wali = textBox13.Text;
            string telepon_wali = textBox14.Text;
            string tahun_masuk = comboBox3.Text;

            try
            {
                string query_create =
                    "INSERT INTO biodata (stb,nama,prodi,agama,tempat_lahir,tanggal_lahir,jenis_kelamin,alamat,kota,provinsi,kode_pos,telepon,handphone,hobi,wali,alamat_wali,telepon_wali,tahun_masuk) " +
                    "VALUES ('" + stb + "', '" + nama + "', '" + prodi + "', '" + agama + "', '" + tempat_lahir + "', '" + tanggal_lahir +
                    "', '" + jenis_kelamin + "', '" + alamat + "', '" + kota + "', '" + provinsi + "', '" + kode_pos + "', '" + telepon + "', '" + handphone + "', '" + hobi + "', '" + wali + "', '" + alamat_wali + "', '" + telepon_wali + "', '" + tahun_masuk + "') ";

                MySqlConnection koneksi = buat_koneksi();
                MySqlCommand cmd = new MySqlCommand(query_create, koneksi);
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int row_id = e.RowIndex;
            int col_id = e.ColumnIndex;
            string record_id = dataGridView1.Rows[row_id].Cells[0].Value.ToString();
            string record_name = dataGridView1.Columns[col_id].Name;
            string dataku = dataGridView1.Rows[row_id].Cells[col_id].Value.ToString();

            try
            {
                string query_update = "UPDATE biodata SET "+record_name+"='"+dataku+"' WHERE id=" + record_id;

                MySqlConnection koneksi = buat_koneksi();
                MySqlCommand cmd = new MySqlCommand(query_update, koneksi);
                koneksi.Open();
                cmd.ExecuteNonQuery();
                koneksi.Close();
                MessageBox.Show("Data berhasil di-ubah");
                ambil_data();
            }
            catch (Exception)
            {
                MessageBox.Show("Data gagal di-ubah");
            }

        }

        private void hapus_data(object sender, EventArgs e)
        {
            int row_id = dataGridView1.SelectedCells[0].RowIndex;
            string record_id = dataGridView1.Rows[row_id].Cells[0].Value.ToString();

            try
            {
                string query_delete = "DELETE FROM biodata WHERE id=" + record_id;

                MySqlConnection koneksi = buat_koneksi();
                MySqlCommand cmd = new MySqlCommand(query_delete, koneksi);
                koneksi.Open();
                cmd.ExecuteNonQuery();
                koneksi.Close();
                MessageBox.Show("Data berhasil di-hapus");
                ambil_data();
            }
            catch (Exception)
            {
                MessageBox.Show("Data gagal di-hapus");
            }
        }

        private void cari_data(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataView dv = tabelmhs.DefaultView;
                dv.RowFilter = string.Format("nama like '%{0}%'", textBox15.Text);
                dataGridView1.DataSource = dv.ToTable();
            }
        }
    }
}
