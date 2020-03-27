using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappySitter.Controllers
{
    public class SitterController : Controller
    {
        // GET: Sitter
        public ActionResult CheckAppointment()
        {
            return View();
        }
        public ActionResult WorkTimeSet()
        {
            return View();
        }
        public ActionResult Information()
        {
            return View();
        }
        public ActionResult ContactQM()
        {
            return View();
        }
        public ActionResult SitterIndex()
        {
            return View();
        }
    }
}