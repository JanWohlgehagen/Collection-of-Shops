using GeoCoordinatePortable;

namespace Domain.Interfaces;

using Domain;

public interface IShopService
{
    List<Entities.Shop> SortByDistance(List<Entities.Shop> source, GeoCoordinate x);
    
    List<Entities.Shop> FilterByLocation(List<Entities.Shop> source, GeoCoordinate x, int width, int height);
}