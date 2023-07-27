using QuanLyCuaHangMyPham.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangMyPham.DAO
{
    public class PartnerDAO
    {
        private static PartnerDAO instance;

        public static PartnerDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new PartnerDAO();
                return instance;
            }
            private set => instance = value;
        }

        public PartnerDAO()
        { }

        public int GetIDByPartner(string name, string address, string phoneNumber)
        {
            int id = -1;
            try
            {
                DataTable data = DataProvider.Instance.ExecuteQuery($"SELECT * FROM dbo.DoiTac WHERE TenDT = N'{name}' AND DiaChi = N'{address}' AND SDT = N'{phoneNumber}'");
                id = (int)data.Rows[0]["MaDT"];
            }
            catch (Exception)
            {
            }
            return id;
        }

        public int GetIDByPartner(string name)
        {
            int id = -1;
            try
            {
                DataTable data = DataProvider.Instance.ExecuteQuery($"SELECT * FROM dbo.DoiTac WHERE TenDT = N'{name}'");
                id = (int)data.Rows[0]["MaDT"];
            }
            catch (Exception)
            {
            }
            return id;
        }
    }
}
