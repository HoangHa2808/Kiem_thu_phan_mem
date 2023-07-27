using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangMyPham.DTO
{
    public class BillDTO
    {
        private int id;
        private int accountID;
        private int partnerID;
        private DateTime? dateCreate;
        private DateTime? dateCheckOut;
        private int discount;
        private double totalPrice;
        private bool type;
        private bool status;
        public string AccountName { get; set; }
        public string PartnerName { get; set; }

        public int Id { get => id; set => id = value; }
        public int AccountID { get => accountID; set => accountID = value; }
        public int PartnerID { get => partnerID; set => partnerID = value; }
        public DateTime? DateCreate { get => dateCreate; set => dateCreate = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Discount { get => discount; set => discount = value; }
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
        public bool Type { get => type; set => type = value; }
        public bool Status { get => status; set => status = value; }

        public BillDTO()
        { }

        public BillDTO(int id, int accountID, int partnerID, DateTime? dateCreate, DateTime? dateCheckOut, int discount, double totalPrice, bool type, bool status)
        {
            this.Id = id;
            this.AccountID = accountID;
            this.PartnerID = partnerID;
            this.DateCreate = dateCreate;
            this.DateCheckOut = dateCheckOut;
            this.Discount = discount;
            this.TotalPrice = totalPrice;
            this.Type = type;
            this.Status = status;
        }

        public BillDTO(int id, string accountName, string partnerName, DateTime? dateCreate, DateTime? dateCheckOut, int discount, double totalPrice, bool type)
        {
            this.Id = id;
            this.AccountName = accountName;
            this.PartnerName = partnerName;
            this.DateCreate = dateCreate;
            this.DateCheckOut = dateCheckOut;
            this.Discount = discount;
            this.TotalPrice = totalPrice;
            this.Type = type;
        }

        public BillDTO(DataRow row)
        {
            this.Id = (int)row["id"];
            this.AccountID = (int)row["accountID"];
            this.PartnerID = (int)row["partnerID"];
            this.DateCreate = (DateTime?)row["dateCreate"];
            this.DateCheckOut = (DateTime?)row["dateCheckOut"];
            this.Discount = (int)row["discount"];
            this.TotalPrice = (float)row["totalPrice"];
            this.Type = (bool)row["type"];
            this.Status = (bool)row["status"];
        }
    }
}
