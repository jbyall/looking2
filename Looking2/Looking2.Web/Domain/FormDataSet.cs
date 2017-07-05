using Looking2.Web.DataAccess;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.Domain
{
    public class FormDataSet : Entity
    {
        public string FormName { get; set; }
        public string Title0Label { get; set; }
        public string Title1Label { get; set; }
        public string Description0Label { get; set; }
        public string Description1Label { get; set; }
        public string Description2Label { get; set; }
        public string Description3Label { get; set; }
        public string Description4Label { get; set; }
        public string Description5Label { get; set; }
        public string Description6Label { get; set; }
        public string Description7Label { get; set; }
        public string Description8Label { get; set; }
        public string Description9Label { get; set; }
        public string Contact0Label { get; set; }
        public string Contact1Label { get; set; }
        public string Contact2Label { get; set; }
        public string Contact3Label { get; set; }
        public string Contact4Label { get; set; }
        public string BragLabel { get; set; }
        public string LongDescriptionLabel { get; set; }
        public string HeadingPartial { get; set; }
        public string PageTitle { get; set; }

    }
}
