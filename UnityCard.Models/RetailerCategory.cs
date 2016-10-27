using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCard.Models
{
    public class RetailerCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Retailer> Retailers { get; set; }

        public RetailerCategory()
        {
        }

        public RetailerCategory(string name)
        {
            Name = name;
        }
    }
}
