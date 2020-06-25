using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PaymentWebAPI.Models;

namespace PaymentWebAPI.Controllers
{
    public class OrderController : ApiController
    {
    }

    //Get action methods of the previous section
    //public IHttpActionResult PostNewOrder(OrderViewModel order)
    //{
        //if (!ModelState.IsValid)
        //    return BadRequest("Not a valid model");

        /*
        using (var ctx = new SchoolDBEntities())
        {
            ctx.Students.Add(new Student()
            {
                StudentID = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName
            });

            ctx.SaveChanges();
        }

        order.Amount = 103.50;
        order.CustomerId = 1;
        order.OrderId = 3;
        */

        //return Ok();
    //}

}
