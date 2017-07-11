using Looking2.Web.Domain;
using Looking2.Web.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;

namespace Looking2.Web.DataAccess
{
    public interface IBusinessFormsRepo : IRepository<BusinessFormData>
    {
        BusinessFormData GetByName(string name);
    }
    public class BusinessFormsRepository : Repository<BusinessFormData>, IBusinessFormsRepo
    {
        public BusinessFormsRepository(IOptions<DbSettings> settings) : base(settings)
        {
            this.Collection = Db.GetCollection<BusinessFormData>(settings.Value.BusinessFormsCollection);
        }

        public BusinessFormData GetByName(string name)
        {
            return this.Collection.AsQueryable().Where(f => f.FormName.ToLower() == name.ToLower()).SingleOrDefault();
        }
    }
}
