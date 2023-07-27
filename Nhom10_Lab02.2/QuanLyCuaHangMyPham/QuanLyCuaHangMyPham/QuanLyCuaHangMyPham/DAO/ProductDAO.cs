using QuanLyCuaHangMyPham.DTO;
using QuanLyCuaHangMyPham.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangMyPham.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance;

        public static ProductDAO Instance 
        {
            get
            {
                if (instance ==null)
                    instance = new ProductDAO();
                return ProductDAO.instance;
            }
            set => instance = value; 
        }

        private ProductDAO() { }

        public List<ProductDTO> GetListProduct()
        {
            List<ProductDTO> list = new List<ProductDTO>();

            string query = "SELECT * FROM dbo.SanPham";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                ProductDTO product = new ProductDTO(item);
                list.Add(product);
            }

            return list;
        }

        public List<ProductDTO> GetListProduct(string query)
        {
            List<ProductDTO> list = new List<ProductDTO>();

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                ProductDTO product = new ProductDTO(item);
                list.Add(product);
            }

            return list;
        }


        public void InsertProduct(string maSP, string tenSP, int giaNhap, int giaBan, int soLuong)
        {
            int insert = DataProvider.Instance.ExecuteNonQuery("exec usp_InsertProduct @maSP , @tenSP , @giaNhap , @giaBan , @soLuong", new object[] { maSP , tenSP , giaNhap , giaBan , soLuong });
            if (insert > 0)
                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo");
        }

        public void DeleteProduct(string maSP)
        {
            DataProvider.Instance.ExecuteNonQuery("delete from [SanPham] where maSP='" + maSP + "' ");
            MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo");
        }
    }
}
