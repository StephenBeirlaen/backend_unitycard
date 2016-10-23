using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCard.Models
{
    public class CustomerJunction
    {
        public int Id { get; set; }
        public int LoyaltyCardId { get; set; }
        public int RetailerId { get; set; }

    }
}
