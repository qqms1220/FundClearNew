namespace FundClear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Fix_Batch_Running_Summary
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fix_Batch_Running_Summary()
        {
            Fix_Prod_Batch_running = new HashSet<Fix_Prod_Batch_running>();
        }

        [Key]
        public int Summary_id { get; set; }

        [StringLength(50)]
        public string 摘要 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fix_Prod_Batch_running> Fix_Prod_Batch_running { get; set; }
    }
}
