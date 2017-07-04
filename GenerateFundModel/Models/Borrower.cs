namespace FundClear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Borrower")]
    public partial class Borrower
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Borrower()
        {
            Fix_Product = new HashSet<Fix_Product>();
        }

        [Key]
        public int Borrower_id { get; set; }

        [StringLength(200)]
        public string 融资方名字 { get; set; }

        [StringLength(200)]
        public string 融资方开户银行 { get; set; }

        [StringLength(200)]
        public string 融资方账户名 { get; set; }

        [StringLength(200)]
        public string 融资方银行账号 { get; set; }

        [StringLength(200)]
        public string 融资方电话 { get; set; }

        [StringLength(200)]
        public string 融资方电邮 { get; set; }

        [StringLength(500)]
        public string 融资方地址 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fix_Product> Fix_Product { get; set; }
    }
}
