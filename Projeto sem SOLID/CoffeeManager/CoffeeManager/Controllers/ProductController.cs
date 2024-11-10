using CoffeeManager.Model;
using CoffeeManager.Service;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManager.Controllers
{
    //Dependency Inversion Principle - DIP
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;

        private readonly ProductService _service;

        public ProductController(ILogger<ProductController> logger, ProductService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Product> products = _service.GetAll();
            return Ok(products);
        }

        [HttpGet("/id")]
        public IActionResult GetById(Guid id)
        {
            Product? products = _service.GetById(id);
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            _service.Add(product);
            return Ok();
        }
    }
}
