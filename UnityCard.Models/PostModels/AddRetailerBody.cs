using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCard.Models.PostModels
{
    public class AddRetailerBody
    {
        public string RetailerName { get; set; }
        public string RetailerTagline { get; set; }
        public string RetailerLogoUrl { get; set; }
        public int RetailerCategoryId { get; set; }
        public bool IsChain { get; set; }
    }
}
