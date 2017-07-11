using Looking2.Web.Domain;
using Looking2.Web.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;

namespace Looking2.Web.DataAccess
{
    public interface IEventFormsRepo : IRepository<EventFormData>
    {
        EventFormData GetByName(string name);
    }
    public class EventFormsRepository : Repository<EventFormData>, IEventFormsRepo
    {
        public EventFormsRepository(IOptions<DbSettings> settings) : base(settings)
        {
            this.Collection = Db.GetCollection<EventFormData>(settings.Value.EventFormsCollection);
        }

        public EventFormData GetByName(string name)
        {
            return this.Collection.AsQueryable().Where(f => f.FormName.ToLower() == name.ToLower()).SingleOrDefault();
        }
    }
}
