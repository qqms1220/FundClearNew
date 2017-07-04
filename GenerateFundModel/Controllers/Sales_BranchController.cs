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
    public class Sales_BranchController : Controller
    {
        private Fund db = new Fund();
        [Authorize]
        // GET: Sales_Branch
        public ActionResult Index(string SearchString)
        {
            IQueryable<Sales_Branch> sales_Branches;
            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                SearchString = SearchString.Replace(" ", "");
                sales_Branches = db.Sales_Branch.Where(b => b.分公司名称.Contains(SearchString));
            }
            else
            {
                sales_Branches = db.Sales_Branch;
            }         

            return View(sales_Branches.ToList());
        }
        [Authorize]
        // GET: Sales_Branch/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_Branch sales_Branch = db.Sales_Branch.Find(id);
            if (sales_Branch == null)
            {
                return HttpNotFound();
            }
            return View(sales_Branch);
        }
        [Authorize]
        // GET: Sales_Branch/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: Sales_Branch/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Branch_id,分公司名称,分公司地址,电话,联系人")] Sales_Branch sales_Branch)
        {
            if (ModelState.IsValid)
            {
                db.Sales_Branch.Add(sales_Branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sales_Branch);
        }
        [Authorize]
        // GET: Sales_Branch/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_Branch sales_Branch = db.Sales_Branch.Find(id);
            if (sales_Branch == null)
            {
                return HttpNotFound();
            }
            return View(sales_Branch);
        }
        [Authorize]
        // POST: Sales_Branch/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Branch_id,分公司名称,分公司地址,电话,联系人")] Sales_Branch sales_Branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales_Branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sales_Branch);
        }
        [Authorize]
        // GET: Sales_Branch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_Branch sales_Branch = db.Sales_Branch.Find(id);
            if (sales_Branch == null)
            {
                return HttpNotFound();
            }
            return View(sales_Branch);
        }
        [Authorize]
        // POST: Sales_Branch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sales_Branch sales_Branch = db.Sales_Branch.Find(id);
            db.Sales_Branch.Remove(sales_Branch);
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
