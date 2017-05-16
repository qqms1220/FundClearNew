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
        public int Fix_running_ID { get; set; }

        public int Contract_id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime 发生时间 { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal 金额 { get; set; }

        public 合同流水摘要 合同流水摘要 { get; set; }
               
    }
}
