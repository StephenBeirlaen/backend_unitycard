using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCard.Models
{
    public class AdvertisementNotification
    {
        public int RetailerId { get; set; }

        public string Title { get; set; }

        public AdvertisementNotification(int retailerId, string title)
        {
            RetailerId = retailerId;
            Title = title;
        }
    }
}
