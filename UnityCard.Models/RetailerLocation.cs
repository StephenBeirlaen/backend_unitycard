using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCard.Models
{
    public class RetailerLocation
    {
        public int Id { get; set; }
        public int RetailerId { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; } //Voor stephen: kan dit geen double zijn?
        public string Longitude { get; set; } //Voor stephen: kan dit geen double zijn?
        public string Street { get; set; }
        public string Number { get; set; } //Voor stephen: kan dit geen int zijn? tenzij we een huisnummer hebben zoals 3A
        public string Zipcode { get; set; } //Voor stephen: kan dit geen int zijn? zipcode = postalcode
        public string City { get; set; }
        public string Country { get; set; }

    }
}
