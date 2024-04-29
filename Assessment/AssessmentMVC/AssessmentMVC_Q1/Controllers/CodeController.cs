using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssessmentMVC_Q1.Models;

namespace AssessmentMVC_Q1.Controllers
{
    public class CodeController : Controller
    {
        private NorthwindEntities NE = new NorthwindEntities();

        public ActionResult AllCustomers()
        {
            var customers = NE.Customers.Where(c => c.Country == "Germany").ToList();
            return View(customers);
        }

        public ActionResult CustomerDetails()
        {
            var customerdetails = NE.Customers
                .Where(c => c.Orders.Any(o => o.OrderID == 10248))
                .ToList();
            return View(customerdetails);
        }
        // GET: Code
        public ActionResult Index()
        {
            return View();
        }
    }
}