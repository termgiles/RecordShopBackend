using Microsoft.AspNetCore.Mvc;
using RecordShopBackend.Service;

namespace RecordShopBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordShopController : ControllerBase
    {
        private readonly IRecordShopService _service;
        public RecordShopController(IRecordShopService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Welcome()
        {
            return Ok(_service.ReturnWelcomeMessage());
        }

        [HttpGet("Albums")]
        public IActionResult GetAllAlbums()
        {
            return Ok(_service.ReturnAllAlbums());
        }

        [HttpGet("Album/{id}")]
        public IActionResult GetAlbumById(int id)
        {
            AlbumReturn result = _service.ReturnAlbumById(id);
            if (result.Found)
            {
                return Ok(result.ReturnedObject);
            }
            return NotFound();
        }

        [HttpPut("Album/{id}")]
        public IActionResult PutAlbumById(int id, AlbumModification ammendments)
        {
            AlbumReturn result = _service.AmmendAlbumById(id, ammendments);
            if (result.Found)
            {
                return Ok(result.ReturnedObject);
            }
            return NotFound();
        }

        [HttpDelete("Album/{id}")]
        public IActionResult DeleteAlbumById(int id)
        {
            bool found = _service.RemoveAlbumById(id);
            if (found)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPost("Album")]
        public IActionResult PostAlbum(Album album)
        {
            AlbumReturn result = _service.AddAlbum(album);
            if(result.Found && result.ReturnedObject != null)
            {
                return BadRequest($"album with Id {result.ReturnedObject.Id} already exists");
            }
            else if(!result.Found && result.ReturnedObject.Name != null)
            {
                return Created();
            }
            else
            {
                return BadRequest("Album not created, please try again");
            }

        }

        [HttpGet("Albums/Query")]
        public IActionResult QueryAlbums(string? name, string? artist, int? released, string? genre, string? information)
        {
            try
            {
                List<Album> result = _service.ParseQuery(name, artist, released, genre, information);
                if (result.Count > 0) return Ok(result);
                return NotFound("No albums matching the query were found.");
            }
            catch
            {
                return BadRequest("Invalid query paramerters.");
            }
        }

    }
}