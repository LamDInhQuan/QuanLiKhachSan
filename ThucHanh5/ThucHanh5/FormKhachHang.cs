using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThucHanh5
{
    public partial class FormKhachHang : Form
    {
        private string connectionString;
        public FormKhachHang()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["db_Shop4Training"].ConnectionString;
   

        }
        private void FormKhachHang_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void lblTenCoQuan_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        public bool KiemTraSoDienThoai(string soDienThoai)
        {
            return Regex.IsMatch(soDienThoai, @"^\d{10,12}$");
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            String hoTen = txtHoTen.Text;
            if(hoTen.Length == 0)
            {
                MessageBox.Show("Họ tên không được để trống  !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            String diaChi = txtDiaChi.Text;
            // xử lí sdt 
            String sdt = "";
            Boolean checkSDT = KiemTraSoDienThoai(txtSDT.Text);
            if (checkSDT == true)
            {
                 sdt = txtSDT.Text;
            }
            else
            {
                MessageBox.Show("Số điện thoại không hợp lệ !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // xử lí giới tính 
            if(radio_Nam.Checked == false && radio_Nu.Checked == false)
            {
                MessageBox.Show("Giới tính không được để trống  !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int gioiTinh ;
            if (radio_Nam.Checked)
            {
                gioiTinh = 0;
            }
            else
            {
                gioiTinh = 1;
            }
            // xử lí date 
            MessageBox.Show(txtDateTime.Text);
            Boolean check = DateTime.TryParse(txtDateTime.Text, out DateTime date2);
            MessageBox.Show(date2.ToString());
            DateTime date1 = new DateTime();
            if (check)
            {
                date1 = date2;
            }
            if(date1 > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại   !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // xử lí tên cơ quan không quá 100 kí tự 

            String tenCoQuan = txtTenCoquan.Text;
            if (tenCoQuan.Length >100)
            {
                MessageBox.Show("Tên cơ quan không vượt quá 100 kí tự   !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                themKhachHangVaoCSDL(cnn, hoTen, diaChi, sdt, gioiTinh, date1, tenCoQuan);
                cnn.Close();
            }
        }

        public static void themKhachHangVaoCSDL(SqlConnection connection, string hoTen, string diaCHi, string sdt, int gioiTinh, DateTime ngaySinh ,string tenCoQuan)
        {
            using (SqlCommand insertKhachHang = new SqlCommand("spKhachHang_Insert", connection))
            {
                insertKhachHang.CommandType = CommandType.StoredProcedure;
                insertKhachHang.Parameters.Add("@sHoten", hoTen);
                insertKhachHang.Parameters.Add("@sDiachi", diaCHi);
                insertKhachHang.Parameters.Add("@sDienthoai", sdt);
                insertKhachHang.Parameters.Add("@bGioitinh", gioiTinh);
                insertKhachHang.Parameters.Add("@tNgaysinh", ngaySinh);
                insertKhachHang.Parameters.Add("@sTenCoquan", tenCoQuan);
                int soDong = insertKhachHang.ExecuteNonQuery();
                if (soDong == 0)
                {
                    MessageBox.Show("Them cong dan vao bang tblKhachHang that bai ! ");
                }
                else
                {
                    MessageBox.Show("Them cong dan vao bang tblKhachHang thanh cong  ! ");
                    FormDanhSachKhachHang form = new FormDanhSachKhachHang();
                    form.Show();
          
                }
            }
        }
        //private String intThang(String thang)
        //{
        //    if (thang == null) return "00"; // Xử lý trường hợp null

        //    thang = thang.Trim(); // Xóa khoảng trắng dư thừa

        //    switch (thang)
        //    {
        //        case "ThángGiêng": return "01";
        //        case "ThángHai": return "02";
        //        case "ThángBa": return "03";
        //        case "ThángTư": return "04";
        //        case "ThángNăm": return "05";
        //        case "ThángSáu": return "06";
        //        case "ThángBảy": return "07";
        //        case "ThángTám": return "08";
        //        case "ThángChín": return "09";
        //        case "ThángMười": return "10";
        //        case "ThángMườiMột": return "11";
        //        case "ThángMườiHai": return "12";
        //        default: return "00"; // Giá trị mặc định nếu không hợp lệ
        //    }
        //}

        //private String parseChuoiSangDate(String chuoiSangDate)
        //{
        //    String[] textDate = txtDateTime.Text.Split(' ');
        //    String thang = "";
        //    Boolean dkien1 = int.TryParse(textDate[1], out int result);
        //    Boolean dkien2 = int.TryParse(textDate[2], out int result2);
        //    if (!dkien1) thang += textDate[1];
        //    if (!dkien2) thang += textDate[2];
        //    Boolean dkien3 = true;
        //    if (textDate.Length > 4)
        //    {
        //        dkien3 = int.TryParse(textDate[3], out int result3);
        //        if (!dkien3) thang += textDate[3];
        //    }
        //    String parseThang = intThang(thang);
        //    String stringDate = "";
        //    return stringDate;
        //}

       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
