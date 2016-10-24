using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCard.Models
{
    public class RetailerLocation
    {
        public int Id { get; set; }

        public int RetailerId { get; set; }
        public Retailer Retailer { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Number { get; set; } // String voor huisnummers hebben zoals 3A

        [Required]
        public int ZipCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
