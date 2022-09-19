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
    
    #region Sort by distance
    
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
    
    [Theory]
    [InlineData(-46.0, 0.0, "Your location is unknown, try a different location.")]
    [InlineData(0.0, 46.0, "Your location is unknown, try a different location.")]
    [InlineData(-45.1, 45.1, "Your location is unknown, try a different location.")]
    public void TestSortByDistanceInvalidData_ClientOutOfTheBoundsOfThePlane(double latitude, double longitude, string errorMsg)
    {
        // Arrange
        List<Entities.Shop> source = SupplyValidShops();
        GeoCoordinate geoCoordinate = new GeoCoordinate(latitude, longitude);

        // Act + Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _shopService.SortByDistance(source, geoCoordinate));
        Assert.Equal(errorMsg, ex.ParamName);
    }
    
    [Fact]
    public void TestSortByDistance_ValidAndInvalidShops()
    {
        // Arrange
        var theMixedList = new List<Entities.Shop>();
        var geoLocation = new GeoCoordinate(0.0, 0.0);
        var invalidShops = SupplyInvalidShops();
        var validShops = SupplyValidShops();
        
        for (int i = 0; i < 9; i++)
        {
            theMixedList.Add(invalidShops.ElementAt(i));
            theMixedList.Add(validShops.ElementAt(i));
        }
        
        // Act
        var result = _shopService.SortByDistance(theMixedList, geoLocation);

        //Assert
        Assert.Equal(validShops.Count,result.Count);
    }
    
    [Fact]
    public void TestSortByDistance_NullVallues()
    {
        // Arrange
        string errorMsg = "Something went wrong, try again later.";

        // Act + Assert
        var ex = Assert.Throws<NullReferenceException>(() => _shopService.SortByDistance(null, null));
        Assert.Equal(errorMsg, ex.Message);
    }
    
    [Fact]
    public void TestSortByDistance_EmptyListOfShops()
    {
        // Arrange
        var source = new List<Entities.Shop>();
        var geoLocation = new GeoCoordinate(0.0, 0.0);
        string errorMsg = "Unable to find shops near your location.";

        // Act + Assert
        var ex = Assert.Throws<ArgumentException>(() => _shopService.SortByDistance(source, geoLocation));
        Assert.Equal(errorMsg, ex.Message);
    }
    #endregion
    
    
    #region Filter by location
    
    [Theory]
    [InlineData(0.0, 0.0, 45, 45, 4)] // Quadrant I (+, +)
    [InlineData(0.0, 0.0, -45, 45, 3)] // Quadrant II (-, +)
    [InlineData(0.0, 0.0, -45, -45, 3)] // Quadrant III (-, -)
    [InlineData(0.0, 0.0, 45, -45, 2)] // Quadrant IV (+, -)
    [InlineData(-25.0, -25.0, 50, 50, 5)] // 25 km^2 of all quadrants
    [InlineData(-45.0, -45.0, 90, 90, 9)] // The whole plane
    public void TestFilterByLocation_ValidInput(double startX, double startY, int width, int height, int expected)
    {
        // Arrange
        var source = SupplyValidShops();
        var geoCoordinate = new GeoCoordinate(startX, startY);

        // Act
        var actual = _shopService.FilterByLocation(source, geoCoordinate, width, height);

        // Assert
        Assert.Equal(expected, actual.Count);
    }
    
    
    [Fact]
    public void TestFilterByLocation_NullVallues()
    {
        // Arrange
        string errorMsg = "Something went wrong, try again later.";

        // Act + Assert
        var ex = Assert.Throws<NullReferenceException>(() => _shopService.FilterByLocation(null, null, 45, 45));
        Assert.Equal(errorMsg, ex.Message);
    }
    
    [Fact]
    public void TestFilterByLocation_EmptyListOfShops()
    {
        // Arrange
        var source = new List<Entities.Shop>();
        var geoLocation = new GeoCoordinate(0.0, 0.0);
        string errorMsg = "Unable to find shops near your location.";

        // Act + Assert
        var ex = Assert.Throws<ArgumentException>(() => _shopService.FilterByLocation(source, geoLocation, 45, 45));
        Assert.Equal(errorMsg, ex.Message);
    }
    
    #endregion
    
    #region mock data

    static List<Entities.Shop> SupplyValidShops()
    {
        return new List<Entities.Shop>()
        {
            new Entities.Shop(){Name = "Pet World", website = "Petworld.dk", Address = "H.C Andersensvej 23", _gpsLocation = new Entities.GPSLocation(){Latitude = 0.0, Longitude = 0.0}},
            new Entities.Shop(){Name = "Bilka", website = "Bilka.dk", Address = "H.C Andersensvej 12", _gpsLocation = new Entities.GPSLocation(){Latitude = -40.0, Longitude = -45.0}},
            new Entities.Shop(){Name = "Black Rose", website = "Black-rose.dk", Address = "H.C Andersensvej 13", _gpsLocation = new Entities.GPSLocation(){Latitude = -45.0, Longitude = 40.0}},
            new Entities.Shop(){Name = "Normal", website = "Normal.dk", Address = "H.C Andersensvej 223", _gpsLocation = new Entities.GPSLocation(){Latitude = 34.5, Longitude = -45.0}},
            new Entities.Shop(){Name = "EASV", website = "EASV.dk", Address = "H.C Andersensvej 123", _gpsLocation = new Entities.GPSLocation(){Latitude = 45.0, Longitude = 45.0}},
            new Entities.Shop(){Name = "Støvsugeposer", website = "Støvsugeposer.dk", Address = "H.C Andersensvej 3", _gpsLocation = new Entities.GPSLocation(){Latitude = 22.5, Longitude = 1.0}},
            new Entities.Shop(){Name = "Handy Mand John Johnson", website = "Handymandesbjerg.dk", Address = "H.C Andersensvej 212", _gpsLocation = new Entities.GPSLocation(){Latitude = -5.0, Longitude = -5.0}},
            new Entities.Shop(){Name = "Fitness World", website = "Fitnessworld.dk", Address = "H.C Andersensvej 2323", _gpsLocation = new Entities.GPSLocation(){Latitude = -17.5, Longitude = 7.5}},
            new Entities.Shop(){Name = "Gamestop", website = "Gamestop.dk", Address = "H.C Andersensvej 2223", _gpsLocation = new Entities.GPSLocation(){Latitude = 7.5, Longitude = 7.5}},
        };
        
        
    }

    static List<Entities.Shop> SupplyInvalidShops()
    {
       return new List<Entities.Shop>()
        {
            new Entities.Shop(){Name = "Pet World", website = "Petworld.dk", Address = "H.C Andersensvej 23", _gpsLocation = new Entities.GPSLocation(){Latitude = 55.0, Longitude = 0.0}},
            new Entities.Shop(){Name = "Bilka", website = "Bilka.dk", Address = "H.C Andersensvej 12", _gpsLocation = new Entities.GPSLocation(){Latitude = -46.0, Longitude = -46.0}},
            new Entities.Shop(){Name = "Black Rose", website = "Black-rose.dk", Address = "H.C Andersensvej 13", _gpsLocation = new Entities.GPSLocation(){Latitude = -46.0, Longitude = 46.0}},
            new Entities.Shop(){Name = "Normal", website = "Normal.dk", Address = "H.C Andersensvej 223", _gpsLocation = new Entities.GPSLocation(){Latitude = 46.0, Longitude = -46.0}},
            new Entities.Shop(){Name = "EASV", website = "EASV.dk", Address = "H.C Andersensvej 123", _gpsLocation = new Entities.GPSLocation(){Latitude = 46.0, Longitude = 46.0}},
            new Entities.Shop(){Name = "Støvsugeposer", website = "Støvsugeposer.dk", Address = "H.C Andersensvej 3", _gpsLocation = new Entities.GPSLocation(){Latitude = 72.5, Longitude = 1.0}},
            new Entities.Shop(){Name = "Handy Mand John Johnson", website = "Handymandesbjerg.dk", Address = "H.C Andersensvej 212", _gpsLocation = new Entities.GPSLocation(){Latitude = -50.0, Longitude = -5.0}},
            new Entities.Shop(){Name = "Fitness World", website = "Fitnessworld.dk", Address = "H.C Andersensvej 2323", _gpsLocation = new Entities.GPSLocation(){Latitude = -17.5, Longitude = 75.5}},
            new Entities.Shop(){Name = "Gamestop", website = "Gamestop.dk", Address = "H.C Andersensvej 2223", _gpsLocation = new Entities.GPSLocation(){Latitude = 57.5, Longitude = 7.5}},
        };
    }
    
    #endregion
}