﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnityCard.Models
{
    public class RetailerLocation
    {
        public int Id { get; set; }

        [Required]
        public int RetailerId { get; set; }
        [JsonIgnore]
        public virtual Retailer Retailer { get; set; }

        [Required]
        public string Name { get; set; }

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

        [Required]
        public DateTime UpdatedTimestamp { get; set; }

        public RetailerLocation()
        {
        }

        public RetailerLocation(int retailerId, string name, string street, string number, int zipCode, string city, string country)
        {
            RetailerId = retailerId;
            Name = name;
            Street = street;
            Number = number;
            ZipCode = zipCode;
            City = city;
            Country = country;
            UpdatedTimestamp = DateTime.UtcNow;
        }
    }
}
