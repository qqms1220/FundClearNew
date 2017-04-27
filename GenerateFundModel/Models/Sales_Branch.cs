namespace FundClear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sales_Branch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sales_Branch()
        {
            Fix_Contract = new HashSet<Fix_Contract>();
            FOF_Contract = new HashSet<FOF_Contract>();
            Sales_Person = new HashSet<Sales_Person>();
        }

        [Key]
        public int Branch_id { get; set; }

        [StringLength(50)]
        public string 分公司名称 { get; set; }

        [StringLength(50)]
        public string 分公司地址 { get; set; }

        [StringLength(50)]
        public string 电话 { get; set; }

        [StringLength(50)]
        public string 联系人 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fix_Contract> Fix_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FOF_Contract> FOF_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sales_Person> Sales_Person { get; set; }
    }
}
