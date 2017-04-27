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
    public class Fix_Prod_BatchController : Controller
    {
        private Fund db = new Fund();

        // GET: Fix_Prod_Batch
        public ActionResult Index(int? SelectedProduct, int? SelectedStatus)
        { 
            var fix_Prod_Batch = db.Fix_Prod_Batch.Include(f => f.Batch_Status).Include(f => f.Fix_Product);

            var products = db.Fix_Product.OrderByDescending(p => p.Product_id).ToList();
            ViewBag.SelectedProduct = new SelectList(products, "Product_id", "产品名称", SelectedProduct);
            int productID = SelectedProduct.GetValueOrDefault();

            var batch_status = db.Batch_Status.OrderBy(s => s.Batch_Status_id).ToList();
            ViewBag.SelectedStatus = new SelectList(batch_status, "Batch_Status_id", "产品批次状态", SelectedStatus);
            int batch_status_id = SelectedStatus.GetValueOrDefault();

            fix_Prod_Batch = fix_Prod_Batch.Where(b => !SelectedProduct.HasValue || b.Product_id == productID)
                .Where(b => !SelectedStatus.HasValue || b.Batch_status_id == batch_status_id)
                .OrderByDescending(d => d.Batch_id);                

            return View(fix_Prod_Batch.ToList());
        }

        // GET: Fix_Prod_Batch/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fix_Prod_Batch fix_Prod_Batch = db.Fix_Prod_Batch.Find(id);
            if (fix_Prod_Batch == null)
            {
                return HttpNotFound();
            }
            return View(fix_Prod_Batch);
        }

        // GET: Fix_Prod_Batch/Create
        public ActionResult Create()
        {
            ViewBag.Batch_status_id = new SelectList(db.Batch_Status, "Batch_Status_id", "产品批次状态");
            ViewBag.Product_id = new SelectList(db.Fix_Product, "Product_id", "产品名称");
            return View();
        }

        // POST: Fix_Prod_Batch/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Batch_id,批次名称,批次金额,Product_id,Batch_status_id")] Fix_Prod_Batch fix_Prod_Batch)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(User.Identity.Name))
                {
                    fix_Prod_Batch.批次创建人 = string.Empty;
                } else
                {
                    fix_Prod_Batch.批次创建人 = User.Identity.Name;
                }

                fix_Prod_Batch.批次创建时间 = DateTime.Now;
           
                db.Fix_Prod_Batch.Add(fix_Prod_Batch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Batch_status_id = new SelectList(db.Batch_Status, "Batch_Status_id", "产品批次状态", fix_Prod_Batch.Batch_status_id);
            ViewBag.Product_id = new SelectList(db.Fix_Product, "Product_id", "产品名称", fix_Prod_Batch.Product_id);
            return View(fix_Prod_Batch);
        }

        // GET: Fix_Prod_Batch/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fix_Prod_Batch fix_Prod_Batch = db.Fix_Prod_Batch.Find(id);
            if (fix_Prod_Batch == null)
            {
                return HttpNotFound();
            }
            ViewBag.Batch_status_id = new SelectList(db.Batch_Status, "Batch_Status_id", "产品批次状态", fix_Prod_Batch.Batch_status_id);
            ViewBag.Product_id = new SelectList(db.Fix_Product, "Product_id", "产品名称", fix_Prod_Batch.Product_id);
            return View(fix_Prod_Batch);
        }

        // POST: Fix_Prod_Batch/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Batch_id,批次名称,批次金额,批次创建时间,Product_id,Batch_status_id")] Fix_Prod_Batch fix_Prod_Batch)
        {
            if (ModelState.IsValid)
            {
               if (string.IsNullOrEmpty(User.Identity.Name))
                {
                    fix_Prod_Batch.批次创建人 = string.Empty;
                }
                else
                {
                    fix_Prod_Batch.批次创建人 = User.Identity.Name;
                }

                fix_Prod_Batch.批次创建时间 = DateTime.Now;


                db.Entry(fix_Prod_Batch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Batch_status_id = new SelectList(db.Batch_Status, "Batch_Status_id", "产品批次状态", fix_Prod_Batch.Batch_status_id);
            ViewBag.Product_id = new SelectList(db.Fix_Product, "Product_id", "产品名称", fix_Prod_Batch.Product_id);
            return View(fix_Prod_Batch);
        }

        // GET: Fix_Prod_Batch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fix_Prod_Batch fix_Prod_Batch = db.Fix_Prod_Batch.Find(id);
            if (fix_Prod_Batch == null)
            {
                return HttpNotFound();
            }
            return View(fix_Prod_Batch);
        }

        // POST: Fix_Prod_Batch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fix_Prod_Batch fix_Prod_Batch = db.Fix_Prod_Batch.Find(id);
            db.Fix_Prod_Batch.Remove(fix_Prod_Batch);
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
