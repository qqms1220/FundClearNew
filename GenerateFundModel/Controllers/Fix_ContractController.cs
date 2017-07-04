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
using System.IO;

namespace FundClear.Controllers
{
    //[Authorize]
    public class Fix_ContractController : Controller
    {
        private Fund db = new Fund();

        //[Authorize]
        // GET: Fix_Contract
        public ActionResult Index(string nameFilter, string productFilter , int? statusFilter, string name, string product, int? contract_status, int? page)
        {
            if (name != null)
            {
                page = 1;
            }
            else
            {
                name = nameFilter;
            }

            if (product != null)
            {
                page = 1;
            }
            else
            {
                product = productFilter;
            }

            if(contract_status != null)
            {
                page = 1;
            }
            else
            {
                contract_status = statusFilter;
            }

            Dictionary<string,int> status = new Dictionary<string, int>();
            string[] enum_keys = Enum.GetNames(typeof(合同状态));
            int[] enum_values = (int[])Enum.GetValues(typeof(合同状态));

            for (int i = 0; i < enum_keys.Length; i++)
            {
                status.Add(enum_keys[i], enum_values[i]);
            }
            ViewBag.contract_status = new SelectList(status, "value", "key", contract_status);
           
            ViewBag.nameFilter = name;
            ViewBag.productFilter = product;
            ViewBag.statusFilter = contract_status;
                       
            var fix_Contract = db.Fix_Contract.Include(f => f.Fix_Prod_Batch).Include(f => f.Fix_Product).Include(f => f.Sales_Branch).Include(f => f.Sales_Person);

            if (!String.IsNullOrEmpty(name))
            {            
                fix_Contract = fix_Contract.Where(f => f.投资人姓名.Contains(name));            
            }
            if (!String.IsNullOrEmpty(product))
            {
                fix_Contract = fix_Contract.Where(f => f.Fix_Product.产品名称.Contains(product));
            }
            if(contract_status != null)
            {
                fix_Contract = fix_Contract.Where(f => f.合同状态 == (合同状态)contract_status);
            }

            int pageNumber = (page ?? 1);
            return View(fix_Contract.OrderByDescending(c => c.Contract_id).ToPagedList(pageNumber,Config.pageSize));  
        }
        public ActionResult Import()
        {
            ViewBag.Message = "上传要导入的Excel文件";
            return View();
        }
        [HttpPost]
        public ActionResult Import(FormCollection collection)
        {
            UploadExcel();            
            return View();
        }

