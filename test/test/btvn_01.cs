using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    internal class btvn_01
    {
        static void Main(string[] args)
        {
            // bai 1 
            Console.WriteLine("BAI 1  : ");
            Console.WriteLine("Nhap so thu nhat : ");
            int i = int.Parse(Console.ReadLine());
            Console.WriteLine("Nhap so thu hai : ");
            int j = int.Parse(Console.ReadLine());
            int tong;
            tong = i + j;
            Console.WriteLine("Tong la : " + tong);
            // bai 2 
            Console.Write("\n========== \n");
            Console.WriteLine("BAI 2  : ");
            Console.WriteLine("Nhap so thu nhat : ");
            int so1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Nhap so thu hai : ");
            int so2 = int.Parse(Console.ReadLine());
            Console.WriteLine("Nhap so thu ba : ");
            int so3 = int.Parse(Console.ReadLine());
            int tich = so1 * so2 * so3;
            Console.WriteLine("Tich 3 so nhap vao la  : " + tich);
            // bai 3
            Console.Write("\n========== \n");
            Console.WriteLine("BAI 3  : ");
            Console.WriteLine("Nhap mot chuoi : ");
            String chuoi = Console.ReadLine();
            int tongSoTu = chuoi.Length;
            Console.WriteLine("Chuoi nay co " + tongSoTu + " tu .");
            string chuHoa = "";
            string chuThuong = "";
            foreach (char c in chuoi)
            {
                if (char.IsUpper(c))
                {
                    chuHoa += c;
                }
                else if (char.IsLower(c))
                {
                    chuThuong += c;
                }
            }
            int tongChuHoa = chuHoa.Length;
            int tongChuThuong = chuThuong.Length;
            Console.WriteLine("Chuoi nay co " + tongChuHoa + " chu Hoa va " + tongChuThuong + " chu Thuong .");
            string connectionString = "Server=LAPTOP-H70MGE35\\COLADEPTRAI;Database=lonmemay;User Id=lamdinhquan;Password=coco;";

            // Tạo kết nối tới SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Mở kết nối
                    connection.Open();

                    // Thực hiện các câu lệnh SQL hoặc truy vấn nếu cần
                    string query = "SELECT * FROM tblBan";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    // Đọc kết quả truy vấn
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["sMaban"]); // Thay "ColumnName" bằng tên cột thực tế trong bảng
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi kết nối nếu có
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

        }
    }
}