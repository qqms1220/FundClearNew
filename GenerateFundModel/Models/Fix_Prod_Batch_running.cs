namespace FundClear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Fix_Prod_Batch_running
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Batch_id { get; set; }

        public DateTime? 发生时间 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? 金额 { get; set; }

        public int? Summary_id { get; set; }

        public virtual Fix_Batch_Running_Summary Fix_Batch_Running_Summary { get; set; }
    }
}
