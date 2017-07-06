namespace FundClear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FOF_Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FOF_Product()
        {
            FOF_Contract = new HashSet<FOF_Contract>();
        }

        [Key]
        public int Product_id { get; set; }

        [StringLength(50)]
        public string 产品名称 { get; set; }

        [StringLength(50)]
        public string 管理机构 { get; set; }

        public int? 封闭月数 { get; set; }

        [StringLength(50)]
        public string 托管人 { get; set; }

        [StringLength(50)]
        public string 托管账户银行户名 { get; set; }

        [StringLength(50)]
        public string 托管账户银行账号 { get; set; }

        [StringLength(50)]
        public string 托管账号开户行 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? 募集规模 { get; set; }

        public decimal? 最低认购金额 { get; set; }

        public double? 认购费率 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FOF_Contract> FOF_Contract { get; set; }
    }
}
