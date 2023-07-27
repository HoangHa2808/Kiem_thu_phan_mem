using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangMyPham.DTO
{
    public class PartnerDTO
    {
        private int id;
        private string name;
        private string address;
        private string phoneNumber;
        private bool type;
        private bool status;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public bool Type { get => type; set => type = value; }
        public bool Status { get => status; set => status = value; }

        public PartnerDTO()
        { }

        public PartnerDTO(int id, string name, string address, string phoneNumber, bool type, bool status)
        {
            this.Id = id;
            this.Name = name;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.Type = type;
            this.Status = status;
        }

        public PartnerDTO(DataRow row)
        {
            this.Id = (int)row["MaDT"];
            this.Name = row["TenDT"].ToString();
            this.Address = row["DiaChi"].ToString();
            this.PhoneNumber = row["SDT"].ToString();
            this.Type = (bool)row["Loai"];
            this.Status = (bool)row["TrangThai"];
        }
    }
}
