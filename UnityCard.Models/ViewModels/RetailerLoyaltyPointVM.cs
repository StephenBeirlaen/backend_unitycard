using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnityCard.Models.ViewModels
{
    public class RetailerLoyaltyPointVM
    {
        [Required]
        public Retailer Retailer { get; set; }

        [Required]
        public int LoyaltyPoints { get; set; }
    }
}
