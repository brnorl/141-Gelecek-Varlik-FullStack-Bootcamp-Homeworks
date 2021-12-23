using Microsoft.AspNetCore.Mvc;
using odev3.Service.Product;

namespace odev3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SortController : ControllerBase
    {
        private readonly IProductService productService;
        public SortController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpGet]
        [Route("Sort")]
        public IActionResult GetSortedProducts([FromQuery] string sortingParameter)
        {
            return Ok(productService.GetSortedProducts(sortingParameter));
        }
    }
}