using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitin.News.BL.DTO
{
    public class Location
    {
        public Location()
            : this("", "")
        {
            Country = "India";
            City = "Mumbai";
        }

        public Location(string latitude, string longitude)
        {
            Latitude = latitude;
            Longitude = longitude;

        }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        /// <summary>
        /// current country according to latitude and longitude
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// current city according to latitude and longitude
        /// </summary>
        public string City { get; set; }
    }
}
