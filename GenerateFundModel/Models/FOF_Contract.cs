namespace FundClear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FOF_Contract
    {
        [Key]
        public int Contract_id { get; set; }

        public int? Product_Id { get; set; }

        public decimal? 购买金额 { get; set; }

        public int? 购买份额 { get; set; }

        public decimal? 赎回金额 { get; set; }

        public int? Investor_id { get; set; }

        [StringLength(50)]
        public string 投资人姓名 { get; set; }

        [StringLength(50)]
        public string 投资人身份证号 { get; set; }

        [StringLength(50)]
        public string 投资人开户银行 { get; set; }

        [StringLength(50)]
        public string 投资人银行账号 { get; set; }

        [StringLength(50)]
        public string 投资人银行账户名 { get; set; }

        public int? SalesPerson_id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime 购买日期 { get; set; }

        public int? Branch_id { get; set; }

        public int? Input_person_id { get; set; }

        public DateTime? 录入时间 { get; set; }

        public int? Audit_person_id { get; set; }

        public DateTime? 审核时间 { get; set; }

        public 合同状态 合同状态 { get; set; }       

        public virtual FOF_Product FOF_Product { get; set; }

        public virtual Investor Investor { get; set; }

        public virtual Sales_Branch Sales_Branch { get; set; }

        public virtual Sales_Person Sales_Person { get; set; }
    }
}
