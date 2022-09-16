using System.Collections;
using Domain;
using Domain.Interfaces;
using GeoCoordinatePortable;

namespace Application;

public class ShopService : IShopService
{
    public List<Entities.Shop> SortByDistance(List<Entities.Shop> source, GeoCoordinate y)
    {
        

        if (y.Latitude >= -90.0 && y.Longitude >= -90.0 && y.Latitude <= 90.0 && y.Longitude <= 90.0)
        { 
            List<Entities.Shop> sortedList = source.OrderBy(x => new GeoCoordinate(x._gpsLocation.Longitude, x._gpsLocation.Latitude).GetDistanceTo(new GeoCoordinate(y.Longitude, y.Latitude))).ToList();
            return sortedList;
        }
        else
        {
            throw new ArgumentException();
        }
    }

    public List<Entities.Shop> FilterByLocation(List<Entities.Shop> source, GeoCoordinate x, int width, int height)
    {
        throw new NotImplementedException();
    }
}