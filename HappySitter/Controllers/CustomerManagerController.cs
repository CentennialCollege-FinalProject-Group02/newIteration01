using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappySitter.Controllers
{
    public class CustomerManagerController : Controller
    {
        // GET: CustomerManager
        public ActionResult BlockCustomer()
        {
            return View();
        }
        public ActionResult CustomerManagerIndex()
        {
            return View();
        }
        public ActionResult SearchCustomer()
        {
            return View();
        }
    }
}