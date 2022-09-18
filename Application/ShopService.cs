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

        // Filter shops that are outside the bounds of the plane, then order by distance to the GeoCoordinate y
        return source.Where(x => x._gpsLocation.Latitude >= -45.0 && x._gpsLocation.Longitude >= -45.0 && x._gpsLocation.Latitude <= 45.0 && x._gpsLocation.Longitude <= 45.0)
            .OrderBy(x => new GeoCoordinate(x._gpsLocation.Longitude, x._gpsLocation.Latitude).GetDistanceTo(new GeoCoordinate(y.Longitude, y.Latitude)))
            .ToList();
    }

    public List<Entities.Shop> FilterByLocation(List<Entities.Shop> source, GeoCoordinate y, int width, int height)
    {
        if (source == null || y == null)
            throw new NullReferenceException("Something went wrong, try again later.");
        if (source.Count <= 0)
            throw new ArgumentException("Unable to find shops near your location.");

        var filteredSource = source;

        if (width < 0) // Negative width values
           filteredSource = filteredSource.Where(s =>
                s._gpsLocation.Latitude <= y.Latitude && s._gpsLocation.Latitude >= y.Latitude + width).ToList();
        else // Positive width values
            filteredSource = filteredSource.Where(s =>
                s._gpsLocation.Latitude >= y.Latitude && s._gpsLocation.Latitude <= y.Latitude + width).ToList();
        if (height < 0) // Negative height values
            filteredSource = filteredSource.Where(s =>
                s._gpsLocation.Longitude <= y.Longitude && s._gpsLocation.Longitude >= y.Longitude + height).ToList();
        else // Positive height values
            filteredSource = filteredSource.Where(s =>
                s._gpsLocation.Longitude >= y.Longitude && s._gpsLocation.Longitude <= y.Longitude + height).ToList();

        return filteredSource;
    }
}