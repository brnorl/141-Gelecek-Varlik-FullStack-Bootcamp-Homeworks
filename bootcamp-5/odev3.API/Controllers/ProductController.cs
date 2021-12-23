using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using odev3.API.Attribute;
using odev3.DB.Models;
using odev3.Models.Product;
using odev3.Service.Product;

namespace odev3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductController(IProductService _productService, IMapper _mapper)
        {
            productService = _productService;
            mapper = _mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(LoginFilter))]
        public IActionResult Get()
        {
            return Ok(productService.Get());
        }

        [HttpPost]
        [Route("registerProduct")]
        [ServiceFilter(typeof(LoginFilter))]
        public IActionResult CreateProduct([FromBody] CreateProductModel newProduct)
        {
            var data = mapper.Map<Product>(newProduct);
            return Ok(productService.Insert(data));
        }
        [HttpPut]
        [Route("updateProduct")]
        [ServiceFilter(typeof(LoginFilter))]
        public IActionResult UpdateProduct([FromBody] UpdateProductModel updatedProduct, int id)
        {

            return Ok(productService.Update(updatedProduct, id));
        }

        [HttpDelete]
        [Route("deleteProduct")]
        [ServiceFilter(typeof(LoginFilter))]
        public IActionResult DeleteProduct(int id)
        {
            return Ok(productService.Delete(id));
        }



    }
}