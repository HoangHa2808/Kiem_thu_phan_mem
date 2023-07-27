using QuanLyCuaHangMyPham.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangMyPham.DAO
{
    public class StaffDAO
    {
        private static StaffDAO instance;

        public static StaffDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new StaffDAO();
                return instance;
            }
            private set => instance = value;
        }

        public StaffDAO()
        { }

        public StaffDTO GetStaffByAccount(AccountDTO acc)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery($"SELECT * FROM dbo.NhanVien WHERE IDTK = {acc.Id}");
            foreach (DataRow item in data.Rows)
            {
                return new StaffDTO(item);
            }
            return null;
        }
    }
}
