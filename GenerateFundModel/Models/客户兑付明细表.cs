namespace FundClear.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class 客户兑付明细表
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? 进账日期 { get; set; }

        [StringLength(50)]
        public string 姓名 { get; set; }

        public decimal 金额 { get; set; }

        [StringLength(50)]
        public string 本金归还账户 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? 分配日期 { get; set; }

        public decimal? 收益率 { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal 到期本金 { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal? 到期收益 { get; set; }

        [StringLength(100)]
        public string 收益分配账户 { get; set; }

        public int? 购买期限 { get; set; }

        [StringLength(50)]
        public string 分配方式 { get; set; }

        [StringLength(100)]
        public string 客户入账账户 { get; set; }

        [StringLength(50)]
        public string 项目批次 { get; set; }

        [StringLength(100)]
        public string 备注 { get; set; }
    }
}
