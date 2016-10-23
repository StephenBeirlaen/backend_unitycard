using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCard.Models
{
    public class Retailer
    {
        public int Id { get; set; }
        public int RetailerCategoryId { get; set; }
        public string Name { get; set; }
        public string Tagline { get; set; }
        public bool Chain { get; set; }
        public string LogoUrl { get; set; }

    }
}
