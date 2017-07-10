using Looking2.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.ViewModels
{
    public class EventFormViewModel
    {
        public string Id { get; set; }
        public EventType Type { get; set; }
        //public EventDescription Category { get; set; }
        public string FormName { get; set; }
        public string Title0Label { get; set; }
        public string Title1Label { get; set; }
        public string Title2Label { get; set; }
        public string Title3Label { get; set; }
        public string Title4Label { get; set; }
        public string VenueLabel { get; set; }
        public string DefaultDescriptionText { get; set; }
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
        
        // Unique labels
        public string PriceLabel { get; set; }
        public string AdmissionInfoLabel { get; set; }
        public string DateLabel { get; set; }
    }
}
