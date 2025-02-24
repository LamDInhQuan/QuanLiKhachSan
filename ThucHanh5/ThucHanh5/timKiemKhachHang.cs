using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThucHanh5
{
    public partial class timKiemKhachHang : Form
    {
        private string connectionString = "";
        private string idKhachHang = "";
        public timKiemKhachHang()
        {
            connectionString = ConfigurationManager.ConnectionStrings["db_Shop4Training"].ConnectionString;
            InitializeComponent();
            hienThiDanhSachKhachHangTrongDataGridView();
            radioValue_Nam.Checked = true;
        }
        private DataTable getListKhachHang()
        {
            DataTable dbKhachHang = new DataTable();
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("dfvdv");
            String hoTen = txtHoTen.Text;
            String diaChi = txtDiaChi.Text;
            String sdt = txtSDT.Text;
            // xử lí giới tính 
            bool gioiTinh = radioValue_Nu.Checked ? true : false;
            // xử lí date 
            DateTime.TryParse(dateTimeNgaySinh.Text, out DateTime date2);
            // xử lí tên cơ quan không quá 100 kí tự
            String tenCoQuan = txtCoQuan.Text;
            if (hoTen == "" && diaChi == "" && sdt == "" && gioiTinh == null && date2.ToString().Length == 0 && tenCoQuan == "")
            {
                MessageBox.Show("Vui lòng nhập một trường để tìm kiếm ");
                return;
            }
            tblKhachHang.Rows.Clear();
            DataView khachHangView = hienThiDanhSachKhachHang();
            string filter = "";
            if (!string.IsNullOrEmpty(hoTen))
                filter += $"sHoten LIKE '*{hoTen}*' AND ";
            if (!string.IsNullOrEmpty(diaChi))
                filter += $"sDiachi LIKE '*{diaChi}*' AND ";
            if (!string.IsNullOrEmpty(sdt))
                filter += $"sDienthoai LIKE '*{sdt}*' AND ";
            if (gioiTinh == true || gioiTinh == false) // Nếu giới tính có giá trị hợp lệ (0 = Nữ, 1 = Nam)
                filter += $"bGioitinh = {gioiTinh} AND ";
            if (date2 != DateTime.MinValue)
                filter += $"tNgaysinh = #{date2:yyyy-MM-dd}# AND ";
            if (!string.IsNullOrEmpty(tenCoQuan))
                filter += $"sTenCoquan LIKE '*{tenCoQuan}*' AND ";

            // Xóa "AND" cuối cùng nếu có
            if (filter.EndsWith(" AND "))
                filter = filter.Substring(0, filter.Length - 5);
            khachHangView.RowFilter = filter;
            foreach (DataRowView item in khachHangView)
            {
                tblKhachHang.Rows.Add(item[0], item[1]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("324234");
            tblKhachHang.Rows.Clear();
            hienThiDanhSachKhachHangTrongDataGridView();
        }

        private void hienThiDanhSachKhachHangTrongDataGridView()
        {
            foreach (DataRow item in hienThiDanhSachKhachHang().Table.Rows)
            {
                tblKhachHang.Rows.Add(item[0], item[1]);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("324234");
            DataGridViewRow row = tblKhachHang.CurrentRow;
            string id = row.Cells[0].Value.ToString();
            idKhachHang = id;
            foreach (DataRowView item in hienThiDanhSachKhachHang())
            {
                if (item[0].ToString().Equals(id))
                {
                    txtHoTen.Text = item[1].ToString();
                    txtDiaChi.Text = item[2].ToString();
                    txtSDT.Text = item[3].ToString();
                    if (bool.Parse(item[4].ToString()) == false) radioValue_Nam.Checked = true;
                    else radioValue_Nu.Checked = true;
                    dateTimeNgaySinh.Value = DateTime.Parse(item[5].ToString());
                    txtCoQuan.Text = item[6].ToString();
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text;
            string diaChi = txtDiaChi.Text;
            string sdt = txtSDT.Text;
            bool gioiTinh = radioValue_Nam.Checked ? true : false;
            DateTime ngaySinh = dateTimeNgaySinh.Value;
            string tenCoQuan = txtCoQuan.Text;
            updateKhachHang(connectionString,idKhachHang,hoTen,diaChi,sdt,gioiTinh,ngaySinh,tenCoQuan);
        }

        private void updateKhachHang(String connection, string idKH, string hoTen, string diaChi, string sdt, bool gioiTinh
            , DateTime ngaySinh, string tenCoQuan)
        {
            try
            {
                DataGridViewRow dongDangCHon = tblKhachHang.CurrentRow;
                string idKhachHangDUocCHon = dongDangCHon.Cells[0].Value.ToString();
                if (!idKhachHangDUocCHon.Equals(idKhachHang))
                {
                    MessageBox.Show("Dòng được chọn không phải trùng với id khách hàng cần cập nhật !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                using (SqlConnection cnn = new SqlConnection(connection))
                {
                    cnn.Open();
                    SqlCommand updateKhachHang = new SqlCommand("sp_updateKhachHang", cnn);

                    updateKhachHang.CommandType = CommandType.StoredProcedure;
                    updateKhachHang.Parameters.Add("@idKhachHang", int.Parse(idKH));
                    updateKhachHang.Parameters.Add("@shoTen", hoTen);
                    updateKhachHang.Parameters.Add("@sDiaChi", diaChi);
                    updateKhachHang.Parameters.Add("@sDienThoai", sdt);
                    updateKhachHang.Parameters.Add("@bGioiTinh", gioiTinh);
                    updateKhachHang.Parameters.Add("@tNgaySinh", ngaySinh);
                    updateKhachHang.Parameters.Add("@sTenCoQuan", tenCoQuan);
                    int soDong = updateKhachHang.ExecuteNonQuery();
                    if (soDong == 0)
                    {
                        MessageBox.Show("Cập nhật thông tin khách hàng thất bại ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thông tin khách hàng thành công ", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
            catch (SqlException exSql)
            {
                MessageBox.Show("Lỗi : "+exSql.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    } 
}
