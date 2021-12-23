using Microsoft.AspNetCore.Mvc;
using odev3.Models.Filters;
using odev3.Service.Product;

namespace odev3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FilterController : ControllerBase
    {
        private readonly IProductService productService;

        public FilterController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpGet]
        [Route("Product")]
        public IActionResult GetFilteredProduct([FromQuery] ProductParameters productParameters)
        {//controller içinde sadece basit kontrolü yaptım.
            var list = productService.Get().productList;
            if (productParameters.minPrice > productParameters.maxPrice)
            {
                return BadRequest("Invalid values.");
            }
            return Ok(productService.GetFilteredProducts(productParameters.maxPrice, productParameters.minPrice, list));
        }

    }
}