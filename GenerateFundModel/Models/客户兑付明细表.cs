namespace FundClear.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class 客户兑付明细表
    {
        public int ID { get; set; }

    //    public int Contract_ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? 进账日期 { get; set; }

        [StringLength(50)]
        public string 姓名 { get; set; }

        public decimal 金额 { get; set; }

        [StringLength(200)]
        public string 本金开户行 { get; set; }

        [StringLength(100)]
        public string 本金账号 { get; set; }

        [StringLength(100)]
        public string 本金户名 { get; set; }

        [StringLength(200)]
        public string 收益开户行 { get; set; }

        [StringLength(100)]
        public string 收益账号 { get; set; }

        [StringLength(100)]
        public string 收益户名 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? 起息日期 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? 分配日期 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? 实际兑付日期 { get; set; }
        public decimal? 收益率 { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal 到期本金 { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal? 到期收益 { get; set; }

        public int? 购买期限 { get; set; }

        [StringLength(50)]
        public string 分配方式 { get; set; }

        [StringLength(100)]
        public string 客户入账账户 { get; set; }

        [StringLength(50)]
        public string 项目批次 { get; set; }

        [StringLength(100)]
        public string 备注 { get; set; }
        public int contract_id { get; set; }
    }
}
