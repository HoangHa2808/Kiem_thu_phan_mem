using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangMyPham.DTO
{
    public class AccountDTO
    {
        private int id;
        private string name;
        private string userName;
        private string password;
        private int type;
        private string sex;
        private DateTime? dateOfBirth;
        private DateTime? dateStart;
        private int identityCode;
        private string address;
        private string phoneNumber;
        private bool status;


        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public int Type { get => type; set => type = value; }
        public bool Status { get => status; set => status = value; }
        public string Sex { get => sex; set => sex = value; }
        public DateTime? DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public DateTime? DateStart { get => dateStart; set => dateStart = value; }
        public int IdentityCode { get => identityCode; set => identityCode = value; }
        public string Address { get => address; set => address = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        public AccountDTO() { }

        public AccountDTO(int id, string userName, string displayName, int type, string sex, DateTime? dateOfBirth, DateTime? dateStart, int identityCode, string address, string phoneNumber, bool status, string password = null)
        {
            this.Id = id;
            this.Name = displayName;
            this.UserName = userName;
            this.Password = password;
            this.Type = type;
            this.Sex = sex;
            this.DateOfBirth = dateOfBirth;
            this.DateStart = dateStart;
            this.IdentityCode = identityCode;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.Status = status;
        }

        public AccountDTO(DataRow row)
        {
            this.Id = (int)row["ID"];
            this.Name = row["Ten"].ToString();
            this.UserName = row["TenDangNhap"].ToString();
            this.Password = row["MatKhau"].ToString();
            this.Type = (int)row["Loai"];
            this.Sex = row["GioiTinh"].ToString();
            this.DateOfBirth = (DateTime?)row["NgaySinh"];
            this.DateStart = (DateTime?)row["NgayVaoLam"];
            this.IdentityCode = (int)row["SoCMND"];
            this.Address = row["DiaChi"].ToString();
            this.PhoneNumber = row["SDT"].ToString();
            this.Status = (bool)row["TrangThai"];
        }
    }
}
