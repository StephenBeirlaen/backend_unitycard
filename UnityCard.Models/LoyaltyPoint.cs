using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCard.Models
{
    public class LoyaltyPoint
    {
        public int Id { get; set; }

        [Required]
        public int LoyaltyCardId { get; set; }
        public virtual LoyaltyCard LoyaltyCard { get; set; }

        [Required]
        public int RetailerId { get; set; }
        public virtual Retailer Retailer { get; set; }

        [Required]
        public int Points { get; set; }

        public LoyaltyPoint()
        {
        }

        public LoyaltyPoint(int loyaltyCardId, int retailerId, int points)
        {
            LoyaltyCardId = loyaltyCardId;
            RetailerId = retailerId;
            Points = points;
        }
    }
}
