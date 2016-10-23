using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCard.Models
{
    public class LoyaltyPoint
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime Timestamp { get; set; } //Voor stephen: is datetime correct?
        public int CustomerJunctionId { get; set; }

    }
}
