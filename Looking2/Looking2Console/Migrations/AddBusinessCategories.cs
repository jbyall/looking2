using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using Looking2.Web.Domain;
using Looking2.Web;
using System.Linq;

namespace Looking2Console.Migrations
{
    public class AddBusinessCategories : Looking2Migration, ILooking2Migration
    {
        public AddBusinessCategories() : base()
        {
            this.Created = new DateTime(2017, 9, 10, 10, 42, 0);
        }
        public override bool Logic(MongoClient client, IMongoDatabase db)
        {
            var businessCollection = db.GetCollection<BusinessListing>("businesses");
            var businesses = businessCollection.AsQueryable().ToList();

            foreach (var item in businesses)
            {
                var categories = ListingHelper.GetCategories(item);
                item.Categories.AddRange(categories);
                businessCollection.FindOneAndReplace<BusinessListing>(l => l.Id == item.Id, item);
            }

            var categoriesCollection = db.GetCollection<Category>("categories");

            // Update shopkeepers to retail
            var shopKeepersCategory = categoriesCollection.AsQueryable().Single(c => c.Name == "Shopkeepers");
            shopKeepersCategory.DisplayName = "Retail";
            categoriesCollection.FindOneAndReplace<Category>(c => c.Id == shopKeepersCategory.Id, shopKeepersCategory);

            // Update Information to Support & Information
            var informationCategory = categoriesCollection.AsQueryable().Single(c => c.Name == "Information");
            informationCategory.DisplayName = "Support & Information";
            informationCategory.Description = "Do you offer support or information?";
            categoriesCollection.FindOneAndReplace<Category>(c => c.Id == informationCategory.Id, informationCategory);

            var businessCategories = categoriesCollection.AsQueryable().ToList().Where(c => c.Type == ListingCategory.Business).OrderBy(c => c.DisplayName);
            int count = 1;
            foreach (var item in businessCategories)
            {
                if (item.Name == "Other")
                {
                    item.DisplayOrder = 10;
                }
                else
                {
                    item.DisplayOrder = 10 + count;
                    count++;
                }
                categoriesCollection.FindOneAndReplace<Category>(c => c.Id == item.Id, item);
                
            }


            return true;
        }
    }
}
