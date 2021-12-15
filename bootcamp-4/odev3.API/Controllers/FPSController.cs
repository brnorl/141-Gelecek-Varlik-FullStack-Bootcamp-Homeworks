using Microsoft.AspNetCore.Mvc;
using odev3.Models.Filters;
using odev3.Models.Pagination;
using odev3.Service.Product;

namespace odev3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FPSController : ControllerBase
    {
        private readonly IProductService productService;

        public FPSController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpGet]
        [Route("FPS")]
        public IActionResult GetProductsFPS([FromQuery] PaginationFilter filter, int maxPrice, int minPrice, string sortingParameter)
        {
            return Ok(productService.GetFPS(sortingParameter, maxPrice, minPrice, filter.PageNumber, filter.PageSize));
        }

    }
}