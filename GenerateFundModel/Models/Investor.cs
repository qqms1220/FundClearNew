namespace FundClear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;  

    [Table("Investor")]
    public partial class Investor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Investor()
        {
            Fix_Contract = new HashSet<Fix_Contract>();
            FOF_Contract = new HashSet<FOF_Contract>();
        }

        [Key]
        public int Investor_id { get; set; }

        [Required]
        [StringLength(50)]
        public string 姓名 { get; set; }

        public 性别 性别 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? 生日 { get; set; }

        [Required]
        [StringLength(50)]
        public string 证件号码 { get; set; }

        public 证件类型 证件类型 { get; set; }

        [StringLength(50)]
        public string 电话 { get; set; }

        [StringLength(50)]
        public string 电子邮件 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? 加盟日期 { get; set; }

        [StringLength(50)]
        public string 地址 { get; set; }

        [StringLength(50)]
        public string 开户银行 { get; set; }

        [StringLength(50)]
        public string 银行账号 { get; set; }

        [StringLength(50)]
        public string 银行户名 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fix_Contract> Fix_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FOF_Contract> FOF_Contract { get; set; }
    }
}
