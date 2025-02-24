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
    public partial class FormDataListKhachHang : Form
    {

        public FormDataListKhachHang()
        {
            InitializeComponent();
            lstKhachHang.DataSource = hienThiDanhSachKhachHang();
            lstKhachHang.DisplayMember = "sHoten"; // Tên cột hiển thị
            lstKhachHang.ValueMember = "PK_iKhachhangID"; // Giá trị ẩn (ID)
        }

        private void lstKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
           
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

        private void btnXemTT_Click(object sender, EventArgs e)
        {
            List<DataRowView> rows = new List<DataRowView>();
            foreach (DataRowView item in lstKhachHang.SelectedItems)
            {
                rows.Add(item);
            }
            if (lstKhachHang.SelectedItems != null)
            {
                String gioiTinh = "";
                foreach (DataRowView item in rows)
                {
                    if (bool.TryParse(item[4].ToString(), out bool gioiTinhValue))
                    {
                        gioiTinh = (gioiTinhValue == true) ? "Nữ" : "Nam";
                    }
                    MessageBox.Show(string.Format("Mã khách hàng : {0} \n" +
                                              "Tên khách hàng : {1} \n" +
                                              "Địa chỉ : {2} \n" +
                                              "Số điện thoại : {3} \n" +
                                              "Giới tính : {4} \n" +
                                              "Ngày sinh : {5} \n" +
                                              "Tên cơ quan : {6} ", item[0], item[1], item[2], item[3], gioiTinh, item[5], item[6]));


                }
            }
        }
    }
}
