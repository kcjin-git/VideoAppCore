using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VideoAppCore.Data.Models
{
    public class User : ControlField
    {
        [Key]
        public int USER_ID { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public string USER_NAME { get; set; }
        public string PART_NAME { get; set; }
        public string ORGN_NAME { get; set; }
    }
}
