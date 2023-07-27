using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangMyPham.DTO
{
    public class ProductDTO
    {
        private int maSP;
        private string tenSP;
        private int phanLoai;
        private int giaNhap;
        private int gia;
        private string donViTinh;
        private int soLuongTon;
        private string xuatXu;
        private int nhaCC;
        private string moTa;
        private bool trangThai;

        public int MaSP { get => maSP; set => maSP = value; }
        public string TenSP { get => tenSP; set => tenSP = value; }
        public int PhanLoai { get => phanLoai; set => phanLoai = value; }
        public int Gia { get => gia; set => gia = value; }
        public string DonViTinh { get => donViTinh; set => donViTinh = value; }
        public int SoLuongTon { get => soLuongTon; set => soLuongTon = value; }
        public string XuatXu { get => xuatXu; set => xuatXu = value; }
        public int NhaCC { get => nhaCC; set => nhaCC = value; }
        public string MoTa { get => moTa; set => moTa = value; }
        public bool TrangThai { get => trangThai; set => trangThai = value; }
        public int GiaNhap { get => giaNhap; set => giaNhap = value; }

        public ProductDTO(int maSP, string tenSP, int phanLoai, int gia, string donViTinh, int soLuongTon, string xuatXu, int nhaCC, string moTa, bool trangThai, int giaNhap)
        {
            this.MaSP = maSP;
            this.TenSP = tenSP;
            this.PhanLoai = phanLoai;
            this.Gia = gia;
            this.DonViTinh = donViTinh;
            this.SoLuongTon = soLuongTon;
            this.XuatXu = xuatXu;
            this.NhaCC = nhaCC;
            this.MoTa = moTa;
            this.TrangThai = trangThai;
            this.GiaNhap = giaNhap;
        }

        public ProductDTO(DataRow row)
        {
            this.MaSP = (int)row["MaSP"];
            this.TenSP = row["TenSP"].ToString();
            this.PhanLoai = (int)row["PhanLoai"];
            this.Gia = (int)row["GiaNhap"];
            this.Gia = (int)row["Gia"];
            this.DonViTinh = row["DonViTinh"].ToString();
            this.SoLuongTon = (int)row["SoLuongTon"];
            this.XuatXu = row["XuatXu"].ToString();
            this.NhaCC = (int)row["NhaCC"];
            this.MoTa = row["MoTa"].ToString();
            this.TrangThai = (bool)row["TrangThai"];
        }
    }
}
