using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCard.Models
{
    public class Offer
    {
        public int Id { get; set; }

        public int RetailerId { get; set; }
        public virtual Retailer Retailer { get; set; }

        [Required]
        public string OfferDemand { get; set; }

        [Required]
        public string OfferReceive { get; set; }

        [Required]
        public DateTime CreatedTimestamp { get; set; }
    }
}
