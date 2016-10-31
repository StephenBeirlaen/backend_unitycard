using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnityCard.Models
{
    public class Retailer
    {
        public int Id { get; set; }

        [Required]
        public int RetailerCategoryId { get; set; }
        [JsonIgnore]
        public virtual RetailerCategory RetailerCategory { get; set; }

        [Required]
        public string Name { get; set; }

        public string Tagline { get; set; }

        [Required]
        public bool Chain { get; set; }

        public string LogoUrl { get; set; }

        [JsonIgnore]
        public virtual ICollection<LoyaltyPoint> LoyaltyPoints { get; set; }

        [JsonIgnore]
        public virtual ICollection<RetailerLocation> RetailerLocations { get; set; }

        [JsonIgnore]
        public virtual ICollection<Offer> Offers { get; set; }

        public Retailer()
        {
        }

        public Retailer(int retailerCategoryId, string name, string tagline, bool chain, string logoUrl)
        {
            RetailerCategoryId = retailerCategoryId;
            Name = name;
            Tagline = tagline;
            Chain = chain;
            LogoUrl = logoUrl;
        }
    }
}
