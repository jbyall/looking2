using Looking2.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Looking2.Web
{
    public static class ListingHelper
    {
        public static List<BusinessSearchCategory> GetCategories(BusinessListing business)
        {
            var result = new List<BusinessSearchCategory>();
            switch (business.BusinessType)
            {
                case BusinessType.Artists:
                    result.Add(BusinessSearchCategory.Art);
                    break;
                case BusinessType.HealthCare:
                    result.Add(BusinessSearchCategory.Healthcare);
                    break;
                case BusinessType.AltHealthCare:
                    result.Add(BusinessSearchCategory.HealthcareAlt);
                    break;
                case BusinessType.Information:
                    result.Add(BusinessSearchCategory.SupportInformation);
                    result.Add(BusinessSearchCategory.All);
                    break;
                case BusinessType.Instruction:
                    result.Add(BusinessSearchCategory.Lessons);
                    break;
                case BusinessType.Lawyers:
                    result.Add(BusinessSearchCategory.Lawyers);
                    break;
                case BusinessType.Restaurant:
                    result.Add(BusinessSearchCategory.FoodAndBeverage);
                    break;
                case BusinessType.ServiceProviders:
                    result.Add(BusinessSearchCategory.Services);
                    break;
                case BusinessType.Shopkeepers:
                    result.Add(BusinessSearchCategory.Retail);
                    break;
                case BusinessType.Support:
                    result.Add(BusinessSearchCategory.SupportInformation);
                    result.Add(BusinessSearchCategory.All);
                    break;
                case BusinessType.Other:
                    result.Add(BusinessSearchCategory.All);
                    result.Add(BusinessSearchCategory.All);
                    break;
                default:
                    break;
            }

            result.AddRange(getAdditionalCategories(DisplayHelper.ParseListingDescription(business.Descriptions)));
            result.AddRange(getAdditionalCategories(DisplayHelper.ParseBusinessTitle(business.Titles)));
            //result.AddRange(getAdditionalCategories(business.Descriptions[0]));
            //result.AddRange(getAdditionalCategories(business.Titles[0]));
            return result;
            
        }

        private static List<BusinessSearchCategory> getAdditionalCategories(string description)
        {
            var result = new List<BusinessSearchCategory>();
            var categoryDeterminator = description.ToLower();
            if (categoryDeterminator.Contains("sports"))
            {
                result.Add(BusinessSearchCategory.SportFitness);
            }

            if (categoryDeterminator.Contains("window blinds") || categoryDeterminator.Contains("storage") || categoryDeterminator.Contains("moving supplies") || categoryDeterminator.Contains("duct system"))
            {
                result.Add(BusinessSearchCategory.Services);
            }

            if (categoryDeterminator.Contains("lesson"))
            {
                result.Add(BusinessSearchCategory.Lessons);
            }

            if (categoryDeterminator.Contains("to eat") || categoryDeterminator.Contains("bakery") || categoryDeterminator.Contains("catering") || categoryDeterminator.Contains("snack") ||
                categoryDeterminator.Contains("beverage") || categoryDeterminator.Contains("candy") || categoryDeterminator.Contains("butcher"))
            {
                result.Add(BusinessSearchCategory.FoodAndBeverage);
            }

            if (categoryDeterminator.Contains("message") || categoryDeterminator.Contains("audiologist") || categoryDeterminator.Contains("dermatolog") || categoryDeterminator.Contains("health care") ||
                categoryDeterminator.Contains("pedorth") || categoryDeterminator.Contains("sleep coach"))
            {
                result.Add(BusinessSearchCategory.Healthcare);
            }

            if (categoryDeterminator.Contains("running") || categoryDeterminator.Contains("buy & sell") || categoryDeterminator.Contains("movies") ||
                categoryDeterminator.Contains("antique"))
            {
                result.Add(BusinessSearchCategory.Retail);
            }

            if (categoryDeterminator.Contains("equipment") || categoryDeterminator.Contains("swimming pools") || categoryDeterminator.Contains("sandblasting"))
            {
                result.Add(BusinessSearchCategory.Supplies);
            }

            if (categoryDeterminator.Contains("tires") || categoryDeterminator.Contains("mechanic") || categoryDeterminator.Contains("car care") || categoryDeterminator.Contains("vehicles") ||
                categoryDeterminator.Contains("alignments") || categoryDeterminator.Contains("car dealership") || categoryDeterminator.Contains("motorcycle") || categoryDeterminator.Contains("auto parts") ||
                categoryDeterminator.Contains("truck parts") || categoryDeterminator.Contains("dump trailers"))
            {
                result.Add(BusinessSearchCategory.AutoVehicle);
            }

            if (categoryDeterminator.Contains("attorney") || categoryDeterminator.Contains("lawyer"))
            {
                result.Add(BusinessSearchCategory.Lawyers);
            }

            if (categoryDeterminator.Contains("hacking") || categoryDeterminator.Contains("financial") || categoryDeterminator.Contains("cleaning") || categoryDeterminator.Contains("consulting") ||
                categoryDeterminator.Contains("fire escape") || categoryDeterminator.Contains("banking") || categoryDeterminator.Contains("memorials") || categoryDeterminator.Contains("recycling"))
            {
                result.Add(BusinessSearchCategory.Services);
            }

            if (categoryDeterminator.Contains("movie house") || categoryDeterminator.Contains("drive-in") || categoryDeterminator.Contains("gravesite") || categoryDeterminator.Contains("museum") ||
                categoryDeterminator.Contains("black history") || categoryDeterminator.Contains("game") || categoryDeterminator.Contains("arcade") || categoryDeterminator.Contains("amusement") || categoryDeterminator.Contains("convention"))
            {
                result.Add(BusinessSearchCategory.EntertainmentAttractions);
            }

            if (categoryDeterminator.Contains("salon") || categoryDeterminator.Contains("tattoo") || categoryDeterminator.Contains("hair") || categoryDeterminator.Contains("beauty"))
            {
                result.Add(BusinessSearchCategory.Salons);
            }

            var eatPattern = @"\beat\b";
            var foodPattern = @"\bfood\b";
            if (!Regex.IsMatch(categoryDeterminator,eatPattern) && !Regex.IsMatch(categoryDeterminator, foodPattern) && (categoryDeterminator.Contains("veterina") || categoryDeterminator.Contains("pets") || categoryDeterminator.Contains("fish")))
            {
                result.Add(BusinessSearchCategory.PetAnimal);
            }

            if (categoryDeterminator.Contains("adoption") || categoryDeterminator.Contains("civil rights") || categoryDeterminator.Contains("law enforcement"))
            {
                result.Add(BusinessSearchCategory.Services);
            }

            if (categoryDeterminator.Contains("public transport"))
            {
                result.Add(BusinessSearchCategory.Services);
            }

            if (categoryDeterminator.Contains("music"))
            {
                result.Add(BusinessSearchCategory.Music);
            }

            var artPattern = @"\bart\b";
            if (Regex.IsMatch(categoryDeterminator, artPattern))
            {
                result.Add(BusinessSearchCategory.Art);
            }

            var coachPattern = @"\bcoach\b";
            var coachingPattern = @"\bcoaching\b";
            var supportPattern = @"\bsupport\b";
            if (Regex.IsMatch(categoryDeterminator, coachingPattern) || Regex.IsMatch(categoryDeterminator, coachPattern) || Regex.IsMatch(categoryDeterminator, supportPattern))
            {
                result.Add(BusinessSearchCategory.SupportInformation);
            }

            return result;
        }
    }
}
