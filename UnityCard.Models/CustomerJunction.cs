using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCard.Models
{
    public class CustomerJunction
    {
        public int Id { get; set; }

        public int LoyaltyCardId { get; set; }
        public LoyaltyCard LoyaltyCard { get; set; }

        public int RetailerId { get; set; }
        public Retailer Retailer { get; set; }

        public virtual ICollection<LoyaltyPoint> LoyaltyPoints { get; set; }
    }
}
