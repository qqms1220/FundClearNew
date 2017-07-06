namespace FundClear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;    

    [Table("Sales_Person")]
    public partial class Sales_Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sales_Person()
        {
            Fix_Contract = new HashSet<Fix_Contract>();
            FOF_Contract = new HashSet<FOF_Contract>();
        }

        [Key]
        public int Sales_Person_Id { get; set; }

        [StringLength(50)]
        public string 理财师姓名 { get; set; }

        [StringLength(50)]
        public string 理财师身份证号 { get; set; }

        public int? Branch_Id { get; set; }

        [StringLength(50)]
        public string 电话 { get; set; }

        [StringLength(50)]
        public string 电子邮件 { get; set; }       
        public 状态 状态 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fix_Contract> Fix_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FOF_Contract> FOF_Contract { get; set; }

        public virtual Sales_Branch Sales_Branch { get; set; }
    }
}
