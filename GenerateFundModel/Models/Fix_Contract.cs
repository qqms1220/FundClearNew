namespace FundClear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
       

    public partial class Fix_Contract
    {
        [Key]
        public int Contract_id { get; set; }

        [StringLength(50)]
        public string 合同号 { get; set; }

        public int Product_id { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal 金额 { get; set; }

        [Display(Name = "投资人")]
        public int Investor_id { get; set; }

        [StringLength(50)]
        public string 投资人姓名 { get; set; }

        [StringLength(50)]
        public string 投资人身份证号 { get; set; }

        [StringLength(50)]
        public string 本金账户名 { get; set; }

        [StringLength(50)]
        public string 本金银行账号 { get; set; }

        [StringLength(50)]
        public string 本金开户银行 { get; set; }

        [StringLength(50)]
        public string 收益账户名 { get; set; }

        [StringLength(50)]
        public string 收益银行账号 { get; set; }

        [StringLength(50)]
        public string 收益开户银行 { get; set; }


        [Display(Name = "理财师/渠道")]
        public int? Salesperson_id { get; set; }

        [Required]
        public decimal 收益率 { get; set; }

        public int 存款月数 { get; set; }

        public decimal 已付收益 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? 购买日期 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? 成立日期 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? 到期日期 { get; set; }

        public 付息方式? 付息方式 { get; set; }

        public int? 付息日 { get; set; }


        [Display(Name = "产品批次")]
        public int Batch_id { get; set; }

        public int? Input_person_id { get; set; }

        public DateTime? 输入时间 { get; set; }

        public int? Audit_person_id { get; set; }

        public DateTime? 审计时间 { get; set; }
     
        public 合同状态 合同状态 { get; set; }

        [Display(Name = "分公司")]
        public int? Branch_id { get; set; }

        public string 备注 { get; set; }

        public virtual Fix_Prod_Batch Fix_Prod_Batch { get; set; }

        public virtual Fix_Product Fix_Product { get; set; }

        public virtual Investor Investor { get; set; }

        public virtual Sales_Branch Sales_Branch { get; set; }

        public virtual Sales_Person Sales_Person { get; set; }
    }
}
