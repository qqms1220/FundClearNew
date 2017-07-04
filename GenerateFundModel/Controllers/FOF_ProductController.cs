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
    [Authorize]
    public class FOF_ProductController : Controller
    {
        private Fund db = new Fund();

        // GET: FOF_Product
        public ActionResult Index()
        {
            return View(db.FOF_Product.ToList());
        }

        // GET: FOF_Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOF_Product fOF_Product = db.FOF_Product.Find(id);
            if (fOF_Product == null)
            {
                return HttpNotFound();
            }
            return View(fOF_Product);
        }

        // GET: FOF_Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FOF_Product/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_id,产品名称,管理机构,封闭月数,托管人,托管账户银行户名,托管账户银行账号,托管账号开户行,募集规模,最低认购金额,认购费率")] FOF_Product fOF_Product)
        {
            if (ModelState.IsValid)
            {
                db.FOF_Product.Add(fOF_Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fOF_Product);
        }

        // GET: FOF_Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOF_Product fOF_Product = db.FOF_Product.Find(id);
            if (fOF_Product == null)
            {
                return HttpNotFound();
            }
            return View(fOF_Product);
        }

        // POST: FOF_Product/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_id,产品名称,管理机构,封闭月数,托管人,托管账户银行户名,托管账户银行账号,托管账号开户行,募集规模,最低认购金额,认购费率")] FOF_Product fOF_Product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fOF_Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fOF_Product);
        }

        // GET: FOF_Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOF_Product fOF_Product = db.FOF_Product.Find(id);
            if (fOF_Product == null)
            {
                return HttpNotFound();
            }
            return View(fOF_Product);
        }

        // POST: FOF_Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FOF_Product fOF_Product = db.FOF_Product.Find(id);
            db.FOF_Product.Remove(fOF_Product);
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
