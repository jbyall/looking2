using Looking2.Web.Domain;
using Looking2.Web.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Looking2.Web.DataAccess
{
    public interface ICategoriesRepository : IRepository<Category>
    {
        IEnumerable<Category> GetByType(ListingCategory type);
    }

    public class CategoriesRepository : Repository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(IOptions<DbSettings> settings) : base(settings)
        {
            this.Collection = Db.GetCollection<Category>(settings.Value.CategoriesCollection);
        }

        public IEnumerable<Category> GetByType(ListingCategory type)
        {
            return this.Collection.AsQueryable().Where(c => c.Type == type);
        }
    }
}
