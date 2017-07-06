namespace FundClear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Fix_Prod_Batch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fix_Prod_Batch()
        {
            Fix_Contract = new HashSet<Fix_Contract>();
            Fix_Prod_Batch_running = new HashSet<Fix_Prod_Batch_running>();
            到期项目表 = new HashSet<到期项目表>();
        }

        [Key]
        public int Batch_id { get; set; }

        [StringLength(50)]
        public string 批次名称 { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal? 批次金额 { get; set; }

        [Required]
        [Range(0.00,100.00)]
        public decimal 批次收益率 { get; set; }

        [Required]
        [Range(0.00, 100.00)]
        public decimal? 服务费率 { get; set; }

        [StringLength(50)]
        public string 批次创建人 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? 划款日期 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? 到期日期 { get; set; }

        public 付息方式 付息方式 { get; set; }

        [Range(1,30)]
        public int? 季末付息日 { get; set; }

        public decimal? 已付收益 { get; set; }

        public DateTime? 批次创建时间 { get; set; }

        public int? Product_id { get; set; }

        public 产品批次状态 产品批次状态 { get; set; }
        public bool? 已清算 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fix_Contract> Fix_Contract { get; set; }

        public virtual Fix_Product Fix_Product { get; set; }
        public virtual ICollection<Fix_Prod_Batch_running> Fix_Prod_Batch_running { get; set; }

        public virtual ICollection<到期项目表> 到期项目表 { get; set; }
    }
}
