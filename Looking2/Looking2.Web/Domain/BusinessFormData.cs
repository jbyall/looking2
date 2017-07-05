using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Domain
{
    public class BusinessFormData : FormDataSet
    {
        public BusinessType BusinessType { get; set; }
        public EventDescription SearchDescription { get; set; }

        // Unique labels
    }
}
