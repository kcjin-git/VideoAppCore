using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWork.Models
{
    public class WeeklyReport
    {
        public int ID { get; set; }

        public DateTime FROM_DATE { get; set; }
        public DateTime TO_DATE { get; set; }

        public string PART { get; set; }
        public string AUTHOR { get; set; }
        
        public string TITLE { get; set; }
        public string CONTENT { get; set; }
    }
}
