using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Domain
{
    public class EventFieldSet : FormFieldSet
    {
        public EventType Type { get; set; }
        public EventCategory Category { get; set; }
        
        // Unique labels
        public string PriceLabel { get; set; }
        public string AdmissionInfoLabel { get; set; }
        public string DateLabel { get; set; }
    }
}
