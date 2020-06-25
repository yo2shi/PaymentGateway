using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PaymentWebAPI.Models;
using PaymentWebAPI.Logger;
using System.Collections;

namespace PaymentWebAPI.Controllers
{
    public class PaymentGatewayController : ApiController
    {

        //public ActionResult Index()
        //{
        OrderViewModel data = new OrderViewModel();
        DefaultLogger logger = new DefaultLogger();

        //data.OrderId = int.Parse(Request.QueryString["OrderId"]);
        //data.CustomerId = Request.QueryString["CustomerId"];
        //data.Amount = Request.QueryString["Amount"];
        //return View(data);
        //}

        OrderViewModel[] orders = new OrderViewModel[]{
         new OrderViewModel { OrderId = 1, CustomerId = 4, Amount = 147 },            
         new OrderViewModel { OrderId = 2, CustomerId = 9, Amount = 263 },
         new OrderViewModel { OrderId = 3, CustomerId = 7, Amount = 65 }
        };

        static Random r = new Random();
        static string cardType = "VISA";

        Transaction[] maskedtransactions;
        Transaction[] transactions = new Transaction[]{
         new Transaction { TransactionId = "MCB" + r.Next(99) + cardType + r.Next(99), MerchantId = 4, Amount = 147 , IsSuccessful = true ,
                            Card = new CardDetails { CardNumber = "1122334455667788", ExpiryDate = DateTime.Parse("30/09/2024"), Currency = "MUR", CVV = 435 } },
         new Transaction { TransactionId = "MCB" + r.Next(99) + cardType + r.Next(99), MerchantId = 9, Amount = 263 , IsSuccessful = false ,
                            Card = new CardDetails { CardNumber = "8899300411117788", ExpiryDate = DateTime.Parse("31/08/2023"), Currency = "MUR", CVV = 379 } },
         new Transaction { TransactionId = "MCB" + r.Next(99) + cardType + r.Next(99), MerchantId = 7, Amount = 65 , IsSuccessful = true ,
                            Card = new CardDetails { CardNumber = "7788300411115098", ExpiryDate = DateTime.Parse("30/06/2021"), Currency = "MUR", CVV = 145 } },
         new Transaction { TransactionId = "SMB" + r.Next(99) + cardType + r.Next(99), MerchantId = 4, Amount = 69 , IsSuccessful = true ,
                            Card = new CardDetails { CardNumber = "5566778830041111", ExpiryDate = DateTime.Parse("30/04/2022"), Currency = "EUR", CVV = 649 } },
         new Transaction { TransactionId = "MCB" + r.Next(99) + cardType + r.Next(99), MerchantId = 9, Amount = 46 , IsSuccessful = true ,
                            Card = new CardDetails { CardNumber = "4444300360078888", ExpiryDate = DateTime.Parse("31/03/2025"), Currency = "USD", CVV = 867 } },
         new Transaction { TransactionId = "MCB" + r.Next(99) + cardType + r.Next(99), MerchantId = 7, Amount = 23.6 , IsSuccessful = true ,
                            Card = new CardDetails { CardNumber = "3333222277778009", ExpiryDate = DateTime.Parse("28/02/2023"), Currency = "GBP", CVV = 321 } },
         new Transaction { TransactionId = "SMB" + r.Next(99) + cardType + r.Next(99), MerchantId = 4, Amount = 58 , IsSuccessful = false ,
                            Card = new CardDetails { CardNumber = "2222111144447777", ExpiryDate = DateTime.Parse("31/01/2024"), Currency = "MUR", CVV = 692 } },
         new Transaction { TransactionId = "MCB" + r.Next(99) + cardType + r.Next(99), MerchantId = 7, Amount = 245 , IsSuccessful = true ,
                            Card = new CardDetails { CardNumber = "1001200234434564", ExpiryDate = DateTime.Parse("31/08/2022"), Currency = "MUR", CVV = 809 } },
         new Transaction { TransactionId = "HSBC" + r.Next(99) + cardType + r.Next(99), MerchantId = 7, Amount = 74 , IsSuccessful = true ,
                            Card = new CardDetails { CardNumber = "1111222233334444", ExpiryDate = DateTime.Parse("30/06/2023"), Currency = "MUR", CVV = 657 } }

      };


        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Currency { get; set; }
        public int CVV { get; set; }



        public IEnumerable<OrderViewModel> GetAllOrders()
        {
            return orders;
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            logger.AppendToLog("Get all transactions method called : " + System.DateTime.Now.ToString("dd / MM / yyyy") + "   at: " + System.DateTime.Now.ToString("HH: mm:ss"));;
            //logger.AppendToLog("\n");

            Transaction[] ts;
            ts = PoupulateMaskedTransactions();
            return ts;
        }

        public Transaction[] PoupulateMaskedTransactions()
        {
            //ArrayList ts = new ArrayList();
            Transaction[] ts = new Transaction[transactions.Length];
            int i = 0;
            foreach (Transaction t in transactions)
            {
                Transaction maskt = new Transaction();
                maskt.TransactionId = t.TransactionId;
                maskt.MerchantId = t.MerchantId;
                maskt.Amount = t.Amount;
                maskt.IsSuccessful = t.IsSuccessful;

                CardDetails mcard = new CardDetails();
                mcard.CardNumber = Mask(t.Card.CardNumber, 0, t.Card.CardNumber.Length, 'X');
                mcard.Currency = t.Card.Currency;
                mcard.CVV = t.Card.CVV;
                mcard.ExpiryDate = t.Card.ExpiryDate ;
                //mcard.ExpiryDate = DateTime.Parse(t.Card.ExpiryDate.Month + "/" + t.Card.ExpiryDate.Year);
                maskt.Card = mcard;

                //ts.Append(maskt);
                ts[i] = maskt;
                i++;
            }

            maskedtransactions = ts;
            return maskedtransactions;
        }


