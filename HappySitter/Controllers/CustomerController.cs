using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappySitter.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult GoogleSearch()
        {
            return View();
        }

        public ActionResult SeachList()
        {
            return View();
        }

        public ActionResult Payment()
        {
            return View();
        }
        public ActionResult Review()
        {
            return View();
        }
    }
}