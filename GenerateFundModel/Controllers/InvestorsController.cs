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
    public class InvestorsController : Controller
    {
        private Fund db = new Fund();

        // GET: Investors
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            int pageNumber = (page ?? 1);

            if (!String.IsNullOrEmpty(searchString))
            {
                var investors = db.Investor.Where(b => b.姓名.Contains(searchString)).OrderByDescending(i => i.Investor_id);
                return View(investors.ToPagedList(pageNumber, Config.pageSize));
            } else
            {
                var investors = db.Investor.OrderByDescending(i => i.Investor_id);
                return View(investors.ToPagedList(pageNumber, Config.pageSize));
            }
        }

           
         

        // GET: Investors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Investor investor = db.Investor.Find(id);
            if (investor == null)
            {
                return HttpNotFound();
            }
            return View(investor);
        }

        // GET: Investors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Investors/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Investor_id,姓名,性别,生日,证件号码,证件类型,电话,电子邮件,加盟日期,地址,本金开户行,本金账号,本金户名,收益开户行,收益账号,收益户名")] Investor investor)
        {
            if (ModelState.IsValid)
            {
                db.Investor.Add(investor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(investor);
        }

        // GET: Investors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Investor investor = db.Investor.Find(id);
            if (investor == null)
            {
                return HttpNotFound();
            }
            return View(investor);
        }

        // POST: Investors/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Investor_id,姓名,性别,生日,证件号码,证件类型,电话,电子邮件,加盟日期,地址,本金开户行,本金账号,本金户名,收益开户行,收益账号,收益户名")] Investor investor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(investor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(investor);
        }

        // GET: Investors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Investor investor = db.Investor.Find(id);
            if (investor == null)
            {
                return HttpNotFound();
            }
            return View(investor);
        }

        // POST: Investors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Investor investor = db.Investor.Find(id);
            db.Investor.Remove(investor);
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
