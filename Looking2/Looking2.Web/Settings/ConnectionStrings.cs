using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Settings
{
    public class DbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string EventsCollection { get; set; }
        public string BusinessesCollection { get; set; }
        public string CategoriesCollection { get; set; }
        public string UsersCollection { get; set; }
        public string BusinessFormsCollection { get; set; }
        public string EventFormsCollection { get; set; }

    }
}
