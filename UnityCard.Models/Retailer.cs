﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCard.Models
{
    public class Retailer
    {
        public int Id { get; set; }

        public int RetailerCategoryId { get; set; }
        public virtual RetailerCategory RetailerCategory { get; set; }

        [Required]
        public string Name { get; set; }

        public string Tagline { get; set; }

        [Required]
        public bool Chain { get; set; }

        public string LogoUrl { get; set; }

        public virtual ICollection<CustomerJunction> CustomerJunctions { get; set; }

        public virtual ICollection<RetailerLocation> RetailerLocations { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }
}