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
    public class LoyaltyPoint
    {
        public int Id { get; set; }

        [Required]
        public int LoyaltyCardId { get; set; }
        [JsonIgnore]
        public virtual LoyaltyCard LoyaltyCard { get; set; }

        [Required]
        public int RetailerId { get; set; }
        [JsonIgnore]
        public virtual Retailer Retailer { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public DateTime UpdatedTimestamp { get; set; }

        public LoyaltyPoint()
        {
        }
        
        public LoyaltyPoint(int loyaltyCardId, int retailerId, int points)
        {
            LoyaltyCardId = loyaltyCardId;
            RetailerId = retailerId;
            Points = points;
            UpdatedTimestamp = DateTime.Now;
        }
    }
}
