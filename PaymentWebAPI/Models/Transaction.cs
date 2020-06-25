using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentWebAPI.Models
{
    public class Transaction
    {
        //[Key]
        public string TransactionId { get; set; }
        public int MerchantId { get; set; }
        public CardDetails Card { get; set; }
        public Double Amount { get; set; }
        public Boolean IsSuccessful { get; set; }
    }
}