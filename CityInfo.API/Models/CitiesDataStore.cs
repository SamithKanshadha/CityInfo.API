namespace CityInfo.API.Models;

public class CitiesDataStore
{
    public List<CityDto> Cities { get; set; }
    public static CitiesDataStore Current { get; } = new CitiesDataStore();

    public CitiesDataStore()
    {

        Cities = new List<CityDto>()
        {
            new CityDto()
            {
                Id = 1,
                Name = "New York",
                Description = "New York City",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 1,
                        Name = "Central Park",
                        Description = "The most visited park",
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2,
                        Name = "Empire State Building",
                        Description = "Iconic skyscraper with city views",
                    }
                }
            },

            new CityDto()
            {
                Id = 2,
                Name = "Los Angeles",
                Description = "City of Angels",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 3,
                        Name = "Hollywood Sign",
                        Description = "Iconic landmark in the hills",
                    },
                    new PointOfInterestDto()
                    {
                        Id = 4,
                        Name = "Venice Beach",
                        Description = "Famous beach with boardwalk",
                    }
                }
            },
            new CityDto()
            {
                Id = 3,
                Name = "Chicago",
                Description = "The Windy City",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 5,
                        Name = "Millennium Park",
                        Description = "Home to the Cloud Gate sculpture",
                    },
                    new PointOfInterestDto()
                    {
                        Id = 6,
                        Name = "Willis Tower",
                        Description = "Skyscraper with a skydeck",
                    }
                }
            },
            new CityDto()
            {
                Id = 4,
                Name = "Houston",
                Description = "Space City",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 7,
                        Name = "Space Center Houston",
                        Description = "Museum with space exhibits",
                    },
                    new PointOfInterestDto()
                    {
                        Id = 8,
                        Name = "Houston Zoo",
                        Description = "A large zoo with diverse species",
                    }
                }
            }
        };

    }
}
