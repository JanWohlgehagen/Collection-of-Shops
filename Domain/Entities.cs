namespace Domain;

public class Entities
{
    public class Shop
    {
        private string Name { get; set; }
        private string Address { get; set; }
        private string website { get; set; }
        private GPSLocation _gpsLocation;

        public Shop()
        {
            _gpsLocation = new GPSLocation();
        }
    }

    public class GPSLocation
    {
        private int Latitude { get; set; }
        private int Longitude { get; set; }
    }
}