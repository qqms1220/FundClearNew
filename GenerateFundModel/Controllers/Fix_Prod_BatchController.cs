﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FundClear.Models;
using PagedList;

namespace FundClear.Controllers
{
    public class Fix_Prod_BatchController : Controller
    {
        private Fund db = new Fund();
        [Authorize]
        // GET: Fix_Prod_Batch
        public ActionResult Index(int? productFilter, int?batchStatusFilter, int? SelectedProduct, int? product_batch_status,int? page)
        { 
            if(SelectedProduct != null) { page = 1; } else { SelectedProduct = productFilter; }
            if(product_batch_status != null) { page = 1; } else { product_batch_status = batchStatusFilter; }

            var products = db.Fix_Product.OrderByDescending(p => p.Product_id).ToList();
            ViewBag.SelectedProduct = new SelectList(products, "Product_id", "产品名称", SelectedProduct);

            Dictionary<string, int> status = new Dictionary<string, int>();
            string[] enum_keys = Enum.GetNames(typeof(产品批次状态));
            int[] enum_values = (int[])Enum.GetValues(typeof(产品批次状态));
            for (int i = 0; i < enum_keys.Length; i++)
            {
                status.Add(enum_keys[i], enum_values[i]);
            }
            ViewBag.product_batch_status = new SelectList(status, "value", "key", product_batch_status);

            ViewBag.productFilter = SelectedProduct;
            ViewBag.batchStatusFilter = product_batch_status;

            var fix_Prod_Batch = db.Fix_Prod_Batch.Include(f => f.Fix_Product).Include(c => c.Fix_Contract);
            if (SelectedProduct != null)
            {
                fix_Prod_Batch = fix_Prod_Batch.Where(b => b.Product_id == SelectedProduct);
            }
            if (product_batch_status != null)
            {
                fix_Prod_Batch = fix_Prod_Batch.Where(c => c.产品批次状态 == (产品批次状态)product_batch_status);
            }
           

            int pageNumber = (page ?? 1);
            return View(fix_Prod_Batch.OrderByDescending(d => d.Batch_id).ToPagedList(pageNumber, Config.pageSize));
        }
        [Authorize]
        // GET: Fix_Prod_Batch
        public ActionResult BatchContractList(int? BatchID)
        {
            if (BatchID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Batch_Contract_List = db.Fix_Contract.Where(c => c.Batch_id == BatchID &&c.合同状态 == 合同状态.生效).Include(x => x.Sales_Branch).Include(a => a.Sales_Person);
            if (Batch_Contract_List == null)
            {
                return HttpNotFound();
            }
            return View(Batch_Contract_List.ToList());
        }
        [Authorize]
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
            ViewBag.Product_id = new SelectList(db.Fix_Product, "Product_id", "产品名称");
            return View();
        }
        [Authorize]
        // POST: Fix_Prod_Batch/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Batch_id,批次名称,Product_id,批次金额,批次收益率,服务费率,付息方式,季末付息日,产品批次状态, 划款日期")] Fix_Prod_Batch fix_Prod_Batch)
        {
            if (ModelState.IsValid)
            {
                if (fix_Prod_Batch.划款日期 != null)
                {
                    DateTime dt = (DateTime)fix_Prod_Batch.划款日期;
                    var products = from p in db.Fix_Product
                                   where p.Product_id == fix_Prod_Batch.Product_id
                                   select p;
                    int n = products.FirstOrDefault().存期月数;
                    dt = dt.AddMonths(n);
                    fix_Prod_Batch.到期日期 = dt.Date;
                }

                if (string.IsNullOrEmpty(User.Identity.Name))
                {
                    fix_Prod_Batch.批次创建人 = string.Empty;
                } else
                {
                    fix_Prod_Batch.批次创建人 = User.Identity.Name;
                }

                fix_Prod_Batch.批次创建时间 = DateTime.Now;
                fix_Prod_Batch.已付收益 = 0;

                
                 db.Fix_Prod_Batch.Add(fix_Prod_Batch);
                 db.SaveChanges();
                
                return RedirectToAction("Index");
            }
     
            ViewBag.Product_id = new SelectList(db.Fix_Product, "Product_id", "产品名称", fix_Prod_Batch.Product_id);
            return View(fix_Prod_Batch);
        }
        [Authorize]
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
                 
            ViewBag.Product_id = new SelectList(db.Fix_Product, "Product_id", "产品名称", fix_Prod_Batch.Product_id);
            return View(fix_Prod_Batch);
        }
        [Authorize]
        // POST: Fix_Prod_Batch/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Batch_id,批次名称,批次创建时间,Product_id,批次金额,批次收益率,服务费率,已付收益,付息方式,季末付息日,产品批次状态,划款日期,已清算")] Fix_Prod_Batch fix_Prod_Batch)
        {
            if (ModelState.IsValid)
            {
                if (fix_Prod_Batch.划款日期 != null)
                {
                    DateTime dt = (DateTime)fix_Prod_Batch.划款日期;
                    var products = from p in db.Fix_Product
                                 where p.Product_id == fix_Prod_Batch.Product_id
                                 select p;
                    int n = products.FirstOrDefault().存期月数;
                    dt = dt.AddMonths(n);
                    fix_Prod_Batch.到期日期 = dt.Date;
                }

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
                string url = Request.QueryString["returnurl"] != null ? Request.QueryString["returnurl"].ToString() : "";
                if (url != "")
                    Response.Redirect(url);
                else
                    return RedirectToAction("Index");                
            }
   
            ViewBag.Product_id = new SelectList(db.Fix_Product, "Product_id", "产品名称", fix_Prod_Batch.Product_id);
            return View(fix_Prod_Batch);
        }
        [Authorize]
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
