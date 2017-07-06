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
        public int Batch_Running_ID { get; set; }
        public int Batch_id { get; set; }

        public DateTime? 发生时间 { get; set; }
               
        public decimal? 金额 { get; set; }

        public 批次流水摘要? 批次流水摘要 { get; set; }       
    }
}
