using Application;
using Domain;
using GeoCoordinatePortable;

namespace ShopServiceTest;

public class UnitTest1
{

    private ShopService _shopService;
    public UnitTest1()
    {
        _shopService = new ShopService();
    }
    
    [Theory]
    [InlineData(0.0, 0.0, 0, "Pet World")] // First Element
    [InlineData(0.0, 0.0, 1, "Handy Mand John Johnson")] // Second Element
    [InlineData(0.0, 0.0, 8, "EASV")] // Last Element
    public void TestSortByDistanceValidData(double latitude, double longitude, int index, string ShopName)
    {
        // Arrange
        List<Entities.Shop> source = SupplyValidShops();
        GeoCoordinate geoCoordinate = new GeoCoordinate(latitude, longitude);

        // Act
        List<Entities.Shop> actual = _shopService.SortByDistance(source, geoCoordinate);

        // Assert
        Assert.Equal(ShopName, actual.ElementAt(index).Name);
    }



    static List<Entities.Shop> SupplyValidShops()
    {
        return new List<Entities.Shop>()
        {
            new Entities.Shop(){Name = "Pet World", website = "Petworld.dk", Address = "H.C Andersensvej 23", _gpsLocation = new Entities.GPSLocation(){Latitude = 0.0, Longitude = 0.0}},
            new Entities.Shop(){Name = "Bilka", website = "Bilka.dk", Address = "H.C Andersensvej 12", _gpsLocation = new Entities.GPSLocation(){Latitude = -80.0, Longitude = -90.0}},
            new Entities.Shop(){Name = "Black Rose", website = "Black-rose.dk", Address = "H.C Andersensvej 13", _gpsLocation = new Entities.GPSLocation(){Latitude = -90.0, Longitude = 80.0}},
            new Entities.Shop(){Name = "Normal", website = "Normal.dk", Address = "H.C Andersensvej 223", _gpsLocation = new Entities.GPSLocation(){Latitude = 75.0, Longitude = -90.0}},
            new Entities.Shop(){Name = "EASV", website = "EASV.dk", Address = "H.C Andersensvej 123", _gpsLocation = new Entities.GPSLocation(){Latitude = 90.0, Longitude = 90.0}},
            new Entities.Shop(){Name = "Støvsugeposer", website = "Støvsugeposer.dk", Address = "H.C Andersensvej 3", _gpsLocation = new Entities.GPSLocation(){Latitude = 45.0, Longitude = 2.0}},
            new Entities.Shop(){Name = "Handy Mand John Johnson", website = "Handymandesbjerg.dk", Address = "H.C Andersensvej 212", _gpsLocation = new Entities.GPSLocation(){Latitude = -10.0, Longitude = -10.0}},
            new Entities.Shop(){Name = "Fitness World", website = "Fitnessworld.dk", Address = "H.C Andersensvej 2323", _gpsLocation = new Entities.GPSLocation(){Latitude = -35.0, Longitude = 15.0}},
            new Entities.Shop(){Name = "Gamestop", website = "Gamestop.dk", Address = "H.C Andersensvej 2223", _gpsLocation = new Entities.GPSLocation(){Latitude = 15.0, Longitude = 15.0}},
        };
        
        
    }

    static List<Entities.Shop> SupplyInvalidShops()
    {
       return new List<Entities.Shop>()
        {
            new Entities.Shop(){Name = "Pet World", website = "Petworld.dk", Address = "H.C Andersensvej 23", _gpsLocation = new Entities.GPSLocation(){Latitude = 0.0, Longitude = 0.0}},
            new Entities.Shop(){Name = "Bilka", website = "Bilka.dk", Address = "H.C Andersensvej 12", _gpsLocation = new Entities.GPSLocation(){Latitude = -91.0, Longitude = -90.0}},
            new Entities.Shop(){Name = "Black Rose", website = "Black-rose.dk", Address = "H.C Andersensvej 13", _gpsLocation = new Entities.GPSLocation(){Latitude = -90.0, Longitude = 91.0}},
            new Entities.Shop(){Name = "Normal", website = "Normal.dk", Address = "H.C Andersensvej 223", _gpsLocation = new Entities.GPSLocation(){Latitude = 90.0, Longitude = -90.0}},
            new Entities.Shop(){Name = "EASV", website = "EASV.dk", Address = "H.C Andersensvej 123", _gpsLocation = new Entities.GPSLocation(){Latitude = 92.0, Longitude = 90.0}},
            new Entities.Shop(){Name = "Støvsugeposer", website = "Støvsugeposer.dk", Address = "H.C Andersensvej 3", _gpsLocation = new Entities.GPSLocation(){Latitude = 45.0, Longitude = 2.0}},
            new Entities.Shop(){Name = "Handy Mand John Johnson", website = "Handymandesbjerg.dk", Address = "H.C Andersensvej 212", _gpsLocation = new Entities.GPSLocation(){Latitude = -10.0, Longitude = -10.0}},
            new Entities.Shop(){Name = "Fitness World", website = "Fitnessworld.dk", Address = "H.C Andersensvej 2323", _gpsLocation = new Entities.GPSLocation(){Latitude = -35.0, Longitude = 15.0}},
            new Entities.Shop(){Name = "Gamestop", website = "Gamestop.dk", Address = "H.C Andersensvej 2223", _gpsLocation = new Entities.GPSLocation(){Latitude = 95.0, Longitude = 15.0}},
        };
    }
}