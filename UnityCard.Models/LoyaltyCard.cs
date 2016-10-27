using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCard.Models
{
    public class LoyaltyCard
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime CreatedTimestamp { get; set; }

        public virtual ICollection<LoyaltyPoint> LoyaltyPoints { get; set; }

        public LoyaltyCard()
        {
        }

        public LoyaltyCard(string userId)
        {
            UserId = userId;
            CreatedTimestamp = DateTime.Now;
        }
    }
}
