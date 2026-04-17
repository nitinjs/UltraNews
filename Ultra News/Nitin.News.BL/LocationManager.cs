using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device;
using System.Device.Location;
using Nitin.News.BL.DTO;
using BenjaminSchroeter.GeoNames;

namespace Nitin.News.BL
{
    public class LocationManager
    {
        private volatile GeoPosition<GeoCoordinate> _geopos;
        private GeoPositionStatus _geosta;
        private GeoCoordinateWatcher _geolocator;

        public LocationManager()
        {
            // Initialize Geolocator.
            _geolocator = new GeoCoordinateWatcher();
            if (_geolocator == null)
                Console.WriteLine("The position is unavailable.");
            else
                Console.WriteLine("Position sensor found.");
        }

        public GeoCoordinate CurrentLocation { get; set; }

        public string LocationName
        {
            get{
                return GeoNamesOrgWebservice.FindNearbyPlaceName((decimal)CurrentLocation.Latitude, (decimal)CurrentLocation.Longitude).Name;
            }
        }

        public GeoCoordinate GetLocation()
        {
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);

            // Do not suppress prompt, and wait 1000 milliseconds to start.
            watcher.TryStart(false, TimeSpan.FromMilliseconds(8000));

            watcher.StatusChanged += delegate(object sender, GeoPositionStatusChangedEventArgs ex)
            {
                Console.WriteLine("World Status Changed!");
            };

            EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>> Handler = delegate(object sender, GeoPositionChangedEventArgs<GeoCoordinate> ex)
            {
                CurrentLocation = ex.Position.Location;
            };

            watcher.PositionChanged += Handler;

            GeoCoordinate coord = watcher.Position.Location;

            if (coord.IsUnknown != true)
            {
                Console.WriteLine("Lat: {0}, Long: {1}",
                    coord.Latitude,
                    coord.Longitude);
            }
            else
            {
                Console.WriteLine("Unknown latitude and longitude.");
            }
            CurrentLocation = coord;
            return coord;
        }
    }
}
