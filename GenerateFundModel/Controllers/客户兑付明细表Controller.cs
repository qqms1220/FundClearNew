using FundClear.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;

namespace FundClear.Controllers
{
    public class 客户兑付明细表Controller : Controller
    {
        private Fund db = new Fund();
        [Authorize]

        public ActionResult Index(string 姓名, string 项目批次, string 分配日期, string 实际兑付日期, string 姓名过滤器, string 项目批次过滤器, string 分配日期过滤器, string 实际兑付日期过滤器, int? page)
        {
            if (姓名 != null)
            {
                page = 1;
            }
            else
            {
                姓名 = 姓名过滤器;
            }

            if (项目批次 != null)
            {
                page = 1;
            }
            else
            {
                项目批次 = 项目批次过滤器;
            }

            if (分配日期 != null)
            {
                page = 1;
            }
            else
            {
                分配日期 = 分配日期过滤器;
            }

            if (实际兑付日期 != null)
            {
                page = 1;
            }
            else
            {
                实际兑付日期 = 实际兑付日期过滤器;
            }

            ViewBag.姓名过滤器 = 姓名;
            ViewBag.项目批次过滤器 = 项目批次;
            ViewBag.分配日期过滤器 = 分配日期;
            ViewBag.实际兑付日期过滤器 = 实际兑付日期;


            IQueryable<客户兑付明细表> PayList = db.客户兑付明细表;
            if (!string.IsNullOrWhiteSpace(姓名))
            {
                PayList = PayList.Where(p => p.姓名.Contains(姓名));
            }

            if (!string.IsNullOrWhiteSpace(项目批次))
            {
                PayList = PayList.Where(p => p.项目批次.Contains(项目批次));
            }

            if (!string.IsNullOrWhiteSpace(分配日期))
            {
                DateTime dt;
                DateTime.TryParse(分配日期, out dt);
                if (dt != null)
                {
                    PayList = PayList.Where(p => p.分配日期 == dt);
                }
            }

            if (!string.IsNullOrWhiteSpace(实际兑付日期))
            {
                DateTime dt;
                DateTime.TryParse(实际兑付日期, out dt);
                if (dt != null)
                {
                    PayList = PayList.Where(p => p.实际兑付日期 == dt);
                }
            }
            int pageNumber = (page ?? 1);         
            return View(PayList.OrderBy(p => p.ID).ToPagedList(pageNumber, Config.pageSize));
        }

        public ActionResult Export()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Export(string searchString, string 项目批次, string 分配日期, string 实际兑付日期)
        {
            IQueryable<客户兑付明细表> PayList = db.客户兑付明细表;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                PayList = PayList.Where(p => p.姓名.Contains(searchString));
            }

            if (!string.IsNullOrWhiteSpace(项目批次))
            {
                PayList = PayList.Where(p => p.项目批次.Contains(项目批次));
            }

            if (!string.IsNullOrWhiteSpace(分配日期))
            {
                DateTime dt;
                DateTime.TryParse(分配日期, out dt);
                if (dt != null)
                {
                    PayList = PayList.Where(p => p.分配日期 == dt);
                }
            }

            if (!string.IsNullOrWhiteSpace(实际兑付日期))
            {
                DateTime dt;
                DateTime.TryParse(实际兑付日期, out dt);
                if (dt != null)
                {
                    PayList = PayList.Where(p => p.实际兑付日期 == dt);
                }
            }

            if (searchString != null || 项目批次 != null || 分配日期 != null|| 实际兑付日期 != null)
            {
                ExportToExcel(searchString, 项目批次, 分配日期, 实际兑付日期);
            }
            return View(PayList.ToList());
        }

