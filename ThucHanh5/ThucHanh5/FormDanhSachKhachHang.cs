using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThucHanh5
{
    public partial class FormDanhSachKhachHang : Form
    {
        public FormDanhSachKhachHang()
        {
            InitializeComponent();
            dataGridViewKhachHang.DataSource = hienThiDanhSachKhachHang();
        }

        private DataTable getListKhachHang()
        {
            DataTable dbKhachHang = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["db_Shop4Training"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("khachHang_Get", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {

                    adapter.Fill(dbKhachHang);
                }
            }
            return dbKhachHang;
        }


        private DataView hienThiDanhSachKhachHang()
        {
            DataTable dbKhachHang = getListKhachHang();
            DataView dataView = dbKhachHang.AsDataView();
            return dataView;

        }
        private void dataGridViewKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
