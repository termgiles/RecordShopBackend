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
    }
}