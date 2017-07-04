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
        [Authorize]
        // GET: Sales_Person
        public ActionResult Index(int? SelectedBranch, string searchString)
        {
            var sales_Person = db.Sales_Person.Include(s => s.Sales_Branch);
            var branches = db.Sales_Branch.OrderByDescending(b => b.分公司名称).ToList();
            ViewBag.SelectedBranch = new SelectList(branches, "Branch_id", "分公司名称", SelectedBranch);
            int branch_id = SelectedBranch.GetValueOrDefault();

            sales_Person = sales_Person.Where(s => !SelectedBranch.HasValue || s.Branch_Id == branch_id)
                .OrderByDescending(d => d.Sales_Person_Id);

            if (!String.IsNullOrEmpty(searchString))
            {
                sales_Person = sales_Person.Where(s => s.理财师姓名.Contains(searchString));
            }           
            return View(sales_Person.ToList());
        }
        [Authorize]
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
        [Authorize]
        // GET: Sales_Person/Create
        public ActionResult Create()
        {
            ViewBag.Branch_Id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称");
            return View();
        }
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
