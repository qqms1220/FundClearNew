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
                borrowers = borrowers.Where(s => s.借款方名字.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    borrowers = borrowers.OrderByDescending(s => s.借款方名字);
                    break;                
                default:  // Name ascending 
                    borrowers = borrowers.OrderBy(s => s.借款方名字);
                    break;
            }

            int pageSize = Config.pageSize;
            int pageNumber = (page ?? 1);
            return View(borrowers.ToPagedList(pageNumber, pageSize));
        }

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

        // POST: Borrowers/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Borrower_id,借款方名字,借款方开户银行,借款方账户名,借款方银行账号,借款方电话,借款方电邮")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                db.Borrower.Add(borrower);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(borrower);
        }

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

        // POST: Borrowers/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Borrower_id,借款方名字,借款方开户银行,借款方账户名,借款方银行账号,借款方电话,借款方电邮")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrower).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(borrower);
        }

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
