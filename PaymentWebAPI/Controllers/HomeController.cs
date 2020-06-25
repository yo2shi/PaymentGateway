using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaymentWebAPI.Models;

namespace PaymentWebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            CardDetails card = new CardDetails();
            card.CardNumber = "1111222233334444";
            card.Currency = "MUR";
            card.CVV = 177;
            card.ExpiryDate = DateTime.Parse("31/12/2024");
            

            OrderViewModel data = new OrderViewModel()
            {
                OrderId = 1,
                CustomerId = 144,
                Card = card,
                Amount = 147.00
            };
            string url = string.Format("/PaymentGateway/" + data,
               data.OrderId, data.CustomerId, data.Amount);
            url = string.Format("/api/PaymentGateway/Get/1");
            return Redirect(url);

            //return View();
        }
    }
}
