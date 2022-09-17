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
        if (y.Latitude < -45.0 || y.Longitude < -45.0 || y.Latitude > 45.0 || y.Longitude > 45.0)
            throw new ArgumentOutOfRangeException("Your location is unknown, try a different location.");
        if (source.Count <= 0)
            throw new ArgumentException("Unable to find shops near your location.");
        
        
        var shopsNearby = source.OrderBy(x => new GeoCoordinate(x._gpsLocation.Longitude, x._gpsLocation.Latitude).GetDistanceTo(new GeoCoordinate(y.Longitude, y.Latitude))).ToList();

        foreach (var shop in shopsNearby.ToList())
        {
            var g = shop._gpsLocation;
            
            //If the shops in the list is outside the specified range of the plane, remove them from the list.
            if (g.Latitude < -45.0 || g.Longitude < -45.0 || g.Latitude > 45.0 || g.Longitude > 45.0)
            {
                shopsNearby.Remove(shop);
            }
        }
        
        return shopsNearby;
    }

    public List<Entities.Shop> FilterByLocation(List<Entities.Shop> source, GeoCoordinate x, int width, int height)
    {
        throw new NotImplementedException();
    }
}