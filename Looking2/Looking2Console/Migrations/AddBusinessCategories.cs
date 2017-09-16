using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using Looking2.Web.Domain;

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
                switch (item.BusinessType)
                {
                    case BusinessType.Artists:
                        item.Categories.Add(BusinessSearchCategory.Art);
                        break;
                    case BusinessType.HealthCare:
                        item.Categories.Add(BusinessSearchCategory.Healthcare);
                        break;
                    case BusinessType.AltHealthCare:
                        item.Categories.Add(BusinessSearchCategory.HealthcareAlt);
                        break;
                    case BusinessType.Information:
                        item.Categories.Add(BusinessSearchCategory.SupportInformation);
                        break;
                    case BusinessType.Instruction:
                        item.Categories.Add(BusinessSearchCategory.Lessons);
                        break;
                    case BusinessType.Lawyers:
                        item.Categories.Add(BusinessSearchCategory.Lawyers);
                        break;
                    case BusinessType.Restaurant:
                        item.Categories.Add(BusinessSearchCategory.FoodAndBeverage);
                        break;
                    case BusinessType.Shopkeepers:
                        item.Categories.Add(BusinessSearchCategory.Retail);
                        break;
                    case BusinessType.Support:
                        item.Categories.Add(BusinessSearchCategory.SupportInformation);
                        break;
                    default:
                        break;
                }

                var additionalCategories = getCategories(item);
                foreach (var cat in additionalCategories)
                {
                    if (!item.Categories.Contains(cat))
                    {
                        item.Categories.Add(cat);
                    }
                }


                businessCollection.FindOneAndReplace<BusinessListing>(l => l.Id == item.Id, item);

            }

            return true;
        }

        private List<BusinessSearchCategory> getCategories(BusinessListing item)
        {
            var result = new List<BusinessSearchCategory>();

            var categoryDet2 = item.Descriptions[0].ToLower();
            if (categoryDet2.Contains("sports"))
            {
                item.Categories.Add(BusinessSearchCategory.SportFitness);
            }

            if (categoryDet2.Contains("window blinds") || categoryDet2.Contains("storage") || categoryDet2.Contains("moving supplies") || categoryDet2.Contains("duct system"))
            {
                item.Categories.Add(BusinessSearchCategory.HomeServices);
            }

            if (categoryDet2.Contains("lesson"))
            {
                item.Categories.Add(BusinessSearchCategory.Lessons);
            }

            if (categoryDet2.Contains("to eat") || categoryDet2.Contains("bakery") || categoryDet2.Contains("catering") || categoryDet2.Contains("snack") ||
                categoryDet2.Contains("beverage") || categoryDet2.Contains("candy") || categoryDet2.Contains("butcher"))
            {
                item.Categories.Add(BusinessSearchCategory.FoodAndBeverage);
            }

            if (categoryDet2.Contains("message") || categoryDet2.Contains("audiologist") || categoryDet2.Contains("dermatolog") || categoryDet2.Contains("health care") ||
                categoryDet2.Contains("pedorth") || categoryDet2.Contains("sleep coach") || categoryDet2.Contains("horolog"))
            {
                item.Categories.Add(BusinessSearchCategory.Healthcare);
            }

            if (categoryDet2.Contains("running") || categoryDet2.Contains("buy & sell") || categoryDet2.Contains("movies") ||
                categoryDet2.Contains("antique"))
            {
                item.Categories.Add(BusinessSearchCategory.Retail);
            }

            if (categoryDet2.Contains("equipment") || categoryDet2.Contains("swimming pools") || categoryDet2.Contains("sandblasting"))
            {
                item.Categories.Add(BusinessSearchCategory.Supplies);
            }

            if (categoryDet2.Contains("tires") || categoryDet2.Contains("mechanic") || categoryDet2.Contains("car care") || categoryDet2.Contains("vehicles") ||
                categoryDet2.Contains("alignments") || categoryDet2.Contains("car dealership") || categoryDet2.Contains("motorcycle") || categoryDet2.Contains("auto parts") ||
                categoryDet2.Contains("truck parts") || categoryDet2.Contains("dump trailers"))
            {
                item.Categories.Add(BusinessSearchCategory.AutoVehicle);
            }

            if (categoryDet2.Contains("attorney") || categoryDet2.Contains("lawyer"))
            {
                item.Categories.Add(BusinessSearchCategory.Lawyers);
            }

            if (categoryDet2.Contains("hacking") || categoryDet2.Contains("financial") || categoryDet2.Contains("cleaning") || categoryDet2.Contains("consulting") ||
                categoryDet2.Contains("fire escape") || categoryDet2.Contains("banking") || categoryDet2.Contains("memorials") || categoryDet2.Contains("recycling"))
            {
                item.Categories.Add(BusinessSearchCategory.GeneralServices);
            }

            if (categoryDet2.Contains("movie house") || categoryDet2.Contains("drive-in") || categoryDet2.Contains("gravesite") || categoryDet2.Contains("museum") ||
                categoryDet2.Contains("black history") || categoryDet2.Contains("game") || categoryDet2.Contains("arcade") || categoryDet2.Contains("amusement") || categoryDet2.Contains("convention"))
            {
                item.Categories.Add(BusinessSearchCategory.EntertainmentAttractions);
            }

            if (categoryDet2.Contains("salon") || categoryDet2.Contains("tattoo") || categoryDet2.Contains("hair") || categoryDet2.Contains("beauty"))
            {
                item.Categories.Add(BusinessSearchCategory.Salons);
            }

            if (categoryDet2.Contains("veterina") || categoryDet2.Contains("pets") || categoryDet2.Contains("fish"))
            {
                item.Categories.Add(BusinessSearchCategory.PetAnimal);
            }

            if (categoryDet2.Contains("adoption") || categoryDet2.Contains("civil rights") || categoryDet2.Contains("law enforcement"))
            {
                item.Categories.Add(BusinessSearchCategory.HumanServices);
            }

            if (categoryDet2.Contains("public transport"))
            {
                item.Categories.Add(BusinessSearchCategory.PublicServices);
            }

            if (categoryDet2.Contains("music"))
            {
                item.Categories.Add(BusinessSearchCategory.Music);
            }

            return result;
        }
    }
}
