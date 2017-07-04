using FundClear.Models;
using System.Data.Entity;
using System.Linq;
using System;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PagedList;

namespace FundClear.Controllers
{
    public class 到期项目表Controller : Controller
    {
        private Fund db = new Fund();

        // GET: 到期项目表
        public ActionResult Index(string 借款人, string 还款日期起始, string 还款日期终止, string 合伙企业, string 借款人过滤器, string 还款日期起始过滤器, string 还款日期终止过滤器, string 合伙企业过滤器, int? page)
        {
            IQueryable<到期项目表> list = db.到期项目表;
            foreach (var item in list)
            {
                item.投资金额 = item.投资金额 / 10000;
            }
            #region 设定页码和过滤器
            if (借款人 != null)
            {
                page = 1;
            }
            else
            {
                借款人 = 借款人过滤器;
            }
            if (还款日期起始 != null)
            {
                page = 1;
            }
            else
            {
                还款日期起始=还款日期起始过滤器;
            }
            if(还款日期终止 != null)
            {
                page = 1;
            }
            else
            {
                还款日期终止 = 还款日期终止过滤器;
            }
            if(合伙企业 != null) { page = 1; }
            else{ 合伙企业 = 合伙企业过滤器; }

            ViewBag.借款人过滤器 = 借款人;
            ViewBag.还款日期起始过滤器 = 还款日期起始;
            ViewBag.还款日期终止过滤器 = 还款日期终止;
            ViewBag.合伙企业过滤器 = 合伙企业;

            #endregion
            if (!string.IsNullOrWhiteSpace(借款人))
            {
                list = list.Where(p => p.融资方.Contains(借款人));
            }

            if (!string.IsNullOrWhiteSpace(合伙企业))
            {
                list = list.Where(p => p.合伙企业.Contains(合伙企业));
            }

            if (!string.IsNullOrWhiteSpace(还款日期起始))
            {
                DateTime start_date;
                DateTime.TryParse(还款日期起始, out start_date);
                if (start_date!= null)
                {
                    list = list.Where(p => p.还款日期 >= start_date);
                }
            }

            if (!string.IsNullOrWhiteSpace(还款日期终止))
            {
                DateTime end_date;
                DateTime.TryParse(还款日期终止, out end_date);
                if (end_date != null)
                {
                    list = list.Where(p => p.还款日期 <= end_date);
                }
            }

            int pageNumber = (page ?? 1);
            return View(list.OrderBy(p => p.ID).ToPagedList(pageNumber, Config.pageSize));            
        }

        // GET: 到期项目表/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            到期项目表 到期项目表 = db.到期项目表.Find(id);
            if (到期项目表 == null)
            {
                return HttpNotFound();
            }
            return View(到期项目表);
        }

        // GET: 到期项目表/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 到期项目表/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,融资方,合伙企业,还款日期,投资金额,回款本金,收益金额,服务费金额,回款金额,打款日期,产品期限,兑付方式,本期兑付内容")] 到期项目表 到期项目表)
        {
            if (ModelState.IsValid)
            {
                db.到期项目表.Add(到期项目表);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(到期项目表);
        }

        // GET: 到期项目表/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            到期项目表 到期项目表 = db.到期项目表.Find(id);
            if (到期项目表 == null)
            {
                return HttpNotFound();
            }
            return View(到期项目表);
        }

        // POST: 到期项目表/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,融资方,合伙企业,还款日期,投资金额,回款本金,收益金额,服务费金额,回款金额,打款日期,产品期限,兑付方式,本期兑付内容")] 到期项目表 到期项目表)
        {
            if (ModelState.IsValid)
            {
                db.Entry(到期项目表).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(到期项目表);
        }

        // GET: 到期项目表/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            到期项目表 到期项目表 = db.到期项目表.Find(id);
            if (到期项目表 == null)
            {
                return HttpNotFound();
            }
            return View(到期项目表);
        }

        // POST: 到期项目表/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            到期项目表 到期项目表 = db.到期项目表.Find(id);
            db.到期项目表.Remove(到期项目表);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Export()
        {
            return View();
        }
        [HttpPost]
        public void Export(string 借款人, string 还款日期起始, string 还款日期终止, string 合伙企业)
        {
            IQueryable<到期项目表> list = db.到期项目表;
            if (!string.IsNullOrWhiteSpace(借款人))
            {
                list = list.Where(p => p.融资方.Contains(借款人));
            }

            if (!string.IsNullOrWhiteSpace(合伙企业))
            {
                list = list.Where(p => p.合伙企业.Contains(合伙企业));
            }

            if (!string.IsNullOrWhiteSpace(还款日期起始))
            {
                DateTime start_date;
                DateTime.TryParse(还款日期起始, out start_date);
                if (start_date != null)
                {
                    list = list.Where(p => p.还款日期 >= start_date);
                }
            }

            if (!string.IsNullOrWhiteSpace(还款日期终止))
            {
                DateTime end_date;
                DateTime.TryParse(还款日期终止, out end_date);
                if (end_date != null)
                {
                    list = list.Where(p => p.还款日期 <= end_date);
                }
            }
            var grid = new System.Web.UI.WebControls.GridView();          
            grid.DataSource = list.ToList();
            grid.DataBind();
            GridViewToExcel(grid, "到期项目表.xls");            
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
                for(int k = 0; k < grid1.Rows[i].Cells.Count; k++)
                {
                    if(k == 3 || k == 9)
                    {
                        grid1.Rows[i].Cells[k].Attributes.Add("style", "vnd.ms-excel.numberformat:yyyy-mm-dd");
                    }
                    if (k == 4 || k == 5 || k == 6 || k == 7 || k == 8)
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
