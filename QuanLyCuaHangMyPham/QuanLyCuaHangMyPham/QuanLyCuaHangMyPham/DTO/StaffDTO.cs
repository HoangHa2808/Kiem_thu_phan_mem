using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangMyPham.DTO
{
    public class StaffDTO
    {
        private int maNV;
        private string tenNV;
        private string gioiTinh;
        private DateTime? ngaySinh;
        private DateTime? ngayVaoLam;
        private int soCMND;
        private string diaChi;
        private string sDT;
        private int iDTK;
        private bool trangThai;

        public int MaNV { get => maNV; set => maNV = value; }
        public string TenNV { get => tenNV; set => tenNV = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public DateTime? NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public DateTime? NgayVaoLam { get => ngayVaoLam; set => ngayVaoLam = value; }
        public int SoCMND { get => soCMND; set => soCMND = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string SDT { get => sDT; set => sDT = value; }
        public int IDTK { get => iDTK; set => iDTK = value; }
        public bool TrangThai { get => trangThai; set => trangThai = value; }

        public StaffDTO(int maNV, string tenNV, string gioiTinh, DateTime? ngaySinh, DateTime? ngayVaoLam, int soCMND, string diaChi, string sDT, int iDTK, bool trangThai)
        {
            this.MaNV = maNV;
            this.TenNV = tenNV;
            this.GioiTinh = gioiTinh;
            this.NgaySinh = ngaySinh;
            this.NgayVaoLam = ngayVaoLam;
            this.SoCMND = soCMND;
            this.DiaChi = diaChi;
            this.SDT = sDT;
            this.IDTK = iDTK;
            this.TrangThai = trangThai;
        }

        public StaffDTO(DataRow row)
        {
            this.MaNV = (int)row["maNV"];
            this.TenNV = row["tenNV"].ToString();
            this.GioiTinh = row["gioiTinh"].ToString();
            this.NgaySinh = (DateTime?)row["ngaySinh"];
            this.NgayVaoLam = (DateTime?)row["ngayVaoLam"];
            this.SoCMND = (int)row["soCMND"];
            this.DiaChi = row["diaChi"].ToString();
            this.SDT = row["sDT"].ToString();
            this.IDTK = (int)row["iDTK"];
            this.TrangThai = (bool)row["trangThai"];
        }
    }
}
