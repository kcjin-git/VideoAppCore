using System.Collections.Generic;
using System.Text;

namespace VideoAppCore.Models
{

    public class Video : AuditableBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
    }
}
