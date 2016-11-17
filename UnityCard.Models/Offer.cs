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
    public class Offer
    {
        public int Id { get; set; }

        [Required]
        public int RetailerId { get; set; }
        [JsonIgnore]
        public virtual Retailer Retailer { get; set; }

        [Required]
        public string OfferDemand { get; set; }

        [Required]
        public string OfferReceive { get; set; }

        [Required]
        public DateTime CreatedTimestamp { get; set; }

        [Required]
        public DateTime UpdatedTimestamp { get; set; }

        public Offer()
        {
        }

        public Offer(int retailerId, string offerDemand, string offerReceive)
        {
            RetailerId = retailerId;
            OfferDemand = offerDemand;
            OfferReceive = offerReceive;
            CreatedTimestamp = DateTime.UtcNow;
            UpdatedTimestamp = DateTime.UtcNow;
        }
    }
}