        // GET: 客户兑付明细表/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客户兑付明细表 客户兑付明细表 = db.客户兑付明细表.Find(id);
            if (客户兑付明细表 == null)
            {
                return HttpNotFound();
            }
            return View(客户兑付明细表);
        }

        // GET: 客户兑付明细表/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客户兑付明细表/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,进账日期,姓名,金额,本金归还账户,分配日期,收益率,到期本金,到期收益,收益分配账户,购买期限,分配方式,客户入账账户,项目批次,备注")] 客户兑付明细表 客户兑付明细表)
        {
            if (ModelState.IsValid)
            {
                db.客户兑付明细表.Add(客户兑付明细表);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客户兑付明细表);
        }

        // GET: 客户兑付明细表/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客户兑付明细表 客户兑付明细表 = db.客户兑付明细表.Find(id);
            if (客户兑付明细表 == null)
            {
                return HttpNotFound();
            }
            return View(客户兑付明细表);
        }

        // POST: 客户兑付明细表/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,进账日期,姓名,金额,本金开户行,本金账号,本金户名,收益开户行,收益账号,收益户名,起息日期,分配日期,实际兑付日期,收益率,到期本金,到期收益,收益分配账户,购买期限,分配方式,客户入账账户,项目批次,备注")] 客户兑付明细表 客户兑付明细表)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客户兑付明细表).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客户兑付明细表);
        }

        // GET: 客户兑付明细表/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客户兑付明细表 客户兑付明细表 = db.客户兑付明细表.Find(id);
            if (客户兑付明细表 == null)
            {
                return HttpNotFound();
            }
            return View(客户兑付明细表);
        }

        // POST: 客户兑付明细表/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客户兑付明细表 客户兑付明细表 = db.客户兑付明细表.Find(id);
            db.客户兑付明细表.Remove(客户兑付明细表);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult ExportToExcel(string searchString, string 项目批次, string 分配日期, string 实际兑付日期)
        {
            var grid = new System.Web.UI.WebControls.GridView();          

            IQueryable<客户兑付明细表> PayList = db.客户兑付明细表;           

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                PayList = PayList.Where(p => p.姓名.Contains(searchString));
            }

            if (!string.IsNullOrWhiteSpace(项目批次))
            {
                PayList = PayList.Where(p => p.项目批次.Contains(项目批次));
            }

            if (!string.IsNullOrWhiteSpace(分配日期))
            {
                DateTime dt;
                DateTime.TryParse(分配日期, out dt);
                if (dt != null)
                {
                    PayList = PayList.Where(p => p.分配日期 == dt);
                }
            }

            if (!string.IsNullOrWhiteSpace(实际兑付日期))
            {
                DateTime dt;
                DateTime.TryParse(实际兑付日期, out dt);
                if (dt != null)
                {
                    PayList = PayList.Where(p => p.实际兑付日期 == dt);
                }
            }

            var customerPayList = PayList.ToList();

            foreach (var pay in customerPayList)
            {
                pay.收益账号 = pay.收益账号.Replace(" ", "");
                pay.本金账号 = pay.本金账号.Replace(" ", "");
            }

            grid.DataSource = customerPayList;
            grid.DataBind();        
            GridViewToExcel(grid, "客户兑付明细表.xls");
            return View("Index");
        }

        //1）  文本：vnd.ms-excel.numberformat:@
        //2）  日期：vnd.ms-excel.numberformat:yyyy/mm/dd
        //3）  数字：vnd.ms-excel.numberformat:#,##0.00
        //4）  货币：vnd.ms-excel.numberformat:￥#,##0.00
        //5）  百分比：vnd.ms-excel.numberformat: #0.00%
        public static void GridViewToExcel(System.Web.UI.WebControls.GridView grid1, string exportFile)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", string.Format("attachment;filename={0}", exportFile));
            System.Web.HttpContext.Current.Response.Charset = "UTF-8";
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
            for (int i = 0; i < grid1.Rows.Count; i++)
            {
                for (int k = 0; k < grid1.Rows[i].Cells.Count; k++)
                {
                    if (k == 1 || k == 10||k==11||k==12)
                    {
                        grid1.Rows[i].Cells[k].Attributes.Add("style", "vnd.ms-excel.numberformat:yyyy-mm-dd");
                    }
                    if (k == 5 || k == 8)
                    {
                        grid1.Rows[i].Cells[k].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    }
                    if (k == 3 || k == 14 || k == 15)
                    {
                        grid1.Rows[i].Cells[k].Attributes.Add("style", "vnd.ms-excel.numberformat:#,##0.00");
                    }                   
                }
            }
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            grid1.RenderControl(hw);
            System.Web.HttpContext.Current.Response.Write(tw.ToString());
            System.Web.HttpContext.Current.Response.End();
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
