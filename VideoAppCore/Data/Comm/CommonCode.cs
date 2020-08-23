using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TeamWork.Data.Comm
{
    public class CommonCode : ControlField
    {
        [Key]
        public int ID { get; set; }

        public DateTime EXPR_DATE { get; set; }
        public string GROUP_CODE { get; set; }
        public string CODE { get; set; }
        public string CODE_NAME { get; set; }
    }
}
