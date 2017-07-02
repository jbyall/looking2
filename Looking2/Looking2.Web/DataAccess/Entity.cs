using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.DataAccess
{
    public class Entity
    {
        public Entity()
        {
            this.Titles = new List<string>();
            this.Descriptions = new List<string>();
            this.Contact = new List<string>();
        }
        public ObjectId Id { get; set; }
        public List<string> Titles { get; set; }
        public List<string> Descriptions { get; set; }
        public List<string> Contact { get; set; }
        public List<string> Location { get; set; }

        /// <summary>
        /// Removes empty fields from lists
        /// </summary>
        public void Clean()
        {
            try
            {
                this.Titles.RemoveAll(t => string.IsNullOrWhiteSpace(t));
            }
            catch { }

            try
            {
                this.Descriptions.RemoveAll(d => string.IsNullOrWhiteSpace(d));
            }
            catch { }
            try
            {
                this.Contact.RemoveAll(c => string.IsNullOrWhiteSpace(c));
            }
            catch { }
            try
            {
                this.Location.RemoveAll(l => string.IsNullOrWhiteSpace(l));
            }
            catch { }
        }

        /// <summary>
        /// Creates empty fields for lists to use in view
        /// </summary>
        public void Initialize()
        {
            this.Titles = Enumerable.Repeat("", 5).ToList();
            this.Descriptions = Enumerable.Repeat("", 10).ToList();
            this.Contact = Enumerable.Repeat("", 5).ToList();
            this.Location = Enumerable.Repeat("", 5).ToList();
        }
    }

    
}
