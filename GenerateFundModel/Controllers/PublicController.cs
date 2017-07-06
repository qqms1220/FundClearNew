using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FundClear.Models;

namespace FundClear.Controllers
{
    public class PublicController : Controller
    {
        private Fund db = new Fund();
        // GET: Public
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void Export(string type)
        {
            var grid = new System.Web.UI.WebControls.GridView();
            string ExcelName = "";
            switch (type)
            {
                case "Sales_Person":
                    ExcelName = "理财师明细表.xls";
                    grid.DataSource = db.Sales_Person.ToList();
                    break;
                case "Sales_Branch":
                    ExcelName = "分公司明细表.xls";
                    grid.DataSource = db.Sales_Branch.ToList();
                    break;
                case "Fix_Product":
                    ExcelName = "固收产品明细表.xls";
                    grid.DataSource = db.Fix_Product.ToList();
                    break;
                case "Fix_Prod_Batch":
                    ExcelName = "产品批次明细表.xls";
                    grid.DataSource = db.Fix_Prod_Batch.ToList();
                    break;
                default:
                    ExcelName = "理财师明细表.xls";
                    grid.DataSource = db.Sales_Person.ToList();
                    break;
            }
            grid.DataBind();
            客户兑付明细表Controller.GridViewToExcel(grid, ExcelName);
        }
    }
}