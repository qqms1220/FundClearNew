namespace FundClear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Batch_Status
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Batch_Status()
        {
            Fix_Prod_Batch = new HashSet<Fix_Prod_Batch>();
        }

        [Key]
        public int Batch_Status_id { get; set; }

        [StringLength(50)]
        public string 产品批次状态 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fix_Prod_Batch> Fix_Prod_Batch { get; set; }
    }
}
