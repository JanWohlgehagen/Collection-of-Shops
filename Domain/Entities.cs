namespace Domain;

public class Entities
{
    public class Shop
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string website { get; set; }
        public GPSLocation _gpsLocation;

        public Shop()
        {
            _gpsLocation = new GPSLocation();
        }
    }

    public class GPSLocation
    {
        public GPSLocation()
        {
            
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}