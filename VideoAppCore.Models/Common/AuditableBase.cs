using System;

namespace VideoAppCore.Models
{
    public class AuditableBase
    {
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
