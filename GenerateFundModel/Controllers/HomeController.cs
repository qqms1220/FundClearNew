using System;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using FundClear.Models;

namespace FundClear.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

     
        public ActionResult About()
        {     
                 
            return View();
        }

        // POST: Home/About
        [HttpPost, ActionName("About")]
        [ValidateAntiForgeryToken]
        public ActionResult About(int id = 1)
        {
            //删除已有清算记录();
            //生成兑付明细表记录();
            //生成到期项目表();
            return RedirectToAction("Index");
        }
    }
}
