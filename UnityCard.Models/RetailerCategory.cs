using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnityCard.Models
{
    public class RetailerCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime UpdatedTimestamp { get; set; }

        [JsonIgnore]
        public virtual ICollection<Retailer> Retailers { get; set; }

        public RetailerCategory()
        {
        }

        public RetailerCategory(string name)
        {
            Name = name;
            UpdatedTimestamp = DateTime.Now;
        }
    }
}
