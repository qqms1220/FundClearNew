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
    public class Sales_PersonController : Controller
    {
        private Fund db = new Fund();

        // GET: Sales_Person
        public ActionResult Index()
        {
            var sales_Person = db.Sales_Person.Include(s => s.Sales_Branch);
            return View(sales_Person.ToList());
        }

        // GET: Sales_Person/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_Person sales_Person = db.Sales_Person.Find(id);
            if (sales_Person == null)
            {
                return HttpNotFound();
            }
            return View(sales_Person);
        }

        // GET: Sales_Person/Create
        public ActionResult Create()
        {
            ViewBag.Branch_Id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称");
            return View();
        }

        // POST: Sales_Person/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sales_Person_Id,理财师姓名,理财师身份证号,Branch_Id,电话,电子邮件,状态")] Sales_Person sales_Person)
        {
            if (ModelState.IsValid)
            {
                db.Sales_Person.Add(sales_Person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Branch_Id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称", sales_Person.Branch_Id);
            return View(sales_Person);
        }

        // GET: Sales_Person/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_Person sales_Person = db.Sales_Person.Find(id);
            if (sales_Person == null)
            {
                return HttpNotFound();
            }
            ViewBag.Branch_Id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称", sales_Person.Branch_Id);
            return View(sales_Person);
        }

        // POST: Sales_Person/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sales_Person_Id,理财师姓名,理财师身份证号,Branch_Id,电话,电子邮件,状态")] Sales_Person sales_Person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales_Person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Branch_Id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称", sales_Person.Branch_Id);
            return View(sales_Person);
        }

        // GET: Sales_Person/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_Person sales_Person = db.Sales_Person.Find(id);
            if (sales_Person == null)
            {
                return HttpNotFound();
            }
            return View(sales_Person);
        }

        // POST: Sales_Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sales_Person sales_Person = db.Sales_Person.Find(id);
            db.Sales_Person.Remove(sales_Person);
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