        public IEnumerable<Transaction> GetMerchantTransaction(int id)
        {
            if (maskedtransactions == null || maskedtransactions.Length == 0 ){
                Transaction[] ts;
                ts = PoupulateMaskedTransactions();
            }

            var merchantTransactions = maskedtransactions.Where((mt) => mt.MerchantId == id);
            logger.AppendToLog("Get MerchantTransaction() method called : " + System.DateTime.Now.ToString("dd / MM / yyyy") + "   at: " + System.DateTime.Now.ToString("HH: mm:ss")); ;
            //logger.AppendToLog("\n");

            return merchantTransactions;
        }

        public IHttpActionResult GetOrder(int id)
        {
            var order = orders.FirstOrDefault((p) => p.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        public string Get()
        {
            string s = "Welcome To Web API";
            //s += "\n" + GetAllOrders();
                
            return s ;
        }


        public List<string> Get(int Id)
        {
            Boolean isSuccessful = false;
            string strg1 = "";
            string strg2 = "";
            string transactionNumber = "";

            BankResponse bankResponse;
            bankResponse = ProcessPayment(Id);
            isSuccessful = bankResponse.status;

            logger.AppendToLog("Get method called : " + System.DateTime.Now.ToString("dd / MM / yyyy") + "   at: " + System.DateTime.Now.ToString("HH: mm:ss")); ;
            //logger.AppendToLog("\n");

            if (isSuccessful)
            {

                //transactionNumber = GenerateTransactionNumber();
                transactionNumber = bankResponse.transactionID;
                strg1 = "Order successfully placed.";
                strg2 = "The payment was successful. The transaction id is : " + transactionNumber;

            } else
            {
                strg1 = "The payment could not be processed.";
                strg2 = "Please check the card and bank details";
            }

            // Insert Transaction in the backend. 
            // All sucessful and failed transactions should be added to the database here so that the merchants can track all their transactions. 

            return new List<string> {
                strg1,
                strg2
            };
        }

        public string GetX(OrderViewModel order)
        {

            OrderViewModel m = new OrderViewModel();

            Boolean isSuccessful = false;
            BankResponse bankResponse;
            bankResponse = ProcessPayment(order.OrderId);
            isSuccessful = bankResponse.status;

            return m.GetValues(order);

        }

        public IHttpActionResult Post(OrderViewModel order, ItemDetails[] details)
        {
            OrderViewModel ord = order;
            var ordDetails = details;
            return Ok();
        }

        /*
        public IHttpActionResult PostNewOrder(OrderViewModel order)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");


            var ord = new OrderViewModel();
            ord.Amount = 100;
            ord.CustomerId = 1;
            ord.OrderId = 44;

            return Ok();
        }
        */

        public BankResponse ProcessPayment(int orderid)
        {
            Boolean isSuccessful = false;

            // For the sake of simplicity, an order has been hard coded.
            OrderViewModel order = new OrderViewModel();
            order.Amount = 100;
            order.CustomerId = 1;
            order.OrderId = orderid;

            logger.AppendToLog("Get ProcessPayment method called : " + System.DateTime.Now.ToString("dd / MM / yyyy") + "   at: " + System.DateTime.Now.ToString("HH: mm:ss")); ;

            CardDetails cardDetails = new CardDetails();
            if (orderid < 5)
            {
                cardDetails.CardNumber = "3000111122223333";
            }
            else
            {
                cardDetails.CardNumber = "4400111122223333";
            }
            cardDetails.ExpiryDate = DateTime.Parse("31/12/2024");
            cardDetails.Currency = "MUR";
            cardDetails.CVV = 177;

            order.Card = cardDetails;

            BankSimulator bank = new BankSimulator();
            BankResponse bankResponse = bank.ProcessCardPayment(order.Card, order.Amount);
            isSuccessful = bankResponse.status;

            return bankResponse;
        }

        //public static string Mask(this string source, int start, int maskLength)
        //{
        //    return source.Mask(start, maskLength, 'X');
        //}

        
        //public string Mask(this string source, int start, int maskLength, char maskCharacter)
        public string Mask(string source, int start, int maskLength, char maskCharacter)
        {
            if (start > source.Length - 1)
            {
                throw new ArgumentException("Start position is greater than string length");
            }

            if (maskLength > source.Length)
            {
                throw new ArgumentException("Mask length is greater than string length");
            }

            if (start + maskLength > source.Length)
            {
                throw new ArgumentException("Start position and mask length imply more characters than are present");
            }

            string mask = new string(maskCharacter, maskLength);
            string unMaskStart = source.Substring(0, start);
            string unMaskEnd = source.Substring(start + maskLength, source.Length - maskLength);

            return unMaskStart + mask + unMaskEnd;
        }
        //*/


    }
}
