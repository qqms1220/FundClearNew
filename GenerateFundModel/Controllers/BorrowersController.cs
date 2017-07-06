using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FundClear.Models;
using PagedList;

namespace FundClear.Controllers
{
    public class BorrowersController : Controller
    {
        private Fund db = new Fund();
        [Authorize]
        // GET: Student
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
          
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var borrowers = from s in db.Borrower select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                borrowers = borrowers.Where(s => s.融资方名字.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    borrowers = borrowers.OrderByDescending(s => s.融资方名字);
                    break;                
                default:  // Name ascending 
                    borrowers = borrowers.OrderBy(s => s.融资方名字);
                    break;
            }

            int pageSize = Config.pageSize;
            int pageNumber = (page ?? 1);
            return View(borrowers.ToPagedList(pageNumber, pageSize));
        }
        [Authorize]
        // GET: Borrowers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower borrower = db.Borrower.Find(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        // GET: Borrowers/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: Borrowers/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Borrower_id,融资方名字,融资方开户银行,融资方账户名,融资方银行账号,融资方电话,融资方电邮,融资方地址")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                db.Borrower.Add(borrower);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(borrower);
        }
        [Authorize]
        // GET: Borrowers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower borrower = db.Borrower.Find(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }
        [Authorize]
        // POST: Borrowers/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Borrower_id,融资方名字,融资方开户银行,融资方账户名,融资方银行账号,融资方电话,融资方电邮,融资方地址")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrower).State = EntityState.Modified;
                db.SaveChanges();
                string url = Request.QueryString["returnurl"] != null ? Request.QueryString["returnurl"].ToString() : "";
                if (url != "")
                    Response.Redirect(url);
                else
                    return RedirectToAction("Index"); 
            }
            return View(borrower);
        }
        [Authorize]
        // GET: Borrowers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower borrower = db.Borrower.Find(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }
        [Authorize]
        // POST: Borrowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Borrower borrower = db.Borrower.Find(id);
            db.Borrower.Remove(borrower);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
