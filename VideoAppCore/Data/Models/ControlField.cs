using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAppCore.Data.Models
{
    public class ControlField
    {
        public string CREATOR { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public string MODIFIER { get; set; }
        public DateTime? MODIFICATION_DATE { get; set; }
        public string API_NAME { get; set; }
    }
}
