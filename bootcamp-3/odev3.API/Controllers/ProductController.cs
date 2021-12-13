using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public ProductListModel<ProductViewModel> Get()
        {
            return productService.Get();
        }

        [HttpPost]
        [Route("registerProduct")]
        public bool CreateProduct([FromBody] CreateProductModel newProduct)
        {
            bool result;
            var data = mapper.Map<Product>(newProduct);
            result = productService.Insert(data);
            return result;
        }
        [HttpPut]
        [Route("updateProduct")]
        public bool UpdateProduct([FromBody] UpdateProductModel updatedProduct, int id)
        {

            return productService.Update(updatedProduct, id);
        }

        [HttpDelete]
        [Route("deleteProduct")]
        public bool DeleteProduct(int id)
        {
            return productService.Delete(id);
        }



    }
}