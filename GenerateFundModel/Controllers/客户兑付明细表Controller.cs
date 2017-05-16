using FundClear.Models;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;

namespace FundClear.Controllers
{
    public class 客户兑付明细表Controller : Controller
    {
        private Fund db = new Fund();

        // GET: 客户兑付明细表
        public ActionResult Index()
        {
            return View(db.客户兑付明细表.ToList());
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
        public ActionResult Edit([Bind(Include = "ID,进账日期,姓名,金额,本金归还账户,分配日期,收益率,到期本金,到期收益,收益分配账户,购买期限,分配方式,客户入账账户,项目批次,备注")] 客户兑付明细表 客户兑付明细表)
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

        public void ExportToExcel()
        {
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = db.客户兑付明细表.ToList(); 
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=客户兑付明细表.xls");
            Response.ContentType = "application/excel";
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
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
