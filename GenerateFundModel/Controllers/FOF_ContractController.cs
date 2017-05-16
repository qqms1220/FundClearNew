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
    public class FOF_ContractController : Controller
    {
        private Fund db = new Fund();

        // GET: FOF_Contract
        public ActionResult Index()
        {
            var fOF_Contract = db.FOF_Contract.Include(f => f.FOF_Product).Include(f => f.Investor).Include(f => f.Sales_Branch).Include(f => f.Sales_Person);
            return View(fOF_Contract.ToList());
        }

        // GET: FOF_Contract/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOF_Contract fOF_Contract = db.FOF_Contract.Find(id);
            if (fOF_Contract == null)
            {
                return HttpNotFound();
            }
            return View(fOF_Contract);
        }

        // GET: FOF_Contract/Create
        public ActionResult Create()
        {     
            ViewBag.Product_Id = new SelectList(db.FOF_Product, "Product_id", "产品名称");
            ViewBag.Investor_id = new SelectList(db.Investor, "Investor_id", "姓名");
            ViewBag.Branch_id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称");
            ViewBag.SalesPerson_id = new SelectList(db.Sales_Person, "Sales_Person_Id", "理财师姓名");
            return View();
        }

        // POST: FOF_Contract/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Contract_id,Product_Id,购买金额,购买份额,赎回金额,Investor_id,投资人姓名,投资人身份证号,投资人开户银行,投资人银行账号,投资人银行账户名,SalesPerson_id,购买日期,Branch_id,Input_person_id,录入时间,Audit_person_id,审核时间,合同状态")] FOF_Contract fOF_Contract)
        {
            if (ModelState.IsValid)
            {
                var investor = db.Investor.Where(i => i.Investor_id == fOF_Contract.Investor_id).FirstOrDefault();
                fOF_Contract.投资人姓名 = investor.姓名;
                fOF_Contract.投资人开户银行 = investor.本金开户行;
                fOF_Contract.投资人身份证号 = investor.证件号码;
                fOF_Contract.投资人银行账号 = investor.本金账号;
                fOF_Contract.投资人银行账户名 = investor.本金户名;

                db.FOF_Contract.Add(fOF_Contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
     
            ViewBag.Product_Id = new SelectList(db.FOF_Product, "Product_id", "产品名称", fOF_Contract.Product_Id);
            ViewBag.Investor_id = new SelectList(db.Investor, "Investor_id", "姓名", fOF_Contract.Investor_id);
            ViewBag.Branch_id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称", fOF_Contract.Branch_id);
            ViewBag.SalesPerson_id = new SelectList(db.Sales_Person, "Sales_Person_Id", "理财师姓名", fOF_Contract.SalesPerson_id);
            return View(fOF_Contract);
        }

        // GET: FOF_Contract/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOF_Contract fOF_Contract = db.FOF_Contract.Find(id);
            if (fOF_Contract == null)
            {
                return HttpNotFound();
            }

            ViewBag.Product_Id = new SelectList(db.FOF_Product, "Product_id", "产品名称", fOF_Contract.Product_Id);
            ViewBag.Investor_id = new SelectList(db.Investor, "Investor_id", "姓名", fOF_Contract.Investor_id);
            ViewBag.Branch_id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称", fOF_Contract.Branch_id);
            ViewBag.SalesPerson_id = new SelectList(db.Sales_Person, "Sales_Person_Id", "理财师姓名", fOF_Contract.SalesPerson_id);
            return View(fOF_Contract);
        }

        // POST: FOF_Contract/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Contract_id,Product_Id,购买金额,购买份额,赎回金额,Investor_id,投资人姓名,投资人身份证号,投资人开户银行,投资人银行账号,投资人银行账户名,SalesPerson_id,购买日期,Branch_id,Input_person_id,录入时间,Audit_person_id,审核时间,合同状态")] FOF_Contract fOF_Contract)
        {
            if (ModelState.IsValid)
            {
                var investor = db.Investor.Where(i => i.Investor_id == fOF_Contract.Investor_id).FirstOrDefault();
                fOF_Contract.投资人姓名 = investor.姓名;
                fOF_Contract.投资人开户银行 = investor.本金开户行;
                fOF_Contract.投资人身份证号 = investor.证件号码;
                fOF_Contract.投资人银行账号 = investor.本金账号;
                fOF_Contract.投资人银行账户名 = investor.本金户名;

                db.Entry(fOF_Contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
    
            ViewBag.Product_Id = new SelectList(db.FOF_Product, "Product_id", "产品名称", fOF_Contract.Product_Id);
            ViewBag.Investor_id = new SelectList(db.Investor, "Investor_id", "姓名", fOF_Contract.Investor_id);
            ViewBag.Branch_id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称", fOF_Contract.Branch_id);
            ViewBag.SalesPerson_id = new SelectList(db.Sales_Person, "Sales_Person_Id", "理财师姓名", fOF_Contract.SalesPerson_id);
            return View(fOF_Contract);
        }

        // GET: FOF_Contract/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOF_Contract fOF_Contract = db.FOF_Contract.Find(id);
            if (fOF_Contract == null)
            {
                return HttpNotFound();
            }
            return View(fOF_Contract);
        }

        // POST: FOF_Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FOF_Contract fOF_Contract = db.FOF_Contract.Find(id);
            db.FOF_Contract.Remove(fOF_Contract);
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
