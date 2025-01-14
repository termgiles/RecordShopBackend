using Microsoft.AspNetCore.Mvc;
using RecordShopBackend.Service;

namespace RecordShopBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordShopController : ControllerBase
    {
        private readonly RecordShopService _service;
        public RecordShopController(RecordShopService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Welcome()
        {
            return Ok(_service.ReturnWelcomeMessage());
        }
    }
}