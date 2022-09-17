using System.Collections;
using Domain;
using Domain.Interfaces;
using GeoCoordinatePortable;

namespace Application;

public class ShopService : IShopService
{
    public List<Entities.Shop> SortByDistance(List<Entities.Shop> source, GeoCoordinate y)
    {
        if (source == null || y == null)
            throw new NullReferenceException("Something went wrong, try again later.");
        if (y.Latitude < -90.0 && y.Longitude < -90.0 && y.Latitude > 90.0 && y.Longitude > 90.0)
            throw new ArgumentException("Your location is unknown, try a different location.");
        if (source.Count <= 0)
            throw new ArgumentException("Unable to find shops near your location.");
        
        
        
        return source.OrderBy(x => new GeoCoordinate(x._gpsLocation.Longitude, x._gpsLocation.Latitude).GetDistanceTo(new GeoCoordinate(y.Longitude, y.Latitude))).ToList();
    }

    public List<Entities.Shop> FilterByLocation(List<Entities.Shop> source, GeoCoordinate x, int width, int height)
    {
        throw new NotImplementedException();
    }
}