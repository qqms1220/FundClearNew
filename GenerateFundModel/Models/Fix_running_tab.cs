namespace FundClear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Fix_running_tab
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Contract_id { get; set; }

        public DateTime? 发生时间 { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal 金额 { get; set; }

        public int? Summary_id { get; set; }

        public virtual Fix_Running_Summary Fix_Running_Summary { get; set; }
    }
}
