using FundClear.Models;
using System.Data.Entity;
using System.Linq;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;

namespace FundClear.Controllers
{
    public class 到期项目表Controller : Controller
    {
        private Fund db = new Fund();

        // GET: 到期项目表
        public ActionResult Index()
        {
            var list = db.到期项目表.ToList();
            foreach(var item in list)
            {
                item.投资金额 = item.投资金额 / 10000;
            }
            return View(list);       
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

        public void ExportToExcel()
        {
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = db.到期项目表.ToList();
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=到期项目表.xls");
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
