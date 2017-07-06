namespace FundClear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;  

    [Table("Fix_Product")]

    public partial class Fix_Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fix_Product()
        {
            Fix_Contract = new HashSet<Fix_Contract>();
            Fix_Prod_Batch = new HashSet<Fix_Prod_Batch>();
        }

        [Key]
        public int Product_id { get; set; }

        [Required]
        [StringLength(50)]
        public string 产品名称 { get; set; }

        public 产品状态 产品状态 { get; set; }

        public int 存期月数 { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal? 募集规模 { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal? 最低认购金额 { get; set; }

        [StringLength(50)]
        public string 管理机构 { get; set; }

        [StringLength(50)]
        public string 托管机构 { get; set; }

        [StringLength(50)]
        public string 托管账户名 { get; set; }

        [StringLength(50)]
        public string 托管账号 { get; set; }

        [StringLength(50)]
        public string 托管账户开户行 { get; set; }

        public decimal 服务费率 { get; set; }
        public 付息方式 付息方式 { get; set; }

        [Range(1,28)]
        public int 付息日 { get; set; }

        public int Borrower_id { get; set; }

        public virtual Borrower Borrower { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fix_Contract> Fix_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fix_Prod_Batch> Fix_Prod_Batch { get; set; }
    }
}
