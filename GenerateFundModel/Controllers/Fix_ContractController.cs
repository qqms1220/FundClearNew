using System;
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
    public class Fix_ContractController : Controller
    {
        private Fund db = new Fund();

        // GET: Fix_Contract
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
                       
            var fix_Contract = db.Fix_Contract.Include(f => f.Fix_Prod_Batch).Include(f => f.Fix_Product).Include(f => f.Investor).Include(f => f.Sales_Branch).Include(f => f.Sales_Person);

            if (!String.IsNullOrEmpty(searchString))
            {            
                fix_Contract = fix_Contract.Where(f => f.投资人姓名.Contains(searchString));            
            }
            int pageNumber = (page ?? 1);
            return View(fix_Contract.OrderByDescending(c => c.Contract_id).ToPagedList(pageNumber,Config.pageSize));  
        }

        // GET: Fix_Contract/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fix_Contract fix_Contract = db.Fix_Contract.Find(id);
            if (fix_Contract == null)
            {
                return HttpNotFound();
            }
            return View(fix_Contract);
        }

        // GET: Fix_Contract/RunRecord/5
        public ActionResult Running(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fix_Contract fix_Contract = db.Fix_Contract.Find(id);

            if (fix_Contract == null)
            {
                return HttpNotFound();
            }

            IEnumerable<Fix_running_tab> run_list = db.Fix_running_tab.Where(t => t.Contract_id == fix_Contract.Contract_id).ToList();
            return View(run_list);
        }

        // GET: Fix_Contract/Create
        public ActionResult Create()
        {      
            ViewBag.Batch_id = new SelectList(db.Fix_Prod_Batch.Where(p => p.产品批次状态 == 产品批次状态.在售), "Batch_id", "批次名称");
            ViewBag.Product_id = new SelectList(db.Fix_Product.Where(p=>p.产品状态 == 产品状态.在售), "Product_id", "产品名称");
            ViewBag.Investor_id = new SelectList(db.Investor.OrderBy(i => i.姓名) , "Investor_id", "姓名");
            ViewBag.Branch_id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称");
            ViewBag.Salesperson_id = new SelectList(db.Sales_Person.OrderBy(i => i.理财师姓名), "Sales_Person_Id", "理财师姓名");
            return View();
        }

        // POST: Fix_Contract/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Contract_id,合同号,Product_id,金额,Investor_id,投资人姓名,投资人身份证号,本金开户银行,本金账户名,本金银行账号,收益开户银行,收益账户名,收益银行账号,Salesperson_id,收益率,存款月数,购买日期,成立日期,Batch_id,Input_person_id,输入时间,Audit_person_id,审计时间,合同状态,Branch_id,备注")] Fix_Contract fix_Contract)
        {
            if (ModelState.IsValid)
            {
                //从投资人表中找到所选择的投资人的信息，填入合同中，这样做的目的是合同一旦生效就不准修改
                //1 留作原始备份
                //2 将来CRM系统可以填入这些信息
                var investor = db.Investor.Where(i => i.Investor_id == fix_Contract.Investor_id).FirstOrDefault();
                fix_Contract.投资人姓名 = investor.姓名;
                fix_Contract.投资人身份证号 = investor.证件号码;

                fix_Contract.本金开户银行 = investor.本金开户行;               
                fix_Contract.本金银行账号 = investor.本金账号;
                fix_Contract.本金账户名 = investor.本金户名;

                fix_Contract.收益开户银行 = investor.收益开户行;
                fix_Contract.收益银行账号 = investor.收益账号;
                fix_Contract.收益账户名 = investor.收益户名;

                //找到所选择的产品，把产品的信息填入合同表
                var product = db.Fix_Product.Where(p => p.Product_id == fix_Contract.Product_id).FirstOrDefault();
                fix_Contract.存款月数 = product.存期月数;
                fix_Contract.付息方式 = product.付息方式;
                fix_Contract.付息日 = product.付息日;

                //先判断成立日期,如果不为空则到期日期加上存款月数
                if(fix_Contract.成立日期 != null)
                {
                    fix_Contract.到期日期 = fix_Contract.成立日期.Value.AddMonths(fix_Contract.存款月数);
                }


                db.Fix_Contract.Add(fix_Contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                  
            ViewBag.Batch_id = new SelectList(db.Fix_Prod_Batch.Where(p => p.产品批次状态 == 产品批次状态.在售), "Batch_id", "批次名称", fix_Contract.Batch_id);
            ViewBag.Product_id = new SelectList(db.Fix_Product.Where(p => p.产品状态 == 产品状态.在售), "Product_id", "产品名称", fix_Contract.Product_id);
            ViewBag.Investor_id = new SelectList(db.Investor.OrderBy(i => i.姓名), "Investor_id", "姓名", fix_Contract.Investor_id);
            ViewBag.Branch_id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称", fix_Contract.Branch_id);
            ViewBag.Salesperson_id = new SelectList(db.Sales_Person.OrderBy(i => i.理财师姓名), "Sales_Person_Id", "理财师姓名", fix_Contract.Salesperson_id);
            return View(fix_Contract);
        }

        // GET: Fix_Contract/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fix_Contract fix_Contract = db.Fix_Contract.Find(id);
            if (fix_Contract == null)
            {
                return HttpNotFound();
            }
      
            ViewBag.Batch_id = new SelectList(db.Fix_Prod_Batch.Where(p => p.产品批次状态 == 产品批次状态.在售), "Batch_id", "批次名称", fix_Contract.Batch_id);
            ViewBag.Product_id = new SelectList(db.Fix_Product.Where(p => p.产品状态 == 产品状态.在售), "Product_id", "产品名称", fix_Contract.Product_id);
            ViewBag.Investor_id = new SelectList(db.Investor.OrderBy(i => i.姓名), "Investor_id", "姓名", fix_Contract.Investor_id);
            ViewBag.Branch_id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称", fix_Contract.Branch_id);
            ViewBag.Salesperson_id = new SelectList(db.Sales_Person.OrderBy(i => i.理财师姓名), "Sales_Person_Id", "理财师姓名", fix_Contract.Salesperson_id);
            return View(fix_Contract);
        }

        // POST: Fix_Contract/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Contract_id,合同号,Product_id,金额,Investor_id,投资人姓名,投资人身份证号,本金开户银行,本金账户名,本金银行账号,收益开户银行,收益账户名,收益银行账号,Salesperson_id,收益率,存款月数,已付收益,购买日期,成立日期,Batch_id,Input_person_id,输入时间,Audit_person_id,审计时间,合同状态,Branch_id,备注")] Fix_Contract fix_Contract)
        {
            if (ModelState.IsValid)
            {
                //从投资人表中找到所选择的投资人的信息，填入合同中，这样做的目的是合同一旦生效就不准修改
                //1 留作原始备份
                //2 将来CRM系统可以填入这些信息
                var investor = db.Investor.Where(i => i.Investor_id == fix_Contract.Investor_id).FirstOrDefault();
                fix_Contract.投资人姓名 = investor.姓名;
                fix_Contract.投资人身份证号 = investor.证件号码;

                fix_Contract.本金开户银行 = investor.本金开户行;
                fix_Contract.本金银行账号 = investor.本金账号;
                fix_Contract.本金账户名 = investor.本金户名;

                fix_Contract.收益开户银行 = investor.收益开户行;
                fix_Contract.收益银行账号 = investor.收益账号;
                fix_Contract.收益账户名 = investor.收益户名;


                //找到所选择的产品，把产品的信息填入合同表
                var product = db.Fix_Product.Where(p => p.Product_id == fix_Contract.Product_id).FirstOrDefault();
                fix_Contract.存款月数 = product.存期月数;
                fix_Contract.付息方式 = product.付息方式;
                fix_Contract.付息日 = product.付息日;

                //先判断成立日期,如果不为空则到期日期加上存款月数
                if (fix_Contract.成立日期 != null)
                {
                    fix_Contract.到期日期 = fix_Contract.成立日期.Value.AddMonths(fix_Contract.存款月数);
                }

                db.Entry(fix_Contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }    
            ViewBag.Batch_id = new SelectList(db.Fix_Prod_Batch.Where(p=>p.产品批次状态 == 产品批次状态.在售), "Batch_id", "批次名称", fix_Contract.Batch_id);
            ViewBag.Product_id = new SelectList(db.Fix_Product.Where(p => p.产品状态 == 产品状态.在售), "Product_id", "产品名称", fix_Contract.Product_id);
            ViewBag.Investor_id = new SelectList(db.Investor.OrderBy(i => i.姓名), "Investor_id", "姓名", fix_Contract.Investor_id);
            ViewBag.Branch_id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称", fix_Contract.Branch_id);
            ViewBag.Salesperson_id = new SelectList(db.Sales_Person.OrderBy(i => i.理财师姓名), "Sales_Person_Id", "理财师姓名", fix_Contract.Salesperson_id);
            return View(fix_Contract);
        }

        // GET: Fix_Contract/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fix_Contract fix_Contract = db.Fix_Contract.Find(id);
            if (fix_Contract == null)
            {
                return HttpNotFound();
            }
            return View(fix_Contract);
        }

        // POST: Fix_Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fix_Contract fix_Contract = db.Fix_Contract.Find(id);
            db.Fix_Contract.Remove(fix_Contract);
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
