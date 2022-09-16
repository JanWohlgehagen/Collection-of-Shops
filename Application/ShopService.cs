using Domain;
using Domain.Interfaces;
using GeoCoordinatePortable;

namespace Application;

public class ShopService : IShopService
{
    public List<Entities.Shop> SortByDistance(List<Entities.Shop> source, GeoCoordinate x)
    {
        throw new NotImplementedException();
    }

    public List<Entities.Shop> FilterByLocation(List<Entities.Shop> source, GeoCoordinate x, int width, int height)
    {
        throw new NotImplementedException();
    }
}