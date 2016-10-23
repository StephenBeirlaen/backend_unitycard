using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCard.Models
{
    public class LoyaltyCard
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedTimestamp { get; set; } //Voor stephen: is datetime correct?
    }
}
