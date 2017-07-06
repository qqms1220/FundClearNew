namespace FundClear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class 到期项目表
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string 融资方 { get; set; }

        [StringLength(50)]
        public string 合伙企业 { get; set; }
              
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime 还款日期 { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal? 投资金额 { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal? 回款本金 { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal? 收益金额 { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal? 服务费金额 { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal? 回款金额 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? 打款日期 { get; set; }

        public int? 产品期限 { get; set; }

        [StringLength(50)]
        public string 兑付方式 { get; set; }

        [StringLength(50)]
        public string 本期兑付内容 { get; set; }

        public int? Batch_ID { get; set; }

        public virtual Fix_Prod_Batch Fix_Prod_Batch { get; set; }
    }
}
