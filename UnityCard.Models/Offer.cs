using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCard.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public int RetailerId { get; set; }
        public string OfferDemand { get; set; }
        public string OfferReceive { get; set; }
        public DateTime CreatedTimestamp { get; set; } //Voor stephen: is datetime correct?

    }
}
