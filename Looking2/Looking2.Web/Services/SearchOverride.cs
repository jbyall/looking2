using Looking2.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Looking2.Web.DataAccess;
namespace Looking2.Web.Services
{
    public interface ISearchOverride
    {
        Dictionary<EventType, string> EventDescriptionOverrides { get; set; }
        Dictionary<BusinessType, string> BusinessDescriptionOverrides { get; set; }
    }
    public class SearchOverride : ISearchOverride
    {
        public SearchOverride(IEventFormsRepo _eventForms, IBusinessFormsRepo _businessForms)
        {
            this.EventDescriptionOverrides = new Dictionary<EventType, string>();
            this.BusinessDescriptionOverrides = new Dictionary<BusinessType, string>();

            var eventForms = _eventForms.GetAll().ToList();
            foreach (var form in eventForms)
            {
                if (!string.IsNullOrWhiteSpace(form.DefaultDescriptionText))
                {
                    EventDescriptionOverrides.Add(form.Type, form.DefaultDescriptionText);
                }
            }

            var businessForms = _businessForms.GetAll().ToList();
            foreach (var form in businessForms)
            {
                if (!string.IsNullOrWhiteSpace(form.DefaultDescriptionText))
                {
                    BusinessDescriptionOverrides.Add(form.BusinessType, form.DefaultDescriptionText);
                }
            }
        }
        public Dictionary<EventType, string> EventDescriptionOverrides { get; set; }
        public Dictionary<BusinessType, string> BusinessDescriptionOverrides { get; set; }
    }
}
