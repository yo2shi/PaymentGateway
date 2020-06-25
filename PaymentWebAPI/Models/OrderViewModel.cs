using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PaymentWebAPI.Models
{
    public class OrderViewModel
    {
        //[Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public CardDetails Card { get; set; }
        public Double Amount { get; set; }
        public ICollection<ItemDetails> Details { get; set; }





        public string GetValues(OrderViewModel ovm)
        {
            Dictionary<string, object> spParam = new Dictionary<string, object>();
            spParam.Add("@OrderId", ovm.OrderId = 1);
            string msg = "Success";

            return msg;   


        }
    }



    public class CardDetails
    {
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Currency { get; set; }
        public int CVV { get; set; }
    }

    public class ItemDetails
    {
        //[Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int OrderId { get; set; }
    }

}