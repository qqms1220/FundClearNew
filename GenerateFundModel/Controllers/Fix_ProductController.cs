using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FundClear.Models;

namespace FundClear.Controllers
{
    public class Fix_ProductController : Controller
    {
        private Fund db = new Fund();

        // GET: Fix_Product
        public ActionResult Index()
        {
            var fix_Product = db.Fix_Product.Include(f => f.Borrower);
            return View(fix_Product.ToList());
        }

        // GET: Fix_Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fix_Product fix_Product = db.Fix_Product.Find(id);
            if (fix_Product == null)
            {
                return HttpNotFound();
            }
            return View(fix_Product);
        }

        // GET: Fix_Product/ BatchContracts/5
        public ActionResult BatchContracts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fix_Product fix_Product = db.Fix_Product.Find(id);
            if (fix_Product == null)
            {
                return HttpNotFound();
            }           
            return View(fix_Product);
        }

        // GET: Fix_Product/Create
        public ActionResult Create()
        {
            ViewBag.Borrower_id = new SelectList(db.Borrower, "Borrower_id", "借款方名字");
            return View();
        }

        // POST: Fix_Product/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_id,产品名称,产品状态,存期月数,募集规模,最低认购金额,管理机构,托管机构,托管账户名,托管账号,托管账户开户行,服务费率,付息方式,付息日,Borrower_id")] Fix_Product fix_Product)
        {
            if (ModelState.IsValid)
            {
                db.Fix_Product.Add(fix_Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Borrower_id = new SelectList(db.Borrower, "Borrower_id", "借款方名字", fix_Product.Borrower_id);
            return View(fix_Product);
        }

        // GET: Fix_Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fix_Product fix_Product = db.Fix_Product.Find(id);
            if (fix_Product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Borrower_id = new SelectList(db.Borrower, "Borrower_id", "借款方名字", fix_Product.Borrower_id);
            return View(fix_Product);
        }

        // POST: Fix_Product/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_id,产品名称,产品状态,存期月数,募集规模,最低认购金额,管理机构,托管机构,托管账户名,托管账号,托管账户开户行,服务费率,付息方式,付息日,Borrower_id")] Fix_Product fix_Product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fix_Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Borrower_id = new SelectList(db.Borrower, "Borrower_id", "借款方名字", fix_Product.Borrower_id);
            return View(fix_Product);
        }

        // GET: Fix_Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fix_Product fix_Product = db.Fix_Product.Find(id);
            if (fix_Product == null)
            {
                return HttpNotFound();
            }
            return View(fix_Product);
        }

        // POST: Fix_Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fix_Product fix_Product = db.Fix_Product.Find(id);
            db.Fix_Product.Remove(fix_Product);
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
