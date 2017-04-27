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
        }

        [Key]
        public int Batch_id { get; set; }

        [StringLength(50)]
        public string 批次名称 { get; set; }

        public decimal? 批次金额 { get; set; }

        [StringLength(50)]
        public string 批次创建人 { get; set; }

        public DateTime? 批次创建时间 { get; set; }

        public int? Product_id { get; set; }

        public int? Batch_status_id { get; set; }

        public virtual Batch_Status Batch_Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fix_Contract> Fix_Contract { get; set; }

        public virtual Fix_Product Fix_Product { get; set; }
    }
}
