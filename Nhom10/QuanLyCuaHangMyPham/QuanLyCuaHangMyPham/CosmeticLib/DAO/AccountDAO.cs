using CosmeticLib.DTO;
using System.Data;

namespace CosmeticLib.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null) instance = new AccountDAO();
                return instance; 
            }
            private set => instance = value; 
        }

        private AccountDAO() { }

        public bool Login(string userName, string passWord)
        {
            string query = "select * from TaiKhoan where tenDangNhap = N'" + userName + "' and matKhau=N'" + passWord + "'" ;

            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result.Rows.Count > 0;
        }

        public bool GetInfoAccount(string userName, string passWord)
        {
            string query = "select tenHienThi from TaiKhoan where tenDangNhap = N'" + userName + "' and matKhau=N'" + passWord + "' ";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result.Rows.Count > 0;
        }

        public bool CheckTypeAccount(string userName, string passWord) // = 0: staff, <> 0: admin
        {
            string query = "SELECT * FROM dbo.TaiKhoan WHERE TenDangNhap = '" + userName + "' AND MatKhau = '" + passWord + "' AND Loai = 1";

            if (DataProvider.Instance.ExecuteQuery(query).Rows.Count > 0)
                return true;
            return false;
        }

        public AccountDTO GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.TaiKhoan WHERE TenDangNhap = '" + userName + "'");
            foreach (DataRow item in data.Rows)
            {
                return new AccountDTO(item);
            }
            return null;
        }
    }
}
