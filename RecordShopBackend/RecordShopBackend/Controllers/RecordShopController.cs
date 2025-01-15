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
    }
}