        /// <summary>
        /// 读取上传的excel文件，插入到数据库的fix_contract表中
        /// </summary>
        private void UploadExcel()
        {
            try
            {
                HttpPostedFileBase file = Request.Files["excel"];
                string path = Server.MapPath("~/Upload/");//这个控制文件上传的路径，现在默认的是网站根目录，你只需要更改"~/",就可以上传到自定义的路径了

                //判断要上传的目录是否存在，不存在的话会穿件目录文件夹
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string filename = file.FileName; //获得上传文件全路径 
                int place = filename.LastIndexOf(".") + 1; //获得文件扩展名的位置 
                string extname = filename.Substring(place).ToLower(); //获得上传文件的扩展名，全部转换为小写的

                string allowedType = "csv|xls|xlsx";//允许上传的文件类型

                long allowedSize = 512000 * 1024;//允许上传的饿文件大小，单位是字节（byte），现在默认的我设置的是500M，你自己改成你需要的。

                if (allowedType.Contains(extname) && file.ContentLength < allowedSize)
                {

                    string filePath = path + filename;//上传文件的存放路径和文件的名称，我这里就是用文件原来的名字了，你可以更改为其他的名字，如加时间的就是：path+DateTime.Now+filename 

                    file.SaveAs(filePath); //文件上传
                    ViewBag.Message = "文件上传成功";
                    //上传成功后把文件导入Fix_Contract表
                    try
                    {
                        InsertToFix_Contract(filePath);
                        ViewBag.Message = "Excel文件导入成功！";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "文件导入失败，错误信息：" + ex.Message;
                    }
                    
                }
                else
                {
                    ViewBag.Message = "文件上传失败，估计是格式不支持或者文件太大啦";                  
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }            
        }

        private void InsertToFix_Contract(string excelName)
        {
            DataTable fix_contract_tab = FundClear.Help.ReadExcel.GetExcelDatatable(excelName, "Fix_contract");
            Fund db = new Fund();          
            foreach (DataRow row in fix_contract_tab.Rows)
            {
                Fix_Contract fix_contract = new Fix_Contract();
                fix_contract.合同号 = row["合同号"].ToString().Trim() == "NULL" ? string.Empty : row["合同号"].ToString().Trim();
                fix_contract.Product_id = int.Parse(row["Product_id"].ToString());
                fix_contract.金额 = decimal.Parse(row["金额"].ToString());
                fix_contract.投资人姓名 = row["投资人姓名"].ToString();
                fix_contract.投资人身份证号 = row["投资人身份证号"].ToString();
                fix_contract.电话 = row["电话"].ToString().Trim() == "NULL" ? string.Empty : row["电话"].ToString().Trim();
                fix_contract.地址 = row["地址"].ToString().Trim() == "NULL" ? string.Empty : row["地址"].ToString().Trim();
                fix_contract.本金开户银行 = row["本金开户银行"].ToString();
                fix_contract.本金账户名 = row["本金账户名"].ToString();
                fix_contract.本金银行账号 = row["本金银行账号"].ToString();
                fix_contract.收益开户银行 = row["收益开户银行"].ToString();
                fix_contract.收益账户名 = row["收益账户名"].ToString();
                fix_contract.收益银行账号 = row["收益银行账号"].ToString();
                fix_contract.Salesperson_id = int.Parse(row["Salesperson_id"].ToString());
                fix_contract.收益率 = decimal.Parse(row["收益率"].ToString());
                fix_contract.存款月数 = int.Parse(row["存款月数"].ToString());
                fix_contract.已付收益 = 0;
                fix_contract.支付日期 = null;
                fix_contract.购买日期 = HandleDateTime(row["购买日期"].ToString().Trim());
                fix_contract.计息日期 = HandleDateTime(row["计息日期"].ToString().Trim());
                fix_contract.成立日期 = HandleDateTime(row["成立日期"].ToString().Trim());
                fix_contract.到期日期 = HandleDateTime(row["到期日期"].ToString().Trim());
                fix_contract.付息方式 =(付息方式) int.Parse(row["付息方式"].ToString());
                int 付息日;
                int.TryParse(row["付息日"].ToString(), out 付息日);
                fix_contract.付息日 = 付息日;
                fix_contract.Batch_id = int.Parse(row["Batch_id"].ToString());
                fix_contract.录入人 = row["录入人"].ToString();
                fix_contract.输入时间 = DateTime.Now;
                fix_contract.审核人 = row["审核人"].ToString();
                fix_contract.审计时间 = DateTime.Now;
                fix_contract.合同状态 = (合同状态)int.Parse(row["合同状态"].ToString());
                fix_contract.Branch_id = int.Parse(row["Branch_id"].ToString());
                fix_contract.备注 = row["备注"].ToString();
                fix_contract.已清算 = false;

                db.Fix_Contract.Add(fix_contract);
            }
            db.SaveChanges();
            db.Dispose();
        }

        private DateTime? HandleDateTime(string dateString)
        {           
            if (string.IsNullOrWhiteSpace(dateString))
            {
                return null ;
            }
            else
            {       
                DateTime dt;
                DateTime.TryParse(dateString, out dt);         
                 return dt; 
            }           
        }


        //[Authorize]
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
        //[Authorize]
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
        
        //[Authorize]
        // GET: Fix_Contract/Create
        public ActionResult Create()
        {        
            List<string> ListBatches = new List<string>();
            ListBatches.Add("请选择产品批次");
            ViewBag.Batch_id =  new SelectList(ListBatches);

            List<string> List_Sales_Person = new List<string>();
            List_Sales_Person.Add("请选择理财师");
            ViewBag.Salesperson_id = new SelectList(List_Sales_Person);

            ViewBag.Product_id = new SelectList(db.Fix_Product.Where(p=>p.产品状态 == 产品状态.在售), "Product_id", "产品名称");           
            ViewBag.Branch_id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称");
            BindContractDays(0);
            BindContractType(0);
            return View();
        }
        //[Authorize]
        // POST: Fix_Contract/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Contract_id,合同号,Product_id,金额,存款月数,付息方式,付息日,投资人姓名,电话,地址,投资人身份证号,本金开户银行,本金账户名,本金银行账号,收益开户银行,收益账户名,收益银行账号,Salesperson_id,收益率,存款月数,购买日期,计息日期,成立日期,Batch_id,Input_person_id,输入时间,Audit_person_id,审计时间,合同状态,Branch_id,备注")] Fix_Contract fix_Contract)
        {
            int ContractDays = int.Parse(Request.Form["ContractDays"].ToString());
            int ContractType = int.Parse(Request.Form["ContractType"].ToString());
            if (ModelState.IsValid)       
            {  
                //先判断成立日期,如果不为空则到期日期加上存款月数
                if(fix_Contract.成立日期 != null)
                {
                    fix_Contract.到期日期 = GetExpireDate(ContractType, ContractType == 1 ? fix_Contract.存款月数 : fix_Contract.存款天数, fix_Contract.成立日期.Value);
                }

                fix_Contract.录入人 = User.Identity.Name;
                fix_Contract.输入时间 = DateTime.Now;
                fix_Contract.存入方式 = ContractType;
                fix_Contract.存款天数 = ContractType == 2 ? ContractDays : 0;

                db.Fix_Contract.Add(fix_Contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                  
            ViewBag.Batch_id = new SelectList(db.Fix_Prod_Batch.Where(p => p.产品批次状态 == 产品批次状态.在售), "Batch_id", "批次名称", fix_Contract.Batch_id);
            ViewBag.Product_id = new SelectList(db.Fix_Product.Where(p => p.产品状态 == 产品状态.在售), "Product_id", "产品名称", fix_Contract.Product_id);         
            ViewBag.Branch_id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称", fix_Contract.Branch_id);
            ViewBag.Salesperson_id = new SelectList(db.Sales_Person.OrderBy(i => i.理财师姓名), "Sales_Person_Id", "理财师姓名", fix_Contract.Salesperson_id);
            BindContractDays(ContractDays);
            BindContractType(ContractType);
            return View(fix_Contract);
        }
        //[Authorize]
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
            ViewBag.Product_id = new SelectList(db.Fix_Product, "Product_id", "产品名称", fix_Contract.Product_id);
            ViewBag.Batch_id = new SelectList(db.Fix_Prod_Batch.Where(b => b.Product_id == fix_Contract.Product_id), "Batch_id", "批次名称", fix_Contract.Batch_id); 
            ViewBag.Branch_id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称", fix_Contract.Branch_id);
            ViewBag.Salesperson_id = new SelectList(db.Sales_Person.OrderBy(i => i.理财师姓名), "Sales_Person_Id", "理财师姓名", fix_Contract.Salesperson_id);
            BindContractDays(fix_Contract.存款天数);
            BindContractType(fix_Contract.存入方式);
            ViewData["PayType"] = fix_Contract.付息方式.Value.ToString() == "季末付" ? "true" : "false";
            return View(fix_Contract);
        }
        //[Authorize]
        // POST: Fix_Contract/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Contract_id,合同号,Product_id,金额,存款月数,付息方式,付息日,投资人姓名,投资人身份证号,电话,地址,本金开户银行,本金账户名,本金银行账号,收益开户银行,收益账户名,收益银行账号,Salesperson_id,收益率,存款月数,购买日期,计息日期,成立日期,Batch_id,Input_person_id,输入时间,Audit_person_id,审计时间,合同状态,Branch_id,备注,已清算")] Fix_Contract fix_Contract)
        {
            int ContractDays = int.Parse(Request.Form["ContractDays"].ToString());
            int ContractType = int.Parse(Request.Form["ContractType"].ToString());
            if (ModelState.IsValid)
            {  
                //先判断成立日期,如果不为空则到期日期加上存款月数
                if (fix_Contract.成立日期 != null)
                {
                    fix_Contract.到期日期 = GetExpireDate(ContractType, ContractType == 1 ? fix_Contract.存款月数 : fix_Contract.存款天数, fix_Contract.成立日期.Value);
                }

                fix_Contract.录入人 = User.Identity.Name;
                fix_Contract.输入时间 = DateTime.Now;
                fix_Contract.存入方式 = ContractType;
                fix_Contract.存款天数 = ContractType == 2 ? ContractDays : 0;

                db.Entry(fix_Contract).State = EntityState.Modified;
                db.SaveChanges();
                Response.Write("<script language=javascript>history.go(-2);</script>");
             //   return RedirectToAction("Index");
           
            }
            BindContractDays(ContractDays);
            BindContractType(ContractType);
            ViewBag.Batch_id = new SelectList(db.Fix_Prod_Batch.Where(p=>p.产品批次状态 == 产品批次状态.在售), "Batch_id", "批次名称", fix_Contract.Batch_id);
            ViewBag.Product_id = new SelectList(db.Fix_Product.Where(p => p.产品状态 == 产品状态.在售), "Product_id", "产品名称", fix_Contract.Product_id);
            ViewBag.Branch_id = new SelectList(db.Sales_Branch, "Branch_id", "分公司名称", fix_Contract.Branch_id);
            ViewBag.Salesperson_id = new SelectList(db.Sales_Person.OrderBy(i => i.理财师姓名), "Sales_Person_Id", "理财师姓名", fix_Contract.Salesperson_id);
            return View(fix_Contract);
        }
        //[Authorize]
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
        //[Authorize]
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

        public JsonResult Batches(int Product_ID)
        {
            var BatchList = db.Fix_Prod_Batch.Where(b => b.Product_id == Product_ID).Select(p => new { p.Batch_id, p.批次名称 }).ToList();           
            return Json(BatchList);
        }

        public JsonResult Sales_Persons(int Branch_ID)
        {
            var SalesList = db.Sales_Person.Where(s => s.Branch_Id == Branch_ID).Select(p => new { p.Sales_Person_Id, p.理财师姓名 }).OrderBy(a => a.理财师姓名).ToList();
            return Json(SalesList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 绑定存入方式并赋初始值
        /// </summary>
        /// <param name="value"></param>
        private void BindContractType(int value)
        {
            List<SelectListItem> List_ContractType = new List<SelectListItem>();
            foreach (int item in Enum.GetValues(typeof(存入方式)))
            {
                List_ContractType.Add(new SelectListItem() { Text = Enum.GetName(typeof(存入方式), item), Value = item.ToString(), Selected = item == value });
            }
            ViewData["ContractType"] = List_ContractType;
        }

        /// <summary>
        /// 绑定存款天数并赋初始值
        /// </summary>
        /// <param name="value"></param>
        private void BindContractDays(int value)
        {
            List<SelectListItem> List_ContractDays = new List<SelectListItem>();
            List_ContractDays.Add(new SelectListItem() { Text = "30天", Value = "30", Selected = value == 30 });
            List_ContractDays.Add(new SelectListItem() { Text = "60天", Value = "60", Selected = value == 60 });
            List_ContractDays.Add(new SelectListItem() { Text = "90天", Value = "90", Selected = value == 90 });
            List_ContractDays.Add(new SelectListItem() { Text = "180天", Value = "180", Selected = value == 180 });
            ViewBag.ContractDays = List_ContractDays;
        }

        /// <summary>
        /// 获取到期日期
        /// </summary>
        /// <param name="ContractType">存入方式（按月/按天）</param>
        /// <param name="ContractTerm">存入期限</param>
        /// <param name="CreateDate">存入日期</param>
        /// <returns></returns>
        private DateTime GetExpireDate(int ContractType,int ContractTerm, DateTime CreateDate)
        {
            switch (ContractType)
            {
                case 1:
                    return CreateDate.AddMonths(ContractTerm);
                case 2:
                    return CreateDate.AddDays(ContractTerm);
                default:
                    return CreateDate.AddMonths(ContractTerm);
            }
        }
    }
}
