using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/files")]
public class FilesController : Controller
{
    
    [HttpGet("{fileId}")]
    public ActionResult GetFile(string fileId)
    {
        var pathToFile = "img.jpg";
        if (!System.IO.File.Exists(pathToFile))
        {
            return NotFound();
        }
        var bytes = System.IO.File.ReadAllBytes(pathToFile);
        return File(bytes, "image/jpeg",Path.GetFileName(pathToFile));
    }

    [HttpPost]
    public async Task<ActionResult> PostFile(IFormFile file)
    {
        //validation of file
        if (file.Length == 0 || file.Length > 20971520 || file.ContentType != "application/pdf")
        {
            return BadRequest("Not a valid file");
        }
        var path = Path.Combine(Directory.GetCurrentDirectory(), $"uploaded_file_{Guid.NewGuid()}.pdf");
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return Ok("File uploaded");
    }
    
}