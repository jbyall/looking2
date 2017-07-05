using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Domain
{
    public class BusinessFieldSet : FormFieldSet
    {
        public BusinessType Type { get; set; }
        public BusinessCategory Category { get; set; }

        // Unique labels
    }
}
