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
        public int Value { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        public int CustomerJunctionId { get; set; }
        public virtual CustomerJunction CustomerJunction { get; set; }
    }
}
