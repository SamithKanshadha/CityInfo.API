using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/cities/{cityId}/pointsofinterest")]

public class PointsOfInterestController : Controller
{
    private readonly ILogger<PointsOfInterestController> _logger;

    public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    [HttpGet]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
    {
        throw new Exception("Invalid request");
        try
        {
            
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                _logger.LogInformation("City {CityId} was not found", cityId);

                return NotFound();
            }

            return Ok(city.PointsOfInterest);

        }
        catch (Exception e)
        {
            _logger.LogCritical($"Error while getting ID {cityId}", e);
            return StatusCode(500,"A error occurred while getting the points of interest list");
        }
    }

    [HttpGet("{pointOfInterestId}" , Name = "GetPointOfInterest")]
    public ActionResult<PointOfInterestDto> GetPointOfInterest( int cityId,int pointOfInterestId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault((c=>c.Id == cityId));

        if (city == null)
        {
            return NotFound();
        }
          
        var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

        if (pointOfInterest == null)
        {
            return NotFound();
        }
        
        return Ok(pointOfInterest);
    }

    [HttpPost]
    public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId, PointOfInterestForCreationDto pointOfInterest)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }
           
        var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

        var finalPointOfInterest = new PointOfInterestDto()
        {
            Id = ++maxPointOfInterestId,
            Name = pointOfInterest.Name,
            Description = pointOfInterest.Description
        };
            city.PointsOfInterest.Add(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest", 
                new
                {
                    cityId,
                    pointOfInterestId = finalPointOfInterest.Id
                }
                ,finalPointOfInterest);
            
            //return Created Route Structure
            //return CreatedAtRoute(
            //     routeName,          // The name of the route that will handle the GET request for this resource
            //     routeValues,        // The values that will be used to construct the URI (e.g., cityId, pointOfInterestId)
            //     resource            // The actual resource data being returned in the response body
            // );
            
        
    }
    
    [HttpPut("{pointOfInterestId}")]
    public ActionResult UpdatePointOfInterest(int cityId,int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
           return NotFound();
        }
        var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        if(pointOfInterestFromStore == null)
        {
           return NotFound();
        }

        pointOfInterestFromStore.Name = pointOfInterest.Name;
        pointOfInterestFromStore.Description = pointOfInterest.Description;
        
        return NoContent();
    }

    [HttpPatch("{pointOfInterestId}")]
    public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
        JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }
        var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        if (pointOfInterestFromStore == null)
        {
            return NotFound();
        }

        var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
        {
            Name = pointOfInterestFromStore.Name,
            Description = pointOfInterestFromStore.Description
        };
        patchDocument.ApplyTo(pointOfInterestToPatch,ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!TryValidateModel(pointOfInterestToPatch))
        {
            return BadRequest(ModelState);
        }
            
        pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
        pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;
        return NoContent();
    }

    [HttpDelete("{pointOfInterestId}")]
    public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }
        var cityPointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        if (cityPointOfInterest == null)
        {
            return NotFound();
        }
        
        city.PointsOfInterest.Remove(cityPointOfInterest);
        return NoContent();
        
    }
}
